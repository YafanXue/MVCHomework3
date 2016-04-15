using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticeCustomer3.Models
{
    public class CusContactViewModel
    {
        public 客戶資料 Customer { get; set; }

        public IEnumerable<客戶聯絡人> Contacts { get; set; }
    }
}