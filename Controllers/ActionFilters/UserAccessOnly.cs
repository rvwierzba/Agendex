using Agendex.Data;
using Agendex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Agendex.Controllers.ActionFilters
{
    public class UserAccessOnly : ActionFilterAttribute, IActionFilter
    {
        private DAL _dal = new DAL();
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values.ContainsKey("id"))
            {
                int id = int.Parse((String)context.RouteData.Values["id"]);
                if(context.HttpContext.User != null)
                {
                    var userName = context.HttpContext.User.Identity.Name;
                    if(userName != null)
                    {
                        var myEvent = _dal.GetEvent(id);
                        if(myEvent.User != null)
                        {
                            if (myEvent.User.UserName != userName)
                            {
                                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "NotFound" }));
                            }
                        }
                                      
                    }
                }
            }
        }

    }
}
