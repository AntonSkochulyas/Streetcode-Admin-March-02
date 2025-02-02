﻿using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Transactions.TransactionLink
{
    public class GetByStreetcodeIdTransactionLinkSpec : Specification<Entities.Transactions.TransactionLink>, ISingleResultSpecification<Entities.Transactions.TransactionLink>
    {
        public GetByStreetcodeIdTransactionLinkSpec(int streetcodeId)
        {
            StreetcodeId = streetcodeId;
            Query.Where(t => t.StreetcodeId == streetcodeId);
        }

        public int StreetcodeId { get; set; }
    }
}
