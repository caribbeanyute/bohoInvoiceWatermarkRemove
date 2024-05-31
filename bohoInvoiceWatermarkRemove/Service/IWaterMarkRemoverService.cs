namespace bohoInvoiceWatermarkRemove.Service
{
    public interface IWaterMarkRemoverService
    {
        MemoryStream RemoveWaterMark(Stream stream);
    }
}
