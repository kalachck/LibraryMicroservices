using System.ComponentModel.DataAnnotations;

namespace LibraryService.BussinesLogic.Validators
{
    public class HasMaxLength : ValidationAttribute
    {
        private readonly int _maxLength;

        public HasMaxLength(int maxLength)
        {
            _maxLength = maxLength;
        }

        public override bool IsValid(object? value)
        {
            return value.ToString().Length <= _maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (IsValid(value) == false)
            {
                var errorMessage = $"{FormatErrorMessage(validationContext.DisplayName)} " +
                    $"Field mustn't contain more than {_maxLength} characters";

                throw new ValidationException(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
