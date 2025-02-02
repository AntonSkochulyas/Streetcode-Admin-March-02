﻿using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

namespace Streetcode.BLL.MediatR.Fact.Update;

public record UpdateFactCommand(FactDto Fact)
    : IRequest<Result<FactDto>>;
