using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaUnoAgioGlobal.Web.ModelsViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces
{
    /// <summary>
    /// Interface for the files application service.
    /// </summary>
    public interface IFilesService
    {
        /// <summary>
        /// Reads the file bytes and output the tex of the file.
        /// </summary>
        /// <param name="fileModel">Model with data and the file uploaded by the user.</param>
        /// <returns>Text included in the file.</returns>
        Task<FileUploadedViewModel> ReadTextFile(FileUploadViewModel fileModel);

        /// <summary>
        /// Returns the different file types that support the application.
        /// </summary>
        /// <returns>List of SelectListItem with the different file types that support the aaplication.</returns>
        List<SelectListItem> GetFileTypes();

        /// <summary>
        /// Returns the different user roles that support the application.
        /// </summary>
        /// <returns>List of SelectListItem with the different user roles that support the aaplication.</returns>
        List<SelectListItem> GetUserRoles();
    }
}
