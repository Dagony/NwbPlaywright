@LoanUpdate @CsvFiles
Feature: LoanUpdateCsvFilesBaseRate

    As a user I each loan type to create the SECM, SECMNWB and BRED CSV file with the right output data for a loan update baserate type.

    Scenario: TC01A CSV files output for BaseRate loan update type with basic counterparty, no guarantor, and StartDate-CurrentDate is longer than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Interest reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | OfferReference   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <OfferReference> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | ExpectedAmortType   | ExpectedAmortFreq   | ExpectedNumbOfAmort   | DayCount   | ExpectedDayCount   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | BaseRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <ExpectedAmortType> | <ExpectedAmortFreq> | <ExpectedNumbOfAmort> | <DayCount> | <ExpectedDayCount> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <BaseRate> | <CustomerRate> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM baserate loan case is valid
        And the assertions for each testcase for SECMNWB baserate loan case is valid
        And the assertions for each testcase for BRED baserate loan case is valid
        # TODO: Check that I indeed needed to delete the Guarantor2 values for the first three lines
        Examples:
            | TC | TestUser     | SecID        | Notional | ExpectedNotional | Guarantor2 | GuarantorRefNumber | CounterParty                        | StartDate | MaturityDate | ExpectedAmortType | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedCouponFreq | ExpectedDayCount | ExpectedNumbOfAmort | ExpectedAmortFreq | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | BaseRate |
            | N  | AccountDesk1 | 1 0027616    |          | 500000           |            | 43801              | Trx Stichting Greendale Wonen       | 31-8-2027 | 31-8-2038    | Bullet            |                 |                |                | 31        | Annual             | Act/Act          |                     |                   | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            | 3,83     |
            | O  | AccountDesk1 | 1 0027619    |          | 150000           |            | 43799              | Trx Stichting Greendale Wonen       | 31-8-2027 | 31-8-2038    | Linear            | 01-9-2027       | 27-8-2029      | 30-8-2029      | 31        | Semi annual        | Act/360          | 10                  | Semi annual       | 6mE           | -22      |               | -5,75       |                  | 4,10     |
            | P  | AccountDesk1 | 1 0027620    |          | 5254175,95       |            | 43808              | Trx Stichting Greendale Wonen       | 31-8-2027 | 31-8-2038    | Annuity           | 01-9-2027       |                | 31-8-2029      | 31        | Annual             | EBOND            | 20                  | Annual            | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            | 3,87     |
