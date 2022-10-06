USE [SmuOk]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLogNewReport]
AS
SELECT e.EUF [Фамилия], q.BolFillAdd_cnt [Кол-во записей УПД добавлено], q1.BolFillUpd_cnt [Кол-во записей УПД обновлено]
,q2.SupplyOrderFillAdd_cnt [Кол-во записей з. на пост. добавлено], q3.SupplyOrderFillUpd_cnt [Кол-во записей з. на пост. обновлено]
,q4.InvCfmFillAdd_cnt [Кол-во записей согл. счета добавлено],q5.InvCfmFillUpd_cnt [Кол-во записей согл. счета обновлено]
  FROM _engUser e
  --left join _engEntityOperation on 
  outer apply(select ELDBUser, count(*) as BolFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2000 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q 
  outer apply(select ELDBUser, count(*) as BolFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2001 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q1
  outer apply(select ELDBUser, count(*) as SupplyOrderFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2002 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q2
  outer apply(select ELDBUser, count(*) as SupplyOrderFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2003 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q3 
  outer apply(select ELDBUser, count(*) as InvCfmFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2004 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q4 
  outer apply(select ELDBUser, count(*) as InvCfmFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2005 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q5 
  outer apply(select ELDBUser, count(*) as InvDocFillAdd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2006 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q6 
  outer apply(select ELDBUser, count(*) as InvDocFillUpd_cnt from _engLog where ELDBUser = e.EUId and ELEvent = 2007 and ELTimeStamp between DATEADD(day,-6,getdate()) and GETDATE() group by ELDBUser )q7 
  where 
  q.BolFillAdd_cnt is not null or q1.BolFillUpd_cnt  is not null or q2.SupplyOrderFillAdd_cnt  is not null or q3.SupplyOrderFillUpd_cnt  is not null or
q4.InvCfmFillAdd_cnt  is not null or q5.InvCfmFillUpd_cnt is not null
GO