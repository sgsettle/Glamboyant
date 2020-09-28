using Glamboyant.Models.HairServiceModels;
using Glamboyant.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glamboyant.Controllers
{
    public class HairServiceController : Controller
    {
        // GET: HairService
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new HairServiceService(userID);
            var model = service.GetHairServices();
            return View(model);
        }

        // GET: Create HairService
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HairServiceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateHairServiceService();

            if (service.CreateHairService(model))
            {
                TempData["SaveResult"] = "Your service was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Service could not be created.");

            return View(model);
        }

        private HairServiceService CreateHairServiceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new HairServiceService(userID);
            return service;
        }
    }
}