using Castle.Core.Resource;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class ShortUGSheetFields
    {
        public string Datum { get; set; } = string.Empty;

        public string TraderAfkorting { get; set; } = string.Empty;

        public string Pega { get; set; } = string.Empty;

        public string Via { get; set; } = string.Empty;

        public string Soort { get; set; } = string.Empty;

        public string Geldnemer { get; set; } = string.Empty;

        public string Contactpersoon { get; set; } = string.Empty;

        public string Hoofdsom { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string Eind { get; set; } = string.Empty;

        public string CustomerRate { get; set; } = string.Empty;

        public string MaturityInYears { get; set; } = string.Empty;

        public string CDD { get; set; } = string.Empty;

        public string CreditAssessment { get; set; } = string.Empty;

        public string? Opmerking { get; set; } = string.Empty;

        // public string? SoortHerziening { get; set; } = "";
        // public string? BaseSecID { get; set; } = "";
    }
}