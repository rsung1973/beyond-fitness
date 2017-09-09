using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using Utility;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class InvoiceExtensionMethods
    {
        public static bool IsB2C(this InvoiceBuyer buyer)
        {
            return String.IsNullOrEmpty(buyer.ReceiptNo) || buyer.ReceiptNo == "0000000000";
        }

        public static bool IsB2C(this InvoiceAllowanceBuyer buyer)
        {
            return buyer.ReceiptNo == "0000000000";
        }
    }
}