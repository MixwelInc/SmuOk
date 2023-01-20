alter procedure NZP_minus_doc
	@num nvarchar(max)
	, @dt nvarchar(max)
as
begin
	set nocount on;
	declare @new_NZPId bigint;
	declare @new_DoneHeaderId bigint;
	insert into NZPDoc (ZP,EM,ZPm,TMC,DTMC,HPotZP,SPotZP,HPandSPotZPm,VZIS,ZTR,CalcNZPNum
      ,CalcNZPDate,MntMaster,downKoefSMRPNR,downKoefTMC,subDownKoefSMRPNR,subDownKoefTMC
      ,BudgId,SpecId,Note)
	select ZP * (-1)
      ,EM * (-1)
      ,ZPm * (-1)
      ,TMC * (-1)
      ,DTMC * (-1)
      ,HPotZP * (-1)
      ,SPotZP * (-1)
      ,HPandSPotZPm * (-1)
      ,VZIS * (-1)
      ,ZTR * (-1)
      ,concat('M-',CalcNZPNum)
      ,CONVERT (date, @dt)
      ,MntMaster
      ,downKoefSMRPNR
      ,downKoefTMC
      ,subDownKoefSMRPNR
      ,subDownKoefTMC
      ,BudgId
      ,SpecId
      ,Note
	from NZPDoc
	where CalcNZPNum = @num

	select @new_NZPId = SCOPE_IDENTITY()

	insert into NZPFill (NZPId,SpecFillExecId,NFSum,NFNote,SpecFillId)
	select @new_NZPId
      ,SpecFillExecId
      ,NFSum * (-1)
      ,NFNote
      ,SpecFillId
	  from NZPFill f
	  inner join NZPDoc d on d.NZPId = f.NZPId
	  where CalcNZPNum = @num

	  insert into DoneHeader (DHdate,DHSpecTitle)
	  select distinct CONVERT (date, @dt),DHSpecTitle
	  from DoneHeader dh
	  inner join Done d on d.DHeader = dh.DHId
	  inner join NZPFill f on f.SpecFillExecId = d.DSpecExecFill
	  inner join NZPDoc dd on dd.NZPId = f.NZPId
	  where dd.CalcNZPNum = @num

	  select @new_DoneHeaderId = SCOPE_IDENTITY()

	  insert into Done (DSpecExecFill, DDate, DQty, DHeader, DCaption)
	  select  DSpecExecFill
	  , CONVERT (date, @dt)
	  , DQty * (-1)
	  , @new_DoneHeaderId
	  , DCaption
	  from Done d
	  inner join NZPFill f on f.SpecFillExecId = d.DSpecExecFill
	  inner join NZPDoc dd on dd.NZPId = f.NZPId
	  where dd.CalcNZPNum = @num
end;