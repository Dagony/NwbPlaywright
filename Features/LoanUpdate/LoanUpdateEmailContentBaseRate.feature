@LoanUpdate
Feature: LoanUpdateEmailContentBaseRate

    Scenario: TC01A BASERATE WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | MinimalWithdrawal   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <MinimalWithdrawal> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | A  | AccountDesk1 | 1 0027767    | 1 0027767             | Trx Stichting Greendale Wonen       | Stichting Waarborgfonds Sociale Woningbouw |          | 20.000.000,00    | 01-03-2027 | 16-06-2087   | Weekly             | 06-01-2028      |                | 02-12-2030     | Monday    | ACT365           | 1wE           | 0,5      | 0,5           | 0,75        | 0,75             |                       |                     | 3,545    |

    Scenario: TC02A BASERATE NORMAL clients | Non Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser     | SecID         | ExpectedOriginalSecID | CounterParty                     | ExpectedGuarantor1    | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | D  | AccountDesk1 | 1W0026221RH4  | 1 0026221             | Trx Gemeente Meerdijk            |                       |          | 6.875.000,44     | 15-06-2028 | 15-06-2088   | Annual             |                 |                |                | 16        | Act/act          | 1mE           | 1        | 1             | 10          | 10               | Deal Remark voor TC D | Offer ref voor TC D | 1,1      |
