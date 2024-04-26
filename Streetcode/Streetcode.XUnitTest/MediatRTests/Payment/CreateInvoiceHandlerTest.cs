using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Payment;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.BLL.MediatR.Payment;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class CreateInvoiceHandlerTest
{
    private readonly Mock<IPaymentService> _mockRepository;

    public CreateInvoiceHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetPaymentMock();
    }

    [Fact]
    public async Task CreateI_InvoiceDTOIsNotNull_IsSuccessShouldBeTrue()
    {
        // Arrange

        var paymentDto = new PaymentDto
        {
            Amount = 100,
            RedirectUrl = "https://example.com"
        };

        var request = new CreateInvoiceCommand(paymentDto);
        var handler = new CreateInvoiceHandler(_mockRepository.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}