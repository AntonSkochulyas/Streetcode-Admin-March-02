using AutoMapper;
using Streetcode.BLL.Dto.Payment;
using Streetcode.DAL.Entities.Payment;

namespace Streetcode.BLL.Mapping.Payment;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<InvoiceInfo, PaymentResponseDto>().ReverseMap();
    }
}