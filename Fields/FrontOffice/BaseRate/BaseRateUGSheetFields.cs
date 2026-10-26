using Castle.Core.Resource;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class BaseRateUGSheetFields
    {
        public string Datum { get; set; } = string.Empty;

        public string TraderAfkorting { get; set; } = string.Empty;

        public string Via { get; set; } = string.Empty;

        public string Soort { get; set; } = string.Empty;

        public string Geldnemer { get; set; } = string.Empty;

        public string Contactpersoon { get; set; } = string.Empty;

        public string Hoofdsom { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string Eind { get; set; } = string.Empty;

        public string Renteherziening { get; set; } = string.Empty;

        public string Amortizatie { get; set; } = string.Empty;

        public string VastOfVariabeleHoofdsom { get; set; } = string.Empty;

        public string MaturityInYears { get; set; } = string.Empty;

        public string Euribor { get; set; } = string.Empty;

        public string Spread { get; set; } = string.Empty;

        public string CouponDatum { get; set; } = string.Empty;

        public string Conventie { get; set; } = string.Empty;

        public string BasisSwap3mEvs6mE { get; set; } = string.Empty;

        public string Spread3mE { get; set; } = string.Empty;

        public string Spread6mE { get; set; } = string.Empty;

        public string Spread6meFundingFTP { get; set; } = string.Empty;

        public string MargeTovRegFunding { get; set; } = string.Empty;

        public string? Basisrente { get; set; } = string.Empty;

        public string? OriginalSecID { get; set; } = string.Empty;

        public string? SwapVastePoot { get; set; } = string.Empty;

        public string BasisSwap3mEvs6mE2 { get; set; } = string.Empty;

        public string Looptijd { get; set; } = string.Empty;
    }
}