using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByIdTransactionLinkSpec : Specification<Entities.Transactions.TransactionLink>, ISingleResultSpecification<Entities.Transactions.TransactionLink>
    {
        public int Id;
        public GetByIdTransactionLinkSpec(int id)
        {
            Id = id;
            Query.Where(t => t.Id == id);
        }
    }
}
