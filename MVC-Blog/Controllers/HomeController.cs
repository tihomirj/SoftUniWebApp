using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Blog.Models;

namespace MVC_Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();

            List<Post> mostRecentPost = db.Posts
                .Include(p=>p.Author)
                .OrderByDescending(p => p.Date)
                .Take(3)
                .ToList();

            List<Post> mostCommentedPosts = db.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Comments.Count)
                .Take(3)
                .ToList();
            ViewBag.MostCommentedPosts = mostCommentedPosts;

            return View(mostRecentPost);
        }

       
    }
}