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
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Appointment
        public ActionResult Index()
        {
            var content = _db.Reviews.Select(s => new
            {
                s.ReviewID,
                s.Rating,
                s.Text,
                s.Image
            });
            
            List<ReviewListItem> reviews = content.Select(item => new ReviewListItem()
            {
                ReviewID = item.ReviewID,
                Rating = item.Rating,
                Text = item.Text,
                Image = item.Image
            }).ToList();

            return View(reviews);
            //var userID = Guid.Parse(User.Identity.GetUserId());
            //var service = new ReviewService(userID);
            //var model = service.GetReviews();
            //return View(model);
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDB(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
            
        }

        public byte[] GetImageFromDB(int id)
        {
            var q = from temp in _db.Reviews where temp.ReviewID == id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            //var photoService = new ReviewCreate();
            //int i = photoService.UploadImageInDatabase(file, model);
            //if (i == 1)
            //{
            //    return RedirectToAction("Index");
            //}
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