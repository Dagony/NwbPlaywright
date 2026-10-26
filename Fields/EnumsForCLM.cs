using System.ComponentModel.DataAnnotations;

namespace NwbPlaywright.Fields
{
    public class EnumsForCLM
    {
        public enum Environment
        {
            Dev,
            Test,
            Accept,
        }

        public enum CDDcategory
        {
            Low,
            Normal,
            High,
        }

        public enum LoanType
        {
            BaseExtendable,
            BaseInterest,
            CoveredBond,
            Extendable,
            InterestReset,
            Normal,
            Structure,
            VariableNotional,
            ZeroCoupon,
        }

        public enum AmortisationFrequency
        {
            Annual,
            [Display(Name = "Semi annual")]
            Semiannual,
            Quarterly,
            Monthly,
            Weekly,
        }

        public enum LegalForm
        {
            Eenmanszaak,
            EenmanszaakMetMeerDanEen,
            Maatschap,
            NVBVinOprichtingOpA,
            NVBVinOprichtingOpB,
            Rederij,
            VenootschapOnderFirma,
            Trust,
            BeslotenVennootschapMetGewoneStructuur,
        }

        public enum ReferenceRate
        {
            [Display(Name = "1wE")]
            Euribor1wE,
            [Display(Name = "1mE")]
            Euribor1mE,
            [Display(Name = "3mE")]
            Euribor3mE,
            [Display(Name = "6mE")]
            Euribor6mE,
            [Display(Name = "12mE")]
            Euribor12mE,
        }

        public enum DataTransformOptions
        {
            Offermail,
            Contracts,
            UGsheet,
            OpicsFiles,
            FrontOfficeScreens,
            BackOfficeScreens,
            Coverletter,
            Sluitbrief,
        }
    }
}