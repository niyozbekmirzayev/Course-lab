using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Courselab.Domain.CustomAttributes
{
    public class LimitEnumValueAttribute : ValidationAttribute
    {
        private readonly int maxValue;
        private readonly int minValue;

        public LimitEnumValueAttribute(int maxValue, int minValue = 0)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string state = value.ToString();

            bool result = state.All(Char.IsDigit);

            if (result)
            {
                int stateValue = Convert.ToInt32(state);

                if (stateValue <= maxValue)
                    if (stateValue >= minValue)
                        return ValidationResult.Success;

                    else return new ValidationResult("You can't enter a value less than " + minValue);
                else return new ValidationResult("You can't enter a value greater than " + maxValue);
            }

            else return ValidationResult.Success;
        }
    }
}
