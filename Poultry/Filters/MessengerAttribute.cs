using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poultry.Filters
{
    public class MessengerAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.Controller as Controller;
            try
            {
                var data = controller.TempData["Messege"].ToString();
                controller.ViewBag.Messege = data;
            }
            catch { }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}