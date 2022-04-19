using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Courselab.Domain.CustomAttributes
{
    public class YouTubeLinkAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string link = value as string;
            if (String.IsNullOrEmpty(link))
                return new ValidationResult("Invalid YouTube link");

            else if (Regex.IsMatch(link, @"^(http|https)://(www.youtube.com/watch\?v=|youtu.be/)[a-zA-Z0-9_-]{11}$"))
                return ValidationResult.Success;

            else return new ValidationResult("Invalid YouTube link");
        }

    }
}
