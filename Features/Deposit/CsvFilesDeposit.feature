@Deposit
Feature: CsvFilesDeposit

    @CSV
    Scenario: TC01B Create Opics Csv file for a deposit - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Deposit"
        And I create a deposit loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | DealMoment   |
            | <TC> | <CounterParty> | <Notional> | <DealMoment> |
        And I create a deposit loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval for the new PEGA class structure
        And I download the "DEPLO.csv" file
        Then the assertions for each testcase for DEPLO short loan or deposit case is valid
        Examples:
            | TC | TestUser         | CounterParty                        | Notional     | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin  | DealRemark          | DealMoment |
            | B  | Treasury1        | Trx Dunder Mifflin Paper Company    | 19125975     | 2026-12-01 | 2026-12-09   | -0,5    | -0,5       | 0,1     | Dealremark for TC B | 2023-11-30 |
            | C  | Treasury1        | Trx Stichting Zorggroep Heilig Hart | 100000000,01 | 2026-12-01 | 2026-12-08   | 11,5921 | 11,5921    | 10,5521 | Dealremark for TC C | 2023-11-30 |
            | D  | TreasuryManager1 | Trx Waterschap Groot Waterland      | 200000000,01 | 2026-12-01 | 2026-12-09   | 4,25    | 4,29       | 1,12    | Dealremark for TC D | 2023-11-30 |