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
using System.Data.Entity.Validation;

namespace ParticeCustomer3.Controllers
{
    [Authorize(Roles = "sysadmin")]
    public class ContactController : BaseController
    {
        // GET: Contact
        public ActionResult Index(string Title ,string sortcolumn,int page=1)
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            if (string.IsNullOrEmpty(sortcolumn))
            {
                sortcolumn = "contactid";
            }
            ViewBag.TitleSortParm = sortcolumn == "title" ? "title_desc" : "title";
            ViewBag.NameSortParm = sortcolumn == "name" ? "name_desc" : "name";
            ViewBag.EmailSortParm= sortcolumn == "email" ? "email_desc" : "email";
            ViewBag.MobileSortParm = sortcolumn == "mobile" ? "mobile_desc" : "mobile";
            ViewBag.TELSortParm = sortcolumn == "tel" ? "tel_desc" : "tel";
            ViewBag.CusName= sortcolumn == "cusname" ? "cusname_desc" : "cusname";
            ViewBag.TitleList = repoContact.GetContactTitleList();
            

            ViewBag.CurrentFilter = Title;
            var data = repoContact.Search(Title, sortcolumn);

            int pageSize = 3;
            return View(data.ToPagedList(page, pageSize));
            //return View(repoContact.Search(Title, sortcolumn));
        }

        public ActionResult ExcelExport(string Title, string sortcolumn, int page = 1)
        {
            var data = repoContact.Search(Title, sortcolumn);

            return File(repoContact.GenerateDataTable(data), "application/vnd.ms-excel", "bank.xls");
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = repoContact.GetCustomer();//new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repoContact.Add(客戶聯絡人);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id =repoContact.GetCustomer(客戶聯絡人);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 =repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = repoContact.GetCustomer(客戶聯絡人);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection form)
        {
            var data = repoContact.Find(id);
            if(TryUpdateModel(data, new string[] { "Id", "客戶Id", "職稱", "姓名", "Email", "手機", "電話" }))
            {
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = repoContact.GetCustomer(data);
            return View(data);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id);

            repoContact.Delete(客戶聯絡人);
            repoContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoContact.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
