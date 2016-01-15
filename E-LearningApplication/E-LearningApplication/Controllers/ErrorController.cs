using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_LearningApplication.Controllers
{
    public class ErrorController : Controller
    {
        // Return the 404 not found page 
        public ActionResult Error404()
        {
            ViewBag.Title = "404 - Not Found!";
            ViewBag.Error = "You messed up...I don't know what you're asking of me!";
            return View("CustomErrorView");
        }

        // Return the 500 not found page 
        public ActionResult Error500()
        {
            ViewBag.Title = "Error 500...";
            ViewBag.Error = "An internal server error occurred! That or the server burned down...";
            return View("CustomErrorView");
        }
    }
}
