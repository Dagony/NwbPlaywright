@Swaps
Feature: SwapCopyFunction

    A short summary of the feature

    Scenario: TC01 SWAP COPY LOANUPDATE - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | ExpectedOriginalSecID   |
            | <TC> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | True | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <CustomerRate> |
        When I click the Swap Copy button and copy the string for "Traditional fixed rate loan long - Interest reset" loan type
        Then the values should match the expected values for the Swap Copy string

        Examples:
            | TC     | TestUser     | SecID     | ExpectedOriginalSecID | CounterParty          | Notional | ExpectedNotional | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | SwapReferenceRate | ClearingHouse | BusinessDayConvention | CouponDay | CouponFreq  | InterestRRDate | AmortType | AmortFreq | NumbOfAmort | ExpectedDayCount | SpreadH1 | FinalSpread | MarketIRate |
            | A (LU) | AccountDesk1 | 1 0027545 | 1 0027545             | Trx Gemeente Meerdijk |          | 105000000        | 03-12-2026 | 03-12-2037   | 04-12-2027      | 02-12-2034     | 3mE               | LCH           | Mod Foll Unadj        | 3         | Annual      | 01-10-2035     | Bullet    |           |             | Act/Act          | 1        | 1           | 1           |
            | B (LU) | AccountDesk1 | 1 0027546 | 1 0027546             | Trx Gemeente Meerdijk |          | 55000000         | 03-12-2026 | 03-12-2037   | 04-12-2027      | 02-12-2034     | 3mE               | LCH           | Mod Foll Adj          | 3         | Semi annual | 31-10-2035     | Bullet    |           |             | Act/360          | 1        | 1           | 2           |

    Scenario: TC02 SWAP COPY NORMAL - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | Notional   |
            | <TC> | <Notional> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap | SwapReferenceRate   | ClearingHouse   | BusinessDayConvention   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | True | <SwapReferenceRate> | <ClearingHouse> | <BusinessDayConvention> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I click the Swap Copy button and copy the string for "Traditional fixed rate loan long - Primary" loan type
        Then the values should match the expected values for the Swap Copy string

        Examples:
            | TC | TestUser     | CounterParty          | Notional  | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | SwapReferenceRate | ClearingHouse | BusinessDayConvention | CouponDay | CouponFreq  | InterestRRDate | AmortType | AmortFreq | NumbOfAmort | DayCount | SpreadH1 | FinalSpread | MarketIRate |
            | A  | AccountDesk1 | Trx Gemeente Meerdijk | 105000000 | 03-12-2026 | 03-12-2037   | 04-12-2026      | 02-12-2034     | 3mE               | LCH           | Mod Foll Unadj        | 3         | Annual      | 01-10-2035     | Bullet    |           |             | Act/Act  | 1        | 10          | -1          |
            | B  | AccountDesk1 | Trx Gemeente Meerdijk | 55000000  | 03-12-2026 | 03-12-2037   | 04-12-2026      | 02-12-2034     | 3mE               | LCH           | Mod Foll Adj          | 3         | Semi annual | 31-10-2035     | Bullet    |           |             | Act/360  | 1        | 550         | 2,5         |