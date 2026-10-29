@Variable
Feature: EmailContentVariable

    A short summary of the feature


    Scenario: TC01 Variable WSW WFZ Clients - 1wE - PF
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
