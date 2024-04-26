// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all streetcodes from database.
    /// </summary>
    public record GetAllStreetcodesQuery(GetAllStreetcodesRequestDto Request)
        : IRequest<Result<GetAllStreetcodesResponseDto>>;
}