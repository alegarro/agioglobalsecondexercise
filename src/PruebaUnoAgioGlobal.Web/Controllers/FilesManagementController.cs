using Microsoft.AspNetCore.Mvc;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsViews;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.Controllers
{
    /// <summary>
    /// Controller for flights management views.
    /// </summary>
    public class FilesManagementController : Controller
    {
        private readonly IFilesService _filesService;

        /// <summary>
        /// Generates a new instance of the Files Management Controller.
        /// </summary>
        /// <param name="filesService">Files application service.</param>
        public FilesManagementController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        /// <summary>
        /// Loads the main files management view.
        /// </summary>
        /// <returns>Files management view.</returns>
        public IActionResult Index()
        {
            var fileUploadViewModel = new FileUploadViewModel()
            {
                FileTypes = _filesService.GetFileTypes(),
                UserRoles = _filesService.GetUserRoles(),
            };

            return View(fileUploadViewModel);
        }

        /// <summary>
        /// Uploads the files and show the file content in the loaded page.
        /// </summary>
        /// <returns>Upload file view.</returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadViewModel model)
        {
            var readedTextFile = await _filesService.ReadTextFile(model);

            return View(readedTextFile);
        }
    }
}
