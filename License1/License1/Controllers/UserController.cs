using License1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace License1.Controllers
{
    public class UserController : Controller
    {
        LicenseEntities db = new LicenseEntities();
        IUser userInterface;
        public UserController() : this(new UserRepository()) { }
        // GET: /User/
        public UserController(IUser repository)

        {

           userInterface = repository;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Register(User user)
        { 
        try
            {
                if (ModelState.IsValid)
                {
                        using (db)
                         {
                             userInterface.CreateNewUser(user);
                             return RedirectToAction("Index","User");
                         }
                     }
                     else
                     {
                         ModelState.AddModelError("", "Invalid Data Given");
                     }
                
                }

        catch (DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName,
                                  validationError.PropertyName, validationError.ErrorMessage);
                }
            }
        }

            return View();
        }
        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            userInterface.Delete(id);
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }



        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Admin/Login        
        [HttpPost]
        public ActionResult Login(User user)
        {
            using (db)
            {
                try
                {
                    User us = db.Users.SingleOrDefault(m => m.Username == user.Username);
                    var UserId = (from j in db.Users.Where(m => m.Username == user.Username)
                                  select j.Id).SingleOrDefault<System.Int32>();
                    if (us != null)
                    {
                        if (us.Password == user.Password)
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);
                            Session["UserId"] = UserId;
                            return RedirectToAction("Index", "User");
                        }
                        ModelState.AddModelError("", "Invalid Username or Password");
                        return View();
                    }
                    throw new ArgumentException("User doesn't exists");
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View();
                }
            }
        }
        



        public int SaveChanges()
        {

            return db.SaveChanges();

        }
        }
    }

