USE [SmuOk]
GO

/****** Object:  View [dbo].[vwLogNewReport]    Script Date: 14.12.2022 20:21:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[vwLogNewReport]
AS
SELECT 
ROW_NUMBER() over (order by vwe.UFIO) [№ п/п]
, vwe.UFIO [ФИО]
, isnull(q_pto.PTOAdd_cnt,0) + isnull(q2.SupplyOrderFillAdd_cnt,0) + isnull(q3.SupplyOrderFillUpd_cnt,0) + isnull(q.BolFillAdd_cnt,0) 
+ isnull(q1.BolFillUpd_cnt,0) + isnull(q4.InvCfmFillAdd_cnt,0) + isnull(q5.InvCfmFillUpd_cnt,0) + isnull(q6.InvDocFillAdd_cnt,0)
+ isnull(q7.InvDocFillUpd_cnt,0) + isnull(q8.OrderDocAdd_cnt,0) + isnull(q9.OrderDocUpd_cnt,0) + isnull(q10.DoneAdd_cnt,0) + isnull(q11.BudgAdd_cnt,0)
+ isnull(q12.BudgUpd_cnt,0) + isnull(q13.BudgetAdd_cnt,0) + isnull(q14.BudgetUpd_cnt,0) + isnull(q15.KS2DocAdd_cnt,0) + isnull(q16.KS2Add_cnt,0)
+ isnull(q17.M15Add_cnt,0) + isnull(q18.M15Upd_cnt,0) as [всего]
, isnull(q_pto.PTOAdd_cnt,0) [добавлено1]
, isnull(q2.SupplyOrderFillAdd_cnt,0) [добавлено2]
, isnull(q3.SupplyOrderFillUpd_cnt,0) [обновлено2]
, isnull(q.BolFillAdd_cnt,0) [добавлено3]
, isnull(q1.BolFillUpd_cnt,0) [обновлено3]
, isnull(q4.InvCfmFillAdd_cnt,0) [добавлено4]
, isnull(q5.InvCfmFillUpd_cnt,0) [обновлено4]
, isnull(q6.InvDocFillAdd_cnt,0) [добавлено5]
, isnull(q7.InvDocFillUpd_cnt,0) [обновлено5]
, isnull(q8.OrderDocAdd_cnt,0) [1]
, isnull(q9.OrderDocUpd_cnt,0) [2]
, isnull(q10.DoneAdd_cnt,0) [3]
, isnull(q11.BudgAdd_cnt,0) [4]
, isnull(q12.BudgUpd_cnt,0) [5]
, isnull(q13.BudgetAdd_cnt,0) [6]
, isnull(q14.BudgetUpd_cnt,0) [7]
, isnull(q15.KS2DocAdd_cnt,0) [8]
, isnull(q16.KS2Add_cnt,0) [9]
, isnull(q17.M15Add_cnt,0) [10]
, isnull(q18.M15Upd_cnt,0) [11]

  FROM _engUser e
  left join vwUser vwe on vwe.UId = e.EUId
  outer apply(
		select vwu2.UId, sum(vwSpec.NewestFillingCount) as PTOAdd_cnt 
		from vwUser vwu2
		inner join _engLog on ELDBUser = vwu2.UId
		inner join (select max(ELId) max_elid,ELSpec max_elspec 
					from _engLog 
					where ELEvent=13 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() 
					group by ELSpec) max_el on ELId=max_elid
		inner join vwSpec on ELSpec = SId 
		where vwu2.UId = vwe.UId and SPTODone IS NOT NULL and SPTODone between DATEADD(day,-6,getdate()) and GETDATE() 
		group by vwu2.UId 
		)q_pto
  outer apply(select ELDBUser, count(*) as BolFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2000 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q 
  outer apply(select ELDBUser, count(*) as BolFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2001 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q1
  outer apply(select ELDBUser, count(*) as SupplyOrderFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2002 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q2
  outer apply(select ELDBUser, count(*) as SupplyOrderFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2003 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q3 
  outer apply(select ELDBUser, count(*) as InvCfmFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2004 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q4 
  outer apply(select ELDBUser, count(*) as InvCfmFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2005 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q5 
  outer apply(select ELDBUser, count(*) as InvDocFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2006 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q6 
  outer apply(select ELDBUser, count(*) as InvDocFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2007 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q7 
  outer apply(select ELDBUser, count(*) as OrderDocAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2014 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q8 
  outer apply(select ELDBUser, count(*) as OrderDocUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2015 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q9 
  outer apply(select ELDBUser, count(*) as DoneAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2016 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q10 
  outer apply(select ELDBUser, count(*) as BudgAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2017 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q11
  outer apply(select ELDBUser, count(*) as BudgUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2018 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q12 
  outer apply(select ELDBUser, count(*) as BudgetAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2019 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q13 
  outer apply(select ELDBUser, count(*) as BudgetUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2020 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q14 
  outer apply(select ELDBUser, count(*) as KS2DocAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2021 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q15 
  outer apply(select ELDBUser, count(*) as KS2Add_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2022 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q16 
  outer apply(select ELDBUser, count(*) as M15Add_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2009 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q17
  outer apply(select ELDBUser, count(*) as M15Upd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2010 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q18
  where 
  q_pto.PTOAdd_cnt is not null 
  or q.BolFillAdd_cnt is not null or q1.BolFillUpd_cnt is not null 
  or q2.SupplyOrderFillAdd_cnt  is not null or q3.SupplyOrderFillUpd_cnt  is not null
  or q4.InvCfmFillAdd_cnt  is not null or q5.InvCfmFillUpd_cnt is not null
  or q6.InvDocFillAdd_cnt is not null or q7.InvDocFillUpd_cnt is not null
  or q8.OrderDocAdd_cnt is not null or q.BolFillAdd_cnt is not null
  or q10.DoneAdd_cnt is not null or q11.BudgAdd_cnt is not null
  or q12.BudgUpd_cnt is not null or q13.BudgetAdd_cnt is not null
  or q14.BudgetUpd_cnt is not null or q15.KS2DocAdd_cnt is not null
  or q16.KS2Add_cnt is not null or q17.M15Add_cnt is not null
  or q18.M15Upd_cnt is not null

GO


