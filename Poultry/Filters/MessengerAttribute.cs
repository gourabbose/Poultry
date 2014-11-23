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
                var data = controller.TempData["Messege"];
                var type = controller.TempData["MessegeType"];
                controller.ViewBag.Messege = data==null?null:data.ToString();
                controller.ViewBag.MessegeType = type==null?null:type.ToString();
            }
            catch { }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}