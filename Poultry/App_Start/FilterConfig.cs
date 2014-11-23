using Poultry.Filters;
using System.Web;
using System.Web.Mvc;

namespace Poultry
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MessengerAttribute());
            filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new NoCacheAttribute());
        }
    }
}