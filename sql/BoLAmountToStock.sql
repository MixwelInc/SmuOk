ALTER PROCEDURE [dbo].[BoLAmountToStock]     
 @invDocId bigint

as
begin
	set nocount on; 

	insert into wh_stock (Surname, Num, Name, Code, Unit, Price, Amount, Total, BoLDocFillingId)
	select 'автомат', 777, idf.Name, concat(bd.Num, '-', bdf.No1), idf.Unit, idf.PriceWOVAT, bdf.Amount - isnull(q.done_qty, 0) - isnull(wh.Amount, 0), idf.PriceWOVAT * (bdf.Amount - isnull(q.done_qty, 0) - isnull(wh.Amount, 0)), bdf.BoLDocFillingId
	from InvDoc id 
	join InvDocFilling_new idf on id.InvId = idf.InvDocId
	join BoLDocFilling bdf on bdf.InvDocPosId = idf.InvDocPosId
	join BoLDoc bd on bd.BoLDocId = bdf.BoLDocId
	join wh_stock wh on wh.BoLDocFillingId = bdf.BoLDocFillingId
	outer apply(select sum(sfb.SFBQtyForTSK) as done_qty from SpecFillBol sfb where sfb.BoLDocFillingId = bdf.BoLDocFillingId) q
	where id.InvId = @invDocId and bdf.Amount - isnull(q.done_qty, 0) > 0
end
