using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BlogTag
    {
        public int DocID { get; set; }
        public int CategoryID { get; set; }

        public virtual BlogCategoryDefinition Category { get; set; }
        public virtual BlogArticle Doc { get; set; }
    }
}
