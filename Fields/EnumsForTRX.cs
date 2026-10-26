using System.ComponentModel.DataAnnotations;

namespace NwbPlaywright.Fields
{
    public class EnumsForTRX
    {
        public enum PEGAClassStructure
        {
            New,   // geldt voor deposito en herzieningen
            Old, // geldt voor normale leningen: long, var en kort.
        }

        public enum LanguageSetting
        {
            NL,
            ENG,
        }

        public enum WorkListType
        {
            LoanUpdate,
            LoanUpdateResolved,
            LoanUpdateAdministration,
            Loan,
            LoanResolved,
            LoanAdministration,
            Deposit,
            DepositResolved,
            DepositAdministration,
            SWAP,
            SWAPAdministration,
        }

        public enum CaseType
        {
            LoanRequest,
            ResearchInteraction,
            PricingGrid,
            Swap,
        }

        public enum LoanRequest
        {
            Loan,
            Deposit,
            Other,
            InterestSpreadReset,
            OtherUpdate,
            Short,
            Long,
            Variable,
            Fixed,
            LoanUpdateFixed,
            LoanUpdateVariable,
        }

        public enum SelectProduct
        {
            TraditionalFixedRateLoanLong,
            FloatingRateLoanFixedNotional,
            FloatingRateLoanFloatingNotional,
            TraditionalFixedRateLoanShort,
            BaseRateLoan,
            MTNLocalAuthority,
            SustainableLinkedLoan,
            ExportCreditGuaranteeFixedrateFixedNotional,
            FixedRateLoanWithPrefinancingPeriod,
            ShortTermFacility,
            FloatingRateLoanCapFloor,
            WSWObligo,
            VATFacility,
            RevolvingCreditFacility,
            DebtServiceReserveFacility,
            TermLoanFacility,
            ExportCreditGuaranteeFloatingRateFixedNotional,
            ExportCreditGuaranteeFloatingRateVariableNotional,
            Miscellaneous,
            Deposit,
        }

        public enum SelectEvent
        {
            Primary,
            Secondary,
            Doorzak,
            InterestReset,
            SpreadReset,
            Restructuring,
            ExtendableOption,
            DrawdownFacility,
            EarlyRepaymentRestructuring,
            EarlyRepaymentMarketValue,
            EarlyRepaymentResetDate,
            EarlyRepaymentDoorzak,
            Miscellaneous,
        }

        public enum TestUser
        {
            AccountDesk1,
            AccountDesk2,
            BackOffice1,
            BackOffice2,
            CreditManagement1,
            CreditManagement2,
            BackOfficeFundingDerivates1,
            BackOfficeFundingDerivates2,
            Treasury1,
            Treasury2,
            TreasuryManager1,
            TreasuryManager2,
            SpecializedFinanceTRX1,
            AccountDesk1PrevRelease,
            AccountDesk2PrevRelease,
            BackOffice1PrevRelease,
            BackOffice2PrevRelease,
        }

        public enum PopupWindow
        {
            SluitbriefNew,
            SluitbriefLoanUpdate,
            SluitbriefDeposito,
            Offertemail,
        }

        public enum CouponDay
        {
            None,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
        }

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

        public enum NotificationMinutes
        {
            Five = 5,
            Ten = 10,
            Fifteen = 15,
            Thirty = 30,
            Sixty = 60,
            Ninety = 90,
        }

        public enum AmortisationType
        {
            Bullet,
            Linear,
            Annuity,
            [Display(Name = "Linear balloon")]
            Linearballoon,
            [Display(Name = "Annuity balloon")]
            Annuityballoon,
            Manual,
            Zero,
        }

        public enum CouponFrequency
        {
            Annual,
            [Display(Name = "Semi annual")]
            Semiannual,
            Quarterly,
            Monthly,
            Weekly,
        }

        public enum SwapNeeded
        {
            True,
            False,
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

        public enum DayCount
        {
            [Display(Name = "Act/Act")]
            ActAct,
            [Display(Name = "Act/360")]
            Act360,
            [Display(Name = "30/360")]
            Thirty360,
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
            SwapCopyFunction,
        }
    }
}