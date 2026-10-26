using Reqnroll;
using System;
using System.Reflection;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class CreateFrontOfficeCasesStepDefinitions
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
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly IShortModel _shortModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IFixedModel _fixedModel;
        private readonly IVariableModel _variableModel;
        private readonly ISharedModel _sharedModel;

        public CreateFrontOfficeCasesStepDefinitions(IPlaywrightDriver playwrightDriver, ISharedDataIndividuals sharedDataIndividuals, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, IShortModel shortModel, IFixedModel fixedModel, IVariableModel variableModel, ISharedModel sharedModel, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject, ISendConfirmationObject sendConfirmationObject, ISharedDataTransforms sharedDataTransforms)
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
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _sharedModel = sharedModel;
            _checkDossierCompleteObject = checkDossierCompleteObject;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataIndividuals = sharedDataIndividuals;
            _sendConfirmationObject = sendConfirmationObject;
            _shortModel = shortModel;
            _sharedDataTransforms = sharedDataTransforms;
        }

        [Given("I create a FIXED case with the following OfferData")]
        public async Task GivenICreateAFIXEDCaseWithTheFollowingOfferData(DataTable offerData)
        {
            FixedLoanCaseOfferFields fixedOfferData = offerData.CreateInstance<FixedLoanCaseOfferFields>();
            _scenarioContext["savedFixedOfferData"] = fixedOfferData;
            _scenarioContext["savedTC"] = fixedOfferData.TC;
        }

        [Given("I use the following FIXED alternative data")]
        public async Task GivenIUseTheFollowingFIXEDAlternativeData(DataTable alternativeData)
        {
            FixedLoanAlternativeFields fixedAlternativeData = alternativeData.CreateInstance<FixedLoanAlternativeFields>();
            _scenarioContext["savedFixedAlternativeData"] = fixedAlternativeData;
        }

        [Given("I use the follow FIXED spread data")]
        public async Task GivenIUseTheFollowFIXEDSpreadData(DataTable spreadData)
        {
            FixedLoanAlternativeFields fixedSpreadData = spreadData.CreateInstance<FixedLoanAlternativeFields>();
            _scenarioContext["savedFixedSpreadData"] = fixedSpreadData;
        }

        [When("I fill in the FIXED create offer with all the data")]
        public async Task WhenIFillInTheFIXEDCreateOfferWithAllTheData()
        {
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedFixedOfferData"];
            var fixedAlternativeData = (FixedLoanAlternativeFields)_scenarioContext["savedFixedAlternativeData"];
            var fixedSpreadData = (FixedLoanAlternativeFields)_scenarioContext["savedFixedSpreadData"];

            // await _basePageObject.SelectLoanRequest(LoanRequest.Fixed);
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notionalWithoutDouble;

            if (fixedOfferData.CreditLimit != null && fixedOfferData.CreditLimit != string.Empty)
            {
                await _finalSpreadObject.SetCreditLimit(fixedOfferData.CreditLimit);
                await _finalSpreadObject.SetCounterpartyUpdateComment("This is a comment to overwrite credit limit or outstanding principle amount. Text inputted by Playwright");
            }

            // Setting guarantor and emails if present
            if (fixedOfferData.Guarantor1 != null && fixedOfferData.Guarantor1 != string.Empty)
            {
                await _sharedModel.SetGuarantorField(fixedOfferData.Guarantor1);
                _scenarioContext["savedGuarantor1"] = fixedOfferData.Guarantor1;
            }
            else
            {
                _scenarioContext["savedGuarantor1"] = string.Empty;
            }

            // Setting Offer data
            if (fixedOfferData.CDDReviewDate != null && fixedOfferData.CDDReviewDate != string.Empty && fixedOfferData.DealMoment != null && fixedOfferData.DealMoment != string.Empty)
            {
                string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
                await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, fixedOfferData.Notional);
            }
            else
            {
                await _sharedModel.CaseSetOfferFields(fixedOfferData.CDDReviewDate, fixedOfferData.DealMoment, fixedOfferData.Notional);
            }

            if (fixedOfferData.AdditEmail1 != null && fixedOfferData.AdditEmail1 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(fixedOfferData.AdditEmail1);
            }

            if (fixedOfferData.AdditEmail2 != null && fixedOfferData.AdditEmail2 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(fixedOfferData.AdditEmail1, fixedOfferData.AdditEmail2, 2);
            }

            // Set LONG dates
            if (fixedAlternativeData.StartDate == "today")
            {
                if (fixedAlternativeData.MaturityDate != null && fixedAlternativeData.MaturityDate != string.Empty)
                {
                    if (fixedAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(fixedAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, fixedAlternativeData.MaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, fixedAlternativeData.MaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDay: fixedAlternativeData.CouponDay);
                    }
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 40, false);
                    if (fixedAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(fixedAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDay: fixedAlternativeData.CouponDay);
                    }
                }
            }
            else
            {
                if (fixedAlternativeData.MaturityDate != null && fixedAlternativeData.MaturityDate != string.Empty)
                {
                    if (fixedAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(fixedAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _fixedModel.FixedCaseSetDateFields(fixedAlternativeData.StartDate, fixedAlternativeData.MaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _fixedModel.FixedCaseSetDateFields(fixedAlternativeData.StartDate, fixedAlternativeData.MaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDay: fixedAlternativeData.CouponDay);
                    }
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 40, false);
                    if (fixedAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(fixedAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _fixedModel.FixedCaseSetDateFields(fixedAlternativeData.StartDate, formattedMaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _fixedModel.FixedCaseSetDateFields(fixedAlternativeData.StartDate, formattedMaturityDate, couponFreq: fixedAlternativeData.CouponFreq, fixedAlternativeData.FirstCouponDate, fixedAlternativeData.LastCouponDate, fixedAlternativeData.InterestRRDate, couponDay: fixedAlternativeData.CouponDay);
                    }
                }
            }

            if (fixedAlternativeData.AmortType != "Bullet")
            {
                await _fixedModel.FixedCaseSetAlternativeFields(fixedAlternativeData.DayCount, fixedAlternativeData.Swap, fixedAlternativeData.AmortType, fixedAlternativeData.NumbOfAmort, fixedAlternativeData.AmortFreq, offerRef: fixedOfferData.OfferReference);
            }
            else
            {
                await _fixedModel.FixedCaseSetAlternativeFields(fixedAlternativeData.DayCount, fixedAlternativeData.Swap, fixedAlternativeData.AmortType, offerRef: fixedOfferData.OfferReference);
            }

            if (fixedOfferData.SpreadCheckBy == true)
            {
                await _fixedModel.FixedCaseSetSpreadFieldsFourEyes(fixedSpreadData.SpreadH1, fixedSpreadData.SpreadH2, fixedSpreadData.FinalSpread, fixedSpreadData.MarketIRate, "FIXED", lalCaseNumber, setSpreadMessages: true, fixedSpreadData.OverwriteSpreadH1Comment, fixedSpreadData.OverwriteSpreadH2Comment, fixedSpreadData.OverwriteFinalSpreadComment, fixedSpreadData.OverwriteSpreadMismatchComment);
            }
            else if (fixedOfferData.SpreadCheckBy == false)
            {
                await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes(fixedSpreadData.SpreadH1, fixedSpreadData.FinalSpread, fixedSpreadData.MarketIRate, "FIXED", lalCaseNumber, setSpreadMessages: true, fixedSpreadData.OverwriteSpreadH1Comment, fixedSpreadData.OverwriteFinalSpreadComment);
            }

            if (fixedOfferData.SpreadCheckRemarks != null && fixedOfferData.SpreadCheckRemarks != string.Empty)
            {
                await _sharedModel.OverwriteSpreadCheckRemark(fixedOfferData.SpreadCheckRemarks);
            }

            if (fixedAlternativeData.DealRemark != null && fixedAlternativeData.DealRemark != string.Empty)
            {
                await _sharedModel.OverwriteDealRemark(fixedAlternativeData.DealRemark);
            }
        }

        [Given("I create a VARIABLE case with the following OfferData")]
        public async Task GivenICreateAVARIABLECaseWithTheFollowingOfferData(DataTable offerData)
        {
            VariableLoanCaseOfferFields variableOfferData = offerData.CreateInstance<VariableLoanCaseOfferFields>();
            _scenarioContext["savedVariableOfferData"] = variableOfferData;
            _scenarioContext["savedTC"] = variableOfferData.TC;
        }

        [Given("I use the following VARIABLE alternative data")]
        public async Task GivenIUseTheFollowingVARIABLEAlternativeData(DataTable alternativeData)
        {
            VariableLoanAlternativeFields variableAlternativeData = alternativeData.CreateInstance<VariableLoanAlternativeFields>();
            _scenarioContext["savedVariableAlternativeData"] = variableAlternativeData;
        }

        [Given("I use the following VARIABLE spread data")]
        public async Task GivenIUseTheFollowingVARIABLESpreadData(DataTable spreadData)
        {
            VariableLoanAlternativeFields variableSpreadData = spreadData.CreateInstance<VariableLoanAlternativeFields>();
            _scenarioContext["savedVariableSpreadData"] = variableSpreadData;
        }

        [When("I fill in the VARIABLE create offer with all the data")]
        public async Task WhenIFillInTheVARIABLECreateOfferWithAllTheData()
        {
            var variableOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedVariableOfferData"];
            var variableAlternativeData = (VariableLoanAlternativeFields)_scenarioContext["savedVariableAlternativeData"];
            var variableSpreadData = (VariableLoanAlternativeFields)_scenarioContext["savedVariableSpreadData"];

            // await _basePageObject.SelectLoanRequest(LoanRequest.Variable);
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notionalWithoutDouble;

            if (variableOfferData.CreditLimit != null && variableOfferData.CreditLimit != string.Empty)
            {
                await _finalSpreadObject.SetCreditLimit(variableOfferData.CreditLimit);
                await _finalSpreadObject.SetCounterpartyUpdateComment("This is a comment to overwrite credit limit or outstanding principle amount. Text inputted by Playwright");
            }

            // Setting guarantor and emails if present
            if (variableOfferData.Guarantor1 != null && variableOfferData.Guarantor1 != string.Empty)
            {
                await _sharedModel.SetGuarantorField(variableOfferData.Guarantor1);
                _scenarioContext["savedGuarantor1"] = variableOfferData.Guarantor1;
            }
            else
            {
                _scenarioContext["savedGuarantor1"] = string.Empty;
            }

            // Setting Offer data
            if (variableOfferData.CDDReviewDate != null && variableOfferData.CDDReviewDate != string.Empty && variableOfferData.DealMoment != null && variableOfferData.DealMoment != string.Empty)
            {
                string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
                await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, variableOfferData.Notional);
            }
            else
            {
                await _sharedModel.CaseSetOfferFields(variableOfferData.CDDReviewDate, variableOfferData.DealMoment, variableOfferData.Notional);
            }

            if (variableOfferData.AdditEmail1 != null && variableOfferData.AdditEmail1 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(variableOfferData.AdditEmail1);
            }

            if (variableOfferData.AdditEmail2 != null && variableOfferData.AdditEmail2 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(variableOfferData.AdditEmail1, variableOfferData.AdditEmail2, 2);
            }

            // Setting Fixed or Variable notional
            if (variableOfferData.SetNotionalTypeToFixed == "false")
            {
                await _finalSpreadObject.SetMinimalWithdrawalPercent(variableOfferData.MinimalWithdrawal);
                await _finalSpreadObject.SetCommitmentFee(variableOfferData.CommitmentFee);
            }

            // Set LONG dates
            if (variableAlternativeData.StartDate == "today")
            {
                if (variableAlternativeData.MaturityDate != null && variableAlternativeData.MaturityDate != string.Empty)
                {
                    if (variableAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(variableAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _variableModel.VariableCaseSetDateFields(currentFormattedDate, variableAlternativeData.MaturityDate, variableAlternativeData.ReferenceRate, bool.Parse(variableAlternativeData.HoursClause), variableAlternativeData.FirstCouponDate, variableAlternativeData.LastCouponDate, variableAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _variableModel.VariableCaseSetDateFields(currentFormattedDate, variableAlternativeData.MaturityDate, variableAlternativeData.ReferenceRate, firstCouponDate: variableAlternativeData.FirstCouponDate, lastCouponDate: variableAlternativeData.LastCouponDate, interestRRDate: variableAlternativeData.InterestRRDate, couponDay: variableAlternativeData.CouponDay);
                    }
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 20, false);
                    if (variableAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(variableAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _variableModel.VariableCaseSetDateFields(currentFormattedDate, formattedMaturityDate, variableAlternativeData.ReferenceRate, bool.Parse(variableAlternativeData.HoursClause), variableAlternativeData.FirstCouponDate, variableAlternativeData.LastCouponDate, variableAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _variableModel.VariableCaseSetDateFields(currentFormattedDate, formattedMaturityDate, variableAlternativeData.ReferenceRate, firstCouponDate: variableAlternativeData.FirstCouponDate, lastCouponDate: variableAlternativeData.LastCouponDate, interestRRDate: variableAlternativeData.InterestRRDate, couponDay: variableAlternativeData.CouponDay);
                    }
                }
            }
            else
            {
                if (variableAlternativeData.MaturityDate != null && variableAlternativeData.MaturityDate != string.Empty)
                {
                    if (variableAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(variableAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _variableModel.VariableCaseSetDateFields(variableAlternativeData.StartDate, variableAlternativeData.MaturityDate, variableAlternativeData.ReferenceRate, bool.Parse(variableAlternativeData.HoursClause), variableAlternativeData.FirstCouponDate, variableAlternativeData.LastCouponDate, variableAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _variableModel.VariableCaseSetDateFields(variableAlternativeData.StartDate, variableAlternativeData.MaturityDate, variableAlternativeData.ReferenceRate, firstCouponDate: variableAlternativeData.FirstCouponDate, lastCouponDate: variableAlternativeData.LastCouponDate, interestRRDate: variableAlternativeData.InterestRRDate, couponDay: variableAlternativeData.CouponDay);
                    }
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 40, false);
                    if (variableAlternativeData.CouponDay.Length > 2)
                    {
                        Enum.TryParse(variableAlternativeData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                        await _variableModel.VariableCaseSetDateFields(variableAlternativeData.StartDate, formattedMaturityDate, variableAlternativeData.ReferenceRate, bool.Parse(variableAlternativeData.HoursClause), variableAlternativeData.FirstCouponDate, variableAlternativeData.LastCouponDate, variableAlternativeData.InterestRRDate, couponDayOption: couponDayOptionFromDateTime);
                    }
                    else
                    {
                        await _variableModel.VariableCaseSetDateFields(variableAlternativeData.StartDate, formattedMaturityDate, variableAlternativeData.ReferenceRate, firstCouponDate: variableAlternativeData.FirstCouponDate, lastCouponDate: variableAlternativeData.LastCouponDate, interestRRDate: variableAlternativeData.InterestRRDate, couponDay: variableAlternativeData.CouponDay);
                    }
                }
            }

            // Set SPREAD data
            if (variableOfferData.SpreadCheckBy == true)
            {
                await _variableModel.VariableSetSpreadFieldsFourEyes(variableAlternativeData.ReferenceRate, "VARIABLE", lalCaseNumber, variableSpreadData.SpreadH1, variableSpreadData.SpreadH2, variableSpreadData.FinalSpread, setSpreadMessages: true, variableSpreadData.SpreadH1Basis, variableSpreadData.SpreadH2Basis, variableSpreadData.FinalSpreadBasis, variableSpreadData.OverwriteSpreadH1Comment, variableSpreadData.OverwriteSpreadH2Comment, variableSpreadData.OverwriteFinalSpreadComment, variableSpreadData.OverwriteSpreadMismatchComment);
            }
            else if (variableOfferData.SpreadCheckBy == false)
            {
                await _variableModel.VariableSetSpreadFieldsTwoEyes(variableAlternativeData.ReferenceRate, "VARIABLE", lalCaseNumber, variableSpreadData.SpreadH1, variableSpreadData.FinalSpread, setSpreadMessages: true, variableSpreadData.SpreadH1Basis, variableSpreadData.FinalSpreadBasis, variableSpreadData.OverwriteSpreadH1Comment, variableSpreadData.OverwriteSpreadH2Comment, variableSpreadData.OverwriteFinalSpreadComment, variableSpreadData.OverwriteSpreadMismatchComment);
            }
        }

        [Given("I create a SHORT case with the following OfferData")]
        public async Task GivenICreateASHORTCaseWithTheFollowingOfferData(DataTable offerData)
        {
            ShortLoanCaseOfferFields shortOfferData = offerData.CreateInstance<ShortLoanCaseOfferFields>();
            _scenarioContext["savedShortOfferData"] = shortOfferData;
            _scenarioContext["savedTC"] = shortOfferData.TC;
        }

        [Given("I use the SHORT VARIABLE alternative data")]
        public async Task GivenIUseTheSHORTVARIABLEAlternativeData(DataTable alternativeData)
        {
            ShortLoanAlternativeFields shortAlternativeData = alternativeData.CreateInstance<ShortLoanAlternativeFields>();
            _scenarioContext["savedShortAlternativeData"] = shortAlternativeData;
        }

        [Given("I use the following SHORT spread data")]
        public async Task GivenIUseTheFollowingSHORTSpreadData(DataTable spreadData)
        {
            ShortLoanAlternativeFields shortSpreadData = spreadData.CreateInstance<ShortLoanAlternativeFields>();
            _scenarioContext["savedShortSpreadData"] = shortSpreadData;
        }

        [When("I fill in the SHORT create offer with all the data")]
        public async Task WhenIFillInTheSHORTCreateOfferWithAllTheData()
        {
            var shortOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedShortOfferData"];
            var shortAlternativeData = (ShortLoanAlternativeFields)_scenarioContext["savedShortAlternativeData"];
            var shortSpreadData = (ShortLoanAlternativeFields)_scenarioContext["savedShortSpreadData"];

            // await _basePageObject.SelectLoanRequest(LoanRequest.Short);
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notionalWithoutDouble;

            if (shortOfferData.CreditLimit != null && shortOfferData.CreditLimit != string.Empty)
            {
                await _finalSpreadObject.SetCreditLimit(shortOfferData.CreditLimit);
                await _finalSpreadObject.SetCounterpartyUpdateComment("This is a comment to overwrite credit limit or outstanding principle amount. Text inputted by Playwright");
            }

            // Setting guarantor and emails if present
            if (shortOfferData.Guarantor1 != null && shortOfferData.Guarantor1 != string.Empty)
            {
                await _sharedModel.SetGuarantorField(shortOfferData.Guarantor1);
                _scenarioContext["savedGuarantor1"] = shortOfferData.Guarantor1;
            }
            else
            {
                _scenarioContext["savedGuarantor1"] = string.Empty;
            }

            // Setting Offer data
            if (shortOfferData.CDDReviewDate != null && shortOfferData.CDDReviewDate != string.Empty && shortOfferData.DealMoment != null && shortOfferData.DealMoment != string.Empty)
            {
                string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
                await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, shortOfferData.Notional);
            }
            else
            {
                await _sharedModel.CaseSetOfferFields(shortOfferData.CDDReviewDate, shortOfferData.DealMoment, shortOfferData.Notional);
            }

            if (shortOfferData.AdditEmail1 != null && shortOfferData.AdditEmail1 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(shortOfferData.AdditEmail1);
            }

            if (shortOfferData.AdditEmail2 != null && shortOfferData.AdditEmail2 != string.Empty)
            {
                await _sharedModel.SetExtraEmailFields(shortOfferData.AdditEmail1, shortOfferData.AdditEmail2, 2);
            }

            // Set LONG dates
            if (shortAlternativeData.StartDate == "today")
            {
                if (shortAlternativeData.MaturityDate != null && shortAlternativeData.MaturityDate != string.Empty)
                {
                    await _shortModel.ShortCaseSetDateFields(currentFormattedDate, shortAlternativeData.MaturityDate);
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 1, false);
                    await _shortModel.ShortCaseSetDateFields(currentFormattedDate, formattedMaturityDate);
                }
            }
            else
            {
                if (shortAlternativeData.MaturityDate != null && shortAlternativeData.MaturityDate != string.Empty)
                {
                    await _shortModel.ShortCaseSetDateFields(shortAlternativeData.StartDate, shortAlternativeData.MaturityDate);
                }
                else
                {
                    var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 1, false);
                    await _shortModel.ShortCaseSetDateFields(shortAlternativeData.StartDate, formattedMaturityDate);
                }
            }

            if (shortOfferData.SpreadCheckBy == true)
            {
                await _shortModel.ShortCaseSetSpreadFieldsFourEyes(shortSpreadData.PriceH1, shortSpreadData.PriceH2, shortSpreadData.PriceFinal, shortSpreadData.Margin, "SHORT", lalCaseNumber, setSpreadMessages: true, shortSpreadData.OverwritePriceH1Comment, shortSpreadData.OverwritePriceH2Comment, shortSpreadData.OverwriteFinalPriceComment, shortSpreadData.OverwriteMarginComment, shortSpreadData.OverwriteSpreadMismatchComment);
            }
            else if (shortOfferData.SpreadCheckBy == false)
            {
                await _shortModel.ShortCaseSetSpreadFieldsTwoEyes(shortSpreadData.PriceH1, shortSpreadData.PriceFinal, shortSpreadData.Margin, "SHORT", lalCaseNumber, setSpreadMessages: true, shortSpreadData.OverwritePriceH1Comment, shortSpreadData.OverwriteFinalPriceComment, shortAlternativeData.OverwriteMarginComment);
            }
        }
    }
}