@CLM
Feature: CreateIndividual

    A short summary of the feature

    Background:
        Given I login to the environment with test user "CreditManagement1"

    @tag1
    Scenario: Create Individual
        Given I go to the create Individual page
        When I create a individual with the following properties
            | FirstName   | MiddleName   | LastName   | FullName   | Preposition   | DateOfBirth   | GenderType   | CountryOfCitizenship   |
            | <FirstName> | <MiddleName> | <LastName> | <FullName> | <Preposition> | <DateOfBirth> | <GenderType> | <CountryOfCitizenship> |
        Then the individual is created

        Examples:
            | FirstName | MiddleName | LastName | FullName | Preposition | DateOfBirth | GenderType | CountryOfCitizenship |
            | Lola      |            | Top      |          | de          | 01-01-1990  | Female     | Netherlands          |