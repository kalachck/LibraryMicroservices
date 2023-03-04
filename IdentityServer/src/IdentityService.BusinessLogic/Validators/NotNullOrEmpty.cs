using System.ComponentModel.DataAnnotations;

namespace IdentityService.BusinessLogic.Validators
{
    public class NotNullOrEmpty : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var stringValue = value as string;

            return !string.IsNullOrWhiteSpace(stringValue);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (IsValid(value) == false)
            {
                var errorMessage = $"{FormatErrorMessage(validationContext.DisplayName)} Field mustn't be null or empty";

                throw new ValidationException(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
