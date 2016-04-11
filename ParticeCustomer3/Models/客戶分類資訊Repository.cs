using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer3.Models
{   
	public  class 客戶分類資訊Repository : EFRepository<客戶分類資訊>, I客戶分類資訊Repository
	{

	}

	public  interface I客戶分類資訊Repository : IRepository<客戶分類資訊>
	{

	}
}