// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Toponyms;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Toponyms.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all toponyms from database.
    /// </summary>
    public record GetAllToponymsQuery(GetAllToponymsRequestDto request)
        : IRequest<Result<GetAllToponymsResponseDto>>;
}