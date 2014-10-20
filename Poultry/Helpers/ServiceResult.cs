using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Helpers
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string Messege { get; set; }
    }
}