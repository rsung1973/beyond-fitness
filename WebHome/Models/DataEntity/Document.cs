using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Document
    {
        public Document()
        {
            Attachments = new HashSet<Attachment>();
            DerivedDocumentSources = new HashSet<DerivedDocument>();
            DocumentPrintLogs = new HashSet<DocumentPrintLog>();
        }

        public int DocID { get; set; }
        public int? DocType { get; set; }
        public DateTime DocDate { get; set; }
        public int? CurrentStep { get; set; }
        public int? ChannelID { get; set; }

        public virtual LevelExpression CurrentStepNavigation { get; set; }
        public virtual DocumentType DocTypeNavigation { get; set; }
        public virtual Article Article { get; set; }
        public virtual BlogArticle BlogArticle { get; set; }
        public virtual DerivedDocument DerivedDocumentDoc { get; set; }
        public virtual DocumentPrintQueue DocumentPrintQueue { get; set; }
        public virtual InvoiceAllowance InvoiceAllowance { get; set; }
        public virtual InvoiceAllowanceSeller InvoiceAllowanceSeller { get; set; }
        public virtual InvoiceItem InvoiceItem { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<DerivedDocument> DerivedDocumentSources { get; set; }
        public virtual ICollection<DocumentPrintLog> DocumentPrintLogs { get; set; }
    }
}
