@Shared
Feature: WSWContract


    WSW contracts are only generated for WSW clients (WSW, non-WfZ, non normal clients).

    per 01-08-2025 besloten met key user om geen negatieve rente te ondersteunen. Zie design.


    Scenario: TC01 Create WsW Long
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        #Given I select counterparty "Woonstichting Charlotte van Beuningen" with individual(s) "FirstName de LastName489616"
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a fixed loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> |
        And I create a fixed loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | CouponDay   | InterestRRDate   | AmortType   | NumbOfAmort   | AmortFreq   | CouponFreq   | Swap   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <InterestRRDate> | <AmortType> | <NumbOfAmort> | <AmortFreq> | <CouponFreq> | <Swap> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <CustomerRate> |
        When I choose won and complete the bill of closure phase
        And I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
        And I download the "WSW Contract" file
        #	And I download the "WSW General Conditions" file
        #Then the WSW Contract should be generated correctly
        Examples:
            | TC | TestUser     | CounterParty                  | Guarantor1 | Notional    | AmortType       | NumbOfAmort | AmortFreq   | CouponFreq  | Swap  | DayCount | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | FinalSpread | MarketIRate |
            | A  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 75000500    | Bullet          |             |             | Annual      | False | Act/Act  | 05-01-2027 | 20-01-2050   | 20-01-2027      | 18-01-2043     | 19-01-2043     | 10        | 15       | 17,94       | 4           |
            | B  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 96123854,95 | Linear          | 10          | Annual      | Annual      | False | Act/360  | 01-09-2027 | 01-09-2074   |                 |                |                | 1         | 15       | 20          | 3,66        |
            | C  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 5750125     | Annuity         | 20          | Semi annual | Semi annual | False | 30/360   | 21-12-2027 | 21-12-2051   |                 |                |                | 21        | 5,75     | 5,75        | -2          |
            | C2 | AccountDesk1 | Trx Stichting Greendale Wonen |            | 5750125     | Annuity         | 20          | Semi annual | Semi annual | False | 30/360   | 21-12-2027 | 21-12-2051   | 09-03-2028      |                |                | 21        | 5,75     | 5,75        | -2          |
            | D  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 2943750,50  | Linear balloon  | 30          | Quarterly   | Quarterly   | False | Act/Act  | 05-01-2027 | 05-01-2039   |                 |                | 05-01-2030     | 5         | 20       | 15,75       | 1           |
            | E  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 100000      | Annuity balloon | 40          | Quarterly   | Quarterly   | False | Act/360  | 20-11-2027 | 20-02-2074   | 05-02-2028      |                |                | 20        | 5,75     | 185         | 0,50        |
            | F  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 10000001    | Manual          | 30          | Monthly     | Monthly     | False | 30/360   | 05-01-2027 | 05-01-2039   |                 |                |                | 5         | 20       | 13,2        | 2           |
            | G  | AccountDesk1 | Trx Stichting Greendale Wonen |            | 20000001    | Zero            | 40          | Weekly      | Weekly      | False | Act/Act  | 20-11-2027 | 20-12-2074   | 20-11-2027      | 24-10-2052     | 24-11-2052     | Friday    | 5,75     | 90,4        | 1           |


    Scenario: TC02 Create WSW Variable
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        #Given I select counterparty "Woonstichting Charlotte van Beuningen" with individual(s) "FirstName de LastName489616"
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> |
        When I choose won and complete the bill of closure phase
        And I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
        And I download the "WSW Contract" file
        #And I download the "WSW General Conditions" file
        Examples:
            | TC | TestUser     | Notional    | CounterParty                  | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | H  | AccountDesk1 | 75000500    | Trx Stichting Greendale Wonen |            | true                 | true                   | 18-12-2027 | 26-06-2049   | 1wE           | true        | 21-12-2027      | 20-05-2039     | 11-06-2039     | Friday    | 10       | 10            | 25               | 25          |
            | I  | AccountDesk1 | 96123854,95 | Trx Stichting Greendale Wonen |            | true                 | true                   | 18-12-2027 | 26-06-2049   | 1wE           | false       | 18-12-2027      | 26-06-2049     |                | Friday    | 2        | 2             | -2               | -2          |
            | J  | AccountDesk1 | 5750125     | Trx Stichting Greendale Wonen |            | false                | true                   | 01-06-2027 | 01-12-2032   | 1mE           |             |                 |                |                | 1         | 3        | 3             | 30               | 13,868      |
            | K  | AccountDesk1 | 2943750,50  | Trx Stichting Greendale Wonen |            | false                | true                   | 19-06-2027 | 09-03-2033   | 3mE           |             | 22-6-2027       | 30-8-2029      | 31-8-2029      | 15        | 25       | 25            | -20              | -20         |
            | L  | AccountDesk1 | 10000001    | Trx Stichting Greendale Wonen |            | false                | true                   | 01-06-2027 | 01-12-2032   | 6mE           |             |                 |                |                | 1         | 55       | 55            | 151              | 151         |
            | M  | AccountDesk1 | 20000001    | Trx Stichting Greendale Wonen |            | false                | true                   | 01-06-2027 | 01-06-2033   | 12mE          |             |                 |                |                | 1         | 3        | 3             | 103              | 3,868       |



    Scenario: TC03 Create WSW Variable with variable hoofdsom
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        #Given I select counterparty "Woonstichting Charlotte van Beuningen" with individual(s) "FirstName de LastName489616"
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | ExpectedNotional   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <ExpectedNotional> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | NegativeInterestRate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <NegativeInterestRate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> |
        When I choose won and complete the bill of closure phase
        And I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
        And I download the "WSW Contract" file
        #	And I download the "WSW General Conditions" file
        Examples:
            | TC | TestUser     | Notional    | CounterParty                  | Guarantor1 | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | N  | AccountDesk1 | 75000500    | Trx Stichting Greendale Wonen |            | true                 | false                  | 20                | 10,75         | 18-12-2027 | 26-06-2049   | 1wE           | true        | 21-12-2027      | 21-05-2039     | 11-06-2039     | Friday    | 10       | 10            | 25               | 25          |
            | O  | AccountDesk1 | 96123854,95 | Trx Stichting Greendale Wonen |            | true                 | false                  | 25,55             | 20            | 18-12-2027 | 26-06-2049   | 1wE           | false       | 18-12-2027      | 26-06-2049     |                | Friday    | 2        | 2             | -2               | -2          |
            | P  | AccountDesk1 | 5750125     | Trx Stichting Greendale Wonen |            | false                | false                  | 35                |               | 01-06-2027 | 01-12-2032   | 1mE           |             |                 |                |                | 1         | 3        | 3             | 3                | 3,868       |
            | Q  | AccountDesk1 | 2943750,50  | Trx Stichting Greendale Wonen |            | false                | false                  | 40                | 125           | 19-06-2027 | 09-03-2033   | 3mE           |             | 22-6-2027       | 30-8-2029      | 31-8-2029      | 15        | 2        | 2             | -2               | -2          |
            | R  | AccountDesk1 | 10000001    | Trx Stichting Greendale Wonen |            | false                | false                  | 50                |               | 01-06-2027 | 01-12-2031   | 6mE           |             |                 |                |                | 1         | 3        | 3             | 3                | 3,868       |
            | S  | AccountDesk1 | 20000001    | Trx Stichting Greendale Wonen |            | false                | false                  | 50                |               | 01-06-2027 | 01-06-2032   | 12mE          |             |                 |                |                | 1         | 3        | 3             | 103              | 3,868       |




