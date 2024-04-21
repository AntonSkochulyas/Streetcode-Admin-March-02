using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByStreetcodeIdTransactionLinkSpec : Specification<Streetcode.DAL.Entities.Transactions.TransactionLink>, ISingleResultSpecification<Streetcode.DAL.Entities.Transactions.TransactionLink>
    {
        public GetByStreetcodeIdTransactionLinkSpec(int streetcodeId)
        {
            Query.Where(tr => tr.StreetcodeId == streetcodeId);
        }
    }
}
