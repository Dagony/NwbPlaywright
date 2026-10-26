@PricingGrid
Feature: PricingGridSmoke

    A short summary of the feature

    Scenario: As a user I want to update the Pricing Grid
        Given I login to the environment with test user "<username>"
        And I get the initial Pricing Grid data table from the Pricing Grid screen
        And I login to the environment with test user "Treasury1" and start a "UPG" case
        When I update the Pricing Grid and approve without a comment
        Then the Pricing Grid should be updated with the inputted values for account "<username>"

        Examples:
            | username               |
            | AccountDesk1           |
            | SpecializedFinanceTRX1 |

