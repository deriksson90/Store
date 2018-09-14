using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        conDb storeDb = new conDb();

        // GET: Store
        public ActionResult Index()
        {


            var category = storeDb.Categories.ToList();

            return View(category);
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = storeDb.Categories.ToList();
            return PartialView(categories);

        }

        public ActionResult Browse(string category)
        {
            var categoryModel = storeDb.Categories.Include("Items").
                Single(c=>c.CategoryName==category);

            return View(categoryModel);
        }

        public ActionResult Details(int id)
        {
            var item = storeDb.Items.Find(id);


            return View(item);
        }
    }
}