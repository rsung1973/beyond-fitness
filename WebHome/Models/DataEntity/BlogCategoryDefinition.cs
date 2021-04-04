using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BlogCategoryDefinition
    {
        public BlogCategoryDefinition()
        {
            BlogTags = new HashSet<BlogTag>();
        }

        public int CategoryID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CategoryIndication { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
