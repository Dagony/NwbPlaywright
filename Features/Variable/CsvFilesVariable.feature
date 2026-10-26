@Variable
Feature: CsvFiles Variable loan type

    As a user I each loan type to create the SECM, SECMNWB and BRED CSV file with the right output data.

    Two test cases. Entire testontwerp can be found in sprint 34 for Fixed and Variable loan csv files.

    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case


    @CsvFiles
    Scenario: TC01 CSV files output for Variable loan type with waterschap counterparty, variable notional type, WSW guarantor and StartDate-CurrentDate is longer than 5 workdays - PF
        Given I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid
        Examples:
            | TC | TestUser     | Notional | ExpectedNotional | CounterParty                  | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | A  | AccountDesk1 | 77777,42 |                  | Trx Stichting Greendale Wonen |            | true                 | false                  | 10,50             | 10            | 18-12-2026 | 26-06-2048   | 1wE           | true        | 21-12-2026      |                | 11-06-2038     | Friday    | A360             | 10       | 10            | 25               | 25          | 0.50         |

    @CsvFiles @BekijkenVoorVerwijdering
    Scenario: TC01 CSV files output for Variable loan type with waterschap counterparty, variable notional type, WSW guarantor and StartDate-CurrentDate is longer than 5 workdays - TRY
        Given I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid
        Examples:
            | TC | TestUser  | Notional   | ExpectedNotional | CounterParty                        | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | B  | Treasury1 | 1500000,00 |                  | Trx Stichting Zorggroep Heilig Hart |            | false                | false                  | 25,55             | 20            | 19-06-2027 | 09-03-2033   | 1mE           |             | 22-6-2027       | 30-8-2029      | 31-8-2029      | 15        | A360             | 2        | 2             | -2               | -2          | -0.04        |
            | C  | Treasury1 | 350000     |                  | Trx Waterschap Groot Waterland      |            | false                | false                  | 50                |               | 01-06-2027 | 01-12-2032   | 3mE           |             |                 |                |                | 1         | A360             | 3        | 3             | 3                | 3,868       | 0.069        |


    @CsvFiles
    Scenario: TC02 CSV files output for Variable loan type with basic counterparty, no guarantor, and StartDate-CurrentDate is shorter than 5 workdays - PF
        Given I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid
        Examples:
            | TC | TestUser     | Notional | ExpectedNotional | CounterParty                  | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | StartDate | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | D  | AccountDesk1 | 10492032 |                  | Trx Stichting Greendale Wonen |            | true                 | true                   | today     | 21-8-2051    | 6mE           |             |                 | 27-8-2049      |                | 21        | A360             | 4        |               |                  | 4,92        | 0.05         |


    @CsvFiles @BekijkenVoorVerwijdering
    Scenario: TC02 CSV files output for Variable loan type with basic counterparty, no guarantor, and StartDate-CurrentDate is shorter than 5 workdays - TRY
        Given I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid
        Examples:
            | TC | TestUser  | Notional  | ExpectedNotional | CounterParty                        | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | StartDate | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | E  | Treasury1 | 53392,48  |                  | Trx Stichting Zorggroep Heilig Hart |            | false                | true                   | today     | 30-8-2034    | 12mE          |             |                 | 07-8-2034      |                | 30        | A360             | 5        | 5             | 2,42             | -2,842      | -0.004       |
            | F  | Treasury1 | 100000000 |                  | Trx Waterschap Groot Waterland      |            | false                | true                   | today     | 09-8-2034    | 12mE          |             |                 | 07-8-2034      |                | 30        | A360             | 50       | 50            | 50               | 55,55       | 1.06         |


