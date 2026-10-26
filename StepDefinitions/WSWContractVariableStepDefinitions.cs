using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class WSWContractVariableStepDefinitions
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

        public WSWContractVariableStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject)
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

        [When(@"I have filled the following Variable loan details:")]
        public async Task WhenIHaveFilledTheFollowingVariableLoanDetails(Table table)
        {
            LongFinalSpreadFields exampledata = table.CreateInstance<LongFinalSpreadFields>();
            var caseNumber = await _finalSpreadObject.GetCaseID();

            decimal notional = decimal.Parse(exampledata.Notional, new CultureInfo("nl-NL"));
            string formattedNotional = notional.ToString("N2", new CultureInfo("nl-NL"));

            DateTime startdate = DateTime.ParseExact(exampledata.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string formattedStartDate = startdate.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));

            DateTime maturitydate = DateTime.ParseExact(exampledata.MaturityDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string formattedMaturityDate = startdate.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));

            _scenarioContext["expectedStartDate"] = formattedStartDate;
            _scenarioContext["expectedMaturityDate"] = formattedMaturityDate;
            _scenarioContext["expectedNotionalInNumbers"] = formattedNotional;
            _scenarioContext["savedTC"] = exampledata.TC;

            await _finalSpreadObject.SetCreditLimit("100000000");
            await _finalSpreadObject.SetNotional(exampledata.Notional);
            await _finalSpreadObject.SetDealMoment(" 2-05-2023");
            await _finalSpreadObject.SetCDDReviewDate(" 2-05-2023");
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"This is spread check remark for case: {caseNumber}");

            await _finalSpreadObject.SetSwap(true);
            await _finalSpreadObject.SetStartDate(exampledata.StartDate);
            await _finalSpreadObject.SetMaturityDate(exampledata.MaturityDate);
            await _finalSpreadObject.SetInterestSpreadRRDate(exampledata.InterestSpreadRRDate);
            await _finalSpreadObject.SetCouponDay("25");

            await _finalSpreadObject.SetSpreadH1("11");
            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalSpread("11");
            await _finalSpreadObject.SetFinalSpreadComment($"FinalSpreadComment for case: {caseNumber}");
            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetDealRemark("Deal remark for case: " + caseNumber);
            await _finalSpreadObject.SetMarketRate("2");

            await _finalSpreadObject.ClickSaveMarketRate();

            if (exampledata.DeleteGuarantor == true)
            {
                await _finalSpreadObject.UncheckGuarantorBox();
            }
        }
    }
}