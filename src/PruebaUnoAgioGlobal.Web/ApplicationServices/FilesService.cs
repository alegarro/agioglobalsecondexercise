using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsViews;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static PruebaUnoAgioGlobal.Web.Enum.FileTypesEnums;
using static PruebaUnoAgioGlobal.Web.Enum.RolesEnums;

namespace PruebaUnoAgioGlobal.Web.ApplicationServices
{
    /// <summary>
    /// Files Application Service.
    /// </summary>
    public class FilesService : IFilesService
    {
        /// <summary>
        /// Reads the file bytes and output the tex of the file.
        /// </summary>
        /// <param name="fileModel">Model with data and the file uploaded by the user.</param>
        /// <returns>Text included in the file.</returns>
        public async Task<FileUploadedViewModel> ReadTextFile(FileUploadViewModel fileModel)
        {
            var textFileContent = string.Empty;

            if (fileModel.UserRole != (int)Roles.ADMINISTRATOR && !fileModel.AttachedFile.FileName.Contains(".ForUsersRoles."))
            {
                textFileContent = "User not allowed to read current files.";
            }
            else
            {
                if (fileModel.AttachedFile != null)
                {
                    using (var reader = new StreamReader(fileModel.AttachedFile.OpenReadStream()))
                    {
                        textFileContent = reader.ReadToEnd();
                    }
                }
                else
                {
                    textFileContent = "A valid file was not uploaded.";
                }

                // Decrypt if is necesary
                if (fileModel.FileType == (int)FileType.ENCRIPTED_TEXT_FILE || 
                    fileModel.FileType == (int)FileType.ENCRIPTED_XML_FILE ||
                    fileModel.FileType == (int)FileType.ENCRIPTED_JSON_FILE)
                {
                    textFileContent = string.Concat(textFileContent.Reverse()).Replace('#', 'a');
                }                
            }
            
            var modelFileUploaded = new FileUploadedViewModel()
            {
                TextFileReaded = textFileContent
            };

            return await Task.FromResult(modelFileUploaded);
        }

        /// <summary>
        /// Returns the different file types that support the application.
        /// </summary>
        /// <returns>List of SelectListItem with the different file types that support the aaplication.</returns>
        public List<SelectListItem> GetFileTypes()
        {
            var fileTypesDictionary = new List<SelectListItem>();

            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.TEXT_FILE).ToString(), Text = "Text File" });
            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.XML_FILE).ToString(), Text = "XML File" });
            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.JSON_FILE).ToString(), Text = "JSON File" });
            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.ENCRIPTED_TEXT_FILE).ToString(), Text = "Encripted Text File" });
            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.ENCRIPTED_XML_FILE).ToString(), Text = "Encripted XML File" });
            fileTypesDictionary.Add(new SelectListItem() { Value = ((int)FileType.ENCRIPTED_JSON_FILE).ToString(), Text = "Encripted JSON File" });

            return fileTypesDictionary;
        }

        /// <summary>
        /// Returns the different user roles that support the application.
        /// </summary>
        /// <returns>List of SelectListItem with the different user roles that support the aaplication.</returns>
        public List<SelectListItem> GetUserRoles()
        {
            var userRolesDictionary = new List<SelectListItem>();

            userRolesDictionary.Add(new SelectListItem() { Value = ((int)Roles.ADMINISTRATOR).ToString(), Text = "Administrator" });
            userRolesDictionary.Add(new SelectListItem() { Value = ((int)Roles.USER).ToString(), Text = "User" });            

            return userRolesDictionary;
        }
    }
}
