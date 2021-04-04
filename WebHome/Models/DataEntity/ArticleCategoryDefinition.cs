using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ArticleCategoryDefinition
    {
        public ArticleCategoryDefinition()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
        }

        public string Category { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
