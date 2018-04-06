using System.Collections.Generic;

namespace PruebaUnoAgioGlobal.Web.Exceptions.Entities
{
    public class BadRequestExceptionInfo
    {
        public string Code { get; set; }
        public List<string> Reasons { get; set; }
    }
}
