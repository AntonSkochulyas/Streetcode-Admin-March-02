using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specification.Team
{
    public class GetAllTeamSpec : Specification<TeamMember>
    {
        public GetAllTeamSpec()
        {
            Query.Include(p => p.Positions).Include(tl => tl.TeamMemberLinks);
        }
    }
}
