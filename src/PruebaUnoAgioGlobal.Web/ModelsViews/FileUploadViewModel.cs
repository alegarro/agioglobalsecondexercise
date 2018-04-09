using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PruebaUnoAgioGlobal.Web.ModelsViews
{
    public class FileUploadViewModel
    {
        public int FileType { get; set; }

        public List<SelectListItem> FileTypes { get; set; }

        public IFormFile AttachedFile { get; set; }        
    }
}
