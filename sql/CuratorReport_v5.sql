alter procedure [dbo].[CuratorReport_v5]  
@spec nvarchar(max)  
as  
begin  
  drop table if exists #datatbl
  declare @do_report bigint;        
 set @do_report = (        
  select count(*)        
  from  
 SpecFill   
 inner join SpecVer on SFSpecVer=SVId   
 left join SpecFillExec on SFId = SFEFill   
 left join Executor on SFEExec=EId  
 where SVSpec = @spec       
 );    
  
 if @do_report=0  
 begin  
  select SFNo [1], SFSupplyPID [2], SFName [3], SFMark [4], SFUnit [5], SFQty [6], null [7]  
  from SpecFill   
  inner join SpecVer on SFSpecVer=SVId   
  left join SpecFillExec on SFId = SFEFill   
  left join Executor on SFEExec=EId  
  where SVSpec = @spec  
 end;  
  else  
 begin 
  select SFNo [1], SFSupplyPID [2], SFName [3], SFMark [4], SFUnit [5], SFQty [6], EName as header  
  into #datatbl  
  from  
  SpecFill   
  inner join (select max(svid) sv_spec_ver_max, SVSpec from SpecVer where SVSpec = @spec group by SVSpec)max_ver on SFSpecVer=sv_spec_ver_max   
  left join SpecFillExec on SFId = SFEFill   
  left join Executor on SFEExec=EId  
  where SVSpec = @spec  
  order by   
   case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end,   
   case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end  
  
  DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),        
  @PivotColumnNames AS NVARCHAR(MAX),        
  @PivotSelectColumnNames AS NVARCHAR(MAX),        
  @PivotTotalColumnNames AS NVARCHAR(MAX),        
  @PivotTotalColumnNamesPlus AS NVARCHAR(MAX)  
  
  SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')        
  + QUOTENAME(header)        
  FROM (SELECT DISTINCT header FROM #datatbl) AS header;  
  
  SELECT @PivotSelectColumnNames         
  = ISNULL(@PivotSelectColumnNames + ',','')        
  + 'ISNULL(' + QUOTENAME(header) + ', 0) AS '        
  + QUOTENAME(header)        
  FROM (SELECT top 100 percent header FROM (select distinct header from #datatbl)q order by header) AS headers;  
  
  drop table #datatbl;  
  
  set @DynamicPivotQuery =   
  N'  
  select SFNo, SFSupplyPID, SFName, SFMark, SFUnit, SFQty, SFEQty, EName as header  
  into #datatbl  
  from  
  SpecFill   
  inner join (select max(svid) sv_spec_ver_max, SVSpec from SpecVer where SVSpec = '+CAST(@spec as varchar(max))+' group by SVSpec)max_ver on SFSpecVer=sv_spec_ver_max   
  left join SpecFillExec on SFId = SFEFill   
  left join Executor on SFEExec=EId  
  where SVSpec = '+CAST(@spec as varchar(max))+'  
  order by   
   case IsNumeric(SFNo) when 1 then Replicate(''0'', 10 - Len(SFNo)) + SFNo else SFNo end,   
   case IsNumeric(SFNo2) when 1 then Replicate(''0'', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end;  
     
  SELECT SFNo [1], SFSupplyPID [2], SFName [3], SFMark [4], SFUnit [5], SFQty [6],  
  '+@PivotSelectColumnNames+'
  into #supertotals
  FROM #datatbl        
  PIVOT(        
  sum(SFEQty)  
  FOR header IN (' + @PivotColumnNames + ')        
  ) AS PVTTable

  select *  
  from #supertotals  
  order by case IsNumeric([1]) when 1 then Replicate(''0'', 10 - Len([1])) + [1] else [1] end
      
   drop table #datatbl; 
   drop table #supertotals; 
  '  
  exec sp_executesql @DynamicPivotQuery;  
  end;
  end;