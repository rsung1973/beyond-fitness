truncate table PaymentAudit
insert PaymentAudit (paymentID)
select PaymentID from Payment where Status is not null
go
INSERT INTO CourseContract
               (ContractType, ContractDate, Subject, ValidFrom, Expiration, OwnerID, SequenceNo, Lessons, PriceID, Remark, FitnessConsultant, Status, AgentID, 
               ContractNo, TotalCost, EffectiveDate, RegisterID)
SELECT  r.ContractType, r2.RegisterDate AS ContractDate, NULL AS Subject, r2.RegisterDate AS ValidFrom, DATEADD(month, 18, r2.RegisterDate) AS Expiration, 
               r2.UID AS OwnerID, NULL AS SequenceNo, r2.Lessons, r2.ClassLevel AS PriceID, NULL AS Remark, r2.AdvisorID AS FitnessConsultant, 1205 AS Status, 
               r2.AdvisorID AS AgentID, CourseContractType.ContractCode + CONVERT(nvarchar(8), r2.RegisterDate, 112) 
               + RIGHT('000' + CAST(r.RegisterID AS nvarchar(8)), 4) AS ContractNo, 
               r2.Lessons * r.GroupingCount * l2.ListPrice * GroupingLessonDiscount.PercentageOfDiscount / 100 AS TotalCost, NULL AS EffectiveDate, 
               r.RegisterID
FROM     (SELECT  MIN(RegisterLesson.RegisterID) AS RegisterID, RegisterLesson.RegisterGroupID, CASE COUNT(RegisterLesson.RegisterID) 
                               WHEN 1 THEN 1 WHEN 2 THEN 3 WHEN 3 THEN 4 END AS ContractType, COUNT(RegisterLesson.RegisterID) AS GroupingCount
               FROM      RegisterLesson INNER JOIN
                               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
               WHERE   (LessonPriceType.Status NOT IN (103, 1005, 1006, 1007))
               GROUP BY RegisterLesson.RegisterGroupID) AS r INNER JOIN
               RegisterLesson AS r2 ON r.RegisterID = r2.RegisterID INNER JOIN
               CourseContractType ON r.ContractType = CourseContractType.TypeID INNER JOIN
               GroupingLessonDiscount ON r.GroupingCount = GroupingLessonDiscount.GroupingMemberCount INNER JOIN
               LessonPriceType AS l2 ON r2.ClassLevel = l2.PriceID
go
INSERT INTO RegisterLessonContract
               (RegisterID, ContractID)
select RegisterID, ContractID from CourseContract
go
INSERT INTO CourseContractMember
               (ContractID, UID)
SELECT  CourseContract.ContractID, RegisterLesson_1.UID
FROM     CourseContract INNER JOIN
               RegisterLesson ON CourseContract.RegisterID = RegisterLesson.RegisterID INNER JOIN
               RegisterLesson AS RegisterLesson_1 ON RegisterLesson.RegisterGroupID = RegisterLesson_1.RegisterGroupID
go
UPDATE CourseContract
SET        TotalCost = CourseContract.Lessons * LessonPriceType.ListPrice * RegisterLesson.GroupingMemberCount * GroupingLessonDiscount.PercentageOfDiscount
                / 100
FROM     CourseContract INNER JOIN
               RegisterLesson ON CourseContract.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID INNER JOIN
               GroupingLessonDiscount ON RegisterLesson.GroupingMemberCount = GroupingLessonDiscount.GroupingMemberCount
go
INSERT INTO PaymentTransaction
               (PaymentID, BranchID)
SELECT  InstallmentID,ISNull(
                   (SELECT  TOP (1) BranchID
                   FROM     LessonTime
                   WHERE   (RegisterID = TuitionInstallment.RegisterID)),1) AS BranchID
FROM     TuitionInstallment
go
UPDATE Payment
SET        Status = 1205, HandlerID = RegisterLesson.AdvisorID, InvoiceID = 2215
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               RegisterLesson ON TuitionInstallment.RegisterID = RegisterLesson.RegisterID
go
UPDATE Payment
SET        TransactionType = 2
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               RegisterLesson ON TuitionInstallment.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status = 103)
go
UPDATE Payment
SET        TransactionType = 1
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               RegisterLesson ON TuitionInstallment.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status <> 103)
go
INSERT INTO PaymentAudit
               (PaymentID, AuditorID, AuditDate)
SELECT  PaymentID, HandlerID, PayoffDate
FROM     Payment
go
INSERT INTO ContractPayment
               (PaymentID, ContractID)
