@Long
Feature: LoanEmailFlow

    Offers which are marked for correction on the Collect Offer page and offers that are rejected on the BoC page should be returned to the FinalSpread Page and
    checkbox for "Include in mail" should be checked. For all other values (won, lost, approved) offers should be unchecked.
    Background:
        Given I login to the environment with test user "<TestUser>" and start a "NORMAL" case
        And I select counterparty "Trx Gemeente Meerdijk" with "3" random counterparty individual(s)
        And I select PEGA task "Loan"

    Scenario: TC03 Won, Lost and Corrected deal
        Given I select PEGA product "<offerType>" and event "Primary"
        And I create random offer of type "<offerType>"
        When I chose "<WinLostCorrect>" and click the SubmitButton
        Then the case status should be "<ExpectedCaseStatus>"
        Examples: Input value in offer
            | TestUser     | offerType                            | WinLostCorrect | ExpectedCaseStatus |
            | AccountDesk1 | Traditional fixed rate loan long     | Won            | Open-Approval      |
            | AccountDesk1 | Traditional fixed rate loan long     | Lost           | Resolved-Lost      |
            | AccountDesk1 | Traditional fixed rate loan long     | Correct        | Open-CreateRequest |
            | AccountDesk1 | Floating rate loan floating notional | Won            | Open-Approval      |
            | AccountDesk1 | Floating rate loan floating notional | Lost           | Resolved-Lost      |
            | AccountDesk1 | Floating rate loan floating notional | Correct        | Open-CreateRequest |
            | AccountDesk1 | Traditional fixed rate loan short    | Won            | Open-Approval      |
            | AccountDesk1 | Traditional fixed rate loan short    | Lost           | Resolved-Lost      |
            | AccountDesk1 | Traditional fixed rate loan short    | Correct        | Open-CreateRequest |








