using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwbPlaywright.Fields
{
    public class VariableFinalSpreadFields
    {
        public string? TC { get; set; }

        public string? ExpectedAanhef { get; set; }

        public string? GuarantorType { get; set; }

        public string? ExpectedLooptijd { get; set; }

        public string? OfferCaseType { get; set; }

        public string? CustAdvisorName { get; set; }

        public string? SetGuarantor { get; set; }

        public string? SetExtraEmailAddresses { get; set; }

        public string? AdditEmail1 { get; set; }

        public string? AdditEmail2 { get; set; }

        public string? CDDReviewDate { get; set; }

        public string? Guarantor1 { get; set; }

        public string? Guarantor2 { get; set; }

        public string? EmailAdress1 { get; set; }

        public string? EmailAdress2 { get; set; }

        public bool DeleteGuarantor { get; set; }

        public bool SpreadCheckBy { get; set; }

        public string? SpreadCheckRemarks { get; set; }

        public string? Notional { get; set; }

        public string? DealMoment { get; set; }

        public string? LoanType { get; set; }

        // public string? NotionalTypeIsVariable { get; set; }
        public string? SetNotionalTypeToFixed { get; set; }

        public string? MinimalWithdrawal { get; set; }

        public string? CommitmentFee { get; set; }

        public string? NotificationMinutes { get; set; }

        public string? OfferReference { get; set; }

        public string? StartDate { get; set; }

        public string? MaturityDate { get; set; }

        public string? FirstCouponDate { get; set; }

        public string? LastCouponDate { get; set; }

        public string? InterestSpreadRRDate { get; set; }

        public string? CouponDay { get; set; } = "99";

        public string? ReferenceRate { get; set; }

        public string? HoursClause { get; set; } = "True";

        public string? SpreadH1 { get; set; }

        public string? SpreadH1Basis { get; set; }

        public string? SpreadH1Comment { get; set; }

        public string? SpreadH2 { get; set; }

        public string? SpreadH2Basis { get; set; }

        public string? SpreadH2Comment { get; set; }

        public string? FinalSpread { get; set; }

        public string? FinalSpreadBasis { get; set; }

        public string? FinalSpreadComment { get; set; }

        public string? CustomerRate { get; set; }

        public string? DealRemark { get; set; }
        //// public string EmailContentText { get; } =
        //     @"Dit tarief is 5 minuten handelbaar. Wij horen graag of u hier gebruik van wilt maken.

        // Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door uw gevraagde bedrag dient te worden overgemaakt. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze back office aan u zal worden toegestuurd.

        // Op deze lening zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden
        //     De NWB Bank kan pas tot storting van de lening overgaan nadat de geldleningsovereenkomst door alle partijen rechtsgeldig is ondertekend en door de NWB Bank is ontvangen. Mocht onverhoopt de ondertekening van de geldleningsovereenkomst niet tijdig rondkomen, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door de NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door de NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.

        // Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.

        // Bovenstaande is onder voorbehoud van typefouten.";
    }
}