SELECT  Payment.PaymentID, RegisterLessonContract.ContractID
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               RegisterLesson ON TuitionInstallment.RegisterID = RegisterLesson.RegisterID INNER JOIN
               RegisterLessonContract ON RegisterLesson.RegisterID = RegisterLessonContract.RegisterID
GROUP BY Payment.PaymentID, RegisterLessonContract.ContractID
go
UPDATE Payment
SET        PaymentType = IntuitionCharge.Payment
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               IntuitionCharge ON TuitionInstallment.RegisterID = IntuitionCharge.RegisterID
go
update Payment set PaymentType='現金' where PaymentType='Cash'
update Payment set PaymentType='刷卡' where PaymentType='CreditCard'
go
update CourseContract set SequenceNo=0
go
INSERT INTO RegisterLessonContract
               (ContractID, RegisterID)
select ContractID,TRID from (
SELECT  t.ContractID, t.RegisterID, t.TRID, c.ContractID AS TCID
FROM     (SELECT  RegisterLessonContract.ContractID, RegisterLessonContract.RegisterID, tr.RegisterID AS TRID
               FROM      RegisterLesson INNER JOIN
                               RegisterLessonContract ON RegisterLesson.RegisterID = RegisterLessonContract.RegisterID INNER JOIN
                               GroupingLesson ON RegisterLesson.RegisterGroupID = GroupingLesson.GroupID INNER JOIN
                               RegisterLesson AS tr ON GroupingLesson.GroupID = tr.RegisterGroupID
               WHERE   (RegisterLesson.GroupingMemberCount > 1)) AS t LEFT OUTER JOIN
               RegisterLessonContract AS c ON t.ContractID = c.ContractID AND t.TRID = c.RegisterID) s
where TCID is null
go
insert ContractPayment (PaymentID,ContractID)
select InstallmentID,ContractID from (
SELECT  s.InstallmentID, s.ListPrice, s.Status, s.ContractID, ContractPayment.ContractID AS CHID
FROM     (SELECT  TuitionInstallment.InstallmentID, LessonPriceType.ListPrice, LessonPriceType.Status, RegisterLessonContract.ContractID
               FROM      TuitionInstallment INNER JOIN
                               RegisterLesson ON TuitionInstallment.RegisterID = RegisterLesson.RegisterID INNER JOIN
                               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID LEFT OUTER JOIN
                               RegisterLessonContract ON RegisterLesson.RegisterID = RegisterLessonContract.RegisterID
               WHERE   (LessonPriceType.Status NOT IN (103, 1006, 1007, 1008))) AS s LEFT OUTER JOIN
               ContractPayment ON s.InstallmentID = ContractPayment.PaymentID AND s.ContractID = ContractPayment.ContractID) s1 where CHID is null

go
--UPDATE LessonPriceType
--SET        Description = s.Description, ListPrice = s.ListPrice, Status = s.Status, UsageType = s.UsageType, CoachPayoff = s.CoachPayoff, 
--               CoachPayoffCreditCard = s.CoachPayoffCreditCard, ExcludeQuestionnaire = s.ExcludeQuestionnaire, LowerLimit = s.LowerLimit, 
--               UpperBound = s.UpperBound, BranchID = s.BranchID, DiscountedPrice = s.DiscountedPrice, DurationInMinutes = s.DurationInMinutes, 
--               SeriesID = s.SeriesID
--FROM     LessonPriceType INNER JOIN
--               BeyondFitnessProd3.dbo.LessonPriceType AS s ON LessonPriceType.PriceID = s.PriceID
--WHERE   (s.PriceID <= 80)
--go
--INSERT INTO LessonPriceType
--               (PriceID, Description, ListPrice, Status, UsageType, CoachPayoff, CoachPayoffCreditCard, ExcludeQuestionnaire, LowerLimit, UpperBound, BranchID, 
--               DiscountedPrice, DurationInMinutes, SeriesID)
--select PriceID, Description, ListPrice, Status, UsageType, CoachPayoff, CoachPayoffCreditCard, ExcludeQuestionnaire, LowerLimit, UpperBound, BranchID, 
--               DiscountedPrice, DurationInMinutes, SeriesID
--from BeyondFitnessProd3.dbo.LessonPriceType AS s where s.PriceID not in (select PriceID from LessonPriceType) and s.PriceID<=80
go
-- copy LessonPriceProperty
go
INSERT INTO CourseContractExtension
               (ContractID, BranchID)
SELECT  CourseContract.ContractID, ISNULL(LessonPriceType.BranchID,1)
FROM     CourseContract LEFT OUTER JOIN
               LessonPriceType ON CourseContract.PriceID = LessonPriceType.PriceID
