using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHome.Models.Locale
{
    public class Naming
    {
        private Naming()
        {

        }

        public enum DataResultMode
        {
            Display = 0,
            Print = 1,
            Download = 2
        }

        public enum DocumentTypeDefinition
        {
            Professional = 1,
            Knowledge = 2,
            Rental = 3,
            Products = 4,
            Cooperation = 5,
            ContactUs = 6,
            Inspirational = 7,
            E_Invoice = 8,
            E_InvoiceCancellation = 9,
            E_Allowance = 10,
            E_AllowanceCancellation = 11,
            PaperInvoice = 12,
            PaperInvoiceCancellation = 13
        }

        public enum DocumentLevelDefinition
        {
            已刪除 = 0,
            正常 = 1,
            暫存 = 2,
            自主訓練 = 103,
            自由教練預約 = 1004,
            內部訓練 = 1005,
            體驗課程 = 1006,
            點數兌換課程 = 1007
        }

        public enum LessonPriceStatus
        {
            已刪除 = 0,
            一般課程 = 1,
            暫存 = 2,
            自主訓練 = 103,
            自由教練預約 = 1004,
            內部訓練 = 1005,
            體驗課程 = 1006,
            點數兌換課程 = 1007
        }

        public enum LessonPriceFeature
        {
            自主訓練 = 103,
            自由教練預約 = 1004,
            內部訓練 = 1005,
            體驗課程 = 1006,
            點數兌換課程 = 1007,
            舊會員續約 = 1008
        }

        public enum LessonSeriesStatus
        {
            已停用 = 0,
            已啟用 = 1,
            準備中 = 2,
        }

        public enum MemberStatusDefinition
        {
            ReadyToRegister = 1001,
            Deleted = 1002,
            Checked = 1003,
            Anonymous = 1004 
        }

        public enum MemberStatus
        {
            尚未註冊 = 1001,
            已停用 = 1002,
            已註冊 = 1003,
            訪客 = 1004
        }

        public enum RoleID
        {
            Administrator = 1,
            Coach = 2,
            FreeAgent = 3,
            Learner = 4,
            Guest = 5,
            Accounting = 6,
            Officer = 7,
            Assistant = 8,
            Manager = 9,
            ViceManager = 10,
            Preliminary = 11
        }

        public static readonly String[] RoleName = new String[] {
            "",
            "系統管理員",
            "體能顧問",
            "自由教練",
            "學員",
            "訪客",
            "財務助理",
            "經理",
            "行政助理"
        };

        public enum LessonStatus
        {
            準備上課 = 100,
            上課中 = 101,
            課程結束 = 102
        }

        public enum QuestionType
        {
            問答題 = 200,
            單選題 = 201,
            多重選 = 202,
            是非題 = 203,
            單選其他 = 204,
            多重選其他 = 205
        }

        public enum FitnessAssessmentGroup
        {
            檢測體能 = 1,
        }

        public enum IncommingMessageStatus
        {
            未讀 = 3,
            已讀 = 4,
            拒答 = 5
        }

        public enum LessonQueryType
        {
            一般課程 = 0,
            自主訓練,
            內部訓練,
            體驗課程
        }

        public enum ProfessionalCategory
        {
            舊制 = 1101,
            新制 = 1102
        }

        public enum ProfessionLevelDefinition
        {
            FM_1st = 1,
            AFM_1st = 2,
            Level_5_1st = 14,
            Level_4_1st = 3,
            Level_3_1st = 4,
            Level_2_1st = 5,
            Level_1_1st = 6,
            FM_2nd = 7,
            AFM_2nd = 8,
            Level_5_2nd = 9,
            Level_4_2nd = 10,
            Level_3_2nd = 11,
            Level_2_2nd = 12,
            Level_1_2nd = 13
        }

        public enum CourseContractStatus
        {
            草稿 = 1201,
            待確認 = 1202,
            待簽名 = 1203,
            待審核 = 1204,
            已生效 = 1205,
        }

        public enum PaymentTransactionType
        {
            體能顧問費 = 1,
            自主訓練 = 2,
            飲品 = 3,
            運動商品 = 4
        }

        public enum MerchandiseStatus
        {
            Discontinued = 0,
            OnSale = 1,
            SoldOut = 2
        }


        public enum InvoiceTypeDefinition
        {
            三聯式 = 1,
            二聯式 = 2,
            二聯式收銀機 = 3,
            特種稅額 = 4,
            電子計算機 = 5,
            三聯式收銀機 = 6,
            一般稅額計算之電子發票 = 7,
            特種稅額計算之電子發票 = 8,
        }

    }
}