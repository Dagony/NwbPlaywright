using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.Models
{
    public interface IVariableModel
    {
        Task VariableSetSpreadFieldsFourEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string h2, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? h2Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "");

        Task VariableSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "");

        Task VariableCaseSetDateFields(string startDate, string maturityDate, string referenceRate, bool? hoursClause = null, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "");

        Task LoanUpdateVariableCaseSetDateFields(string startDate, string maturityDate, string referenceRate, bool? hoursClause = null, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "");

        Task LoanUpdateVariableSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "");

        Task LoanUpdateBaseRateSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "");
    }

    public class VariableModel : IVariableModel
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

        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;

        private readonly IVariableDataTransforms _variableDataTransforms;

        private readonly IPlaywrightDriver _playwrightDriver;

        private readonly ISharedDataIndividuals _sharedDataIndividuals;

        private readonly ISendConfirmationObject _sendConfirmationObject;

        private readonly ISharedDataIndividuals _sharedDataIndividual;

        public VariableModel(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IClientApprovalObject clientApprovalObject,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpread,
            IVariableDataTransforms variableDataTransforms)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _clientApprovalObject = clientApprovalObject;

            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpread;
            _variableDataTransforms = variableDataTransforms;
        }

        public async Task VariableCaseSetDateFields(string startDate, string maturityDate, string referenceRate, bool? hoursClause = null, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "")
        {
            await _finalSpreadObject.SetReferenceRate(referenceRate);
            await _finalSpreadObject.SetStartDate(startDate);
            await _basePageObject.PressTab();
            await Task.Delay(2000); // Hope to be able to remove quickly
            await _finalSpreadObject.SetMaturityDate(maturityDate);

            if (firstCouponDate != null && firstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(firstCouponDate);
            }

            if (lastCouponDate != null && lastCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetLastCouponDate(lastCouponDate);
            }

            if (interestRRDate != null && interestRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(interestRRDate);
            }

            if (hoursClause != null)
            {
                await _finalSpreadObject.Set48HoursClause(hoursClause);
            }

            if (referenceRate == "1wE")
            {
                if (couponDayOption == null)
                {
                    System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");
                    var startDateTime = default(DateTime);
                    var maturityDateTime = default(DateTime);
                    DateTime.TryParse(startDate, cultureinfo, out startDateTime);
                    DateTime.TryParse(maturityDate, cultureinfo, out maturityDateTime);

                    DayOfWeek dayStart = startDateTime.DayOfWeek;
                    DayOfWeek dayMaturity = maturityDateTime.DayOfWeek;

                    if (dayStart == dayMaturity)
                    {
                        var dayOfWeek = dayStart.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime);
                    }
                    else if (dayStart != dayMaturity)
                    {
                        var dayOfWeek = dayMaturity.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _finalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime);

                        if (firstCouponDate == null && firstCouponDate == string.Empty)
                        {
                            await _finalSpreadObject.SetFirstCouponDate(startDate);
                        }
                    }
                }
                else
                {
                    await _finalSpreadObject.SetCouponDayOptionSet(couponDayOption);
                }
            }
            else
            {
                await _finalSpreadObject.SetCouponDay(couponDay);
            }
        }

        public async Task LoanUpdateVariableCaseSetDateFields(string startDate, string maturityDate, string referenceRate, bool? hoursClause = null, string? firstCouponDate = "", string? lastCouponDate = "", string interestRRDate = "", CouponDay? couponDayOption = null, string? couponDay = "")
        {
            await _finalSpreadObject.SetReferenceRate(referenceRate);

            await _finalSpreadObject.SetStartDate(startDate);
            await _finalSpreadObject.SetMaturityDate(maturityDate);

            if (firstCouponDate != null && firstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(firstCouponDate);
            }

            if (lastCouponDate != null && lastCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetLastCouponDate(lastCouponDate);
            }

            if (interestRRDate != null && interestRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(interestRRDate);
            }

            if (hoursClause != null)
            {
                await _finalSpreadObject.Set48HoursClause(hoursClause);
                await _basePageObject.PressTab();
            }

            if (referenceRate == "1wE")
            {
                if (couponDayOption == null)
                {
                    System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");
                    var startDateTime = default(DateTime);
                    var maturityDateTime = default(DateTime);
                    DateTime.TryParse(startDate, cultureinfo, out startDateTime);
                    DateTime.TryParse(maturityDate, cultureinfo, out maturityDateTime);

                    DayOfWeek dayStart = startDateTime.DayOfWeek;
                    DayOfWeek dayMaturity = maturityDateTime.DayOfWeek;

                    if (dayStart == dayMaturity)
                    {
                        var dayOfWeek = dayStart.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _loanUpdateFinalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime);
                    }
                    else if (dayStart != dayMaturity)
                    {
                        var dayOfWeek = dayMaturity.ToString();
                        Enum.TryParse(dayOfWeek, out CouponDay couponDayOptionFromDateTime);
                        await _loanUpdateFinalSpreadObject.SetCouponDayOptionSet(couponDayOptionFromDateTime);

                        if (firstCouponDate == null && firstCouponDate == string.Empty)
                        {
                            await _loanUpdateFinalSpreadObject.SetFirstCouponDate(startDate);
                        }
                    }
                }
                else
                {
                    await _finalSpreadObject.SetCouponDayOptionSet(couponDayOption);
                }
            }
            else
            {
                await _finalSpreadObject.SetCouponDay(couponDay);
            }
        }

        public async Task VariableSetSpreadFieldsFourEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string h2, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? h2Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "")
        {
            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            if (referenceRate == "6mE")
            {
                var expectedCustomerRate = await _variableDataTransforms.TransformSpreadDataVariableStringWithThreeDecimalPlaces(finalSpread);
                _scenarioContext["savedExpectedCustomerRate"] = expectedCustomerRate;

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
                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginAccountDeskUser2();

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
                await _basePageObject.OpenCreateLoanRequestLink();
                await _finalSpreadObject.SetSpreadH2(h2);
                if (setSpreadMessages == true)
                {
                    if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                    {
                        await _finalSpreadObject.SetSpreadH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                    }
                    else
                    {
                        await _finalSpreadObject.SetSpreadH2Comment(overWriteCommentH2);
                    }
                }

                if (h1 != h2)
                {
                    await _finalSpreadObject.ClickSaveH2Button();
                    await _finalSpreadObject.OverwriteSpreadMisMatch();
                    if (setSpreadMessages == true)
                    {
                        if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                        {
                            await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ");
                        }
                        else
                        {
                            await _finalSpreadObject.SetSpreadMisMatchComment(overWriteSpreadMismatchComment);
                        }
                    }

                    await _finalSpreadObject.ClickSaveH2Button();
                }
                else if (h1 == h2)
                {
                    await _finalSpreadObject.ClickSaveH2Button();
                }

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
            }
            else
            {
                var expectedCustomerRate = await _variableDataTransforms.TransformSpreadDataVariableStringWithThreeDecimalPlaces(finalSpread, finalBasis);
                _scenarioContext["savedExpectedCustomerRate"] = expectedCustomerRate;

                if (h1Basis is not null && h2Basis is not null && finalBasis is not null)
                {
                    await _finalSpreadObject.SetSpreadH1(h1);
                    await _finalSpreadObject.SetSpreadH1Basis(h1Basis);
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
                    await _basePageObject.LogOutSystem();
                    await _loginPageObject.LoginAccountDeskUser2();

                    await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
                    await _basePageObject.OpenCreateLoanRequestLink();
                    await _finalSpreadObject.SetSpreadH2(h2);
                    await _finalSpreadObject.SetSpreadH2Basis(h2Basis);
                    if (setSpreadMessages == true)
                    {
                        if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                        {
                            await _finalSpreadObject.SetSpreadH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                        }
                        else
                        {
                            await _finalSpreadObject.SetSpreadH2Comment(overWriteCommentH2);
                        }
                    }

                    if (h1 != h2 || h1Basis != h2Basis)
                    {
                        await _finalSpreadObject.ClickSaveH2Button();
                        await _finalSpreadObject.OverwriteSpreadMisMatch();
                        if (setSpreadMessages == true)
                        {
                            if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                            {
                                await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ");
                            }
                            else
                            {
                                await _finalSpreadObject.SetSpreadMisMatchComment(overWriteSpreadMismatchComment);
                            }
                        }

                        await _finalSpreadObject.ClickSaveH2Button();
                    }
                    else
                    {
                        await _finalSpreadObject.ClickSaveH2Button();
                    }

                    await _finalSpreadObject.SetFinalSpread(finalSpread);
                    await _finalSpreadObject.SetFinalSpreadBasis(finalBasis);
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
                }
            }
        }

        public async Task VariableSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "")
        {
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            if (referenceRate == "6mE")
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread);
                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

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
            }
            else
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread, finalBasis);
                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

                await _finalSpreadObject.SetSpreadH1(h1);
                await _finalSpreadObject.SetSpreadH1Basis(h1Basis);
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
                await _finalSpreadObject.SetFinalSpreadBasis(finalBasis);
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
            }
        }

        public async Task LoanUpdateVariableSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "")
        {
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            if (referenceRate == "6mE")
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread);

                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

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
            }
            else
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread, finalBasis);
                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

                await _finalSpreadObject.SetSpreadH1(h1);
                await _finalSpreadObject.SetSpreadH1Basis(h1Basis);
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
                await _finalSpreadObject.SetFinalSpreadBasis(finalBasis);
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
            }
        }

        public async Task LoanUpdateBaseRateSetSpreadFieldsTwoEyes(string referenceRate, string loanType, string lalCaseNumber, string h1, string finalSpread, bool setSpreadMessages, string? h1Basis = null, string? finalBasis = null, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteSpreadMismatchComment = "")
        {
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            if (referenceRate == "6mE")
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread);
                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

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
                await Task.Delay(800);
                await _finalSpreadObject.ClickSaveBaseRate();
            }
            else
            {
                double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(finalSpread, finalBasis);
                _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

                await _finalSpreadObject.SetSpreadH1(h1);
                await _finalSpreadObject.SetSpreadH1Basis(h1Basis);
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
                await _finalSpreadObject.SetFinalSpreadBasis(finalBasis);
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
                await Task.Delay(800);
                await _finalSpreadObject.ClickSaveBaseRate();
            }
        }
    }
}