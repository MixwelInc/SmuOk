alter procedure [dbo].[uspReport_SpecFillBudgetHistory_v3.0]      
  (@spec bigint)      
 as      
begin      
    
 select SFHSpecFill sf, max(SFHSpecVer) SFHSpecVer_max, cast ('' as nvarchar(max)) SpecName, cast ('' as nvarchar(max)) [No], cast ('' as nvarchar(max)) [No2]      
 into #t1      
 from SpecFillHistory inner join SpecVer on SFHSpecVer=SVId      
 where SVSpec = @spec     
 group by SFHSpecFill;  --получили филки из истории и их максимальные измы по переданной спеке (залили в #t1)    
      
 update #t1 set SpecName=SVName      
 from #t1 inner join SpecVer on SFHSpecVer_max=SVId  --проапдейтили #t1, залили туда имя шифра    
      
    
 select SFHSpecFill /*SpecFill*/[ID записи],SFHId /*ActiveHistoryId*/[ID истории],SpecName [Шифр проекта], SFSubcode [Шифр по спецификации], e.ename [Исполнитель],     
 sf.SFType [Вид по спецификации], SFHNo /*[No]*/[№ п/п], SFHNo2 /*No2*/[№ п/п 2], SFHName /*[Name]*/[Наименование и техническая характеристика], SFMark [Тип, марка, обозначение документа], SFUnit [Ед. изм]  
 into #t2      
 from SpecFillHistory inner join #t1 on SFHSpecFill=sf and SFHSpecVer=SFHSpecVer_max --в #t2 занесли нужные поля по последней     
 left join SpecFill sf on sf.SFId = sf
 left join BudgetFill bf on bf.ICId = sf.SFId
 left join SpecFillExec sfe on sfefill = sf.SFId  
 left join Executor e on e.EId = sfe.sfeexec  

   
    
  alter table #t2 add [PID] Varchar(MAX);    
  alter table #t2 add [Чьи материалы] Varchar(MAX);    
  alter table #t2 add [ВОР накопительный] decimal(18,4);    
  alter table #t2 add [Средняя цена по счету] decimal(18,2); 
  alter table #t2 add [Кол-во счетов] bigint;
  alter table #t2 add [ID сметы] bigint;    
  alter table #t2 add [Номер сметы] Varchar(MAX);  
  alter table #t2 add [ID позиции сметы] bigint;
  alter table #t2 add [Вид по смете] Varchar(MAX);
  alter table #t2 add [№ по смете] Varchar(MAX);    
  alter table #t2 add [№ по СМР] Varchar(MAX);    
  alter table #t2 add [Шифр расценки и коды ресурсов] Varchar(MAX);    
  alter table #t2 add [Наименование по смете] Varchar(MAX);    
  alter table #t2 add [Ед. изм. по смете] Varchar(MAX);    
  alter table #t2 add [К перевода] decimal(18,4);    
  alter table #t2 add [Кол-во по смете] decimal(18,4);
  alter table #t2 add [Сумма по смете] decimal(18,2);
  alter table #t2 add [Цена сметная за ед. без НДС] Varchar(MAX); 
  
 declare @q nvarchar(max)      
 declare @rez_col_names nvarchar(max)      
 declare @ver_id bigint, @ver_no decimal(18,1), @ver_qty decimal(18,4)      
      
 declare ver_cursor cursor for     --создали курсор    
 select distinct SVId, SVNo    --заносим в него уникальные пары (айди версии,номер версии)     
 from SpecFillHistory inner join SpecVer on SFHSpecVer=SVId      
 where SVSpec = @spec  --по нужной спеке    
 order by SVNo;  --сортируем по номеру версии    
      
  open ver_cursor        
   fetch next from ver_cursor into @ver_id, @ver_no        
   while @@fetch_status = 0    --открыли курсор и тащим пока не закончится    
   begin      
    select SFHId,SFHSpecFill,SFHQty into #t3 from SpecFillHistory where SFHSpecVer=@ver_id      
    set @q = ' alter table #t2 add [К-во\n(вер. '+cast(@ver_no as nvarchar(max))+')] decimal(18,4) not null default(0.0);'       
    exec(@q);      
    set @q = ' update #t2 set [К-во\n(вер. '+cast(@ver_no as nvarchar(max))+')]=SFHQty from #t2 inner join #t3 on SFHSpecFill=/*SpecFill*/[ID записи]'      
    exec(@q);      
    drop table #t3;      
    set @rez_col_names = @rez_col_names + ', [К-во\n(вер. '+cast(@ver_no as nvarchar(max))+')]'      
    fetch next from ver_cursor into @ver_id, @ver_no;      
   end      
  close ver_cursor;      
 deallocate ver_cursor;      
    
    
  update #t2    
  set [Вид по смете] = BFType, 
  [ID сметы] = bf.BudgId,
  [ID позиции сметы] = BFId,
  [Номер сметы] = b.BNumber,    
  [№ по смете] = BFNum,    
  [№ по СМР] = BFSMRNum,    
  [Шифр расценки и коды ресурсов] = BFCode,  
  [Наименование по смете] = BFName,    
  [Ед. изм. по смете] = BFUnit,    
  [К перевода] = cast(BFKoeff as decimal(18,4)),    
  [Кол-во по смете] = cast(BFQty as decimal(18,4)),    
  [Цена сметная за ед. без НДС] = '=RC[-1]/RC[-2]',    
  [ВОР накопительный] = cast(d.DSumQty as decimal(18,4)),    
  [Средняя цена по счету] = cast(q.res as decimal(18,2)),
  [Кол-во счетов] = q.cnt,
  [PID] = sf.SFSupplyPID,    
  [Чьи материалы] = CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' end,
  [Сумма по смете] = bf.BFSum
 from #t2     
 left join SpecFill sf on sf.SFId = [ID записи]    
 left join SpecFillExec sfe on sfefill = sf.sfid
 left join (select DSpecExecFill, sum(DQty) DSumQty from Done group by DSpecExecFill)d on DSpecExecFill = SFEId
 left join Executor e on e.EId = sfe.sfeexec    
 --left join (select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on SFBFill = sf.SFId
 outer apply (select sum(ICPrc)/COUNT(*) as res, count(*) as cnt from InvCfm where ICFill = sf.SFId)q
 left join BudgetFill bf on bf.SpecFillId = sf.SFId
 left join Budget b on b.BId = bf.BudgId
    
 select *    
 from #t2   --все резы берем из #t2    
 order by      
  case IsNumeric([№ п/п]) when 1 then Replicate('0', 10 - Len([№ п/п])) + [№ п/п] else [№ п/п] end,      
  case IsNumeric([№ п/п 2]) when 1 then Replicate('0', 10 - Len([№ п/п 2])) + [№ п/п 2] else [№ п/п 2] end,      
  [ID истории]      
      
 drop table #t1      
 drop table #t2      
 end