@Smoke
Feature: WFZClientsSmoke
    This feature smoke tests important functionally and the happy path
    for the TRX application for front- and back office for fixed, variable
    and short loan for the WFZ clients flow. Functionality checked per loantype:
    - Offer emails
    - Aanhef, Inserted emails of contacts, Attachment (for short loan), Empty WSW Zekerheid property
    - UG sheet copy function
    - Generation of BO documents (WfZ Contract, Coverletter)
    - Generation of Opics .csv files
    - Generation of BO Sluitbrief

    Configuration loan settings:
    - Long, Daycount = 30/360,Swap = no, Amort = Linear CouponAmorFreq = Quarterly, SuretyNumber&Date
    - H1 = Finalspread > No obligatory Finalspread Comment
    - Variable Notionaltype = Variable, Euribor = 6mE
    - Short, SuretyNumber

    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Zorggroep Heilig Hart" with "3" random counterparty individual(s) and "3" random advisor individual(s)

    @BekijkenVoorVerwijdering
    Scenario: Smoke TC01 WFZ Client happy flow
        Given I create a "<LoanType>" WFZ LR case on the front office with start date "<StartDate>" and a maturity of "<MaturityInYears>" Years
        When I continue the emails should be send out and the WFZ case to BO first flow LoanDataApproval
        When I go through all the steps of the back office for a WFZ case
        Then the backoffice case is succesfully created
        Examples:
            | TC | TestUser  | LoanType | StartDate  | MaturityInYears |
            | A  | Treasury1 | LONG     | 13-05-2026 | 56              |
            | B  | Treasury1 | VARIABLE | 14-05-2026 | 21              |
            | C  | Treasury1 | SHORT    | 15-12-2026 | 1               |