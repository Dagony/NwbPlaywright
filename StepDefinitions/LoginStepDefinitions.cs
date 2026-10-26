using DocumentFormat.OpenXml.Spreadsheet;
using FluentAssertions;
using System.Reflection;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly IBasePageObject _basePageObject;

        private readonly ILoginPageObject _loginPageObject;

        private readonly INewCaseObject _newCaseObject;

        private readonly TestSettings _testSettings;

        private readonly IPlaywrightDriver _playwrightDriver;

        public LoginStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            TestSettings testSettings)
        {
            _playwrightDriver = playwrightDriver;
            _scenarioContext = scenarioContext;
            _basePageObject = basePageObject;
            _testSettings = testSettings;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
        }

        [StepArgumentTransformation]
        public string[] TransformToArrayOfStrings(string commaSeparatedStepArgumentValues)
        {
            string sourceString = commaSeparatedStepArgumentValues;
            string[] stringSeparators = new string[] { "," };
            return sourceString.Split(stringSeparators, StringSplitOptions.None);
        }

        [Given("I select counterparty {string} without an individual")]
        public async Task GivenISelectCounterpartyWithoutAnIndividual(string counterParty)
        {
            _scenarioContext["savedCounterParty"] = counterParty;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = 0;
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterParty);

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.GetCustomerInformation();

            await _basePageObject.OpenTaskMenu();
        }

        [Given("I select a random counterparty")]
        public async Task GivenISelectARandomCounterparty()
        {
            string randomCounterparty = await _newCaseObject.GetRandomCustomer();
            _scenarioContext["savedCounterParty"] = randomCounterparty;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = 0;
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, randomCounterparty);

            await _newCaseObject.SelectFirstIndividual();
            var legalNameAdvisory = await _newCaseObject.GetLegalNameAdvisory();
            _scenarioContext["savedLegalNameAdvisory"] = legalNameAdvisory;

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.GetCustomerInformation();

            await _basePageObject.OpenTaskMenu();
        }

        [Given("I select counterparty {string} with individual\\(s\\) {string}")]
        public async Task GivenISelectCounterpartyWithIndividualS(string counterParty, string[] individuals)
        {
            _scenarioContext["savedCounterParty"] = counterParty;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = 0;
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterParty);
            foreach (var individual in individuals)
            {
                await _newCaseObject.SelectCustomerNew(individual);
            }

            _scenarioContext["savedLegalNameAdvisory"] = string.Empty;

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.GetCustomerInformation();

            await _basePageObject.OpenTaskMenu();
        }

        [Given(@"I select counterparty ""(.*)"" with ""(.*)"" random counterparty individual\(s\)")]
        public async Task GivenISelectCounterpartyWithRandomCounterpartyIndividualS(string counterParty, int numberOfCounterpartyIndividuals)
        {
            _scenarioContext["savedCounterParty"] = counterParty;
            _scenarioContext["savedNumbOfIndividuals"] = numberOfCounterpartyIndividuals;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = 0;
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterParty);
            for (int i = 0; i < numberOfCounterpartyIndividuals; i++)
            {
                var randomIndividualBasedOnCounterparty = await _newCaseObject.GetRandomIndividual(counterParty);
                await _newCaseObject.SelectCustomerNew(randomIndividualBasedOnCounterparty);
            }

            try
            {
                var legalNameAdvisory = await _newCaseObject.GetLegalNameAdvisory();
                _scenarioContext["savedLegalNameAdvisory"] = legalNameAdvisory;
            }
            catch (TimeoutException e)
            {
                _scenarioContext["savedLegalNameAdvisory"] = "COULD NOT FIND ANY ADVISORY FIRM WITH THIS CUSTOMER, call origin from GivenISelectCounterpartyWithRandomCounterpartyIndividualS()";
            }

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.GetCustomerInformation();
            await _basePageObject.OpenTaskMenu();
        }

        [Given("I select counterparty {string} with {string} random counterparty individual\\(s\\) and {string} random advisor individual\\(s\\)")]
        public async Task GivenISelectCounterpartyWithRandomCounterpartyIndividualSAndRandomAdvisorIndividualS(string counterParty, int numberOfCounterpartyIndividuals, int numberOfAdvisorIndividuals)
        {
            _scenarioContext["savedCounterParty"] = counterParty;
            _scenarioContext["savedNumbOfIndividuals"] = numberOfCounterpartyIndividuals;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = numberOfAdvisorIndividuals;
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterParty);
            for (int i = 0; i < numberOfCounterpartyIndividuals; i++)
            {
                var randomIndividualBasedOnCounterparty = await _newCaseObject.GetRandomIndividual(counterParty);
                await _newCaseObject.SelectCustomerNew(randomIndividualBasedOnCounterparty);
            }

            for (int i = 0; i < numberOfAdvisorIndividuals; i++)
            {
                var randomAdvisorIndividualBasedOnCounterparty = await _newCaseObject.GetRandomAdvisorIndividual(counterParty);
                await _newCaseObject.SelectCustomerNew(randomAdvisorIndividualBasedOnCounterparty);
            }

            var legalNameAdvisory = await _newCaseObject.GetLegalNameAdvisory();
            _scenarioContext["savedLegalNameAdvisory"] = legalNameAdvisory;

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.GetCustomerInformation();

            await _basePageObject.OpenTaskMenu();
        }

        [Given("I search for the automatic loanupdate case with SECID {string} with expected loanType of {string} and open the select contact page")]
        public async Task GivenISearchForTheAutomaticLoanupdateCaseWithSECIDWithExpectedLoanTypeOfAndOpenTheSelectContactPage(string secID, string expectedLoanType)
        {
            _scenarioContext["savedLoanType"] = expectedLoanType;
            _scenarioContext["savedLoanUpdateSecID"] = secID;
            await _basePageObject.SearchSecIDMainScreen(WorkListType.LoanUpdate, secID);
            await _basePageObject.ClickSelectContactInitializationLink();
        }

        [Given("I select {string} random counterparty individual\\(s\\) and {string} random advisor individual\\(s\\) for expected counterparty {string}")]
        public async Task GivenISelectRandomCounterpartyIndividualSAndRandomAdvisorIndividualSForExpectedCounterparty(int numberOfCounterpartyIndividuals, int numberOfAdvisorIndividuals, string counterParty)
        {
            _scenarioContext["savedCounterParty"] = counterParty;
            _scenarioContext["savedNumbOfIndividuals"] = numberOfCounterpartyIndividuals;
            _scenarioContext["savedSelectedAdvisorIndividuals"] = numberOfAdvisorIndividuals;

            await _basePageObject.GetCustomerInformationLoanUpdateAutomatic();

            for (int i = 0; i < numberOfCounterpartyIndividuals; i++)
            {
                var randomIndividualBasedOnCounterparty = await _newCaseObject.GetRandomIndividual(counterParty);
                await _newCaseObject.SelectCustomerNewLoanUpdateAutomatic(randomIndividualBasedOnCounterparty);
            }

            for (int i = 0; i < numberOfAdvisorIndividuals; i++)
            {
                var randomAdvisorIndividualBasedOnCounterparty = await _newCaseObject.GetRandomAdvisorIndividual(counterParty);
                await _newCaseObject.SelectCustomerNewLoanUpdateAutomatic(randomAdvisorIndividualBasedOnCounterparty);
            }

            var legalNameAdvisory = await _newCaseObject.GetLegalNameAdvisoryLoanUpdateAuto();
            _scenarioContext["savedLegalNameAdvisory"] = legalNameAdvisory;

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
        }

        [Given(@"I chose counterparty ""([^""]*)"" with advisory ""([^""]*)""")]
        public async Task GivenIChoseCounterpartyWithAdvisory(string counterParty, string advisory)
        {
            SynchronizationContext.SetSynchronizationContext(null);
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterParty);
            await _basePageObject.WaitForPageLoadedState();

            switch (counterParty)
            {
                case "Gemeente Ooststellingwerf":
                    await _newCaseObject.SelectCustomerNew("Cristiano Pijl");
                    break;
                case "Gemeente Amsterdam":
                    await _newCaseObject.SelectCustomerNew("Chiara van Grunsven");
                    break;
                case "HVC N.V.":
                    // No action needed for this case
                    break;
                case "Stichting nationaal restauratiefonds":
                    await _newCaseObject.SelectCustomerNew("Andreas Jongsma");
                    break;
                case "Wetterskip Fryslan":
                    await _newCaseObject.SelectCustomerNew("Bowie de Lagerwerf");
                    break;
                case "Stichting Groenfonds":
                    await _newCaseObject.SelectCustomerNew("Bennie Bondje");
                    break;
            }

            switch (advisory)
            {
                case "ILFA Treasury Support":
                    await _newCaseObject.SelectCustomerNew("Carsten van Bozkurt");
                    break;
                case "Thesor Treasury Outsourcing BV":
                    await _newCaseObject.SelectCustomerNew("Moira Flinterman");
                    break;
                case "Adviseur met medewerkers":
                    await _newCaseObject.SelectCustomerNew("Marlene van Gelderen");
                    break;
            }

            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.OpenTaskMenu();
        }

        [When(@"I enter my login credentials")]
        public async Task WhenIEnterMyLoginCredentials(Reqnroll.Table table)
        {
            var data = table.CreateInstance<(string userName, string passWord)>();

            await _loginPageObject.SetLoginData(data.userName, data.passWord);
            await _basePageObject.WaitForPageLoadedState();
        }

        [Then(@"I am denied access to the website")]
        public async Task ThenIAmDeniedAccessToTheWebsite()
        {
            var errorMessage = await _loginPageObject.GetErrorMessage();
            errorMessage.Should().Be("The information you entered was not recognized.");
        }

        [Then(@"I land on the landing page for the accountdesk")]
        public async Task ThenILandOnTheLandingPageForTheAccountdesk()
        {
            var nameOfUserInTitlePage = await _basePageObject.GetUserTextTopPage();
            nameOfUserInTitlePage.Should().Be("TestUser AccountDesk1");
        }

        [Given("I login to the environment with test user {string} and start a {string} case")]
        public async Task GivenILoginToTheEnvironmentWithTestUserAndStartACase(string testUser, string caseType)
        {
            await _basePageObject.PageWaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();

            Enum.TryParse(testUser, out TestUser chosenTestUser);
            _scenarioContext["savedLoggedInTestUser"] = chosenTestUser;
            await _loginPageObject.LoginUser(chosenTestUser);

            var chooseCaseType = caseType switch
            {
                "UPG" => _newCaseObject.StartNewPricingGridCase(),
                "RESEARCH" => _newCaseObject.StartNewResearchInteraction(),
                "NORMAL" => _newCaseObject.StartNewCase(),
                _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
            };
            await chooseCaseType;
        }

        [Given("I login to the environment with test user {string}")]
        public async Task GivenILoginToTheEnvironmentWithTestUser(string testUser)
        {
            await _basePageObject.PageWaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();

            Enum.TryParse(testUser, out TestUser chosenTestUser);
            _scenarioContext["savedLoggedInTestUser"] = chosenTestUser;
            await _loginPageObject.LoginUser(chosenTestUser);
        }

        [Given(@"I login to the environment and start a new case")]
        public async Task GivenILoginToTheEnvironmentAndStartANewCase()
        {
            await _basePageObject.PageWaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();
            await _loginPageObject.LoginAccountDeskUser1();
            _scenarioContext["savedLoggedInTestUser"] = TestUser.AccountDesk1;
            await Task.Delay(1500);
            await _newCaseObject.StartNewCase();
        }

        [Given("I login to the environment")]
        public async Task GivenILoginToTheEnvironment()
        {
            await _basePageObject.PageWaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();
            await _loginPageObject.LoginAccountDeskUser1();
            _scenarioContext["savedLoggedInTestUser"] = TestUser.AccountDesk1;
            await Task.Delay(500);
        }

        [Given(@"I navigate to the environment")]
        public async Task GivenINavigateToTheEnvironment()
        {
            await _basePageObject.PageWaitForUrlLoaded();
        }

        [When(@"I enter the environment specific login credentials")]
        public async Task WhenIEnterTheEnvironmentSpecificLoginCredentials()
        {
            await _loginPageObject.LoginAccountDeskUser1();
            _scenarioContext["savedLoggedInTestUser"] = TestUser.AccountDesk1;
        }
    }
}