using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParticeCustomer3.Models
{
    public class CustSearchViewModel
    {
        public string Keyword { get; set; }
        public SelectList CusType { get; set; }
        public string sortcolumn { get; set; }
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }
        public string 電話 { get; set; }
        public string 傳真 { get; set; }
        public string 地址 { get; set; }
        public string Email { get; set; }
        public bool 已刪除 { get; set; }
        public int 客戶分類 { get; set; }
        public IEnumerable<客戶資料> Customers { get; set; }

        public IPagedList<客戶資料> SearchDetails { get; set; }
    }
}