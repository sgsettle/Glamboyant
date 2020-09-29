using Glamboyant.Models.UserModels;
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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new UserService(userID);
            var model = service.GetUsers();
            return View(model);
        }
                public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateUserService();

            if (service.CreateUserService(model))
            {
                TempData["SaveResult"] = "User was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "User could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateUserService();
            var model = svc.GetUserByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateUserService();
            var detail = service.GetUserByID(id);
            var model =
                new UserEdit
                {
                    UserID = detail.UserID,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.UserID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateUserService();

            if (service.UpdateUserService(model))
            {
                TempData["SaveResult"] = "User was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "User could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserService();
            var model = svc.GetUserByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserService(int id)
        {
            var service = CreateUserService();

            service.DeleteUserService(id);

            TempData["SaveResult"] = "User was deleted.";

            return RedirectToAction("Index");
        }

        private UserService CreateUserService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new UserService(userID);
            return service;
        }
    }
}