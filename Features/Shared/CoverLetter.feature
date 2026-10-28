@Shared
Feature: CoverLetter

    Scenario: TC01A VAR variable hoofdsom Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And I download the "Cover Letter" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser | CounterParty                        | Notional  | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | A  | AccountDesk1 | Trx Stichting Greendale Wonen       | 77777,42  | false                  | 10,50             | 10            | 19-06-2027 | 26-06-2049   | 1wE           | true        | 28-8-2027       |                | 10-06-2039     | Friday    | A360             | 1        | 1             | 1                | 1           | 0.02         |

    Scenario: TC02A VAR fixed hoofdsom Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And I download the "Cover Letter" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser     | CounterParty                        | Notional | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | C  | AccountDesk1 | Trx Stichting Greendale Wonen       | 95753,00 | true                   |                   |               | 30-08-2028 | 30-08-2038   | 12mE          |             |                 |                |                | 30        | A360             | 25,75    | 25,75         | 25,75            | 25,75       | 0.515        |


    Scenario: TC04A Fixed Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | Notional   | OfferReference   | CommentForBO   |
            | <TC> | <Notional> | <OfferReference> | <CommentForBO> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval
        And I download the "Cover Letter" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser     | Notional   | CounterParty                        | StartDate | MaturityDate | AmortType      | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq  | DayCount | NumbOfAmort | AmortFreq | SpreadH1 | FinalSpread | MarketIRate |
            | F  | AccountDesk1 | 5254175,95 | Trx Stichting Greendale Wonen       | 31-8-2027 | 30-8-2039    | Annuity        | 01-9-2027       |                | 31-8-2029      | 31        | Semi annual | Act/360  | 20          | Annual    | 1        | 7,1         | -3,123      |
