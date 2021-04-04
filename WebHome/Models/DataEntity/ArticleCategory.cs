using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ArticleCategory
    {
        public int DocID { get; set; }
        public string Category { get; set; }

        public virtual ArticleCategoryDefinition CategoryNavigation { get; set; }
        public virtual Article Doc { get; set; }
    }
}
