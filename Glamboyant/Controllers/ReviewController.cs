using Glamboyant.Models.ReviewModels;
using Glamboyant.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glamboyant.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ReviewService(userID);
            var model = service.GetReviews();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateReviewService();

            if (service.CreateReview(model))
            {
                TempData["SaveResult"] = "Your review was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your review could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateReviewService();
            var model = svc.GetReviewByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateReviewService();
            var detail = service.GetReviewByID(id);
            var model =
                new ReviewEdit
                {
                    Rating = detail.Rating,
                    Text = detail.Text,
                    UserID = detail.UserID
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ReviewID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateReviewService();

            if (service.UpdateReview(model))
            {
                TempData["SaveResult"] = "Your review was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your review could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateReviewService();
            var model = svc.GetReviewByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int id)
        {
            var service = CreateReviewService();

            service.DeleteReview(id);

            TempData["SaveResult"] = "Your review was deleted.";

            return RedirectToAction("Index");
        }

        private ReviewService CreateReviewService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ReviewService(userID);
            return service;
        }
    }
}