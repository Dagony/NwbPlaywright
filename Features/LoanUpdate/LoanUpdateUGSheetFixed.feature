@LoanUpdate
Feature: LoanUpdateUGSheetFixed

    Compared to the normal UG sheet test automation, this one implements a ExpectedGuarantor1 property, because this field is already set in the loan update database record.

    Scenario: TC01 FIXED UG SHEET | Municipality with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEFIXED" loan type
        Then the values should match the expected values for the FIXED UGSHEET

        Examples:
            | TC | TestUser     | SecID       | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | CouponFreq  | InterestRRDate | AmortType | AmortFreq   | NumbOfAmort | DayCount     | SpreadH1 | FinalSpread | MarketIRate | DealRemark           |
            | A  | AccountDesk1 | 1 0028100   | 1 0028100             | Stichting Waarborgfonds Sociale Woningbouw |          | 125125,87        | 17-10-2028 | 01-04-2069   | 19-10-2028      | 1         | Annual      | 01-10-2035     | Bullet    |             |             | Act/360      | 1        | 10          | -1          | Deal Remark for TC A |
            | B  | AccountDesk1 | 1 0028101   | 1 0028101             | Waarborgfonds voor de Zorgsector           |          | 2768342,00       | 17-10-2028 | 31-10-2069   | 19-10-2028      | 31        | Semi annual | 31-10-2035     | Linear    | Semi annual | 99          | Act/Act      | 1        | 550         | 2,5         |                      |
            | C  | AccountDesk1 | 1 0028118   | 1 0028118             | Stichting Waarborgfonds Sociale Woningbouw |          | 125125,00        | 17-10-2028 | 17-10-2069   |                 | 17        | Annual      | 17-10-2035     | Bullet    |             |             | Act/360      | 1        | 10          | 1           | Deal Remark for TC C |
            | D  | AccountDesk1 | 1W0025262RH | 1 0025262             | Waarborgfonds voor de Zorgsector           |          | 2768342,00       | 17-10-2028 | 17-10-2069   |                 | 17        | Semi annual | 17-10-2035     | Linear    | Semi annual | 99          | Act/Act ISDA | 1        | 550         | 2,5         |                      |


    Scenario: TC02 FIXED UG SHEET | Municipality without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEFIXED" loan type
        Then the values should match the expected values for the FIXED UGSHEET

        Examples:
            | TC | TestUser     | SecID     | ExpectedOriginalSecID | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | CouponFreq | InterestRRDate | AmortType | AmortFreq | NumbOfAmort | DayCount | SpreadH1 | FinalSpread | MarketIRate | DealRemark           |
            | E  | AccountDesk1 | 1 0028706 | 1 0028706             |                    |          | 32412,00         | 03-03-2027 | 01-04-2069   | 06-04-2045      | 1         | Quarterly  |                | Annuity   | Quarterly | 9           | 30/360   | 1        | 7,59        | 3           | Deal Remark for TC E |
            | F  | AccountDesk1 | 1 0028709 | 1 0028709             |                    |          | 32412,00         | 03-03-2027 | 03-06-2069   |                 | 3         | Quarterly  |                | Annuity   | Quarterly | 9           | 30/360   | 1        | 7,59        | 3           | Deal Remark for TC F |

