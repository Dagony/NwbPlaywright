@Long
Feature: CsvFiles Fixed

    As a user I each loan type to create the SECM, SECMNWB and BRED CSV file with the right output data for Fixed loan type.
    Two test cases. Entire test design can be found in sprint 34 for Fixed and Variable loan csv files.

    @CsvFiles
    Scenario: TC03A CSV files output for Fixed loan type with basic counterparty, no guarantor, and StartDate-CurrentDate is longer than 5 workdays

        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | ExpectedNotional   |
            | <TC> | <CounterParty> | <Notional> | <ExpectedNotional> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> |
        When I continue the case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM fixed loan case is valid
        And the assertions for each testcase for SECMNWB fixed loan case is valid
        And the assertions for each testcase for BRED fixed loan case is valid
        Examples:
            | TC | TestUser     | Notional | ExpectedNotional | CounterParty                  | StartDate | MaturityDate | AmortType | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq  | DayCount | NumbOfAmort | AmortFreq   | SpreadH1 | FinalSpread | MarketIRate |
            | G  | AccountDesk1 | 1500000  |                  | Trx Stichting Greendale Wonen | 31-8-2027 | 31-8-2039    | Bullet    |                 |                |                | 31        | Annual      | Act/Act  |             |             | 1        | -101        | 1           |
            | H  | AccountDesk1 | 350000   |                  | Trx Stichting Greendale Wonen | 31-8-2027 | 31-8-2038    | Linear    | 01-9-2027       | 27-8-2029      | 30-8-2029      | 31        | Annual      | Act/360  | 12          | Annual      | 1        | 6           | 2           |
            | I  | AccountDesk1 | 10492032 |                  | Trx Stichting Greendale Wonen | 31-8-2027 | 31-8-2050    | Annuity   | 01-9-2027       |                | 31-8-2029      | 31        | Semi annual | 30/360   | 24          | Semi annual | 1        | 7,1         | 3           |