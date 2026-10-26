using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using NwbPlaywright.PageObjects.TRX;
using static NUnit.Framework.Internal.OSPlatform;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class WFZClientsSmokeStepDefinitions
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
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly ISharedModel _sharedModel;
        private readonly IVariableModel _variableModel;
        private readonly IShortModel _shortModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly IFixedModel _fixedModel;
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
        private readonly ILoanUpdateCollectDocumentInformationObject _loanUpdateCollectDocumentInformationObject;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;

        public string ChosenStartDate { get; set; }

        public WFZClientsSmokeStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ISharedDataIndividuals sharedDataIndividuals,
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
            ILoanUpdateCheckDossierCompleteObject loanUpdateCheckDossierCompleteObject,
            ILoanUpdateCollectDocumentInformationObject loanUpdateCollectDocumentInformationObject)
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
            _sharedDataIndividuals = sharedDataIndividuals;
            _fixedModel = fixedModel;
            _checkDossierCompleteObject = checkDossierCompleteObject;
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
            _loanUpdateCollectDocumentInformationObject = loanUpdateCollectDocumentInformationObject;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            ChosenStartDate = _sharedModel.SharedEmptyString;
        }

        [Given("I create a {string} WFZ LR case on the front office with start date {string} and a maturity of {string} Years")]
        public async Task GivenICreateAWFZLRCaseOnTheFrontOfficeWithStartDateAndAMaturityOfYears(string productType, string startDate, int maturityInYears)
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
                "LONG" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanLong),
                 _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "VARIABLE" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFloatingNotional),
            _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "SHORT" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, true), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanShort),
            _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "LOANUPDATEVARIABLE" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.InterestSpreadReset), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFixedNotional),
                  _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "LOANUPDATEFIXED" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.InterestSpreadReset), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, true), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanLong),
            _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
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
                    await _fixedModel.FixedCaseSetDateFields(
                        ChosenStartDate,
                        formattedMaturityDate,
                        couponFreq: "Quarterly",
                        firstCouponDate,
                        lastCouponDate,
                        interestRRDate,
                        couponDay: couponDay);
                    await _fixedModel.FixedCaseSetAlternativeFields(
                        "30/360",
                        swap: false,
                        amortType: "Linear",
                        numbOfAmort: "99",
                        amortFreq: "Quarterly");
                    await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("15,5", "15,5", "3", productType, lalCaseNumber, true);
                }
                else if (productType == "VARIABLE")
                {
                    await _finalSpreadObject.SetMinimalWithdrawalPercent("50");
                    await _finalSpreadObject.SetCommitmentFee("50");
                    await _variableModel.VariableCaseSetDateFields(
                        ChosenStartDate,
                        formattedMaturityDate,
                        "6mE",
                        firstCouponDate: firstCouponDate,
                        lastCouponDate: lastCouponDate,
                        interestRRDate: interestRRDate,
                        couponDay: "22");
                    await _variableModel.VariableSetSpreadFieldsTwoEyes("6mE", productType, lalCaseNumber, h1: "-2,85", finalSpread: "-2,85", setSpreadMessages: false);
                }
                else if (productType == "SHORT")
                {
                    await _shortModel.ShortCaseSetDateFields(ChosenStartDate, formattedMaturityDate);
                    await _shortModel.ShortCaseSetSpreadFieldsTwoEyes("4,85", "4,85", "85", productType, lalCaseNumber, true);
                }
            }
            else if (productType == "DEPOSIT")
            {
                await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, notional);
                await _shortModel.LoanUpdateShortCaseSetDateFields(ChosenStartDate, formattedMaturityDate);
                await _shortModel.LoanUpdateShortCaseSetSpreadFieldsTwoEyes("4,85", "4,95", "95", productType, lalCaseNumber, true);
            }
        }

        [Given("I create a WFZ loan update case on the front office with start date {string} and a maturity of {string} Years")]
        public async Task GivenICreateAWFZLoanUpdateCaseOnTheFrontOfficeWithStartDateAndAMaturityOfYears(string startDate, int maturityInYears)
        {
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var setChosenStartDate = startDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = startDate, _scenarioContext["savedStartDate"] = startDate),
            };

            Task<string> stringTask = productType switch
            {
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false),
                _ => throw new Exception("Unknown option chosen for switch expression INPUT"),
            };

            var formattedMaturityDate = await stringTask;

            // ivm met min max withdrawal implementatie sprint 8-2025
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(3000000, 15000000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-01-2027";
            var lastCouponDate = "20-01-2027";
            var interestRRDate = "27-01-2027";
            var couponDay = "22";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, notional);

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
            {
                var couponFreqValue = await _loanUpdateFinalSpreadObject.GetValueOfCouponFrequency();
                await _fixedModel.LoanUpdateFixedCaseSetDateFields(
                    ChosenStartDate,
                    formattedMaturityDate,
                    couponFreq: couponFreqValue,
                    firstCouponDate,
                    lastCouponDate,
                    interestRRDate,
                    couponDay: couponDay);
                await _finalSpreadObject.SetStartDate(ChosenStartDate);
                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(
                    swap: false,
                    amortType: "Linear",
                    numbOfAmort: "99",
                    amortFreq: "Quarterly");
                await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("15,5", "15,5", "3", loanType, lalCaseNumber, true);
            }
            else if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional || productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                await _variableModel.LoanUpdateVariableCaseSetDateFields(
                    ChosenStartDate,
                    formattedMaturityDate,
                    "6mE",
                    firstCouponDate: firstCouponDate,
                    lastCouponDate: lastCouponDate,
                    interestRRDate: interestRRDate,
                    couponDay: couponDay);
                await _finalSpreadObject.SetStartDate(ChosenStartDate);
                await _variableModel.LoanUpdateVariableSetSpreadFieldsTwoEyes("6mE", loanType, lalCaseNumber, h1: "-2,85", finalSpread: "-2,85", setSpreadMessages: false);
            }
        }

        [When("I continue the emails should be send out and the WFZ case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheWFZCaseToBOFirstFlowLoanDataApproval()
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();

            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);
            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, _, _) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            var loanType = (string)_scenarioContext["savedLoanType"];

            for (int i = 0; i < _sharedDataIndividuals.Customers.Count; i++)
            {
                if (_sharedDataIndividuals.Customers[i].EmailIndividual is not null)
                {
                    string emailAdress = _sharedDataIndividuals.Customers[i].EmailIndividual!.ToString();
                    actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
                }
                else
                {
                    throw new Exception($"Emailadress of individual (origin: _sharedDataIndividual.Customers[{i}].EmailIndividual)");
                }
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().ContainEquivalentOf("Zekerheid" + "\t" + "Waarborgfonds voor de Zorgsector");

            var assertLoanType = productType switch
            {
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => actualTable.Should().Contain($"Offerte Vastrentende lening ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => actualTable.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => actualTable.Should().Contain($"Offerte Roll-over lening met variabele hoofdsom ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => actualTable.Should().Contain("Offerte Kasgeld"),
                _ => throw new Exception("Unknown option chosen for switch expression ASSERTION LOANTYPE"),
            };

            if (productType != EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WfZ standaard geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
            }
            else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen.");
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. Bovenstaande is onder voorbehoud van typefouten.");
            }

            await popupOfferEmail.CloseAsync();

            var isAttachmentVisible = await _winLostCorrectObject.IsAttachmentVisible();
            isAttachmentVisible.Should().BeFalse();

            await _winLostCorrectObject.ClickWinLostCorrect("Won");

            if (productType != EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                var suretyDate = _sharedDataTransforms.GetDateYesterday();
                await _winLostCorrectObject.SetSuretyStatementNumberAndDate("99999999999999999911", suretyDate);
            }
            else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                await _winLostCorrectObject.SetSuretyStatementNumber("99999999999999999933");
            }

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

            var aLLCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALLCaseNumber"] = aLLCaseNumber;
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
                await _billOfClosureObject.ClickALLCaseNumber(aLLCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(2500);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, aLLCaseNumber);
        }

        [When("I continue the emails should be send out and the loan update WFZ case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheLoanUpdateWFZCaseToBOFirstFlowLoanDataApproval()
        {
            var rLUCaseNumber = await _finalSpreadObject.GetCaseID();
            await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();

            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);

            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, actualTableENG, actualAanhefENG) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            for (int i = 0; i < _sharedDataIndividuals.Customers.Count; i++)
            {
                if (_sharedDataIndividuals.Customers[i].EmailIndividual is not null)
                {
                    string emailAdress = _sharedDataIndividuals.Customers[i].EmailIndividual!.ToString();
                    actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
                }
                else
                {
                    throw new Exception($"Emailadress of individual (origin: _sharedDataIndividual.Customers[{i}].EmailIndividual)");
                }
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().ContainEquivalentOf("Zekerheid" + "\t" + "Waarborgfonds voor de Zorgsector");

            var assertLoanType = productType switch
            {
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => actualTable.Should().Contain($"Vastrentende lening ({rLUCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => actualTable.Should().Contain($"Roll-over lening met vaste hoofdsom ({rLUCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => actualTable.Should().Contain($"Roll-over lening met variabele hoofdsom ({rLUCaseNumber})"),
                _ => throw new Exception("Unknown option chosen for switch expression ASSERTION LOANTYPE"),
            };

            actualFootTextEmailBasic.Should().ContainEquivalentOf("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
            actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");

            await popupOfferEmail.CloseAsync();

            var isAttachmentVisible = await _winLostCorrectObject.IsAttachmentVisible();
            isAttachmentVisible.Should().BeFalse();

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(600);
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdate, rLUCaseNumber);
            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _loanUpdateBillOfClosureObject.ClickSubmitBillOfClosureCases();

            await Task.Delay(1000);
            var aLUCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALUCaseNumber"] = aLUCaseNumber;

            await _sharedModel.LogOutFrontOfficeLogInBackOffice(PEGAClassStructure.New, aLUCaseNumber);

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, aLUCaseNumber);

            await _loanUpdateLoanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();
        }

        [When("I go through all the steps of the back office for a WFZ case")]
        public async Task WhenIGoThroughAllTheStepsOfTheBackOfficeForAWFZCase()
        {
            var aLLCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _sendConfirmationObject.ClickSubmitSendConfirmation();

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                await _sendConfirmationObject.SetConfirmationSentToCustomer("Yes");
                await _sendConfirmationObject.SetConfirmationSentToGuarantor("Yes");

                // Sluitbrief controle
                var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                var textFromSluitbrief = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);
                textFromSluitbrief.Should().ContainAll(
                    $"Traditional fixed rate loan Short - Primary",
                    $"In offer case {lalCaseNumber}",
                    "Product: KORTLE",
                    "Product type: UG",
                    "Act/360",
                    $"{savedNotional}",
                    "Waarborgfonds voor de Zorgsector");

                await popupSluitbrief.CloseAsync();

                // Controle generatie DEPLO CSV
                var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
                getAllTextFromRecentContent.Should().Match("*DEPLO_*.csv*");
                getAllTextFromRecentContent.Should().NotContain("WfZ Contract.docx");

                await _sendConfirmationObject.ClickSubmitSendConfirmation();
                await _surePayCheckObject.SetIsTheIbanInOPICS("Yes");
                await _basePageObject.PressTab();
                await _surePayCheckObject.SetIstheIBANverifiedInSurePay("Yes");
                await _basePageObject.PressTab();
                await _surePayCheckObject.ClickSubmitButtonSurePayCheckPage();
                await _basePageObject.WaitForPageLoadedState();
            }
            else
            {
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
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional || productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
                {
                    try
                    {
                        // Sluitbrief controle
                        var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                        var sluitBriefText = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                        sluitBriefText.Should().ContainAll(
                            $"Floating rate loan Floating notional - Primary",
                            $"In offer case {lalCaseNumber} heeft trader",
                            "Security ID: 2",
                            "Product: OHLE",
                            "Product type: UG",
                            "Act/360",
                            "Waarborgfonds voor de Zorgsector",
                            "6mE -2.85");
                        await popupSluitbrief.CloseAsync();

                        await Task.Delay(1000);

                        // controle generatie csv files.
                        var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");
                        getAllTextFromRecentContent.Should().NotContain("WfZ Contract.docx");
                    }
                    catch (PlaywrightException ex)
                    {
                        // Handle any potential errors, such as element not found
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
                else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
                {
                    try
                    {
                        // Sluitbrief controle
                        var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                        var sluitBriefText = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                        sluitBriefText.Should().ContainAll(
                            $"In offer case {lalCaseNumber} heeft trader",
                            $"Traditional fixed rate loan Long - Primary",
                            "Notional type: Fixed",
                            "Loan type: Normaal",
                            "30/360",
                            $"{savedNotional}",
                            "Waarborgfonds voor de Zorgsector");

                        await popupSluitbrief.CloseAsync();

                        await Task.Delay(1000);

                        // controle generatie csv files.
                        var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WfZ Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WfZ Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
                        getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "WfZ Contract.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");
                    }
                    catch (PlaywrightException ex)
                    {
                        // Handle any potential errors, such as element not found
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }

                await _createContractObject.ClickSubmitButtonCreateAgreement();
                await _surePayCheckObject.SetIsTheIbanInOPICS("Yes");
                await _surePayCheckObject.SetIstheIBANverifiedInSurePay("Yes");
                await _surePayCheckObject.ClickSubmitButtonSurePayCheckPage();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice2();
                await Task.Delay(1500);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, aLLCaseNumber);
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
        }

        [When("I go through all the steps of the loan update back office for a WFZ case")]
        public async Task WhenIGoThroughAllTheStepsOfTheLoanUpdateBackOfficeForAWFZCase()
        {
            var aLUCaseNumber = (string)_scenarioContext["savedALUCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            await _loanUpdateLoanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _loanUpdateLoanDataApprovalObject.ClickSubmitButtonCheckProductData();
            await _basePageObject.ClickLoanUpdateOpenValidateRequestLink();

            // onderstaande uitgezet voor CLONE ENVIRONMENT pega upgrade//
            await _loanUpdateValidateRequestObject.SetCounterPartyConfirmReceived(true);

            await _loanUpdateValidateRequestObject.SetIsTheLoanGuaranteed(true);
            await _loanUpdateValidateRequestObject.SetWhatIsTheLoanRequest("Split");
            await _loanUpdateValidateRequestObject.SetBillOfClosureConfirmedByPF(true);
            await _loanUpdateValidateRequestObject.ClickSubmitValidationPage();

            // nieuwe stap alleen bij normal clients en waterschappen!
            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
            {
                await _loanUpdateCollectDocumentInformationObject.SetRepaymentAmount("250125100,95");
                await _basePageObject.PressTab();
                await _loanUpdateCollectDocumentInformationObject.ClickSubmitLoanUpdateCollectDocumentInformation();
            }

            await Task.Delay(2000);

            if ((productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional && loanType == "INTERESTSPREADRESET")
                || (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional && loanType == "INTERESTSPREADRESET"))
            {
                try
                {
                    // Sluitbrief controle
                    var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefLoanUpdate);
                    var textFromSluitbrief = await _loanUpdateLoanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                    textFromSluitbrief.Should().ContainAll(
                        $"In offer case {lalCaseNumber} heeft trader",
                        "Original Security ID",
                        "Floating rate loan Floating notional - Spread reset",
                        "Product: OHLE",
                        "Product type: UG",
                        "Act/360",
                        $"{savedNotional}",
                        "6mE",
                        "Waarborgfonds voor de Zorgsector (0636907813)",
                        "Customer spread");
                    await popupSluitbrief.CloseAsync();
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    // Sluitbrief controle
                    var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefLoanUpdate);
                    var textFromSluitbrief = await _loanUpdateLoanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                    textFromSluitbrief.Should().ContainAll(
                        $"In offer case {lalCaseNumber} heeft trader",
                        "Original Security ID",
                        "Traditional fixed rate loan Long - Interest reset",
                        "Product: OHLE",
                        "Act/365",
                        "Product type: UG",
                        $"{savedNotional}",
                        "Waarborgfonds voor de Zorgsector (0636907813)",
                        "Customer rate");
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
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");

            await _loanUpdateCreateContractObject.ClickSubmitButtonCreateAgreement();

            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginBackOffice2();
            await Task.Delay(1500);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, aLUCaseNumber);

            await _loanUpdateAgreementReviewObject.SideLetterInDossier(true);
            await _loanUpdateAgreementReviewObject.AgreementReviewed(true);
            await _loanUpdateAgreementReviewObject.ClickSubmitButtonAgreementReview();

            // new geimplementeerd nav back to back processing:
            ////////////////////////////////////////////////////////////////////////////////
            await _loanUpdateCheckGuarantorDocumentObject.OpenCheckGuarantorDocumentLink();
            /////////////////////////////////////////////////////////////////////////////////
            await _loanUpdateCheckGuarantorDocumentObject.ClickSubmitOpenCheckGuarantorDocumentPage();

            await _loanUpdateGuarantorApprovalObject.ClickApproveButtonGuarantorApprovalPage();

            await _loanUpdateSendAgreementObject.OpenSendAgreementLink();
            await _loanUpdateSendAgreementObject.ClickSubmitSendAgreementPage();

            await _loanUpdateClientApprovalObject.ClickApproveButtonClientApprovalPage();

            await _loanUpdateProcessClientResponseObject.OpenCheckCDDLink();
            await _loanUpdateProcessClientResponseObject.AreTheSubscriberInCDD(true);
            await _loanUpdateProcessClientResponseObject.IsPFNotifiedToExecuteCDD("Yes");
            await _loanUpdateProcessClientResponseObject.AreTheStandardCDDActivitiesExecuted("Yes");
            await _loanUpdateProcessClientResponseObject.IsTheGuarantorAuthenticatedAndVerified("Not applicable");

            await _loanUpdateProcessClientResponseObject.ClickSubmitButtonCDDPage();

            await _loanUpdateLegalReviewObject.ClickSubmitButtonPendingLegalReviewPage();
            await Task.Delay(500);
            await _loanUpdateCheckDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();

            await _loanUpdateCheckDossierCompleteObject.FullSignedAgreementInDossier(true);
            await _loanUpdateCheckDossierCompleteObject.AuthenticityofAgreementChecked(true);
            await _loanUpdateCheckDossierCompleteObject.FullSignedAgreementSendOutToAllParties(true);
            await _loanUpdateCheckDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();
            await _basePageObject.WaitForPageLoadedState();
        }
    }
}