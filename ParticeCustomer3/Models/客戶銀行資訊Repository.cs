using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using ParticeCustomer.Models;

namespace ParticeCustomer3.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p=>p.已刪除== false);
        }

        public IQueryable<客戶銀行資訊> Search(int 客戶Id,string sortcolumn)
        {
            var data = this.All();
            if (客戶Id > 0)
                data = data.Where(p => p.客戶Id == 客戶Id);
            switch (sortcolumn)
            {
                case "bkname":
                    data = data.OrderBy(p => p.銀行名稱);
                    break;
                case "bkname_desc":
                    data = data.OrderByDescending(p => p.銀行名稱);
                    break;
                case "bkid":
                    data = data.OrderBy(p => p.銀行代碼);
                    break;
                case "bkid_desc":
                    data = data.OrderByDescending(p => p.銀行代碼);
                    break;
                case "branch":
                    data = data.OrderBy(p => p.分行代碼);
                    break;
                case "branch_desc":
                    data = data.OrderByDescending(p => p.分行代碼);
                    break;
                case "accname":
                    data = data.OrderBy(p => p.帳戶名稱);
                    break;
                case "accname_desc":
                    data = data.OrderByDescending(p => p.帳戶名稱);
                    break;
                case "account":
                    data = data.OrderBy(p => p.帳戶號碼);
                    break;
                case "account_desc":
                    data = data.OrderByDescending(p => p.帳戶號碼);
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

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public SelectList GetCustomer()
        {
            var data = RepositoryHelper.Get客戶資料Repository().All();
            return new SelectList(data, "Id", "客戶名稱");
        }
        public SelectList GetCustomer(int cusid)
        {
            var data = RepositoryHelper.Get客戶資料Repository().All();
            return new SelectList(data, "Id", "客戶名稱", cusid);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.已刪除 = true;
        }

        public Stream GenerateDataTable(IEnumerable<客戶銀行資訊> data)
        {
            var query = data.Select(x => new
            {
                Id = x.Id,
                銀行代碼=x.銀行代碼,
                銀行名稱=x.銀行名稱,
                分行代碼=x.分行代碼.HasValue?x.分行代碼.Value:0,
                帳戶名稱=x.帳戶名稱,
                帳戶號碼=x.帳戶號碼,
                客戶名稱=x.客戶資料.客戶名稱
                
            });
            return NPOIExcel.RenderListToExcel(query.ToList());
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}