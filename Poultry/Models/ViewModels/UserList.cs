using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Poultry.Models.ViewModels
{
    public class UserList
    {
        public string UserId { get; set; }
        public MembershipUser Membership { get; set; }
    }
}