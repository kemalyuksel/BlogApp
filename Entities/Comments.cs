using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Entities
{
    public class Comments
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Lütfen yorum giriniz.")]
        public string Article { get; set; }
        public DateTime When { get; set; }
        public string UserName { get; set; }
        public bool isApproved { get; set; }

        public int BlogModelId { get; set; }
        public virtual BlogModel Blog { get; set; }

    }
}