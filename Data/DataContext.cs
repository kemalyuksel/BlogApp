using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext():base("BlogAppDb")
        {

        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }

    }
}