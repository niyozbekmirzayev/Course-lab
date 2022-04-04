using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Courselab.Domain.CustomAttributes
{
    public class PasswordAttribute : ValidationAttribute
    {
        private readonly int minPasswordLenth;

        public PasswordAttribute(int minPasswordLenth) => this.minPasswordLenth = minPasswordLenth;
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as String;
            
            if (password != null)
                if (password.Length >= minPasswordLenth)
                    if (password.Any(char.IsUpper))
                        if (password.Any(char.IsLower))
                            if (password.Any(char.IsDigit)) return ValidationResult.Success;

                            else return new ValidationResult("Password must contain digit");
                        else return new ValidationResult("Password must contain lowercase letter");
                    else return new ValidationResult("Password must contain uppercase letter");
                else return new ValidationResult($"Password must contain at least {minPasswordLenth} characters");
            else return new ValidationResult("Password must be created");
        }
    }
}
