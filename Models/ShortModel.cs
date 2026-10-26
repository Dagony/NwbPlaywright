using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.Models
{
    public interface IShortModel
    {
        Task ShortCaseSetSpreadFieldsTwoEyes(string priceH1, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "");

        Task ShortCaseSetDateFields(string startDate, string maturityDate);

        Task ShortCaseSetSpreadFieldsFourEyes(string priceH1, string priceH2, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "", string? overWriteSpreadMismatchComment = "");

        Task LoanUpdateShortCaseSetDateFields(string startDate, string maturityDate);

        Task LoanUpdateShortCaseSetSpreadFieldsTwoEyes(string priceH1, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "");

        Task LoanUpdateShortCaseSetSpreadFieldsFourEyes(string priceH1, string priceH2, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "", string? overWriteSpreadMismatchComment = "");
    }

    public class ShortModel : IShortModel
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
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly ISharedDataIndividuals _sharedDataIndividual;

        public ShortModel(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IClientApprovalObject clientApprovalObject,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject)
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
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
        }

        public async Task ShortCaseSetDateFields(string startDate, string maturityDate)
        {
            await _finalSpreadObject.SetStartDate(startDate);
            await _finalSpreadObject.SetMaturityDate(maturityDate);
        }

        public async Task LoanUpdateShortCaseSetDateFields(string startDate, string maturityDate)
        {
            await _finalSpreadObject.SetStartDate(startDate);
            await _basePageObject.PressTab();
            await _finalSpreadObject.SetMaturityDate(maturityDate);
            await _basePageObject.PressTab();
        }

        public async Task ShortCaseSetSpreadFieldsFourEyes(string priceH1, string priceH2, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "", string? overWriteSpreadMismatchComment = "")
        {
            double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(priceFinal);

            _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _finalSpreadObject.SetCustomerRateH1(priceH1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetCustomerH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH1Comment(overWriteCommentH1);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();
            await _finalSpreadObject.ClickSaveOffer();

            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);

            await _finalSpreadObject.SetCustomerRateH2(priceH2);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                {
                    await _finalSpreadObject.SetCustomerH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH2Comment(overWriteCommentH2);
                }
            }

            if (priceH1 != priceH2)
            {
                await _finalSpreadObject.ClickSaveH2Button();
                await _finalSpreadObject.OverwriteSpreadMisMatch();
                if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                {
                    await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ");
                }

                await _finalSpreadObject.ClickSaveH2Button();
            }
            else if (priceH1 == priceH2)
            {
                await _finalSpreadObject.ClickSaveH2Button();
            }

            await _finalSpreadObject.SetFinalCustomerRate(priceFinal);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment(overWriteCommentFinal);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetMargin(margin);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentMargin == string.Empty || overWriteCommentMargin is null)
                {
                    await _finalSpreadObject.SetMarginComment($"Margin comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetMarginComment(overWriteCommentMargin);
                }
            }

            await _finalSpreadObject.ClickSaveMargin();
        }

        public async Task ShortCaseSetSpreadFieldsTwoEyes(string priceH1, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "")
        {
            double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(priceFinal);

            _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

            await _finalSpreadObject.SpreadCheckBy(false);
            _scenarioContext["savedSpreadCheck"] = false;
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _finalSpreadObject.SetCustomerRateH1(priceH1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetCustomerH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH1Comment(overWriteCommentH1);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalCustomerRate(priceFinal);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment(overWriteCommentFinal);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton();

            await _finalSpreadObject.SetMargin(margin);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentMargin == string.Empty || overWriteCommentMargin is null)
                {
                    await _finalSpreadObject.SetMarginComment($"Margin comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetMarginComment(overWriteCommentMargin);
                }

                await _finalSpreadObject.ClickSaveMargin();
            }
        }

        public async Task LoanUpdateShortCaseSetSpreadFieldsTwoEyes(string priceH1, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "")
        {
            double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(priceFinal);

            _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;

            await _finalSpreadObject.SpreadCheckBy(false);
            _scenarioContext["savedSpreadCheck"] = false;
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _finalSpreadObject.SetCustomerRateH1(priceH1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetCustomerH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH1Comment(overWriteCommentH1);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalCustomerRate(priceFinal);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment(overWriteCommentFinal);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton();

            await _finalSpreadObject.SetMargin(margin);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentMargin == string.Empty || overWriteCommentMargin is null)
                {
                    await _finalSpreadObject.SetMarginComment($"Margin comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetMarginComment(overWriteCommentMargin);
                }

                await _finalSpreadObject.ClickSaveMargin();
            }
        }

        public async Task LoanUpdateShortCaseSetSpreadFieldsFourEyes(string priceH1, string priceH2, string priceFinal, string margin, string loanType, string lalCaseNumber, bool setSpreadMessages, string? overWriteCommentH1 = "", string? overWriteCommentH2 = "", string? overWriteCommentFinal = "", string? overWriteCommentMargin = "", string? overWriteSpreadMismatchComment = "")
        {
            double doubleCustomerRate = _basePageObject.TransformVariableCustomerRateToDouble(priceFinal);
            _scenarioContext["savedExpectedCustomerRate"] = doubleCustomerRate;
            var initialLoggedInTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];

            await _finalSpreadObject.SpreadCheckBy(true);
            _scenarioContext["savedSpreadCheck"] = true;
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for {loanType} case: {lalCaseNumber}");

            await _finalSpreadObject.SetCustomerRateH1(priceH1);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH1 == string.Empty || overWriteCommentH1 is null)
                {
                    await _finalSpreadObject.SetCustomerH1Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH1Comment(overWriteCommentH1);
                }
            }

            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.ClickSaveOffer();
            await _basePageObject.LogOutSystem();

            var chooseLogin = initialLoggedInTestUser switch
            {
                TestUser.AccountDesk1 => _loginPageObject.LoginAccountDeskUser2(),
                TestUser.Treasury1 => _loginPageObject.LoginTreasury2(),
                TestUser.TreasuryManager1 => _loginPageObject.LoginTreasuryManager2(),
                _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
            };

            await Task.Delay(5000);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await _basePageObject.OpenCreateOffer();
            await _finalSpreadObject.SetCustomerRateH2(priceH2);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentH2 == string.Empty || overWriteCommentH2 is null)
                {
                    await _finalSpreadObject.SetCustomerH2Comment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetCustomerH2Comment(overWriteCommentH2);
                }
            }

            if (priceH1 != priceH2)
            {
                await _finalSpreadObject.ClickSaveH2Button();
                await _finalSpreadObject.OverwriteSpreadMisMatch();
                if (overWriteSpreadMismatchComment == string.Empty || overWriteSpreadMismatchComment is null)
                {
                    await _finalSpreadObject.SetSpreadMisMatchComment($"Mismatch comment for {loanType} case: {lalCaseNumber} ");
                }

                await _finalSpreadObject.ClickSaveH2Button();
            }
            else if (priceH1 == priceH2)
            {
                await _finalSpreadObject.ClickSaveH2Button();
            }

            await _finalSpreadObject.SetFinalCustomerRate(priceFinal);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentFinal == string.Empty || overWriteCommentFinal is null)
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment($"Final Spread comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetFinalCustomerRateComment(overWriteCommentFinal);
                }
            }

            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetMargin(margin);
            if (setSpreadMessages == true)
            {
                if (overWriteCommentMargin == string.Empty || overWriteCommentMargin is null)
                {
                    await _finalSpreadObject.SetMarginComment($"Margin comment for {loanType} case: {lalCaseNumber} ");
                }
                else
                {
                    await _finalSpreadObject.SetMarginComment(overWriteCommentMargin);
                }
            }

            await _finalSpreadObject.ClickSaveMargin();
        }
    }
}