go
UPDATE CourseContract
SET        EffectiveDate = RegisterLesson.RegisterDate
FROM     CourseContract INNER JOIN
               RegisterLessonContract ON CourseContract.ContractID = RegisterLessonContract.ContractID INNER JOIN
               RegisterLesson ON RegisterLessonContract.RegisterID = RegisterLesson.RegisterID
WHERE   (CourseContract.EffectiveDate IS NULL)
go
DELETE FROM Payment
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               IntuitionCharge ON TuitionInstallment.RegisterID = IntuitionCharge.RegisterID INNER JOIN
               RegisterLesson ON IntuitionCharge.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status IN (1005, 1006, 1007))
go

/****** Object:  Table [dbo].[LessonTimeSettlement]    Script Date: 2017/9/21 下午 09:16:15 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[LessonTimeSettlement](
--	[LessonID] [int] NOT NULL,
--	[ProfessionalLevelID] [int] NOT NULL,
--	[SettlementID] [int] NULL,
-- CONSTRAINT [PK_LessonTimeSettlement] PRIMARY KEY CLUSTERED 
--(
--	[LessonID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_LessonTime] FOREIGN KEY([LessonID])
--REFERENCES [dbo].[LessonTime] ([LessonID])
--ON DELETE CASCADE
--GO

--ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_LessonTime]
--GO

--ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_ProfessionalLevel] FOREIGN KEY([ProfessionalLevelID])
--REFERENCES [dbo].[ProfessionalLevel] ([LevelID])
--GO

--ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_ProfessionalLevel]
--GO

--ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_Settlement] FOREIGN KEY([SettlementID])
--REFERENCES [dbo].[Settlement] ([SettlementID])
--GO

--ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_Settlement]
--GO
INSERT INTO LessonTimeSettlement
                            (LessonID, ProfessionalLevelID)
SELECT          LessonTime.LessonID, ServingCoach.LevelID
FROM              ServingCoach RIGHT OUTER JOIN
                            LessonTime ON ServingCoach.CoachID = LessonTime.AttendingCoach
go
INSERT INTO ContractTrustTrack
               (ContractID, EventDate, TrustType, SettlementID, LessonID)
SELECT  RegisterLessonContract.ContractID, LessonTime.ClassTime AS EventDate, 'N' AS Expr1, NULL AS Expr2, LessonTime.LessonID
FROM     LessonTime INNER JOIN
               RegisterLesson ON LessonTime.RegisterID = RegisterLesson.RegisterID INNER JOIN
               RegisterLessonContract ON RegisterLesson.RegisterID = RegisterLessonContract.RegisterID
go
INSERT INTO ContractTrustTrack
               (ContractID, EventDate, TrustType, SettlementID, PaymentID)
SELECT  ContractPayment.ContractID, Payment.PayoffDate AS EventDate, 'B' AS Expr2, NULL AS Expr1, Payment.PaymentID
FROM     Payment INNER JOIN
               TuitionInstallment ON Payment.PaymentID = TuitionInstallment.InstallmentID INNER JOIN
               ContractPayment ON Payment.PaymentID = ContractPayment.PaymentID
			   go
DELETE FROM ContractTrustTrack
FROM     ContractTrustTrack INNER JOIN
               CourseContract ON ContractTrustTrack.ContractID = CourseContract.ContractID
WHERE   (CourseContract.ContractDate < '2017/8/1')
go

Alter TABLE [dbo].[Settlement]
	add
	[StartDate] [datetime] NOT NULL,
	[EndExclusiveDate] [datetime] NOT NULL
go
-- copy EnterpriseCourseContract
-- copy EnterpriseLessonType
-- copy EnterpriseCourseContent
go
insert into RegisterLessonEnterprise (RegisterID,ContractID)
values
(	7835	,	1	),
(	7837	,	1	),
(	7838	,	1	),
(	7839	,	1	),
(	7840	,	1	),
(	7909	,	1	),
(	7911	,	1	),
(	7919	,	1	),
(	7928	,	1	),
(	7929	,	1	),
(	7930	,	1	),
(	7936	,	1	),
(	7941	,	1	),
(	8080	,	1	),
(	7980	,	1	),
(	8004	,	1	),
(	8054	,	1	),
(	8055	,	1	),
(	8165	,	1	),
(	8178	,	1	),
(	8179	,	1	),
(	8181	,	1	),
(	8183	,	1	),
(	8192	,	1	),
(	8220	,	1	),
(	8225	,	1	),
(	8262	,	1	),
(	8287	,	1	),
(	8335	,	1	),
(	8480	,	1	),
(	8693	,	1	)

go

insert into RegisterLessonEnterprise (RegisterID,ContractID)
values
(	7843	,	2	),
(	7844	,	2	),
(	7845	,	2	),
(	7846	,	2	),
(	7847	,	2	),
(	7848	,	2	),
(	7853	,	2	),
(	7854	,	2	),
(	8039	,	2	),
(	8040	,	2	),
(	7910	,	2	),
(	8038	,	2	),
(	8041	,	2	),
(	8042	,	2	)
go
INSERT INTO EnterpriseCourseMember
               (ContractID, UID)
SELECT  RegisterLessonEnterprise.ContractID, RegisterLesson.UID
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
go
DELETE FROM CourseContract
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               RegisterLessonContract ON RegisterLesson.RegisterID = RegisterLessonContract.RegisterID INNER JOIN
               CourseContract ON RegisterLessonContract.ContractID = CourseContract.ContractID
go
INSERT INTO EnterpriseCourseContent
               (ContractID, TypeID, Lessons, ListPrice, DurationInMinutes)
SELECT  ContractID, TypeID, 1 AS Expr1, 0 AS Expr2, 60 AS Expr3
FROM     RegisterLessonEnterprise
WHERE   (TypeID = 1)
GROUP BY ContractID, TypeID
go
INSERT INTO EnterpriseCourseContent
               (ContractID, TypeID, Lessons, ListPrice, DurationInMinutes)
SELECT  ContractID, TypeID, 6 AS Expr1, 300 AS Expr2, 60 AS Expr3
FROM     RegisterLessonEnterprise
WHERE   (TypeID = 4)
GROUP BY ContractID, TypeID
go
INSERT INTO EnterpriseCourseContent
               (ContractID, TypeID, Lessons, ListPrice, DurationInMinutes)
SELECT  ContractID, TypeID, 6 AS Expr1, 1400 AS Expr2, 60 AS Expr3
FROM     RegisterLessonEnterprise
WHERE   (TypeID in (2,3))
GROUP BY ContractID, TypeID
go
INSERT INTO RegisterLessonEnterprise
               (RegisterID, ContractID)
SELECT  r.RegisterID,  RegisterLessonEnterprise.ContractID
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               RegisterLesson AS r ON RegisterLesson.UID = r.UID INNER JOIN
               LessonPriceType ON r.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status IN (103, 1006))
go
UPDATE RegisterLessonEnterprise
SET        TypeID = 1
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status = 1006)
go
UPDATE RegisterLessonEnterprise
SET        TypeID = 4
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (LessonPriceType.Status = 103)
go
UPDATE RegisterLessonEnterprise
SET        TypeID = 2
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (RegisterLessonEnterprise.TypeID IS NULL) AND (RegisterLesson.GroupingMemberCount = 1)
go
UPDATE RegisterLessonEnterprise
SET        TypeID = 3
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               LessonPriceType ON RegisterLesson.ClassLevel = LessonPriceType.PriceID
WHERE   (RegisterLessonEnterprise.TypeID IS NULL) AND (RegisterLesson.GroupingMemberCount = 2)
go


UPDATE LessonTime
SET        GroupID = r1.RegisterGroupID, RegisterID = r1.RegisterID
FROM     (SELECT  MAX(RegisterLessonEnterprise.RegisterID) AS RegisterID, RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, 
                               RegisterLesson.UID
               FROM      RegisterLessonEnterprise INNER JOIN
                               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
               GROUP BY RegisterLessonEnterprise.ContractID, RegisterLessonEnterprise.TypeID, RegisterLesson.UID) AS s INNER JOIN
               RegisterLesson AS r1 ON s.RegisterID = r1.RegisterID INNER JOIN
               RegisterLesson AS r2 ON r1.UID = r2.UID AND r1.ClassLevel = r2.ClassLevel INNER JOIN
               LessonTime ON r2.RegisterID = LessonTime.RegisterID
WHERE   (s.TypeID = 4)
go

DELETE FROM GroupingLesson
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID INNER JOIN
               GroupingLesson ON RegisterLesson.RegisterGroupID = GroupingLesson.GroupID
WHERE   (RegisterLesson.RegisterGroupID NOT IN
                   (SELECT  GroupID
                   FROM     LessonTime)) AND (RegisterLessonEnterprise.TypeID = 4)
go
UPDATE RegisterLesson
SET        Lessons = 6, Attended = 100
FROM     RegisterLessonEnterprise INNER JOIN
               RegisterLesson ON RegisterLessonEnterprise.RegisterID = RegisterLesson.RegisterID
WHERE   (RegisterLesson.RegisterGroupID IS NOT NULL) AND (RegisterLessonEnterprise.TypeID = 4)
go