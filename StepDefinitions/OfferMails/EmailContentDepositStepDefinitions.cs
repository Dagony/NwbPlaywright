using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class EmailContentDepositStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IFixedDataTransforms _fixedDataTransforms;
        private readonly IShortModel _shortModel;
        private readonly ISharedModel _sharedModel;
        private readonly IPlaywrightDriver _playwrightDriver;

        public EmailContentDepositStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ISharedDataTransforms sharedDataTransforms,
            IFixedDataTransforms fixedDataTransforms,
            IShortModel shortModel,
            ISharedModel sharedModel)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _winLostCorrectObject = winLostCorrectObject;
            _billOfClosureObject = billOfClosureObject;
            _sharedDataTransforms = sharedDataTransforms;
            _fixedDataTransforms = fixedDataTransforms;
            _shortModel = shortModel;
            _sharedModel = sharedModel;
        }

        [Given("I create a deposit loan with the following case and offer specifics")]
        public async Task GivenICreateADepositLoanWithTheFollowingCaseAndOfferSpecifics(DataTable table)
        {
            ShortLoanCaseOfferFields depositOfferData = table.CreateInstance<ShortLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = depositOfferData;

            // geen guarantor sectie bij deposito.
            _scenarioContext["savedGuarantor1"] = string.Empty;
            string setCDDDate = _sharedDataTransforms.GetDateYesterday();

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = depositOfferData.TC;
            _scenarioContext["savedNotional"] = depositOfferData.Notional;

            await _sharedModel.LoanUpdateCaseSetOfferFields(depositOfferData.DealMoment);
            await _loanUpdateFinalSpreadObject.SetNotional(depositOfferData.Notional.Replace(".", string.Empty));
        }

        [Given("I create a deposit loan with the following alternative specifics")]
        public async Task GivenICreateADepositLoanWithTheFollowingAlternativeSpecifics(DataTable table)
        {
            ShortLoanAlternativeFields alternativeDepositData = table.CreateInstance<ShortLoanAlternativeFields>();
            _scenarioContext["savedAltData"] = alternativeDepositData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();

            if (alternativeDepositData.StartDate == "today")
            {
                await _shortModel.LoanUpdateShortCaseSetDateFields(currentFormattedDate, alternativeDepositData.MaturityDate);
            }
            else
            {
                await _shortModel.LoanUpdateShortCaseSetDateFields(alternativeDepositData.StartDate, alternativeDepositData.MaturityDate);
            }

            await _shortModel.ShortCaseSetSpreadFieldsTwoEyes(alternativeDepositData.PriceH1, alternativeDepositData.PriceFinal, alternativeDepositData.Margin, "D", lalCaseNumber, true);

            if (alternativeDepositData.DealRemark != null && alternativeDepositData.DealRemark != string.Empty)
            {
                await _finalSpreadObject.SetDealRemark(alternativeDepositData.DealRemark);
            }
        }

        [Then("the deposit offer email table should match the expected table")]
        public async Task ThenTheDepositOfferEmailTableShouldMatchTheExpectedTable()
        {
            var depositCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var depositAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualHeader = (string)_scenarioContext["savedActualAddressees"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var actualTableENG = (string)_scenarioContext["savedActualTableENG"];
            var actualNotional = decimal.Parse(depositCaseOfferData.Notional, new CultureInfo("nl-NL")).ToString("N2", new CultureInfo("nl-NL"));

            // var pricefinalTransformed = await _basePageObject.TransformSpreadDataToStringWithThreeDecimalPlaces(DepositAltData.PriceFinal);
            var (formattedStartDateNL, formattedMaturityDateNL) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, depositAltData.StartDate, depositAltData.MaturityDate);
            var (formattedStartDateENG, formattedMaturityDateENG) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.ENG, depositAltData.StartDate, depositAltData.MaturityDate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            actualTable.Should().NotContainAny("Aflossing", "Aantal Aflossingen", "Aflossingsfrequentie", "Variabel Rentende Lening", "Variabele Hoofdsom Lening", "Euribor", "Kredietopslag (in bps)", "48uurs clausule", "Bereidstellingsprovisie (bps)", "Renteherziening", "Spreadherzieningsdatum", "Eerste coupondatum", "Laatste coupondatum", "Ingangsdatum");

            actualHeader.Should().Contain($"Offerte Deposito ({lalCaseNumber})");

            actualTable.Should().Contain($"Offerte Deposito ({lalCaseNumber})", Exactly.Once());
            actualTable.Should().Contain($"Offer Deposit ({lalCaseNumber})", Exactly.Once());
            actualTable.Should().Contain($"Geldgever\t{depositCaseOfferData.CounterParty}", Exactly.Once());
            actualTable.Should().Contain($"Hoofdsom (in eur)\t{actualNotional}", Exactly.Once());
            actualTable.Should().Contain($"Stortingsdatum\t{formattedStartDateNL}", Exactly.Once());
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDateNL}", Exactly.Once());
            actualTable.Should().Contain($"Rente%\t{depositAltData.PriceFinal}", Exactly.Once());
            actualTable.Should().Contain($"Renteconventie\tAct/360", Exactly.Once());

            actualTableENG.Should().Contain($"Counterparty\t{depositCaseOfferData.CounterParty}", Exactly.Once());
            actualTableENG.Should().Contain($"Notional (in eur)\t{actualNotional}", Exactly.Once());
            actualTableENG.Should().Contain($"Start date\t{formattedStartDateENG}", Exactly.Once());
            actualTableENG.Should().Contain($"End date\t{formattedMaturityDateENG}", Exactly.Once());
            actualTableENG.Should().Contain($"Interest rate%\t{depositAltData.PriceFinal}", Exactly.Once());
            actualTableENG.Should().Contain($"Day count convention\tAct/360", Exactly.Once());

            if (depositAltData.DealRemark != null && depositAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{depositAltData.DealRemark}");
                actualTableENG.Should().Contain($"Remarks\t{depositAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
                actualTableENG.Should().NotContain($"Remarks");
            }
        }

        [Then("the displayed deposit email text should match the expected text")]
        public async Task ThenTheDisplayedDepositEmailTextShouldMatchTheExpectedText()
        {
            var depositCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var depositAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];

            EnumsForTRX.TestUser testUser = (EnumsForTRX.TestUser)_scenarioContext["savedLoggedInTestUser"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            Console.WriteLine($"PRINTING ENTIRE EMAIL TEXT BELOW FOR DEPOSIT CASE {lalCaseNumber}:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualEmailText);
            Console.WriteLine("----------------------------------------------------------------------------------");

            actualEmailText.Should().Contain("Bovenstaande is onder voorbehoud van typefouten.", Exactly.Once());
            actualEmailText.Should().Contain("The above is true under the reservation of typos.", Exactly.Once());

            actualEmailText.Should().Contain("Met vriendelijke groet,", Exactly.Once());
            actualEmailText.Should().Contain("Kind regards,", Exactly.Once());

            if (testUser == TestUser.AccountDesk1 || testUser == TestUser.AccountDesk2)
            {
                actualEmailText.Should().Contain("Public Finance", Exactly.Twice());
                actualEmailText.Should().NotContain("NWB Treasury Team");
                actualEmailText.Should().Contain("pf@nwbbank.com", Exactly.Twice());
                actualEmailText.Should().NotContain("treasury@nwbbank.com");
            }
            else if (testUser == TestUser.Treasury1 || testUser == TestUser.Treasury2 || testUser == TestUser.TreasuryManager1 || testUser == TestUser.TreasuryManager2)
            {
                actualEmailText.Should().Contain("NWB Treasury Team", Exactly.Twice());
                actualEmailText.Should().NotContain("Public Finance");
                actualEmailText.Should().Contain("treasury@nwbbank.com", Exactly.Twice());
                actualEmailText.Should().NotContain("pf@nwbbank.com");
            }

            actualEmailText.Should().Contain("Nederlandse Waterschapsbank N.V.", Exactly.Twice());
            actualEmailText.Should().Contain("Rooseveltplantsoen 3", Exactly.Twice());
            actualEmailText.Should().Contain("2517 KR Den Haag", Exactly.Once());
            actualEmailText.Should().Contain("2517 KR The Hague", Exactly.Once());
            actualEmailText.Should().Contain("P.O. Box 580 2501 CN Den Haag", Exactly.Once());
            actualEmailText.Should().Contain("P.O. Box 580 2501 CN The Hague", Exactly.Once());
            actualEmailText.Should().Contain("t");

            actualEmailText.Should().Contain("w www.nwbbank.com", Exactly.Twice());

            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.", Exactly.Once());
            actualEmailText.Should().Contain("Op dit deposito zullen de Algemene Voorwaarden NWB Bank van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.", Exactly.Once());
            actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop wij de hoofdsom inclusief rentevergoeding aan het einde van de looptijd terug mogen storten. Deze e-mail, uw acceptatie middels een reply op deze e-mail en de Algemene Voorwaarden vormen tezamen een transactie overeenkomst.", Exactly.Once());
            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. Bovenstaande is onder voorbehoud van typefouten.", Exactly.Once());

            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.", Exactly.Once());

            actualEmailText.Should().Contain("Please let us know if you wish to proceed with these terms.", Exactly.Once());
            actualEmailText.Should().Contain("The General Terms and Conditions of NWB Bank (available at https://nwbbank.com/public-finance/algemene-voorwaarden) and the General Credit Conditions of NWB Bank, which are attached to this email, will apply to the deposit.", Exactly.Once());
            actualEmailText.Should().Contain("Should you accept our offer, the transaction will be finalized. To confirm your acceptance, please reply to this email and include the bank account number to which the principal amount, along with the accrued interest, should be transferred. This email, your acceptance by replying, and the General Terms and Conditions together form the transaction agreement.", Exactly.Once());
            actualEmailText.Should().Contain("Please note that NWB Bank does not intend to provide any advice or recommendation through this email. You are responsible for making your own considerations (legal, financial, or otherwise) regarding this transaction and for obtaining professional advice if necessary. The above is true under the reservation of typos.", Exactly.Once());

            // ActualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.", Exactly.Once());
            // ActualEmailText.Should().Contain("Op dit deposito zullen de Algemene Voorwaarden NWB Bank van toepassing zijn (deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden) en de Algemene Kredietvoorwaarden NWB Bank, welke bij deze e-mail zijn aangehecht.", Exactly.Once());
            //// ActualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop wij de hoofdsom inclusief rentevergoeding mogen overmaken. Deze e-mail, uw acceptatie middels een reply op deze e-mail en de Algemene Voorwaarden vormen tezamen een transactie overeenkomst.", Exactly.Once());
            // ActualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop wij de hoofdsom inclusief rentevergoeding aan het einde van de looptijd terug mogen storten. Deze e-mail, uw acceptatie middels een reply op deze e-mail en de Algemene Voorwaarden vormen tezamen een transactie overeenkomst.", Exactly.Once());

            // ActualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.", Exactly.Once());

            // ActualEmailText.Should().Contain("Please let us know if you wish to proceed with these terms.", Exactly.Once());
            // ActualEmailText.Should().Contain("The General Terms and Conditions of NWB Bank (available at https://nwbbank.com/public-finance/algemene-voorwaarden) and the General Credit Conditions of NWB Bank, which are attached to this email, will apply to the deposit.", Exactly.Once());
            // ActualEmailText.Should().Contain("Should you accept our offer, the transaction will be finalized. To confirm your acceptance, please reply to this email and include the bank account number to which the principal amount, along with the accrued interest, should be transferred. This email, your acceptance by replying, and the General Terms and Conditions together form the transaction agreement.", Exactly.Once());

            // ActualEmailText.Should().Contain("Please note that NWB Bank does not intend to provide any advice or recommendation through this email. You are responsible for making your own considerations (legal, financial, or otherwise) regarding this transaction and for obtaining professional advice if necessary.", Exactly.Once());
        }

        [Then("offerte attachment should not be visible")]
        public async Task ThenOfferteAttachmentShouldNotBeVisible()
        {
            throw new PendingStepException();
        }
    }
}