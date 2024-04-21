// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.BLL.Interfaces.Logging;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Toponyms.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all toponyms from database.
    /// </summary>
    public class GetAllToponymsHandler : IRequestHandler<GetAllToponymsQuery,
        Result<GetAllToponymsResponseDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public GetAllToponymsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that get all toponyms from database.
        /// </summary>
        /// <param name="query">
        /// Request to get all toponyms from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of ToponymsDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<GetAllToponymsResponseDto>> Handle(GetAllToponymsQuery query, CancellationToken cancellationToken)
        {
            var filterRequest = query.request;

            var toponyms = _repositoryWrapper.ToponymRepository
                 .FindAll();

            if (filterRequest.Title is not null)
            {
                FindStreetcodesWithMatchTitle(ref toponyms, filterRequest.Title);
            }

            var toponymDtos = _mapper.Map<IEnumerable<ToponymDto>>(toponyms.AsEnumerable());

            var response = new GetAllToponymsResponseDto
            {
                Pages = 1,
                Toponyms = toponymDtos
            };

            return Result.Ok(response);
        }

        private void FindStreetcodesWithMatchTitle(
            ref IQueryable<Toponym> toponyms,
            string title)
        {
            toponyms = toponyms.Where(s => s.StreetName
                .ToLower()
                .Contains(title
                .ToLower()))
                .GroupBy(s => s.StreetName)
                .Select(g => g.First());
        }
    }
}