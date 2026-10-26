using DocumentFormat.OpenXml.Spreadsheet;
using Reqnroll;
using System;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.CLM;
using TA.Playwright.PageObjects.CLM.Individual;
using TA.Playwright.PageObjects.TRX.FrontOffice;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class CreateIndividualStepDefinitions
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
        private readonly INewIndividualEntityObject _newIndividualEntityObject;
        private readonly IVariableModel _variableModel;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;

        public CreateIndividualStepDefinitions(
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
            IBasePageCLMObject basePageCLMObject,
            INewIndividualEntityObject newIndividualEntityObject)
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
            _newIndividualEntityObject = newIndividualEntityObject;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
        }

        [Given("I go to the create Individual page")]
        public async Task GivenIGoToTheCreateIndividualPage()
        {
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "New onboarding" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByPlaceholder("Search for customer...").ClickAsync();
            await _playwrightDriver.Page.Result.GetByPlaceholder("Search for customer...").FillAsync("test");
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Link, new() { Name = "Create new" }).ClickAsync();
            await _basePageObject.PageWaitForUrlLoaded();
        }

        [When("I create a individual with the following properties")]
        public async Task WhenICreateAIndividualWithTheFollowingProperties(DataTable table)
        {
            IndividualFields data = table.CreateInstance<IndividualFields>();

            await _newIndividualEntityObject.SetFirstName(data.FirstName);
            if (data.MiddleName != null && data.MiddleName != string.Empty)
            {
                await _newIndividualEntityObject.SetMiddleName(data.MiddleName);
            }

            await _newIndividualEntityObject.SetLastName(data.LastName);
            if (data.Preposition != null && data.Preposition != string.Empty)
            {
                await _newIndividualEntityObject.SetPreposition(data.Preposition);
            }

            await _newIndividualEntityObject.SetDateOfBirth(data.DateOfBirth);

            if (data.CountryOfCitizenship != null && data.CountryOfCitizenship != string.Empty)
            {
                await _newIndividualEntityObject.SetCountryOfCitizenship(data.CountryOfCitizenship);
            }

            (Task emptyTask, Task emtpyTask2) tasks = data.GenderType switch
            {
                "Male" => (_newIndividualEntityObject.SetPrefix("Dhr."), _newIndividualEntityObject.SetGender("Male")),
                "Female" => (_newIndividualEntityObject.SetPrefix("Mevr."), _newIndividualEntityObject.SetGender("Female")),
                "Unspecified" => (_newIndividualEntityObject.SetPrefix("Beste"), _newIndividualEntityObject.SetGender("Unspecified")),
                _ => throw new Exception("Unknown option chosen for switch expression INPUT"),
            };

            await tasks.emptyTask;

            await tasks.emtpyTask2;

            try
            {
                var elementDubplicates = _playwrightDriver.Page.Result.GetByText("Resolve duplicates Due inin");
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
                await Task.Delay(500);
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
                await Task.Delay(1000);
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
                await _playwrightDriver.Page.Result.GetByText("None of these are the").ClickAsync();
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
                await Task.Delay(1000);
                await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
                await Task.Delay(500);
            }
            catch (TimeoutException)
            {
                // nothing happens here, no timeoutexception
            }

            await _playwrightDriver.Page.Result.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await Task.Delay(500);
        }

        [Then("the individual is created")]
        public async Task ThenTheIndividualIsCreated()
        {
            await Task.Delay(2500);
        }
    }
}