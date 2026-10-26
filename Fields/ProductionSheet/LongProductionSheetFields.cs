using Castle.Core.Resource;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields
{
    public class LongProductionSheetFields
    {
        public string CaseNumber { get; set; } = string.Empty;

        public string Afdeling { get; set; } = string.Empty;

        public string Product { get; set; } = string.Empty;

        public string Event { get; set; } = string.Empty;

        public string DatumDealMoment { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string Geldnemer { get; set; } = string.Empty;

        public string Hoofdsom { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string Eind { get; set; } = string.Empty;

        public string RenteSpreadherziening { get; set; } = string.Empty;

        public string Amortizatie { get; set; } = string.Empty;

        public string AmortFrequency { get; set; } = string.Empty;

        // public string VastOfVariabeleHoofdsom { get; set; } = "";
        public string MaturityInYears { get; set; } = string.Empty;

        public string Customerrate { get; set; } = string.Empty;

        public string Euribor { get; set; } = string.Empty;

        public string Spread { get; set; } = string.Empty;

        public string Fee { get; set; } = string.Empty;

        public string BasisSwap3mEvs6mE { get; set; } = string.Empty;

        public string Spread3mE { get; set; } = string.Empty;

        public string Spread6mE { get; set; } = string.Empty;

        public string Funding6mE { get; set; } = string.Empty;

        public string MargeTovFunding6mE { get; set; } = string.Empty;

        public string? Sweging { get; set; } = string.Empty;

        public string? ISF { get; set; } = string.Empty;

        public string? RWA { get; set; } = string.Empty;

        public string? OriginalLoanNumber { get; set; } = string.Empty;

        public string? Opmerking { get; set; } = string.Empty;
    }
}