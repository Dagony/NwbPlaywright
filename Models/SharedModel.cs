using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.Models
{
    public interface ISharedModel
    {
        string SharedEmptyString { get; set; }

        Task CaseSetOfferFields(string setCDDReviewDate = "", string setDealMoment = "", string setNotional = "", string setCreditLimit = "");

        Task GuarantorSteps();

        Task SetGuarantorField(string guarantor);

        Task LoanUpdateCaseSetOfferFields(string setDealMoment = "", string setNotional = "", string setCreditLimit = "");

        Task SetExtraEmailFields(string emailOne, string? emailTwo = "", int? numbOfMails = 1);

        Task OverwriteSpreadCheckRemark(string overwriteText);

        Task OverwriteDealRemark(string overwriteText);

        Task OverwriteLoanUpdateSpreadCheckRemark(string overwriteText);

        Task OverwriteLoanUpdateDealRemark(string overwriteText);

        Task SetGuarantorIfPresent(string guarantor1);

        Task LogOutFrontOfficeLogInBackOffice(PEGAClassStructure classOption, string aLLorALUCaseNumber);

        Task SetFirstCouponLastCouponInterestRRDatesIfPresent(string firstCouponDate, string lastCouponDate, string interestRRDate);

        Task LoanUpdateSetGuarantorIfPresent(string guarantor1);

        Task DepositCaseSetOfferFields(string setDealMoment = "", string setNotional = "", string setCreditLimit = "");

        Task LoanUpdateSetExtraEmailFields(string emailOne, string? emailTwo = "", int? numbOfMails = 1);
    }

    public class SharedModel : ISharedModel
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

        private readonly ILoanUpdateLoanDataApprovalObject _loanUpdateLoanDataApprovalObject;

        private readonly ILoanUpdateBillOfClosureObject _loanUpdateBillOfClosureObject;

        private readonly IValidateRequestObject _validateRequestObject;

        private readonly ISurePayCheckObject _surePayCheckObject;

        private readonly ICreateContractObject _createContractObject;

        private readonly IAgreementReviewObject _agreementReviewObject;

        private readonly ICheckGuarantorDocumentObject _checkGuarantorDocumentObject;

        private readonly IGuarantorApprovalObject _guarantorApprovalObject;

        private readonly ISharedDataTransforms _sharedDataTransforms;

        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;

        private readonly ISendAgreementObject _sendAgreementObject;

        private readonly IClientApprovalObject _clientApprovalObject;

        private readonly IProcessClientResponseObject _processClientResponseObject;

        private readonly ILegalReviewObject _legalReviewObject;

        private readonly ICheckDossierCompleteObject _checkDossierCompleteObject;

        private readonly ILocator _includeInMailBoxLocator;

        private readonly IPlaywrightDriver _playwrightDriver;

        private readonly ISendConfirmationObject _sendConfirmationObject;

        private readonly ISharedDataIndividuals _sharedDataIndividual;

        public SharedModel(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IBillOfClosureObject billOfClosureObject,
            ILoanUpdateBillOfClosureObject loanUpdateBillOfClosureObject,
            IClientApprovalObject clientApprovalObject,
            ILoanDataApprovalObject loanDataApprovalObject,
            ICheckGuarantorDocumentObject checkGuarantorDocumentObject,
            IGuarantorApprovalObject guarantorApprovalObject,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            ILoanUpdateLoanDataApprovalObject loanUpdateLoanDataApprovalObject)
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
            _checkGuarantorDocumentObject = checkGuarantorDocumentObject;
            _guarantorApprovalObject = guarantorApprovalObject;
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _billOfClosureObject = billOfClosureObject;
            _loanDataApprovalObject = loanDataApprovalObject;
            _loanUpdateLoanDataApprovalObject = loanUpdateLoanDataApprovalObject;
            _loanUpdateBillOfClosureObject = loanUpdateBillOfClosureObject;
        }

        public string SharedEmptyString { get; set; } = string.Empty;

        public async Task SetGuarantorIfPresent(string guarantor1)
        {
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];

            if (cifNumber == "0636914739")
            {
                await _finalSpreadObject.ClickGuarantorField();
                await _finalSpreadObject.DeleteGuarantorField();
            }

            if (guarantor1 != null && guarantor1 != string.Empty)
            {
                if (await _finalSpreadObject.CheckIfGuarantorFieldIsAlreadyPresent() == false)
                {
                    await _finalSpreadObject.ClickAddGuarantorButton();
                }

                await _finalSpreadObject.SetGuarantorField(guarantor1, 1);
                _scenarioContext["savedGuarantor1"] = guarantor1;
            }
            else
            {
                _scenarioContext["savedGuarantor1"] = string.Empty;
            }

            await Task.Delay(800);
        }

        public async Task LoanUpdateSetGuarantorIfPresent(string guarantor1)
        {
            if (guarantor1 != null && guarantor1 != string.Empty)
            {
                if (await _loanUpdateFinalSpreadObject.GetGuarantorSectionStatus() == false)
                {
                    await _loanUpdateFinalSpreadObject.OpenGuarantorSection();
                    await Task.Delay(500);
                }

                await _loanUpdateFinalSpreadObject.ClickAddGuarantorButton();
                await _loanUpdateFinalSpreadObject.SetGuarantorField(guarantor1, 1);
                _scenarioContext["savedGuarantor1"] = guarantor1;
            }
            else
            {
                _scenarioContext["savedGuarantor1"] = string.Empty;
            }
        }

        public async Task GuarantorSteps()
        {
            ///////////////  Open - CheckGuarantorDocument page// ALLEEN nodig bij GUARANTOR
            await _checkGuarantorDocumentObject.OpenCheckGuarantorDocumentLink();
            await _checkGuarantorDocumentObject.ClickSubmitOpenCheckGuarantorDocumentPage();

            ///////////////Pending - GuarantorApproval page// ALLEEN nodig bij GUARANTOR
            await _guarantorApprovalObject.ClickApproveButtonGuarantorApprovalPage();
        }

        public async Task SetGuarantorField(string guarantor)
        {
            _scenarioContext["savedGuarantor1"] = guarantor;
            await _finalSpreadObject.CheckGuarantorBox();
            await _finalSpreadObject.ClickAddGuarantorButton();
            await _finalSpreadObject.SetGuarantorField(guarantor);
        }

        public async Task SetFirstCouponLastCouponInterestRRDatesIfPresent(string firstCouponDate, string lastCouponDate, string interestRRDate)
        {
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
        }

        public async Task CaseSetOfferFields(string setCDDReviewDate = "", string setDealMoment = "", string setNotional = "", string setCreditLimit = "")
        {
            var loanType = (string)_scenarioContext["savedLoanType"];

            string? cDDreviewDate = _sharedDataTransforms.GetDateYesterday();
            string? dealMoment = _sharedDataTransforms.GetDateYesterday();
            string? notional = _basePageObject.GetRandomNotional(1000, 50000000);

            cDDreviewDate = string.IsNullOrEmpty(setCDDReviewDate) ? cDDreviewDate : setCDDReviewDate;
            dealMoment = string.IsNullOrEmpty(setDealMoment) ? dealMoment : setDealMoment;
            notional = string.IsNullOrEmpty(setNotional) ? notional : setNotional;
            if (loanType == "LOANUPDATEFIXED" | loanType == "LOANUPDATEVARIABLE")
            {
                await _loanUpdateFinalSpreadObject.SetDealMoment(dealMoment);
            }
            else
            {
                await _finalSpreadObject.SetDealMoment(dealMoment);
                await _finalSpreadObject.SetNotional(notional);
                await _basePageObject.PressTab();
            }

            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedDealMoment"] = dealMoment;
            if (setCreditLimit != null && setCreditLimit != string.Empty)
            {
                await _finalSpreadObject.SetCreditLimit(setCreditLimit);
            }
        }

        public async Task LoanUpdateCaseSetOfferFields(string setDealMoment = "", string setNotional = "", string setCreditLimit = "")
        {
            string? dealMoment = _sharedDataTransforms.GetDateYesterday();
            dealMoment = string.IsNullOrEmpty(setDealMoment) ? dealMoment : setDealMoment;
            await _finalSpreadObject.SetDealMoment(dealMoment);

            if (setNotional != null && setNotional != string.Empty)
            {
                await _finalSpreadObject.SetNotional(setNotional);
            }

            _scenarioContext["savedNotional"] = setNotional;
            _scenarioContext["savedDealMoment"] = dealMoment;
            if (setCreditLimit != null && setCreditLimit != string.Empty)
            {
                await _finalSpreadObject.SetCreditLimit(setCreditLimit);
            }
        }

        public async Task DepositCaseSetOfferFields(string setDealMoment = "", string setNotional = "", string setCreditLimit = "")
        {
            string? dealMoment = _sharedDataTransforms.GetDateYesterday();
            string? notional = _basePageObject.GetRandomNotional(1000, 50000000);
            dealMoment = string.IsNullOrEmpty(setDealMoment) ? dealMoment : setDealMoment;
            notional = string.IsNullOrEmpty(setNotional) ? notional : setNotional;
            await _loanUpdateFinalSpreadObject.SetDealMoment(dealMoment);

            if (setNotional is not null)
            {
                await _loanUpdateFinalSpreadObject.SetNotional(setNotional);
            }

            _scenarioContext["savedNotional"] = setNotional;
            _scenarioContext["savedDealMoment"] = dealMoment;
            if (setCreditLimit != null && setCreditLimit != string.Empty)
            {
                await _loanUpdateFinalSpreadObject.SetCreditLimit(setCreditLimit);
            }
        }

        public async Task SetExtraEmailFields(string emailOne, string? emailTwo = "", int? numbOfMails = 1)
        {
            await _finalSpreadObject.OpenEmailSection();

            await _finalSpreadObject.ClickAddEmailButton(numbOfMails);
            if (numbOfMails > 1)
            {
                await _finalSpreadObject.SetEmailAddress(emailOne, 1);
                await _finalSpreadObject.SetEmailAddress(emailTwo!, 2);
            }
            else
            {
                await _finalSpreadObject.SetEmailAddress(emailOne, 1);
            }
        }

        public async Task LoanUpdateSetExtraEmailFields(string emailOne, string? emailTwo = "", int? numbOfMails = 1)
        {
            await _loanUpdateFinalSpreadObject.OpenExtraEmailSection();
            await _loanUpdateFinalSpreadObject.ClickAddEmailButton(numbOfMails);

            if (numbOfMails > 1)
            {
                await _loanUpdateFinalSpreadObject.SetEmailAddress(emailOne, 1);
                await _loanUpdateFinalSpreadObject.SetEmailAddress(emailTwo!, 2);
            }
            else
            {
                await _loanUpdateFinalSpreadObject.SetEmailAddress(emailOne, 1);
            }
        }

        public async Task LogOutFrontOfficeLogInBackOffice(PEGAClassStructure classOption, string allOrAluCaseNumber)
        {
            await Task.Delay(1000);
            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
                await Task.Delay(1000);
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                if (classOption == PEGAClassStructure.New)
                {
                    await _loanUpdateBillOfClosureObject.ClickALLCaseNumber(allOrAluCaseNumber);
                    await _basePageObject.PageWaitForUrlLoaded();
                    await _loanUpdateLoanDataApprovalObject.OpenWaitInitiateDataLink();
                    await _loanUpdateLoanDataApprovalObject.ClickYesContinueButton();
                }
                else
                {
                    await _billOfClosureObject.ClickALLCaseNumber(allOrAluCaseNumber);
                    await _basePageObject.PageWaitForUrlLoaded();
                    await _loanDataApprovalObject.OpenWaitInitiateDataLink();
                    await _loanDataApprovalObject.ClickYesContinueButton();
                }

                await _basePageObject.WaitForPageLoadedState();
                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
                await Task.Delay(2500);
            }
            else
            {
                throw new Exception("Unknown parameter set for _testSettings.RunModePRODUCTIONLIKE. Origin call from SharedModel.cs LogOutFrontOfficeLogInBackOffice()");
            }

            await Task.Delay(2500);
        }

        public async Task OverwriteSpreadCheckRemark(string overwriteText)
        {
            await _finalSpreadObject.SpreadCheckSetRemarks(overwriteText);
        }

        public async Task OverwriteDealRemark(string overwriteText)
        {
            await _finalSpreadObject.SetDealRemark(overwriteText);
        }

        public async Task OverwriteLoanUpdateSpreadCheckRemark(string overwriteText)
        {
            await _loanUpdateFinalSpreadObject.SpreadCheckSetRemarks(overwriteText);
        }

        public async Task OverwriteLoanUpdateDealRemark(string overwriteText)
        {
            await _loanUpdateFinalSpreadObject.SetDealRemark(overwriteText);
        }
    }
}