use SmuOkk
GO
alter trigger BoLDocFilling_INSERT
on BoLDocFilling
after insert
as

	declare @BoLAmount decimal(18,4)
	declare @AmountToBeSet decimal(18,4)
	declare @BoLId bigint
	declare @BoLDocFillingId bigint
	declare @AmountToBeFilledForRow decimal(18,4)
	declare @SpecId bigint

	select @BoLAmount = Amount, @BoLId = BoLDocId, @BoLDocFillingId = BoLDocFillingId
	from BoLDocFilling
	where BoLDocFillingId in (select BoLDocFillingId from inserted)

	select @AmountToBeSet = sum(isnull(ic.ICQty, 0) - isnull(q.sum_filled, 0))
	from InvCfm ic
	join InvDocFilling_new idf on idf.InvDocPosId = ic.InvDocPosId
	outer apply(select sum(SFBQtyForTSK) as sum_filled from SpecFillBoL where SFBFill = ic.ICFill)q
	where idf.InvDocPosId in (select i.InvDocPosId from inserted i) and idf.InvDocPosId is not null

	if(@BoLAmount <= 0) 
		return
	else if(@AmountToBeSet <= 0) 
		return
	else
		begin

			select top(1) @SpecId = SId
			from vwSpecFill vwsf
			join InvCfm ic on ic.ICFill = vwsf.SFId
			join InvDocFilling_new idf on idf.InvDocPosId = ic.InvDocPosId
			where idf.InvDocPosId in (select i.InvDocPosId from inserted i) and idf.InvDocPosId is not null

			drop table if exists #tmp_RowsToFill

			create table #tmp_RowsToFill
			(id bigint identity,
			FillId bigint,
			AmountToFill decimal(18,4),
			InvDocPosId bigint,
			InvDocId bigint
			)

			insert into #tmp_RowsToFill
			select ic.ICFill, ic.ICQty - isnull(q.sum_filled, 0), ic.InvDocPosId, idf.InvDocId
			from InvCfm ic
			join InvDocFilling_new idf on idf.InvDocPosId = ic.InvDocPosId
			outer apply(select sum(SFBQtyForTSK) as sum_filled from SpecFillBoL where SFBFill = ic.ICFill)q
			where idf.InvDocPosId in (select i.InvDocPosId from inserted i) and ic.ICQty - isnull(q.sum_filled, 0) > 0

			declare @RowsToBeFilled_cnt int

			select @RowsToBeFilled_cnt = count(*)
			from #tmp_RowsToFill

			if(@RowsToBeFilled_cnt = 0) --тут можно теоретически можно заносить в складские остатки (если счет заполнен и все разнесено)
				return
			else if(@RowsToBeFilled_cnt = 1)
				begin
					insert into SpecWarehouse (SpecId)
					values (@SpecId)

					select @AmountToBeFilledForRow = AmountToFill
					from #tmp_RowsToFill

					if(@AmountToBeFilledForRow >= @BoLAmount)
						begin
							insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
							select FillId, @BoLAmount, bd.Recipient, bd.ShipmentPlace, @BoLDocFillingId
							from #tmp_RowsToFill rtf
							join BolDoc bd on bd.InvDocId = rtf.InvDocId and bd.BoLDocId = @BoLId
						end
					else
						begin
							insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
							select FillId, @AmountToBeFilledForRow, bd.Recipient, bd.ShipmentPlace, @BoLDocFillingId
							from #tmp_RowsToFill rtf
							join BolDoc bd on bd.InvDocId = rtf.InvDocId and bd.BoLDocId = @BoLId
						end
				end
			else if(@RowsToBeFilled_cnt > 1)
				begin
					while(@BoLAmount > 0 and @RowsToBeFilled_cnt > 0)
						begin
							declare @CurrentRowId bigint

							select @CurrentRowId = FillId, @AmountToBeFilledForRow = AmountToFill
							from #tmp_RowsToFill
							where FillId = (select min(fillid)
											from #tmp_RowsToFill)

							if(@AmountToBeFilledForRow >= @BoLAmount)
								begin
									insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
									select FillId, @BoLAmount, bd.Recipient, bd.ShipmentPlace, @BoLDocFillingId
									from #tmp_RowsToFill rtf
									join BolDoc bd on bd.InvDocId = rtf.InvDocId and bd.BoLDocId = @BoLId
									where rtf.FillId = @CurrentRowId
									set @BoLAmount = 0
								end
							else
								begin
									insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
									select FillId, @AmountToBeFilledForRow, bd.Recipient, bd.ShipmentPlace, @BoLDocFillingId
									from #tmp_RowsToFill rtf
									join BolDoc bd on bd.InvDocId = rtf.InvDocId and bd.BoLDocId = @BoLId
									where rtf.FillId = @CurrentRowId
									set @BoLAmount = @BoLAmount - @AmountToBeFilledForRow
								end

							delete 
							from #tmp_RowsToFill
							where FillId = @CurrentRowId

							select @RowsToBeFilled_cnt = count(*)
							from #tmp_RowsToFill
						end
				end
		end