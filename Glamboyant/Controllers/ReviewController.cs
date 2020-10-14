using Glamboyant.Data;
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
            var userID = User.Identity.GetUserId();
            var service = new ReviewService(userID);
            var model = service.GetReviews();
            return View(model);
        }


        public ActionResult RetrieveImage(int id)
        {
            var userId = User.Identity.GetUserId();
            var service = new ReviewService(userId);
            byte[] cover = service.GetImageFromDB(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }        

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            
            if (!ModelState.IsValid) return View(model);

            var service = CreateReviewService();

            if (service.CreateReview(file, model))
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
                    ReviewID = detail.ReviewID,
                    Rating = detail.Rating,
                    Text = detail.Text,
                    Image = detail.Image,
                    UserID = detail.UserID
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewEdit model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];

            if (!ModelState.IsValid) return View(model);

            if (model.ReviewID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateReviewService();

            if (service.UpdateReview(file, model))
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
            var userID = User.Identity.GetUserId();
            var service = new ReviewService(userID);
            return service;
        }
    }
}