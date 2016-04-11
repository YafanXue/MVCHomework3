using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer3.Models
{   
	public  class BanksExcelViewRepository : EFRepository<BanksExcelView>, IBanksExcelViewRepository
	{

	}

	public  interface IBanksExcelViewRepository : IRepository<BanksExcelView>
	{

	}
}