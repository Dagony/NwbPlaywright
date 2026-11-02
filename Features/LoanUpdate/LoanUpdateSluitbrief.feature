@LoanUpdate
Feature: LoanUpdateSluitbrief

    Scenario: TC01 Sluitbrief for a loan update fixed - No advisors
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | SwapReferenceRate | ClearingHouse | BusinessDayConvention | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | 3mE               | LCH           | Mod Foll Unadj        | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval for the new PEGA class structure
        And I open the sluitbrief
        #Then the displayed sluitbrief for fixed loan update should match the expected table
        Then the displayed sluitbrief for fixed loan should match the expected table

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | ExpectedDayCount | SpreadH1 | FinalSpread | DealRemark                                     | OfferReference | MarketIRate | CustomerRate |
            | A  | AccountDesk1    | 1 0027588    | 1 0027588             | Stichting Waarborgfonds Sociale Woningbouw |          | 573.223,95       | Bullet    | Annual    |             | 19-06-2028 | 19-06-2087   | Annual     |                 |                |                | 19        | False | Act/365          | 0,5      | -50         |                                                |                | 0,3         | -0,20        |
            | B  | AccountDesk1 | 1W0014249RH2 | 1 0014249             |                                            |          | 623.423          | Linear    | Weekly    | 15          | 03-03-2027 | 10-04-2069   | Weekly     | 05-03-2027      | 04-12-2035     | 05-12-2035     | Wednesday | True  | Act/360          | 1,25     | 85          | This is a test deal remark for tc B Sluitbrief |                | 3           | 3,850        |

    Scenario: TC02 BASERATE - Municipality without Advisory
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
        When I continue the case to BO first flow LoanDataApproval for the new PEGA class structure
        And I open the sluitbrief
        #Then the displayed sluitbrief for baserate loan update should match the expected table

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ReferenceRate | FirstCouponDate | CouponDay | ExpectedCouponFreq | InterestRRDate | ExpectedAmortType | ExpectedAmortFreq | NumbOfAmort | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate |
            | E  | AccountDesk1 | 1 0027872WZ1 | 1 0027872             |                    |          | 132412,55        | 01-04-2030 | 01-04-2069   | 3mE           | 06-04-2045      | 1         | Quarterly          |                | Annuity           | Quarterly         | 9           | 30/360           | 1        | 1             | 7,59        | 1                | 4.1000   |
