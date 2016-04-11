using System;
using System.Web.Mvc;

namespace ParticeCustomer3.Controllers
{
    internal class CalculateActionSpendTimesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtStart = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("OnActionExecuting");
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.dtEnd = DateTime.Now;
            var dtTimespan = (DateTime)filterContext.Controller.ViewBag.dtEnd -
                (DateTime)filterContext.Controller.ViewBag.dtStart;
            filterContext.Controller.ViewBag.dtTimespan = dtTimespan;
            System.Diagnostics.Debug.WriteLine(string.Format("OnActionExecuted:{0}", dtTimespan));
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtResultStart = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("OnResultExecuting");
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.dtResultEnd = DateTime.Now;
            var dtResultTimespan = (DateTime)filterContext.Controller.ViewBag.dtResultEnd -
                (DateTime)filterContext.Controller.ViewBag.dtResultStart;
            filterContext.Controller.ViewBag.dtResultTimespan = dtResultTimespan;
            System.Diagnostics.Debug.WriteLine(string.Format("OnResultExecuted:{0}", dtResultTimespan));
            base.OnResultExecuted(filterContext);
        }
    }
}