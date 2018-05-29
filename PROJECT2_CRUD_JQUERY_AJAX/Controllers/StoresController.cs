using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJECT2_CRUD_JQUERY_AJAX.Models;

namespace PROJECT2_CRUD_JQUERY_AJAX.Controllers
{
    public class StoresController : Controller
    {
        private MVCEntities db = new MVCEntities();

        // GET: Stores
        public ActionResult Index()
        {
            return View("");
        }
        // Get Store list to display on the landing page
        public JsonResult GetStoreList()
        {

            List<StoreViewModel> storeModel = db.Stores.Select(x => new StoreViewModel
            {
                ID = x.ID,
                Name = x.Name,
                Address= x.Address
            }).ToList();
            return Json(storeModel, JsonRequestBehavior.AllowGet);
        }

        // Save data in the product database
        public JsonResult CreateStore(StoreViewModel storeViewModel)
        {

            Store store = new Store();
            store.Name = storeViewModel.Name;
            store.Address= storeViewModel.Address;
            db.Stores.Add(store);
            db.SaveChanges();
            return Json(storeViewModel, JsonRequestBehavior.AllowGet);
        }

        // Get Store for editing
        public JsonResult GetStoreRecord(int id)
        {
            var storeModel = db.Stores.Where(x => x.ID == id).Select(y => new StoreViewModel
            {
                ID = y.ID,
                Name = y.Name,
                Address = y.Address
            }).FirstOrDefault();

            return Json(storeModel, JsonRequestBehavior.AllowGet);
        }

        // Save the edited store in the database
        public JsonResult EditStoreRecord([Bind(Include = "ID,Name,Address")] StoreViewModel storeModel)
        {
            var Sto = db.Stores.Find(storeModel.ID);
            Sto.Name = storeModel.Name;
            Sto.Address = storeModel.Address;
            db.SaveChanges();
            return Json(storeModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {

            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return Json("Not found", JsonRequestBehavior.AllowGet);
            }
            StoreViewModel storeViewModel = new StoreViewModel
            {
                ID = store.ID,
                Name = store.Name,
                Address = store.Address,

            };
            var isExist = !store.ProductSolds.Any();
            return Json(new { storeViewModel = storeViewModel, isExist = isExist }, JsonRequestBehavior.AllowGet);
        }

        // POST: Store/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
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

