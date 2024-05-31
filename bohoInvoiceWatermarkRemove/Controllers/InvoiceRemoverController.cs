using Microsoft.AspNetCore.Mvc;
using bohoInvoiceWatermarkRemove.Model;
using bohoInvoiceWatermarkRemove.Service;

namespace bohoInvoiceWatermarkRemove.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {

        private readonly ILogger<InvoiceController> _logger;

        private readonly IWaterMarkRemoverService _waterMarkRemoverService;



        public InvoiceController(ILogger<InvoiceController> logger, IWaterMarkRemoverService waterMarkRemoverService)

        {
            _logger = logger;
            _waterMarkRemoverService = waterMarkRemoverService;
        }
        

        [HttpPost(Name = "removeWaterMark")]
        [Consumes("multipart/form-data")]
        public IActionResult RemoveWaterMark([FromForm] PDFFileForm formData)
        {
            // Check if the request contains multipart/form-data.
            if (formData.File == null || formData.File.Length == 0)
            {
                return new OkResult();
            }

            if (formData.File.Length > 0)
            {
                IFormFile formFile = formData.File;

                if (formFile != null)
                {

                    try
                    {
                        Stream pdfForm = formFile.OpenReadStream();

                        // Create a memory stream and copy the file data into it

                        // Create the response with the copied stream
  
                        Stream resp = _waterMarkRemoverService.RemoveWaterMark(pdfForm);
                        
                        // Optionally, return a response indicating success
                        return File(resp, "application/pdf", formFile.FileName);



                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex, "Failed to remove watermark");
                        return new BadRequestResult();
                    }
                }
            }

            return new BadRequestResult();

        }
    }
}