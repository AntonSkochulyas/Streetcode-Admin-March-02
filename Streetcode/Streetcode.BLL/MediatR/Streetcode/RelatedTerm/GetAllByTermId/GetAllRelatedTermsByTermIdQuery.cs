﻿using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.GetAllByTermId
{
    public record GetAllRelatedTermsByTermIdQuery(int Id)
        : IRequest<Result<IEnumerable<RelatedTermDto>>>
    {
    }
}
