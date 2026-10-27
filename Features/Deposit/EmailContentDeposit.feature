@Deposit
Feature: EmailContentDeposit

    This automatic tests asserts the different offer mail table, text and attachment for treasury1, treasurymanager1 and accountdesk1.

    @CSV
    Scenario: TC01 Create Opics Csv file for a deposit
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
            | TC | TestUser     | CounterParty          | Notional   | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           | OfferReference    | DealMoment |
            | C  | AccountDesk1 | Trx Gemeente Meerdijk | 7529393,00 | 2026-12-29 | 2027-12-29   | 3,535   | 3,753      | 5,753  | Deal Remark for TC C | OfferRef for TC C | 2023-12-28 |