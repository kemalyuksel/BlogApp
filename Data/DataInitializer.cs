using BlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogApp.Data
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {

            var categories = new List<CategoryModel>()
            {
                new CategoryModel () {Name="Seyahat"},
                 new CategoryModel () {Name="Teknoloji"},
                  new CategoryModel () {Name="Elektronik"}

            };

            foreach (var ctg in categories)
            {
                context.Categories.Add(ctg);
            }
            context.SaveChanges();

        


            var blogs = new List<BlogModel>()
            {
                new BlogModel() {Title="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",Article="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",CategoryModelId=1,When=DateTime.Now ,Url="image_1.jpg",Author="Admin",isApproved=true,Description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,"},
                 new BlogModel() {Title="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",Article="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",CategoryModelId=2,When=DateTime.Now,Url="image_2.jpg",Author="Admin",isApproved=true,Description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,"},
                  new BlogModel() {Title="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",Article="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",CategoryModelId=3,When=DateTime.Now ,Url="image_4.jpg",Author="Admin",isApproved=true,Description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,"},
                  new BlogModel() {Title="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",Article="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,",CategoryModelId=3,When=DateTime.Now ,Url="image_3.jpg",Author="Admin",isApproved=true,Description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis, eius mollitia suscipit,"}
            };

            foreach (var blgs in blogs)
            {
                context.Blogs.Add(blgs);
            }
            context.SaveChanges();



            base.Seed(context);
        }
    }
}