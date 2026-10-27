@Smoke @non-parallelizable
Feature: WSWClientsSmoke
    This feature smoke tests important functionally and the happy path
    for the TRX application for front- and back office for fixed and variable
    for the WSW clients flow. Functionality checked per loantype:
    - Offer emails
    - Aanhef, Inserted emails of contacts, Filled WSW Zekerheid property

    - UG sheet copy function
    - Generation of BO documents (WSW Contract, WSW Summary, Coverletter)
    - Generation of Opics .csv files
    - Generation of BO Sluitbrief

    Configuration loan settings:
    - Long, Daycount = Act/360,Swap = no, Amort = Annuity, CouponAmorFreq = Annual
    - H1 != Finalspread > Obligatory Finalspread Comment
    - Variable Notionaltype = Variable, Euribor = 12mE

    Background:
        Given I login to the environment and start a new case
        Given I select counterparty "Trx Stichting Greendale Wonen" with "3" random counterparty individual(s)

    Scenario: Smoke TC02 WSW Client happy flow
        Given I create a "<LoanType>" WSW LAL case on the front office with start date "<StartDate>" and a maturity of "<MaturityInYears>" Years
        When I continue the emails should be send out and the WSW case to BO first flow LoanDataApproval
        When I go through all the steps of the back office for a WSW case
        Then the backoffice case is succesfully created
        Examples:
            | TC | LoanType | StartDate  | MaturityInYears |
            | A  | LONG     | 13-05-2026 | 56              |
            | B  | VARIABLE | 14-05-2026 | 21              |