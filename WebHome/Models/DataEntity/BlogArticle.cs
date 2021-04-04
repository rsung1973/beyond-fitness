using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BlogArticle
    {
        public BlogArticle()
        {
            BlogTags = new HashSet<BlogTag>();
        }

        public int DocID { get; set; }
        public string Title { get; set; }
        public int? AuthorID { get; set; }
        public string Subtitle { get; set; }
        public string BlogID { get; set; }

        public virtual UserProfile Author { get; set; }
        public virtual Document Doc { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
