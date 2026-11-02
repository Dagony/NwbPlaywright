@Mark @CRM
Feature: First case

    Scenario: TC01 First Case
        Given I login to the CRM environment with test user "<TestUser>"
        And I search for an Organization
        When I create a new Organization
    
    Examples:
        | TestUser     |
        | MarkdeWitCRM |