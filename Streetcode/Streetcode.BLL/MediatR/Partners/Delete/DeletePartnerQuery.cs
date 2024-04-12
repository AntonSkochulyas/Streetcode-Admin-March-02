using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

namespace Streetcode.BLL.MediatR.Partners.Delete
{
    public record DeletePartnerQuery(int Id)
        : IRequest<Result<PartnerDto>>;
}
