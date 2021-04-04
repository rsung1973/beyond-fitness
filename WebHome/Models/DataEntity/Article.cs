using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Article
    {
        public Article()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
        }

        public int DocID { get; set; }
        public string Title { get; set; }
        public string ArticleContent { get; set; }
        public int? AuthorID { get; set; }
        public int? Illustration { get; set; }
        public string Subtitle { get; set; }

        public virtual UserProfile Author { get; set; }
        public virtual Document Doc { get; set; }
        public virtual Attachment IllustrationNavigation { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
