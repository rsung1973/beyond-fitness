using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.PlugInAdapter;
using WebSupergoo.ABCpdf8;

namespace ABCPdfWrapper
{
    public class PdfUtility : IPdfUtility
    {
        public void ConvertHtmlToPDF(string htmlUrl, string pdfFile, double timeOutInMinute)
        {
            String htmlFile = htmlUrl.Substring(0, 4).ToLower() == "http" ? htmlUrl : "file://" + htmlUrl;

            Doc theDoc = new Doc();

            try
            {
                bool pagedOutput = true;
                theDoc.HtmlOptions.AddLinks = true;
                //theDoc.HtmlOptions.Engine = EngineType.Gecko;
                //theDoc.HtmlOptions.PageCacheEnabled = false;
                //theDoc.HtmlOptions.AddForms = false;
                //theDoc.HtmlOptions.AddLinks = false;
                //theDoc.HtmlOptions.AddMovies = false;
                //theDoc.HtmlOptions.FontEmbed = false;
                //theDoc.HtmlOptions.UseResync = false;
                //theDoc.HtmlOptions.UseVideo = false;
                ////theDoc.HtmlOptions.UseJava = Request.Form["UseJava"] == "on";
                ////theDoc.HtmlOptions.UseActiveX = Request.Form["UseActiveX"] == "on";
                //theDoc.HtmlOptions.UseScript = false;
                //theDoc.HtmlOptions.HideBackground = false;
                //theDoc.HtmlOptions.Timeout = 15550;
                //theDoc.HtmlOptions.LogonName = "";
                //theDoc.HtmlOptions.LogonPassword = "";

                //theDoc.HtmlOptions.BrowserWidth = 0;
                //theDoc.HtmlOptions.ImageQuality = 101;

                theDoc.Rect.Inset(5, 5);
                //theDoc.Rect.Inset(5, 5);

                //				XRect rect = theDoc.Rect;
                //				rect.Bottom += 200;
                //				theDoc.Rect.SetRect(rect.Left,rect.Bottom,rect.Width,rect.Height);

                // add url to document
                try
                {
                    int theID = theDoc.AddImageUrl(htmlFile);

                    if (pagedOutput)
                    {
                        // add up to 3 pages
                        while (true)
                        {
                            if (theDoc.GetInfo(theID, "Truncated") != "1")
                                break;
                            theDoc.Page = theDoc.AddPage();
                            theID = theDoc.AddImageToChain(theID);
                        }
                    }

                    theDoc.Save(pdfFile);
                    theDoc.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //					Response.Redirect("warning.aspx?message=Web page is inaccessible. Please try another URL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}
