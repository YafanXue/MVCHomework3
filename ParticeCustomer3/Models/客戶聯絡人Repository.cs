using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.IO;
using ParticeCustomer.Models;

namespace ParticeCustomer3.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.已刪除 == false).Include(p => p.客戶資料);
        }

        public Stream GenerateDataTable(IEnumerable<客戶聯絡人> data)
        {
            // var data = RepositoryHelper.GetCustsExcelViewRepository();
            var query = data.Select(p => new { p.Id, p.姓名, p.手機, p.職稱, p.Email, p.電話, p.客戶資料.客戶名稱 });
            return NPOIExcel.RenderListToExcel(query.ToList());
        }

        public IQueryable<客戶聯絡人> Search(string Title,string sortcolumn)
        {

            var data = this.All();
            if (!string.IsNullOrEmpty(Title))
            {
                data= this.All().Where(p => p.職稱 == Title);
            }
            
                switch (sortcolumn)
                {
                    case "title":
                        data = data.OrderBy(p => p.職稱);
                        break;
                    case "title_desc":
                        data = data.OrderByDescending(p => p.職稱);
                        break;
                    case "name":
                        data = data.OrderBy(p => p.姓名);
                        break;
                    case "name_desc":
                        data = data.OrderByDescending(p => p.姓名);
                        break;
                    case "email":
                        data = data.OrderBy(p => p.Email);
                        break;
                    case "email_desc":
                        data = data.OrderByDescending(p => p.Email);
                        break;
                    case "mobile":
                        data = data.OrderBy(p => p.手機);
                        break;
                    case "mobile_desc":
                        data = data.OrderByDescending(p => p.手機);
                        break;
                    case "tel":
                        data = data.OrderBy(p => p.電話);
                        break;
                    case "tel_desc":
                        data = data.OrderByDescending(p => p.電話);
                        break;
                    case "cusname":
                        data = data.OrderBy(p => p.客戶資料.客戶名稱);
                        break;
                    case "cusname_desc":
                        data = data.OrderByDescending(p => p.客戶資料.客戶名稱);
                        break;
                default:
                    data = data.OrderBy(p => p.Id);
                    break;
            }
            
            return data;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public 客戶聯絡人 FindAllById(int id)
        {
            return base.All().FirstOrDefault(p => p.Id == id);
        }

        public SelectList GetCustomer()
        {
            var data = RepositoryHelper.Get客戶資料Repository().All();
            return new SelectList(data, "Id", "客戶名稱");
        }
        public SelectList GetCustomer(客戶聯絡人 entity)
        {
            var data=RepositoryHelper.Get客戶資料Repository().All();
            return new SelectList(data, "Id", "客戶名稱", entity.客戶Id);
        }

        public SelectList GetContactTitleList()
        {
            var data = this.All();
            var tmp = (from p in data select p.職稱).Distinct();
            return new SelectList(tmp, "職稱");
        }

        public override void Delete(客戶聯絡人 entity)
        {
            //base.Delete(entity);
            entity.已刪除 = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}