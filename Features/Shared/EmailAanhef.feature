@Shared
Feature: EmailAanhef

    This feature tests the different possible outcomes for the aanhef in the offer email content. Depending on added individuals and emails
    "Beste <fullname>" or "Beste allen," is displayed.

    Only fixed and variable is tested since each has an individual function within PEGA. The SHORT offer mail calls upon the same function as fixed
    and is therefore also covered in these tests.


    #FIXED SCENARIOS
    Scenario: TC01B FIXED Aanhef with two selected individuals
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And  I select counterparty "Trx Stichting Greendale Wonen" with "0" random counterparty individual(s) and "2" random advisor individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        Given I create a random fixed case with all obligatory fields filled with TC "01B"
        When I press submit an email is generated
        Then the email aanhef should be "Beste allen,"

    Scenario: TC01C FIXED Aanhef with one individual and one added email
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Greendale Wonen" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Traditional fixed rate loan long" and event "Primary"
        Given I create a random fixed case with all obligatory fields filled with TC "01C"
        When I add in an extra emailadress with name "testFixedCase@test.nl"
        When I press submit an email is generated
        Then the email aanhef should be "Beste allen,"

   
    #VARIABLE SCENARIOS
    Scenario: TC02B VAR Aanhef with two selected individuals
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Greendale Wonen" with "2" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        Given I create a random variable case with all obligatory fields filled with TC "02B"
        When I press submit an email is generated
        Then the email aanhef should be "Beste allen,"

    Scenario: TC02C VAR Aanhef with one individual and one added email
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And I select counterparty "Trx Stichting Greendale Wonen" with "1" random counterparty individual(s)
        And I select PEGA task "Loan"
        And I select PEGA product "Floating rate loan fixed notional" and event "Primary"
        Given I create a random variable case with all obligatory fields filled with TC "02C"
        When I add in an extra emailadress with name "testVariableCase@test.nl"
        When I press submit an email is generated
        Then the email aanhef should be "Beste allen,"

    #DEPOSITO SCENARIOS
    Scenario: TC03B DEPOSIT Aanhef with two selected individuals
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And  I select counterparty "Trx Stichting Greendale Wonen" with "2" random counterparty individual(s)
        And I select PEGA task "Deposit"
        And I create a random deposit case with all obligatory fields filled with TC "03B"
        When I press submit an email is generated
        Then the deposit email aanhef should be "Beste allen," and the English aanhef should be "Dear all,"

    Scenario: TC03C DEPOSIT Aanhef with one individual and one added email
        Given I login to the environment with test user "AccountDesk1" and start a "NORMAL" case
        And  I select counterparty "Trx Stichting Greendale Wonen" with "1" random counterparty individual(s)
        Given I select PEGA task "Deposit"
        Given I create a random deposit case with all obligatory fields filled with TC "03C"
        When I add in an extra emailadress with name "testDEPOSITCase@test.nl"
        When I press submit an email is generated
        Then the deposit email aanhef should be "Beste allen," and the English aanhef should be "Dear all,"
