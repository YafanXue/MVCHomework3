using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using ParticeCustomer.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Security;

namespace ParticeCustomer3.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            //var 客戶資料 = db.客戶資料.Include(客 => 客.客戶分類資訊);
            return base.All().Where(p=>p.已刪除== false).Include(p=>p.客戶分類資訊);
        }

        public IQueryable<客戶資料> Search(string Keyword,int 客戶分類,string sortcolumn)
        {
            var data = this.All();
            if (string.IsNullOrEmpty(Keyword))
            {
                if (客戶分類 != 0)
                    data= data.Where(p => p.客戶分類 == 客戶分類 );
            }
            else
            {
                if (客戶分類 == 0)
                    data= this.All().Where(p => p.客戶名稱.Contains(Keyword));
                else
                    data= this.All().Where(p => p.客戶名稱.Contains(Keyword) && p.客戶分類 == 客戶分類);
            }
            switch (sortcolumn)
            {
                case "name":
                    data = data.OrderBy(p => p.客戶名稱);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(p => p.客戶名稱);
                    break;
                case "serial":
                    data = data.OrderBy(p => p.統一編號);
                    break;
                case "serial_desc":
                    data = data.OrderByDescending(p => p.統一編號);
                    break;
                case "tel":
                    data = data.OrderBy(p => p.電話);
                    break;
                case "tel_desc":
                    data = data.OrderByDescending(p => p.電話);
                    break;
                case "fax":
                    data = data.OrderBy(p => p.傳真);
                    break;
                case "fax_desc":
                    data = data.OrderByDescending(p => p.傳真);
                    break;
                case "address":
                    data = data.OrderBy(p => p.地址);
                    break;
                case "address_desc":
                    data = data.OrderByDescending(p => p.地址);
                    break;
                case "email":
                    data = data.OrderBy(p => p.Email);
                    break;
                case "email_desc":
                    data = data.OrderByDescending(p => p.Email);
                    break;
                case "custype":
                    data = data.OrderBy(p => p.客戶分類);
                    break;
                case "custype_desc":
                    data = data.OrderByDescending(p => p.客戶分類);
                    break;
                default:
                    data = data.OrderBy(p => p.Id);
                    break;
            }
            return data;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public 客戶資料 FindByAccount(string ACCOUNT,int Id=0)
        {
            var data = this.All().FirstOrDefault(p => p.ACCOUNT == ACCOUNT);
            if (Id != 0)
                data= this.All().FirstOrDefault(p => p.ACCOUNT == ACCOUNT && p.Id != Id);
            return data;
        }

        public EditProfileViewModel GetCustomerProfile(string ACCOUNT)
        {
            var CUSDATA = this.All().FirstOrDefault(p => p.ACCOUNT == ACCOUNT);

            return new EditProfileViewModel { Id = CUSDATA.Id, Email = CUSDATA.Email, 電話 = CUSDATA.電話, 傳真 = CUSDATA.傳真, 地址 = CUSDATA.地址, PASSWORD = CUSDATA.PASSWORD };
        
        }

        public void EditProfile(客戶資料 entity, EditProfileViewModel data)
        {
            entity.PASSWORD = data.PASSWORD;
            entity.Email = data.Email;
            entity.電話 = data.電話;
            entity.地址 = data.地址;
            entity.傳真 = data.傳真;
        }

        public override void Delete(客戶資料 entity)
        {
            entity.已刪除 = true;
        }

        public override void Add(客戶資料 entity)
        {
            string strpassword = GenHashPwd(entity.PASSWORD);
            entity.PASSWORD = strpassword;
            base.Add(entity);
        }

        public string GenHashPwd(string Password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
        }

        public SelectList GetCusType()
        {
            var data = RepositoryHelper.Get客戶分類資訊Repository().All();
            return new SelectList(data, "Id", "分類名稱");
        }
        public SelectList GetCusType(客戶資料 entity)
        {
            var data = RepositoryHelper.Get客戶分類資訊Repository().All();
            return new SelectList(data, "Id", "分類名稱", entity.客戶分類);
        }

        public Stream GenerateDataTable()
        {
            var data = RepositoryHelper.GetCustsExcelViewRepository();

            return NPOIExcel.RenderListToExcel(data.All().ToList());
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}