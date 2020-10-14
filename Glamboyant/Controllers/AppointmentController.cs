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
        //private ApplicationDbContext _db = new ApplicationDbContext();


        // GET: Appointment
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var service = new AppointmentService(userID);
            var model = service.GetAppointments();
            return View(model);
        }

        public ActionResult Create()
        {
            //var services = new SelectList(_db.HairServices.ToList(), "HairServiceID", "Name");
            ViewBag.HairServiceID = GetHairServiceNames();
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
                    AppointmentID = detail.AppointmentID,
                    AppointmentDate = detail.AppointmentDate,
                    AppointmentTime = detail.AppointmentTime,
                    Address = detail.Address,
                    HairServiceID = detail.HairServiceID
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

            if (service.UpdateAppointment(model))
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
            var userID = User.Identity.GetUserId();
            var service = new AppointmentService(userID);
            return service;
        }

        private List<SelectListItem> GetHairServiceNames()
        {
            var service = new HairServiceService(User.Identity.GetUserId());
            List<SelectListItem> hairServices = new List<SelectListItem>();
            foreach (var hairService in service.GetHairServices())
                hairServices.Add(
                    new SelectListItem
                    {
                        Text = hairService.Name
                        //Value = hairService.HairServiceID.ToString()
                    });
            return hairServices;
        }
    }
}