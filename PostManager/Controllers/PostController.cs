using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Post.Models;

namespace PostManager.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Post/Edit/Id
        [HttpGet]
        public ActionResult Edit(int id = 0, string redirectUrl = "")
        {
            PostModel model = null;
            using (Contexts.PostContext context = new Contexts.PostContext())
            {
                model = context.GetPost(id);
                ViewBag.PostPriceTypes = context.GetPostPriceTypes(true);
            }
            return View(model);
        }

        // GET: Post/Edit/Id
        [HttpPost]
        public ActionResult Edit(PostModel model, string redirectUrl = "")
        {
            if (!ModelState.IsValid)
            {
                using (Contexts.PostContext context = new Contexts.PostContext())
                {
                    ViewBag.PostPriceTypes = context.GetPostPriceTypes(true);
                }
                return View("Edit", model);
            }
            else
            {
                using (Contexts.PostContext context = new Contexts.PostContext())
                {
                    context.Edit(model);
                }
                return Redirect(redirectUrl);
            }
        }
    }
}