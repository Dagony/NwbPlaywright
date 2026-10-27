@LoanUpdate
Feature: LoanUpdateCsvFilesFixed


    As a user I each loan type to create the SECM, SECMNWB and BRED CSV file with the right output data for a loan update Fixed type.

    @CsvFiles
    Scenario: TC01A CSV files output for Fixed loan update type with basic counterparty, no guarantor, and StartDate-CurrentDate is longer than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | OfferReference   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <OfferReference> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <CustomerRate> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM fixed loan case is valid
        And the assertions for each testcase for SECMNWB fixed loan case is valid
        And the assertions for each testcase for BRED fixed loan case is valid

        Examples:
            | TC | TestUser     | SecID           | Notional | ExpectedNotional | Guarantor2 | GuarantorRefNumber | CounterParty                  | StartDate | MaturityDate | AmortType | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq  | DayCount | NumbOfAmort | AmortFreq   | SpreadH1 | FinalSpread | MarketIRate |
            | G  | AccountDesk1 | 1W0026161RH7WZ1 |          | 500000,00        |            | 35359              | Trx Stichting Greendale Wonen | 31-8-2027 | 31-8-2038    | Bullet    |                 |                |                | 31        | Annual      | ACT365   |             | Annual      | 1        | -101        | 1           |
            | H  | AccountDesk1 | 1W0026162RH5    |          | 150000           |            | 35584              | Trx Stichting Greendale Wonen | 31-8-2027 | 31-8-2038    | Linear    | 01-9-2027       | 27-8-2029      | 30-8-2029      | 31        | Annual      | A365     | 10          | Semi annual | 1        | 6           | 2           |
            | I  | AccountDesk1 | 1W0026170RH9B   |          | 5254175,95       |            | 35651              | Trx Stichting Greendale Wonen | 28-8-2027 | 31-8-2038    | Annuity   | 01-9-2027       |                | 31-8-2029      | 31        | Semi annual | EBOND    | 20          | Annual      | 1        | 7,1         | 3           |

    