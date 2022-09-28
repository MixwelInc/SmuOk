alter procedure uspSpecFillExecOrderDataFix
  (@entityId bigint ,@id bigint,@parent bigint,@date datetime, @qty decimal(18,4), @newPost int, @fill bigint,@SOResponsOS nvarchar(MAX)
  ,@SOPlan1CNum nvarchar(MAX),@SO1CPlanDate datetime,@SOOrderDocId bigint,@SORealNum nvarchar(MAX),@ICINN numeric(12,0),@ICNo nvarchar(MAX)
  ,@ICDate datetime,@ICRowNo int,@ICName nvarchar(MAX),
@ICUnit nvarchar(MAX),@ICQty decimal(18,4),@ICPrc decimal(18,2),@ICK decimal(18,4),@SFSupplyDate1C datetime,@SFLegalName nvarchar(MAX),
@SFDocType nvarchar(50),@SFDaysUntilSupply int,@IC1SOrderNo nvarchar(MAX),@InvDocId bigint)    
as    
begin   
   if not exists (select * from SpecFillExecOrder where SFEOId=@id)    
   begin    
       insert into SpecFillExecOrder (SFEOSpecFillExec, SFEOQty, SFEOStartDate)    
       values (@parent, @qty, @date)  
    select @id = SCOPE_IDENTITY()  
    insert into SupplyOrder(SOFILL, SOOrderNumPref, SOOrderDate, SOOrderID,SOResponsOS,SOPlan1CNum,SO1CPlanDate,SOOrderDocId,SORealNum)   
    values (@fill, @newPost, GETDATE(), @id,@SOResponsOS,@SOPlan1CNum,@SO1CPlanDate,@SOOrderDocId,@SORealNum)  
	insert into InvCfm (ICFill, ICOrderId,ICINN, ICNo,ICDate,ICRowNo,ICName,
ICUnit,ICQty,ICPrc,ICK,SFSupplyDate1C,SFLegalName,
SFDocType,SFDaysUntilSupply,IC1SOrderNo,InvDocId)
	values (@fill,@id,@ICINN,@ICNo,@ICDate,@ICRowNo,@ICName,
@ICUnit,@ICQty,@ICPrc,@ICK,@SFSupplyDate1C,@SFLegalName,
@SFDocType,@SFDaysUntilSupply,@IC1SOrderNo,@InvDocId)
    update SpecFillExecOrder set SFEONum = concat(@entityId , '-' , @newPost) where SFEOSpecFillExec = @parent  
   end    
    
  else    
  begin    
  update SpecFillExecOrder set SFEOQty=@qty, SFEOStartDate=@date where SFEOId=@id;  
  update SupplyOrder set SOOrderNumPref=@newPost, SOOrderDate=GETDATE() where SOOrderID = @id;  
  update SpecFillExecOrder set SFEONum = concat(@entityId , '-' , @newPost) where SFEOSpecFillExec = @parent  
  end    
    
end 

declare @entityId bigint ,@id bigint,@parent bigint,@date datetime, @qty decimal(18,4), @newPost int, @fill bigint,
		@SOResponsOS nvarchar(MAX),@SOPlan1CNum nvarchar(MAX),@SO1CPlanDate datetime,@SOOrderDocId bigint,@SORealNum nvarchar(MAX),
		@ICINN numeric(12,0),@ICNo nvarchar(MAX),@ICDate datetime,@ICRowNo int,@ICName nvarchar(MAX),@ICUnit nvarchar(MAX),
		@ICQty decimal(18,4),@ICPrc decimal(18,2),@ICK decimal(18,4),@SFSupplyDate1C datetime,@SFLegalName nvarchar(MAX),
		@SFDocType nvarchar(50),@SFDaysUntilSupply int,@IC1SOrderNo nvarchar(MAX),@InvDocId bigint;

declare the_cursor cursor fast_forward
for
select vws.sid as entityId
,-1 as id
,sfeo.SFEOSpecFillExec as parent
,SFEOStartDate as date
,SFEOQty as qty
,so.SOOrderNumPref as newPost
,vws.sfid as fill
,SOResponsOS,SOPlan1CNum,SO1CPlanDate,SOOrderDocId,SORealNum
,ICINN, ICNo,ICDate,ICRowNo,ICName,
ICUnit,ICQty,ICPrc,ICK,SFSupplyDate1C,SFLegalName,
SFDocType,SFDaysUntilSupply,IC1SOrderNo,InvDocId
from SpecFillExecOrder sfeo
inner join SpecFillExec sfe on sfe.SFEId = sfeo.SFEOSpecFillExec
left join InvCfm ic on ic.ICFill = sfe.SFEFill
left join SupplyOrder so on so.SOFill = sfe.SFEFill
left join vwSpecFill vws on vws.SFId = sfe.SFEFill
where ic.SFOrderDate < '26-09-2022'

open the_cursor

	fetch next from the_cursor
	into @entityId, @id, @parent, @date, @qty, @newPost, @fill, @SOResponsOS, @SOPlan1CNum, @SO1CPlanDate, @SOOrderDocId, @SORealNum,
		@ICINN, @ICNo, @ICDate, @ICRowNo, @ICName, @ICUnit, @ICQty, @ICPrc, @ICK, @SFSupplyDate1C, @SFLegalName,
		@SFDocType, @SFDaysUntilSupply, @IC1SOrderNo, @InvDocId

	while @@FETCH_STATUS = 0
	begin
		exec uspSpecFillExecOrderDataFix @entityId, @id, @parent, @date, @qty, @newPost, @fill,@SOResponsOS, @SOPlan1CNum, @SO1CPlanDate, @SOOrderDocId, @SORealNum,
		@ICINN, @ICNo, @ICDate, @ICRowNo, @ICName, @ICUnit, @ICQty, @ICPrc, @ICK, @SFSupplyDate1C, @SFLegalName,
		@SFDocType, @SFDaysUntilSupply, @IC1SOrderNo, @InvDocId
		fetch next from the_cursor
		into @entityId, @id, @parent, @date, @qty, @newPost, @fill, @SOResponsOS, @SOPlan1CNum, @SO1CPlanDate, @SOOrderDocId, @SORealNum,
		@ICINN, @ICNo, @ICDate, @ICRowNo, @ICName, @ICUnit, @ICQty, @ICPrc, @ICK, @SFSupplyDate1C, @SFLegalName,
		@SFDocType, @SFDaysUntilSupply, @IC1SOrderNo, @InvDocId
	end

close the_cursor
deallocate the_cursor
