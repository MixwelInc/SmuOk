alter PROCEDURE [dbo].[uspReport_NZP] 
	@spec nvarchar(max)

as 
begin
	set nocount on;
	declare @y nvarchar(max);
	declare @m nvarchar(max);
	set @y = year(getdate());
	set @m = month(getdate()) - 1;
	declare @do_report bigint;
	set @do_report = (
		select count(*)
		from Done
			inner join SpecFillExec on DSpecExecFill=SFEId
			inner join SpecFill on SFId=SFEFill
			inner join SpecVer on SVId=SFSpecVer
		where SVSpec=@spec
	);

	if @do_report=0 
		begin
			
			select
				sf.SFId [-6], SFEId [-5],EName [-4], tmp.bfnum [-3], tmp.smrnum [-2],sf.SFSupplyPID [-1],vwsf.[Чьи материалы] [0]
				,sf.SFNo+'.'+sf.SFNo2 [1],sf.SFName [2],sf.SFMark [3],sf.SFUnit [4],SFEQty [5], null [6], null [7], null [8], null [9]
			from SpecVer
			inner join SpecFill sf on SVId=SFSpecVer
			left join vwSpecFill vwsf on vwsf.SFId = sf.SFId
			inner join SpecFillExec on sf.SFId=SFEFill
			inner join Executor on SFEExec=EId
			left join vw_tmpBudgSMRNums tmp on tmp.SFId = sf.SFId
			where SVSpec=@spec
			order by case IsNumeric(sf.SFNo) when 1 then Replicate('0', 10 - Len(sf.SFNo)) + sf.SFNo else sf.SFNo end, 
			case IsNumeric(sf.SFNo2) when 1 then Replicate('0', 10 - Len(sf.SFNo2)) + sf.SFNo2 else sf.SFNo2 end

		end;
	else
		begin
		drop table if exists #datatbl

		select SFEId,mnth,m,s
		into #datatbl
		from SpecVer
			inner join (
				select max(svid) sv_spec_ver_max, SVSpec
				from SpecVer
				where SVSpec=@spec
				group by SVSpec
			) max_ver on sv_spec_ver_max=SVId
			inner join SpecFill on SFSpecVer = sv_spec_ver_max
			inner join (
				select
					SFEId,SFEFill ,format([DDate],'yyyy, MMMM') mnth,format([DDate],'yyyy-MM') m,Sum([DQty]) s
				from
					Done
					inner join SpecFillExec on SFEId=DSpecExecFill
				group by SFEId,SFEFill,format([DDate],'yyyy, MMMM'),format([DDate],'yyyy-MM')
			)d_done on SFId=SFEFill;

		DECLARE @DynamicPivotQuery AS NVARCHAR(MAX),
				@PivotColumnNames AS NVARCHAR(MAX),
				@PivotSelectColumnNames AS NVARCHAR(MAX),
				@PivotTotalColumnNames AS NVARCHAR(MAX),
				@PivotTotalColumnNamesPlus AS NVARCHAR(MAX)
		--Get distinct values of the PIVOT Column
		SELECT @PivotColumnNames= ISNULL(@PivotColumnNames + ',','')
		+ QUOTENAME(mnth)
		FROM (SELECT DISTINCT mnth FROM #datatbl) AS mnth;

		--Get distinct values of the PIVOT Column with isnull
		SELECT @PivotSelectColumnNames 
			= ISNULL(@PivotSelectColumnNames + ',','')
			+ 'ISNULL(' + QUOTENAME(mnth) + ', 0) AS '
			+ QUOTENAME(mnth)
		FROM (SELECT top 100 percent substring(mnth,8,100)mnth FROM (select distinct m+mnth mnth from #datatbl)q order by mnth) AS mnths;

		SELECT @PivotTotalColumnNames 
			= ISNULL(@PivotTotalColumnNames + ',','')
			+ QUOTENAME(mnth)
		FROM (SELECT top 100 percent substring(mnth,8,100)mnth FROM (select distinct m+mnth mnth from #datatbl)q order by mnth) AS mnths;

		SELECT @PivotTotalColumnNamesPlus
			= ISNULL(@PivotTotalColumnNamesPlus + '+','')
			+ QUOTENAME(mnth)
		FROM (SELECT top 100 percent substring(mnth,8,100)mnth FROM (select distinct m+mnth mnth from #datatbl)q order by mnth) AS mnths;

		--select * from #datatbl

		drop table #datatbl;

		--Prepare the PIVOT query using the dynamic
		SET @DynamicPivotQuery =
		N'
		select SFEId,mnth,s
		into #datatbl
		from SpecVer
			inner join (
				select max(svid) sv_spec_ver_max, SVSpec
				from SpecVer
				where SVSpec='+CAST(@spec as varchar(max))+'
				group by SVSpec
			) max_ver on sv_spec_ver_max=SVId
			inner join SpecFill on SFSpecVer = sv_spec_ver_max
			inner join (
				select
					SFEId,SFEFill ,format([DDate],''yyyy, MMMM'') mnth,format([DDate],''yyyy-MM'') m,Sum([DQty]) s
				from
					Done
					inner join SpecFillExec on SFEId=DSpecExecFill
				group by SFEId,SFEFill,format([DDate],''yyyy, MMMM''),format([DDate],''yyyy-MM'')
			)d_done on SFId=SFEFill
		;
	
		SELECT SFEId, ' + @PivotSelectColumnNames + '
		into #totals
		FROM #datatbl
		PIVOT(
			SUM(s)
			FOR mnth IN (' + @PivotColumnNames + ')
		) AS PVTTable;


		select
			sf.SFId [-6], SFEId [-5],EName [-4], tmp.bfnum [-3], tmp.smrnum [-2],sf.SFSupplyPID [-1],vwsf.[Чьи материалы] [0] ,
			sf.SFNo+''.''+sf.SFNo2 [1],sf.SFName [2],sf.SFMark [3],sf.SFUnit [4],SFEQty [5], DSumQty [6], DSumNow [7], null [8], null [9],
					'+@PivotSelectColumnNames+'
			from SpecVer
			inner join SpecFill sf on SVId=SFSpecVer
			left join vwSpecFill vwsf on vwsf.SFId = sf.SFId
			left join SpecFillExec sfe on sf.SFId=SFEFill
			left join Executor on SFEExec=EId
			left join (select DSpecExecFill, sum(DQty) DSumQty from (select * from Done where ((year(DDate) = ' + @y + '  and month(DDate) < ' + @m + ' ) or (year(DDate) <  ' + @y + ' )))w group by DSpecExecFill)d on DSpecExecFill = sfe.SFEId
			outer apply (select DSpecExecFill, sum(DQty) DSumNow from Done dd where dd.DSpecExecFill = sfe.SFEId and year(DDate) = ' + @y + '  and month(DDate) = ' + @m + ' group by dd.DSpecExecFill)q
			left join #totals on #totals.SFEId=Sfe.SFEId 
			left join vw_tmpBudgSMRNums tmp on tmp.SFId = sf.SFId
		where SVSpec= '+CAST(@spec as varchar(max))+'
		order by case IsNumeric(sf.SFNo) when 1 then Replicate(''0'', 10 - Len(sf.SFNo)) + sf.SFNo else sf.SFNo end, 
		case IsNumeric(sf.SFNo2) when 1 then Replicate(''0'', 10 - Len(sf.SFNo2)) + sf.SFNo2 else sf.SFNo2 end

		drop table #totals;
		drop table #datatbl;'

		exec sp_executesql @DynamicPivotQuery;
	end;
end