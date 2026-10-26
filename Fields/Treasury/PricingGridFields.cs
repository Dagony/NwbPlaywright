using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class PricingGridFields
    {
        public string MaturityColumn { get; set; }

        public string FundingCurveColumn { get; set; }

        public string FTPColumn { get; set; }

        public string MinSpreadUgColumn { get; set; }

        public PricingGridFields(string maturityColumn, string fundingCurveColumn, string ftpColumn, string minSpreadUgColumn)
        {
            this.MaturityColumn = maturityColumn;
            this.FundingCurveColumn = fundingCurveColumn;
            this.FTPColumn = ftpColumn;
            this.MinSpreadUgColumn = minSpreadUgColumn;
        }

        public override string ToString()
        {
            return $"Maturity: {MaturityColumn.PadRight(5)} FundingCurve: {FundingCurveColumn.PadRight(8)} FTP: {FTPColumn.PadRight(10)} MinSpreadUg: {MinSpreadUgColumn.PadRight(10)}";
        }
    }
}