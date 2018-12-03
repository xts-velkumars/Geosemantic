using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Geosemantic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Geosemantic.Api.Extensions
{
    public static class UploadFileWithDataExtensions
    {
        public static async Task<KeyValuePair<IActionResult, UploadDataViewModel>>GetUploadedFileWithData(this HttpRequest request)
        {
            var result = await GetUploadedFiles(request);
            return new KeyValuePair<IActionResult, UploadDataViewModel>(result.Key, result.Value);
        }

        private static async Task<KeyValuePair<IActionResult, UploadDataViewModel>>GetUploadedFiles(this HttpRequest request)
        {
            try
            {
                var fileCount = request.Form.Files.Count;

                if (fileCount > 1)
                    return new KeyValuePair<IActionResult, UploadDataViewModel>(new BadRequestObjectResult("Multiple Files Not Allowed"), null);
                 
                var files = await GetFiles(request.Form);
                return new KeyValuePair<IActionResult, UploadDataViewModel>(new OkResult(), files);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<IActionResult, UploadDataViewModel>(new BadRequestObjectResult(ex.Message), null);
            }
        }

        private static async Task<UploadDataViewModel> GetFiles(IFormCollection formCollection)
        {
            var uploadDataViewModel = new UploadDataViewModel
            {
                FileViewModel = new FileViewModel(),
                FormData = new List<KeyValuePair<string, string>>()
            };

            foreach (var item in formCollection.Files)
            {
                if (item.ContentType != null)
                {
                    var data = new MemoryStream();
                    await item.CopyToAsync(data);
                    uploadDataViewModel.FileViewModel = new FileViewModel
                    {
                        Name = item.FileName,
                        Type = item.ContentType,
                        Length = item.Length,
                        Data = data.ToArray()
                    };
                }
            }
            foreach (var formDataKey in formCollection.Keys)
            {
                uploadDataViewModel.FormData.Add(new KeyValuePair<string, string>(formDataKey, formCollection[formDataKey].FirstOrDefault()));
            }

            return uploadDataViewModel;
        }
    }
}
