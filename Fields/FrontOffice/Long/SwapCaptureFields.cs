using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields.FrontOffice.Long
{
    public class SwapCaptureFields
    {
        public string Notional { get; set; } = string.Empty;

        public string AmortisationType { get; set; } = string.Empty;

        public string StartDate { get; set; } = string.Empty;

        public string MaturityDate { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public string ClearingHouse { get; set; } = string.Empty;

        public string FixedDayCountConvention { get; set; } = string.Empty;

        public string FixedBusinessDayConvention { get; set; } = string.Empty;

        public string FirstCouponDate { get; set; } = string.Empty;

        public string CouponLastStartDate { get; set; } = string.Empty;

        public string CouponDay { get; set; } = string.Empty;

        public string CouponFrequency { get; set; } = string.Empty;

        public string ReferenceRate { get; set; } = string.Empty;

        public string FloatingDayCountConvention { get; set; } = string.Empty;

        public string FloatingBusinessDayConvention { get; set; } = string.Empty;

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

        public override string ToString()
        {
            return $"Notional: {Notional} AmortisationType: {AmortisationType} NumbOfAmort: {NumbOfAmort} AmortFreq: {AmortFreq}";
        }
    }
}