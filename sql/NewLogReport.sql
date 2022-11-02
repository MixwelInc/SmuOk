USE [SmuOk]
GO

/****** Object:  View [dbo].[vwLogNewReport]    Script Date: 02.11.2022 23:14:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwLogNewReport]
AS
SELECT 
ROW_NUMBER() over (order by vwe.UFIO) [№ п/п]
, vwe.UFIO [ФИО]
, isnull(q_pto.PTOAdd_cnt,0) + isnull(q2.SupplyOrderFillAdd_cnt,0) + isnull(q3.SupplyOrderFillUpd_cnt,0) + isnull(q.BolFillAdd_cnt,0) 
+ isnull(q1.BolFillUpd_cnt,0) + isnull(q4.InvCfmFillAdd_cnt,0) + isnull(q5.InvCfmFillUpd_cnt,0) + isnull(q6.InvDocFillAdd_cnt,0)
+ isnull(q7.InvDocFillUpd_cnt,0) as [Всего строк]
, q_pto.PTOAdd_cnt [добавлено1]
, q2.SupplyOrderFillAdd_cnt [добавлено2]
, q3.SupplyOrderFillUpd_cnt [обновлено2]
, q.BolFillAdd_cnt [добавлено3]
, q1.BolFillUpd_cnt [обновлено3]
, q4.InvCfmFillAdd_cnt [добавлено4]
, q5.InvCfmFillUpd_cnt [обновлено4]
, q6.InvDocFillAdd_cnt [добавлено5]
, q7.InvDocFillUpd_cnt [обновлено5]
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
  where 
  q_pto.PTOAdd_cnt is not null 
  or q.BolFillAdd_cnt is not null or q1.BolFillUpd_cnt is not null 
  or q2.SupplyOrderFillAdd_cnt  is not null or q3.SupplyOrderFillUpd_cnt  is not null
  or q4.InvCfmFillAdd_cnt  is not null or q5.InvCfmFillUpd_cnt is not null
  or q6.InvDocFillAdd_cnt is not null or q7.InvDocFillUpd_cnt is not null

GO


