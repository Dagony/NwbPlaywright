@LoanUpdate
Feature: LoanUpdateEmailContentFixed

    Scenario: TC01A Fixed WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s) and "3" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap  | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | False | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedLoanUpdateGuarantor                | Notional | ExpectedNotional | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | ExpectedDayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | A  | AccountDesk1 | 1 0027561RH  | 1 0027561             | Trx Stichting Greendale Wonen       | Stichting Waarborgfonds Sociale Woningbouw |          | 70.000.000,00    | Bullet    |           |             | 01-12-2031 | 06-07-2037   | Weekly     |                 |                | 04-12-2034     | Monday    | True  | ACT365           | 0,5      | 0,75        |            |                | 1,5         | 1,508        |

    @BekijkenVoorVerwijdering
    Scenario: TC01B Fixed WSW WFZ Clients | Bullet
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s) and "3" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap  | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | False | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser  | SecID        | ExpectedOriginalSecID | CounterParty                        | ExpectedLoanUpdateGuarantor                | Notional | ExpectedNotional | AmortType | AmortFreq | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | ExpectedDayCount | SpreadH1 | FinalSpread | DealRemark | OfferReference | MarketIRate | CustomerRate |
            | B  | Treasury1 | 1 0028262WZ1 | 1 0028262             | Trx Stichting Zorggroep Heilig Hart | Stichting Waarborgfonds voor de Zorgsector |          | 50.000,00        | Bullet    |           |             | 01-12-2031 | 01-12-2051   | Weekly     | 02-12-2031      | 09-12-2031     | 03-12-2035     | Wednesday | False | A365             | 1,25     | 85          |            |                | 3           | 3,850        |
            | C  | Treasury1 | 1 0028339TR3 | 1 0028339             | Trx Dunder Mifflin Paper Company    |                                            |          | 15.100.200,00    | Bullet    |           |             | 01-12-2031 | 01-12-2051   | Annual     | 02-12-2031      | 09-12-2031     |                | 22        | False | EBOND            | 33       | 53          |            |                | 1           | 1,530        |


    Scenario: TC02A Fixed Normal Clients | Linear, Annuity etc
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap  | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | False | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser     | SecID          | ExpectedOriginalSecID | CounterParty                     | ExpectedLoanUpdateGuarantor | Notional | ExpectedNotional | AmortType       | AmortFreq   | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | ExpectedDayCount | SpreadH1 | FinalSpread | DealRemark            | OfferReference      | MarketIRate | CustomerRate |
            | D  | AccountDesk1 | 1 0028569RHWZ1 | 1 0028569             | Trx Gemeente Meerdijk            |                             |          | 25.000.000,00    | Linear          | Annual      | 6           | 08-01-2032 | 08-01-2038   | Annual     |                 |                |                | 8         | False | ACTUAL           | 0,5      | 5           | Deal Remark voor TC D | Offer ref voor TC D | 1           | 1,050        |

    @BekijkenVoorVerwijdering
    Scenario: TC02B Fixed Normal Clients | Linear, Annuity etc
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a fixed loan update with the following case and offer specifics
            | TC   | CounterParty   | ExpectedLoanUpdateGuarantor   | Notional   | ExpectedNotional   | OfferReference   | ExpectedOriginalSecID   |
            | <TC> | <CounterParty> | <ExpectedLoanUpdateGuarantor> | <Notional> | <ExpectedNotional> | <OfferReference> | <ExpectedOriginalSecID> |
        And I create a fixed loan update with the following alternative specifics
            | StartDate   | MaturityDate   | CouponFreq   | FirstCouponDate   | LastCouponDate   | CouponDay   | Swap  | InterestRRDate   | AmortType   | AmortFreq   | NumbOfAmort   | ExpectedDayCount   | SpreadH1   | FinalSpread   | MarketIRate   | MarketIRate   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <CouponFreq> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | False | <InterestRRDate> | <AmortType> | <AmortFreq> | <NumbOfAmort> | <ExpectedDayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <MarketIRate> | <CustomerRate> |
        When I press submit an email is generated
        Then the fixed offer email table should match the expected table
        And the displayed fixed email text should match the expected text

        Examples:
            | TC | TestUser | SecID          | ExpectedOriginalSecID | CounterParty                     | ExpectedLoanUpdateGuarantor | Notional | ExpectedNotional | AmortType       | AmortFreq   | NumbOfAmort | StartDate  | MaturityDate | CouponFreq | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | Swap  | ExpectedDayCount | SpreadH1 | FinalSpread | DealRemark            | OfferReference      | MarketIRate | CustomerRate |
            | E  | Treasury1 | 1 0031960      | 1 0031960             | Trx Dunder Mifflin Paper Company | Trx Gemeente Meerdijk       |          | 1.500.450,00     | Annuity         | Monthly     | 1           | 19-01-2032 | 19-02-2032   | Monthly    |                 |                |                | 19        | True  | A365             | 1,25     | 15,75       | Deal Remark voor TC E | Offer ref voor TC E | 1,5         | 1,658        |
            | F  | Treasury1 | 1W0015300RH2   | 1 0015300             | Trx Dunder Mifflin Paper Company |                             |          | 9.999.999,00     | Linear balloon  | Quarterly   | 268         | 09-01-2032 | 19-02-2093   | Quarterly  | 30-01-2032      |                |                | 19        | False | EBOND            | -5,75    | -5,75       |                       | Offer ref voor TC F | 3           | 2,942        |
            | G  | Treasury1 | 1W0020387      | 1 0020387             | Trx Waterschap Groot Waterland   |                             |          | 50.000.000,00    | Annuity balloon | Quarterly   | 268         | 19-01-2032 | 20-02-2093   | Quarterly  |                 | 20-02-2032     |                | 19        | False | ACTUAL           | -6       | -5          |                       | Offer ref voor TC G | 4,12        | 4,070        |
            | H  | Treasury1 | 1 0029993      | 1 0029993             | Trx Dunder Mifflin Paper Company |                             |          | 100.000,00       | Manual          | Semi annual | 64          | 19-01-2032 | 19-02-2058   | Monthly    |                 |                | 19-02-2042     | 19        | False | A360             | 0,5      | -50         |                       | Offer ref voor TC H | 0,05        | -0,450       |
            | I  | Treasury1 | 1 0029916B     | 1 0029916             | Trx Dunder Mifflin Paper Company |                             |          | 25.255.475,00    | Zero            | Weekly      | 1792        | 19-01-2032 | 19-02-2057   | Weekly     |                 |                |                | Monday    | False | EBOND            | 1,25     | 25          |                       | Offer ref voor TC I | -0,1        | 0,150        |
