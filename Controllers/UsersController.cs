using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LAQY.Models;

namespace LAQY.Controllers
{
    public class UsersController : BaseController
    {
        private LaqyEntities db = new LaqyEntities ();

        // GET: Sign
        public ActionResult Sign()
        {
            return View();
        }
        //Post : Cart
        public ActionResult Cart()
        {
            return View();
        }

        //Get : Cart
        public JsonResult GetAll()
        {
            if (Session["Uid"]!=null)
            {
                int id = (int)Session["Uid"];
                var cart = db.Carts.Where(e => e.user_id == id).ToList();
                return Json(cart, JsonRequestBehavior.AllowGet);
            }
            return Json("no", JsonRequestBehavior.AllowGet);
        }

        //Post : Cart
        [HttpPost]
        public JsonResult Cart(int?ID,string Ty,int? Qty)
        {
            if(ID != null)
            {
                if(Ty =="P")
                {
                    Product pro = db.Products.Find(ID);
                    Cart cart = new Cart();
                    cart.price = pro.price;
                    cart.date = DateTime.Now.Date.ToShortDateString();
                    cart.qty = Qty;
                    cart.title = pro.title;
                    cart.user_id = int.Parse(Session["Uid"].ToString());
                    cart.total = cart.qty * pro.price;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Offer pro = db.Offers.Find(ID);
                    Cart cart = new Cart();
                    cart.price = pro.dis;
                    cart.date = DateTime.Now.Date.ToShortDateString();
                    cart.qty = Qty;
                    cart.title = pro.title;
                    cart.user_id = int.Parse(Session["Uid"].ToString());
                    cart.total = cart.qty * pro.dis;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Not Found ID", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditCart(int?id , int? qty)
        {
            Cart c = db.Carts.Find(id);
            c.qty = qty;
            c.total = c.price * qty;
            db.Entry(c).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("no", JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult CheckEmail(string Email)
        {
            User _user = db.Users.Where(c => c.email == Email.Trim()).FirstOrDefault();

            if (_user == null)
            {
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("no", JsonRequestBehavior.AllowGet);
            }

        }
        // Post: Users/Login
        [HttpGet]
        public JsonResult Login(string email,string password)
        {
            if (db.Users.Any(e => e.email == email))
            {
                var myUser = db.Users.FirstOrDefault(e => e.email == email && e.password == password);
                if (myUser!=null)
                {
                    Session["Uname"] = myUser.name;
                    Session["Uid"] = myUser.id;
                    return Json("user", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("inpass", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if(email=="admin@admin.admin" && password=="admin")
                {
                    Session["Aname"] = "Admin";
                    return Json("admin", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("notreg", JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public JsonResult Register([Bind(Include = "id,name,address,phone,email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("no", JsonRequestBehavior.AllowGet);
        }

        //GET:Edit Profile
        public ActionResult UProfile()
        {
            if (Session["Uid"]!=null)
            {
                return View();
            }
            else
            {
                RedirectToAction("Sign");
            }
            return View();
        }

        public ActionResult GetProfile()
        {
            
            if (Session["Uid"]!=null)
            {
                int id = (int)Session["Uid"];
                User user = db.Users.Find(id);
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RedirectToAction("Sign");
            }
            return View();
        }
        //Post
        [HttpPost]
        public JsonResult UProfile([Bind(Include = "id,name,address,phone,email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("no", JsonRequestBehavior.AllowGet);
        }

        //GET: LogOut
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Products");
        }
        // GET: AProduct/Delete/5")
        [HttpPost]
        public JsonResult DeleteC(int? id)
        {
            if (id == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return Json("not", JsonRequestBehavior.AllowGet);
            }
            db.Carts.Remove(cart);
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete()
        {
            int id = int.Parse(Session["Uid"].ToString());
            var lst = db.Carts.Where(p => p.user_id == id);
            db.Carts.RemoveRange(lst);
            db.SaveChanges();
            return Json("Check Out Success",JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
