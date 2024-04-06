using System;
using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Payment;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.BLL.Mapping.Payment;
using Streetcode.BLL.MediatR.Payment;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class CreateInvoiceHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IPaymentService> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public CreateInvoiceHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetPaymentMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<Streetcode.BLL.Mapping.Payment.PaymentProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
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
        var handler = new CreateInvoiceHandler(_mockRepository.Object, _mockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}