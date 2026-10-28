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