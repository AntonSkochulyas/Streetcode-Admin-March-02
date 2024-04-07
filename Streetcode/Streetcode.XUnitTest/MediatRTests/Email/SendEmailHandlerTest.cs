using System;
using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Email;
using Streetcode.BLL.Interfaces.Email;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Email;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class SendEmailHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IEmailService> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public SendEmailHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetEmailMock(true);

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task Handle_SendEmail_Success()
    {
        // Arrange
        var handler = new SendEmailHandler(_mockRepository.Object, _mockLogger.Object);
        var emailDto = new EmailDto
        {
            From = "sender@example.com",
            Content = "Test content"
        };
        var sendEmailCommand = new SendEmailCommand(emailDto);

        // Setup mock behavior
        _mockRepository.Setup(x => x.SendEmailAsync(It.IsAny<Streetcode.DAL.Entities.AdditionalContent.Email.Message>())).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(sendEmailCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_SendEmail_Failed()
    {
        // Arrange
        var handler = new SendEmailHandler(_mockRepository.Object, _mockLogger.Object);
        var emailDto = new EmailDto
        {
            From = "sender@example.com",
            Content = "Test content"
        };
        var sendEmailCommand = new SendEmailCommand(emailDto);

        // Setup mock behavior
        _mockRepository.Setup(x => x.SendEmailAsync(It.IsAny<Streetcode.DAL.Entities.AdditionalContent.Email.Message>())).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(sendEmailCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }
}