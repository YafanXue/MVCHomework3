using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParticeCustomer3.Models;
using PagedList;

namespace ParticeCustomer3.Controllers
{
    public class BankController : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        // GET: Bank
        public ActionResult Index(int? 客戶Id,string sortcolumn, int page = 1)
        {
            if (string.IsNullOrEmpty(sortcolumn))
            {
                sortcolumn = "bankid";
            }
            ViewBag.BkNameSortParm = sortcolumn == "bkname" ? "bkname_desc" : "bkname";
            ViewBag.BkIdSortParm = sortcolumn == "bkid" ? "bkid_desc" : "bkid";
            ViewBag.BranchSortParm = sortcolumn == "branch" ? "branch_desc" : "branch";
            ViewBag.AccNameSortParm = sortcolumn == "accname" ? "accname_desc" : "accname";
            ViewBag.AccountSortParm = sortcolumn == "account" ? "account_desc" : "account";
            ViewBag.CusSortParm = sortcolumn == "cusname" ? "cusname_desc" : "cusname";
            var cusid = 客戶Id.HasValue ? 客戶Id.Value : 0;
            var 客戶銀行資訊 = repoBank.Search(cusid, sortcolumn);
            int pageSize = 3;
            return View(客戶銀行資訊.ToPagedList(page, pageSize));
            //return View(客戶銀行資訊);
        }
        public ActionResult ExcelExport(int? 客戶Id,string sortcolumn, int page = 1)
        {
            var cusid = 客戶Id.HasValue ? 客戶Id.Value : 0;
            var data = repoBank.Search(cusid, sortcolumn);

            return File(repoBank.GenerateDataTable(data), "application/vnd.ms-excel", "bank.xls");
        }
        // GET: Bank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id =repoBank.GetCustomer();
            return View();
        }

        // POST: Bank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repoBank.Add(客戶銀行資訊);
                repoBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = repoBank.GetCustomer();
            return View(客戶銀行資訊);
        }

        // GET: Bank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id =repoBank.GetCustomer(客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: Bank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int  id, FormCollection form)
        {
            var data = repoBank.Find(id);
            if (TryUpdateModel(data,new string[] {"Id","客戶Id", "銀行名稱","銀行代碼","分行代碼","帳戶名稱","帳戶號碼"}))
            {
                repoBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = repoBank.GetCustomer();
            return View(data);
        }

        // GET: Bank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = repoBank.Find(id);
            repoBank.Delete(客戶銀行資訊);
            repoBank.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoBank.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
