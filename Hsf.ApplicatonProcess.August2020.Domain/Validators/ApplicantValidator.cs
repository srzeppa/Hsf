using System;
using FluentValidation;
using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Hsf.ApplicatonProcess.August2020.Domain.Providers;

namespace Hsf.ApplicatonProcess.August2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private readonly ICountriesProvider _countriesProvider;

        public ApplicantValidator(ICountriesProvider countriesProvider)
        {
            _countriesProvider = countriesProvider;

            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5).NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(x => x.EmailAddress).NotNull().NotEmpty().EmailAddress();//todo top level domain
            RuleFor(x => x.Age).GreaterThan(20).LessThan(60);
            RuleFor(x => x.Hired).NotNull();
            RuleFor(x => x.CountryOfOrigin).Must(x => CountryExists(x)).WithMessage(x => $"Country {x.CountryOfOrigin} does not exists.");
        }

        private bool CountryExists(string countryName)
        {
            var exists = _countriesProvider.CheckIfCountryExists(countryName).GetAwaiter().GetResult();
            return exists;
        }
    }
}
