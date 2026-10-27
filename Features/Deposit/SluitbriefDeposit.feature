@Deposit
Feature: SluitbriefDeposit

    A short summary of the feature

    
    Scenario: TC02 Sluitbrief for a deposit | With multiple advisor
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "0" random counterparty individual(s) and "4" random advisor individual(s)
        Given I select PEGA task "Deposit"
        And I create a deposit loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | DealMoment   |
            | <TC> | <CounterParty> | <Notional> | <DealMoment> |
        And I create a deposit loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I continue the case to BO first flow LoanDataApproval for the new PEGA class structure
        And I open the sluitbrief
        Then the displayed sluitbrief for deposito should match the expected table

        Examples:
            | TC | TestUser         | CounterParty                        | Notional       | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin  | DealRemark          | DealMoment |
            | C  | Treasury1        | Trx Stichting Zorggroep Heilig Hart | 100.000.000,01 | 01-12-2026 | 08-12-2026   | 11,5921 | 11,5921    | 10,5521 | Dealremark for TC C | 29-11-2023 |
            | D  | TreasuryManager1 | Trx Stichting Zorggroep Heilig Hart | 200.000.000,01 | 01-12-2026 | 09-12-2026   | 4,25    | 4,29       | 1,12    |                     | 30-11-2023 |