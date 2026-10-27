@Short
Feature: EmailContentShort

    This feature tests the different types of offer email text for:
    HVC N.V., Stichting Nationaal Restauratiefonds, Stichting Groenfonds, normal and WFZ mail

    Furthermore this email asserts the offer email table.

    Scenario: TC01 Short Text HVC, Stichting Nationaal Restauratiefonds and Stichting Groenfonds
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        Given I select PEGA task "Loan"
        Given I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a short loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a short loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I press submit an email is generated
        Then the short offer email table should match the expected SHORT table
        Then the displayed short email text should match the expected text

        Examples:
            | TC | TestUser     | CounterParty                             | Guarantor1            | Notional   | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           | OfferReference    |
            | A  | AccountDesk1 | Trx HVC N.V.                             | Trx Gemeente Meerdijk | 75.293.932 | 01-07-2026 | 29-12-2026   | 0,5     | 0,75       | 5      | Deal Remark for TC A | OfferRef for TC A |
            | B  | AccountDesk1 | Trx Stichting nationaal restauratiefonds | Trx Gemeente Meerdijk | 100.200    | 01-07-2026 | 29-12-2026   | 1,25    | 1,25       | 15     | Deal Remark for TC B | OfferRef for TC B |
            | C  | AccountDesk1 | Trx Stichting Groenfonds                 | Trx Gemeente Meerdijk | 50.254     | 01-07-2026 | 30-11-2026   | 1       | 1,00       | 25     |                      |                   |