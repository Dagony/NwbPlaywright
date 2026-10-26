using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Reqnroll;
using System;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.TRX.Treasury;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class PricingGridSmokeStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly IBasePageObject _basePageObject;

        private readonly ILoginPageObject _loginPageObject;

        private readonly INewCaseObject _newCaseObject;

        private readonly IUpdatePricingGridObject _updatePricingGridObject;

        private readonly TestSettings _testSettings;

        private readonly IPlaywrightDriver _playwrightDriver;

        public PricingGridSmokeStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IUpdatePricingGridObject updatePricingGridObject,
            TestSettings testSettings)
        {
            _playwrightDriver = playwrightDriver;
            _scenarioContext = scenarioContext;
            _basePageObject = basePageObject;
            _testSettings = testSettings;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _updatePricingGridObject = updatePricingGridObject;
        }

        [Given("I get the initial Pricing Grid data table from the Pricing Grid screen")]
        public async Task GivenIGetTheInitialPricingGridDataTableFromThePricingGridScreen()
        {
            // await Task.Delay(2000);
            await _basePageObject.ClickPricingGridButton();
            var tableObject = await _updatePricingGridObject.GetPricingGridData();
            var commentMessage = await _updatePricingGridObject.GetCommentMessage();
            _scenarioContext["savedPricingGridDataTable"] = tableObject;
            _scenarioContext["savedCommentMessage"] = commentMessage;
            await _basePageObject.LogOutSystem();
        }

        [When("I update the Pricing Grid and approve without a comment")]
        public async Task WhenIUpdateThePricingGridAndApproveWithoutAComment()
        {
            await Task.Delay(500);
            var pricingGridCaseID = await _updatePricingGridObject.GetCaseID();

            for (var i = 1; i < 51; i++)
            {
                Random random = new Random();

                // Niet foolproof! Later herzien. Kan zijn dat twee keer dezelfde random wordt gekozen. Test zal dan falen!
                double newValue = random.Next(1, 200);

                var addedI = i + newValue;
                var iString = addedI.ToString();
                await _updatePricingGridObject.SetFundingCurve(iString, i);
                await _basePageObject.PressTab();
            }

            await _updatePricingGridObject.ClickSubmitPricingGrid();
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginTreasury2();
            await Task.Delay(2500);
            await _updatePricingGridObject.ClickTreasuryWorkqueue();
            await Task.Delay(500);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, pricingGridCaseID);

            await _updatePricingGridObject.SetApprovalDecision(true);
            await _updatePricingGridObject.ClickSubmitReviewPricingGridPage();
            await Task.Delay(3000);
            await _basePageObject.LogOutSystem();
        }

        [Then("the Pricing Grid should be updated with the inputted values for account {string}")]
        public async Task ThenThePricingGridShouldBeUpdatedWithTheInputtedValuesForAccount(string testUser)
        {
            var initialTableObject = (List<PricingGridFields>)_scenarioContext["savedPricingGridDataTable"];
            var commentMessage = (string)_scenarioContext["savedCommentMessage"];

            Enum.TryParse(testUser, out TestUser chosenTestUser);
            await _loginPageObject.LoginUser(chosenTestUser);

            await _basePageObject.ClickPricingGridButton();
            var actualTableObject = await _updatePricingGridObject.GetPricingGridData();
            var actualCommentMessage = await _updatePricingGridObject.GetCommentMessage();

            initialTableObject.Should().NotBeEquivalentTo(actualTableObject, options => options.WithStrictOrdering());
            commentMessage.Should().Be(actualCommentMessage);
        }
    }
}