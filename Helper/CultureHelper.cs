using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace LAQY.Helper
{
    public class CultureHelper
    {
        protected HttpSessionState session;
        public CultureHelper(HttpSessionState httpSessionState)
        {
            session = httpSessionState;
        }
        public static int  CurrentCulture
        {
            get
            {
                if(Thread.CurrentThread.CurrentUICulture.Name =="en")
                {
                    return 0;
                }
                else if(Thread.CurrentThread.CurrentUICulture.Name == "ar")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if(value==1)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                }
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
    }
}