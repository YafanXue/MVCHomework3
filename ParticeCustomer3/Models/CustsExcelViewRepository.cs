using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer3.Models
{   
	public  class CustsExcelViewRepository : EFRepository<CustsExcelView>, ICustsExcelViewRepository
	{
        public override IQueryable<CustsExcelView> All()
        {
            var data = from cust in base.All()
                       select cust;
            return data;
        }
    }

	public  interface ICustsExcelViewRepository : IRepository<CustsExcelView>
	{

	}
}