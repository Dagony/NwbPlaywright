using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class Regression1StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;

        // private readonly EnumsForTRX _enumsAllLoans;
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
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ISharedDataIndividuals _sharedDataIndividual;
        private readonly IFixedModel _fixedModel;

        public Regression1StepDefinitions(IPlaywrightDriver playwrightDriver, ISharedDataIndividuals sharedDataIndividual, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, IFixedModel fixedModel, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject, ISharedDataIndividuals sharedDataIndividuals, ISendConfirmationObject sendConfirmationObject, ISharedDataTransforms sharedDataTransforms)
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
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataIndividuals = sharedDataIndividuals;
            _sendConfirmationObject = sendConfirmationObject;
            _sharedDataTransforms = sharedDataTransforms;
        }

        [Given(@"I chose counterparty ""([^""]*)""")]
        public async Task GivenIChoseCounterparty(string counterparty)
        {
            await _newCaseObject.SetCounterparty(CaseType.LoanRequest, counterparty);
            await _basePageObject.WaitForPageLoadedState();
            await _basePageObject.PressArrowDown();
            await _basePageObject.PressEnter();
        }

        [Given(@"I chose the customer\(s\) ""([^""]*)""\. ""([^""]*)"" and ""([^""]*)""")]
        public async Task GivenIChoseTheCustomerS_And(string customerOne, string customerTwo, string customerThree)
        {
            await _newCaseObject.SelectCustomerNew(customerOne);
            await _newCaseObject.ClickSubmitCustomerAdvisorPage();
            await _basePageObject.WaitForPageLoadedState();
        }

        [Given(@"I chose the advisor\(s\) ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public async Task GivenIChoseTheAdvisorSAnd(string advisorOne, string advisorTwo, string advisorThree)
        {
            await _basePageObject.OpenTaskMenu();
        }

        [When(@"I make a case with the following Offer Data")]
        public async Task WhenIMakeACaseWithTheFollowingOfferData(Reqnroll.Table table)
        {
            LongFinalSpreadFields offerdata = table.CreateInstance<LongFinalSpreadFields>();
            _scenarioContext["savedOfferCaseType"] = offerdata.OfferCaseType;
            _scenarioContext["savedSpreadCheckBy"] = offerdata.SpreadCheckBy.ToString();
            if (offerdata.OfferCaseType == "Variable")
            {
                await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Variable);
            }
            else if (offerdata.OfferCaseType == "Fixed")
            {
                await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Fixed);
            }

            var caseNumber = await _finalSpreadObject.GetCaseID();
            await _finalSpreadObject.SetCDDReviewDate(offerdata.CDDReviewDate);

            if (offerdata.SetGuarantor == "true")
            {
                await _finalSpreadObject.CheckGuarantorBox();
                await _finalSpreadObject.ClickAddGuarantorButton();
                await _finalSpreadObject.SetGuarantorField(offerdata.Guarantor1);
            }

            await _finalSpreadObject.CheckAdditionalEmailBox();
            await _finalSpreadObject.ClickAddEmailButton(4);
            await _finalSpreadObject.SetEmailAddress(offerdata.AdditEmail1, 1);
            await _finalSpreadObject.SetEmailAddress(offerdata.AdditEmail2, 2);
            await _finalSpreadObject.SpreadCheckBy(offerdata.SpreadCheckBy);
            await _finalSpreadObject.SpreadCheckSetRemarks(offerdata.SpreadCheckRemarks);
        }

        [When(@"I fill the case with the following Offer1 information")]
        public async Task WhenIFillTheCaseWithTheFollowingOfferInformation(Reqnroll.Table table)
        {
            LongFinalSpreadFields offerinformation = table.CreateInstance<LongFinalSpreadFields>();

            await _finalSpreadObject.SetNotional(offerinformation.Notional);
            await _finalSpreadObject.SetDealMoment(offerinformation.DealMoment);

            if (offerinformation.NotionalTypeIsVariable == "true")
            {
                await _finalSpreadObject.SetMinimalWithdrawalPercent(offerinformation.MinimalWithdrawal);
                await _finalSpreadObject.SetCommitmentFee(offerinformation.CommitmentFee);
            }

            await _finalSpreadObject.SetNotificationMinutes(offerinformation.NotificationMinutes);
        }

        [When(@"I fill the case with the following Alternative1 standard fields")]
        public async Task WhenIFillTheCaseWithTheFollowingAlternativeStandardFields(Reqnroll.Table table)
        {
            LongFinalSpreadFields alternative1data = table.CreateInstance<LongFinalSpreadFields>();

            var offerCaseType = (string)_scenarioContext["savedOfferCaseType"];

            await _finalSpreadObject.SetStartDate(alternative1data.StartDate);
            await _finalSpreadObject.SetMaturityDate(alternative1data.MaturityDate);
            await _finalSpreadObject.SetFirstCouponDate(alternative1data.FirstCouponDate);
            await _finalSpreadObject.SetLastCouponDate(alternative1data.LastCouponDate);
            await _finalSpreadObject.SetInterestSpreadRRDate(alternative1data.InterestSpreadRRDate);
            await _finalSpreadObject.SetCouponDay(alternative1data.CouponDay);

            if (offerCaseType == "Fixed")
            {
                await _finalSpreadObject.SetSwap(alternative1data.SwapNeeded);
            }
        }

        [When(@"I fill the case with the following Alternative1 Loan type fields")]
        public async Task WhenIFillTheCaseWithTheFollowingAlternativeLoanTypeFields(Reqnroll.Table table)
        {
            LongFinalSpreadFields loantypedata = table.CreateInstance<LongFinalSpreadFields>();
            await _finalSpreadObject.SetAmortisationType(loantypedata.AmortType);
            await _finalSpreadObject.SetCouponFrequency(loantypedata.CouponFreq);
            await _finalSpreadObject.SetDayCount(loantypedata.DayCount);
            await _finalSpreadObject.SetNumberOfAmortisations(loantypedata.NumbOfAmort);
            await _finalSpreadObject.SetAmortisationFrequency(loantypedata.AmortFreq);
        }

        [When(@"I fill the case with the following Alternative Spread fields for Variable loan")]
        public async Task WhenIFillTheCaseWithTheFollowingAlternativeSpreadFieldsForVariableLoan(Reqnroll.Table table)
        {
            LongFinalSpreadFields loantypedata = table.CreateInstance<LongFinalSpreadFields>();
            string spreadCheckBy = (string)_scenarioContext["savedSpreadCheckBy"];
            var lalCaseID = await _finalSpreadObject.GetCaseID();
            await _finalSpreadObject.SetReferenceRate(loantypedata.ReferenceRate);
            await _finalSpreadObject.Set48HoursClause(loantypedata.HoursClause);
            await _finalSpreadObject.SetSpreadH1(loantypedata.SpreadH1);
            await _finalSpreadObject.SetSpreadH1Basis(loantypedata.SpreadH1Basis);
            await _finalSpreadObject.ClickSaveH1Button();
            if (spreadCheckBy == "True")
            {
                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginAccountDeskUser2();

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseID);
                await _basePageObject.OpenCreateLoanRequestLink();
                await _finalSpreadObject.SetSpreadH2(loantypedata.SpreadH2);
                await _finalSpreadObject.SetSpreadH2Basis(loantypedata.SpreadH2Basis);
                await _finalSpreadObject.SetSpreadH2Comment(loantypedata.SpreadH2Comment);
                await _finalSpreadObject.ClickSaveH2Button();
            }

            await _finalSpreadObject.SetFinalSpread(loantypedata.FinalSpread);
            await _finalSpreadObject.SetFinalSpreadBasis(loantypedata.FinalSpreadBasis);
            await _finalSpreadObject.SetFinalSpreadComment(loantypedata.FinalSpreadComment);
            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetDealRemark(loantypedata.DealRemark);
        }

        [When(@"I fill the case with the following Alternative Spread fields for Fixed loan")]
        public async Task WhenIFillTheCaseWithTheFollowingAlternativeSpreadFieldsForFixedLoan(Reqnroll.Table table)
        {
            LongFinalSpreadFields spreaddata = table.CreateInstance<LongFinalSpreadFields>();
            string spreadCheckBy = (string)_scenarioContext["savedSpreadCheckBy"];
            var lalCaseID = await _finalSpreadObject.GetCaseID();
            await _finalSpreadObject.SetSpreadH1(spreaddata.SpreadH1);
            await _finalSpreadObject.SetSpreadH1Comment(spreaddata.SpreadH1Comment);
            await _finalSpreadObject.ClickSaveH1Button();
            if (spreadCheckBy == "True")
            {
                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginAccountDeskUser2();

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseID);
                await _basePageObject.OpenCreateLoanRequestLink();
                await _finalSpreadObject.SetSpreadH2(spreaddata.SpreadH2);
                await _finalSpreadObject.ClickSaveH2Button();
                await _finalSpreadObject.SetSpreadH2Comment(spreaddata.SpreadH2Comment);
            }

            await _finalSpreadObject.SetFinalSpread(spreaddata.FinalSpread);
            await _finalSpreadObject.SetFinalSpreadComment(spreaddata.FinalSpreadComment);
            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetDealRemark(spreaddata.DealRemark);
            await _finalSpreadObject.SetMarketRate(spreaddata.MarketIRate);
            if (spreadCheckBy == "True")
            {
                await _finalSpreadObject.ClickSaveMarketRate2();
            }
            else
            {
                await _finalSpreadObject.ClickSaveMarketRate();
            }
        }

        [Then(@"The case will be succesfully created")]
        public async Task ThenTheCaseWillBeSuccesfullyCreated()
        {
            var lalCaseID = await _finalSpreadObject.GetCaseID();
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            string spreadCheckBy = (string)_scenarioContext["savedSpreadCheckBy"];

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _winLostCorrectObject.ClickWinLostCorrect("Lost");
            await _winLostCorrectObject.ClickWinLostCorrect("Correct");
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();

            await _basePageObject.LogOutSystem();
            if (spreadCheckBy == "True")
            {
                await _loginPageObject.LoginAccountDeskUser1();
            }
            else
            {
                await _loginPageObject.LoginAccountDeskUser2();
            }

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseID);
            await _billOfClosureObject.OpenBillOfClosure();
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            await _basePageObject.OpenCaseOnScreen(lalCaseID);
            var aLLCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            var caseStatusText = await _basePageObject.GetFOCaseStatus();
            caseStatusText.Should().Be("Resolved Completed");

            Console.WriteLine(">>>>>>>>>>---------------------------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully finished flow for: " + lalCaseID + ". Backoffice case: " + aLLCaseNumber + " was created.");
            Console.WriteLine("Case status found was: " + caseStatusText + ".");
            Console.WriteLine(">>>>>>>>>>---------------------------------------------------------------------------<<<<<<<<<<");
        }

        [When(@"I go through all the steps of the back office case")]
        public async Task WhenIGoThroughAllTheStepsOfTheBackOfficeCase()
        {
            var aLLCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            await _loanDataApprovalObject.ClickApproveButtonOpenLoanData();

            if (loanType == "SHORT")
            {
                await _basePageObject.ClickOpenSendConfirmation();
                await _sendConfirmationObject.SetIsTheIbanInOPICS("Yes");
                await Task.Delay(500);
                await _sendConfirmationObject.ClickSubmitSendConfirmation();

                await _surePayCheckObject.SetIsTheIbanInOPICS("Yes");
                await _surePayCheckObject.SetIstheIBANverifiedInSurePay("Yes");
                await _surePayCheckObject.ClickSubmitButtonSurePayCheckPage();

                await _basePageObject.WaitForPageLoadedState();
            }
            else
            {
                await _basePageObject.ClickOpenValidateRequestLink();
                await _validateRequestObject.SetCounterPartyConfirmReceived(true);
                await _validateRequestObject.SetIsTheLoanGuaranteed(true);
                await _validateRequestObject.SetWhatIsTheLoanRequest("Split");
                await _validateRequestObject.SetBillOfClosureConfirmedByPF(true);
                await _validateRequestObject.SetMunicipalDecisionForGuaranty("Yes");

                await _validateRequestObject.ClickSubmitValidationPage();

                // Aanpassing stap surepay na CreateAgreement. pbi 37588
                await _createContractObject.ClickSubmitButtonCreateAgreement();

                await _surePayCheckObject.SetIsTheIbanInOPICS("Yes");
                await _surePayCheckObject.SetIstheIBANverifiedInSurePay("Yes");
                await _surePayCheckObject.ClickSubmitButtonSurePayCheckPage();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice2();
                await Task.Delay(1500);
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, aLLCaseNumber);

                await _agreementReviewObject.OpenAgreementReviewLink();
                await _agreementReviewObject.SideLetterInDossier(true);
                await _agreementReviewObject.AgreementReviewed(true);
                await _agreementReviewObject.ClickSubmitButtonAgreementReview();

                ///////////////  Open - CheckGuarantorDocument page// ALLEEN nodig bij GUARANTOR
                await _checkGuarantorDocumentObject.OpenCheckGuarantorDocumentLink();
                await _checkGuarantorDocumentObject.ClickSubmitOpenCheckGuarantorDocumentPage();

                ///////////////Pending - GuarantorApproval page// ALLEEN nodig bij GUARANTOR
                await _guarantorApprovalObject.OpenGetApprovalGuarantorApprovalLink();

                await _guarantorApprovalObject.ClickApproveButtonGuarantorApprovalPage();

                await _sendAgreementObject.OpenSendAgreementLink();
                await _sendAgreementObject.ClickSubmitSendAgreementPage();
                await _clientApprovalObject.OpenGetApprovalPendingClientApprovalLink();
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

                var textAuthenticityOfDocumentsValidationText = await _checkDossierCompleteObject.AuthenticityOfDocumentsValidation();
                var agreementOfLoanValidationText = await _checkDossierCompleteObject.AgreementOfLoanValidation();
                var signedAgreementSendValidationText = await _checkDossierCompleteObject.SignedAgreementSendValidation();

                textAuthenticityOfDocumentsValidationText.Should().Be("Authenticity of Documents: Please confirm each step before continuing");
                agreementOfLoanValidationText.Should().Be("Agreement of Loan: Please confirm each step before continuing");
                signedAgreementSendValidationText.Should().Be("Signed Agreement Send: Please confirm each step before continuing");

                await _checkDossierCompleteObject.FullSignedAgreementInDossier(true);
                await _checkDossierCompleteObject.AuthenticityofAgreementChecked(true);
                await _checkDossierCompleteObject.FullSignedAgreementSendOutToAllParties(true);
                await _checkDossierCompleteObject.ClickSubmitButtonOpenCheckDossierCompletePage();
                await _basePageObject.WaitForPageLoadedState();
            }
        }

        [Then(@"the backoffice case is succesfully created")]
        public async Task ThenTheBackofficeCaseIsSuccesfullyCreated()
        {
            await Task.Delay(800);
            var aLLCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var caseStatusText = await _basePageObject.GetCaseStatus();
            caseStatusText.Should().Be("Resolved-Completed");

            Console.WriteLine(">>>>>>>>>>-----------------Test Summary-----------------------<<<<<<<<<<");
            Console.WriteLine($"Loantype: {loanType} case");
            Console.WriteLine($"Loan request (LR) case: {lalCaseNumber}");
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------<<<<<<<<<<");
            Console.WriteLine($"Loan administration (LA) case: {aLLCaseNumber}");
            Console.WriteLine($"BO {loanType} case status: {caseStatusText}");
        }

        [Then("the loan update backoffice case is succesfully created")]
        public async Task ThenTheLoanUpdateBackofficeCaseIsSuccesfullyCreated()
        {
            await Task.Delay(800);
            var aLUCaseNumber = (string)_scenarioContext["savedALUCaseNumber"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var caseStatusText = await _basePageObject.GetCaseStatus();
            caseStatusText.Should().Be("Resolved-Completed");

            Console.WriteLine(">>>>>>>>>>-----------------Test Summary-----------------------<<<<<<<<<<");
            Console.WriteLine($"Loantype: {loanType} case");
            Console.WriteLine($"Request loan update (RLU) case: {lalCaseNumber}");
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------<<<<<<<<<<");
            Console.WriteLine($"Administration loan update (ALU) case: {aLUCaseNumber}");
            Console.WriteLine($"BO {loanType} case status: {caseStatusText}");
        }
    }
}