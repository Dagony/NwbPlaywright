@Short
Feature: CsvFilesShort

    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case

    @CsvFiles @BekijkenVoorVerwijdering
    Scenario: TC01 Create Opics Csv file for Short Loan
        Given I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a short loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> |
        And I create a short loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval
        And I download the "DEPLO.csv" file
        Then the assertions for each SHORT testcase for DEPLO short loan or deposit case is valid
        Examples:
            | TC | TestUser  | CounterParty                     | Guarantor1         | Notional    | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark          |
            | A  | Treasury1 | Trx Waterschap Groot Waterland   |                    | 85550455,75 | 2027-12-01 | 2027-12-08   | 4       | 4,655      | 66     | Dealremark for TC A |
            | B  | Treasury1 | Trx Dunder Mifflin Paper Company | Gemeente Culemborg | 19125975    | 2027-12-01 | 2027-12-09   | -0,5    | -0,499     | 5      | Dealremark for TC B |

