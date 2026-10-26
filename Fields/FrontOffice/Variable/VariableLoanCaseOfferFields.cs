using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class VariableLoanCaseOfferFields
    {
        public string? TC { get; set; }

        public string? Product { get; set; }

        public string? Event { get; set; }

        public string? ExpectedOriginalSecID { get; set; }

        public string? CounterParty { get; set; }

        public string? GuarantorType { get; set; }

        public string? GuarantorRefNumber { get; set; }

        public string? ExpectedAanhef { get; set; }

        public string? ExpectedLooptijd { get; set; }

        public string? ExpectedContractStartDate { get; set; }

        public string? ExpectedContractEndDate { get; set; }

        public string? OfferCaseType { get; set; }

        public string? CustAdvisorName { get; set; }

        public string? SetGuarantor { get; set; }

        public string? SetExtraEmailAddresses { get; set; }

        public string? AdditEmail1 { get; set; }

        public string? AdditEmail2 { get; set; }

        public string? CDDReviewDate { get; set; }

        public string? CreditLimit { get; set; }

        public string? Guarantor1 { get; set; }

        public string? Guarantor2 { get; set; }

        public string? ExpectedGuarantor1 { get; set; }

        // public string? ExpectedLoanUpdateGuarantor { get; set; }
        public string? EmailAdress1 { get; set; }

        public string? EmailAdress2 { get; set; }

        public bool DeleteGuarantor { get; set; }

        public bool SpreadCheckBy { get; set; }

        public string? OfferReference { get; set; }

        public string SpreadCheckRemarks { get; set; }

        public string? Notional { get; set; }

        public string? ExpectedNotional { get; set; }

        public string? DealMoment { get; set; }

        public string? LoanType { get; set; }

        public string? SetNotionalTypeToFixed { get; set; }

        public string? MinimalWithdrawal { get; set; }

        public string? CommitmentFee { get; set; }

        public string? NotificationMinutes { get; set; }
    }
}