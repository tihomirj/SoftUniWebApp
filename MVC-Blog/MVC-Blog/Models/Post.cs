using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class Post
    {
        public Post()
        {
            this.Date = DateTime.Now;
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        [Display(Name = "Статия")]
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Автор")]
        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual Topic Topic { get; set; }
    }
}