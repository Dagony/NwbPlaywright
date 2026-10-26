@Variable
Feature: UGsheetVariable
    For technical details about the columns see FO Variable UGsheet loan document.

    Scenario: TC01 VARIABLE UG SHEET | Municipality with Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1                                 | Notional   | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | A  | AccountDesk1 | Stichting Waarborgfonds Sociale Woningbouw | 125125,87  | 17-10-2028 | 01-04-2069   | 19-10-2028      | Monday    | 1wE           | True        | False                  | 15,00             | 33,75         | 17-10-2035           | 1        | 1             | 10,00       | 10               | 20           | Deal Remark for TC A |
            | B  | AccountDesk1 | Waarborgfonds voor de Zorgsector           | 2768342,00 | 17-10-2028 | 31-10-2069   | 19-10-2028      | 31        | 1mE           |             | False                  | 25,55             |               | 17-10-2035           | 1        | 1             | 540,00      | -547,5           | -7,5         |                      |

    Scenario: TC02 VARIABLE UG SHEET | Municipality without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan floating notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1 | Notional | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | C  | AccountDesk1 |            | 32412,00 | 03-03-2027 | 01-04-2069   | 06-04-2045      | 1         | 3mE           | False                  | 55                |               | 5,78                 | 1        | 1             | -4,59       | 7,59             | 3            | Deal Remark for TC C |

    @BekijkenVoorVerwijdering
    Scenario: TC03A VARIABLE UG SHEET | Waterschap with Advisory - PF
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Waterschap Groot Waterland" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser     | Guarantor1                                 | Notional  | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark           |
            | D  | AccountDesk1 | Stichting Waarborgfonds Sociale Woningbouw | 4663,45   | 01-06-2028 | 19-06-2087   | 21-10-2030      | 19        | 6mE           |             | True                   |                   |               | 12-05-2050           | 1        | 1             | 98,75       | -94,75           | -94,75       |                      |
            | E  | Treasury1 | Waarborgfonds voor de Zorgsector           | 573223,00 | 15-06-2028 | 19-06-2087   | 21-10-2030      | 19        | 12mE          |             | True                   |                   |               | 12-05-2050           | 1        | 1             | -122,95     | 120,95           | -2,00        | Deal Remark for TC E |

    @BekijkenVoorVerwijdering
    Scenario: TC03B VARIABLE UG SHEET | Waterschap with Advisory - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Waterschap Groot Waterland" with "1" random counterparty individual(s) and "1" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser  | Guarantor1                       | Notional     | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark |
            | F  | Treasury1 | Trx Dunder Mifflin Paper Company | 100000000,00 | 01-03-2027 | 01-04-2069   |                 | Monday    | 1wE           | True        | True                   |                   |               |                      | 1        | 1             | 15,00       | 15,6             | 30,6         |            |

    @BekijkenVoorVerwijdering
    Scenario: TC04 VARIABLE UG SHEET | Waterschap without Advisory
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Dunder Mifflin Paper Company" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        And I create a variable loan with the following case and offer specifics
            | TC   | CounterParty   | Guarantor1   | Notional   | OfferReference   | SetNotionalTypeToFixed   | SetNotionalTypeToFixed   | MinimalWithdrawal   | CommitmentFee   |
            | <TC> | <CounterParty> | <Guarantor1> | <Notional> | <OfferReference> | <SetNotionalTypeToFixed> | <SetNotionalTypeToFixed> | <MinimalWithdrawal> | <CommitmentFee> |
        And I create a variable loan with the following alternative specifics
            | StartDate   | MaturityDate   | FirstCouponDate   | CouponDay   | ReferenceRate   | HoursClause   | SpreadH1   | SpreadH1Basis   | FinalSpread   | FinalSpreadBasis   | DealRemark   | CustomerRate   |
            | <StartDate> | <MaturityDate> | <FirstCouponDate> | <CouponDay> | <ReferenceRate> | <HoursClause> | <SpreadH1> | <SpreadH1Basis> | <FinalSpread> | <FinalSpreadBasis> | <DealRemark> | <CustomerRate> |
        When I open the bill of closure phase
        And I click copy UG sheet and I insert it into an excel file for "VARIABLE" loan type
        Then the values should match the expected values for the VARIABLE UGSHEET

        Examples:
            | TC | TestUser  | Guarantor1 | Notional  | StartDate  | MaturityDate | FirstCouponDate | CouponDay | ReferenceRate | HoursClause | SetNotionalTypeToFixed | MinimalWithdrawal | CommitmentFee | InterestSpreadRRDate | SpreadH1 | SpreadH1Basis | FinalSpread | FinalSpreadBasis | CustomerRate | DealRemark |
            | G  | Treasury1 |            | 623423,00 | 01-03-2027 | 01-04-2069   |                 | Monday    | 1wE           | False       | True                   |                   |               |                      | 1        | 1             | 15,00       | 15,65            | 30,65        |            |




















