@Long
Feature: EmailContentFixed

    Scenario: TC01A Fixed WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser     | CounterParty                  | Guarantor1 | Notional     | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap | SwapReferenceRate | ClearingHouse | BusinessDayConvention | DayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | A  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 75.293.932,5 | Bullet    |           |             | 07-12-2026 | 07-7-2031    | Weekly     |                 |                | 04-12-2028     | Monday    | True | 6mE               | EUREX         | Mod Foll Unadj        | Act/Act  | 0,5      | 0,75        |            |                | 1,5         | 1,508        |

    

    Scenario: TC02A Fixed Normal Clients | Linear, Annuity etc
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   | DealRemark   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> | <DealRemark> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser     | CounterParty          | Guarantor1 | Notional   | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | SwapReferenceRate | ClearingHouse | BusinessDayConvention | DayCount | SpreadH1 | FinalSpread | DealRemark            | OfferReference      | MarketIRate | CustomerRate |
            | D  | AccountDesk1 | Trx Gemeente Meerdijk |            | 75.293.932 | Linear    | Annual    | 6           | 08-01-2027 | 08-01-2032   | Annual     |                 |                |                | 8         | False |                   |               |                       | Act/Act  | 0,5      | 5           | Deal Remark voor TC D | Offer ref voor TC D | 1           | 1,050        |

    Scenario: TC03 Fixed Double Table |
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And  I select counterparty "Trx Stichting Greendale Wonen" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed case with one offer with two alternatives
        When I press submit an email is generated
        Then the fixed offer email table should include two tables


