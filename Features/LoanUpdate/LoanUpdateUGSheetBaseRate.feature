@LoanUpdate
Feature: LoanUpdateUGSheetBaseRate


    compared to the normal UG sheet test automation, this one implements a ExpectedGuarantor1 property, because this field is already set in the loan update database record.


    Scenario: TC01 BASERATE UG SHEET | Municipality with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a baserate loan update with the following alternative specifics
            | ExpectedAmortType   | StartDate   | MaturityDate   | ExpectedCouponFreq   | ExpectedAmortFreq   | FirstCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | ExpectedDayCount   |
            | <ExpectedAmortType> | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <ExpectedAmortFreq> | <FirstCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <ExpectedDayCount> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEBASERATE" loan type
        Then the values should match the expected values for the BASERATE UGSHEET

        Examples:
            | TC | TestUser     | SecID     | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ReferenceRate | FirstCouponDate | CouponDay | ExpectedCouponFreq | InterestRRDate | ExpectedAmortType | ExpectedAmortFreq | NumbOfAmort | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate   |
            | A  | AccountDesk1 | 1 0027660 | 1 0027660             | Stichting Waarborgfonds Sociale Woningbouw |          | 125125,87        | 17-10-2028 | 01-04-2069   | 12mE          | 19-10-2028      | 1         | Annual             | 01-10-2035     | Bullet            |                   |             | Act/360          | 1        | 1             | 10,00       | 1                | 3.43000000 |
            | B  | AccountDesk1 | 1 0027661 | 1 0027661             | Waarborgfonds voor de Zorgsector           |          | 2768342,00       | 17-10-2028 | 31-10-2069   | 6mE           | 19-10-2028      | 31        | Semi annual        | 31-10-2035     | Linear            | Semi annual       | 99          | Act/Act          | 1        | 1             | 550,00      | 1                | 3.50000000 |
            | C  | AccountDesk1 | 1 0027662 | 1 0027662             | Stichting Waarborgfonds Sociale Woningbouw |          | 125125,00        | 17-10-2028 | 17-05-2069   | 12mE          |                 | 17        | Annual             | 17-10-2035     | Bullet            |                   |             | Act/360          | 1        | 1             | 10,00       | 1                | 4.74200000 |
            | D  | AccountDesk1 | 1 0027664 | 1 0027664             | Waarborgfonds voor de Zorgsector           |          | 2768342,00       | 17-10-2028 | 17-10-2069   | 6mE           |                 | 17        | Semi annual        | 17-10-2035     | Linear            | Semi annual       | 99          | Act/Act ISDA     | 1        | 1             | 550,00      | 1                | 1.10000000 |

    Scenario: TC02 BASERATE UG SHEET | Municipality without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a baserate loan update with the following alternative specifics
            | ExpectedAmortType   | StartDate   | MaturityDate   | ExpectedCouponFreq   | ExpectedAmortFreq   | FirstCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | ExpectedDayCount   |
            | <ExpectedAmortType> | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <ExpectedAmortFreq> | <FirstCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <ExpectedDayCount> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEBASERATE" loan type
        Then the values should match the expected values for the BASERATE UGSHEET

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ReferenceRate | FirstCouponDate | CouponDay | ExpectedCouponFreq | InterestRRDate | ExpectedAmortType | ExpectedAmortFreq | NumbOfAmort | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate   |
            | E  | AccountDesk1 | 1 0027872WZ1 | 1 0027872             |                    |          | 32412,00         | 01-04-2030 | 01-04-2069   | 3mE           | 06-04-2045      | 1         | Quarterly          |                | Annuity           | Quarterly         | 9           | 30/360           | 1        | 1             | 7,59        | 1                | 4.10000000 |
            | F  | AccountDesk1 | 1 0028372WZ1 | 1 0028372             |                    |          | 32412,00         | 03-04-2030 | 03-04-2069   | 3mE           |                 | 3         | Quarterly          |                | Annuity           | Quarterly         | 9           | 30/360           | 1        | 1             | 7,59        | 1                | 3.87000000 |


    @BekijkenVoorVerwijdering
    Scenario: TC03 BASERATE UG SHEET | Waterschap with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Waterschap Groot Waterland" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a baserate loan update with the following alternative specifics
            | ExpectedAmortType   | StartDate   | MaturityDate   | ExpectedCouponFreq   | ExpectedAmortFreq   | ReferenceRate   | FirstCouponDate   | InterestRRDate   | CouponDay   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | ExpectedDayCount   |
            | <ExpectedAmortType> | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <ExpectedAmortFreq> | <ReferenceRate> | <FirstCouponDate> | <InterestRRDate> | <CouponDay> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <ExpectedDayCount> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEBASERATE" loan type
        Then the values should match the expected values for the BASERATE UGSHEET

        Examples:
            | TC | TestUser  | SecID          | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ReferenceRate | FirstCouponDate | CouponDay | ExpectedCouponFreq | InterestRRDate | ExpectedAmortType | ExpectedAmortFreq | ExpectedNumbOfAmort | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate   |
            | G  | Treasury1 | 1 0029854TR1   | 1 0029854             | Stichting Waarborgfonds Sociale Woningbouw |          | 4663,45          | 01-06-2028 | 19-06-2087   | 1mE           | 21-10-2030      | 19        | Monthly            | 19-05-2050     | Annuity balloon   | Monthly           | 708                 | Act/360          | 1        | 1             | 98,75       | 1                | 0.00000000 |
            | H  | Treasury1 | 1 0029897RH    | 1 0029897             | Waarborgfonds voor de Zorgsector           |          | 573223,00        | 15-06-2028 | 19-06-2087   | 1wE           | 21-10-2030      | Thursday  | Weekly             | 12-05-2050     | Manual            | Weekly            | 95                  | 30/360           | 1        | 1             | 127,95      | 1                | 3.55000000 |
            | I  | Treasury1 | 1 0030001RHWZ1 | 1 0030001             | Stichting Waarborgfonds Sociale Woningbouw |          | 4663,45          | 19-06-2028 | 19-06-2087   | 1mE           |                 | 19        | Monthly            | 19-05-2050     | Linear balloon    | Monthly           | 708                 | Act/360          | 1        | 1             | 98,75       | 1                | 3.55000000 |
            | J  | Treasury1 | 1 0030108      | 1 0030108             | Trx Dunder Mifflin Paper Company           |          | 573223,00        | 15-06-2028 | 19-06-2087   | 1wE           |                 | Thursday  | Weekly             | 12-05-2050     | Manual            | Weekly            | 95                  | 30/360           | 1        | 1             | 127,95      | 1                | 4.10000000 |

    @BekijkenVoorVerwijdering
    Scenario: TC04 BASERATE UG SHEET | Waterschap without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Dunder Mifflin Paper Company" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractEndDate   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractEndDate> |
        And I create a baserate loan update with the following alternative specifics
            | ExpectedAmortType   | StartDate   | MaturityDate   | ExpectedCouponFreq   | ExpectedAmortFreq   | FirstCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | ExpectedDayCount   |
            | <ExpectedAmortType> | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <ExpectedAmortFreq> | <FirstCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <ExpectedDayCount> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEBASERATE" loan type
        Then the values should match the expected values for the BASERATE UGSHEET

        Examples:
            | TC | TestUser  | SecID     | ExpectedOriginalSecID | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ReferenceRate | FirstCouponDate | CouponDay | ExpectedCouponFreq | InterestRRDate | ExpectedAmortType | NumbOfAmort | ExpectedAmortFreq | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate   |
            | K  | Treasury1 | 1 0030109 | 1 0030109             |                    |          | 623423,00        | 01-03-2027 | 08-04-2069   | 1wE           |                 | Monday    | Weekly             |                | Zero              | 96          | Weekly            | 30/360           | 1        | 1             | 15,60       | 1                | 4.74200000 |
