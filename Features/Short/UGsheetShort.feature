@Short
Feature: UGsheetShort

    A short summary of the feature

    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case

    Scenario: TC01 SHORT UG SHEET | Normal Counterparty with guarantors
        Given I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        Given I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a short loan with the following case and offer specifics
            | TC   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a short loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "SHORT" loan type
        Then the values should match the expected values for the SHORT UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1                       | Notional    | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           |
            | A  | AccountDesk1 | Waarborgfonds voor de Zorgsector | 10000000,00 | 01-07-2026 | 29-12-2026   | 0,5     | 0,75       | 1      | Deal Remark for TC A |
            | B  | Treasury1    | Trx Dunder Mifflin Paper Company | 100200,00   | 01-07-2026 | 29-12-2026   | 1,25    | 225        | 15     |                      |

    Scenario: TC02 SHORT UG SHEET | GR0% Scenario
        Given I select counterparty "Trx Samenwerkingsverband Nul" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        Given I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a short loan with the following case and offer specifics
            | TC   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a short loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "SHORT" loan type
        Then the values should match the expected values for the SHORT UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1 | Notional  | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark           |
            | C  | AccountDesk1 |            | 597854,95 | 01-07-2026 | 29-12-2026   | 0,5     | 0,75       | 5      | Deal Remark for TC C |

    Scenario: TC03 SHORT UG SHEET | GR20% Scenario
        Given I select counterparty "Trx Samenwerkingsverband Twintig" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        Given I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a short loan with the following case and offer specifics
            | TC   | Guarantor1   | Notional   | OfferReference   |
            | <TC> | <Guarantor1> | <Notional> | <OfferReference> |
        And I create a short loan with the following alternative specifics
            | StartDate   | MaturityDate   | PriceH1   | PriceFinal   | Margin   | DealRemark   |
            | <StartDate> | <MaturityDate> | <PriceH1> | <PriceFinal> | <Margin> | <DealRemark> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "SHORT" loan type
        Then the values should match the expected values for the SHORT UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1 | Notional    | StartDate  | MaturityDate | PriceH1 | PriceFinal | Margin | DealRemark |
            | D  | AccountDesk1 |            | 75293932,00 | 01-07-2026 | 29-12-2026   | 0,5     | 0,75       | 5,77   |            |
