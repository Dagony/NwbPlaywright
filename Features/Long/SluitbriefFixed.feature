@Long
Feature: SluitbriefFixed

    A short summary of the feature


    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case



    Scenario: TC01 FIXED Sluitbrief with Advisory
        Given I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | Guarantor1   | Notional   | OfferReference   | CommentForBO   |
            | <TC> | <Guarantor1> | <Notional> | <OfferReference> | <CommentForBO> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval
        And I open the sluitbrief
        Then the displayed sluitbrief for fixed loan should match the expected table
        Examples:
            | TC | TestUser     | Guarantor1                                 | Notional     | StartDate  | MaturityDate | FirstCouponDate | Swap  | SwapReferenceRate | ClearingHouse | BusinessDayConvention | CouponDay | CouponFreq  | InterestRRDate | AmortType | AmortFreq   | NumbOfAmort | DayCount | SpreadH1 | FinalSpread | MarketIRate | DealRemark           | CommentForBO |
            | A  | AccountDesk1 | Stichting Waarborgfonds Sociale Woningbouw | 125.125,87   | 17-10-2028 | 01-04-2069   | 19-10-2028      | True  | 3mE               | LCH           | Foll Unadj            | 1         | Annual      | 01-10-2035     | Bullet    |             |             | Act/360  | 1        | 10          | -1          | Deal Remark for TC A |              |
            | B  | AccountDesk1 |                                            | 2.768.342,00 | 17-10-2028 | 31-10-2069   | 19-10-2028      | False |                   |               |                       | 31        | Semi annual | 31-10-2035     | Linear    | Semi annual | 99          | Act/Act  | 1        | 550         | 2,5         |                      |              |



    Scenario: TC02 FIXED Sluitbrief without Advisory
        Given I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | Guarantor1   | Notional   | OfferReference   | CommentForBO   |
            | <TC> | <Guarantor1> | <Notional> | <OfferReference> | <CommentForBO> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | CouponDay   | Swap   | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <CouponDay> | <Swap> | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval
        And I open the sluitbrief
        Then the displayed sluitbrief for fixed loan should match the expected table
        Examples:
            | TC | TestUser     | Guarantor1                       | Notional  | StartDate  | MaturityDate | FirstCouponDate | Swap  | SwapReferenceRate | ClearingHouse | BusinessDayConvention | CouponDay | CouponFreq | InterestRRDate | AmortType | AmortFreq | NumbOfAmort | DayCount | SpreadH1 | FinalSpread | MarketIRate | DealRemark           | CommentForBO |
            | C  | AccountDesk1 | Waarborgfonds voor de Zorgsector | 32.412,00 | 03-03-2027 | 01-04-2069   | 06-04-2045      | True  | 6mE               | EUREX         | Foll Adj              | 1         | Quarterly  |                | Annuity   | Quarterly | 9           | 30/360   | 1        | 7,59        | 3           | Deal Remark for TC C |              |
            | D  | AccountDesk1 |                                  | 32.412,55 | 03-03-2027 | 03-06-2069   |                 | False |                   |               |                       | 3         | Quarterly  |                | Manual    | Quarterly | 9           | 30/360   | 1        | 7,59        | 3           |                      |              |



