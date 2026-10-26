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

    @BekijkenVoorVerwijdering
    Scenario: TC01B Fixed WSW WFZ Clients | Bullet
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
            | TC | TestUser  | CounterParty                        | Guarantor1 | Notional   | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | SwapReferenceRate | ClearingHouse | BusinessDayConvention | DayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | B  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 100.200    | Bullet    |           |             | 07-12-2026 | 01-12-2045   | Weekly     | 08-12-2026      | 10-12-2026     | 03-12-2035     | Wednesday | False |                   |               |                       | Act/360  | 1,25     | 85          |            |                | 3           | 3,850        |
            | C  | Treasury1 | Trx Dunder Mifflin Paper Company    |            | 37.200.958 | Bullet    |           |             | 07-12-2026 | 01-12-2045   | Annual     | 08-12-2026      | 10-12-2026     |                | 22        | False |                   |               |                       | 30/360   | 33       | 53          |            |                | 1           | 1,530        |


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

    @BekijkenVoorVerwijdering
    Scenario: TC02B Fixed Normal Clients | Linear, Annuity etc
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
            | TC | TestUser  | CounterParty                     | Guarantor1            | Notional      | AmortType       | AmortFreq   | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | SwapReferenceRate | ClearingHouse | BusinessDayConvention | DayCount | SpreadH1 | FinalSpread | DealRemark            | OfferReference      | MarketIRate | CustomerRate |
            | E  | Treasury1 | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk | 2.333.500     | Annuity         | Monthly     | 1           | 19-01-2027 | 19-02-2031   | Monthly    |                 |                |                | 19        | True  | 3mE               | LCH           | Foll Unadj            | Act/360  | 1,25     | 15,75       | Deal Remark voor TC E | Offer ref voor TC E | 1,5         | 1,658        |
            | F  | Treasury1 | Trx Dunder Mifflin Paper Company |                       | 99.999.999,99 | Linear balloon  | Quarterly   | 268         | 09-01-2029 | 19-02-2093   | Quarterly  | 30-01-2029      |                |                | 19        | False |                   |               |                       | 30/360   | -5,75    | -5,75       |                       | Offer ref voor TC F | 3           | 2,942        |
            | G  | Treasury1 | Trx Waterschap Groot Waterland   |                       | 100.000.000   | Annuity balloon | Quarterly   | 268         | 19-01-2027 | 20-02-2093   | Quarterly  |                 | 20-02-2027     |                | 19        | False |                   |               |                       | Act/Act  | -6       | -5          |                       | Offer ref voor TC G | 4,12        | 4,070        |
            | H  | Treasury1 | Trx Dunder Mifflin Paper Company |                       | 32.759        | Manual          | Semi annual | 64          | 19-01-2027 | 19-02-2058   | Monthly    |                 |                | 19-02-2036     | 19        | False |                   |               |                       | Act/360  | 0,5      | -50         |                       | Offer ref voor TC H | 0,05        | -0,450       |
            | I  | Treasury1 | Trx Dunder Mifflin Paper Company |                       | 45.555.958    | Zero            | Weekly      | 1792        | 18-01-2027 | 19-02-2057   | Weekly     |                 |                |                | Monday    | False |                   |               |                       | 30/360   | 1,25     | 25          |                       | Offer ref voor TC I | -0,1        | 0,150        |

    Scenario: TC03 Fixed Double Table |
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And  I select counterparty "Trx Stichting Greendale Wonen" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed case with one offer with two alternatives
        When I press submit an email is generated
        Then the fixed offer email table should include two tables


