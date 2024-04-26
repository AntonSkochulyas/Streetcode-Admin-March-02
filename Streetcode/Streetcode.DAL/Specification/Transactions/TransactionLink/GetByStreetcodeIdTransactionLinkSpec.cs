using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByStreetcodeIdTransactionLinkSpec : Specification<Entities.Transactions.TransactionLink>, ISingleResultSpecification<Entities.Transactions.TransactionLink>
    {
        public GetByStreetcodeIdTransactionLinkSpec(int streetcodeId)
        {
            Query.Where(tr => tr.StreetcodeId == streetcodeId);
        }
    }
}
