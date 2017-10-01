set IDENTITY_INSERT Payment on
go
INSERT INTO Payment
               (PaymentID, PayoffAmount, PayoffDate)
SELECT  InstallmentID, PayoffAmount, PayoffDate
FROM     TuitionInstallment
go
set IDENTITY_INSERT Payment off
go

/****** Object:  Table [dbo].[CourseContractExtension]    Script Date: 2017/9/13 ¤U¤È 09:10:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CourseContractExtension](
	[ContractID] [int] NOT NULL,
	[BranchID] [int] NOT NULL,
	[RevisionTrackingID] [int] NULL,
	[SettlementPrice] [int] NULL,
 CONSTRAINT [PK_CourseContractExtension] PRIMARY KEY CLUSTERED 
(
	[ContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CourseContractExtension]  WITH CHECK ADD  CONSTRAINT [FK_CourseContractExtension_BranchStore] FOREIGN KEY([BranchID])
REFERENCES [dbo].[BranchStore] ([BranchID])
GO

ALTER TABLE [dbo].[CourseContractExtension] CHECK CONSTRAINT [FK_CourseContractExtension_BranchStore]
GO

ALTER TABLE [dbo].[CourseContractExtension]  WITH CHECK ADD  CONSTRAINT [FK_CourseContractExtension_CourseContract] FOREIGN KEY([ContractID])
REFERENCES [dbo].[CourseContract] ([ContractID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CourseContractExtension] CHECK CONSTRAINT [FK_CourseContractExtension_CourseContract]
GO

ALTER TABLE [dbo].[CourseContractExtension]  WITH CHECK ADD  CONSTRAINT [FK_CourseContractExtension_CourseContractRevision] FOREIGN KEY([RevisionTrackingID])
REFERENCES [dbo].[CourseContractRevision] ([RevisionID])
GO

ALTER TABLE [dbo].[CourseContractExtension] CHECK CONSTRAINT [FK_CourseContractExtension_CourseContractRevision]
GO



/****** Object:  Table [dbo].[LessonTimeSettlement]    Script Date: 2017/9/17 ¤U¤È 12:56:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LessonTimeSettlement](
	[LessonID] [int] NOT NULL,
	[ProfessionalLevelID] [int] NOT NULL,
	[SettlementID] [int] NULL,
 CONSTRAINT [PK_LessonTimeSettlement] PRIMARY KEY CLUSTERED 
(
	[LessonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_LessonTime] FOREIGN KEY([LessonID])
REFERENCES [dbo].[LessonTime] ([LessonID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_LessonTime]
GO

ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_ProfessionalLevel] FOREIGN KEY([ProfessionalLevelID])
REFERENCES [dbo].[ProfessionalLevel] ([LevelID])
GO

ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_ProfessionalLevel]
GO

ALTER TABLE [dbo].[LessonTimeSettlement]  WITH CHECK ADD  CONSTRAINT [FK_LessonTimeSettlement_Settlement] FOREIGN KEY([SettlementID])
REFERENCES [dbo].[Settlement] ([SettlementID])
GO

ALTER TABLE [dbo].[LessonTimeSettlement] CHECK CONSTRAINT [FK_LessonTimeSettlement_Settlement]
GO


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PaymentAudit
	DROP CONSTRAINT FK_PaymentAudit_Payment
GO
ALTER TABLE dbo.PaymentTransaction
	DROP CONSTRAINT FK_PaymentTransaction_Payment
GO
ALTER TABLE dbo.TuitionAchievement
	DROP CONSTRAINT FK_TuitionAchievement_Payment
GO
ALTER TABLE dbo.ContractPayment
	DROP CONSTRAINT FK_ContractPayment_Payment
GO
ALTER TABLE dbo.Payment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ContractPayment WITH NOCHECK ADD CONSTRAINT
	FK_ContractPayment_Payment FOREIGN KEY
	(
	PaymentID
	) REFERENCES dbo.Payment
	(
	PaymentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ContractPayment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.TuitionAchievement WITH NOCHECK ADD CONSTRAINT
	FK_TuitionAchievement_Payment FOREIGN KEY
	(
	InstallmentID
	) REFERENCES dbo.Payment
	(
	PaymentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.TuitionAchievement SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PaymentTransaction WITH NOCHECK ADD CONSTRAINT
	FK_PaymentTransaction_Payment FOREIGN KEY
	(
	PaymentID
	) REFERENCES dbo.Payment
	(
	PaymentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.PaymentTransaction SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PaymentAudit WITH NOCHECK ADD CONSTRAINT
	FK_PaymentAudit_Payment FOREIGN KEY
	(
	PaymentID
	) REFERENCES dbo.Payment
	(
	PaymentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.PaymentAudit SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



-- add BranchStore to Organization
-- add CourseContractType
-- add temp column RegisterID in table CourseContract
-- add DocumentType data
-- insert dummy invoice
