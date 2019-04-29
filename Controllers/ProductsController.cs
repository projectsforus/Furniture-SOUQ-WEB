using System;
using LAQY.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using System.Web;
using System.IO;
using System.Data.Entity;

namespace LAQY.Controllers
{
    public class ProductsController : BaseController
    {
        LaqyEntities db = new  LaqyEntities();

        // GET: Products
        public ActionResult Index(int? PSearch,int? SSearch)
        {
            if (PSearch != null && SSearch!=null)
            {
                List<Product> products = db.Products.Where(p => (p.price >= PSearch || PSearch == null) && (p.price <= SSearch || SSearch == null)).OrderBy(e => e.id).ToList();
                return View("Index", products);
            }
            else
            {
                List<Product> products = db.Products.OrderBy(e => e.id).ToList();
                return View("Index", products);
            }
        }
        public ActionResult IndexO(int? PSearch, int? SSearch)
        {
            if (PSearch != null && SSearch != null)
            {
                List<Offer> offers = db.Offers.Where(p => (p.dis >= PSearch || PSearch == null) && (p.dis <= SSearch || SSearch == null)).OrderBy(e => e.id).ToList();
                return View("IndexO", offers);
            }
            else
            {
                List<Offer> offers = db.Offers.OrderBy(e => e.id).ToList();
                return View("IndexO", offers);
            }
        }

        // GET: About Us
        public ActionResult AboutUs()
        {
            return View();
        }
        //GET : Contact Us
        public ActionResult ContactUs()
        {
            return View();
        }
        //Get : Location
        public ActionResult Location()
        {
            return View();
        }
        //Get : FeedBack
        public ActionResult FeedBack()
        {
            return View();
        }
        // GET: Products/Details/5
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
