@Variable
Feature: UGsheetVariable
    For technical details about the columns see FO Variable UGsheet loan document.

    Scenario: TC01 VARIABLE UG SHEET - Municipality with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1                                 | Notional   | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | A  | AccountDesk1 | Stichting Waarborgfonds Sociale Woningbouw | 125125,87  | 17-10-2028 | 01-04-2069   | 19-10-2028      | Monday    | 1wE           | True        | False                  | 15,00             | 33,75         | 17-10-2035           | 1        | 1             | 10,00       | 10               | 20           | Deal Remark for TC A |
            | B  | AccountDesk1 | Waarborgfonds voor de Zorgsector           | 2768342,00 | 17-10-2028 | 31-10-2069   | 19-10-2028      | 31        | 1mE           |             | False                  | 25,55             |               | 17-10-2035           | 1        | 1             | 540,00      | -547,5           | -7,5         |                      |

    Scenario: TC02 VARIABLE UG SHEET - Municipality without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1 | Notional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | C  | AccountDesk1 |            | 32412,00 | 03-03-2027 | 01-04-2069   | 06-04-2045      | 1         | 3mE           | False                  | 55                |               | 5,78                 | 1        | 1             | -4,59       | 7,59             | 3            | Deal Remark for TC C |
