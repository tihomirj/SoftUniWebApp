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

            List<Post> post = db.Posts
                .Include(p=>p.Author)
                .OrderByDescending(p => p.Date)
                .Take(3)
                .ToList();

            return View(post);
        }

       
    }
}