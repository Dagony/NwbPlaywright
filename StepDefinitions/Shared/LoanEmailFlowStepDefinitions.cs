using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
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
using DataTable = Reqnroll.DataTable;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class LoanEmailFlowStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly ISharedModel _sharedModel;
        private readonly IFixedModel _fixedModel;
        private readonly IVariableModel _variableModel;
        private readonly IShortModel _shortModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IPlaywrightDriver _playwrightDriver;

        public LoanEmailFlowStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject, ISharedModel sharedModel, IFixedModel fixedModel, IVariableModel variableModel, IShortModel shortModel, ISharedDataTransforms sharedDataTransforms)
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
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedModel = sharedModel;
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _shortModel = shortModel;
            _sharedDataTransforms = sharedDataTransforms;
        }

        [Given("I create a variable case with one offer with two alternatives")]
        public async Task GivenICreateAVariableCaseWithOneOfferWithTwoAlternatives()
        {
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Fixed);
            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 20, false);
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 25000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-01-2025";
            var lastCouponDate = "20-01-2025";
            var interestRRDate = "20-01-2026";
            var couponDay = "5";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;
            _scenarioContext["savedTC"] = "TC03 Variable Double Offer Table";

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, notional);
            await _variableModel.VariableCaseSetDateFields(currentFormattedDate, formattedMaturityDate, "12mE", firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: couponDay);

            await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, "Annual", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay, offerNumbInCase: 1, alternativeNumbIncase: 1);
            _scenarioContext["savedStartDate"] = currentFormattedDate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for Multiple offers case: {lalCaseNumber}");

            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(1, 1);
            await Task.Delay(1000);

            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: 1, alternativeNumbIncase: i);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes("1", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            // LOGIN AD2 //
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await _basePageObject.OpenCreateLoanRequestLink();

            await Task.Delay(3000);

            // SET H2 //
            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{10 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            // SET MULTIPLE Spread MisMatches //
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "11", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 1, indexOfSpreadMismatch: 0);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "12", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 2, indexOfSpreadMismatch: 1);

            // SET FINAL + Marketrates//
            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }
        }

        [Given("I create a fixed case with one offer with two alternatives")]
        public async Task GivenICreateAFixedCaseWithOneOfferWithTwoAlternatives()
        {
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();

            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 20, false);
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 25000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-06-2027";
            var lastCouponDate = "21-06-2027";
            var interestRRDate = "21-01-2028";
            var couponDay = "5";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;
            _scenarioContext["savedTC"] = "TC03 Fixed Double Offer Table";

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, notional);

            await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, "Annual", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay, offerNumbInCase: 1, alternativeNumbIncase: 1);
            _scenarioContext["savedStartDate"] = currentFormattedDate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for TC03 Fixed Offer double table: {lalCaseNumber}");
            await _finalSpreadObject.SetSwap(false);

            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(1, 1);
            await Task.Delay(1000);

            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: 1, alternativeNumbIncase: i);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes("1", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            await _finalSpreadObject.ClickSaveOffer();

            // LOGIN AD2 //
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await Task.Delay(3000);

            // SET H2 //
            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{10 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            // SET MULTIPLE Spread MisMatches //
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "11", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 1, indexOfSpreadMismatch: 1);
            await Task.Delay(800);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "12", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 2, indexOfSpreadMismatch: 2);
            await Task.Delay(800);

            // SET FINAL + Marketrates//
            for (int i = 1; i < 3;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }
        }

        [Then("the fixed offer email table should include two tables")]
        public async Task ThenTheFixedOfferEmailTableShouldIncludeTwoTables()
        {
            Console.WriteLine("TEST DONE");
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[15].GetValue(fieldsObject).ToString();

            actualEmailText.Should().Contain($"Geldnemer", Exactly.Twice());
            actualEmailText.Should().Contain($"Hoofdsom (in eur)", Exactly.Twice());
            actualEmailText.Should().Contain($"Ingangsdatum", Exactly.Twice());
            actualEmailText.Should().Contain($"Einddatum", Exactly.Twice());
            actualEmailText.Should().Contain($"Renteherziening", Exactly.Twice());
            actualEmailText.Should().Contain($"Coupondag", Exactly.Twice());
            actualEmailText.Should().Contain($"Eerste coupondatum", Exactly.Twice());
            actualEmailText.Should().Contain($"Laatste coupondatum", Exactly.Twice());
            actualEmailText.Should().Contain($"Aflossing", Exactly.Twice());
            actualEmailText.Should().Contain($"Rente%", Exactly.Twice());
            actualEmailText.Should().Contain($"Renteconventie", Exactly.Twice());
            actualEmailText.Should().Contain($"Rentefrequentie", Exactly.Twice());

            if (sicCode == "0030000000")
            {
                actualEmailText.Should().Contain($"Zekerheid\tStichting Waarborgfonds Sociale Woningbouw", Exactly.Twice());
            }
            else if (sicCode == "0031000000")
            {
                actualEmailText.Should().Contain($"Zekerheid\tStichting Waarborgfonds voor de Zorgsector", Exactly.Twice());
            }

            Console.WriteLine($"PRINTING ENTIRE EMAIL TEXT BELOW FOR CASE {lalCaseNumber}:");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualEmailText);
            Console.WriteLine("----------------------------------------------------------------------------------");
        }

        [Given("I create a case with three offers with each {int} alternatives")]
        public async Task GivenICreateACaseWithThreeOffersWithEachAlternatives(int x)
        {
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();

            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 20, false);
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 25000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-01-2026";
            var lastCouponDate = "20-01-2026";
            var interestRRDate = "20-01-2027";
            var couponDay = "5";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, notional);

            await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, "Annual", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay, offerNumbInCase: 1, alternativeNumbIncase: 1);
            _scenarioContext["savedStartDate"] = currentFormattedDate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for Multiple offers case: {lalCaseNumber}");

            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await _finalSpreadObject.ClickAddOffer(2);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(1, 1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(1, 1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(2, 1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(2, 1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(3, 1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddAlternative(3, 1);
            await Task.Delay(1000);

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: 1, alternativeNumbIncase: i);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes("1", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: 2, alternativeNumbIncase: i);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes("2", "LONG", lalCaseNumber, true, offerNumbInCase: 2, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: 3, alternativeNumbIncase: i);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes("3", "LONG", lalCaseNumber, true, offerNumbInCase: 3, alternativeNumbIncase: i);
                i++;
            }

            // LOGIN AD2 //
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await _basePageObject.OpenCreateLoanRequestLink();

            await Task.Delay(3000);

            // SET H2 //
            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{10 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{20 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 2, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{30 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 3, alternativeNumbIncase: i);
                i++;
            }

            // SET MULTIPLE Spread MisMatches //
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "11", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 1, indexOfSpreadMismatch: 0);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "12", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 2, indexOfSpreadMismatch: 1);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "13", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 3, indexOfSpreadMismatch: 2);

            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "21", "LONG", lalCaseNumber, offerNumbInCase: 2, alternativeNumbIncase: 1, indexOfSpreadMismatch: 3);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "22", "LONG", lalCaseNumber, offerNumbInCase: 2, alternativeNumbIncase: 2, indexOfSpreadMismatch: 4);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "23", "LONG", lalCaseNumber, offerNumbInCase: 2, alternativeNumbIncase: 3, indexOfSpreadMismatch: 5);

            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "31", "LONG", lalCaseNumber, offerNumbInCase: 3, alternativeNumbIncase: 1, indexOfSpreadMismatch: 6);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "32", "LONG", lalCaseNumber, offerNumbInCase: 3, alternativeNumbIncase: 2, indexOfSpreadMismatch: 7);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "33", "LONG", lalCaseNumber, offerNumbInCase: 3, alternativeNumbIncase: 3, indexOfSpreadMismatch: 8);

            // SET FINAL + Marketrates//
            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{i}", "LONG", lalCaseNumber, true, offerNumbInCase: 1, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{3 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 2, alternativeNumbIncase: i);
                i++;
            }

            for (int i = 1; i < 4;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{6 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: 3, alternativeNumbIncase: i);
                i++;
            }
        }

        [Given("I create six offers and chose the following actions")]
        public async Task GivenICreateSixOffersAndChoseTheFollowingActions()
        {
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            string currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();

            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(currentFormattedDate, 20, false);
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 25000);
            var notional = notionalWithoutDouble + ",99";
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var firstCouponDate = "14-01-2026";
            var lastCouponDate = "20-01-2026";
            var interestRRDate = "20-01-2027";
            var couponDay = "5";

            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, notional);
            await _fixedModel.FixedCaseSetDateFields(currentFormattedDate, formattedMaturityDate, "Annual", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay, offerNumbInCase: 1, alternativeNumbIncase: 1);
            _scenarioContext["savedStartDate"] = currentFormattedDate;

            await _finalSpreadObject.SpreadCheckBy(true);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Spreadcheck message for Multiple offers case: {lalCaseNumber}");

            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await Task.Delay(1000);
            await _finalSpreadObject.ClickAddOffer(1);
            await Task.Delay(1000);

            for (int i = 1; i < 7;)
            {
                await _fixedModel.FixedCaseSetAlternativeFields("Act/Act", false, "Bullet", offerNumbInCase: i, alternativeNumbIncase: 1);
                await _fixedModel.FixedCaseSetMultipleH1FourEyes(i.ToString(), "LONG", lalCaseNumber, true, offerNumbInCase: i, alternativeNumbIncase: 1);

                i++;
            }

            // LOGIN AD2 //
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await _basePageObject.OpenCreateLoanRequestLink();

            await Task.Delay(3000);

            // SET H2 //
            for (int i = 1; i < 7;)
            {
                await _fixedModel.FixedCaseSetMultipleH2FourEyes($"{10 + i}", "LONG", lalCaseNumber, true, offerNumbInCase: i, alternativeNumbIncase: 1);
                i++;
            }

            // SET MULTIPLE Spread MisMatches //
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("1", "11", "LONG", lalCaseNumber, offerNumbInCase: 1, alternativeNumbIncase: 1, indexOfSpreadMismatch: 0);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("2", "21", "LONG", lalCaseNumber, offerNumbInCase: 2, alternativeNumbIncase: 1, indexOfSpreadMismatch: 1);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("3", "31", "LONG", lalCaseNumber, offerNumbInCase: 3, alternativeNumbIncase: 1, indexOfSpreadMismatch: 2);

            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("4", "41", "LONG", lalCaseNumber, offerNumbInCase: 4, alternativeNumbIncase: 1, indexOfSpreadMismatch: 3);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("5", "51", "LONG", lalCaseNumber, offerNumbInCase: 5, alternativeNumbIncase: 1, indexOfSpreadMismatch: 4);
            await _fixedModel.FixedCaseSetMultipleSpreadMismatches("6", "61", "LONG", lalCaseNumber, offerNumbInCase: 6, alternativeNumbIncase: 1, indexOfSpreadMismatch: 5);

            // SET FINAL + Marketrates//
            for (int i = 1; i < 7;)
            {
                await _fixedModel.FixedCaseSetMultipleFinalFourEyes("4", $"{i}", "LONG", lalCaseNumber, true, offerNumbInCase: i, alternativeNumbIncase: 1);
                i++;
            }
        }

        [Then("In WinLostCorrect a total of {string} offer emails are sent out")]
        public async Task ThenInWinLostCorrectATotalOfOfferEmailsAreSentOut(string expectedNumbOfEmailsSent)
        {
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();

            var currentNumberOfMailsSentOutOnPage = await _winLostCorrectObject.GetNumberOfAttachmentsOnPage();
            currentNumberOfMailsSentOutOnPage.Should().ContainEquivalentOf($"({expectedNumbOfEmailsSent})");
        }

        [When("I navigate to the Collect Offer screen and chose for the six instances:")]
        public async Task WhenINavigateToTheCollectOfferScreenAndChoseForTheSixInstances(Table table)
        {
            CollectOfferFields casedata = table.CreateInstance<CollectOfferFields>();
            _scenarioContext["caseData"] = casedata;

            var index = 1;
            foreach (var property in casedata.GetType().GetProperties())
            {
                var value = property.GetValue(casedata) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    await _winLostCorrectObject.ClickWinLostCorrect(value, index);
                    index++;
                }
            }

            await Task.Delay(500);
            await _winLostCorrectObject.UploadDocuments();
        }

        [When("I navigate to the Collect Offer screen and chose for the nine instances:")]
        public async Task WhenINavigateToTheCollectOfferScreenAndChoseForTheNineInstances(DataTable dataTable)
        {
            CollectOfferFields casedata = dataTable.CreateInstance<CollectOfferFields>();
            _scenarioContext["caseData"] = casedata;

            var index = 1;
            foreach (var property in casedata.GetType().GetProperties())
            {
                var value = property.GetValue(casedata) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    await _winLostCorrectObject.ClickWinLostCorrect(value, index);
                    index++;
                }
            }

            await Task.Delay(500);
            await _winLostCorrectObject.UploadDocuments();
        }

        [Then("only the alternatives which are corrected are turned on for send email")]
        public async Task ThenOnlyTheAlternativesWhichAreCorrectedAreTurnedOnForSendEmail()
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(3000);
            await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]").GetByRole(AriaRole.Cell, new() { Name = lalCaseNumber, Exact = true }).First.ClickAsync();

            await _basePageObject.OpenCreateLoanRequestLink();

            var firstOfferCheckBox = await _includeInMailBoxLocator.Nth(0).IsCheckedAsync();
            var secondOfferCheckBox = await _includeInMailBoxLocator.Nth(1).IsCheckedAsync();
            var thirdOfferCheckBox = await _includeInMailBoxLocator.Nth(2).IsCheckedAsync();
            var fourthOfferCheckBox = await _includeInMailBoxLocator.Nth(3).IsCheckedAsync();
            var fifthOfferCheckBox = await _includeInMailBoxLocator.Nth(4).IsCheckedAsync();
            var sixthOfferCheckBox = await _includeInMailBoxLocator.Nth(5).IsCheckedAsync();
            var seventhOfferCheckBox = await _includeInMailBoxLocator.Nth(6).IsCheckedAsync();
            var eighthOfferCheckBox = await _includeInMailBoxLocator.Nth(7).IsCheckedAsync();
            var ninethOfferCheckBox = await _includeInMailBoxLocator.Nth(8).IsCheckedAsync();

            firstOfferCheckBox.Should().BeFalse();
            secondOfferCheckBox.Should().BeFalse();
            thirdOfferCheckBox.Should().BeTrue();
            fourthOfferCheckBox.Should().BeFalse();
            fifthOfferCheckBox.Should().BeFalse();
            sixthOfferCheckBox.Should().BeTrue();
            seventhOfferCheckBox.Should().BeFalse();
            eighthOfferCheckBox.Should().BeFalse();
            ninethOfferCheckBox.Should().BeTrue();
        }

        [Then(@"only the offers which are corrected are turned on for send email")]
        public async Task ThenOnlyTheOffersWhichAreCorrectedAreTurnedOnForSendEmail()
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(3000);
            await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]").GetByRole(AriaRole.Cell, new() { Name = lalCaseNumber, Exact = true }).First.ClickAsync();

            await _basePageObject.OpenCreateLoanRequestLink();

            var firstOfferCheckBox = await _includeInMailBoxLocator.Nth(0).IsCheckedAsync();
            var secondOfferCheckBox = await _includeInMailBoxLocator.Nth(1).IsCheckedAsync();
            var thirdOfferCheckBox = await _includeInMailBoxLocator.Nth(2).IsCheckedAsync();
            var fourthOfferCheckBox = await _includeInMailBoxLocator.Nth(3).IsCheckedAsync();
            var fifthOfferCheckBox = await _includeInMailBoxLocator.Nth(4).IsCheckedAsync();

            firstOfferCheckBox.Should().BeFalse();
            secondOfferCheckBox.Should().BeFalse();
            thirdOfferCheckBox.Should().BeTrue();
            fourthOfferCheckBox.Should().BeFalse();
            fifthOfferCheckBox.Should().BeFalse();
        }

        [Then("One Offer is returned to Finalspread and the checkbox includeInMail is checked")]
        public async Task ThenOneOfferIsReturnedToFinalspreadAndTheCheckboxIncludeInMailIsChecked()
        {
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            await _basePageObject.OpenCaseOnScreen(lalCaseNumber);
            await _basePageObject.OpenCreateLoanRequestLink();

            var firstOfferCheckBox = await _includeInMailBoxLocator.Nth(0).IsCheckedAsync();
            firstOfferCheckBox.Should().BeTrue();
        }

        [Then("three offers are returned to Finalspread and the checkbox includeInMail is checked")]
        public async Task ThenThreeOffersAreReturnedToFinalspreadAndTheCheckboxincludeInMailIsChecked()
        {
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            await _basePageObject.OpenCaseOnScreen(lalCaseNumber);

            await _basePageObject.OpenCreateLoanRequestLink();

            var firstOfferCheckBox = await _includeInMailBoxLocator.Nth(0).IsCheckedAsync();
            var secondOfferCheckBox = await _includeInMailBoxLocator.Nth(1).IsCheckedAsync();
            var thirdOfferCheckBox = await _includeInMailBoxLocator.Nth(2).IsCheckedAsync();
            var fourthOfferCheckBoxVisible = await _includeInMailBoxLocator.Nth(3).IsHiddenAsync();
            var fifthOfferCheckBoxVisible = await _includeInMailBoxLocator.Nth(4).IsHiddenAsync();
            var sixthOfferCheckBoxVisible = await _includeInMailBoxLocator.Nth(5).IsHiddenAsync();
            firstOfferCheckBox.Should().BeTrue();
            secondOfferCheckBox.Should().BeTrue();
            thirdOfferCheckBox.Should().BeTrue();
            fourthOfferCheckBoxVisible.Should().BeTrue();
            fifthOfferCheckBoxVisible.Should().BeTrue();
            sixthOfferCheckBoxVisible.Should().BeTrue();
        }

        [Then(@"only offer three and four are returned to FinalSpread and the checkbox include in mail is checked")]
        public async Task ThenOnlyOfferThreeAndFourAreReturnedToFinalSpreadAndTheCheckboxIncludeInMailIsChecked()
        {
            var firstRejectedOfferCheckBox = await _includeInMailBoxLocator.First.IsCheckedAsync();
            var secondRejectedOfferCheckBox = await _includeInMailBoxLocator.Nth(1).IsCheckedAsync();

            firstRejectedOfferCheckBox.Should().Be(true);
            secondRejectedOfferCheckBox.Should().Be(true);
        }

        [Given(@"I create random offer of type ""([^""]*)""")]
        public async Task GivenICreateRandomOfferOfType(string productType)
        {
            var startDate = "04-01-2030";
            var firstCouponDate = "07-01-2030";
            var lastCouponDate = "11-01-2030";
            var interestRRDate = "04-01-2040";
            var couponDay = "22";
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            var notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            var notional = notionalWithoutDouble + ",95";
            Task<string> stringTask = productType switch
            {
                "Traditional fixed rate loan long" => _sharedDataTransforms.CalculateFormattedMaturityDate(startDate, 40, false),
                "Floating rate loan floating notional" => _sharedDataTransforms.CalculateFormattedMaturityDate(startDate, 20, false),
                "Traditional fixed rate loan short" => _sharedDataTransforms.CalculateFormattedMaturityDate(startDate, 1, true),
                _ => throw new Exception("Unknown option chosen for switch expression LoanType"),
            };

            var formattedMaturityDate = await stringTask;

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = notional;
            _scenarioContext["savedMaturityDate"] = formattedMaturityDate;

            await _sharedModel.CaseSetOfferFields(setCDDandDealDateTime, setCDDandDealDateTime, notional);

            if (productType == "Traditional fixed rate loan long")
            {
                await _fixedModel.FixedCaseSetDateFields(startDate, formattedMaturityDate, couponFreq: "Quarterly", firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponDay);
                await _fixedModel.FixedCaseSetAlternativeFields("30/360", swap: false, amortType: "Linear", numbOfAmort: "99", amortFreq: "Quarterly");
                await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("15,5", "15,5", "3", productType, lalCaseNumber, true);
            }
            else if (productType == "Floating rate loan floating notional")
            {
                await _finalSpreadObject.SetMinimalWithdrawalPercent("50");
                await _finalSpreadObject.SetCommitmentFee("50");
                await _variableModel.VariableCaseSetDateFields(startDate, formattedMaturityDate, "6mE", firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: "22");

                await _variableModel.VariableSetSpreadFieldsTwoEyes("6mE", productType, lalCaseNumber, h1: "-2,85", finalSpread: "-2,85", setSpreadMessages: false);
            }
            else if (productType == "Traditional fixed rate loan short")
            {
                await _shortModel.ShortCaseSetDateFields(startDate, formattedMaturityDate);
                await _shortModel.ShortCaseSetSpreadFieldsTwoEyes("4,85", "4,85", "85", productType, lalCaseNumber, true);
            }
        }

        [When(@"I chose ""([^""]*)"" and click the SubmitButton")]
        public async Task WhenIChoseAndClickTheSubmitButton(string wonLostCorrect)
        {
            var caseNumber = await _finalSpreadObject.GetCaseID();
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            _scenarioContext["savedWonLostCorrect"] = wonLostCorrect;

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect(wonLostCorrect, 1);
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();
            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();

            if (wonLostCorrect == "Won")
            {
                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginAccountDeskUser2();

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            }
            else if (wonLostCorrect == "Correct")
            {
                await Task.Delay(2000);
                await _basePageObject.CloseTabInPega();

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            }
        }

        [Then(@"the case status should be ""([^""]*)""")]
        public async Task ThenTheCaseStatusShouldBe(string caseStatus)
        {
            var savedWonLostCorrect = (string)_scenarioContext["savedWonLostCorrect"];
            await Task.Delay(800);
            var caseStatusText = await _basePageObject.GetFOCaseStatus();

            caseStatusText.Should().Be(caseStatus);

            if (savedWonLostCorrect == "Correct")
            {
                var firstOfferCheckBox = await _includeInMailBoxLocator.First.IsCheckedAsync();
                firstOfferCheckBox.Should().BeTrue();
            }
        }
    }
}