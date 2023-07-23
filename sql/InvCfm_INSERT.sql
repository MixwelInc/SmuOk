use SmuOkk
GO
alter trigger InvCfm_INSERT
on InvCfm
after insert
as

	declare @BoLAmount decimal(18,4)
	declare @AmountToBeSet decimal(18,4)
	declare @BoLId bigint
	declare @AmountAvailableToFill decimal(18,4)
	declare @SpecId bigint

	select top(1) @SpecId = SId
	from vwSpecFill
	where SFId in (select icfill from inserted)

	select @BoLAmount = sum(bdf.Amount - isnull(done_qty, 0))
	from BoLDocFilling bdf
	outer apply(select sum(sfb.SFBQtyForTSK) as done_qty from SpecFillBol sfb where sfb.BoLDocFillingId = bdf.BoLDocFillingId) q
	where bdf.InvDocPosId in (select i.InvDocPosId from inserted i)

	select @AmountToBeSet = i.ICQty
	from inserted i

	if(@BoLAmount <= 0) 
		return
	else if(@AmountToBeSet <= 0) 
		return
	else
		begin

			drop table if exists #tmp_SrcToFill

			create table #tmp_SrcToFill
			(id bigint identity,
			BolDocId bigint,
			BolDocFillingId bigint,
			AmountAvailable decimal(18,4)
			)

			insert into #tmp_SrcToFill
			select bdf.BoLDocId, bdf.BoLDocFillingId, bdf.Amount - isnull(done_qty, 0)
			from BoLDocFilling bdf
			outer apply(select sum(sfb.SFBQtyForTSK) as done_qty from SpecFillBol sfb where sfb.BoLDocFillingId = bdf.BoLDocFillingId) q
			where bdf.InvDocPosId in (select i.InvDocPosId from inserted i)

			declare @SrcToFill_cnt int

			select @SrcToFill_cnt = count(*)
			from #tmp_SrcToFill

			if(@SrcToFill_cnt = 0) 
				return
			else if(@SrcToFill_cnt = 1)
				begin
			
					select @AmountAvailableToFill = AmountAvailable
					from #tmp_SrcToFill

					if(@AmountAvailableToFill >= @AmountToBeSet)
						begin
							insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
							select i.ICFill, @AmountToBeSet, bd.Recipient, bd.ShipmentPlace, bdf.BoLDocFillingId
							from #tmp_SrcToFill rtf
							join BolDocFilling bdf on bdf.BoLDocFillingId = rtf.BolDocFillingId
							join inserted i on i.InvDocPosId = bdf.InvDocPosId
							join BoLDoc bd on bd.BoLDocId = bdf.BoLDocId
						end
					else
						begin
							insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
							select i.ICFill, @AmountAvailableToFill, bd.Recipient, bd.ShipmentPlace, bdf.BoLDocFillingId
							from #tmp_SrcToFill rtf
							join BolDocFilling bdf on bdf.BoLDocFillingId = rtf.BolDocFillingId
							join inserted i on i.InvDocPosId = bdf.InvDocPosId
							join BoLDoc bd on bd.BoLDocId = bdf.BoLDocId
						end
				end
			else if(@SrcToFill_cnt > 1)
				begin
					while(@SrcToFill_cnt > 0 and @AmountToBeSet > 0)
						begin
							declare @CurrentRowId bigint

							select top(1) @CurrentRowId = BolDocFillingId, @AmountAvailableToFill = AmountAvailable
							from #tmp_SrcToFill
							order by BoLDocFillingId asc


							if(@AmountAvailableToFill >= @AmountToBeSet)
								begin
									insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
									select i.ICFill, @AmountToBeSet, bd.Recipient, bd.ShipmentPlace, bdf.BoLDocFillingId
									from #tmp_SrcToFill rtf
									join BolDocFilling bdf on bdf.BoLDocFillingId = rtf.BolDocFillingId
									join inserted i on i.InvDocPosId = bdf.InvDocPosId
									join BoLDoc bd on bd.BoLDocId = bdf.BoLDocId
									where rtf.BolDocFillingId = @CurrentRowId
									set @AmountToBeSet = 0
								end
							else
								begin
									insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
									select i.ICFill, @AmountAvailableToFill, bd.Recipient, bd.ShipmentPlace, bdf.BoLDocFillingId
									from #tmp_SrcToFill rtf
									join BolDocFilling bdf on bdf.BoLDocFillingId = rtf.BolDocFillingId
									join inserted i on i.InvDocPosId = bdf.InvDocPosId
									join BoLDoc bd on bd.BoLDocId = bdf.BoLDocId
									where rtf.BolDocFillingId = @CurrentRowId
									set @AmountToBeSet = @AmountToBeSet - @AmountAvailableToFill
								end

							delete 
							from #tmp_SrcToFill
							where BolDocFillingId = @CurrentRowId

							select @SrcToFill_cnt = count(*)
							from #tmp_SrcToFill
						end
				end

				insert into SpecWarehouse (SpecId)
				values (@SpecId)
		end