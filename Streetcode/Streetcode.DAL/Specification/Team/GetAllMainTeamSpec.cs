using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specification.Team
{
    public class GetAllMainTeamSpec : Specification<TeamMember>
    {
        public GetAllMainTeamSpec()
        {
            Query.Where(t => t.IsMain).Include(p => p.Positions).Include(tl => tl.TeamMemberLinks);
        }
    }
}
