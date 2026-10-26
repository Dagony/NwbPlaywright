using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class WSWContractLongStepDefinitions
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

        public WSWContractLongStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject)
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

        [When(@"I have filled in the following Bullet fixed loan details:")]
        public async Task WhenIHaveFilledInTheFollowingBulletFixedLoanDetails(Reqnroll.Table table)
        {
            await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Fixed);
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
            await _finalSpreadObject.SetDealMoment("02-02-2024");
            await _finalSpreadObject.SetCDDReviewDate("02-02-2024");
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"This is spread check remark for case: {caseNumber}");

            await _finalSpreadObject.SetSwap(true);
            await _finalSpreadObject.SetStartDate(exampledata.StartDate);
            await _finalSpreadObject.SetMaturityDate(exampledata.MaturityDate);
            await _finalSpreadObject.SetInterestSpreadRRDate(exampledata.InterestSpreadRRDate);
            await _finalSpreadObject.SetCouponDay(exampledata.CouponDay);
            await _finalSpreadObject.SetFirstCouponDate(exampledata.FirstCouponDate);

            if (exampledata.AmortType != "Bullet")
            {
                await _finalSpreadObject.SetCouponFrequency(exampledata.CouponFreq);
                await _finalSpreadObject.SetDayCount(exampledata.DayCount);
            }

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

        [When(@"I have filled in the following Non-Bullet fixed loan details:")]
        public async Task WhenIHaveFilledInTheFollowingNon_BulletFixedLoanDetails(Reqnroll.Table table)
        {
            await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Fixed);
            LongFinalSpreadFields nonbulletdata = table.CreateInstance<LongFinalSpreadFields>();
            var caseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedTC"] = nonbulletdata.TC;

            await _finalSpreadObject.SetCDDReviewDate("2-05-2023");
            await _finalSpreadObject.SetNotional(nonbulletdata.Notional);
            await _finalSpreadObject.SetDealMoment("2-05-2023");
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"This is spread check remark for case: {caseNumber}");
            await _finalSpreadObject.SetSwap(true);
            await _finalSpreadObject.SetStartDate(nonbulletdata.StartDate);
            await _finalSpreadObject.SetMaturityDate(nonbulletdata.MaturityDate);
            await _finalSpreadObject.SetInterestSpreadRRDate(nonbulletdata.InterestSpreadRRDate);
            await _finalSpreadObject.SetCouponFrequency(nonbulletdata.CouponFreq);
            await _finalSpreadObject.SetDayCount(nonbulletdata.DayCount);
            await _finalSpreadObject.SetCouponDay(nonbulletdata.CouponDay);
            await _finalSpreadObject.SetAmortisationType(nonbulletdata.AmortType);
            if (nonbulletdata.AmortType != "Bullet")
            {
                await _finalSpreadObject.SetNumberOfAmortisations(nonbulletdata.NumbOfAmort);
                await _finalSpreadObject.SetAmortisationFrequency(nonbulletdata.AmortFreq);
            }

            await _finalSpreadObject.SetSpreadH1("11");
            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalSpread("35,79");
            await _finalSpreadObject.SetFinalSpreadComment($"FinalSpreadComment for case: {caseNumber}");
            await _finalSpreadObject.ClickSaveFinalSpreadButton();

            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await Task.Delay(800);
            await _finalSpreadObject.SetMarketRate("5");
            await Task.Delay(800);
            await _finalSpreadObject.SetDealRemark("Deal remark for case: " + caseNumber);
            await _finalSpreadObject.ClickSaveMarketRate();
        }

        [Then(@"the outcome in the WSW word doc should be ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public async Task ThenTheOutcomeInTheWSWWordDocShouldBeAnd(string resultLooptijd, string resultNotationalInWords, string resultArticle4)
        {
            var filepathWSWContract = (string)_scenarioContext["savedFilePathWSWContract"];
            var filepathWSWSummary = (string)_scenarioContext["savedFilePathCoverWSWSummary"];
            var filepathCoverSheet = (string)_scenarioContext["savedFilePathCoverSheetLetter"];

            var (bodyText, footerText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathWSWContract);

            // // Perform assertions
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
            Console.WriteLine(bodyText.ToString());
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
            Console.WriteLine(bodyText);
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
        }

        [Then(@"the NWB Contract should be generated correctly")]
        public async Task ThenTheNWBContractShouldBeGeneratedCorrectly()
        {
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAlternativeData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];

            var filepathNWBContract = (string)_scenarioContext["savedFilePathNWBContract"];
            var htmlTEXT = await _createContractObject.ConvertWordToHtml(filepathNWBContract);

            var propertyValue = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            object organisationFields = (object)_scenarioContext["savedCustomerObject"];

            var counterpartyName = propertyValue[7].GetValue(organisationFields).ToString();
            var coCNumber = propertyValue[11].GetValue(organisationFields).ToString();
            var sICCode = propertyValue[15].GetValue(organisationFields).ToString();

            // // Perform assertions
            Console.WriteLine("----------------------------------STRING ARRAY: ----------------------------------------------------------------------------------");
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");

            // foreach (var index in htmlTEXT)
            // {
            Console.WriteLine(htmlTEXT);

            // }
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");

            // Console.WriteLine(htmlTEXT);
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
        }

        [Then(@"the WSW contract should include the following written text")]
        public async Task ThenTheWSWContractShouldIncludeTheFollowingWrittenText(Reqnroll.Table table)
        {
            WSWContractFields wswExpectedData = table.CreateInstance<WSWContractFields>();

            await Task.Delay(2000);
            if (wswExpectedData.ResultArt1ResultNotionalInNumbers != "no word.docx")
            {
                var filepathWSWContract = (string)_scenarioContext["savedFilePathWSWContract"];
                var filepathWSWSummary = (string)_scenarioContext["savedFilePathWSWSummary"];
                var filepathCoverSheet = (string)_scenarioContext["savedFilePathCoverLetter"];

                var (bodyText, footerText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathWSWContract);

                // // Perform assertions
                Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
                Console.WriteLine(bodyText.ToString());
                Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
                Console.WriteLine(footerText);
                Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");

                bodyText.Should().Contain(wswExpectedData.ResultPage1Counterparty);
                bodyText.Should().Contain("POSTBUS 91, 9000 AB GROU");
                bodyText.Should().Contain(wswExpectedData.ResultArt1ResultNotionalInWords);
                bodyText.Should().Contain(wswExpectedData.ResultArt1ResultNotionalInNumbers);

                bodyText.Should().Contain(wswExpectedData.ResultArt1StartDatum);
                bodyText.ToString().Should().Contain(wswExpectedData.ResultLooptijd);
                bodyText.Should().Contain(wswExpectedData.ResultArt3MaturityDatum);
            }
            else
            {
                await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByText("NWB contract cover sheet.docx").First.IsVisibleAsync();
                var nameOfCoverSheetLink = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByRole(AriaRole.Link, new FrameLocatorGetByRoleOptions
                {
                    Exact = false,
                    Name = "Er is een nieuwe lening afgesloten",
                }).InnerTextAsync();

                Console.WriteLine(nameOfCoverSheetLink);
            }
        }
    }
}