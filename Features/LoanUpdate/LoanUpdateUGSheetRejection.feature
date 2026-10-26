@LoanUpdate
Feature: LoanUpdateUGSheetRejection


    Scenario: TC A FIXED REJECTION UG SHEET | WSW with Advisory
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

    @BekijkenVoorVerwijdering
    Scenario: TC B VARIABLE REJECTION UG SHEET | WFZ with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | ExpectedDayCount   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <ExpectedDayCount> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <DealRemark> |
        When I set the case to lost and continue to rejection resolved stage with rejection comment "<RejectionComment>"
        And I click the production sheet button and insert the string into an excel file for a rejected loan
        Then the values should match the expected values for loantype "LOANUPDATEVARIABLE" for the REJECTION UGSHEET

        Examples:
            | TC | TestUser  | SecID     | ExpectedOriginalSecID | CounterParty                        | ExpectedGuarantor1               | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedContractEndDate | FirstCouponDate | LastCouponDate | InterestSpreadRRDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | RejectionComment | DealRemark |
            | B  | Treasury1 | 2 0031407 | 2 0031407             | Trx Stichting Zorggroep Heilig Hart | Waarborgfonds voor de Zorgsector |          | 50100,00         | 01-12-2027 | 01-12-2045   | 01-12-2045              | 02-12-2027      | 30-11-2037     | 03-12-2037           | Wednesday | 1wE           | True        | False                  | 5,75              |               | Act/360          | 1        | 1             | 1,25        | -0,25            | 0.001        |                  |            |

    @BekijkenVoorVerwijdering
    Scenario: TC C VARIABLE REJECTION UG SHEET | WFZ with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan fixed notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | ExpectedDayCount   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <ExpectedDayCount> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <DealRemark> |
        When I set the case to lost and continue to rejection resolved stage with rejection comment "<RejectionComment>"
        And I click the production sheet button and insert the string into an excel file for a rejected loan
        Then the values should match the expected values for loantype "LOANUPDATEVARIABLE" for the REJECTION UGSHEET

        Examples:
            | TC | TestUser  | SecID        | ExpectedOriginalSecID | CounterParty                   | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedContractEndDate | FirstCouponDate | LastCouponDate | InterestSpreadRRDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | RejectionComment | DealRemark           |
            | C  | Treasury1 | 1W0027058RH4 | 1 0027058             | Trx Waterschap Groot Waterland |                    |          | 100000000,00     | 09-11-2028 | 09-8-2034    | 09-08-2034              |                 | 07-8-2034      |                      | 09        | 12mE          |             | True                   |                   |               | Act/360          | 1        | 1             | -2,842      | 2,42             | -0.004       | Reject for TC C  | Deal Remark for TC C |

    Scenario: TC D BASERATE REJECTION UG SHEET | Municipality and normal client without Advisory
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

    @BekijkenVoorVerwijdering
    Scenario: TC E BASERATE REJECTION UG SHEET | Municipality and normal client without Advisory
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
            | TC | TestUser  | SecID        | ExpectedOriginalSecID | CounterParty                     | ExpectedGuarantor1    | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedContractEndDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | RejectionComment | OfferReference      | BaseRate |
            | E  | Treasury1 | 1W0027040RH4 | 1 0027040             | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk |          | 3746249,98       | 15-06-2028 | 15-06-2088   | 15-06-2088              | Monthly            |                 |                |                | 15        | A365             | 3mE           | -22      | -22           | 4,33        | 55,55            |                  | Offer ref voor TC E | 3,43     |


