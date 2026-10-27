@Shared
Feature: WfZContract


    WfZ contracts are only generated for WfZ clients (non WSW, non normal clients).
#
#    @BekijkenVoorVerwijdering
#    Scenario: TC01 Create WfZ Long
#        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
#        And  I select counterparty "<CounterParty>" with "3" random counterparty individual(s)
#        #	Given I select counterparty "Stichting Haaglanden Medisch Centrum" with individual(s) "FirstName de LastName817127"
#        And I select PEGA task "Loan"
#        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
#        And I create a fixed loan with the following case and offer specifics
#            | TC   | CounterParty   | Guarantor1   | Notional   |
#            | <TC> | <CounterParty> | <Guarantor1> | <Notional> |
#        And I create a fixed loan with the following alternative specifics
#            | StartDate   | MaturityDate   | FirstCouponDate   | LastCouponDate   | CouponDay   | InterestRRDate   | AmortType   | NumbOfAmort   | AmortFreq   | CouponFreq   | Swap   | DayCount   | SpreadH1   | FinalSpread   | MarketIRate   | CustomerRate   |
#            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <LastCouponDate> | <CouponDay> | <InterestRRDate> | <AmortType> | <NumbOfAmort> | <AmortFreq> | <CouponFreq> | <Swap> | <DayCount> | <SpreadH1> | <FinalSpread> | <MarketIRate> | <CustomerRate> |
#        When I choose won and complete the bill of closure phase
#        And I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract
#        And I download the "WfZ Contract" file
#        #Then the NWB Contract should be generated correctly
#        Examples:
#            | TC | TestUser  | CounterParty                        | Guarantor1 | Notional    | AmortType | NumbOfAmort | AmortFreq   | CouponFreq  | Swap  | DayCount | StartDate  | MaturityDate | FirstCouponDate | LastCouponDate | InterestRRDate | CouponDay | SpreadH1 | FinalSpread | MarketIRate | CustomerRate |
#            | A  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 85550455,75 | Bullet    |             |             | Annual      | False | Act/Act  | 05-01-2027 | 20-01-2050   | 06-01-2027      | 18-01-2043     | 19-01-2043     | 10        | 15       | 17,94       | 4           | 4,1794       |
#            | B  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 255015      | Linear    | 10          | Semi annual | Semi annual | False | Act/360  | 01-09-2027 | 01-09-2073   |                 |                | 01-09-2055     | 1         | 15       | 20          | 3,66        | 3,8600       |
#            | C  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 1000000     | Annuity   | 20          | Quarterly   | Quarterly   | False | 30/360   | 05-01-2027 | 05-01-2050   |                 |                |                | 5         | 5,75     | 5,75        | -2          | -1,9425      |
#            | D  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 15250750    | Manual    | 30          | Monthly     | Monthly     | False | Act/360  | 05-01-2027 | 05-01-2039   |                 |                |                | 5         | 20       | 20          | 3,66        | 3,8600       |
#            | E  | Treasury1 | Trx Stichting Zorggroep Heilig Hart |            | 875000      | Zero      | 40          | Weekly      | Weekly      | False | 30/360   | 19-11-2027 | 19-12-2087   |                 |                | 24-11-2051     | Friday    | 5,75     | 5,75        | -2          | -1,9425      |
#
