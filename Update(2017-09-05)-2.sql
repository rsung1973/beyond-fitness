set IDENTITY_INSERT Payment on
go
INSERT INTO Payment
               (PaymentID, PayoffAmount, PayoffDate)
SELECT  InstallmentID, PayoffAmount, PayoffDate
FROM     TuitionInstallment
go
set IDENTITY_INSERT Payment off

-- add BranchStore to Organization
-- add CourseContractType
-- add temp column RegisterID in table CourseContract
-- add DocumentType data
