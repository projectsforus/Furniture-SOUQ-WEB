using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LAQY.Models;
using System.IO;

namespace LAQY.Controllers
{
    public class AProductController : BaseController
    {
        private LaqyEntities db = new LaqyEntities();

        // GET: AProduct
        public ActionResult Index()
        {
            if(Session["Aname"]!=null)
            {
                return View(db.Products.OrderBy(p=>p.id).ToList());
            }
            else
            {
                Redirect("/Users/Sign");
            }
            return View();
        }
        // GET: AProduct
        public ActionResult IndexO()
        {
            if (Session["Aname"] != null)
            {
                return View(db.Offers.OrderBy(p => p.id).ToList());
            }
            else
            {
                Redirect("/Users/Sign");
            }
            return View();
        }

        // GET: AProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: AProduct/Details/5
        public ActionResult DetailsO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: AProduct/Create
        public ActionResult Create()
        {
            if (Session["Aname"] == null)
            {
                return RedirectToAction("Sign", "Users");
            }
            else
            {
                return View();
            }
        }

        // POST: AProduct/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,title,price,details,img,date")] Product product, HttpPostedFileBase Ufile)
        {
            if (ModelState.IsValid)
            {
                if (Ufile != null)
                {
                    Random m = new Random();
                    int n = m.Next(0, 20);
                    string filename = Path.GetFileName(Ufile.FileName);
                    Ufile.SaveAs(Server.MapPath("~/Image/" + n + "" + filename));
                    product.img = "~/Image/" + n + "" + filename;
                    product.date = DateTime.Now.Date.ToShortDateString();
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", "AProduct");
                }
            }
            return View(product);
        }
        // GET: AProduct/Create
        public ActionResult CreateO()
        {
            if (Session["Aname"] == null)
            {
                return RedirectToAction("Sign", "Users");
            }
            else
            {
                return View();
            }
        }

        // POST: AProduct/CreateO
        [HttpPost]
        public ActionResult CreateO([Bind(Include = "id,title,price,details,img,date,dis")] Offer offer, HttpPostedFileBase Ufile)
        {
            if (ModelState.IsValid)
            {
                if (Ufile != null)
                {
                    Random m = new Random();
                    int n = m.Next(0, 20);
                    string filename = Path.GetFileName(Ufile.FileName);
                    Ufile.SaveAs(Server.MapPath("~/Image/" + n + "" + filename));
                    offer.img = "~/Image/" + n + "" + filename;
                    offer.date = DateTime.Now.Date.ToShortDateString();
                    db.Offers.Add(offer);
                    db.SaveChanges();
                    return RedirectToAction("IndexO", "AProduct");
                }
            }
            return View(offer);
        }

        // GET: AProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AProduct/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,title,price,details,img,date")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // GET: AProduct/Edit/5
        public ActionResult EditO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: AProduct/Edit/5
        [HttpPost]
        public ActionResult EditO([Bind(Include = "id,title,price,details,img,date,dis")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexO");
            }
            return View(offer);
        }

        // GET: AProduct/Delete/5")
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            var photoName = product.img;
            if (System.IO.File.Exists(Request.MapPath(photoName)))
            {
                System.IO.File.Delete(Request.MapPath(photoName));
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: AProduct/Delete/5")]
        public ActionResult DeleteO(int? id)
        {
            Offer offer = db.Offers.Find(id);
            var photoName = offer.img;
            if (System.IO.File.Exists(Request.MapPath(photoName)))
            {
                System.IO.File.Delete(Request.MapPath(photoName));
            }
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("IndexO");
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
