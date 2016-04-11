namespace ParticeCustomer3.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static BanksExcelViewRepository GetBanksExcelViewRepository()
		{
			var repository = new BanksExcelViewRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static BanksExcelViewRepository GetBanksExcelViewRepository(IUnitOfWork unitOfWork)
		{
			var repository = new BanksExcelViewRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ContactExcelViewRepository GetContactExcelViewRepository()
		{
			var repository = new ContactExcelViewRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ContactExcelViewRepository GetContactExcelViewRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ContactExcelViewRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CustsExcelViewRepository GetCustsExcelViewRepository()
		{
			var repository = new CustsExcelViewRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CustsExcelViewRepository GetCustsExcelViewRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CustsExcelViewRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CUSViewRepository GetCUSViewRepository()
		{
			var repository = new CUSViewRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CUSViewRepository GetCUSViewRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CUSViewRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶分類資訊Repository Get客戶分類資訊Repository()
		{
			var repository = new 客戶分類資訊Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶分類資訊Repository Get客戶分類資訊Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶分類資訊Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶資料Repository Get客戶資料Repository()
		{
			var repository = new 客戶資料Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶資料Repository Get客戶資料Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶資料Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶銀行資訊Repository Get客戶銀行資訊Repository()
		{
			var repository = new 客戶銀行資訊Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶銀行資訊Repository Get客戶銀行資訊Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶銀行資訊Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶聯絡人Repository Get客戶聯絡人Repository()
		{
			var repository = new 客戶聯絡人Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶聯絡人Repository Get客戶聯絡人Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶聯絡人Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}