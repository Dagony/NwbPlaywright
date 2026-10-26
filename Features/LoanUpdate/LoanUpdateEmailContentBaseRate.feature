@LoanUpdate
Feature: LoanUpdateEmailContentBaseRate

    Scenario: TC01A BASERATE WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | MinimalWithdrawal   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <MinimalWithdrawal> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | A  | AccountDesk1 | 1 0027767    | 1 0027767             | Trx Stichting Greendale Wonen       | Stichting Waarborgfonds Sociale Woningbouw |          | 20.000.000,00    | 01-03-2027 | 16-06-2087   | Weekly             | 06-01-2028      |                | 02-12-2030     | Monday    | ACT365           | 1wE           | 0,5      | 0,5           | 0,75        | 0,75             |                       |                     | 3,545    |

    @BekijkenVoorVerwijdering
    Scenario: TC01B BASERATE WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | MinimalWithdrawal   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <MinimalWithdrawal> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedGuarantor1                         | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | B  | Treasury1 | 1 0027769    | 1 0027769             | Trx Stichting Zorggroep Heilig Hart | Stichting Waarborgfonds voor de Zorgsector |          | 16.000.000,00    | 01-03-2027 | 16-06-2087   | Weekly             | 02-12-2027      | 30-11-2037     | 07-12-2037     | Monday    | A365             | 1wE           | 1,25     | -0,25         | 1,25        | -0,25            |                       |                     | 3,73     |
            | C  | Treasury1 | 1 0027777RH4 | 1 0027777             | Trx Dunder Mifflin Paper Company    |                                            |          | 26.000.000,00    | 19-06-2028 | 19-06-2087   | Annual             |                 |                |                | 19        | EBOND            | 1mE           | 1        | 1             | 10          | 10               | Deal Remark voor TC C | Offer ref voor TC C | 4,395    |


    Scenario: TC02A BASERATE NORMAL clients | Non Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser     | SecID         | ExpectedOriginalSecID | CounterParty                     | ExpectedGuarantor1    | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | D  | AccountDesk1 | 1W0026221RH4  | 1 0026221             | Trx Gemeente Meerdijk            |                       |          | 6.875.000,44     | 15-06-2028 | 15-06-2088   | Annual             |                 |                |                | 16        | Act/act          | 1mE           | 1        | 1             | 10          | 10               | Deal Remark voor TC D | Offer ref voor TC D | 1,1      |


    @BekijkenVoorVerwijdering
    Scenario: TC02B BASERATE NORMAL clients | Non Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Base rate loan" and event "Spread reset"
        And I create a baserate loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedGuarantor1   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedGuarantor1> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a baserate loan update with the following alternative specifics
            | StartDate   | MaturityDate   | ExpectedCouponFreq   | FirstCouponDate   | LastCouponDate   | InterestRRDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | BaseRate   | ExpectedDayCount   |
            | <StartDate> | <MaturityDate> | <ExpectedCouponFreq> | <FirstCouponDate> | <LastCouponDate> | <InterestRRDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <BaseRate> | <ExpectedDayCount> |
        When I press submit an email is generated
        Then the base rate offer email table should match the expected table
        And the displayed base rate email text should match the expected text

        Examples:
            | TC | TestUser  | SecID         | ExpectedOriginalSecID | CounterParty                     | ExpectedGuarantor1    | Notional | ExpectedNotional | StartDate  | MaturityDate | ExpectedCouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | ExpectedDayCount | ReferenceRate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | DealRemark            | OfferReference      | BaseRate |
            | E  | Treasury1 | 1W0027040RH4  | 1 0027040             | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk |          | 3.746.249,98     | 15-06-2028 | 15-06-2088   | Monthly            |                 |                |                | 15        | A365             | 3mE           | -22      | -22           | 4,33        | 55,55            |                       | Offer ref voor TC E | 3,43     |
            | F  | Treasury1 | 1W0027043RH2  | 1 0027043             | Trx Dunder Mifflin Paper Company |                       |          | 4.000.000,00     | 15-06-2028 | 15-06-2088   | Quarterly          | 30-06-2028      |                |                | 15        | EBOND            | 6mE           | -22      | 23            | -5,75       | -45,75           |                       |                     | 3,5      |
            | G  | Treasury1 | 1W0027044RH4  | 1 0027044             | Trx Waterschap Groot Waterland   |                       |          | 25.000.000,00    | 15-06-2028 | 15-06-2088   | Quarterly          |                 | 18-02-2088     |                | 15        | Act/act          | 12mE          | 17,50    | 17,50         | 15,70       | 17,35            |                       |                     | 4,742    |
            | H  | Treasury1 | 1 0027694     | 1 0027694             | Trx Dunder Mifflin Paper Company |                       |          | 4.000.000,00     | 15-06-2028 | 15-06-2088   | Monthly            | 30-06-2028      |                |                | 15        | A360             | 6mE           | -22      | 23            | -5,75       | -45,75           |                       |                     | 2,925    |
            | I  | Treasury1 | 1 0026799RH3C | 1 0026799             | Trx Dunder Mifflin Paper Company |                       |          | 6.875.000,44     | 15-06-2028 | 08-04-2069   | Weekly             |                 | 18-02-2069     |                | Thursday  | EBOND            | 1wE           | 17,50    | 17,50         | 15,70       | 17,35            |                       |                     | 1,1      |
