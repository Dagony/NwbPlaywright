@LoanUpdate
Feature: LoanUpdateEmailAanhef

    This feature tests the different possible outcomes for the aanhef in the loan update offer email content. Depending on added individuals and emails
    "Beste <fullname>" or "Beste allen," is displayed.

    Only fixed and variable is tested since each has an individual function within PEGA. The SHORT offer mail calls upon the same function as fixed
    and is therefore also covered in these tests.

    #FIXED SCENARIOS
    Scenario: TC01A FIXED Aanhef with one selected individual
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "1" random counterparty individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "1 0028569RHWZ1"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a random loan update fixed case with all obligatory fields filled with TC "01A"
        When I press submit an email is generated
        Then the email aanhef should be the name of the selected random individual

    Scenario: TC01B FIXED Aanhef with two selected individuals
        #Given I select counterparty "Trx Gemeente Meerdijk" with "2" random counterparty individual(s)
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "0" random counterparty individual(s) and "2" random advisor individual(s)
        And I select PEGA task "Interest spread reset"
        And I select a loanupdate case with prefix SECID "1 0028569RHWZ1"
        And I select PEGA product "Traditional fixed rate loan long" and event "Interest reset"
        And I create a random loan update fixed case with all obligatory fields filled with TC "01B"
        When I press submit an email is generated
        Then the email aanhef should be "Beste allen,"

   