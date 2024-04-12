﻿using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete
{
    public record DeleteRelatedTermCommand(string Word)
        : IRequest<Result<RelatedTermDto>>;
}
