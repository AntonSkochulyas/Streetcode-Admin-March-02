namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Moq;
using Streetcode.BLL.Interfaces.Email;

internal partial class RepositoryMocker
{
    public static Mock<IEmailService> GetEmailMock(bool word)
    {
        var mockService = new Mock<IEmailService>();

        return mockService;
    }
}
