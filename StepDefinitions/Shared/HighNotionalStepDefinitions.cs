using Reqnroll;
using System;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class HighNotionalStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly IShortModel _shortModel;
        private readonly IFixedModel _fixedModel;
        private readonly IVariableModel _variableModel;
        private readonly ISharedModel _sharedModel;

        public HighNotionalStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ISharedDataIndividuals sharedDataIndividuals,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            IShortModel shortModel,
            IFixedModel fixedModel,
            IVariableModel variableModel,
            ISharedModel sharedModel,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ICheckDossierCompleteObject checkDossierCompleteObject,
            ISendConfirmationObject sendConfirmationObject,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject)
        {
            _playwrightDriver = playwrightDriver;
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _winLostCorrectObject = winLostCorrectObject;
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _shortModel = shortModel;
            _sharedModel = sharedModel;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataIndividuals = sharedDataIndividuals;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
        }

        [When("I fill in the High Notional section and choose to approve the high notional")]
        public async Task WhenIFillInTheHighNotionalSectionAndChooseToApproveTheHighNotional()
        {
            await Task.Delay(500);
            await _finalSpreadObject.HighNotionalDealApproved(true);
            await _finalSpreadObject.HighNotionalSetNameOfApprover("Playwright approved this deal!");

            await _finalSpreadObject.HighNotionalProcurationMail();
        }

        [When("I fill in the High Notional section and choose to disapprove the high notional without an overwrite")]
        public async Task WhenIFillInTheHighNotionalSectionAndChooseToDisapproveTheHighNotionalWithoutAnOverwrite()
        {
            await Task.Delay(500);
            await _finalSpreadObject.HighNotionalProcurationMail();
            await _finalSpreadObject.HighNotionalDealApproved(false);
        }

        [When("I fill in the High Notional section and choose to disapprove the high notional with an overwrite")]
        public async Task WhenIFillInTheHighNotionalSectionAndChooseToDisapproveTheHighNotionalWithAnOverwrite()
        {
            await Task.Delay(500);
            await _finalSpreadObject.HighNotionalDealApproved(false);
            await _finalSpreadObject.HighNotionalCheckOverwrite();
            await _finalSpreadObject.HighNotionalSetReasonForOverwite("Reason for overwrite set by Playwright test!");
            await _finalSpreadObject.HighNotionalProcurationMail();
        }

        [Then("when the CreateOffer page is submitted, the case status in WinLostCorrect should be {string}")]
        public async Task ThenWhenTheCreateOfferPageIsSubmittedTheCaseStatusInWinLostCorrectShouldBe(string caseStatus)
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _basePageObject.ReloadPage();

            if (caseStatus == "Open-OrderConfirmation")
            {
                await _basePageObject.CloseTabInPega();
                await _basePageObject.SearchCaseNumberMainScreen(Fields.EnumsForTRX.WorkListType.Loan, lalCaseNumber);
            }

            await Task.Delay(3000);
            var caseStatusText = await _basePageObject.CompareFOCaseStatus(caseStatus);
            caseStatusText.Should().Be(caseStatus);
        }

        [Then("when the loan update CreateOffer page is submitted, the case status in WinLostCorrect should be {string}")]
        public async Task ThenWhenTheLoanUpdateCreateOfferPageIsSubmittedTheCaseStatusInWinLostCorrectShouldBe(string caseStatus)
        {
            var frontOfficeCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            if (caseStatus == "Open-OrderConfirmation")
            {
                await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();
                await _basePageObject.CloseTabInPega();

                // await _basePageObject.SearchRLUCaseNumberMainScreen(FrontOfficeCaseNumber);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdate, frontOfficeCaseNumber);
            }
            else if (caseStatus == "Resolved-Completed")
            {
                await _finalSpreadObject.ClickSubmitFinalSpreadPageClosedOff();
                await Task.Delay(500);
                await _basePageObject.CloseTabInPega();

                // await _basePageObject.SearchResolvedlalCaseNumberMainScreen(RLUCaseNumber);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanResolved, frontOfficeCaseNumber);
            }

            var caseStatusText = await _basePageObject.GetFOCaseStatus();
            caseStatusText.Should().Be(caseStatus);
        }

        [Then("the procuration email contains the notional and specific LAL number")]
        public async Task ThenTheProcurationEmailContainsTheNotionalAndSpecificLALNumber()
        {
            var notional = (string)_scenarioContext["savedNotional"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var filePath = _testSettings.DownloadFilePath;
            var environment = _testSettings.ApplicationEnvironment;

            var popupProcurationMail = await _playwrightDriver.Page.Result.RunAndWaitForPopupAsync(async () =>
            {
                await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByText("Procuration required", new() { Exact = false }).First.ClickAsync();
            });

            var actualFootTextEmailBasic = await _winLostCorrectObject.GetEmailBodyTextProcurationMail(popupProcurationMail);
            _scenarioContext["savedActualEmailText"] = actualFootTextEmailBasic;
            var filepathScreenshot = @$"{filePath}//TC {prefixName} {lalCaseNumber} ProcurationMail {environment} {currentDateTime}.png";

            Console.WriteLine($"Email successfully generated for: {prefixName} on {currentDateTime}");
            Console.WriteLine($">>PRINTING ACTUAL EMAIL BELOW FOR CASE: {lalCaseNumber}<<");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualFootTextEmailBasic);
            Console.WriteLine("----------------------------------------------------------------------------------");

            await popupProcurationMail.SetViewportSizeAsync(2400, 1080);
            await popupProcurationMail.Locator("(//body)[1]").ScrollIntoViewIfNeededAsync();
            await popupProcurationMail.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Task.Delay(3000);
            await popupProcurationMail.ScreenshotAsync(new()
            {
                Path = filepathScreenshot,
                FullPage = true,
                Type = ScreenshotType.Png,

                // Caret = Microsoft.Playwright.ScreenshotCaret.Initial
            });
            await popupProcurationMail.CloseAsync();

            actualFootTextEmailBasic.Should().Contain(lalCaseNumber);
            actualFootTextEmailBasic.Should().Contain(notional);
            actualFootTextEmailBasic.Should().ContainEquivalentOf("Er is een nieuwe lening opgesteld welke een totaal bedrag heeft hoger dan");
            actualFootTextEmailBasic.Should().ContainEquivalentOf("U krijgt deze mail omdat er een goedkeuring gegeven moet worden.");
            actualFootTextEmailBasic.Should().ContainEquivalentOf("De case bedraagt nu een totaal bedrag van:");
        }
    }
}