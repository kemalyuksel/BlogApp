using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Entities
{
    public class BlogModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Blog başlığı giriniz.")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Blog içeriği  giriniz.")]
        public string Article { get; set; }

        public DateTime When { get; set; }

        public string Url { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Açıklama giriniz.")]
        public string Description { get; set; }

        public string Author { get; set; }
        public bool isApproved { get; set; }

        public int Likes { get; set; }
        public int Looks { get; set; }


        public int CategoryModelId { get; set; }
        public virtual CategoryModel Category { get; set; }

        public int CommentsId { get; set; }
        public virtual List<Comments> Comments { get; set; }

    }
}