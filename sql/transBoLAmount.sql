CREATE PROCEDURE [dbo].[transBoLAmount]     
 @srcId bigint,
 @dstId bigint,
 @transAmount decimal(18,4)

as
begin
  set nocount on; 

  declare @amountAvailable decimal(18, 4)
  select @amountAvailable = SFBQtyForTSK from SpecFillBol where SFBId = @srcId

  if @amountAvailable < @transAmount	return
  else if @amountAvailable = @transAmount
  begin
	update SpecFillBol
	set SFBFill = @dstId
	where SFBId = @srcId
  end
  else
  begin
	declare @qtyLeft decimal(18, 4)
	declare @sfbRecipient nvarchar(max)
	declare @sfbShipmentPlace nvarchar(max)
	declare @bolDocFillingId bigint

	set @qtyLeft = @amountAvailable - @transAmount

	update SpecFillBol
	set SFBQtyForTSK = @qtyLeft
	where SFBId = @srcId

	insert into SpecFillBol (SFBFill, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, BoLDocFillingId)
	values (@dstId, @transAmount, @sfbRecipient, @sfbShipmentPlace, @bolDocFillingId)
  end
end
