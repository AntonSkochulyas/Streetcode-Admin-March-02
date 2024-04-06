namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetPartnersRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var createdPartner = new Partner()
        {
            Id = 1,
            IsKeyPartner = true,
            IsVisibleEverywhere = true,
            Title = "Created title",
            Description = "Created description",
            TargetUrl = "Created target url",
            LogoId = 1,
            UrlTitle = "Created url title",
        };

        var partners = new List<Partner>()
            {
                new Partner { Id = 1, Title = "First Title", LogoId = 1, IsKeyPartner = true, IsVisibleEverywhere = true, TargetUrl = "First Url", UrlTitle = "First Url Title", Description = "First Description", Logo = null },
                new Partner { Id = 2, Title = "Second Title", LogoId = 2, IsKeyPartner = true, IsVisibleEverywhere = true, TargetUrl = "Second Url", UrlTitle = "Second Url Title", Description = "Second Description", Logo = null },
                new Partner { Id = 3, Title = "Third Title", LogoId = 3, IsKeyPartner = true, IsVisibleEverywhere = true, TargetUrl = "Third Url", UrlTitle = "Third Url Title", Description = "Third Description", Logo = null },
                new Partner { Id = 4, Title = "Fourth Title", LogoId = 4, IsKeyPartner = true, IsVisibleEverywhere = true, TargetUrl = "Fourth Url", UrlTitle = "Fourth Url Title", Description = "Fourth Description", Logo = null },
            };

        var streetCodes = new List<StreetcodeContent>
            {
                new StreetcodeContent() { Id = 1, Title = "First streetcode content title", AudioId = 1 },
                new StreetcodeContent() { Id = 2, Title = "Second streetcode content title", AudioId = 2 },
                new StreetcodeContent() { Id = 3, Title = "Third streetcode content title", AudioId = 3 },
                new StreetcodeContent() { Id = 4, Title = "Fourth streetcode content title", AudioId = 4 },
            };

        mockRepo.Setup(x => x.PartnersRepository.GetAllAsync(It.IsAny<Expression<Func<Partner, bool>>>(), It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
            .ReturnsAsync(partners);

        mockRepo.Setup(x => x.PartnersRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Partner, bool>>>(), It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
            .ReturnsAsync((Expression<Func<Partner, bool>> predicate, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> include) =>
            {
                return partners.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.PartnersRepository.GetSingleOrDefaultAsync(It.IsAny<Expression<Func<Partner, bool>>>(), It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
            .ReturnsAsync((Expression<Func<Partner, bool>> predicate, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> include) =>
            {
                return partners.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.StreetcodeRepository.GetSingleOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeContent>,
            IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync(
            (Expression<Func<StreetcodeContent, bool>> predicate,
            Func<IQueryable<Partner>, IIncludableQueryable<StreetcodeContent, object>> include) =>
            {
                return streetCodes.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.StreetcodeRepository.GetAllAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeContent>,
            IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync(streetCodes);

        mockRepo.Setup(x => x.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
            .ReturnsAsync(createdPartner);

        mockRepo.Setup(x => x.PartnersRepository.Create(It.IsAny<Partner>()))
            .Returns((Partner partner) =>
            {
                partners.Add(partner);
                return partner;
            });

        mockRepo.Setup(x => x.PartnersRepository.Delete(It.IsAny<Partner>()))
            .Callback((Partner partner) =>
            {
                partner = partners.FirstOrDefault(x => x.Id == partner.Id);
                if(partner != null)
                {
                    partners.Remove(partner);
                }
            });

        return mockRepo;
    }
}
