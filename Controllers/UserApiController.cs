/*using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using Zalgiris.App;
using Zalgiris.Models;
using AspNet;

namespace Zalgiris.Controllers
{
    
    public class UserApiController : Controller
    {
        public ActionResult Index()
        {
            // Your logic here
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Your logic here
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Your logic here
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Your logic to create a user
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Your logic here
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // Your logic to edit a user
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Your logic here
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Your logic to delete a user
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: api/UserApiController/AddUser
        [HttpPost]
        public ActionResult AddUser(string newUserJson)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["IsAdmin"] != null && (bool)System.Web.HttpContext.Current.Session["IsAdmin"])
                {
                    var newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(newUserJson);
                    UsersController.Add(newUser);
                    var debugInfo = $"{newUser.Username} {newUser.Password} {newUser.Email} {newUser.IsAdmin}";
                    return Json(new { success = true, message = "User added successfully!", debugInfo });
                }
                else
                {
                    // Return unauthorized response if the user is not an admin
                    return Content("Unauthorized: User is not authorized to add a new user.");
                }
            }
            catch (Exception ex)
            {
                var newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(newUserJson);
                var debugInfo = $"{newUser.Username} {newUser.Password} {newUser.Email} {newUser.IsAdmin}";
                return Json(new { success = false, message = "User not added", debugInfo });
            }
        }
    }
}
*/