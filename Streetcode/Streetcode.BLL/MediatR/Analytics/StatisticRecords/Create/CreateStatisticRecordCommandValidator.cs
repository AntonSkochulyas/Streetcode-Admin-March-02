// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    /// <summary>
    /// Validator, that validates a model inside a CreateStatisticRecordCommand.
    /// </summary>
    internal class CreateStatisticRecordCommandValidator : AbstractValidator<CreateStatisticRecordCommand>
    {
        // Adress max length
        private const ushort _addressMaxLength = 150;

        // Constructor
        public CreateStatisticRecordCommandValidator()
        {
            RuleFor(command => command.StatisticRecordDto.Address).NotEmpty()
                .MaximumLength(_addressMaxLength).WithMessage(string.Format(StatisticRecordsErrors.CreateStatisticRecordCommandValidatorAddressMaxLengthError, _addressMaxLength));
        }
    }
}
