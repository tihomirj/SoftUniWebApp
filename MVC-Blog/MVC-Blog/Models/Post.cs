using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class Post
    {
        public Post()
        {
            this.Date = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public int TopicId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Съдържание")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Автор")]
        public ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual Topic Topic { get; set; }
    }
}