using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields
{
    public class VariableLoanAlternativeFields
    {
        public string? OfferReference { get; set; }

        public string? StartDate { get; set; }

        public string? MaturityDate { get; set; }

        public string NegativeInterestRate { get; set; } = string.Empty;

        public string? FirstCouponDate { get; set; }

        public string? LastCouponDate { get; set; }

        public string? InterestRRDate { get; set; }

        public string? CouponDay { get; set; }

        public string? ExpectedCouponFreq { get; set; }

        public string? ReferenceRate { get; set; }

        public string? HoursClause { get; set; }

        public string? SpreadH1 { get; set; }

        public string? SpreadH1Basis { get; set; }

        public string? OverwriteSpreadH1Comment { get; set; }

        public string? ExpectedDayCount { get; set; }

        public string? ExpectedAmort { get; set; }

        public string? SpreadH2 { get; set; }

        public string? SpreadH2Basis { get; set; }

        public string? OverwriteSpreadH2Comment { get; set; }

        public string? FinalSpread { get; set; }

        public string? FinalSpreadBasis { get; set; }

        public string? OverwriteFinalSpreadComment { get; set; }

        public string? OverwriteSpreadMismatchComment { get; set; }

        public string? CustomerRate { get; set; }

        public string? DealRemark { get; set; }
    }
}