using Ardalis.Specification;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetByStreetcodeIdSourceLinkCategorySpec : Specification<Entities.Sources.SourceLinkCategory>
    {
        public int StreetcodeId;
        public GetByStreetcodeIdSourceLinkCategorySpec(int streetcodeId)
        {
            StreetcodeId = streetcodeId;
            Query.Where(slc => slc.Streetcodes.Any(s => s.Id == streetcodeId)).Include(scc => scc.Streetcodes).Include(i => i.Image);
        }
    }
}
