using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebHome.Models.ViewModel;

namespace WebHome.Helper
{
    public static class MvcExtensions
    {
        public static void RenderInput(this HtmlHelper Html, String label, String id, String name, String placeHolder, ModelStateDictionary modelState)
        {
            bool? isValid = modelState == null ? (bool?)null : modelState[name] == null || modelState[name].Errors.Count <= 0;
            Html.RenderPartial("~/Views/Shared/HtmlInput.ascx",
                new InputViewModel
                {
                    ErrorMessage = isValid == false ? String.Join("、", modelState[name].Errors.Select(r => r.ErrorMessage)) : null,
                    Id = id,
                    IsValid = isValid,
                    Label = label,
                    PlaceHolder = placeHolder,
                    Name = name
                });
        }
    }
}