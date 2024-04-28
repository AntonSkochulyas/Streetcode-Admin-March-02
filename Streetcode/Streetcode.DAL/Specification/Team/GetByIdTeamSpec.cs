using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specification.Team
{
    public class GetByIdTeamSpec : Specification<TeamMember>, ISingleResultSpecification<TeamMember>
    {
        public GetByIdTeamSpec(int id)
        {
            Id = id;
            Query.Where(t => t.Id == id).Include(p => p.Positions).Include(tl => tl.TeamMemberLinks);
        }

        public int Id { get; set; }
    }
}
