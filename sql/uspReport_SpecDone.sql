
CREATE PROCEDURE [dbo].[uspReport_SpecDone] 
	@spec nvarchar(max)

AS
BEGIN
	set nocount on;
	-- ВНИМАНИЕ! --
	--		Здесь выводим исполнение по наполнению актуальной версии.
	--		Если сделали что-то по устаревшим версиям, нужен отдельный отчет

	-- временная таблица -- для набора месяцев, по которым будем рисовать pivot
	-- она же ниже дорисовывается в генератор запроса -- в него перебором рожаем список столбцов для секции in

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
				SFEId [-3],EName [-2],vwsf.SFSupplyPID [-1], [Чьи материалы] [0],SFNo+'.'+SFNo2 [1],SFName [2],SFMark [3],SFUnit [4],SFEQty [5], null [6], null [7], null [8], null [9]
			from SpecVer sv
			inner join vwSpecFill vwsf on sv.SVId=vwsf.SVId
			inner join SpecFillExec on SFId=SFEFill
			inner join Executor on SFEExec=EId
			where SVSpec=@spec
			order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end

		end;
	else
		begin


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
			sfe.SFEId [-3],EName [-2],vwsf.SFSupplyPID [-1], [Чьи материалы] [0],SFNo+''.''+SFNo2 [1],SFName [2],SFMark [3],SFUnit [4],SFEQty [5], DSumQty [6], null [7], null [8], null [9],
					'+@PivotSelectColumnNames+'
			from SpecVer sv
			inner join vwSpecFill vwsf on sv.SVId=vwsf.SVId
			left join SpecFillExec sfe on SFId=SFEFill
			left join Executor on SFEExec=EId
			left join (select DSpecExecFill, sum(DQty) DSumQty from Done group by DSpecExecFill)d on DSpecExecFill = SFEId
			left join #totals on #totals.SFEId=sfe.SFEId
		where SVSpec='+CAST(@spec as varchar(max))+'
		order by case IsNumeric(SFNo) when 1 then Replicate(''0'', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate(''0'', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end

		drop table #totals;
		drop table #datatbl;'
		exec sp_executesql @DynamicPivotQuery;
	end
END

