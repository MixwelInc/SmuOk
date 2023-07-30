alter PROCEDURE [dbo].[transBoLAmount]     
 @srcId bigint,
 @dstId bigint,
 @transAmount decimal(18,4)

as
begin
	set nocount on; 

	declare @amountAvailable decimal(18, 4)
	declare @checkInv int
	declare @bolDocFillingId bigint

	select @amountAvailable = SFBQtyForTSK from SpecFillBol where SFBId = @srcId

	select @bolDocFillingId = sfb.BoLDocFillingId
	from SpecFillBol sfb
	where sfb.SFBId = @srcId

	select @checkInv = count(*)
	from InvCfm ic 
	join InvDocFilling_new idf on idf.InvDocPosId = ic.InvDocPosId 
	join BolDocFilling bdf on bdf.InvDocPosId = idf.InvDocPosId 
	where ic.ICFill = @dstId and bdf.BoLDocFillingId = @bolDocFillingId

	if @checkInv = 0
	begin
		select 'Ошибка! Необходимо разнести данные на вкладке "Согласование счета"'
		return
	end
	else if @amountAvailable < @transAmount	
	begin
		select 'Ошибка! Объем, который вы пытаетесь перераспределить больше, чем доступно для указанного идентификатора.'
		return
	end
	else if @amountAvailable = @transAmount
	begin
		update SpecFillBol
		set SFBFill = @dstId
		where SFBId = @srcId

		select 'Объем перераспределен'
		return
	end
	else
	begin
		declare @qtyLeft decimal(18, 4)
		declare @sfbRecipient nvarchar(max)
		declare @sfbShipmentPlace nvarchar(max)

		set @qtyLeft = @amountAvailable - @transAmount

		update SpecFillBol
		set SFBQtyForTSK = @qtyLeft
		where SFBId = @srcId

		select @sfbRecipient = sfb.SFBRecipient, @sfbShipmentPlace = sfb.SFBShipmentPlace
		from SpecFillBol sfb
		where SFBId = @srcId

		insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
		values (@dstId, @transAmount, @sfbRecipient, @sfbShipmentPlace, @bolDocFillingId)

		select 'Объем перераспределен'
		return
	end
end
