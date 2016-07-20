using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHome.Helper
{
    public class GlobalDefinition
    {
    }

    public enum CachingKey
    {
        UID = 10001,
        EditMemberUID = 10002,
        Training = 10003,
        TrainingExecution = 10004,
        DailyBookingQuery = 10005,
        MembersQuery = 10006
    }
}