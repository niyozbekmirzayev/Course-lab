using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Courselab.Domain.CustomAttributes
{
    public class FileTypeAttribute : ValidationAttribute
    {
        private readonly string[] extensions;
        public FileTypeAttribute(string[] extensions) => this.extensions = extensions;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!this.extensions.Contains(extension.ToLower()))
                {
                    string extensionCountIdentifier = "extensions";
                    if (this.extensions.Count() == 1)
                        extensionCountIdentifier = "extension";

                    return new ValidationResult($"Only {string.Join(",", this.extensions)} file {extensionCountIdentifier} supported");
                }
            }

            return ValidationResult.Success;
        }
    }
}