#Scenario: DEPRECATED!!!!!!!! TC01 Creating different types of fixed loan UG sheet copies.
#	Given I chose counterparty "<Counterparty>" with advisory "<AdvisoryOrg>"
#	Given I create a variable loan with the following properties
#		| Field                  | Value                    |
#		| TC                     | <TC>                     |
#		| SetGuarantor           | <SetGuarantor>           |
#		| Guarantor1             | <Guarantor1>             |
#		| Notional               | <Notional>               |
#		| ReferenceRate          | <ReferenceRate>          |
#		| StartDate              | <StartDate>              |
#		| MaturityDate           | <MaturityDate>           |
#		| FirstCouponDate        | <FirstCouponDate>        |
#		| InterestSpreadRRDate   | <InterestSpreadRRDate>   |
#		| NotionalTypeIsVariable | <NotionalTypeIsVariable> |
#		| MinimalWithdrawal      | 25,95                    |
#		| CommitmentFee          | 10                       |
#		| FinalSpreadBasis       | <FinalSpreadBasis>       |
#		| FinalSpread            | <FinalSpread>            |
#		| CustomerRate           | <CustomerRate>           |
#		| DealRemark             | <DealRemark>             |
#	When I open the bill of closure phase
#	And I click copy UG sheet and I insert it into an excel file for Variable loan type
#	#Then The case will be paused at BoC phase to exert copy function manually
#Examples:
#	| TC | Counterparty       | AdvisoryOrg                    | SetGuarantor | Guarantor1                                 | Notional  | StartDate  | MaturityDate | FirstCouponDate | ReferenceRate | NotionalTypeIsVariable | InterestSpreadRRDate | FinalSpreadBasis | FinalSpread | MarketIRate | DealRemark           |
#	| A  | Gemeente Amsterdam | Thesor Treasury Outsourcing BV | True         | Stichting Waarborgfonds Sociale Woningbouw | 125125,87 | 17-10-2025 | 01-04-2069   | 19-10-2026      | 1wE           | True                   | 17-10-2035           | 10               | 10          | 20          | Deal Remark for TC A |
#	| B  | Gemeente Amsterdam | Thesor Treasury Outsourcing BV | True         | Waarborgfonds voor de Zorgsector           | 2768342   | 17-10-2025 | 31-10-2069   | 19-10-2026      | 1mE           | True                   | 17-10-2035           | 540,0            | -547,5      | -7,5        |                      |
#	| C  | Gemeente Amsterdam | empty                          | False        | empty                                      | 32412     | 03-03-2025 | 01-04-2069   | 06-04-2045      | 3mE           | True                   |                      | -4,59            | 7,59        | 3           | Deal Remark for TC C |
#	| D  | Wetterskip Fryslan | Thesor Treasury Outsourcing BV | True         | Stichting Waarborgfonds Sociale Woningbouw | 4663,45   | 01-06-2028 | 19-06-2087   | 21-10-2030      | 6mE           | False                  | 12-05-2050           | 98,75            | -94,75      | -94,75      |                      |
#	| E  | Wetterskip Fryslan | Thesor Treasury Outsourcing BV | True         | Waarborgfonds voor de Zorgsector           | 573223    | 15-06-2028 | 19-06-2087   | 21-10-2030      | 12mE          | False                  | 12-05-2050           | -122,95          | 120,95      | -2,00       | Deal Remark for TC E |
#	| F  | Wetterskip Fryslan | empty                          | True         | Provincie Zuid-Holland                     | 623423    | 03-03-2025 | 01-04-2069   |                 | 1wE           | False                  |                      | 15               | 15,6        | 30,6        |                      |

#Examples (outdated):
#	| TC | Counterparty               | AdvisoryOrg         | Guarantor                                  | Notional  | StartDate  | MaturityDate | FirstCouponDate | ReferenceRate | InterestSpreadRRDate | FinalSpreadBasis | FinalSpread | MarketIRate | DealRemark           |
#	| A  | Gemeente Amsterdam         | Gerhold en partners | Stichting Waarborgfonds                    | 125125,87 | 17-10-2025 | 01-04-2069   | 19-10-2026      | 1wE           | 17-10-2035           |                  | 10          | 1           | Deal Remark for TC A |
#	| C  | Gemeente Amsterdam         | empty               | empty                                      | 32412     | 03-03-2025 | 01-04-2069   | 06-04-2045      | 3mE           |                      |                  | 7,59        | 3           | Deal Remark for TC C |
#	| D  | Waterschap Brabantse Delta | LSZ De Groot BV     | Stichting Waarborgfonds                    | 4663,45   | 01-06-2028 | 19-06-2087   | 21-10-2030      | 6mE           | 12-05-2050           |                  | 98,75       | 4           |                      |
#	| E  | Waterschap Brabantse Delta | LSZ De Groot BV     | Stichting Waarborgfonds voor de Zorgsector | 573223    | 15-06-2028 | 19-06-2087   | 21-10-2030      | 12mE          | 12-05-2050           |                  | 127,95      | 5           | Deal Remark for TC E |
#	| F  | Waterschap Brabantse Delta | empty               | Provincie Zuid-Holland                     | 623423    | 03-03-2025 | 01-04-2069   |                 | 1wE           |                      |                  | 15,6        | 6           |                      |
#	| G  | Gemeente Amsterdam         | Gerhold en partners | Stichting Waarborgfonds                    | 125125,87 | 17-10-2025 | 02-05-2069   |                 | 1mE           | 17-10-2035           |                  | 10          | 1           | Deal Remark for TC G |
#	| H  | Gemeente Amsterdam         | Gerhold en partners | Stichting Waarborgfonds voor de Zorgsector | 2768342   | 17-10-2025 | 31-10-2069   |                 | 3mE           | 17-10-2035           |                  | 550         | 2,5         |                      |
#	| I  | Gemeente Amsterdam         | empty               | empty                                      | 32412     | 03-03-2025 | 01-04-2069   |                 | 6mE           |                      |                  | 7,59        | 3           | Deal Remark for TC I |
#	| J  | Waterschap Brabantse Delta | LSZ De Groot BV     | Stichting Waarborgfonds                    | 4663,45   | 01-06-2028 | 19-06-2087   |                 | 12mE          | 12-05-2050           |                  | 98,75       | 4           |                      |
#	| K  | Waterschap Brabantse Delta | LSZ De Groot BV     | Stichting Waarborgfonds voor de Zorgsector | 573223    | 15-06-2028 | 19-06-2087   |                 | 1wE           | 12-05-2050           |                  | 127,95      | 5           | Deal Remark for TC K |