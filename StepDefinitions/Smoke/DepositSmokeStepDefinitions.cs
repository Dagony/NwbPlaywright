using System;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.TRX;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class DepositSmokeStepDefinitions
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
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly ISharedModel _sharedModel;
        private readonly IVariableModel _variableModel;
        private readonly IShortModel _shortModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly ILoanUpdateWinLostCorrectObject _loanUpdateWinLostCorrectObject;
        private readonly ILoanUpdateBillOfClosureObject _loanUpdateBillOfClosureObject;
        private readonly ILoanUpdateLoanDataApprovalObject _loanUpdateLoanDataApprovalObject;
        private readonly ILoanUpdateValidateRequestObject _loanUpdateValidateRequestObject;
        private readonly ILoanUpdateCreateContractObject _loanUpdateCreateContractObject;
        private readonly ILoanUpdateAgreementReviewObject _loanUpdateAgreementReviewObject;
        private readonly ILoanUpdateCheckGuarantorDocumentObject _loanUpdateCheckGuarantorDocumentObject;
        private readonly ILoanUpdateGuarantorApprovalObject _loanUpdateGuarantorApprovalObject;
        private readonly ILoanUpdateSendAgreementObject _loanUpdateSendAgreementObject;
        private readonly ILoanUpdateClientApprovalObject _loanUpdateClientApprovalObject;
        private readonly ILoanUpdateProcessClientResponseObject _loanUpdateProcessClientResponseObject;
        private readonly ILoanUpdateLegalReviewObject _loanUpdateLegalReviewObject;
        private readonly ILoanUpdateCheckDossierCompleteObject _loanUpdateCheckDossierCompleteObject;
        private readonly ISharedDataIndividuals _sharedDataIndividual;
        private readonly IFixedModel _fixedModel;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;

        public string ChosenStartDate { get; set; }

        public DepositSmokeStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ISharedDataIndividuals sharedDataIndividual,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            IFixedModel fixedModel,
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
            ISharedDataIndividuals sharedDataIndividuals,
            ISendConfirmationObject sendConfirmationObject,
            ISharedModel sharedModel,
            IVariableModel variableModel,
            IShortModel shortModel,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            ILoanUpdateWinLostCorrectObject loanUpdateWinLostCorrectObject,
            ILoanUpdateBillOfClosureObject loanUpdateBillOfClosureObject,
            ILoanUpdateLoanDataApprovalObject loanUpdateLoanDataApprovalObject,
            ILoanUpdateValidateRequestObject loanUpdateValidateRequestObject,
            ILoanUpdateCreateContractObject loanUpdateCreateContractObject,
            ILoanUpdateAgreementReviewObject loanUpdateAgreementReviewObject,
            ILoanUpdateCheckGuarantorDocumentObject loanUpdateCheckGuarantorDocumentObject,
            ILoanUpdateGuarantorApprovalObject loanUpdateGuarantorApprovalObject,
            ILoanUpdateSendAgreementObject loanUpdateSendAgreementObject,
            ILoanUpdateClientApprovalObject loanUpdateClientApprovalObject,
            ILoanUpdateProcessClientResponseObject loanUpdateProcessClientResponseObject,
            ILoanUpdateLegalReviewObject loanUpdateLegalReviewObject,
            ILoanUpdateCheckDossierCompleteObject loanUpdateCheckDossierCompleteObject)
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
            _sharedDataIndividual = sharedDataIndividual;
            _fixedModel = fixedModel;
            _checkDossierCompleteObject = checkDossierCompleteObject;
            _sharedDataIndividuals = sharedDataIndividuals;
            _sendConfirmationObject = sendConfirmationObject;
            _sharedModel = sharedModel;
            _variableModel = variableModel;
            _shortModel = shortModel;
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _loanUpdateWinLostCorrectObject = loanUpdateWinLostCorrectObject;
            _loanUpdateBillOfClosureObject = loanUpdateBillOfClosureObject;
            _loanUpdateLoanDataApprovalObject = loanUpdateLoanDataApprovalObject;
            _loanUpdateValidateRequestObject = loanUpdateValidateRequestObject;
            _loanUpdateCreateContractObject = loanUpdateCreateContractObject;
            _loanUpdateAgreementReviewObject = loanUpdateAgreementReviewObject;
            _loanUpdateCheckGuarantorDocumentObject = loanUpdateCheckGuarantorDocumentObject;
            _loanUpdateGuarantorApprovalObject = loanUpdateGuarantorApprovalObject;
            _loanUpdateSendAgreementObject = loanUpdateSendAgreementObject;
            _loanUpdateClientApprovalObject = loanUpdateClientApprovalObject;
            _loanUpdateProcessClientResponseObject = loanUpdateProcessClientResponseObject;
            _loanUpdateLegalReviewObject = loanUpdateLegalReviewObject;
            _loanUpdateCheckDossierCompleteObject = loanUpdateCheckDossierCompleteObject;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            ChosenStartDate = _sharedModel.SharedEmptyString;
        }

        [Given("I create a deposit case with start date {string} and a maturity date {string} with foureyes approval set to {string}")]
        public async Task GivenICreateADepositCaseWithStartDateAndAMaturityDateWithFoureyesApprovalSetTo(string startDate, double maturityDate, bool secondApprover)
        {
            var setChosenStartDate = startDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = startDate, _scenarioContext["savedStartDate"] = startDate),
            };

            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();

            var endDate = DateTime.Now.AddDays(maturityDate).ToString("dd-MM-yyyy");

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedMaturityDate"] = endDate;

            await _sharedModel.DepositCaseSetOfferFields(_setCDDandDealDateTime, notional);
            await _shortModel.LoanUpdateShortCaseSetDateFields(ChosenStartDate, endDate);

            if (secondApprover)
            {
                await _shortModel.LoanUpdateShortCaseSetSpreadFieldsFourEyes("2,48", "2,51", "3,581", "2,581", "DEPOSIT", lalCaseNumber, true);
            }
            else
            {
                await _shortModel.LoanUpdateShortCaseSetSpreadFieldsTwoEyes("-0,525", "-0,525", "0,155", "DEPOSIT", lalCaseNumber, true);
            }
        }

        [When("I continue the emails should be send out and the case to approved by {string} to first BO stage LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheCaseToApprovedByToFirstBOStageLoanDataApproval(string approverBoC)
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var spreadCheckSetToFourEyes = (bool)_scenarioContext["savedSpreadCheck"];
            var chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            var savedStartDate = (string)_scenarioContext["savedStartDate"];
            var savedMaturityDate = (string)_scenarioContext["savedMaturityDate"];
            var savedCounterParty = (string)_scenarioContext["savedCounterParty"];
            await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();

            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);

            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, _, _) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            for (int i = 0; i < _sharedDataIndividual.Customers.Count; i++)
            {
                var emailAdress = _sharedDataIndividual.Customers[i].EmailIndividual.ToString();
                actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().NotContainEquivalentOf("Stichting Waarborgfonds Sociale Woningbouw");
            actualTable.Should().NotContainEquivalentOf("Zekerheid");

            actualTable.Should().Contain($"Offerte Deposito ({lalCaseNumber})");

            actualFootTextEmailBasic.Should().Contain("Op dit deposito zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.", Exactly.Once());
            actualFootTextEmailBasic.Should().Contain("The General Terms and Conditions of NWB Bank (available at https://nwbbank.com/public-finance/algemene-voorwaarden) and the General Credit Conditions of NWB Bank, which are attached to this email, will apply to the deposit.", Exactly.Once());
            await popupOfferEmail.CloseAsync();

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(600);
            await _basePageObject.LogOutSystem();

            Enum.TryParse(approverBoC, out TestUser enumApproverBoC);
            await _loginPageObject.LoginUser(enumApproverBoC);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Deposit, lalCaseNumber);

            var uGSheetData = await _billOfClosureObject.GetUGSheetDataToArray(LoanRequest.Deposit, true);
            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _loanUpdateBillOfClosureObject.ClickSubmitBillOfClosureCases();

            var aLUCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALLCaseNumber"] = aLUCaseNumber;
            await _sharedModel.LogOutFrontOfficeLogInBackOffice(PEGAClassStructure.New, aLUCaseNumber);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.DepositAdministration, aLUCaseNumber);
            await _loanUpdateLoanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();
            await Task.Delay(500);
        }

        [When("I go through all the steps of the back office for a deposit case with {string}")]
        public async Task WhenIGoThroughAllTheStepsOfTheBackOfficeForADepositCase(string product)
        {
            var aLLCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            await _loanUpdateLoanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _loanUpdateLoanDataApprovalObject.ClickSubmitButtonCheckProductData();
            await _basePageObject.WaitForResponse();

            // Sluitbrief controle
            var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
            var textFromSluitbrief = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

            textFromSluitbrief.Should().ContainAll($"Deposit - Primary", $"In offer case {lalCaseNumber} heeft trader", $"Product: {product}", "Product type: OG", "Act/360", $"€ {savedNotional}");
            await popupSluitbrief.CloseAsync();

            await _sendConfirmationObject.SetConfirmationSentToCustomer("Yes");
            await _basePageObject.PressTab();

            // Controle generatie DEPLO CSV
            var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
            getAllTextFromRecentContent.Should().Match("*DEPLO_*.csv*");

            await _sendConfirmationObject.ClickSubmitSendConfirmation();
        }
    }
}