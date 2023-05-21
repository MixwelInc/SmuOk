create PROCEDURE [dbo].[uspReport_PriceApprovement_v1]           
 @spec nvarchar(max)          
          
AS          
BEGIN          
select
vwsf.SFId [-3],
ic.ICId [-2],
sfb2.SFBId [-1],
bf.BFId [0],
vwsf.SFNo+'.'+vwsf.SFNo2 [1],
isnull(BFCode, null) [2],
vwsf.SFName as [3],
coalesce(sfb2.SFBName,ICName) as [4],
vwsf.SFUnit as [5],
vwsf.SFQty as [6],
null [7],
null [8],
BFKoeff [9],
case when isnull(BFQty,0) != 0 then cast(BFSum / BFQty as decimal(18, 4)) else null end as [10],
null [11],
ICK [12],
coalesce(sfb2.SFBPriceWONDS, ic.ICPrc) [13],
null [14],
null [15],
null [16],
'№ ' + coalesce(sfb2.SFBBoLNoFromTSK, InvNum) + ' от ' + cast(convert(date, coalesce(sfb2.SFBBoLDateFromTSK, InvDate), 106) as nvarchar) [17] 
from vwSpecFill vwsf 
left join BudgetFill bf on bf.SpecFillId = vwsf.SFId 
left join Budget b on b.BId = bf.BudgId 
left join SpecFillBol sfb2 on sfb2.SFBFill = vwsf.SFId 
left join InvCfm ic on ic.ICFill = vwsf.SFId and sfb2.SFBId is null
left join InvDoc id on id.InvId = ic.InvDocId 
where 1 = 1 and (ic.ICId is not NULL or sfb2.SFBId is not NULL) and vwsf.SVId=@spec 
order by cast(vwsf.SFNo as bigint), cast(vwsf.SFNo2 as bigint) 

end;