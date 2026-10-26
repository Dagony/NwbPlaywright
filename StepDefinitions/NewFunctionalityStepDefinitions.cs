using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NUnit.Framework.Internal;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Threading;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Extensions;
using NwbPlaywright.Fields;
using NwbPlaywright.Fields.FrontOffice.Long;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using NwbPlaywright.PageObjects.CLM;
using NwbPlaywright.PageObjects.TRX.FrontOffice;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class NewFunctionalityStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ILoanDataApprovalObject _loanDataApprovalObject;
        private readonly IValidateRequestObject _validateRequestObject;
        private readonly ISurePayCheckObject _surePayCheckObject;
        private readonly ICreateContractObject _createContractObject;
        private readonly IAgreementReviewObject _agreementReviewObject;
        private readonly ICheckGuarantorDocumentObject _checkGuarantorDocumentObject;
        private readonly IGuarantorApprovalObject _guarantorApprovalObject;
        private readonly ISendAgreementObject _sendAgreementObject;
        private readonly IClientApprovalObject _clientApprovalObject;
        private readonly IProcessClientResponseObject _processClientResponseObject;
        private readonly ILegalReviewObject _legalReviewObject;
        private readonly ICheckDossierCompleteObject _checkDossierCompleteObject;
        private readonly IFixedModel _fixedModel;
        private readonly ISharedModel _sharedModel;
        private readonly ICaptureSwapObject _captureSwapObject;
        private readonly IBasePageCLMObject _basePageCLMObject;
        private readonly IVariableModel _variableModel;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;

        public NewFunctionalityStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ILoanDataApprovalObject loanDataApprovalObject,
            IValidateRequestObject validateRequestObject,
            ISurePayCheckObject surePayCheckObject,
            ICreateContractObject createContractObject,
            IAgreementReviewObject agreementReviewObject,
            ICheckGuarantorDocumentObject checkGuarantorDocumentObject,
            IGuarantorApprovalObject guarantorApprovalObject,
            ISendAgreementObject sendAgreementObject,
            IClientApprovalObject clientApprovalObject,
            IProcessClientResponseObject processClientResponseObject,
            ILegalReviewObject legalReviewObject,
            ICheckDossierCompleteObject checkDossierCompleteObject,
            IFixedModel fixedModel,
            IVariableModel variableModel,
            ISharedModel sharedModel,
            ICaptureSwapObject captureSwapObject,
            IBasePageCLMObject basePageCLMObject)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _winLostCorrectObject = winLostCorrectObject;
            _billOfClosureObject = billOfClosureObject;
            _loanDataApprovalObject = loanDataApprovalObject;
            _validateRequestObject = validateRequestObject;
            _surePayCheckObject = surePayCheckObject;
            _createContractObject = createContractObject;
            _agreementReviewObject = agreementReviewObject;
            _checkGuarantorDocumentObject = checkGuarantorDocumentObject;
            _guarantorApprovalObject = guarantorApprovalObject;
            _sendAgreementObject = sendAgreementObject;
            _clientApprovalObject = clientApprovalObject;
            _processClientResponseObject = processClientResponseObject;
            _legalReviewObject = legalReviewObject;
            _checkDossierCompleteObject = checkDossierCompleteObject;
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _sharedModel = sharedModel;
            _captureSwapObject = captureSwapObject;
            _basePageCLMObject = basePageCLMObject;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
        }

        [Given(@"I enter data")]
        public async Task GivenIEnterData(Reqnroll.Table table)
        {
            IndividualFields data = table.CreateInstance<IndividualFields>();

            await _basePageObject.PageWaitForUrlLoaded();
            await _basePageObject.GrantGeoLocationPermission();
            await _loginPageObject.LoginCreditManagement1();

            await Task.Delay(2000);

            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "New onboarding" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByPlaceholder("Search for customer...").ClickAsync();
            await _playwrightDriver.Page.Result.GetByPlaceholder("Search for customer...").FillAsync("test");
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Link, new() { Name = "Create new" }).ClickAsync();

            await _basePageObject.PageWaitForUrlLoaded();

            await Task.Delay(3000);

            await _playwrightDriver.Page.Result.GetByLabel("First name").FillAsync(data.FirstName);

            if (data.Preposition != null && data.Preposition != string.Empty)
            {
                await _playwrightDriver.Page.Result.GetByLabel("Preposition").ClickAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Preposition").FillAsync(data.Preposition);
            }

            await _playwrightDriver.Page.Result.GetByLabel("Last name").ClickAsync();
            await _playwrightDriver.Page.Result.GetByLabel("Last name").FillAsync(data.LastName);

            if (data.MiddleName != null && data.MiddleName != string.Empty)
            {
                await _playwrightDriver.Page.Result.GetByLabel("Middle name").ClickAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Middle name").FillAsync(data.MiddleName);
            }

            (Task emptyTask, Task emtpyTask2) tasks = data.GenderType switch
            {
                "Male" => (_playwrightDriver.Page.Result.GetByLabel("Prefix:").SelectOptionAsync(new[] { "Dhr." }), _playwrightDriver.Page.Result.GetByText("Male", new() { Exact = true }).ClickAsync()),
                "Female" => (_playwrightDriver.Page.Result.GetByLabel("Prefix:").SelectOptionAsync(new[] { "Mevr." }), _playwrightDriver.Page.Result.GetByText("Female").ClickAsync()),
                "Unspecified" => (_playwrightDriver.Page.Result.GetByLabel("Prefix:").SelectOptionAsync(new[] { "Beste" }), _playwrightDriver.Page.Result.GetByText("Unspecified").ClickAsync()),
                _ => throw new Exception("Unknown option chosen for switch expression INPUT"),
            };

            await tasks.emptyTask;
            await tasks.emtpyTask2;

            await _playwrightDriver.Page.Result.GetByLabel("Date of birth").ClickAsync();
            await _playwrightDriver.Page.Result.GetByLabel("Date of birth").FillAsync(data.DateOfBirth);

            if (data.CountryOfOrigin != null && data.CountryOfOrigin != string.Empty)
            {
                await _playwrightDriver.Page.Result.GetByLabel("Country of birth", new() { Exact = true }).ClickAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Country of birth", new() { Exact = true }).FillAsync(data.CountryOfOrigin);
                await _playwrightDriver.Page.Result.GetByLabel("Country of birth", new() { Exact = true }).FillAsync("Netherlands");
            }

            if (data.CountryOfCitizenship != null && data.CountryOfCitizenship != string.Empty)
            {
                await _playwrightDriver.Page.Result.GetByLabel("Country of citizenship is").CheckAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Country of citizenship", new() { Exact = true }).ClickAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Country of citizenship", new() { Exact = true }).FillAsync(data.CountryOfCitizenship);
            }

            if (data.CountryOfResidency != null && data.CountryOfResidency != string.Empty)
            {
                await _playwrightDriver.Page.Result.GetByLabel("Country of fiscal residency").CheckAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Primary fiscal residence").ClickAsync();
                await _playwrightDriver.Page.Result.GetByLabel("Primary fiscal residence").FillAsync(data.CountryOfResidency);
            }

            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();

            await Task.Delay(500);

            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            await Task.Delay(1000);

            // if duplicates message appears PLEASE HANDLE IT!
            var elementDubplicates = _playwrightDriver.Page.Result.GetByText("Resolve duplicates Due inin");
            var visibleElement = await elementDubplicates.IsVisibleAsync();

            if (visibleElement == true)
            {
                await _playwrightDriver.Page.Result.GetByText("None of these are the").ClickAsync();
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
                await Task.Delay(1000);
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
                await Task.Delay(500);
            }

            await Task.Delay(500);

            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await Task.Delay(500);

            await _playwrightDriver.Page.Result.GetByLabel("Enrich Due Diligence").GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await Task.Delay(500);
        }

        [When(@"I do this")]
        public async Task WhenIDoThis()
        {
            await _loginPageObject.LoginUser(TestUser.CreditManagement1);

            await _basePageObject.GrantGeoLocationPermission();
            await _basePageCLMObject.CreateNewOrganisation();
            await Task.CompletedTask;
        }

        [Then(@"this is the outcome")]
        public async Task ThenThisIsTheOutcome()
        {
            Console.WriteLine("Done");
            await Task.CompletedTask;
        }

        [When("I insert the following string {string} it should be splitted and printed for easy processing")]
        public void WhenIInsertTheFollowingStringItShouldBeSplittedAndPrintedForEasyProcessing(string input)
        {
            string[] parts = input.Split(';');

            // Loop in pairs (username, password)
            for (int i = 0; i < parts.Length - 1; i += 2)
            {
                // Print username
                Console.WriteLine(parts[i]);

                // Print password
                Console.WriteLine(parts[i + 1]);

                // Blank line between pairs
                Console.WriteLine();
            }
        }
    }
}