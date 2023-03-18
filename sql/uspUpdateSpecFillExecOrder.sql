alter procedure [dbo].[uspUpdateSpecFillExecOrder]    
  (@entityId bigint ,@id bigint,@parent bigint,@date datetime, @qty decimal(18,4), @newPost int, @fill bigint, @address nvarchar(MAX), @response nvarchar(MAX))    
as    
begin   
declare @newSOId bigint
declare @newICId bigint
   if not exists (select * from SpecFillExecOrder where SFEOId=@id)    
   begin    
       insert into SpecFillExecOrder (SFEOSpecFillExec, SFEOQty, SFEOStartDate, SFEOAddress, SFEOResponse)    
       values (@parent, @qty, @date, @address, @response)  
    /*select @id = SCOPE_IDENTITY()  
    insert into SupplyOrder(SOFILL, SOOrderNumPref, SOOrderDate, SOOrderID)   
    values (@fill, @newPost, GETDATE(), @id)
	select @newSOId = SCOPE_IDENTITY()
	insert into InvCfm (ICFill, ICOrderId, SOId)
	values (@fill,@id,@newSOId)
	select @newICId = SCOPE_IDENTITY()
	insert into BudgetFill (SpecFillId,ICId) values(@fill,@newICId)
    update SpecFillExecOrder set SFEONum = concat(@entityId , '-' , @newPost) where SFEOSpecFillExec = @parent */ 
   end    
    
  else    
  begin    
  update SpecFillExecOrder set SFEOQty=@qty, SFEOStartDate=@date, SFEOAddress = @address, SFEOResponse = @response where SFEOId=@id;  
  /*update SupplyOrder set SOOrderNumPref=@newPost, SOOrderDate=GETDATE() where SOOrderID = @id;  
  update SpecFillExecOrder set SFEONum = concat(@entityId , '-' , @newPost) where SFEOSpecFillExec = @parent  */
  end   
end 