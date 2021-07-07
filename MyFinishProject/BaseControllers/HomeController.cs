using MyFinishProject.Extensions;
using MyFinishProject.Models;
using MyFinishProject.Models.HelpersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyFinishProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExceptionView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetReview()
        {
            var reviews = db.Reviews
                .Include(z => z.User)
                .OrderByDescending(d => d.ReviewDate);

            return PartialView(reviews); 
        }

        public ActionResult SendReviewMessage(Review review)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return View();
            }

            review.UserId = userId;
            review.ReviewDate = DateTime.Now;
            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("GetReview");
        }

        public ActionResult DeleteReviewMessage(int? id)
        {
            var findReviewMessage = db.Reviews.Find(id);

            if(findReviewMessage == null)
            {
                return ExceptionView();
            }
            db.Reviews.Remove(findReviewMessage);
            db.SaveChanges();

            return RedirectToAction("GetReview");
        }

        public ActionResult Delete()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();

            var remove = db.Users.Find(userId);

            var removeUser = db.Users.Remove(remove);
            db.SaveChanges();

            return RedirectToAction("Account", "Login");
        }
    }
}