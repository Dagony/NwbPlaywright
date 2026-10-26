@Deposit
Feature: EmailContentDeposit

    This automatic tests asserts the different offer mail table, text and attachment for treasury1, treasurymanager1 and accountdesk1.

    @CSV @BekijkenVoorVerwijdering
    Scenario: TC01 Offermail Deposit |
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        Given I select PEGA task "Deposit"
        And I create a deposit loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | DealMoment   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <DealMoment> |
        And I create a deposit loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I press submit an email is generated
        Then the deposit offer email table should match the expected table
        Then the displayed deposit email text should match the expected text
        #Then offerte attachment should not be visible

        Examples:
            | TC | TestUser         | CounterParty                        | Notional     | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           | OfferReference    | DealMoment |
            | A  | Treasury1        | Trx Stichting Zorggroep Heilig Hart | 175000200,95 | 29-12-2026 | 29-6-2027    | 5       | 5          | 4,75   | Deal Remark for TC A | OfferRef for TC A | 26-12-2023 |
            | B  | TreasuryManager1 | Trx Dunder Mifflin Paper Company    | 25750000,00  | 29-12-2026 | 15-10-2027   | -0,575  | -0,753     | -1,563 |                      |                   | 27-12-2023 |


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