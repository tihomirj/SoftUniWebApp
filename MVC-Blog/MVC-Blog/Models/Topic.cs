using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Категория")]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}