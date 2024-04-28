using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByIdTransactionLinkSpec : Specification<Entities.Transactions.TransactionLink>, ISingleResultSpecification<Entities.Transactions.TransactionLink>
    {
        public GetByIdTransactionLinkSpec(int id)
        {
            Id = id;
            Query.Where(tl => tl.Id == id);
        }

        public int Id { get; set; }
    }
}
