using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Attachment
    {
        public Attachment()
        {
            Articles = new HashSet<Article>();
            CourseContractRevisions = new HashSet<CourseContractRevision>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int AttachmentID { get; set; }
        public string StoredPath { get; set; }
        public int? DocID { get; set; }

        public virtual Document Doc { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<CourseContractRevision> CourseContractRevisions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
