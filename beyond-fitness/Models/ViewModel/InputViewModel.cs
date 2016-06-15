using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHome.Models.ViewModel
{
    public class InputViewModel
    {
        public String Name { get; set; }
        public String Id { get; set; }
        public String Label { get; set; }
        public String Value { get; set; }
        public String PlaceHolder { get; set; }
        public String ErrorMessage { get; set; }
        public bool? IsValid { get; set; }
        public String InputType { get; set; }
    }
}