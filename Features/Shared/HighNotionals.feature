@Shared
Feature: HighNotionals

    Different High Notional tests are included here:
    - Fixed high notional > 100.000.000
    - Fixed high notional > 200.000.000
    - Variable high notional > 100.000.000
    - Variable high notional > 200.000.000
    - Rejected high notional should end case and change status tot Resolved-Completed


    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)

    Scenario: TC01 Fixed High Notional | Approved
        Given I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | AmortType   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <AmortType> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I fill in the High Notional section and choose to approve the high notional
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Open-OrderConfirmation"

        Examples:
            | TC | TestUser     | Notional       | AmortType | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | CouponDay | DayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | A  | AccountDesk1 | 100.000.000,01 | Bullet    | 07-12-2026 | 07-7-2031    | Weekly     |                 |                | Monday    | Act/Act  | 0,5      | 0,75        |            |                | 1,5         | 1,508        |
            | B  | AccountDesk1 | 200.000.000,01 | Bullet    | 07-12-2026 | 07-7-2031    | Weekly     |                 |                | Monday    | 30/360   | 10       | 10          |            |                | 2           | 2,010        |


    Scenario: TC02 Fixed High Notional | Disapproved
        Given I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | AmortType   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <AmortType> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I fill in the High Notional section and choose to disapprove the high notional without an overwrite
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Resolved-NotApproved"

        Examples:
            | TC | TestUser     | Notional       | AmortType | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | CouponDay | DayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | C  | AccountDesk1 | 100.000.000,01 | Bullet    | 07-12-2026 | 07-7-2031    | Weekly     |                 |                | Monday    | Act/Act  | 0,5      | 0,75        |            |                | 1,5         | 1,508        |
            | D  | AccountDesk1 | 200.000.000,01 | Bullet    | 07-12-2026 | 07-7-2031    | Weekly     |                 |                | Monday    | 30/360   | 10       | 10          |            |                | 2           | 2,010        |

    Scenario: TC03 Variable, vaste hoofdsom High Notional | Approved
        Given I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I fill in the High Notional section and choose to approve the high notional
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Open-OrderConfirmation"

        Examples:
            | TC | TestUser     | Notional       | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | OfferReference                       | DealRemark                             | CustomerRate |
            | E  | AccountDesk1 | 100.000.000,01 | 07-12-2026 | 07-7-2031    | true                   |                   |               |                 |                |                | Monday    | 1wE           | true        | 0,5      | 0,5           | 0,75        | 0,75             | OfferRef Variable High notional TC A | Dealremark Variable High notional TC A | 1,500        |
            | F  | AccountDesk1 | 200.000.000,01 | 01-12-2031 | 01-6-2045    | true                   |                   |               |                 |                |                | 1         | 6mE           |             | 10       |               | 10,65       |                  |                                      |                                        | 10,65        |


    Scenario: TC03 Variable, variable hoofdsom High Notional | Approved
        Given I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I fill in the High Notional section and choose to approve the high notional
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Open-OrderConfirmation"

        Examples:
            | TC | TestUser     | Notional       | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | OfferReference | DealRemark | CustomerRate |
            | G  | AccountDesk1 | 100.000.000,01 | 01-12-2031 | 01-8-2045    | false                  | 15,75             | 15,5          | 02-12-2031      | 28-7-2045      |                | 5         | 12mE          |             | -5       | -5            | 10          | -12              |                |            | -2           |
            | H  | AccountDesk1 | 200.000.000,01 | 01-12-2031 | 01-6-2045    | false                  | 15,75             | 15,5          |                 |                |                | 1         | 3mE           |             | 5        | 5             | 10          | 2,75             |                |            | 12,75        |


    Scenario: TC04 Variable,vaste hoofdsom High Notional | Disapproved
        Given I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I fill in the High Notional section and choose to disapprove the high notional without an overwrite
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Resolved-NotApproved"

        Examples:
            | TC | TestUser     | Notional       | AmortType | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | OfferReference                       | DealRemark                             | CustomerRate |
            | I  | AccountDesk1 | 100.000.000,01 | Bullet    | 01-12-2031 | 01-6-2045    | true                   |                   |               |                 |                |                | 1         | 3mE           | 0,5      | 0,5           | 0,75        | 0,75             | OfferRef Variable High notional TC E | Dealremark Variable High notional TC E | 1,500        |


    Scenario: TC04 Variable, variabele hoofdsom High Notional | Disapproved
        Given I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I fill in the High Notional section and choose to disapprove the high notional without an overwrite
        Then the procuration email contains the notional and specific LAL number
        And when the CreateOffer page is submitted, the case status in WinLostCorrect should be "Resolved-NotApproved"

        Examples:
            | TC | TestUser     | Notional       | AmortType | StartDate  | MaturityDate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | OfferReference                       | DealRemark                             | CustomerRate |
            | J  | AccountDesk1 | 200.000.000,01 | Bullet    | 01-12-2031 | 01-12-2045   | false                  | 15,75             | 15,5          |                 |                |                | 1         | 12mE          | 10       | 10            | 10          | 10               | OfferRef Variable High notional TC F | Dealremark Variable High notional TC F | 20           |