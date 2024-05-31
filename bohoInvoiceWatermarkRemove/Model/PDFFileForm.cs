using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace bohoInvoiceWatermarkRemove.Model
{
    public class PDFFileForm
    {

        [Required(ErrorMessage = "Please upload pdf file")]
        [JsonProperty("file")]
        //[CustomMaxFileSize(AppSettings.FILE_SIZE)]
        //[IFormFileValidation(AppSettings.PDF_EXT)]
        public IFormFile File { get; set; }

        public PDFFileForm(IFormFile file)
        {
            File = file;
        }
    }
}
