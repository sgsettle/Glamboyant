using Glamboyant.Data;
using Glamboyant.Models.AppointmentModels;
using Glamboyant.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glamboyant.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();


        // GET: Appointment
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AppointmentService(userID);
            var model = service.GetAppointments();
            return View(model);
        }

        public ActionResult Create()
        {
            var services = new SelectList(_db.HairServices.ToList(), "HairServiceID", "Name");
            ViewBag.HairServices = services;
            var users = new SelectList(_db.Users.ToList(), "UserID", "FullName");
            ViewBag.Users = users;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAppointmentService();

            if (service.CreateAppointmentService(model))
            {
                TempData["SaveResult"] = "Your appointment was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your appointment could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateAppointmentService();
            var model = svc.GetAppointmentByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAppointmentService();
            var detail = service.GetAppointmentByID(id);
            var model =
                new AppointmentEdit
                {
                    AppointmentDate = detail.AppointmentDate,
                    HairServiceID = detail.HairServiceID,
                    UserID = detail.UserID
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AppointmentID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAppointmentService();

            if (service.UpdateAppointmentService(model))
            {
                TempData["SaveResult"] = "Your appointment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your appointment could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAppointmentService();
            var model = svc.GetAppointmentByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointmentService(int id)
        {
            var service = CreateAppointmentService();

            service.DeleteAppointmentService(id);

            TempData["SaveResult"] = "Your appointment was deleted.";

            return RedirectToAction("Index");
        }

        private AppointmentService CreateAppointmentService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AppointmentService(userID);
            return service;
        }
    }
}