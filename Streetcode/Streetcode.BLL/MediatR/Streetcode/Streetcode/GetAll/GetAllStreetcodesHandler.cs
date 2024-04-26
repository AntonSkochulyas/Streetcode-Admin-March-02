// Necessary usings.
using System.Linq.Expressions;
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all streetcodes from database.
    /// </summary>
    public class GetAllStreetcodesHandler : IRequestHandler<GetAllStreetcodesQuery, Result<GetAllStreetcodesResponseDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public GetAllStreetcodesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for retrieving all streetcodes from the database.
        /// </summary>
        /// <param name="query">
        /// The query to retrieve all streetcodes from the database.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token to cancel the operation if necessary.
        /// </param>
        /// <returns>
        /// An IEnumerable of StreetcodeDto, or an error if encountered during the retrieval process.
        /// </returns>
        public async Task<Result<GetAllStreetcodesResponseDto>> Handle(GetAllStreetcodesQuery query, CancellationToken cancellationToken)
        {
            var filterRequest = query.Request;

            var streetcodes = _repositoryWrapper.StreetcodeRepository
                .FindAll();

            if (filterRequest.Title is not null)
            {
                FindStreetcodesWithMatchTitle(ref streetcodes, filterRequest.Title);
            }

            if (filterRequest.Sort is not null)
            {
                FindSortedStreetcodes(ref streetcodes, filterRequest.Sort);
            }

            if (filterRequest.Filter is not null)
            {
                FindFilteredStreetcodes(ref streetcodes, filterRequest.Filter);
            }

            int pagesAmount = ApplyPagination(ref streetcodes, filterRequest.Amount, filterRequest.Page);

            var streetcodeDtos = _mapper.Map<IEnumerable<StreetcodeDto>>(streetcodes.AsEnumerable());

            var response = new GetAllStreetcodesResponseDto
            {
                Pages = pagesAmount,
                Streetcodes = streetcodeDtos
            };

            return Result.Ok(response);
        }

        private void FindStreetcodesWithMatchTitle(
            ref IQueryable<StreetcodeContent> streetcodes,
            string title)
        {
            streetcodes = streetcodes.Where(s => s.Title
                .ToLower()
                .Contains(title
                .ToLower()) || s.Index
                .ToString() == title);
        }

        private void FindFilteredStreetcodes(
            ref IQueryable<StreetcodeContent> streetcodes,
            string filter)
        {
            var filterParams = filter.Split(':');
            var filterColumn = filterParams[0];
            var filterValue = filterParams[1];

            streetcodes = streetcodes
                .AsEnumerable()
                .Where(s => filterValue.Contains(s.Status.ToString()))
                .AsQueryable();
        }

        private void FindSortedStreetcodes(
            ref IQueryable<StreetcodeContent> streetcodes,
            string sort)
        {
            var sortedRecords = streetcodes;

            var sortColumn = sort.Trim();
            var sortDirection = "asc";

            if (sortColumn.StartsWith("-"))
            {
                sortDirection = "desc";
                sortColumn = sortColumn.Substring(1);
            }

            var type = typeof(StreetcodeContent);
            var parameter = Expression.Parameter(type, "p");
            var property = Expression.Property(parameter, sortColumn);
            var lambda = Expression.Lambda(property, parameter);

            streetcodes = sortDirection switch
            {
                "asc" => Queryable.OrderBy(sortedRecords, (dynamic)lambda),
                "desc" => Queryable.OrderByDescending(sortedRecords, (dynamic)lambda),
                _ => sortedRecords,
            };
        }

        private int ApplyPagination(
            ref IQueryable<StreetcodeContent> streetcodes,
            int amount,
            int page)
        {
            var totalPages = (int)Math.Ceiling(streetcodes.Count() / (double)amount);

            streetcodes = streetcodes
                .Skip((page - 1) * amount)
                .Take(amount);

            return totalPages;
        }
    }
}