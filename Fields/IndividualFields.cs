using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class IndividualFields
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; } = string.Empty;

        public string? Preposition { get; set; }

        public string? DateOfBirth { get; set; }

        public string? GenderType { get; set; }

        public string? CountryOfOrigin { get; set; }

        public string? CountryOfCitizenship { get; set; }

        public string? CountryOfResidency { get; set; }

        public string? EmailIndividual { get; set; } = string.Empty;

        public string? PhoneIndividual { get; set; } = string.Empty;
    }
}