using System.Formats.Tar;
using System.Transactions;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.TRX;
using static TA.Playwright.Fields.EnumsForCLM;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class NormalClientsSmokeStepDefinitions
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
        private readonly ILoanUpdateCollectDocumentInformationObject _loanUpdateCollectDocumentInformationObject;
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

        public NormalClientsSmokeStepDefinitions(
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
            ILoanUpdateCollectDocumentInformationObject loanUpdateCollectDocumentInformationObject,
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
            _loanUpdateCollectDocumentInformationObject = loanUpdateCollectDocumentInformationObject;
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

        [Given(@"I create a ""([^""]*)"" LAL case on the front office with start date ""([^""]*)"" and a maturity of ""([^""]*)"" Years")]
        public async Task GivenICreateALALCaseOnTheFrontOfficeWithStartDateAndAMaturityOfYears(string productType, string startDate, int maturityInYears)
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
                "VARIABLE" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, false), _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFixedNotional),
            _basePageObject.SelectLoanEvent(SelectEvent.Primary)),
                "SHORT" => (_basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Loan), _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, maturityInYears, true), _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanShort),
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
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, notional);

            if (productType == "LONG")
            {
                await _fixedModel.FixedCaseSetDateFields(
                    ChosenStartDate,
                    formattedMaturityDate,
                    "Annual",
                    firstCouponDate,
                    lastCouponDate,
                    interestRRDate,
                    couponDay: couponDay);
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet");
                await _fixedModel.FixedCaseSetSpreadFieldsFourEyes("11", "12", "19", "4", productType, lalCaseNumber, true);
            }
            else if (productType == "VARIABLE")
            {
                await _variableModel.VariableCaseSetDateFields(
                    ChosenStartDate,
                    formattedMaturityDate,
                    "1wE",
                    hoursClause: true,
                    firstCouponDate: firstCouponDate,
                    lastCouponDate: lastCouponDate,
                    interestRRDate: interestRRDate,
                    couponDay: couponDay);
                await _variableModel.VariableSetSpreadFieldsTwoEyes("1wE", productType, lalCaseNumber, h1: "-2,85", finalSpread: "45,84", setSpreadMessages: true, h1Basis: "-2,85", finalBasis: "10");
            }
            else if (productType == "SHORT")
            {
                await _shortModel.ShortCaseSetDateFields(ChosenStartDate, formattedMaturityDate);
                await _shortModel.ShortCaseSetSpreadFieldsTwoEyes("4,85", "4,95", "95", productType, lalCaseNumber, true);
            }
        }

        [Given("I create a loan update case on the front office with start date {string} and a maturity of {string} Years")]
        public async Task GivenICreateALoanUpdateCaseOnTheFrontOfficeWithStartDateAndAMaturityOfYears(string startDate, int maturityInYears)
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

            // ivm minimum en max withdrawal implementatie sprint 08-2025
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

                await _fixedModel.LoanUpdateFixedCaseSetDateFields(ChosenStartDate, formattedMaturityDate, couponFreq: couponFreqValue, firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay);
                await _finalSpreadObject.SetStartDate(ChosenStartDate);

                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(false, "Bullet");
                await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("-11", "129", "2", loanType, lalCaseNumber, setSpreadMessages: true);
            }
            else if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional || productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                await _variableModel.VariableCaseSetDateFields(ChosenStartDate, formattedMaturityDate, "1wE", hoursClause: true, firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: couponDay);
                await _finalSpreadObject.SetStartDate(ChosenStartDate);
                await _variableModel.VariableSetSpreadFieldsTwoEyes("1wE", loanType, lalCaseNumber, h1: "-2,85", finalSpread: "45,84", setSpreadMessages: true, h1Basis: "-2,85", finalBasis: "10");
            }
        }

        [When(@"I continue the emails should be send out and the case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheCaseToBOFirstFlowLoanDataApproval()
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var spreadCheckSetToFourEyes = (bool)_scenarioContext["savedSpreadCheck"];
            var chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();

            await Task.Delay(2000);

            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);
            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, _, _) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            for (int i = 0; i < _sharedDataIndividual.Customers.Count; i++)
            {
                var emailAdress = _sharedDataIndividual.Customers[i].EmailIndividual.ToString();
                actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().NotContainEquivalentOf("Zekerheid" + "\t" + "Stichting Waarborgfonds Sociale Woningbouw");
            actualTable.Should().NotContainEquivalentOf("Zekerheid");

            var assertLoanType = productType switch
            {
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => actualTable.Should().Contain($"Offerte Vastrentende lening ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => actualTable.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom ({lalCaseNumber})"),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => actualTable.Should().Contain("Offerte Kasgeld"),
                _ => throw new Exception("Unknown option chosen for switch expression ASSERTION LOANTYPE"),
            };

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door u gevraagde bedrag dient te worden overgemaakt. Deze e-mail, uw acceptatie middels een reply op deze e-mail, de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) vormen tezamen een leningovereenkomst.");
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. Bovenstaande is onder voorbehoud van typefouten.");
            }
            else if (loanType == "DEPOSIT")
            {
                actualFootTextEmailBasic.Should().Contain("Op dit deposito zullen de Algemene Voorwaarden NWB Bank van toepassing zijn (deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden) en de Algemene Kredietvoorwaarden NWB Bank, welke bij deze e-mail zijn aangehecht.", Exactly.Once());
                actualFootTextEmailBasic.Should().Contain("The General Terms and Conditions of NWB Bank (available at https://nwbbank.com/public-finance/algemene-voorwaarden) and the General Credit Conditions of NWB Bank, which are attached to this email, will apply to the deposit.", Exactly.Once());
            }
            else
            {
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door u gevraagde bedrag dient te worden overgemaakt. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                actualFootTextEmailBasic.Should().ContainEquivalentOf("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");
            }

            await popupOfferEmail.CloseAsync();
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(600);
            await _basePageObject.LogOutSystem();

            if (spreadCheckSetToFourEyes == true)
            {
                await _loginPageObject.LoginAccountDeskUser1();
            }
            else
            {
                await _loginPageObject.LoginAccountDeskUser2();
            }

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);

            var savedStartDate = (string)_scenarioContext["savedStartDate"];
            var savedMaturityDate = (string)_scenarioContext["savedMaturityDate"];
            var savedCounterParty = (string)_scenarioContext["savedCounterParty"];

            if (loanType == "DEPOSIT")
            {
                await _billOfClosureObject.ApprovedRejectOfferBoC(true);
                await _loanUpdateBillOfClosureObject.ClickSubmitBillOfClosureCases();
                var aluCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
                _scenarioContext["savedALLCaseNumber"] = aluCaseNumber;
                await _sharedModel.LogOutFrontOfficeLogInBackOffice(PEGAClassStructure.New, aluCaseNumber);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.DepositAdministration, aluCaseNumber);
                await _loanUpdateLoanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();
            }
            else
            {
                await _billOfClosureObject.ApprovedRejectOfferBoC(true);
                await _billOfClosureObject.ClickSubmitBillOfClosureCases();
                var allCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
                _scenarioContext["savedALLCaseNumber"] = allCaseNumber;

                await _sharedModel.LogOutFrontOfficeLogInBackOffice(PEGAClassStructure.Old, allCaseNumber);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, allCaseNumber);
            }
        }

        [When("I continue the emails should be send out and the loan update case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheEmailsShouldBeSendOutAndTheLoanUpdateCaseToBOFirstFlowLoanDataApproval()
        {
            var rluCaseNumber = await _finalSpreadObject.GetCaseID();
            await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();

            var popupOfferEmail = await _winLostCorrectObject.OpenPopupWindow(PopupWindow.Offertemail);
            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, _, _) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupOfferEmail);
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            for (int i = 0; i < _sharedDataIndividual.Customers.Count; i++)
            {
                var emailAdress = _sharedDataIndividual.Customers[i].EmailIndividual.ToString();
                actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
            }

            actualAanhef.Should().ContainEquivalentOf("Beste Allen,");
            actualTable.Should().NotContainEquivalentOf("Zekerheid" + "\t" + "Stichting Waarborgfonds Sociale Woningbouw");

            var assertLoanType = productType switch
            {
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => actualTable.Should().Contain($"Vastrentende lening ({rluCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => actualTable.Should().Contain($"Roll-over lening met vaste hoofdsom ({rluCaseNumber})"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => actualTable.Should().Contain($"Roll-over lening met variabele hoofdsom ({rluCaseNumber})"),
                _ => throw new Exception("Unknown option chosen for switch expression ASSERTION LOANTYPE"),
            };

            actualFootTextEmailBasic.Should().ContainEquivalentOf("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
            actualFootTextEmailBasic.Should().ContainEquivalentOf("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
            await popupOfferEmail.CloseAsync();

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();

            await Task.Delay(600);
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdate, rluCaseNumber);

            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            await Task.Delay(1000);
            var aluCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALUCaseNumber"] = aluCaseNumber;

            await _sharedModel.LogOutFrontOfficeLogInBackOffice(PEGAClassStructure.New, aluCaseNumber);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, aluCaseNumber);
            await _loanUpdateLoanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();
        }

        [When(@"I go through all the steps of the back office for a NON WSW, NON WFZ case")]
        public async Task WhenIGoThroughAllTheStepsOfTheBackOfficeForANONWSWNONWFZcase()
        {
            var allCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            // nieuw toegevoegd in new class structure: stap is extra toegevoegd tov oude implemnt:
            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _sendConfirmationObject.ClickSubmitSendConfirmation();

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                await Task.Delay(1000);

                // Sluitbrief controle
                var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
                var textFromSluitbrief = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

                textFromSluitbrief.Should().ContainAll($"Traditional fixed rate loan Short - Primary", $"In offer case {lalCaseNumber}", "Product: KORTLE", "Product type: UG", "Act/360", $"{savedNotional}");

                await popupSluitbrief.CloseAsync();

                // Controle generatie DEPLO CSV
                var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
                getAllTextFromRecentContent.Should().Match("*DEPLO_*.csv*");

                await _sendConfirmationObject.SetIsTheIbanInOPICS("Yes");
                await _basePageObject.PressTab();
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
                // onderstaande uitgezet voor CLONE ENVIRONMENT pega upgrade//
                await _validateRequestObject.SetCounterPartyConfirmReceived(true);

                await _validateRequestObject.SetIsTheLoanGuaranteed(true);
                await _validateRequestObject.SetWhatIsTheLoanRequest("Split");
                await _validateRequestObject.SetBillOfClosureConfirmedByPF(true);
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

                        sluitBriefText.Should().ContainAll($"In offer case {lalCaseNumber} heeft trader", $"Floating rate loan Fixed notional - Primary", "Security ID: 1", "Notional type: Fixed", "Loan type: Normaal", "Act/360", "1wE", $"{savedNotional}");

                        await popupSluitbrief.CloseAsync();
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

                        sluitBriefText.Should().ContainAll($"In offer case {lalCaseNumber} heeft trader", "Traditional fixed rate loan Long - Primary", "Security ID: 1", "Notional type: Fixed", "Loan type: Normaal", "Act/Act", $"{savedNotional}");

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
                getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
                getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
                getAllTextFromRecentContent.Should().ContainAll("NWB contract cover sheet.docx", "SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");

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

                await _checkDossierCompleteObject.FullSignedAgreementInDossier(true);
                await _checkDossierCompleteObject.AuthenticityofAgreementChecked(true);
                await _checkDossierCompleteObject.FullSignedAgreementSendOutToAllParties(true);
                await _checkDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();
                await _basePageObject.WaitForPageLoadedState();
            }
        }

        [When("I go through all the steps of the loan update back office for a NON WSW, NON WFZ case")]
        public async Task WhenIGoThroughAllTheStepsOfTheLoanUpdateBackOfficeForANONWSWNONWFZCase()
        {
            var aluCaseNumber = (string)_scenarioContext["savedALUCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var savedNotional = (string)_scenarioContext["savedNotional"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            await _loanUpdateLoanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _loanUpdateLoanDataApprovalObject.ClickSubmitButtonCheckProductData();

            // onderstaande uitgezet voor CLONE ENVIRONMENT pega upgrade//
            await _loanUpdateValidateRequestObject.SetCounterPartyConfirmReceived(true);

            // /////////////////// //   await _validateRequestObject.SetOtherTypeOfGuaranty("No");
            await _loanUpdateValidateRequestObject.SetIsTheLoanGuaranteed(true);
            await _loanUpdateValidateRequestObject.SetWhatIsTheLoanRequest("Split");
            await _loanUpdateValidateRequestObject.SetBillOfClosureConfirmedByPF(true);
            await _loanUpdateValidateRequestObject.ClickSubmitValidationPage();

            // nieuwe stap alleen bij normal clients en waterschappen!
            // INTERESTSPREADRESET
            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong && loanType == "INTERESTSPREADRESET")
            {
                await _loanUpdateCollectDocumentInformationObject.SetRepaymentAmount("250125100,95");
                await _basePageObject.PressTab();
                await _loanUpdateCollectDocumentInformationObject.ClickSubmitLoanUpdateCollectDocumentInformation();
            }

            await Task.Delay(2000);

            var popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefLoanUpdate);
            var textFromSluitbrief = await _loanUpdateLoanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);

            if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional && loanType == "INTERESTSPREADRESET")
            {
                try
                {
                    // Sluitbrief controle
                    textFromSluitbrief.Should().ContainAll(
                        $"In offer case {lalCaseNumber} heeft trader",
                        "Original Security ID",
                        "Product: OHLE",
                        "Loan type: Variabele hoofdsom",
                        "Act/360",
                        $"{savedNotional}",
                        "1wE");
                    await popupSluitbrief.CloseAsync();
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional && loanType == "INTERESTSPREADRESET")
            {
                try
                {
                    // Sluitbrief controle
                    textFromSluitbrief.Should().ContainAll($"In offer case {lalCaseNumber} heeft trader", "Original Security ID", "Product: OHLE", "Loan type: Normaal", "Act/360", $"{savedNotional}", "1wE");
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
                    textFromSluitbrief.Should().ContainAll($"In offer case {lalCaseNumber} heeft trader", "Traditional fixed rate loan Long - Interest reset", "Original Security ID", "Product: OHLE", "Bullet", "30/360", $"{savedNotional}");
                    await popupSluitbrief.CloseAsync();
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            await Task.Delay(500);

            // controle generatie csv files.
            var getAllTextFromRecentContent = await _basePageObject.GetTextRecentContentBO();
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*SECMNWB_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*SECM_*.csv*");
            getAllTextFromRecentContent.Should().ContainAll("SECMNWB_", "SECM_", "BRED_").And.Match("*BRED_*.csv*");

            await _loanUpdateCreateContractObject.ClickSubmitButtonCreateAgreement();

            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginBackOffice2();
            await Task.Delay(1500);

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, aluCaseNumber);

            await _loanUpdateAgreementReviewObject.OpenAgreementReviewLink();
            await _loanUpdateAgreementReviewObject.SideLetterInDossier(true);
            await _loanUpdateAgreementReviewObject.AgreementReviewed(true);
            await _loanUpdateAgreementReviewObject.ClickSubmitButtonAgreementReview();

            await _loanUpdateSendAgreementObject.OpenSendAgreementLink();
            await _loanUpdateSendAgreementObject.ClickSubmitSendAgreementPage();

            await _loanUpdateClientApprovalObject.ClickApproveButtonClientApprovalPage();

            // open check cdd link nog nodig ipv met voorgaande approval stap.
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