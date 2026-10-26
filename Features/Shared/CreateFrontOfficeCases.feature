@Shared
Feature: CreateFrontOfficeCases
    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    As a tester I want to quickly create long, variable and short cases with different specifications

    Counterparties to chose from:
    "Trx Stichting Greendale Wonen"			=> WSW
    "Trx Stichting Zorggroep Heilig Hart"	=> WFZ
    "Trx Gemeente Meerdijk"					=> Normal
    "Trx Waterschap Groot Waterland"		=> Waterschap
    "Trx Dunder Mifflin Paper Company"		=> Normal
    "Trx Stichting Groenfonds"
    "Trx HVC N.V."
    "Trx Stichting nationaal restauratiefonds"
    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    -Indien geen MaturityDate wordt gekozen wordt automatisch 40 jaar maturity gekozen voor LONG, 20 jaar voor VARIABLE en 1 jaar voor SHORT (LET OP!: berekend vanaf huidige systeemdate).
    -Indien StartDate wordt gezet op "today" wordt huidige systeemdate gebruikt als StartDate (indien dit op een weekenddag valt worden hier 2 dagen bij opgeteld).
    -Indien geen notional wordt gekozen wordt er een random notional gegenereerd tussen 100 en 63.900.000 EUR
    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Voor het downloaden van .docx, .csvs: specificeer download filepath in appsettings.json. Om BO documenten te downloaden vul volgende waarden in de download stap (of voeg een extra download stap toe):
    "Cover Letter", "WSW Contract" , "WSW Summary", "NWB Contract", "DEPLO.csv", "SECMNWB.csv", "SECM.csv", "BRED.csv", "WfZ Contract", "NWB Contract Annex Guarantor" en "NWB Contract Annex Conditions Precedent"
    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Belangrijke COMMANDS voor test automation:
    -Duik in onderliggende laag van specflow step/method/code => F12
    -Code/specflow stap uitlijnen (bijv na aanpassen parameters) => shift + alt + F
    -Code in specflow (hieronder) disablen => #
    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    @BekijkenVoorVerwijdering
    Scenario: FIXED-A loan creation
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a FIXED case with the following OfferData
            | TC   | Notional   | CddReviewDate   | DealMoment   | Guarantor1   | AdditEmail1   | AdditEmail2   | SpreadCheckBy   | SpreadCheckRemarks   |
            | <TC> | <Notional> | <CddReviewDate> | <DealMoment> | <Guarantor1> | <AdditEmail1> | <AdditEmail2> | <SpreadCheckBy> | <SpreadCheckRemarks> |
        And I use the following FIXED alternative data
            | OfferReference   | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | CouponFreq   | DayCount   | AmortType   | NumbOfAmort   | AmortFreq   | Swap   |
            | <OfferReference> | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <CouponFreq> | <DayCount> | <AmortType> | <NumbOfAmort> | <AmortFreq> | <Swap> |
        And I use the follow FIXED spread data
            | SpreadH1   | SpreadH2   | FinalSpread   | MarketIRate   | OverwriteSpreadH1Comment   | OverwriteSpreadH2Comment   | OverwriteSpreadMismatchComment   | OverwriteFinalSpreadComment   | OverwriteDealRemark   |
            | <SpreadH1> | <SpreadH2> | <FinalSpread> | <MarketIRate> | <OverwriteSpreadH1Comment> | <OverwriteSpreadH2Comment> | <OverwriteSpreadMismatchComment> | <OverwriteFinalSpreadComment> | <OverwriteDealRemark> |
        When I fill in the FIXED create offer with all the data
        And I choose won and complete the bill of closure phase

        Examples: for FIXED
            | TC | TestUser  | CounterParty                   | Notional | SpreadCheckBy | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq | DayCount | AmortType | NumbOfAmort | AmortFreq | Swap  | SpreadH1 | SpreadH2 | FinalSpread | OverwriteFinalSpreadComment | MarketIRate | OverwriteDealRemark | CddReviewDate | DealMoment | Guarantor1 | AdditEmail1 | AdditEmail2 | SpreadCheckRemarks | OfferReference | OverwriteSpreadH1Comment | OverwriteSpreadH2Comment | OverwriteSpreadMismatchComment |
            | A  | Treasury1 | Trx Waterschap Groot Waterland | 15250795 | false         | 03-04-2026 | 13-01-2045   |                 |                |                | Friday    | Weekly     | Act/Act  | Linear    | 10          | Quarterly | false | 5        | 2        | 5           | TESTING OVERWRITE           | 3           |                     |               |            |            |             |             |                    |                |                          |                          |                                |

    Scenario: FIXED-B loan creation
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        And I create a FIXED case with the following OfferData
            | TC   | Notional   | CddReviewDate   | DealMoment   | Guarantor1   | AdditEmail1   | AdditEmail2   | SpreadCheckBy   | SpreadCheckRemarks   |
            | <TC> | <Notional> | <CddReviewDate> | <DealMoment> | <Guarantor1> | <AdditEmail1> | <AdditEmail2> | <SpreadCheckBy> | <SpreadCheckRemarks> |
        And I use the following FIXED alternative data
            | OfferReference   | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | CouponFreq   | DayCount   | AmortType   | NumbOfAmort   | AmortFreq   | Swap   |
            | <OfferReference> | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <CouponFreq> | <DayCount> | <AmortType> | <NumbOfAmort> | <AmortFreq> | <Swap> |
        And I use the follow FIXED spread data
            | SpreadH1   | SpreadH2   | FinalSpread   | MarketIRate   | OverwriteSpreadH1Comment   | OverwriteSpreadH2Comment   | OverwriteSpreadMismatchComment   | OverwriteFinalSpreadComment   | OverwriteDealRemark   |
            | <SpreadH1> | <SpreadH2> | <FinalSpread> | <MarketIRate> | <OverwriteSpreadH1Comment> | <OverwriteSpreadH2Comment> | <OverwriteSpreadMismatchComment> | <OverwriteFinalSpreadComment> | <OverwriteDealRemark> |
        When I fill in the FIXED create offer with all the data
        And I choose won and complete the bill of closure phase

        Examples: for FIXED
            | TC | TestUser     | CounterParty          | Notional | SpreadCheckBy | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq | DayCount | AmortType | NumbOfAmort | AmortFreq   | Swap  | SpreadH1 | SpreadH2 | FinalSpread | OverwriteFinalSpreadComment | MarketIRate | OverwriteDealRemark | CddReviewDate | DealMoment | Guarantor1 | AdditEmail1 | AdditEmail2 | SpreadCheckRemarks | OfferReference | OverwriteSpreadH1Comment | OverwriteSpreadH2Comment | OverwriteSpreadMismatchComment |
            | B  | AccountDesk1 | Trx Gemeente Meerdijk | 4535747  | true          | 06-04-2026 | 11-01-2055   |                 |                |                | Monday    | Weekly     | Act/Act  | Annuity   | 20          | Semi annual | false | 5        | 2        | 5           |                             | 3           |                     |               |            |            |             |             |                    |                |                          |                          |                                |
    #	| C  |          | false         | 07-01-2026 | 15-01-2059   |                 |                |                | Wednesday | Weekly     | Act/Act  | Bullet    |             | Annual      | false |        5 |        2 |           5 |                             |           3 |                     |    99999999 |               |            |            |             |             |                    |                |                          |                          |                                |
    #	| D  |          | false         | 08-01-2026 | 16-01-2059   |                 |                |                | Thursday  | Weekly     | Act/Act  | Bullet    |             | Annual      | false |        5 |        2 |           5 |                             |           3 |                     |    99999999 |               |            |            |             |             |                    |                |                          |                          |                                |




    Scenario: VARIABLE fixed loan creation
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a VARIABLE case with the following OfferData
            | TC   | Notional   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | CddReviewDate   | DealMoment   | Guarantor1   | AdditEmail1   | AdditEmail2   | SpreadCheckBy   | SpreadCheckRemarks             |
            | <TC> | <Notional> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <CddReviewDate> | <DealMoment> | <Guarantor1> | <AdditEmail1> | <AdditEmail2> | <SpreadCheckBy> | <OverWriteSpreadkCheckRemarks> |
        And I use the following VARIABLE alternative data
            | OfferReference   | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   |
            | <OfferReference> | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> |
        And I use the following VARIABLE spread data
            | SpreadH1   | SpreadH1Basis   | SpreadH2   | SpreadH2Basis   | FinalSpread   | FinalSpreadBasis   | OverwriteSpreadH1Comment   | OverwriteSpreadH2Comment   | OverwriteSpreadMismatchComment   | OverwriteFinalSpreadComment   | OverwriteDealRemark   |
            | <SpreadH1> | <SpreadH1Basis> | <SpreadH2> | <SpreadH2Basis> | <FinalSpread> | <FinalSpreadBasis> | <OverwriteSpreadH1Comment> | <OverwriteSpreadH2Comment> | <OverwriteSpreadMismatchComment> | <OverwriteFinalSpreadComment> | <OverwriteDealRemark> |
        When I fill in the VARIABLE create offer with all the data
        And I choose won and complete the bill of closure phase

        Examples: for VARIABLE
            | TC | TestUser     | CounterParty          | Notional | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | SpreadCheckBy | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | SpreadH2 | SpreadH2Basis | FinalSpread | FinalSpreadBasis | OverwriteFinalSpreadComment | OverwriteDealRemark | CddReviewDate | DealMoment | Guarantor1 | AdditEmail1 | AdditEmail2 | SpreadCheckRemarks | OfferReference | OverwriteSpreadH1Comment | OverwriteSpreadH2Comment | OverwriteSpreadMismatchComment |
            | D  | AccountDesk1 | Trx Gemeente Meerdijk | 1000000  |                        |                   |               | false         | 30-01-2029 | 30-04-2035   |                 |                |                | 30        | 3mE           | false       | 11       | 5             | 11       | 5             | 11          | 5                |                             |                     |               |            |            |             |             |                    |                |                          |                          |                                |

    Scenario: VARIABLE floating loan creation
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a VARIABLE case with the following OfferData
            | TC   | Notional   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   | CreditLimit   | CddReviewDate   | DealMoment   | Guarantor1   | AdditEmail1   | AdditEmail2   | SpreadCheckBy   | SpreadCheckRemarks             |
            | <TC> | <Notional> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> | <CreditLimit> | <CddReviewDate> | <DealMoment> | <Guarantor1> | <AdditEmail1> | <AdditEmail2> | <SpreadCheckBy> | <OverWriteSpreadkCheckRemarks> |
        And I use the following VARIABLE alternative data
            | OfferReference   | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   |
            | <OfferReference> | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> |
        And I use the following VARIABLE spread data
            | SpreadH1   | SpreadH1Basis   | SpreadH2   | SpreadH2Basis   | FinalSpread   | FinalSpreadBasis   | OverwriteSpreadH1Comment   | OverwriteSpreadH2Comment   | OverwriteSpreadMismatchComment   | OverwriteFinalSpreadComment   | OverwriteDealRemark   |
            | <SpreadH1> | <SpreadH1Basis> | <SpreadH2> | <SpreadH2Basis> | <FinalSpread> | <FinalSpreadBasis> | <OverwriteSpreadH1Comment> | <OverwriteSpreadH2Comment> | <OverwriteSpreadMismatchComment> | <OverwriteFinalSpreadComment> | <OverwriteDealRemark> |
        When I fill in the VARIABLE create offer with all the data
        And I choose won and complete the bill of closure phase
        #When I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
        #And I download the "Cover Letter" file
        #And I download the "WSW Contract" file
        #And I download the "Cover Letter" file
        #And I download the "WSW Summary" file


        Examples: for VARIABLE
            | TC | TestUser     | Notional | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | SpreadCheckBy | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ReferenceRate | HoursClause | SpreadH1 | SpreadH1Basis | SpreadH2 | SpreadH2Basis | FinalSpread | FinalSpreadBasis | OverwriteFinalSpreadComment | OverwriteDealRemark | CreditLimit | CddReviewDate | DealMoment | Guarantor1 | AdditEmail1 | AdditEmail2 | SpreadCheckRemarks | OfferReference | OverwriteSpreadH1Comment | OverwriteSpreadH2Comment | OverwriteSpreadMismatchComment |
            | E  | AccountDesk1 |          | false                  | 25                | 10            | false         | 30-01-2029 | 30-06-2045   |                 |                | 30-07-2032     | 30        | 6mE           | false       | 2        | 2             | 2        | 2             | 2           | 2                |                             |                     | 99999999    |               |            |            |             |             |                    |                |                          |                          |                                |


    Scenario: SHORT loan creation
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan short" and event "Primary"
        And I create a SHORT case with the following OfferData
            | TC   | Notional   | CreditLimit   | CddReviewDate   | DealMoment   | Guarantor1   | AdditEmail1   | AdditEmail2   | SpreadCheckBy   | SpreadCheckRemarks             |
            | <TC> | <Notional> | <CreditLimit> | <CddReviewDate> | <DealMoment> | <Guarantor1> | <AdditEmail1> | <AdditEmail2> | <SpreadCheckBy> | <OverWriteSpreadkCheckRemarks> |
        And I use the SHORT VARIABLE alternative data
            | OfferReference   | StartDate   | MaturityDate   |
            | <OfferReference> | <StartDate> | <MaturityDate> |
        And I use the following SHORT spread data
            | PriceH1   | PriceH2   | PriceFinal   | Margin   | OverwriteSpreadH1Comment   | OverwriteSpreadH2Comment   | OverwriteSpreadMismatchComment   | OverwriteFinalSpreadComment   | OverwriteDealRemark   |
            | <PriceH1> | <PriceH2> | <PriceFinal> | <Margin> | <OverwriteSpreadH1Comment> | <OverwriteSpreadH2Comment> | <OverwriteSpreadMismatchComment> | <OverwriteFinalSpreadComment> | <OverwriteDealRemark> |
        When I fill in the SHORT create offer with all the data
        And I choose won and complete the bill of closure phase
        #When I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
        #And I download the "DEPLO.csv" file

        Examples: for SHORT
            | TC | TestUser     | Notional | Guarantor1 | SpreadCheckBy | StartDate  | MaturityDate | PriceH1 | PriceH2 | PriceFinal | Margin | OverwriteFinalSpreadComment | OverwriteDealRemark | CreditLimit | CddReviewDate | DealMoment | AdditEmail1 | AdditEmail2 | SpreadCheckRemarks | OfferReference | OverwriteSpreadH1Comment | OverwriteSpreadH2Comment | OverwriteSpreadMismatchComment |
            | A  | AccountDesk1 |          |            | false         | today      | 02-01-2027   | 2       | 2       | 2          | 5      | TESTING OVERWRITE           |                     | 99999999    |               |            |             |             |                    | TESTREF        |                          |                          |                                |
            | B  | AccountDesk1 |          |            | true          | 06-04-2026 | 01-04-2027   | 2       | 2       | 2          | 5      | TESTING OVERWRITE           |                     | 99999999    |               |            |             |             | TESTING            |                |                          |                          |                                |



