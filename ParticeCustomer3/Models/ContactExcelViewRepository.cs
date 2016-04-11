using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer3.Models
{   
	public  class ContactExcelViewRepository : EFRepository<ContactExcelView>, IContactExcelViewRepository
	{

	}

	public  interface IContactExcelViewRepository : IRepository<ContactExcelView>
	{

	}
}