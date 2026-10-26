@Variable
Feature: EmailContentVariable

    A short summary of the feature


    Scenario: TC01 Variable WSW WFZ Clients | 1wE - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | OfferReference   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <OfferReference> |
        When I press submit an email is generated
        Then the variable offer email table should match the expected table
        And the displayed variable email text should match the expected text

        Examples:
            | TC | TestUser     | CounterParty                        | Guarantor1 | Notional     | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark | OfferReference |
            | A  | AccountDesk1 | Trx Stichting Greendale Wonen       |            | 75.293.932,5 | 01-12-2026 | 03-7-2031    | false                  | 35                | 15,42         | 02-12-2026      | 30-11-2028     | 01-12-2028     | Monday    | 1wE           | true        | 0,5      | 0,5           | 0,75        | 0,75             |            |                |

    @BekijkenVoorVerwijdering
    Scenario: TC01 Variable WSW WFZ Clients | 1wE - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | OfferReference   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <OfferReference> |
        When I press submit an email is generated
        Then the variable offer email table should match the expected table
        And the displayed variable email text should match the expected text

        Examples:
            | TC | TestUser     | CounterParty                        | Guarantor1 | Notional     | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark | OfferReference |
            | B  | Treasury1    | Trx Stichting Zorggroep Heilig Hart |            | 100.200      | 01-12-2026 | 01-12-2045   | false                  | 5,75              |               | 02-12-2026      | 30-11-2035     | 03-12-2035     | Wednesday | 1wE           | false       | 1,25     | -0,25         | 1,25        | -0,25            |            |                |

    @BekijkenVoorVerwijdering
    Scenario: TC02 Variable NORMAL clients | 1mE/3mE/6mE/12mE - TRY
        Given I login to the environment with test user "Treasury1" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | OfferReference   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <OfferReference> |
        When I press submit an email is generated
        Then the variable offer email table should match the expected table
        And the displayed variable email text should match the expected text

        Examples:
            | TC | CounterParty                     | Guarantor1            | Notional      | StartDate  | MaturityDate | SetNotionalTypeToFixed | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      |
            | C  | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk | 50.254        | 08-01-2029 | 08-07-2031   | true                   |                 |                |                | 8         | 1mE           | 1        | 1             | 1           | 1                | Deal Remark voor TC C | Offer ref voor TC C |
            | D  | Trx Dunder Mifflin Paper Company |                       | 7.560.500     | 20-01-2028 | 20-04-2028   | true                   |                 |                |                | 20        | 3mE           | -22      | -22           | 40          | -45              |                       | Offer ref voor TC D |
            | E  | Trx Dunder Mifflin Paper Company |                       | 99.999.999,99 | 09-01-2029 | 19-02-2032   | true                   | 30-01-2029      |                |                | 19        | 6mE           | -22      | 23            | -5,75       | -45,75           |                       |                     |
            | F  | Trx Dunder Mifflin Paper Company |                       | 100.000.000   | 19-12-2028 | 20-02-2029   | true                   |                 | 18-02-2028     |                | 19        | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            |                       |                     |