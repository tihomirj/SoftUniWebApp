using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MVC_Blog.Extensions;
using MVC_Blog.Models;
using PagedList;

namespace MVC_Blog.Controllers
{
    [ValidateInput(false)]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts - original index method
        /* public ActionResult Index()
         {
             List<Post> posts = db.Posts
                 .Include(t => t.Topic)
                 .Include(u => u.Author)
                 .Include(c=>c.Comments)
                 .ToList();

             return View(posts);
         }*/
        // GET: Posts - this index method will enable sorting
        public ActionResult Index(string sortOrder, string currentFilterTitle, string currentFilterName, string searchStringTitle, string searchStringName, int? page)
        {
            ViewBag.NameSortParm = sortOrder== "Name" ? "name_desc" : "Name";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TopicSortParm = string.IsNullOrEmpty(sortOrder) ? "topic_desc" : "";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "comment_desc" : "Comment";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            if (searchStringTitle != null && searchStringName != null) 
            {
                page = 1;
            }
            else
            {
                searchStringTitle = currentFilterTitle;
                searchStringName = currentFilterName;
            }
            ViewBag.CurrentFilterTitle = searchStringTitle;
            ViewBag.CurrentFilterName = searchStringName;
            ViewBag.CurrentSort = sortOrder;

            var posts = db.Posts
                .Include(t => t.Topic)
                .Include(u => u.Author)
                .Include(c => c.Comments);

            if (!String.IsNullOrEmpty(searchStringTitle))
            {
                posts = posts.Where(p => p.Title.ToUpper().Contains(searchStringTitle.ToUpper()));
            }
            if (!String.IsNullOrEmpty(searchStringName))
            {
                posts = posts.Where(p => p.Author.FullName.ToUpper().Contains(searchStringName.ToUpper()) || 
                                            p.Author.UserName.ToUpper().Contains(searchStringName.ToUpper()));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    posts = posts.OrderByDescending(p => p.Title);
                    break;
                case "Title":
                    posts = posts.OrderBy(p => p.Title);
                    break;

                case "comment_desc":
                    posts = posts.OrderByDescending(p => p.Comments.Count);
                    break;
                case "Comment":
                    posts = posts.OrderBy(p => p.Comments.Count);
                    break;
                case "topic_desc":
                    posts = posts.OrderByDescending(p => p.Topic.Name);
                    break;
                case "Name":
                    posts = posts.OrderBy(p => p.Author.FullName);
                    break;
                case "name_desc":
                    posts = posts.OrderByDescending(p => p.Author.FullName);
                    break;
                case "Date":
                    posts = posts.OrderBy(p => p.Date);
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(p => p.Date);
                    break;
                default:
                    posts = posts.OrderBy(p => p.Topic.Name);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.CountPosts = posts.Count();
            return View(posts.ToPagedList(pageNumber,pageSize));
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            
            var comments = db.Posts.Find(id).Comments.ToList();
            ViewBag.Comments = comments;

            var tags = db.Posts.Find(id).Tags.ToList();
            ViewBag.Tags = tags;

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            var topics = db.Topics.ToList();
            ViewBag.Topics = topics;

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,TopicId,Title,Body")] Post post) //The date is not coming in from the form
        {
            if (ModelState.IsValid)
            {
                post.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                post.Date=DateTime.Now; // the date of the post is always today
                db.Posts.Add(post);
                db.SaveChanges();
                this.AddNotification("Статията е създадена.",NotificationType.INFO);
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = db.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null || (post.Author.UserName != User.Identity.Name && !User.IsInRole("Administrators")))
            {
                return HttpNotFound();
            }
            var authors = db.Users.ToList();
            ViewBag.Authors = authors;

            var topics = db.Topics.ToList();
            ViewBag.Topics = topics;

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,TopicId,Title,Body,Date,AuthorId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Статията е редактирана.", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = db.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null || (post.Author.UserName != User.Identity.Name && !User.IsInRole("Administrators")))
            {
                return HttpNotFound();
            }
            
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            this.AddNotification("Статията е изтрита.", NotificationType.INFO);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
