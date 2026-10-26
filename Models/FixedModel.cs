using Gherkin;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.PageObjects;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.Models
{
    public interface IFixedModel
    {
        Task FixedCaseSetAlternativeFields(string dayCount, bool swap, string amortType, string? numbOfAmort = "", string? amortFreq = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null, string? offerRef = "");

        Task FixedCaseSetDateFields(string startDate, string maturityDate, string couponFreq, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task FixedCaseSetGuarantor(string guarantor1);

        Task FixedCaseSetMultipleFinalFourEyes(string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentFinal = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task FixedCaseSetMultipleH1FourEyes(string h1, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task FixedCaseSetMultipleH2FourEyes(string h2, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task FixedCaseSetMultipleSpreadMismatches(string h1, string h2, string loanType, string lalCaseNumber, string? overWriteSpreadMismatchComment = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null, int? indexOfSpreadMismatch = null);

        Task FixedCaseSetSpreadFieldsFourEyes(string h1, string h2, string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task FixedCaseSetSpreadFieldsTwoEyes(string h1, string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "");

        Task LoanUpdateFixedCaseSetAlternativeFields(bool swap, string amortType, string? numbOfAmort = "", string? amortFreq = "", int? alternativeNumbIncase = null, string? offerRef = "");

        Task LoanUpdateFixedCaseSetDateFields(string startDate, string maturityDate, string couponFreq, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "", int? alternativeNumbIncase = null);

        Task LoanUpdateFixedCaseSetSpreadFieldsTwoEyes(string h1, string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "");
    }

    public class FixedModel : IFixedModel
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

        private readonly IFixedDataTransforms _fixedDataTransforms;

        private readonly IProcessClientResponseObject _processClientResponseObject;

        private readonly ILegalReviewObject _legalReviewObject;

        private readonly ICheckDossierCompleteObject _checkDossierCompleteObject;

        private readonly ILocator _includeInMailBoxLocator;

        private readonly IPlaywrightDriver _playwrightDriver;

        private readonly ISendConfirmationObject _sendConfirmationObject;

        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;

        public FixedModel(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IClientApprovalObject clientApprovalObject,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            IFixedDataTransforms fixedDataTransforms)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _clientApprovalObject = clientApprovalObject;
            _fixedDataTransforms = fixedDataTransforms;

            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
        }

        public async Task FixedCaseSetDateFields(string startDate, string maturityDate, string couponFreq, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetCouponFrequency(couponFreq, offerCase, altOfferCase);
            await _finalSpreadObject.SetStartDate(startDate, offerCase, altOfferCase);
            await _finalSpreadObject.SetMaturityDate(maturityDate, offerCase, altOfferCase);

            if (firstCouponDate != null && firstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(firstCouponDate, offerCase, altOfferCase);
            }

            if (lastCouponDate != null && lastCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetLastCouponDate(lastCouponDate, offerCase, altOfferCase);
            }

            if (interestRRDate != null && interestRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(interestRRDate, offerCase, altOfferCase);
            }

            if (couponFreq == "Weekly")
            {
                if (couponDayOption == null)
                {
                    var startDateTime = default(DateTime);
                    var maturityDateTime = default(DateTime);
                    DateTime.TryParse(startDate, out startDateTime);
                    DateTime.TryParse(maturityDate, out maturityDateTime);
                    DayOfWeek dayStart = startDateTime.DayOfWeek;
                    DayOfWeek dayMaturity = maturityDateTime.DayOfWeek;

                    if (dayStart == dayMaturity)
                    {
                        var dayOfWeek = dayStart.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, offerCase, altOfferCase);
                    }
                    else if (dayStart != dayMaturity)
                    {
                        var dayOfWeek = dayMaturity.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, offerCase, altOfferCase);

                        if (firstCouponDate == null && firstCouponDate == string.Empty)
                        {
                            await _finalSpreadObject.SetFirstCouponDate(startDate, offerCase, altOfferCase);
                        }
                    }
                }
                else
                {
                    await _finalSpreadObject.SetCouponDayOptionSet(couponDayOption, offerCase, altOfferCase);
                }
            }
            else
            {
                await _finalSpreadObject.SetCouponDay(couponDay, offerCase, altOfferCase);
            }
        }

        public async Task LoanUpdateFixedCaseSetDateFields(string startDate, string maturityDate, string couponFreq, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "", int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            await _finalSpreadObject.SetStartDate(startDate, altOfferCase);
            await _finalSpreadObject.SetMaturityDate(maturityDate, altOfferCase);

            if (firstCouponDate != null && firstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(firstCouponDate, altOfferCase);
            }

            if (lastCouponDate != null && lastCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetLastCouponDate(lastCouponDate, altOfferCase);
            }

            if (interestRRDate != null && interestRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(interestRRDate, altOfferCase);
            }

            if (couponFreq == "Weekly")
            {
                if (couponDayOption == null)
                {
                    var startDateTime = default(DateTime);
                    var maturityDateTime = default(DateTime);
                    DateTime.TryParse(startDate, out startDateTime);
                    DateTime.TryParse(maturityDate, out maturityDateTime);
                    DayOfWeek dayStart = startDateTime.DayOfWeek;
                    DayOfWeek dayMaturity = maturityDateTime.DayOfWeek;

                    if (interestRRDate != null && interestRRDate != string.Empty)
                    {
                        var interestRRDateTime = default(DateTime);
                        DateTime.TryParse(interestRRDate, out interestRRDateTime);
                        DayOfWeek dayInterestRRDate = interestRRDateTime.DayOfWeek;

                        if (dayStart == dayInterestRRDate)
                        {
                            var dayOfWeek = dayStart.ToString();
                            Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);

                            await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, altOfferCase);
                        }
                        else if (dayStart != dayInterestRRDate)
                        {
                            var dayOfWeek = dayInterestRRDate.ToString();
                            Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);

                            await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, altOfferCase);

                            if (firstCouponDate == null && firstCouponDate == string.Empty)
                            {
                                await _finalSpreadObject.SetFirstCouponDate(startDate, altOfferCase);
                            }
                        }
                    }
                    else
                    {
                        if (dayStart == dayMaturity)
                        {
                            var dayOfWeek = dayStart.ToString();
                            Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);

                            await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, altOfferCase);
                        }
                        else if (dayStart != dayMaturity)
                        {
                            var dayOfWeek = dayMaturity.ToString();
                            Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);

                            await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime, altOfferCase);

                            if (firstCouponDate == null && firstCouponDate == string.Empty)
                            {
                                await _finalSpreadObject.SetFirstCouponDate(startDate, altOfferCase);
                            }
                        }
                    }
                }
                else
                {
                    await _finalSpreadObject.SetCouponDayOptionSet(couponDayOption, altOfferCase);
                }
            }
            else
            {
                await _finalSpreadObject.SetCouponDay(couponDay, altOfferCase);
            }
        }

        public async Task FixedCaseSetAlternativeFields(string dayCount, bool swap, string amortType, string? numbOfAmort = "", string? amortFreq = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null, string? offerRef = "")
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetAmortisationType(amortType, offerCase, altOfferCase);
            if (swap == true)
            {
                await _finalSpreadObject.SetSwap(true, offerCase, altOfferCase);
            }
            else
            {
                await _finalSpreadObject.SetSwap(false, offerCase, altOfferCase);
            }

            await _finalSpreadObject.SetDayCount(dayCount, offerCase, altOfferCase);

            if (amortType != "Bullet")
            {
                if (numbOfAmort != null && numbOfAmort != string.Empty && amortFreq != null && amortFreq != string.Empty)
                {
                    await _finalSpreadObject.SetNumberOfAmortisations(numbOfAmort, offerCase, altOfferCase);
                    await _finalSpreadObject.SetAmortisationFrequency(amortFreq, offerCase, altOfferCase);
                }
                else
                {
                    throw new Exception("NumOfAmort or amortFreq has not been properly " +
                        "set in method! Origin call from: FixedModel.FixedCaseSetAlternativeFields()");
                }
            }
        }

        public async Task LoanUpdateFixedCaseSetAlternativeFields(bool swap, string amortType, string? numbOfAmort = "", string? amortFreq = "", int? alternativeNumbIncase = null, string? offerRef = "")
        {
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetSwap(swap, altOfferCase);
            if (amortType != "Bullet")
            {
                if (numbOfAmort != null && numbOfAmort != string.Empty && amortFreq != null && amortFreq != string.Empty)
                {
                }
                else
                {
                    throw new Exception("NumOfAmort or amortFreq has not been properly " +
                        "set in method! Origin call from: FixedModel.FixedCaseSetAlternativeFields()");
                }
            }
        }

        public async Task FixedCaseSetSpreadFieldsFourEyes(string h1, string h2, string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            double expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToDouble(finalSpread, marketRate);
            _scenarioContext["savedExpectedCustomerRate"] = expectedCustomerRate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");
            await _finalSpreadObject.SetSpreadH1(h1, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetSpreadH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetSpreadH1Comment(overWriteCommentH1, offerCase, altOfferCase);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();
            await _finalSpreadObject.ClickSaveOffer();

            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);

            await _finalSpreadObject.SetSpreadH2(h2, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                {
                    await _finalSpreadObject.SetSpreadH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetSpreadH2Comment(overWriteCommentH2, offerCase, altOfferCase);
                }
            }

            if (h1 != h2)
            {
                await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
                await _finalSpreadObject.OverwriteSpreadMisMatch();
                if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                {
                    await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetSpreadMisMatchComment(overWriteSpreadMismatchComment);
                }

                await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
            }
            else if (h1 == h2)
            {
                await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
            }

            await _finalSpreadObject.SetFinalSpread(finalSpread, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalSpreadComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalSpreadComment(overWriteCommentFinal, offerCase, altOfferCase);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton(offerCase, altOfferCase);
            await _finalSpreadObject.SetMarketRate(marketRate, offerCase, altOfferCase);
            await _finalSpreadObject.ClickSaveMarketRate(offerCase, altOfferCase);
        }

        public async Task FixedCaseSetSpreadFieldsTwoEyes(
            string h1,
            string finalSpread,
            string marketRate,
            string loanType,
            string lalCaseNumber,
            bool setSpreadMessages,
            string? overWriteCommentH1 = "",
            string? overWriteCommentFinal = "")
        {
            double expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToDouble(finalSpread, marketRate);
            _scenarioContext["savedExpectedCustomerRate"] = expectedCustomerRate;

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _finalSpreadObject.SetSpreadH1(h1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetSpreadH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetSpreadH1Comment(overWriteCommentH1);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalSpread(finalSpread);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalSpreadComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalSpreadComment(overWriteCommentFinal);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetMarketRate(marketRate);
            await _finalSpreadObject.ClickSaveMarketRate();
        }

        public async Task LoanUpdateFixedCaseSetSpreadFieldsTwoEyes(string h1, string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "")
        {
            double expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToDouble(finalSpread, marketRate);
            _scenarioContext["savedExpectedCustomerRate"] = expectedCustomerRate;

            await _loanUpdateFinalSpreadObject.SpreadCheckBy(false);
            await _loanUpdateFinalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _loanUpdateFinalSpreadObject.SetSpreadH1(h1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _loanUpdateFinalSpreadObject.SetSpreadH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _loanUpdateFinalSpreadObject.SetSpreadH1Comment(overWriteCommentH1);
                }
            }

            await _loanUpdateFinalSpreadObject.ClickSaveH1Button();

            await _loanUpdateFinalSpreadObject.SetFinalSpread(finalSpread);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _loanUpdateFinalSpreadObject.SetFinalSpreadComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _loanUpdateFinalSpreadObject.SetFinalSpreadComment(overWriteCommentFinal);
                }
            }

            await _loanUpdateFinalSpreadObject.ClickSaveFinalSpreadButton();
            await _loanUpdateFinalSpreadObject.SetMarketRate(marketRate);
            await _loanUpdateFinalSpreadObject.ClickSaveMarketRate();
        }

        public async Task FixedCaseSetGuarantor(string guarantor1)
        {
            if (await _finalSpreadObject.GuarantorCheckboxStatus() == true)
            {
                await _finalSpreadObject.UncheckGuarantorBox();
            }

            if (guarantor1 == "Gemeente Culemborg")
            {
                if (await _finalSpreadObject.GuarantorCheckboxStatus() == false)
                {
                    await _finalSpreadObject.CheckGuarantorBox();
                    await Task.Delay(500);
                    await _finalSpreadObject.ClickAddGuarantorButton();
                }

                await _finalSpreadObject.SetGuarantorField(guarantor1, 1);
            }
        }

        public async Task FixedCaseSetMultipleH1FourEyes(string h1, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetSpreadH1(h1, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetSpreadH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetSpreadH1Comment(overWriteCommentH1, offerCase, altOfferCase);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button(offerCase, altOfferCase);
        }

        public async Task FixedCaseSetMultipleH2FourEyes(string h2, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetSpreadH2(h2, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                {
                    await _finalSpreadObject.SetSpreadH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetSpreadH2Comment(overWriteCommentH2, offerCase, altOfferCase);
                }
            }
        }

        public async Task FixedCaseSetMultipleSpreadMismatches(string h1, string h2, string loanType, string lalCaseNumber, string? overWriteSpreadMismatchComment = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null, int? indexOfSpreadMismatch = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;
            int spreadMismatchIndex = indexOfSpreadMismatch ?? 0;

            if (h1 != h2)
            {
                await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
                await _finalSpreadObject.OverwriteSpreadMisMatch(spreadMismatchIndex);
                if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                {
                    await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }

                await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
            }
            else
            {
                if (h1 == h2)
                {
                    await _finalSpreadObject.ClickSaveH2Button(offerCase, altOfferCase);
                }
            }
        }

        public async Task FixedCaseSetMultipleFinalFourEyes(string finalSpread, string marketRate, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentFinal = "", int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            int altOfferCase = alternativeNumbIncase ?? 1;

            await _finalSpreadObject.SetFinalSpread(finalSpread, offerCase, altOfferCase);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalSpreadComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ", offerCase, altOfferCase);
                }
                else
                {
                    await _finalSpreadObject.SetFinalSpreadComment(overWriteCommentFinal, offerCase, altOfferCase);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton(offerCase, altOfferCase);
            await _finalSpreadObject.SetMarketRate(marketRate, offerCase, altOfferCase);
            await _finalSpreadObject.ClickSaveMarketRate(offerCase, altOfferCase);
        }
    }
}