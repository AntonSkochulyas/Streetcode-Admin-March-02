using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByIdTransactionLinkSpec : Specification<Streetcode.DAL.Entities.Transactions.TransactionLink>, ISingleResultSpecification<Streetcode.DAL.Entities.Transactions.TransactionLink>
    {
        public GetByIdTransactionLinkSpec(int id)
        {
            Query.Where(tl => tl.Id == id);
        }
    }
}
