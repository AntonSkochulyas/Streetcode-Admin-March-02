namespace Streetcode.XUnitTest.Mocks;

using Moq;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.DAL.Entities.Payment;

internal partial class RepositoryMocker
{
    public static Mock<IPaymentService> GetPaymentMock()
    {
        var merchantPaymentInfo = new MerchantPaymentInfo
        {
            Destination = "Destination 1"
        };

        var invoices = new List<Invoice>()
            {
                new Invoice(10000, 840, merchantPaymentInfo, "https://example.com/redirect1"),
                new Invoice(10000, 840, merchantPaymentInfo, "https://example.com/redirect2"),
                new Invoice(10000, 840, merchantPaymentInfo, "https://example.com/redirect3"),
            };

        var mockService = new Mock<IPaymentService>();

        mockService.Setup(x => x.CreateInvoiceAsync(It.IsAny<Invoice>()))
           .ReturnsAsync(new InvoiceInfo("invoiceId", "pageUrl"));

        return mockService;
    }
}
