using ParticeCustomer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ParticeCustomer3.Controllers
{
    [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Error2")]
    [HandleError(ExceptionType = typeof(Exception), View = "Error2")]
    [CalculateActionSpendTimes]
    public class BaseController : Controller
    {
        protected 客戶資料Repository repoCustomer = RepositoryHelper.Get客戶資料Repository();
        protected 客戶銀行資訊Repository repoBank = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶聯絡人Repository repoContact = RepositoryHelper.Get客戶聯絡人Repository();
        protected 客戶分類資訊Repository repoCustType = RepositoryHelper.Get客戶分類資訊Repository();

        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);

        }

    }
}