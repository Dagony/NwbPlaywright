@Smoke
Feature: DepositSmoke
    This feature smoke tests important functionally and the happy path
    for the TRX application for front- and back office for deposit product.

    SecondApprover is set based on first logged in test user and is set to the current department (treasury1 > treasury2 // accountdesk1 > accountdesk2 // treasurymanager1 > treasurymanager2)



    Configuration deposit loan settings:
    TC01: Aanhef email 'beste allen',



    Scenario: Smoke TC01 Deposit happy flow - TRY
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        Given I select counterparty "<CounterParty>" with "2" random counterparty individual(s)
        Given I select PEGA task "Deposit"
        Given I create a deposit case with start date "<StartDate>" and a maturity date "<MaturityDateInDays>" with foureyes approval set to "<SecondApprover>"
        When I continue the emails should be send out and the case to approved by "<ApproverBoC>" to first BO stage LoanDataApproval
        When I go through all the steps of the back office for a deposit case with "<Product>"
        Then the backoffice case is succesfully created
        Examples:
            | TC | TestUser         | SecondApprover | ApproverBoC      | CounterParty                        | StartDate | MaturityDateInDays | Product |
            | C  | Treasury1        | True           | Treasury1        | Trx Dunder Mifflin Paper Company    | today     | 60                 | KORTLE  |
            | D  | Treasury1        | False          | TreasuryManager1 | Trx Stichting Zorggroep Heilig Hart | today     | 150                | KORTLE  |
