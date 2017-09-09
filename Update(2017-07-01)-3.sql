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
SET        Status = 1205, HandlerID = RegisterLesson.AdvisorID, InvoiceID = 2199
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
update Payment set PaymentType='²{ª÷' where PaymentType='Cash'
update Payment set PaymentType='¨ê¥d' where PaymentType='CreditCard'
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
