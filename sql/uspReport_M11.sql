alter PROCEDURE [dbo].[uspReport_M11]       
 @spec nvarchar(max)    
      
AS      
BEGIN      
drop table if exists #datatbl;  
drop table if exists #datatbl2;
declare @do_report bigint;      
 set @do_report = (      
  select count(*)      
  from SpecFill sf      
   inner join SpecVer on SVId=SFSpecVer
   left join SpecFillBol sfb on sf.SFId = sfb.SFBFill
   left join M15 mm on mm.FillId = sf.SFId  or mm.PID = sf.SFSupplyPID 
   left join M11 m on sf.SFId = m.FillId 
  where SVSpec=@spec and (sfb.SFBId is not null or mm.M15Id is not null)
 );      
      
 if @do_report=0       
  begin      
         
   return null     
      
  end;      
 else      
  begin      
      
  select SFId,header      
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
   select FillId, concat('M11 №',Num) as header      
   from M11
    )d_ks on d_ks.FillId = SFId      
      
  DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),      
    @PivotColumnNames AS NVARCHAR(MAX),      
    @PivotSelectColumnNames AS NVARCHAR(MAX),      
    @PivotTotalColumnNames AS NVARCHAR(MAX),      
    @PivotTotalColumnNamesPlus AS NVARCHAR(MAX)      
  --Get distinct values of the PIVOT Column      
  SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')      
  + QUOTENAME(header)      
  FROM (SELECT DISTINCT header FROM #datatbl) AS mnth; --получаем заголовки     
  --select @PivotColumnNames      
      
  --Get distinct values of the PIVOT Column with isnull      
  SELECT @PivotSelectColumnNames       
   = ISNULL(@PivotSelectColumnNames + ',','')      
   + 'ISNULL(' + QUOTENAME(mnth) + ', 0) AS '      
   + QUOTENAME(mnth)      
  FROM (SELECT top 100 percent mnth FROM (select distinct header mnth from #datatbl)q order by mnth) AS mnths;      
      
  drop table #datatbl;      
  --Prepare the PIVOT query using the dynamic   ТУТ ОСТАНОВИЛСЯ   
  if @PivotColumnNames is not null
  begin
  SET @DynamicPivotQuery =      
  N'         
     select SFId,header,Released     
  into #datatbl2      
  from SpecVer       
   inner join SpecFill sf on SFSpecVer = svid --получаем наполнение спеки последней версии  
   left join (      
   select FillId, concat(''M11 №'',Num) as header, Released     
   from M11
    )d_ks on d_ks.FillId = SFId        
 where svspec = '+CAST(@spec as varchar(max))+'  
       
  SELECT SFId, ' + @PivotSelectColumnNames + '      
  into #totals      
  FROM #datatbl2      
  PIVOT(      
   SUM(Released)      
   FOR header IN (' + @PivotColumnNames + ')      
  ) AS PVTTable;      
      
   if @
      
 select  sf.SFID [-1], SFNo+''.''+SFNo2 [0],NULL [1],NULL [2],
		case when ic.ICId is not null then ic.ICName 
			 else sf.SFName end [3],
		NULL [4], NULL [5], sf.SFUnit [6], coalesce(sfb.SFBQtyForTSK,M15Qty) - ISNULL(m.Released, 0) [7]
		, coalesce(sfb.ssum,M15Qty) - ISNULL(m.Released, 0) [8], coalesce(ic.ICPrc,mm.M15Price) [9], ''=RC[-1]*RC[-2]'' [10]
		, NULL [11], '+@PivotSelectColumnNames+'
   from SpecVer sv    
   inner join SpecFill sf on sv.SVId=sf.SFSpecVer
   left join M11 m on m.FillId = sf.SFId
   outer apply(select top(1) SFBId ,sum(SFBQtyForTSK)ssum from SpecFillBol where SFBFill = sf.SFId group by SFBId)sfb
   left join M15 mm on mm.FillId = sf.SFId  or mm.PID = sf.SFSupplyPID 
   left join InvCfm ic on ic.ICFill = sf.SFId
   left join #totals on #totals.SFId=sf.sfid        
  where SVSpec='+CAST(@spec as varchar(max))+' and (sfb.SFBId is not null or mm.M15Id is not null)     
  order by case IsNumeric(SFNo) when 1 then Replicate(''0'', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate(''0'', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end      
      
	 
  drop table #totals;      
  drop table #datatbl;'      
      
  exec sp_executesql @DynamicPivotQuery;      
  end;  
  else
  begin

  select  sf.SFID [-1], SFNo+'.'+SFNo2 [0],NULL [1],NULL [2],
		case when ic.ICId is not null then ic.ICName 
			 else sf.SFName end [3],
		NULL [4], NULL [5], sf.SFUnit [6], coalesce(sfb.ssum,M15Qty) - ISNULL(m.Released, 0) [7]
		, coalesce(sfb.ssum,M15Qty) - ISNULL(m.Released, 0) [8], coalesce(ic.ICPrc,mm.M15Price) [9], '=RC[-1]*RC[-2]' [10]
		, NULL [11]
   from SpecVer sv    
   inner join SpecFill sf on sv.SVId=sf.SFSpecVer
   left join M11 m on m.FillId = sf.SFId
   outer apply(select top(1) SFBId ,sum(SFBQtyForTSK)ssum from SpecFillBol where SFBFill = sf.SFId group by SFBId)sfb
   left join M15 mm on mm.FillId = sf.SFId  or mm.PID = sf.SFSupplyPID 
   --left join "Order1S 2022-03-11" as s1 on s1.O1S34 = sfb.SFBBoLNoForTSK
   left join InvCfm ic on ic.ICFill = sf.SFId
   where (sfb.SFBId is not null or mm.M15Id is not null)
  order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) 
			when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end
  end;
  end;
  end;