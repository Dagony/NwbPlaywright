using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using static NUnit.Framework.Internal.OSPlatform;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class EmailContentShortStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IShortModel _shortModel;
        private readonly ISharedModel _sharedModel;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISendConfirmationObject _sendConfirmationObject;

        public EmailContentShortStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ISharedDataTransforms sharedDataTransforms,
            IShortModel shortModel,
            ISharedModel sharedModel)
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
            _sharedDataTransforms = sharedDataTransforms;
            _shortModel = shortModel;
            _sharedModel = sharedModel;
        }

        [Given(@"I create a short loan with the following case and offer specifics")]
        public async Task GivenICreateAShortLoanWithTheFollowingCaseAndOfferSpecifics(Table table)
        {
            ShortLoanCaseOfferFields shortOfferData = table.CreateInstance<ShortLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = shortOfferData;
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();

            // await _basePageObject.SelectLoanRequest(LoanRequest.Short);
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = shortOfferData.TC;
            _scenarioContext["savedNotional"] = shortOfferData.Notional;

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime);
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for short case: {lalCaseNumber} ");
            await _sharedModel.SetGuarantorIfPresent(shortOfferData.Guarantor1);
            await _finalSpreadObject.SetNotional(shortOfferData.Notional);
        }

        [Given(@"I create a short loan with the following alternative specifics")]
        public async Task GivenICreateAShortLoanWithTheFollowingAlternativeSpecifics(Table table)
        {
            ShortLoanAlternativeFields alternativeData = table.CreateInstance<ShortLoanAlternativeFields>();
            _scenarioContext["savedAltData"] = alternativeData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();

            if (alternativeData.StartDate == "today")
            {
                await _shortModel.ShortCaseSetDateFields(currentFormattedDate, alternativeData.MaturityDate);
            }
            else
            {
                await _shortModel.ShortCaseSetDateFields(alternativeData.StartDate, alternativeData.MaturityDate);
            }

            await _shortModel.ShortCaseSetSpreadFieldsTwoEyes(alternativeData.PriceH1, alternativeData.PriceFinal, alternativeData.Margin, "LAL", lalCaseNumber, true);

            if (alternativeData.DealRemark != null && alternativeData.DealRemark != string.Empty)
            {
                await _finalSpreadObject.SetDealRemark(alternativeData.DealRemark);
            }
        }

        [Then("the short offer email table should match the expected SHORT table")]
        public async Task ThenTheShortOfferEmailTableShouldMatchTheExpectedShortTable()
        {
            var shortCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var shortAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var pricefinalTransformed = shortAltData.PriceFinal;
            var (formattedStartDate, formattedMaturityDate) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, shortAltData.StartDate, shortAltData.MaturityDate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            actualTable.Should().NotContainAny("Aflossing", "Aantal Aflossingen", "Aflossingsfrequentie", "Variabel Rentende Lening", "Variabele Hoofdsom Lening", "Euribor", "Kredietopslag (in bps)", "48uurs clausule", "Bereidstellingsprovisie (bps)", "Renteherziening", "Spreadherzieningsdatum", "Eerste coupondatum", "Laatste coupondatum");

            actualEmailText.Should().Contain($"Offerte Kasgeldlening ({lalCaseNumber})");
            actualTable.Should().Contain($"Geldnemer\t{shortCaseOfferData.CounterParty}");
            actualTable.Should().Contain($"Hoofdsom (in eur)\t{shortCaseOfferData.Notional}");
            actualTable.Should().Contain($"Ingangsdatum\t{formattedStartDate}");
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDate}");
            actualTable.Should().Contain($"Rente%\t{pricefinalTransformed}");
            actualTable.Should().Contain($"Renteconventie\tAct/360");

            if (shortAltData.DealRemark != null && shortAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{shortAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
            }

            if (sICCode == "0031000000")
            {
                actualTable.Should().Contain($"Zekerheid\tWaarborgfonds voor de Zorgsector");
            }
            else
            {
                if (shortCaseOfferData.Guarantor1 != null && shortCaseOfferData.Guarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{shortCaseOfferData.Guarantor1}");
                }
                else
                {
                    actualTable.Should().NotContain($"Zekerheid");
                }
            }

            Console.WriteLine($"PRINTING ACTUAL TABLE BELOW FOR SHORT CASE {lalCaseNumber}:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualTable);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }

        [Then("the short offer email table should match the expected table")]
        public async Task ThenTheShortOfferEmailTableShouldMatchTheExpectedTable()
        {
            var shortCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var shortAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var pricefinalTransformed = await _basePageObject.TransformSpreadDataToStringWithThreeDecimalPlaces(shortAltData.PriceFinal);
            var (formattedStartDate, formattedMaturityDate) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, shortAltData.StartDate, shortAltData.MaturityDate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            actualTable.Should().NotContainAny("Aflossing", "Aantal Aflossingen", "Aflossingsfrequentie", "Variabel Rentende Lening", "Variabele Hoofdsom Lening", "Euribor", "Kredietopslag (in bps)", "48uurs clausule", "Bereidstellingsprovisie (bps)", "Renteherziening", "Spreadherzieningsdatum", "Eerste coupondatum", "Laatste coupondatum");

            actualEmailText.Should().Contain($"Offerte Kasgeldlening ({lalCaseNumber})");
            actualTable.Should().Contain($"Geldnemer\t{shortCaseOfferData.CounterParty}");
            actualTable.Should().Contain($"Hoofdsom (in eur)\t{shortCaseOfferData.Notional}");
            actualTable.Should().Contain($"Ingangsdatum\t{formattedStartDate}");
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDate}");
            actualTable.Should().Contain($"Rente%\t{pricefinalTransformed}");
            actualTable.Should().Contain($"Renteconventie\tAct/360");

            if (shortAltData.DealRemark != null && shortAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{shortAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
            }

            if (sICCode == "0031000000")
            {
                actualTable.Should().Contain($"Zekerheid\tWaarborgfonds voor de Zorgsector");
            }
            else
            {
                if (shortCaseOfferData.Guarantor1 != null && shortCaseOfferData.Guarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{shortCaseOfferData.Guarantor1}");
                }
                else
                {
                    actualTable.Should().NotContain($"Zekerheid");
                }
            }

            Console.WriteLine($"PRINTING ACTUAL TABLE BELOW FOR SHORT CASE {lalCaseNumber}:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualTable);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }

        [Then("the displayed short email text should match the expected text")]
        public async Task ThenTheDisplayedShortEmailTextShouldMatchTheExpectedText()
        {
            var varCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            Console.WriteLine($"PRINTING ENTIRE EMAIL TEXT BELOW FOR CASE {lalCaseNumber}:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualEmailText);
            Console.WriteLine("----------------------------------------------------------------------------------");

            if (sICCode == "0031000000")
            {
                // WFZ
                actualEmailText.Should().Contain("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn." +
                    " Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
                actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen.");
            }
            else if (sICCode == "0023000000")
            {
                // Waterschap
                actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Deze e-mail, uw acceptatie middels een reply op deze e-mail, de Algemene Voorwaarden NWB Bank" +
                    " en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) vormen tezamen een leningovereenkomst.");
            }
            else
            {
                // Normal template
                actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer" +
                    " te vermelden waarop het door u gevraagde bedrag dient te worden overgemaakt. Deze e-mail, uw acceptatie middels een reply op deze e-mail, de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023)" +
                    " vormen tezamen een leningovereenkomst.");
                actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");

                if (cifNumber == "0636960641" || cifNumber == "0636914607" || cifNumber == "0636914739")
                {
                    if (cifNumber == "0636960641")
                    {
                        actualEmailText.Should().Contain("Verwezen wordt naar (A) de overeenkomst kasgeldfaciliteit tussen de NWB Bank als geldgever en N.V. HVC (HVC) als geldnemer oorspronkelijk gedateerd 22 mei 2014 (Overeenkomst Kasgeldfaciliteit); en (B) de ballotage-overeenkomst van de aandeelhouders A van HVC oorspronkelijk gedateerd oktober 2002 (Ballotageovereenkomst A)");
                        actualEmailText.Should().Contain("(i) HVC verklaart dat de te verstrekken kasgeldlening onder de Overeenkomst Kasgeldfaciliteit valt binnen het door aandeelhouders laatst vastgestelde garantstellingsplafond.");
                        actualEmailText.Should().Contain("(ii) HVC verklaart dat de Ballotageovereenkomst A met het daarin in artikel 9 lid 2 opgenomen derdenbeding ongewijzigd en onverminderd van kracht is (welk derdenbeding door de NWB Bank ook aanvaard is), zodat voor iedere aandeelhouder A van HVC een hoofdelijke garantstelling geldt jegens de NWB Bank voor de juiste en volledige nakoming van alle betalingsverplichtingen van HVC (dan wel haar rechtsopvolger(s)) uit hoofde van de Overeenkomst Kasgeldfaciliteit.");
                        actualEmailText.Should().Contain("Bovenstaande verklaringen worden door HVC herhaald op het moment van opname van de te verstrekken kasgeldlening.");
                    }
                    else if (cifNumber == "0636914607")
                    {
                        actualEmailText.Should().Contain("NWB Bank N.V. - in haar hoedanigheid als Geldgever (zoals gedefinieerd in de relevante VSPN Uitvoeringsovereenkomst(en)) - verklaart reeds nu voor alsdan de onherroepelijke en onvoorwaardelijke garantie van een (of meer) van de twaalf provincies uit hoofde van art. 5.1 van de relevante VSPN Uitvoeringsovereenkomst(en) te aanvaarden.");
                        actualEmailText.Should().Contain("Verwezen wordt naar (A) de overeenkomsten tot uitvoering van de voorfinanciering subsidie particulier natuurbeheer, zoals oorspronkelijk in 2007 respectievelijk 2008 overeengekomen tussen Stichting Groenfonds en elk van de twaalf provincies (VSPN Uitvoeringsovereenkomsten); en (B) de uitvoeringsovereenkomst 2002 overeengekomen tussen de Staat der Nederlanden, de provincies en Stichting Groenfonds (Uitvoeringsovereenkomst 2002).");
                        actualEmailText.Should().Contain("(i) Stichting Groenfonds verklaart dat de te verstrekken kasgeldlening een lening is als bedoeld in art. 4.1 van de relevante VSPN Uitvoeringsovereenkomst(en) en dat deze email in combinatie met de bevestigingsmail van NWB Bank N.V. de leningsovereenkomst omvat met Stichting Groenfonds als geldnemer en NWB Bank N.V. als Geldgever (zoals gedefinieerd in de relevante VSPN Uitvoeringsovereenkomst(en) en in de zin van de in art. 5.1 opgenomen garantie).");
                        actualEmailText.Should().Contain("(ii) Stichting Groenfonds verklaart dat de te verstrekken kasgeldlening wordt gegarandeerd krachtens de onherroepelijke en onvoorwaardelijke garantie van een (of meer) van de twaalf provincies uit hoofde van art. 5.1 van de relevante VSPN Uitvoeringsovereenkomst(en), welke garantie NWB Bank N.V. in haar hoedanigheid als Geldgever (zoals gedefinieerd in de relevante VSPN Uitvoeringsovereenkomst(en)) heeft aanvaard.");
                        actualEmailText.Should().Contain("(iii) Stichting Groenfonds verklaart gehandeld te hebben en te handelen in overeenstemming met art. 5 en 6 van de Uitvoeringsovereenkomst 2002 en voorts niet in verzuim te zijn met betrekking tot haar verplichtingen op grond van de VSPN Uitvoeringsovereenkomst(en) en Uitvoeringsovereenkomt 2002.");
                        actualEmailText.Should().Contain("(iv) Stichting Groenfonds verklaart dat de relevante VSPN Uitvoeringsovereenkomsten en de Uitvoeringsovereenkomst 2002 ongewijzigd en onverminderd van kracht zijn.");
                        actualEmailText.Should().Contain("Bovenstaande verklaringen worden door Stichting Groenfonds herhaald op het moment van opname van de te verstrekken kasgeldlening.");
                    }
                    else if (cifNumber == "0636914739")
                    {
                        actualEmailText.Should().Contain("NWB Bank N.V. - in haar hoedanigheid als geldgever - verklaart reeds nu voor alsdan de garantie van de Staat uit hoofde van art. 12 van de Achterborgovereenkomst te aanvaarden.");
                        actualEmailText.Should().Contain("Verwezen wordt naar de achterborgovereenkomst tussen de Staat der Nederlanden en Stichting Nationaal Restauratiefonds (NRF) oorspronkelijk gedateerd 12 juni 1996 en zoals gewijzigd op 14 september 2010 (Achterborgovereenkomst).");
                        actualEmailText.Should().Contain("(i) NRF verklaart dat de te verstrekken kasgeldlening een lening is als bedoeld in art. 1 van de Achterborgovereenkomst en dat deze email in combinatie met de bevestigingsmail van NWB Bank N.V. de leningsovereenkomst omvat met NRF als geldnemer en NWB Bank N.V. als geldgever.");
                        actualEmailText.Should().Contain("(ii) NRF verklaart dat de te verstrekken kasgeldlening wordt gegarandeerd krachtens de garantie van de Staat uit hoofde van art. 12.1 van de Achterborgovereenkomst, welke garantie NWB Bank N.V. in haar hoedanigheid als geldgever heeft aanvaard.");
                        actualEmailText.Should().Contain("(iii) NRF verklaart gehandeld te hebben en te handelen in overeenstemming met art. 5 en 6 van de Achterborgovereenkomst en voorts niet in verzuim te zijn met betrekking tot haar verplichtingen op grond van de Achterborgovereenkomst.");
                        actualEmailText.Should().Contain("(iv) NRF verklaart dat de Achterborgovereenkomst ongewijzigd en onverminderd van kracht is.");
                        actualEmailText.Should().Contain("Bovenstaande verklaringen worden door NRF herhaald op het moment van opname van de te verstrekken kasgeldlening.");
                    }
                }
            }

            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.");

            actualEmailText.Should().Contain("Bovenstaande is onder voorbehoud van typefouten.");
            actualEmailText.Should().Contain("Met vriendelijke groet,");

            actualEmailText.Should().Contain("Nederlandse Waterschapsbank N.V.");
            actualEmailText.Should().Contain("Rooseveltplantsoen 3");
            actualEmailText.Should().Contain("2517 KR Den Haag");
            actualEmailText.Should().Contain("t");
            actualEmailText.Should().Contain("e pf@nwbbank.com");
            actualEmailText.Should().Contain("w www.nwbbank.com");
        }
    }
}