@LoanUpdate
Feature: LoanUpdateEmailContentVariable

    Scenario: TC01A Variable WSW WFZ Clients | 1wE
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

    @BekijkenVoorVerwijdering
    Scenario: TC01B Variable WSW WFZ Clients | 1wE
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
            | B  | Treasury1 | 2 0031407    | 2 0031407             | Trx Stichting Zorggroep Heilig Hart | Stichting Waarborgfonds voor de Zorgsector |          | 50.100,00        | 01-12-2027 | 01-12-2045   | false                  | 5,75              |               | 02-12-2027      | 30-11-2037     | 03-12-2037     | Wednesday | A360             | 1wE           | false       | 1,25     | -0,25         | 1,25        | -0,25            |            |                | 1            |

    @BekijkenVoorVerwijdering
    Scenario: TC02 Variable NORMAL clients | 1mE/3mE/6mE/12mE
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan fixed notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | SetNotionalTypeToFixed   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <SetNotionalTypeToFixed> | <ExpectedOriginalSecID> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the variable offer email table should match the expected table
        And the displayed variable email text should match the expected text

        Examples:
            | TC | TestUser | SecID         | ExpectedOriginalSecID | CounterParty                     | ExpectedGuarantor1    | Notional | ExpectedNotional | StartDate  | MaturityDate | SetNotionalTypeToFixed | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | CustomerRate |
            | C  | Treasury1 | 1 0028320RH8  | 1 0028320             | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk |          | 25.000,00        | 08-01-2029 | 08-7-2031    | true                   |                 |                |                | 8         | A360             | 1mE           | 1        | 1             | 1           | 1                | Deal Remark voor TC C | Offer ref voor TC C | 2            |
            | D  | Treasury1 | 1 0029646     | 1 0029646             | Trx Dunder Mifflin Paper Company |                       |          | 35.000.000,00    | 19-01-2027 | 19-02-2035   | true                   |                 |                |                | 19        | A360             | 1mE           | -22      | -22           | 40          | -45              |                       | Offer ref voor TC D | -5           |
            | E  | Treasury1 | 1W0025266WZ1D | 1 0025266             | Trx Dunder Mifflin Paper Company |                       |          | 33.478.457,50    | 09-01-2029 | 19-2-2030    | true                   | 10-01-2029      |                |                | 19        | A360             | 6mE           | -22      | 23            | -5,75       | -45,75           |                       |                     | -5,75        |
            | F  | Treasury1 | 1W0026235RH7B | 1 0026235             | Trx Dunder Mifflin Paper Company |                       |          | 50.000.000,00    | 19-12-2030 | 20-2-2035    | true                   |                 | 18-02-2035     |                | 19        | A360             | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            |                       |                     | 33,05        |

