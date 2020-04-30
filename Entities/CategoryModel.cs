using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Entities
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı  giriniz.")]
        public string Name { get; set; }


        public  virtual List<BlogModel> Bloglar { get; set; }
    }
}