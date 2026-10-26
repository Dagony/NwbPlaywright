@LoanUpdate
Feature: LoanUpdateCoverLetter

    A short summary of the feature

    Scenario: TC01A VAR variable hoofdsom Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   | ExpectedOriginalSecID   | ExpectedContractStartDate   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> | <ExpectedOriginalSecID> | <ExpectedContractStartDate> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file

        Examples:
            | TC | TestUser         | SecID         | ExpectedOriginalSecID | CounterParty                        | Notional | ExpectedNotional | Guarantor1                          | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedContractStartDate | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | A  | AccountDesk1     | 2 0029698     | 2 0029698             | Trx Stichting Greendale Wonen       |          | 77777,42         | Trx Stichting Greendale Wonen       | false                  | 10,50             | 10            | 01-12-2021                | 19-06-2027 | 26-06-2049   | 1wE           | true        | 28-08-2027      | 10-06-2039     | 11-06-2039     | Friday    | EBOND            | 1        | 1             | 1                | 1           | 0.02         |

    @BekijkenVoorVerwijdering
    Scenario: TC01B VAR variable hoofdsom Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan floating notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   | ExpectedOriginalSecID   | ExpectedContractStartDate   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> | <ExpectedOriginalSecID> | <ExpectedContractStartDate> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        Examples:
            | TC | TestUser         | SecID         | ExpectedOriginalSecID | CounterParty                        | Notional | ExpectedNotional | Guarantor1                          | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedContractStartDate | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | B  | TreasuryManager1 | 2 0029818WZ1D | 2 0029818             | Trx Stichting Zorggroep Heilig Hart |          | 100000,00        | Trx Stichting Zorggroep Heilig Hart | false                  | 25,55             | 20            | 01-12-2021                | 19-06-2027 | 09-03-2033   | 1mE           |             | 28-08-2027      | 30-08-2029     | 31-08-2029     | 15        | A360             | 2        | 2             | -2               | -2          | -0.04        |

    @BekijkenVoorVerwijdering
    Scenario: TC02 VAR fixed hoofdsom Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Floating rate loan fixed notional" and event "Spread reset"
        And I create a variable loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | MinimalWithdrawal   | CommitmentFee   | SetNotionalTypeToFixed   | ExpectedOriginalSecID   | ExpectedContractStartDate   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <MinimalWithdrawal> | <CommitmentFee> | <SetNotionalTypeToFixed> | <ExpectedOriginalSecID> | <ExpectedContractStartDate> |
        And I create a variable loan update with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | CustomerRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <CustomerRate> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser | SecID     | ExpectedOriginalSecID | CounterParty                        | Notional | ExpectedNotional | Guarantor1                          | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | ExpectedContractStartDate | StartDate  | MaturityDate | ReferenceRate | HoursClause | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | SpreadH1 | SpreadH1Basis | FinalSpreadBasis | FinalSpread | CustomerRate |
            | C  | Treasury1 | 1 0031562 | 1 0031562             | Trx Stichting Zorggroep Heilig Hart |          | 95753,00         | Trx Stichting Zorggroep Heilig Hart | true                   |                   |               | 1-12-2020                 | 30-08-2028 | 30-08-2038   | 12mE          |             |                 |                |                | 30        | A360             | 25,75    | 25,75         | 25,75            | 25,75       | 0.515        |

    @BekijkenVoorVerwijdering
    Scenario: TC03 Fixed Coverletter Waterschap
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser  | SecID         | ExpectedOriginalSecID | Notional | ExpectedNotional | CounterParty                   | ExpectedContractStartDate | StartDate | MaturityDate | AmortType | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq | DayCount | NumbOfAmort | AmortFreq | SpreadH1 | FinalSpread | MarketIRate |
            | D  | Treasury1 | 1 0026171RH6C | 1 0026171             |          | 77777,42         | Trx Waterschap Groot Waterland | 1-12-2020                 | today     | 03-9-2027    | Zero      |                 | 02-9-2027      |                | 03        | Annual     | BOND     | 55          | Annual    | 1        | 9,4         | 7           |

    Scenario: TC04A Fixed Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractStartDate   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractStartDate> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser     | SecID         | ExpectedOriginalSecID | Notional | ExpectedNotional | CounterParty                        | ExpectedContractStartDate | StartDate | MaturityDate | AmortType      | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq  | DayCount | NumbOfAmort | AmortFreq | SpreadH1 | FinalSpread | MarketIRate |
            | E  | AccountDesk1 | 1W0026170RH9B | 1W0026170             |          | 5254175,95       | Trx Stichting Greendale Wonen       | 1-12-2021                 | 31-8-2027 | 30-8-2039    | Annuity        | 01-09-2027      |                | 31-08-2029     | 31        | Semi annual | EBOND    | 20          | Annual    | 1        | 7,1         | -3,123      |

    @BekijkenVoorVerwijdering
    Scenario: TC04B Fixed Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   | ExpectedContractStartDate   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> | <ExpectedContractStartDate> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap   | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | DayCount   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <Swap> | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <DayCount> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser     | SecID         | ExpectedOriginalSecID | Notional | ExpectedNotional | CounterParty                        | ExpectedContractStartDate | StartDate | MaturityDate | AmortType      | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | CouponFreq  | DayCount | NumbOfAmort | AmortFreq | SpreadH1 | FinalSpread | MarketIRate |
            | F  | Treasury1    | 1W0026171ARH3 | 1W0026171             |          | 25000,50         | Trx Stichting Zorggroep Heilig Hart | 1-12-2020                 | 31-8-2026 | 31-8-2038    | Linear balloon |                 | 27-08-2038     |                | 31        | Quarterly   | ACTUAL   | 40          | Quarterly | 1        | 8,5         | 4,12345678  |

    Scenario: TC05 Baserate Coverletter WSW/WFZ
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Interest reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #	Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser     | SecID        | ExpectedOriginalSecID | CounterParty                  | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | G  | AccountDesk1 | 1 0027767    | 1 0027767             | Trx Stichting Greendale Wonen | Stichting Waarborgfonds Sociale Woningbouw |          | 20.000.000,00    | 01-03-2027 | 19-06-2087   | Weekly             | 06-01-2028      |                | 02-12-2030     | Monday    | ACT365           | 1wE           | 0,5      | 0,5           | 0,75        | 0,75             |                       |                     | 3,123456 |
            | H  | AccountDesk1 | 1W0026221RH4 | 1 0026221             | Trx Gemeente Meerdijk         |                                            |          | 6.875.000,44     | 15-06-2028 | 15-06-2088   | Annual             |                 |                |                | 15        | Act/act          | 1mE           | 1        | 1             | 10          | 10               | Deal Remark voor TC G | Offer ref voor TC G | 1,10     |

    @BekijkenVoorVerwijdering
    Scenario: TC06 Baserate Coverletter Waterschap
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Interest reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <ExpectedDayCount> |
        When I continue the loan update case to BO first flow LoanDataApproval
        And I download the "Cover Letter RH" file
        #	Then the assertions for the loan update Cover Letter is valid

        Examples:
            | TC | TestUser  | SecID        | ExpectedOriginalSecID | CounterParty                   | ExpectedGuarantor1 | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark | OfferReference | BaseRate |
            | I  | Treasury1 | 1W0027044RH4 | 1 0027044             | Trx Waterschap Groot Waterland |                    |          | 25.000.000,00    | 15-06-2028 | 15-06-2088   | Quarterly          |                 | 18-02-2088     |                | 15        | Act/act          | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            |            |                | 4,742    |





