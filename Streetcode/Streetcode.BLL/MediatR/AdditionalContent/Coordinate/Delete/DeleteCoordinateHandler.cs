// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;

/// <summary>
/// Handler that deletes a streetcode coordinate.
/// </summary>
public class DeleteCoordinateHandler : IRequestHandler<DeleteCoordinateCommand, Result<StreetcodeCoordinateDto>>
{
    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    // Parametric constructor
    public DeleteCoordinateHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    // Delete streetcode coordinate method
    public async Task<Result<StreetcodeCoordinateDto>> Handle(DeleteCoordinateCommand request, CancellationToken cancellationToken)
    {
        // Get first finded streetcode coordinate by id
        var findedStreetcodeCoordinateToDelete = await _repositoryWrapper.StreetcodeCoordinateRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

        // If can not find streetcode coordinate by id from request - > return Result.Fail with error from resource file
        if (findedStreetcodeCoordinateToDelete is null)
        {
            return Result.Fail(new Error(string.Format(CoordinateErrors.DeleteCoordinateHandlerNotFoundByIdError, request.Id)));
        }

        // Deleting streetcode coordinate
        _repositoryWrapper.StreetcodeCoordinateRepository.Delete(findedStreetcodeCoordinateToDelete);

        // Check, that save of deleted streetcode coordinate was successfully
        var isDeletedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

        // If save of deleted streetcode coordinate was successfully - > return deleted streetcode coordinate
        if (isDeletedSuccessfully)
        {
            return Result.Ok(_mapper.Map<StreetcodeCoordinateDto>(findedStreetcodeCoordinateToDelete));
        }

        // If save of deleted streetcode coordinate was not successfully - > return Result.Fail with error from resource file
        return Result.Fail(new Error(CoordinateErrors.DeleteCoordinateHandlerDeleteFailedError));
    }
}