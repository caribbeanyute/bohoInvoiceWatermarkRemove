using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.PdfCleanup;

namespace bohoInvoiceWatermarkRemove.Service
{
    public class WaterMarkRemoverService : IWaterMarkRemoverService
    {
        public MemoryStream RemoveWaterMark(Stream inputStream)
        {
            var outStream = new MemoryStream();

            var writer = new PdfWriter(outStream);

            PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputStream), writer);

            writer.SetCloseStream(false);


            int x1 = 0;
            int y1 = 0;
            int width = 720;
            int height = 49;

            int pages = pdfDoc.GetNumberOfPages();


            List<PdfCleanUpLocation> cleanUpLocations = new List<PdfCleanUpLocation>();
            //// red
            //iText.Kernel.Colors.DeviceRgb d = new iText.Kernel.Colors.DeviceRgb(245, 66, 66);
            ////white

            iText.Kernel.Colors.DeviceRgb d = new iText.Kernel.Colors.DeviceRgb(255, 255, 255);

            PdfCleanUpTool cleaner = new PdfCleanUpTool(pdfDoc);

            for (int i = 1; i <= pages; i++)
            {
                PdfCleanUpLocation pclean = new PdfCleanUpLocation(i, new Rectangle(x1, y1, width, height), d);
                cleaner.AddCleanupLocation(pclean);
            }



            cleaner.CleanUp();
            pdfDoc.Close();
            outStream.Position = 0;

            return outStream;
        }
    }
}
