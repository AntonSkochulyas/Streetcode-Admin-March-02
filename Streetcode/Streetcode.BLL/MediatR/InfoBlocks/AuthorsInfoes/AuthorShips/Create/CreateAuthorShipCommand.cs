﻿using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create
{
    public record CreateAuthorShipCommand(AuthorShipDto newAuthorShip) : IRequest<Result<AuthorShipDto>>;
}
