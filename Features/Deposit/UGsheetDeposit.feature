@Deposit
Feature: UGsheetDeposit

    A short summary of the feature


    Scenario: TC01 DEPOSIT UG SHEET - Normal Counterparty with guarantors - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        Given I select PEGA task "Deposit"
        And I create a deposit loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | DealMoment   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <DealMoment> |
        And I create a deposit loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "DEPOSIT" loan type
        Then the values should match the expected values for the DEPOSIT UGSHEET

        Examples:
            | TC | TestUser         | CounterParty                     | Notional  | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark | OfferReference | DealMoment |
            | B  | TreasuryManager1 | Trx Dunder Mifflin Paper Company | 100005,00 | 29-12-2026 | 15-10-2027   | 0,5     | -0,7513    | 5,50   |            |                | 27-12-2023 |


    Scenario: TC02 DEPOSIT UG SHEET - GR0% Scenario - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        Given I select PEGA task "Deposit"
        And I create a deposit loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | DealMoment   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <DealMoment> |
        And I create a deposit loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "DEPOSIT" loan type
        Then the values should match the expected values for the DEPOSIT UGSHEET

        Examples:
            | TC | TestUser     | CounterParty          | Notional    | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           | OfferReference    | DealMoment |
            | C  | AccountDesk1 | Trx Gemeente Meerdijk | 75293932,00 | 29-12-2026 | 29-12-2027   | 0,5     | 0,75       | 195,55 | Deal Remark for TC C | OfferRef for TC C | 28-12-2023 |
