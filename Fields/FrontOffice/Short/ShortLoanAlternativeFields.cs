using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields
{
    public class ShortLoanAlternativeFields
    {
        public string? OfferReference { get; set; }

        public string? StartDate { get; set; }

        public string? MaturityDate { get; set; }

        public string? PriceH1 { get; set; }

        public string? OverwritePriceH1Comment { get; set; }

        public string? PriceH2 { get; set; }

        public string? OverwritePriceH2Comment { get; set; }

        public string? PriceFinal { get; set; }

        public string OverwriteFinalPriceComment { get; set; } = string.Empty;

        public string? Margin { get; set; }

        public string OverwriteMarginComment { get; set; } = string.Empty;

        public string? CustomerRate { get; set; }

        public string OverwriteSpreadMismatchComment { get; set; } = string.Empty;

        public string? DealRemark { get; set; }
    }
}