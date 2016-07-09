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
            ContactUs = 6
        }

        public enum DocumentLevelDefinition
        {
            已刪除 = 0,
            正常 = 1,
            暫存 = 2
        }

        public enum MemberStatusDefinition
        {
            ReadyToRegister = 1001,
            Deleted = 1002,
            Checked = 1003 
        }

        public enum RoleID
        {
            Administrator = 1,
            Coach = 2,
            FreeAgent = 3,
            Learner = 4
        }

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

    }
}