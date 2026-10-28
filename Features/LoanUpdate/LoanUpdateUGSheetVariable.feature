@LoanUpdate
Feature: LoanUpdatUGSheeteVariable


    Scenario: TC01 VARIABLE UG SHEET | Municipality with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <ExpectedOriginalSecID> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | ExpectedAmort   | HoursClause   | ExpectedDayCount   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <ExpectedAmort> | <HoursClause> | <ExpectedDayCount> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEVARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | ExpectedAmort  | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedDayCount | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | A  | AccountDesk1 | 2 0031885    | 2 0031885             | Stichting Waarborgfonds Sociale Woningbouw |          | 125125,87        | 17-10-2028 | 01-04-2069   | 19-10-2028      | Monday    | 1wE           | LINEAIR BALLON | True        | False                  | 15                | 33,75         | 30/360           | 17-10-2035           | 1        | 1             | 10,00       | 10,00            | 20,00        | Deal Remark for TC A |
            | B  | AccountDesk1 | 2 0031766WZ1 | 2 0031766             | Waarborgfonds voor de Zorgsector           |          | 2768342,00       | 17-10-2028 | 31-10-2069   | 19-10-2028      | 31        | 1mE           | MANUAL         |             | False                  | 25,55             |               | Act/360          | 17-10-2035           | 1        | 1             | 540,00      | -547,5           | -7,50        |                      |

    Scenario: TC02 VARIABLE UG SHEET | Municipality without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <ExpectedOriginalSecID> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | ExpectedAmort   | ExpectedDayCount   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <ExpectedAmort> | <ExpectedDayCount> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "LOANUPDATEVARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | ExpectedAmort | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedDayCount | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | C  | AccountDesk1 | 2W0027273RH6 | 2 0027273             |                    |          | 32412,00         | 03-03-2027 | 01-04-2069   | 06-04-2045      | 1         | 3mE           | MANUAL        | False                  | 55                |               | Act/360          | 5,78                 | 1        | 1             | -4,59       | 7,59             | 3            | Deal Remark for TC C |

