using Castle.Core.Resource;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Fields
{
    public class VariableUGSheetFields
    {
        public string Datum { get; set; } = string.Empty;

        public string TraderAfkorting { get; set; } = string.Empty;

        public string Pega { get; set; } = string.Empty;

        public string Via { get; set; } = string.Empty;

        public string Soort { get; set; } = string.Empty;

        public string Geldnemer { get; set; } = string.Empty;

        public string Beslismodel { get; set; } = string.Empty;

        public string Contactpersoon { get; set; } = string.Empty;

        public string Hoofdsom { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string Eind { get; set; } = string.Empty;

        public string Renteherziening { get; set; } = string.Empty;

        public string VastOfVariabeleHoofdsom { get; set; } = string.Empty;

        public string MaturityInYears { get; set; } = string.Empty;

        public string Euribor { get; set; } = string.Empty;

        public string CustomerRate { get; set; } = string.Empty;

        public string CouponDatum { get; set; } = string.Empty;

        public string Conventie { get; set; } = string.Empty;

        public string FirstGuarantor { get; set; } = string.Empty;

        public string CreditAssessment { get; set; } = string.Empty;

        public string Fee { get; set; } = string.Empty;

        public string BasisSwap { get; set; } = string.Empty;

        public string Spread3mE { get; set; } = string.Empty;

        public string Spread6mE { get; set; } = string.Empty;

        public string ReguliereFundingFTP { get; set; } = string.Empty;

        public string? KortingTLTRO { get; set; } = string.Empty;

        // public string? FundingTLTRO { get; set; } = "";
        //  public string? MargeTovRegFunding { get; set; } = "";
        //  public string? MargeTovTLTROFunding { get; set; } = "";
        public string? Opmerking { get; set; } = string.Empty;

        public string? SoortHerziening { get; set; } = string.Empty;

        public string? BaseSecID { get; set; } = string.Empty;
    }
}