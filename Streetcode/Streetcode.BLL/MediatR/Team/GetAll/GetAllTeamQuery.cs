// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Team;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Team.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all team from database.
    /// </summary>
    public record GetAllTeamQuery : IRequest<Result<IEnumerable<TeamMemberDto>>>
    {
        public GetAllTeamQuery()
        {
        }
    }
}
