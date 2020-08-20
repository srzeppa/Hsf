using FluentValidation;
using Hsf.ApplicatonProcess.August2020.Domain.Models;

namespace Hsf.ApplicatonProcess.August2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).Null().MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5).NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(x => x.EmailAddress).NotNull().NotEmpty().EmailAddress();//todo top level domain
            RuleFor(x => x.Age).GreaterThan(20).LessThan(60);
            RuleFor(x => x.Hired).NotNull();
        }
    }
}
