using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticeCustomer3.Models
{
    public class ContactSearchViewModel
    {

        public string Title { get; set; }
        public string sortcolumn { get; set; }
        public string currentFilter { get; set; }
        public IQueryable<客戶聯絡人> Contacts { get; set; }
        public IPagedList SearchResults { get; set; }
    }
}