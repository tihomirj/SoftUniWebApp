using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;

        }

        [Key]
        public int Id { get; set; }


        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст на коментара")]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual Post Post { get; set; }
    }
}