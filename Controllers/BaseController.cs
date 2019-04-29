using LAQY.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAQY.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void ExecuteCore()
        {
            int culuture = 0;
            if(this.Session == null || this.Session["CurrentCulture"] == null)
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culuture);
                this.Session["CurrentCulture"] = culuture;
            }
            else
            {
                culuture = (int)this.Session["CurrentCulture"];
            }
            CultureHelper.CurrentCulture = culuture;
            base.ExecuteCore();
        }
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
    }
}