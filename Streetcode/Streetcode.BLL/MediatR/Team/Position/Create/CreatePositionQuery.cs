﻿using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Team;

namespace Streetcode.BLL.MediatR.Team.Create
{
    public record CreatePositionQuery(PositionDto Position)
        : IRequest<Result<PositionDto>>;
}
