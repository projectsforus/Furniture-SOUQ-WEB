using LAQY.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAQY.Controllers
{
    public class HandlerController : BaseController
    {
        // GET: Handler
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Lang(string user,string lang)
        {
            if (lang == "en")
            {
                CultureHelper.CurrentCulture = 0;
                Session["CurrentCulture"] = 0;
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                CultureHelper.CurrentCulture = 1;
                Session["CurrentCulture"] = 1;
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}