using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParticeCustomer3.Models;
using System.Web.Security;
using PagedList;

namespace ParticeCustomer3.Controllers
{
    [Authorize(Roles = "sysadmin")]
    public class CustomerController : BaseController
    {

        // GET: Customer
        public ActionResult Index(CustSearchViewModel CusSearch, string sortcolumn, string nameFilter,int? custypeFilter, int? page)
        {
            //var 客戶資料 = db.客戶資料.Include(客 => 客.客戶分類資訊);

            //return View(repoCustomer.All());
            ViewBag.CurrentSort = CusSearch.sortcolumn;
            ViewBag.NameSortParm = CusSearch.sortcolumn == "name" ? "name_desc" : "name"; //名稱
            ViewBag.SerialSortParm = CusSearch.sortcolumn == "serial" ? "serial_desc" : "serial"; //統一編號
            ViewBag.TELSortParm = CusSearch.sortcolumn == "tel" ? "tel_desc" : "tel";
            ViewBag.FAXSortParm = CusSearch.sortcolumn == "fax" ? "fax_desc" : "fax";
            ViewBag.AddressSortParm = CusSearch.sortcolumn == "address" ? "address_desc" : "address";
            ViewBag.EmailSortParm = CusSearch.sortcolumn == "email" ? "email_desc" : "email";           
            ViewBag.CusTypeSortParm = CusSearch.sortcolumn == "custype" ? "custype_desc" : "custype";
            ViewBag.CusType = repoCustomer.GetCusType();
            if (CusSearch.Keyword != null)
            {
                page = 1;
            }
            else
            {
                CusSearch.Keyword = nameFilter;
            }
            ViewBag.nameFilter = CusSearch.Keyword;

            if (CusSearch.客戶分類 != 0)
            {
                page = 1;
            }
            else
            {
                CusSearch.客戶分類 = custypeFilter.HasValue ? custypeFilter.Value :0;
            }
            ViewBag.custypeFilter = CusSearch.客戶分類;
            CusSearch.Customers = repoCustomer.Search(CusSearch.Keyword, CusSearch.客戶分類,CusSearch.sortcolumn);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            CusSearch.SearchDetails = CusSearch.Customers.ToPagedList(pageNumber, pageSize);
            return View(CusSearch);
        }

       [AllowAnonymous]
        public ActionResult GenerateError()
        {
            throw new InvalidOperationException("操作錯誤");
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 data =repoCustomer.Find(id.Value);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            //ViewBag.客戶分類 = new SelectList(repoCustType.All(), "Id", "分類名稱");
            ViewBag.客戶分類 = repoCustomer.GetCusType();
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,ACCOUNT,PASSWORD")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repoCustomer.Add(客戶資料);
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            //ViewBag.客戶分類 = new SelectList(repoCustType.All(), "Id", "分類名稱", 客戶資料.客戶分類);
            ViewBag.客戶分類 = repoCustomer.GetCusType(客戶資料);
            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repoCustomer.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.客戶分類 = new SelectList(repoCustType.All(), "Id", "分類名稱", 客戶資料.客戶分類);
            ViewBag.客戶分類 =repoCustomer.GetCusType(客戶資料);
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,已刪除,客戶分類")] 客戶資料 客戶資料)
        public ActionResult Edit(int id,FormCollection form)
        {
            var 客戶資料 = repoCustomer.Find(id);
            if (TryUpdateModel(客戶資料,new string[] {"Id","客戶名稱","統一編號","電話","傳真","地址","Email", "客戶分類","ACCOUNT","PASSWORD" }))
            {
                string hashpwd= repoCustomer.GenHashPwd(客戶資料.PASSWORD);
                客戶資料.PASSWORD = hashpwd;
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶分類 = repoCustomer.GetCusType(客戶資料);
            return View(客戶資料);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repoCustomer.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 =repoCustomer.Find(id);
            repoCustomer.Delete(客戶資料);
            repoCustomer.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //public ActionResult GenJsonResult()
        //{
        //    return View();
        //}
        [AllowAnonymous]
        public ActionResult GenJsonResult(string contactid)
        {
            ViewData["contactid"] = contactid;
            if (contactid == null)
            {
                return View();// Json(emptyobj, JsonRequestBehavior.AllowGet);
            }
            repoCustomer.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            repoCustType.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            repoContact.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            repoBank.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            int strid = int.Parse(contactid);

            客戶資料Entities db = new 客戶資料Entities();
            db.Configuration.LazyLoadingEnabled = false;
            var data=db.客戶聯絡人.FirstOrDefault(p => p.Id == strid);
            //var data = repoContact.Find(strid);
            if (data == null)
            {
                var emptyobj = new { id = contactid, description = "不存在的客戶聯絡人" };
                return Json(emptyobj, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExcelExport()
        {
            return File(repoCustomer.GenerateDataTable(), "application/vnd.ms-excel", "customers.xls");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoCustomer.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
