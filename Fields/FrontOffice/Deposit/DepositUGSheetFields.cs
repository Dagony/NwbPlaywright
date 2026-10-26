using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields.FrontOffice.Deposit
{
    public class DepositUGSheetFields
    {
        public string Datum { get; set; } = string.Empty;

        public string TraderAfkorting { get; set; } = string.Empty;

        public string EmptyColumn { get; set; } = string.Empty;

        public string Soort { get; set; } = string.Empty;

        public string Geldnemer { get; set; } = string.Empty;

        public string EmptyColumn2 { get; set; } = string.Empty;

        public string Hoofdsom { get; set; } = string.Empty;

        public string Start { get; set; } = string.Empty;

        public string Eind { get; set; } = string.Empty;

        public string CustomerRate { get; set; } = string.Empty;

        // public string MaturityInYears { get; set; } = "";
        // public string CDD { get; set; } = "";
        // public string CreditAssessment { get; set; } = "";
        // public string? Opmerking { get; set; } = "";
        // public string? SoortHerziening { get; set; } = "";
        // public string? BaseSecID { get; set; } = "";
    }
}