using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer3.Models
{   
	public  class CUSViewRepository : EFRepository<CUSView>, ICUSViewRepository
	{

	}

	public  interface ICUSViewRepository : IRepository<CUSView>
	{

	}
}