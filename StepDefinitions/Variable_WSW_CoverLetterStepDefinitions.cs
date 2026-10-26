using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.PageObjects;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class Variable_WSW_CoverLetterStepDefinitions
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

        public Variable_WSW_CoverLetterStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ILoanDataApprovalObject loanDataApprovalObject, IValidateRequestObject validateRequestObject, ISurePayCheckObject surePayCheckObject, ICreateContractObject createContractObject, IAgreementReviewObject agreementReviewObject, ICheckGuarantorDocumentObject checkGuarantorDocumentObject, IGuarantorApprovalObject guarantorApprovalObject, ISendAgreementObject sendAgreementObject, IClientApprovalObject clientApprovalObject, IProcessClientResponseObject processClientResponseObject, ILegalReviewObject legalReviewObject, ICheckDossierCompleteObject checkDossierCompleteObject)
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

        [Then(@"the Variable Cover Sheet should include the following written text")]
        public async Task ThenTheVariableCoverSheetShouldIncludeTheFollowingWrittenText()
        {
            var expectedData = (VariableFinalSpreadFields)_scenarioContext["savedTableData"];
            var formattedTransactionDate = (string)_scenarioContext["savedTransactionDate"];
            var filepathCoverSheet = (string)_scenarioContext["savedFilePathCoverLetter"];

            var dtStartDate = DateTime.ParseExact(expectedData.StartDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedStartDate = dtStartDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
            var dtMaturityDate = DateTime.ParseExact(expectedData.MaturityDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedMaturityDate = dtMaturityDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);

            // var formattedNotional = Convert.ToDecimal(expectedData.Notional, _testSettings.cultureInfo)
            // var (BodyText, FooterText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathCoverSheet);
            var bodyText = await _createContractObject.ConvertWordToHtml(filepathCoverSheet);

            if (expectedData.SetNotionalTypeToFixed == "False")
            {
                bodyText.Should().Contain($"Betreft: onderhandse geldlening maximaal groot");
                bodyText.Should().Contain($"bedrag der lening: maximaal € {expectedData.Notional}");
            }
            else if (expectedData.SetNotionalTypeToFixed != "False")
            {
                bodyText.Should().Contain($"Betreft: onderhandse geldlening groot");
                bodyText.Should().Contain($" € {expectedData.Notional}");
            }

            bodyText.Should().Contain("Geachte heer, mevrouw,");
            bodyText.Should().Contain($"Naar aanleiding van ons contact op {formattedTransactionDate}, bevestigen wij u hierbij, dat wij u een onderhandse geldlening verstrekken tegen de volgende voorwaarden:");
            bodyText.Should().Contain($"2. stortingsdatum: {formattedStartDate}");

            if (expectedData.ReferenceRate == "1wE")
            {
                bodyText.Should().Contain($"3. rente: 1-weeks Euribor, verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
                bodyText.Should().Contain($"6. rentebetaling: wekelijks op");
            }
            else if (expectedData.ReferenceRate == "1mE")
            {
                bodyText.Should().Contain($"3. rente: 1-maands Euribor, verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
                bodyText.Should().Contain($"6. rentebetaling: maandelijks te voldoen op de");
            }
            else if (expectedData.ReferenceRate == "3mE")
            {
                bodyText.Should().Contain($"3. rente: 3-maands Euribor, verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
                bodyText.Should().Contain($"6. rentebetaling: per kwartaal te voldoen op");
            }
            else if (expectedData.ReferenceRate == "6mE")
            {
                bodyText.Should().Contain($"3. rente: 6-maands Euribor, verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
                bodyText.Should().Contain($"6. rentebetaling: per half jaar te voldoen op");
            }
            else if (expectedData.ReferenceRate == "12mE")
            {
                bodyText.Should().Contain($"3. rente: 12-maands Euribor, verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
                bodyText.Should().Contain($"6. rentebetaling: jaarlijks te voldoen op");
            }

            bodyText.Should().Contain($"4. koers: 100%");
            bodyText.Should().Contain($"5. looptijd: tot {formattedMaturityDate}");

            if (expectedData.InterestSpreadRRDate != null && expectedData.InterestSpreadRRDate != string.Empty)
            {
                var dtSpreadRRDate = DateTime.ParseExact(expectedData.InterestSpreadRRDate, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedSpreadRRDate = dtSpreadRRDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                bodyText.Should().Contain($"7. spreadherziening: per {formattedSpreadRRDate}");
            }
            else if (expectedData.InterestSpreadRRDate == null && expectedData.InterestSpreadRRDate == string.Empty)
            {
                bodyText.Should().Contain($"7. spreadherziening: niet van toepassing");
            }

            if (expectedData.SetGuarantor == "True")
            {
                bodyText.Should().Contain($"8. garantie: Stichting Waarborgfonds");
            }
            else if (expectedData.SetGuarantor != "True")
            {
                bodyText.Should().Contain($"8. garantie: niet van toepassing");
            }

            bodyText.Should().Contain($"+Bij geen bericht zal op de stortingsdatum de volledige hoofdsom worden overgemaakt. Refererend\r\n30 +aan artikel 5 van de bijgevoegde leningsovereenkomst verzoeken wij u ons over opnames en\r\n31 +aflossingen twee werkdagen van tevoren uitsluitend via e-mail te informeren, naar mailadres\r\n32 +operations@nwbbank.com.");

            bodyText.Should().Contain($"Hoogachtend,");
            bodyText.Should().Contain($"NEDERLANDSE WATERSCHAPSBANK N.V.");

            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
            Console.WriteLine(bodyText.ToString());
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
        }

        [Then(@"the WSW Summary should include the following written text")]
        public async Task ThenTheWSWSummaryShouldIncludeTheFollowingWrittenText()
        {
            var expectedData = (VariableFinalSpreadFields)_scenarioContext["savedTableData"];
            var formattedTransactionDate = (string)_scenarioContext["savedTransactionDate"];
            var filepathCoverSheet = (string)_scenarioContext["savedFilePathWSWSummary"];
            var dtStartDate = DateTime.ParseExact(expectedData.StartDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedStartDate = dtStartDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
            var dtMaturityDate = DateTime.ParseExact(expectedData.MaturityDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedMaturityDate = dtMaturityDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);

            // var formattedNotional = Convert.ToDecimal(expectedData.Notional, _testSettings.cultureInfo)
            var (bodyText, footerText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathCoverSheet);

            if (expectedData.HoursClause == "True")
            {
                bodyText.Should().Contain($"aanvullende trekking binnen 2 werkdagen");
            }
            else
            {
                bodyText.Should().Contain($"Soort contract: Variabel\r\n");
            }

            if (expectedData.SetNotionalTypeToFixed == "False")
            {
                bodyText.Should().Contain($"NWB Leningsnummer: 2");
            }
            else if (expectedData.SetNotionalTypeToFixed != "False")
            {
                bodyText.Should().Contain($"NWB Leningsnummer: 1");
            }

            bodyText.Should().Contain($"Naam statutair corporatie: Stichting Maasdelta Groep (MDG)");
            bodyText.Should().Contain($"Adres: POSTBUS 34, 3200 AA, SPIJKENISSE");
            bodyText.Should().Contain($"Datum acceptatie : {formattedTransactionDate}");
            bodyText.Should().Contain($"Hoofdsom: EUR {expectedData.Notional}");
            bodyText.Should().Contain($"Storting: {formattedStartDate}");

            if (expectedData.ReferenceRate == "1wE")
            {
                bodyText.Should().Contain($"Rentepercentage: 1-weeks Euribor verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
            }
            else if (expectedData.ReferenceRate == "1mE")
            {
                bodyText.Should().Contain($"Rentepercentage: 1-maands Euribor verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
            }
            else if (expectedData.ReferenceRate == "3mE")
            {
                bodyText.Should().Contain($"Rentepercentage: 3-maands Euribor verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
            }
            else if (expectedData.ReferenceRate == "6mE")
            {
                bodyText.Should().Contain($"Rentepercentage: 6-maands Euribor verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
            }
            else if (expectedData.ReferenceRate == "12mE")
            {
                bodyText.Should().Contain($"Rentepercentage: 12-maands Euribor verhoogd met een opslag van {expectedData.CustomerRate} basispunt(en)");
            }

            bodyText.Should().Contain($"Looptijd: {expectedData.ExpectedLooptijd}");
            bodyText.Should().Contain($"Einddatum: {formattedMaturityDate}");
            bodyText.Should().Contain($"Achtervanggemeente: niet van toepassing");

            if (expectedData.InterestSpreadRRDate != string.Empty)
            {
                var dtSpreadRRDate = DateTime.ParseExact(expectedData.InterestSpreadRRDate, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedSpreadRRDate = dtSpreadRRDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                bodyText.Should().Contain($"Spreadherziening: {formattedSpreadRRDate}");
            }
            else if (expectedData.InterestSpreadRRDate == string.Empty)
            {
                bodyText.Should().Contain($"Spreadherziening: niet van toepassing");
            }

            bodyText.Should().Contain($"Renteconventie: Act/360");

            bodyText.Should().Contain($"Aflossingsvorm: v");
            bodyText.Should().Contain($"v = variabel");

            if (expectedData.FirstCouponDate != string.Empty)
            {
                var dtFirstCouponDate = DateTime.ParseExact(expectedData.FirstCouponDate, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedFirstCouponDate = dtFirstCouponDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedFirstCouponDate}");
            }
            else if (expectedData.FirstCouponDate == string.Empty)
            {
                if (expectedData.ReferenceRate == "1wE")
                {
                    var dtStartDatePlusTermijn = dtStartDate.AddDays(7);

                    var formattedStartDatePlusTermijn = dtStartDatePlusTermijn.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                    bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedStartDatePlusTermijn}");
                }
                else if (expectedData.ReferenceRate == "1mE")
                {
                    var dtStartDatePlusTermijn = dtStartDate.AddMonths(1);

                    var formattedStartDatePlusTermijn = dtStartDatePlusTermijn.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                    bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedStartDatePlusTermijn}");
                }
                else if (expectedData.ReferenceRate == "3mE")
                {
                    var dtStartDatePlusTermijn = dtStartDate.AddMonths(3);

                    var formattedStartDatePlusTermijn = dtStartDatePlusTermijn.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                    bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedStartDatePlusTermijn}");
                }
                else if (expectedData.ReferenceRate == "6mE")
                {
                    var dtStartDatePlusTermijn = dtStartDate.AddMonths(6);

                    var formattedStartDatePlusTermijn = dtStartDatePlusTermijn.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                    bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedStartDatePlusTermijn}");
                }
                else if (expectedData.ReferenceRate == "12mE")
                {
                    var dtStartDatePlusTermijn = dtStartDate.AddMonths(12);

                    var formattedStartDatePlusTermijn = dtStartDatePlusTermijn.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                    bodyText.Should().Contain($"Datum eerste aflossing: niet van toepassing\r\n33 +Datum eerste rente: {formattedStartDatePlusTermijn}");
                }
            }

            bodyText.Should().Contain($"Gegevens WSW");

            bodyText.Should().Contain($"Disagio: EUR");
            bodyText.Should().Contain($"Leningnummer WSW:");
            bodyText.Should().Contain($"Projectnummer:");
            bodyText.Should().Contain($"Datum afgifte borgverklaring:");
            bodyText.Should().Contain($"College besluit aanwezig:");
            bodyText.Should().Contain($"Naam Gemeente:");
            bodyText.Should().Contain($"Achtervang overeenkomst:");
            bodyText.Should().Contain($"Overige opmerkingen:");
        }

        [Then(@"the WSW Contract with VARIABLE NOTIONAL should include the following written text")]
        public async Task ThenTheWSWContractWithVARIABLENOTIONALShouldIncludeTheFollowingWrittenText()
        {
            var expectedData = (VariableFinalSpreadFields)_scenarioContext["savedTableData"];
            var formattedTransactionDate = (string)_scenarioContext["savedTransactionDate"];
            var filepathWSWContract = (string)_scenarioContext["savedFilePathWSWContract"];
            var dtStartDate = DateTime.ParseExact(expectedData.StartDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedStartDate = dtStartDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
            var dtMaturityDate = DateTime.ParseExact(expectedData.MaturityDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedMaturityDate = dtMaturityDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);

            // var formattedNotional = Convert.ToDecimal(expectedData.Notional, _testSettings.cultureInfo)
            var (bodyText, footerText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathWSWContract);

            if (expectedData.SetNotionalTypeToFixed == "False")
            {
                bodyText.Should().Contain($"Leningnummer: 2");
            }
            else if (expectedData.SetNotionalTypeToFixed != "False")
            {
                bodyText.Should().Contain($"Leningnummer: 1");
            }

            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
            Console.WriteLine(bodyText.ToString());
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
        }

        [Then(@"the WSW Contract with FIXED NOTIONAL should include the following written text")]
        public async Task ThenTheWSWContractWithFIXEDNOTIONALShouldIncludeTheFollowingWrittenText()
        {
            var expectedData = (VariableFinalSpreadFields)_scenarioContext["savedTableData"];
            var formattedTransactionDate = (string)_scenarioContext["savedTransactionDate"];
            var filepathWSWContract = (string)_scenarioContext["savedFilePathWSWContract"];
            var dtStartDate = DateTime.ParseExact(expectedData.StartDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedStartDate = dtStartDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
            var dtMaturityDate = DateTime.ParseExact(expectedData.MaturityDate, "dd-M-yyyy", _testSettings.CultureInfo);
            var formattedMaturityDate = dtMaturityDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);

            // var formattedNotional = Convert.ToDecimal(expectedData.Notional, _testSettings.cultureInfo)
            var (bodyText, footerText) = await _createContractObject.ExtractBodyandFooterFromDocxFile(filepathWSWContract);

            if (expectedData.SetNotionalTypeToFixed == "False")
            {
                bodyText.Should().Contain($"Leningnummer: 2");
            }
            else if (expectedData.SetNotionalTypeToFixed != "False")
            {
                bodyText.Should().Contain($"Leningnummer: 1");
            }

            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
            Console.WriteLine(bodyText.ToString());
            Console.WriteLine("----------------------------------PRINTED TEXT ----------------------------------------------------------------------------------");
        }
    }
}