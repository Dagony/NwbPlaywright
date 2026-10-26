@LoanUpdate
Feature: LoanUpdateCsvFilesVariable

    As a user I each loan type to create the SECM, SECMNWB and BRED CSV file with the right output data.


    @CsvFiles
    Scenario: TC01A CSV files output for Variable loan update type with waterschap counterparty, variable notional type, WSW guarantor and StartDate-CurrentDate is shorter than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid

        Examples:
            | TC | TestUser     | SecID     | Notional | ExpectedNotional | CounterParty                  | Guarantor1 | Guarantor2 | GuarantorRefNumber | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | A  | AccountDesk1 | 2 0029698 |          | 77777,42         | Trx Stichting Greendale Wonen |            |            | 22334              | true                 | false                  | 10,50             | 10            | 18-06-2027 | 26-06-2048   | 1wE           | true        | 28-08-2027      | 10-06-2039     | 11-06-2039     | Monday    | EBOND            | 1        | 1             | 1                | 1           | 0.02         |

    @CsvFiles @BekijkenVoorVerwijdering
    Scenario: TC01B CSV files output for Variable loan update type with waterschap counterparty, variable notional type, WSW guarantor and StartDate-CurrentDate is shorter than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid

        Examples:
            | TC | TestUser  | SecID          | Notional | ExpectedNotional | CounterParty                        | Guarantor1 | Guarantor2 | GuarantorRefNumber | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | B  | Treasury1 | 2 0029818WZ1D  |          | 100000,00        | Trx Stichting Zorggroep Heilig Hart |            |            | L00385             | false                | false                  | 25,55             | 20            | 19-06-2027 | 09-03-2032   | 1mE           |             | 28-08-2027      | 30-08-2028     | 31-08-2028     | 15        | A360             | 2        | 2             | -2               | -2          | -0.04        |
            | C  | Treasury1 | 2 0031529RHWZ1 |          | 125758,75        | Trx Waterschap Groot Waterland      |            |            |                    | false                | false                  | 39                |               | 19-06-2027 | 04-12-2031   | 3mE           |             | 02-12-2027      |                |                | 4         | A360             | 3        | 3             | 3                | 3,868       | 0.069        |

    @CsvFiles
    Scenario: TC02A CSV files output for Variable loan update type with basic counterparty, no guarantor, and StartDate-CurrentDate is longer than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan fixed notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <SetNotionalTypeToFixed> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid

        Examples:
            | TC | TestUser     | SecID        | Notional | ExpectedNotional | CounterParty                  | Guarantor1 | Guarantor2 | GuarantorRefNumber | NegativeInterestRate | SetNotionalTypeToFixed | StartDate | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | D  | AccountDesk1 | 1 0030826WZ2 |          | 5254758,95       | Trx Stichting Greendale Wonen |            |            | 42702              | true                 | true                   | today     | 21-8-2051    | 6mE           |             |                 | 27-8-2049      |                | 21        | EBOND            | 4        |               |                  | 4,92        | 0.05         |

    @CsvFiles @BekijkenVoorVerwijdering
    Scenario: TC02B CSV files output for Variable loan update type with basic counterparty, no guarantor, and StartDate-CurrentDate is longer than 5 workdays
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan fixed notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | Guarantor2   | GuarantorRefNumber   | ExpectedNotional   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <Guarantor2> | <GuarantorRefNumber> | <ExpectedNotional> | <SetNotionalTypeToFixed> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And download the three csv files
        Then the assertions for each testcase for SECM variable loan case is valid
        And the assertions for each testcase for SECMNWB variable loan case is valid
        And the assertions for each testcase for BRED variable loan case is valid

        Examples:
            | TC | TestUser  | SecID        | Notional | ExpectedNotional | CounterParty                        | Guarantor1 | Guarantor2 | GuarantorRefNumber | NegativeInterestRate | SetNotionalTypeToFixed | StartDate | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | E  | Treasury1 | 1 0031562    |          | 5000,00          | Trx Stichting Zorggroep Heilig Hart |            |            | L00593             | false                | true                   | today     | 30-8-2034    | 12mE          |             |                 | 07-8-2034      |                | 30        | A360             | 5        | 5             | 2,42             | -2,842      | -0.004       |
            | F  | Treasury1 | 1W0027058RH4 |          | 100000000,00     | Trx Waterschap Groot Waterland      |            |            |                    | false                | true                   | today     | 09-8-2034    | 12mE          |             |                 | 07-8-2034      |                | 30        | A360             | 5        | 5             | 2,42             | -2,842      | -0.004       |