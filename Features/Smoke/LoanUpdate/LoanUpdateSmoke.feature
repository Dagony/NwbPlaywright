@Smoke
Feature: LoanUpdateSmoke
    This feature smoke tests important functionally and the happy path
    for the TRX application for front- and back office for fixed, variable
    and short loan for the normal clients flow. Functionality checked per loantype:
    - Offer emails
    - Aanhef, Inserted emails of contacts, Attachment (for short loan), Empty WSW Zekerheid property
    - UG sheet copy function
    - Generation of BO documents (NWB Contract, Coverletter)
    - Generation of Opics .csv files
    - Generation of BO Sluitbrief

    Configuration loan settings:
    - Long, Daycount = Act/act,Swap = no, Amort = Bullet
    - H1 != Finalspread > Obligatory Finalspread Comment
    - Variable Notionaltype = Fixed, Euribor = 1wE, HoursClause = yes,



    # SCENARIO GAAT UIT VAN VASTE HOOFDSOM VARIABLE!!!!!
    @BekijkenVoorVerwijdering
    Scenario: Smoke TC01 Loan Update NORMAL Client happy flow
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And  I select counterparty "Trx Dunder Mifflin Paper Company" with "3" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "<ProductType>" and event "<EventType>"
        And I create a loan update case on the front office with start date "<StartDate>" and a maturity of "<MaturityInYears>" Years
        When I continue the emails should be send out and the loan update case to BO first flow LoanDataApproval
        And I go through all the steps of the loan update back office for a NON WSW, NON WFZ case
        Then the loan update backoffice case is succesfully created
        Examples:
            | TC | TestUser  | LoanType              | ProductType                       | EventType      | SecID        | Notional | StartDate | MaturityInYears |
            | A  | Treasury1 | Interest spread reset | Traditional fixed rate loan long  | Interest reset | 1 0028339TR3 |          | today     | 56              |
            | B  | Treasury1 | Interest spread reset | Floating rate loan fixed notional | Spread reset   | 1 0029646    |          | today     | 20              |

    # SCENARIO GAAT UIT VAN VARIABLE HOOFDSOM VARIABLE!!!!!
    @BekijkenVoorVerwijdering
    Scenario: Smoke TC02 Loan Update WFZ Client happy flow
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Zorggroep Heilig Hart" with "3" random counterparty individual(s) and "3" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "<SecID>"
        And I select PEGA product "<ProductType>" and event "<EventType>"
        And I create a WFZ loan update case on the front office with start date "<StartDate>" and a maturity of "<MaturityInYears>" Years
        When I continue the emails should be send out and the loan update WFZ case to BO first flow LoanDataApproval
        And I go through all the steps of the loan update back office for a WFZ case
        Then the loan update backoffice case is succesfully created
        Examples:
            | TC | TestUser  | LoanType              | ProductType                          | EventType      | SecID        | Notional | StartDate | MaturityInYears |
            | A  | Treasury1 | Interest spread reset | Traditional fixed rate loan long     | Interest reset | 1 0028262WZ1 |          | today     | 56              |
            | B  | Treasury1 | Interest spread reset | Floating rate loan floating notional | Spread reset   | 2 0031407    |          | today     | 20              |