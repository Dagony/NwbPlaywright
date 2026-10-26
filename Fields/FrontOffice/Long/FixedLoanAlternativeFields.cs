using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields
{
    public class FixedLoanAlternativeFields
    {
        // public string OfferReference { get; set; } = "";
        public string StartDate { get; set; } = string.Empty;

        public string MaturityDate { get; set; } = string.Empty;

        public string FirstCouponDate { get; set; } = string.Empty;

        public string LastCouponDate { get; set; } = string.Empty;

        public string InterestRRDate { get; set; } = string.Empty;

        public string CouponDay { get; set; } = string.Empty;

        public string AmortType { get; set; } = string.Empty;

        public string CouponFreq { get; set; } = string.Empty;

        public bool Swap { get; set; }

        public string DayCount { get; set; } = string.Empty;

        public string ExpectedDayCount { get; set; } = string.Empty;

        public string NumbOfAmort { get; set; } = string.Empty;

        public string AmortFreq { get; set; } = string.Empty;

        public string SpreadH1 { get; set; } = string.Empty;

        public string SpreadH1Comment { get; set; } = string.Empty;

        public string OverwriteSpreadH1Comment { get; set; } = string.Empty;

        public string SpreadH2 { get; set; } = string.Empty;

        public string SpreadH2Comment { get; set; } = string.Empty;

        public string OverwriteSpreadH2Comment { get; set; } = string.Empty;

        public string OverwriteSpreadMismatchComment { get; set; } = string.Empty;

        public string FinalSpread { get; set; } = string.Empty;

        public string FinalSpreadComment { get; set; } = string.Empty;

        public string OverwriteFinalSpreadComment { get; set; } = string.Empty;

        public string MarketIRate { get; set; } = string.Empty;

        public string CustomerRate { get; set; } = string.Empty;

        public string? DealRemark { get; set; } = null;

        public string? OverwriteDealRemark { get; set; } = null;

        public string? SwapReferenceRate { get; set; } = null;

        public string? ClearingHouse { get; set; } = null;

        public string? BusinessDayConvention { get; set; } = null;
    }
}