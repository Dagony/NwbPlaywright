using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;
using NwbPlaywright.PageObjects.CRM;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class CrmStepDefinitions(
        IPlaywrightDriver playwrightDriver,
        ScenarioContext scenarioContext,
        IBasePageObject basePageObject,
        ILoginPageObject loginPageObject,
        ICrmBasePageObject crmBasePageObject,
        ICrmLoginPageObject crmLoginPageObject,
        INewCaseObject newCaseObject,
        TestSettings testSettings)
    {
        private readonly ScenarioContext _scenarioContext = scenarioContext;

        private readonly ILoginPageObject _loginPageObject = loginPageObject;

        private readonly ICrmLoginPageObject _crmLoginPageObject = crmLoginPageObject;

        private readonly ICrmBasePageObject _crmBasePageObject = crmBasePageObject;

        private readonly IBasePageObject _basePageObject = basePageObject;

        private readonly INewCaseObject _newCaseObject = newCaseObject;

        private readonly TestSettings _testSettings = testSettings;

        private readonly IPlaywrightDriver _playwrightDriver = playwrightDriver;

        [Given("I login to the CRM environment with test user {string}")]
        public async Task GivenILoginToTheCrmEnvironment(string testUser)
        {
            Console.Write("Mark wil dit zien:");
            Console.Write(_testSettings.CrmApplicationUrl);
            await _crmLoginPageObject.WaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();
            await _crmLoginPageObject.LoginToCrm();
            _scenarioContext["savedLoggedInTestUser"] = EnumsForCRM.TestCrmUser.DevMarkdeWit;

            // Privacy dialog
            var privacyDialogShowing = await _crmLoginPageObject.IsPrivacyDialogVisible();
            privacyDialogShowing.Should().Be(true);
            await _crmLoginPageObject.ClickPrivacyDialogAcceptButton();
            await _crmBasePageObject.WaitForPageToBeLoaded();
        }
    }
}

