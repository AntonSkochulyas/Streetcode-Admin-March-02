// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;

/// <summary>
/// Handler that creates a streetcode coordinate to streetcode.
/// </summary>
public class CreateCoordinateHandler : IRequestHandler<CreateCoordinateCommand, Result<StreetcodeCoordinateDto>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Parametric constructor
    public CreateCoordinateHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    // Create streetcode coordinate method
    public async Task<Result<StreetcodeCoordinateDto>> Handle(CreateCoordinateCommand request, CancellationToken cancellationToken)
    {
        // Map entity from request to DAL StreetcodeCoordinate
        var mappedStreetcodeCoordinate = _mapper.Map<StreetcodeCoordinate>(request.StreetcodeCoordinate);

        // If can not map entity from request - > return Result.Fail with error from resource file
        if (mappedStreetcodeCoordinate is null)
        {
            return Result.Fail(new Error(CoordinateErrors.CreateCoordinateHandlerCanNotConvertFromNullError));
        }

        // Getting created streetcode coordinate
        var createdStreetcodeCoordinate = _repositoryWrapper.StreetcodeCoordinateRepository.Create(mappedStreetcodeCoordinate);

        // Check, that save of created streetcode coordinate was successfully
        var isCreatedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

        // If save of created streetcode coordinate was successfully - > return created streetcode coordinate
        if (isCreatedSuccessfully)
        {
            return Result.Ok(_mapper.Map<StreetcodeCoordinateDto>(createdStreetcodeCoordinate));
        }

        // If save of created streetcode coordinate was not successfully - > return Result.Fail with error from resource file
        return Result.Fail(new Error(CoordinateErrors.CreateCoordinateHandlerCreateFailedError));
    }
}