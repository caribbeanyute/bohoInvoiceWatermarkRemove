using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bohoInvoiceWatermarkRemove.Service;

namespace bohoInvoiceWatermarkRemove.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly IWaterMarkRemoverService _waterMarkRemoverService;

        public HomeController(ILogger<HomeController> logger, IWaterMarkRemoverService waterMarkRemoverService)

        {
            _logger = logger;
            _waterMarkRemoverService = waterMarkRemoverService;
        }


        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(IFormFile postedFile)

        {

            if (postedFile == null || postedFile.Length == 0) { 

                const string error = "No file selected for upload...";

                _logger.LogError(error);

                return BadRequest(error); 
            }


            if (postedFile != null)
            {

                try
                {
                    Stream pdfForm = postedFile.OpenReadStream();

                    // Create a memory stream and copy the file data into it

                    // Create the response with the copied stream

                    Stream resp = _waterMarkRemoverService.RemoveWaterMark(pdfForm);

                    // Optionally, return a response indicating success
                    return File(resp, "application/pdf", postedFile.FileName);



                }
                catch (Exception ex)
                {
                   _logger.LogError(ex, "Failed to remove watermark");
                    return new BadRequestResult();
                }
            }



            return View();

        }
    }
}
