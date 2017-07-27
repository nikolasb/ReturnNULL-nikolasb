using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EachVoice.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error turn off the default error page, it used to piss me off...
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}