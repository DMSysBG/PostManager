using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/SysExceptions
        public ActionResult SysExceptions()
        {
            List<Models.ExceptionModel> model = null;
            using (Contexts.HomeContext context = new Contexts.HomeContext())
            {
                model = context.GetSysExceptions();
            }
            return View(model);
        }

        // GET: Home/SysExceptions/id
        [HttpPost]
        public JsonResult DeleteSysException(int id = 0)
        {
            try
            {
                using (Contexts.HomeContext context = new Contexts.HomeContext())
                {
                    context.DeleteSysException(id);
                }
                return Json(new Models.JsonResponseModel()
                {
                    Status = 0,
                    Message = "Успешно изтриване."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Models.JsonResponseModel()
                {
                    Status = 1,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Home/InvalidPost
        public ActionResult InvalidPost()
        {
            List<Models.ExceptionModel> model = null;
            using (Contexts.HomeContext context = new Contexts.HomeContext())
            {
                model = context.GetInvalidPost();
            }
            return View(model);
        }
    }
}