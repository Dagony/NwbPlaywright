@LoanUpdate
Feature: LoanUpdateEmailContentVariable

    Scenario: TC01A Variable WSW WFZ Clients - 1wE
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> | <ExpectedOriginalSecID> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   | ExpectedDayCount   | HoursClause   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> | <ExpectedDayCount> | <HoursClause> |
        When I press submit an email is generated
        Then the variable offer email table should match the expected table
        And the displayed variable email text should match the expected text

        Examples:
            | TC | TestUser | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark | OfferReference | CustomerRate |
            | A  | AccountDesk1 | 2 0031716WZ1 | 2 0031716             | Trx Stichting Greendale Wonen       | Stichting Waarborgfonds Sociale Woningbouw |          | 17.528.435,50    | 01-12-2027 | 03-7-2034    | false                  | 35                | 15,42         | 06-01-2028      |                | 02-12-2030     | Monday    | EBOND            | 1wE           | true        | 0,5      | 0,5           | 0,75        | 0,75             |            |                | 1,5          |
