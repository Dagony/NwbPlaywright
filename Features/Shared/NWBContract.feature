@Shared
Feature: NWBContract

    NWB contracts are only generated for NWB clients (non WSW, non WFZ clients). A guarantor maybe applicable but they shouldn't be
    Stichting Waarborgfonds voor

    Scenario: TC01A NWB contracts, FIXED, municipality, waterschap and normal org - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        Then the NWB Contract should be generated correctly
        Examples:
            | TC | TestUser     | CounterParty          | Guarantor1 | Notional    | AmortType | NumbOfAmort | AmortFreq | CouponFreq | Swap  | DayCount | StartDate | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | FinalSpread | MarketIRate | CustomerRate |
            | A  | AccountDesk1 | Trx Gemeente Meerdijk |            | 85550455,75 | Bullet    |             |           | Annual     | False | Act/Act  | 1-12-2026 | 31-12-2026   | 1-12-2026       |                |                | 31        | 15       | 17,94       | 4           | 4,1794       |



    @BekijkenVoorVerwijdering
    Scenario: TC01B NWB contracts, FIXED, municipality, waterschap and normal org - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        Then the NWB Contract should be generated correctly
        Examples:
            | TC | TestUser  | CounterParty                     | Guarantor1 | Notional | AmortType | NumbOfAmort | AmortFreq | CouponFreq | Swap  | DayCount | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | FinalSpread | MarketIRate | CustomerRate |
            | B  | Treasury1 | Trx Waterschap Groot Waterland   |            | 125493   | Linear    | 50          | Annual    | Annual     | False | Act/360  | 3-12-2026  | 1-4-2076     |                 |                | 3-12-2040      | 3         | 20       | 20          | 3,66        | 3,8600       |
            | C  | Treasury1 | Trx Dunder Mifflin Paper Company |            | 19125975 | Manual    | 50          | Annual    | Annual     | False | 30/360   | 15-10-2027 | 14-10-2107   | 15-10-2027      |                |                | 14        | 5,75     | 5,75        | -2          | -1,9425      |

    Scenario: TC02 NWB contracts,VAR VAR hoofdsom, with guarantor - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        # And I download the "NWB Contract Annex Conditions Precedent" file ## Correctly commented out?
        And I download the "NWB Contract Annex Model Adjustment Request" file
        And I download the "NWB Contract Annex Guarantor" file
        Examples:
            | TC | TestUser     | CounterParty                   | Guarantor1                       | Notional   | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | D  | AccountDesk1 | Trx Gemeente Meerdijk          | Trx Dunder Mifflin Paper Company | 1125125,55 | true                 | false                  | 20                | 55,95         | 1-12-2026 | 7-12-2027    | 1wE           | true        |                 |                |                | Tuesday   | 10       | 10            | 25               | 25          |


    @BekijkenVoorVerwijdering
    Scenario: TC02B NWB contracts,VAR VAR hoofdsom, with guarantor - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        And I download the "NWB Contract Annex Model Adjustment Request" file
        And I download the "NWB Contract Annex Guarantor" file
        Examples:
            | TC | TestUser  | CounterParty                     | Guarantor1            | Notional    | NegativeInterestRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | E  | Treasury1 | Trx Waterschap Groot Waterland | Trx Gemeente Meerdijk            | 5000000    | true                 | false                  | 50,75             | 105           | 8-01-2027 | 6-01-2034    | 1wE           | false       | 09-01-2027      |                |                | Friday    | 25       | 25            | 25               | 25          |
            | F  | Treasury1 | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk | 10958750,33 | true                 | false                  | 5,75              |               | 17-02-2027 | 17-02-2059   | 1mE           |             |                 |                | 17-02-2031     | 17        | 35,50    | 15,75         | 35,50            | 15,75       |

    Scenario: TC03A NWB contracts,VAR FIXED hoofdsom, without guarantor - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        Examples:
            | TC | TestUser     | CounterParty          | Guarantor1 | Notional   | NegativeInterestRate | SetNotionalTypeToFixed | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | G  | AccountDesk1 | Trx Gemeente Meerdijk |            | 1125125,55 | true                 | true                   | 01-06-2027 | 01-09-2045   | 3mE           |             |                 |                | 01-03-2030     | 1         | -10      | -10           | -25              | -25         |


    @BekijkenVoorVerwijdering
    Scenario: TC03B NWB contracts,VAR FIXED hoofdsom, without guarantor - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        Examples:
            | TC | TestUser  | CounterParty                     | Guarantor1 | Notional    | NegativeInterestRate | SetNotionalTypeToFixed | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | H  | Treasury1 | Trx Waterschap Groot Waterland   |            | 5000000     | true                 | true                   | 27-10-2027 | 27-04-2048   | 6mE           |             |                 |                |                | 27        | 10       |               |                  | 110         |
            | I  | Treasury1 | Trx Dunder Mifflin Paper Company |            | 10958750,33 | true                 | true                   | 04-01-2027 | 28-01-2039   | 12mE          |             | 05-01-2027      | 27-01-2039     |                | 5         | 55       | 55            | 55               | 125         |


    Scenario: TC04 Create NWB Contract for HVC, Groenfonds and Restauratiefonds with guarantor - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file

        Examples:
            | TC | TestUser     | CounterParty                             | Guarantor1         | Notional      | NegativeInterestRate | SetNotionalTypeToFixed | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread |
            | J  | AccountDesk1 | Trx Stichting Groenfonds                 | Gemeente Culemborg | 75.293.932,00 | true                 | true                   | 01-06-2027 | 01-09-2045   | 3mE           |             |                 |                | 01-03-2030     | 1         | -10      | -10           | -25              | -25         |
            | K  | AccountDesk1 | Trx HVC N.V.                             | Gemeente Culemborg | 125.000,75    | true                 | true                   | 27-10-2027 | 27-04-2048   | 6mE           |             |                 |                |                | 27        | 10       |               |                  | 110         |
            | L  | AccountDesk1 | Trx Stichting nationaal restauratiefonds | Gemeente Culemborg | 100.005       | true                 | true                   | 04-01-2027 | 28-01-2039   | 12mE          |             | 05-01-2027      | 27-01-2039     |                | 5         | 55       | 55            | 55               | 125         |

    Scenario: TC05 Create NWB Contract for HVC, Groenfonds and Restauratiefonds without guarantor - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
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
        And I download the "NWB Contract" file
        And I download the "NWB Contract Annex Conditions Precedent" file
        Then the NWB Contract should be generated correctly
        Examples:
            | TC | TestUser     | CounterParty                             | Guarantor1 | Notional      | AmortType       | NumbOfAmort | AmortFreq | CouponFreq | Swap  | DayCount | StartDate | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | FinalSpread | MarketIRate | CustomerRate |
            | M  | AccountDesk1 | Trx Stichting Groenfonds                 |            | 75.293.932,00 | Annuity         | 15          | Annual    | Annual     | False | Act/Act  | 1-12-2026 | 15-1-2066    |                 | 15-12-2026     |                | 1         | -10      | -15         | -0,5        | -0,6500      |
            | N  | AccountDesk1 | Trx HVC N.V.                             |            | 125.000,75    | Linear balloon  | 3           | Annual    | Annual     | False | Act/360  | 1-2-2027  | 26-2-2047    |                 | 26-2-2027      | 31-12-2040     | 1         | 155      | 250         | 2,35        | 4,8500       |
            | O  | AccountDesk1 | Trx Stichting nationaal restauratiefonds |            | 100.005       | Annuity balloon | 100         | Annual    | Annual     | False | 30/360   | 1-06-2027 | 01-06-2032   |                 |                |                | 1         | 130      | 130         | 5           | 6,3000       |
