using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class ControllerExtension
    {
        public static dynamic GetModelStateErrors(this ModelStateDictionary obj)
        {
            var allErrors = obj.Values.Where(m => (m.Errors.Count() > 0)).Select((m, index) => (new { Key = obj.Keys.ElementAt<String>(index), Messages = m.Errors.Select((error) => (error.ErrorMessage)) }));
            return allErrors;
        }
    }

    public sealed class AllowEverybodyAttribute : Attribute
    {
        public AllowEverybodyAttribute()
        {
            
        }
    }

}