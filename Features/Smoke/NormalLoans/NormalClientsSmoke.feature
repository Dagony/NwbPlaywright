@Smoke
Feature: NormalClientsSmoke
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

    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)

    Scenario: Smoke TC01 Normal Client happy flow
        Given I create a "<ProductType>" LAL case on the front office with start date "<StartDate>" and a maturity of "<MaturityInYears>" Years
        When I continue the emails should be send out and the case to BO first flow LoanDataApproval
        And I go through all the steps of the back office for a NON WSW, NON WFZ case
        Then the backoffice case is succesfully created
        Examples:
            | TC | TestUser     | ProductType | StartDate | MaturityInYears |
            | A  | AccountDesk1 | LONG        | today     | 56              |
            | B  | AccountDesk1 | VARIABLE    | today     | 21              |
            | C  | AccountDesk1 | SHORT       | today     | 1               |
