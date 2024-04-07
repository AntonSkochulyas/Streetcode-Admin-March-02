using FluentValidation;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    internal class CreateStatisticRecordCommandValidator : AbstractValidator<CreateStatisticRecordCommand>
    {
        private const ushort _addressMaxLength = 150;
        public CreateStatisticRecordCommandValidator()
        {
            RuleFor(command => command.StatisticRecordDto.Address).NotEmpty()
                .MaximumLength(_addressMaxLength).WithMessage(string.Format(StatisticRecordsErrors.CreateStatisticRecordCommandValidatorAddressMaxLengthError, _addressMaxLength));
        }
    }
}
