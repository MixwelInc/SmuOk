alter PROCEDURE [dbo].[uspReport_KS2_v16]         
 @spec nvarchar(max)/*=1405*/        
        
AS        
BEGIN        
drop table if exists #datatbl;        
declare @do_report bigint;        
 set @do_report = (        
  select count(*)        
  from KS2        
   inner join SpecFill on SFId=KSSpecFillId        
   inner join SpecVer on SVId=SFSpecVer        
  where SVSpec=@spec        
 );        
        
 if @do_report=0         
  begin        
           
   select        
    sf.SFID [-6],SFEId [-5],EName [-4], nvw.bfnum [-3], nvw.smrnum [-2],sf.SFSupplyPID [-1],sf.[Чьи материалы] [0],SFNo+'.'+SFNo2 [1],SFName [2],SFMark [3],SFUnit [4],SFEQty [5]
	, null [6], null [7], null [8], null [9], null [10], m.M15Num [11], m.M15Name [12], m.M15Date [13], m.M15Qty [14], m.M15Price [15], m.M15Qty * m.M15Price [16]
   from SpecVer sv      
   inner join vwSpecFill sf on sv.SVId= sf.SVId    
   left join vw_tmpBudgSMRNums nvw on nvw.SFId = sf.sfid  
   inner join SpecFillExec on sf.SFId=SFEFill        
   inner join Executor on SFEExec=EId   
   left join M15 m on m.FillId = sf.SFId or m.PID = sf.SFSupplyPID
   where SVSpec=@spec        
   order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end        
        
  end;        
 else        
  begin        
        
        
  select SFEId,SFId,header,d_ks.KSSum         
  into #datatbl        
  from SpecVer        
   inner join (        
    select max(svid) sv_spec_ver_max, SVSpec        
    from SpecVer        
    where SVSpec=@spec        
    group by SVSpec        
   ) max_ver on sv_spec_ver_max=SVId --находим последнюю версию спеки        
   inner join SpecFill on SFSpecVer = sv_spec_ver_max --получаем наполнение спеки последней версии        
   left join (        
    select        
     SFEId,SFEFill        
    from        
     Done        
     inner join SpecFillExec on SFEId=DSpecExecFill        
    group by SFEId,SFEFill        
   )d_done on SFId=SFEFill --получаем выполненные объемы по филкам по месяцам        
   left join (        
   select dd.KSSpecFillId, concat('KC2 №',dd.KSNum,' на сумму ',d.KSTotal) as header,dd.KSSum         
   from(        
    select         
     sum(KSSum) sumks, KSNum,KSTotal        
    from KS2        
    group by KSNum,KSTotal)d        
    left join (select KSSpecFillId, KSNum, KSSum from KS2        
     )dd on dd.KSNum = d.KSNum        
    )d_ks on d_ks.KSSpecFillId = SFId        
        
  DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),        
    @PivotColumnNames AS NVARCHAR(MAX),        
    @PivotSelectColumnNames AS NVARCHAR(MAX),        
    @PivotTotalColumnNames AS NVARCHAR(MAX),        
    @PivotTotalColumnNamesPlus AS NVARCHAR(MAX)        
  --Get distinct values of the PIVOT Column        
  SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')        
  + QUOTENAME(header)        
  FROM (SELECT DISTINCT header FROM #datatbl) AS mnth; --получаем месяца через запятую        
  --select @PivotColumnNames        
        
  --Get distinct values of the PIVOT Column with isnull        
  SELECT @PivotSelectColumnNames         
   = ISNULL(@PivotSelectColumnNames + ',','')        
   + 'ISNULL(' + QUOTENAME(mnth) + ', 0) AS '        
   + QUOTENAME(mnth)        
  FROM (SELECT top 100 percent mnth FROM (select distinct header mnth from #datatbl)q order by mnth) AS mnths;        
        
  drop table #datatbl;        
  --Prepare the PIVOT query using the dynamic        
  SET @DynamicPivotQuery =        
  N'           
     select SFEId,SFId,header,d_ks.KSSum         
  into #datatbl        
  from SpecVer         
   inner join SpecFill sf on SFSpecVer = svid --получаем наполнение спеки последней версии     
   left join SpecFillExec sfe on sfe.sfefill = sf.sfid       
   left join (        
   select dd.KSSpecFillExec, concat(''KC2 №'',dd.KSNum,'' на сумму '',d.KSTotal) as header,dd.KSSum         
   from(        
   select         
     KSNum,KSTotal        
    from KS2        
    group by KSNum,KSTotal)d        
    left join (select KSSpecFillExec, KSNum, KSSum from KS2        
     )dd on dd.KSNum = d.KSNum        
    )d_ks on d_ks.KSSpecFillExec = sfe.SFEId     
 where svspec = '+CAST(@spec as varchar(max))+'    
         
  SELECT SFEId, ' + @PivotSelectColumnNames + '        
  into #totals        
  FROM #datatbl        
  PIVOT(        
   SUM(KSSum)        
   FOR header IN (' + @PivotColumnNames + ')        
  ) AS PVTTable;        
        
     
        
  select        
   sf.SFID [-6],sfe.SFEId [-5],EName [-4], nvw.bfnum [-3], nvw.smrnum [-2],sf.SFSupplyPID [-1],sf.[Чьи материалы] [0], SFNo+''.''+SFNo2 [1],SFName [2],SFMark [3],SFUnit [4],SFEQty [5], sumks [6], null [7], null [8], null [9], DSumQty [10],
   m.M15Num [11], m.M15Name [12], m.M15Date [13], m.M15Qty [14], m.M15Price [15], m.M15Qty * m.M15Price [16],
     '+@PivotSelectColumnNames+'        
   from SpecVer sv      
   inner join vwSpecFill sf on sv.SVId=sf.svid  
   left join vw_tmpBudgSMRNums nvw on nvw.SFId = sf.sfid  
   left join SpecFillExec sfe on sf.SFId=sfe.SFEFill        
   left join Executor on SFEExec=EId        
   left join (select DSpecExecFill, sum(DQty) DSumQty from Done group by DSpecExecFill)d on DSpecExecFill = SFEId        
   left join #totals on #totals.SFEId=sfe.sfeid       
   left join (select KSSpecFillExec, sum(KSSum) sumks from KS2 group by KSSpecFillExec)dd on KSSpecFillExec = sfe.SFEid    
   left join M15 m on m.FillId = sf.SFId or m.PID = sf.SFSupplyPID
  where SVSpec='+CAST(@spec as varchar(max))+'        
  order by case IsNumeric(SFNo) when 1 then Replicate(''0'', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate(''0'', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end        
        
  drop table #totals;        
  drop table #datatbl;'        
        
  exec sp_executesql @DynamicPivotQuery;        
  end;        
  end;