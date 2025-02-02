﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.GetShortById
{
    public class GetStreetcodeShortByIdHandler : IRequestHandler<GetStreetcodeShortByIdQuery, Result<StreetcodeShortDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerService _logger;

        public GetStreetcodeShortByIdHandler(IMapper mapper, IRepositoryWrapper repository, ILoggerService logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<StreetcodeShortDto>> Handle(GetStreetcodeShortByIdQuery request, CancellationToken cancellationToken)
        {
            var streetcode = await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(st => st.Id == request.Id);

            if (streetcode == null)
            {
                string errorMsg = StreetcodeErrors.GetStreetcodeShortByIdHandlerCannotFindStreetcodeByIdError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var streetcodeShortDTO = _mapper.Map<StreetcodeShortDto>(streetcode);

            if(streetcodeShortDTO == null)
            {
                string errorMsg = StreetcodeErrors.GetStreetcodeShortByIdHandlerCannotMapStreetcodeToShortDtoError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(streetcodeShortDTO);
        }
    }
}
