using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractRevision
    {
        public CourseContractRevision()
        {
            ContractElements = new HashSet<ContractElement>();
            CourseContractExtensions = new HashSet<CourseContractExtension>();
        }

        public int RevisionID { get; set; }
        public int? OriginalContract { get; set; }
        public int RevisionNo { get; set; }
        public string Reason { get; set; }
        public int? OperationMode { get; set; }
        public int? AttachmentID { get; set; }
        public int? MonthExtension { get; set; }
        public int? BySelf { get; set; }
        public int? ProcessingFee { get; set; }
        public int? CauseForEnding { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual CourseContract OriginalContractNavigation { get; set; }
        public virtual CourseContract Revision { get; set; }
        public virtual CourseContractRevisionItem CourseContractRevisionItem { get; set; }
        public virtual ICollection<ContractElement> ContractElements { get; set; }
        public virtual ICollection<CourseContractExtension> CourseContractExtensions { get; set; }
    }
}
