@LoanUpdate
Feature: LoanUpdateUGSheetRejection


    Scenario: TC A FIXED REJECTION UG SHEET - WSW with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Greendale Wonen" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I set the case to lost and continue to rejection resolved stage with rejection comment "<RejectionComment>"
        And I click the production sheet button and insert the string into an excel file for a rejected loan
        Then the values should match the expected values for loantype "LOANUPDATEFIXED" for the REJECTION UGSHEET

        Examples:
            | TC | TestUser     | SecID       | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedContractEndDate | FirstCouponDate | Swap  | CouponDay | CouponFreq | InterestRRDate | AmortType | AmortFreq | NumbOfAmort | DayCount | SpreadH1 | FinalSpread | MarketIRate | RejectionComment | DealRemark           |
            | A  | AccountDesk1 | 1 0027561RH | 1 0027561             | Stichting Waarborgfonds Sociale Woningbouw |          | 70000000,00      | 01-12-2031 | 06-07-2037   | 08-07-2037              |                 | False | Monday    | Weekly     | 04-12-2034     | Bullet    |           |             | ACT365   | 1        | 0,75        | 1,5         |                  | Dealremark voor TC A |

    Scenario: TC D BASERATE REJECTION UG SHEET - Municipality and normal client without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <ExpectedDayCount> |
        When I set the case to lost and continue to rejection resolved stage with rejection comment "<RejectionComment>"
        And I click the production sheet button and insert the string into an excel file for a rejected loan
        Then the values should match the expected values for loantype "LOANUPDATEBASERATE" for the REJECTION UGSHEET

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | CounterParty          | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedContractEndDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | RejectionComment      | OfferReference      | BaseRate |
            | D  | AccountDesk1 | 1W0026221RH4 | 1 0026221             | Trx Gemeente Meerdijk |                    |          | 6875000,44       | 15-06-2028 | 15-06-2088   | 15-06-2088              | Annual             |                 |                |                | 15        | Act/act          | 1mE           | 1        | 1             | 10          | 10               | Deal Remark voor TC D | Offer ref voor TC D | 1,1      |
