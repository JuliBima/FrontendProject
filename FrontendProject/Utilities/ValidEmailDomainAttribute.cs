using System.ComponentModel.DataAnnotations;

namespace FrontendProject.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object? value)
        {
            var email = value.ToString().Split("@");
            return email[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
