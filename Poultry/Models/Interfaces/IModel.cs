using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.Interfaces
{
    public interface IModel
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime LastModifiedOn { get; set; }
        bool IsDeleted { get; set; }
    }
}