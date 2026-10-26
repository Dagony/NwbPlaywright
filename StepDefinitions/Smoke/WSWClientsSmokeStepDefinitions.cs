using DocumentFormat.OpenXml.Wordprocessing;
using Reqnroll;
using System;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using static NUnit.Framework.Internal.OSPlatform;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class WSWClientsSmokeStepDefinitions
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
        private readonly IVariableModel _variableModel;
        private readonly ISharedModel _sharedModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IShortModel _shortModel;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly ISharedDataIndividuals _sharedDataIndividual;
        private readonly IFixedModel _fixedModel;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;

        public string ChosenStartDate { get; set; }

        public WSWClientsSmokeStepDefinitions(
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
            IVariableModel variableModel,
            ISharedModel sharedModel,
            ISharedDataTransforms sharedDataTransforms,
            IShortModel shortModel,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject)
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
            _variableModel = variableModel;
            _sharedModel = sharedModel;
            _sharedDataTransforms = sharedDataTransforms;
            _shortModel = shortModel;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            ChosenStartDate = _sharedModel.SharedEmptyString;
        }

        [Given("I create a {string} WSW LAL case on the front office with start date {string} and a maturity of {string} Years")]
        public async Task GivenICreateAWSWLALCaseOnTheFrontOfficeWithStartDateAndAMaturityOfYears(string productType, string startDate, int maturityInYears)
        {
            var setChosenStartDate = startDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = startDate, _scenarioContext["savedStartDate"] = startDate),
            };

            (Task emptyTask, Task<string> stringTask, Task emptyTask2, Task emptyTask3) tasks = productType switch
            {
                "LONG" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanLong), _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "VARIABLE" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFloatingNotional), _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "SHORT" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, true), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanShort), _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "LOANUPDATEVARIABLE" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.InterestSpreadReset), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFixedNotional), _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "LOANUPDATEFIXED" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.InterestSpreadReset), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, true), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanLong), _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                _ => throw new Exception("Unknown option chosen for switch expression INPUT"),
            };

            await tasks.emptyTask;
            var formattedMaturityDate = await tasks.stringTask;
            await tasks.emptyTask2;
            await tasks.emptyTask3;
            await _basePageObject.ClickSubmitButton();

            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-01-2027";
            var lastCouponDate = "20-01-2027";
            var interestRRDate = "27-01-2027";
            var couponDay = "22";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            if (productType != "DEPOSIT")
            {
                await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, notional);
                if (productType == "LONG")
                {
                    await _fixedModel.FixedCaseSetDateFields(ChosenStartDate, formattedMaturityDate, couponFreq: "Annual", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay);
                    await _fixedModel.FixedCaseSetAlternativeFields("Act/360", swap: false, amortType: "Annuity", numbOfAmort: $"{maturityInYears}", amortFreq: "Annual");
                    await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("15", "35", "3", productType, lalCaseNumber, true);
                }
                else if (productType == "VARIABLE")
                {
                    await _finalSpreadObject.SetCommitmentFee("25");
                    await _variableModel.VariableCaseSetDateFields(ChosenStartDate, formattedMaturityDate, "12mE", firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: "22");
                    await _variableModel.VariableSetSpreadFieldsTwoEyes("12mE", productType, lalCaseNumber, h1: "2,50", h1Basis: "2,50", finalSpread: "2,50", finalBasis: "15,75", setSpreadMessages: true);
                }
            }
            else if (productType == "DEPOSIT")
            {
                await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, notional);
                await _shortModel.LoanUpdateShortCaseSetDateFields(ChosenStartDate, formattedMaturityDate);
                await _shortModel.LoanUpdateShortCaseSetSpreadFieldsTwoEyes("4,85", "4,95", "95", productType, lalCaseNumber, true);
            }
        }

        [When("I continue the emails should be send out and the WSW case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheWSWCaseToBOFirstFlowLoanDataApproval()
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();

            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);
            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, _, _) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            var loanType = (string)_scenarioContext["savedLoanType"];

            for (int i = 0; i < _sharedDataIndividual.Customers.Count; i++)
            {
                var emailAdress = _sharedDataIndividual.Customers[i].EmailIndividual.ToString();
                actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().ContainEquivalentOf("Zekerheid" + "\t" + "Stichting Waarborgfonds Sociale Woningbouw");

            actualFootTextEmailBasic.Should().ContainEquivalentOf("Op deze lening zullen de “Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021” van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie.");
            actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WSW standaard geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");

            var assertLoanType = productType switch
            {
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => actualTable.Should().Contain($"Offerte Vastrentende lening ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => actualTable.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => actualTable.Should().Contain($"Offerte Roll-over lening met variabele hoofdsom ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => actualTable.Should().Contain("Offerte Kasgeld"),
                _ => throw new Exception("Unknown option chosen for switch expression ASSERTION LOANTYPE"),
            };

            await popupOfferEmail.CloseAsync();

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                await _winLostCorrectObject.OpenAttachment();
                var nameAttachment = await _winLostCorrectObject.GetNameAttachmentOfferMail();
                nameAttachment.Should().Be("Nederlandse Waterschapsbank N.V. - Algemene Kredietvoorwaarden.pdf");
                await _basePageObject.CloseAttachmentFile();
            }

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();
            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();

            await Task.Delay(600);
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();
            await _basePageObject.SearchCaseNumberMainScreen(Fields.EnumsForTRX.WorkListType.Loan, lalCaseNumber);

            var savedStartDate = (string)_scenarioContext["savedStartDate"];
            var savedMaturityDate = (string)_scenarioContext["savedMaturityDate"];
            var savedCounterParty = (string)_scenarioContext["savedCounterParty"];

            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();

            var allCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALLCaseNumber"] = allCaseNumber;
            await Task.Delay(1000);
            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                await _billOfClosureObject.ClickALLCaseNumber(allCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(2500);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, allCaseNumber);
        }

        [When("I go through all the steps of the back office for a WSW case")]
        public async Task WhenIGoThroughAllTheStepsOfTheBackOfficeForAWSWCase()
        {
            var allCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _sendConfirmationObject.ClickSubmitSendConfirmation();

            await _validateRequestObject.SetCounterPartyConfirmReceived(true);
            await _validateRequestObject.SetIsTheLoanGuaranteed(true);
            await _validateRequestObject.SetWhatIsTheLoanRequest("Split");
            await _validateRequestObject.SetBillOfClosureConfirmedByPF(true);
            await _validateRequestObject.SetMunicipalDecisionForGuaranty("Yes");
            await _validateRequestObject.ClickSubmitValidationPage();

            await Task.Delay(1000);

            try
            {
                await Task.Delay(1000);
                var recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                while (!recentContentFrameBOtext.Contains("No more data to load"))
                {
                    await Task.Delay(1000);
                    await _basePageObject.ClickLoadMore();
                    await Task.Delay(1000);
                    recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                    await Task.Delay(1000);
                }
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            if (loanType == "VARIABLE")
            {
                try
                {
                    // Sluitbrief controle
                    var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                    var sluitBriefText = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                    sluitBriefText.Should().ContainAll(
                        $"Pega case\t{allCaseNumber}",
                        $"Loan application case\t{lalCaseNumber}",
                        "Security ID\t2 ",
                        "Rate type\tFloat",
                        "Product Type\tUG",
                        "Day count convention\tAct/360",
                        "Guarantor name\tStichting Waarborgfonds Sociale Woningbouw",
                        "Customer rate\tEuribor 12 months");
                    await popupSluitbrief.CloseAsync();
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else if (loanType == "LONG")
            {
                try
                {
                    // Sluitbrief controle
                    var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                    var sluitBriefText = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                    sluitBriefText.Should().ContainAll(
                        $"Pega case\t{allCaseNumber}",
                        $"Loan application case\t{lalCaseNumber}",
                        "Security ID\t1 ",
                        "Rate type�\tFixed",
                        "Product Type\tUG",
                        "Day count convention\tAct/360",
                        $"Notional\t{savedNotional}",
                        "Guarantor name\tStichting Waarborgfonds Sociale Woningbouw");
                    await popupSluitbrief.CloseAsync();
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            await Task.Delay(1000);

            // controle generatie csv files.
            var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
            getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WSW Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WSW Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WSW Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");

            await _createContractObject.ClickSubmitButtonCreateAgreement();

            await _surePayCheckObject.SetIsTheIbanInOPICS("Yes");
            await _surePayCheckObject.SetIstheIBANverifiedInSurePay("Yes");
            await _surePayCheckObject.ClickSubmitButtonSurePayCheckPage();

            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginBackOffice2();
            await Task.Delay(1500);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, allCaseNumber);
            await _agreementReviewObject.SideLetterInDossier(true);
            await _agreementReviewObject.AgreementReviewed(true);
            await _agreementReviewObject.ClickSubmitButtonAgreementReview();

            await _sharedModel.GuarantorSteps();

            await _sendAgreementObject.OpenSendAgreementLink();
            await _sendAgreementObject.ClickSubmitSendAgreementPage();

            await _clientApprovalObject.ClickApproveButtonClientApprovalPage();

            await _processClientResponseObject.OpenCheckCDDLink();
            await _processClientResponseObject.AreTheSubscriberInCDD(true);
            await _processClientResponseObject.IsPFNotifiedToExecuteCDD("Yes");
            await _processClientResponseObject.AreTheStandardCDDActivitiesExecuted("Yes");
            await _processClientResponseObject.IsTheGuarantorAuthenticatedAndVerified("Not applicable");

            await _processClientResponseObject.ClickSubmitButtonCDDPage();

            await _legalReviewObject.ClickSubmitButtonPendingLegalReviewPage();
            await Task.Delay(500);
            await _checkDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();

            await _checkDossierCompleteObject.FullSignedAgreementInDossier(true);
            await _checkDossierCompleteObject.AuthenticityofAgreementChecked(true);
            await _checkDossierCompleteObject.FullSignedAgreementSendOutToAllParties(true);
            await _checkDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();
            await _basePageObject.WaitForPageLoadedState();
        }

        [Then("the WSW backoffice case is succesfully created")]
        public async Task ThenTheWSWBackofficeCaseIsSuccesfullyCreated()
        {
            throw new PendingStepException();
        }
    }
}