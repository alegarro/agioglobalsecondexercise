using Microsoft.AspNetCore.Http;
using PruebaUnoAgioGlobal.Web.ApplicationServices.Interfaces;
using PruebaUnoAgioGlobal.Web.ModelsViews;
using System.IO;
using System.Threading.Tasks;

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
        /// <param name="file">File uploaded by the user.</param>
        /// <returns>Text included in the file.</returns>
        public async Task<FileUploadedViewModel> ReadTextFile(IFormFile file)        
        {
            var textFileContent = string.Empty;

            if (file != null)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    textFileContent = reader.ReadToEnd();
                }                
            }
            else
            {
                textFileContent = "A valid file was not uploaded.";
            }

            var modelFileUploaded = new FileUploadedViewModel()
            {
                TextFileReaded = textFileContent
            };

            return await Task.FromResult(modelFileUploaded);
        }
      
    }
}
