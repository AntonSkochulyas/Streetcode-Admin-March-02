using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update
{
    public class UpdateRelatedTermHandler : IRequestHandler<UpdateRelatedTermCommand, Result<RelatedTermDto>>
    {
        public UpdateRelatedTermHandler()
        {
        }

        public Task<Result<RelatedTermDto>> Handle(UpdateRelatedTermCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
