using Reqnroll;
using System;
using System.Reflection;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class LoanUpdateCoverLetterStepDefinitions
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

        public LoanUpdateCoverLetterStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject)
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
            _checkDossierCompleteObject = checkDossierCompleteObject;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
        }

        [Then("the assertions for the loan update Cover Letter is valid")]
        public async Task ThenTheAssertionsForTheLoanUpdateCoverLetterIsValid()
        {
            var loanType = (string)_scenarioContext["savedLoanType"];
            if (loanType == "LOANUPDATEFIXED")
            {
                var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                var fixedAlternativeData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];

                var filepathLoanUpdateCoverletter = (string)_scenarioContext["savedFilePathLoanUpdateCoverLetter"];
                var htmlTEXT = await _createContractObject.ConvertWordToHtml(filepathLoanUpdateCoverletter);
            }
            else if (loanType == "LOANUPDATEBASERATE")
            {
                var baseRateOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
                var filepathLoanUpdateCoverletter = (string)_scenarioContext["savedFilePathLoanUpdateCoverLetter"];
                var htmlTEXT = await _createContractObject.ConvertWordToHtml(filepathLoanUpdateCoverletter);
            }
            else if (loanType == "LOANUPDATEVARIABLE")
            {
                var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];

                var filepathLoanUpdateCoverletter = (string)_scenarioContext["savedFilePathLoanUpdateCoverLetter"];
                var htmlTEXT = await _createContractObject.ConvertWordToHtml(filepathLoanUpdateCoverletter);

                var propertyValue = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
                object organisationFields = (object)_scenarioContext["savedCustomerObject"];

                var counterpartyName = propertyValue[7].GetValue(organisationFields).ToString();
                var coCNumber = propertyValue[10].GetValue(organisationFields).ToString();
                var sICCode = propertyValue[14].GetValue(organisationFields).ToString();
            }
        }
    }
}