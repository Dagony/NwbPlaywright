using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using NwbPlaywright.PageObjects.TRX.FrontOffice;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class EmailContentFixedStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IFixedModel _fixedModel;
        private readonly ISharedModel _sharedModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly ICaptureSwapObject _captureSwapObject;
        private readonly IFixedDataTransforms _fixedDataTransforms;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;
        private readonly string _currentCouponDay;

        public string ChosenStartDate { get; set; }

        public string ChosenCouponDay { get; set; }

        public EmailContentFixedStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IFixedModel fixedModel,
            ISharedModel sharedModel,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            ICaptureSwapObject captureSwapObject,
            IFixedDataTransforms fixedDataTransforms)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _winLostCorrectObject = winLostCorrectObject;
            _fixedModel = fixedModel;
            _sharedModel = sharedModel;
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _captureSwapObject = captureSwapObject;
            _fixedDataTransforms = fixedDataTransforms;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            _currentCouponDay = _sharedDataTransforms.GetCurrentDateDDCouponDay();
            ChosenStartDate = _sharedModel.SharedEmptyString;
            ChosenCouponDay = _sharedModel.SharedEmptyString;
        }

        [Given(@"I create a fixed loan with the following case and offer specifics")]
        public async Task GivenICreateAFixedLoanWithTheFollowingCaseAndOfferSpecifics(Reqnroll.Table table)
        {
            FixedLoanCaseOfferFields fixedOfferData = table.CreateInstance<FixedLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = fixedOfferData;
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = fixedOfferData.TC;

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for variable case: {lalCaseNumber} ");
            await _sharedModel.SetGuarantorIfPresent(fixedOfferData.Guarantor1);
            await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, fixedOfferData.Notional);
        }

        [Given(@"I create a fixed loan with the following alternative specifics")]
        public async Task GivenICreateAFixedLoanWithTheFollowingAlternativeSpecifics(Reqnroll.Table table)
        {
            FixedLoanAlternativeFields fixedAltData = table.CreateInstance<FixedLoanAlternativeFields>();
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            _scenarioContext["savedAltData"] = fixedAltData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            double? notionalInDouble = _sharedDataTransforms.ParseAndCheckNotional(fixedOfferData.Notional);

            var setChosenStartDateAndCouponDay = fixedAltData.StartDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = fixedAltData.StartDate, ChosenCouponDay = fixedAltData.CouponDay, _scenarioContext["savedStartDate"] = fixedAltData.StartDate),
            };

            if (fixedAltData.CouponDay.Length > 2)
            {
                Enum.TryParse(fixedAltData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                await _fixedModel.FixedCaseSetDateFields(ChosenStartDate, fixedAltData.MaturityDate, couponFreq: fixedAltData.CouponFreq, couponDayOption: couponDayOptionFromDateTime);
            }
            else
            {
                await _fixedModel.FixedCaseSetDateFields(ChosenStartDate, fixedAltData.MaturityDate, couponFreq: fixedAltData.CouponFreq, couponDay: ChosenCouponDay);
            }

            await _sharedModel.SetFirstCouponLastCouponInterestRRDatesIfPresent(fixedAltData.FirstCouponDate, fixedAltData.LastCouponDate, fixedAltData.InterestRRDate);

            if (fixedAltData.AmortType != "Bullet")
            {
                await _fixedModel.FixedCaseSetAlternativeFields(fixedAltData.DayCount, fixedAltData.Swap, fixedAltData.AmortType, fixedAltData.NumbOfAmort, fixedAltData.AmortFreq);
            }
            else
            {
                await _fixedModel.FixedCaseSetAlternativeFields(fixedAltData.DayCount, fixedAltData.Swap, fixedAltData.AmortType);
            }

            await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes(fixedAltData.SpreadH1, fixedAltData.FinalSpread, fixedAltData.MarketIRate, "FIXED", lalCaseNumber, setSpreadMessages: true);
            if (fixedAltData.DealRemark != null && fixedAltData.DealRemark != string.Empty)
            {
                await _sharedModel.OverwriteDealRemark(fixedAltData.DealRemark);
            }

            if (fixedAltData.Swap == true && fixedAltData.SwapReferenceRate != null && fixedAltData.SwapReferenceRate != string.Empty)
            {
                await _finalSpreadObject.SetSwapReferenceRate(fixedAltData.SwapReferenceRate);
                await Task.Delay(500);
                await _finalSpreadObject.SetClearingHouse(fixedAltData.ClearingHouse);
                await Task.Delay(500);

                await _finalSpreadObject.SetBusinessDayConvention(fixedAltData.BusinessDayConvention);
                await Task.Delay(500);
            }
        }

        [Given("I create a fixed loan update with the following case and offer specifics")]
        public async Task GivenICreateAFixedLoanUpdateWithTheFollowingCaseAndOfferSpecifics(DataTable table)
        {
            FixedLoanCaseOfferFields fixedOfferData = table.CreateInstance<FixedLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = fixedOfferData;
            string setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = fixedOfferData.TC;
            _scenarioContext["savedGuarantor1"] = fixedOfferData.Guarantor1;
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for variable case: {lalCaseNumber} ");

            await _sharedModel.LoanUpdateCaseSetOfferFields(setCDDandDealDateTime, fixedOfferData.Notional);

            await _sharedModel.SetGuarantorIfPresent(fixedOfferData.Guarantor1);
        }

        [Given("I create a fixed loan update with the following alternative specifics")]
        public async Task GivenICreateAFixedLoanUpdateWithTheFollowingAlternativeSpecifics(DataTable table)
        {
            FixedLoanAlternativeFields fixedAltData = table.CreateInstance<FixedLoanAlternativeFields>();
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            _scenarioContext["savedAltData"] = fixedAltData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            var setChosenStartDateAndCouponDay = fixedAltData.StartDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = fixedAltData.StartDate, ChosenCouponDay = fixedAltData.CouponDay, _scenarioContext["savedStartDate"] = fixedAltData.StartDate),
            };

            if (fixedAltData.CouponDay.Length > 2)
            {
                Enum.TryParse(fixedAltData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                await _fixedModel.LoanUpdateFixedCaseSetDateFields(ChosenStartDate, fixedAltData.MaturityDate, couponFreq: fixedAltData.CouponFreq, couponDayOption: couponDayOptionFromDateTime);
            }
            else
            {
                await _fixedModel.LoanUpdateFixedCaseSetDateFields(ChosenStartDate, fixedAltData.MaturityDate, couponFreq: fixedAltData.CouponFreq, couponDay: ChosenCouponDay);
            }

            await _sharedModel.SetFirstCouponLastCouponInterestRRDatesIfPresent(fixedAltData.FirstCouponDate, fixedAltData.LastCouponDate, fixedAltData.InterestRRDate);

            if (fixedAltData.AmortType != "Bullet")
            {
                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(
                    fixedAltData.Swap,
                    fixedAltData.AmortType,
                    fixedAltData.NumbOfAmort,
                    fixedAltData.AmortFreq);
            }
            else
            {
                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(
                    fixedAltData.Swap,
                    fixedAltData.AmortType);
            }

            if (fixedAltData.Swap == true && fixedAltData.SwapReferenceRate != null && fixedAltData.SwapReferenceRate != string.Empty)
            {
                await _finalSpreadObject.SetSwapReferenceRate(fixedAltData.SwapReferenceRate);
                await _finalSpreadObject.SetClearingHouse(fixedAltData.ClearingHouse);
                await _finalSpreadObject.SetBusinessDayConvention(fixedAltData.BusinessDayConvention);
            }

            await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes(fixedAltData.SpreadH1, fixedAltData.FinalSpread, fixedAltData.MarketIRate, "InterestRESET", lalCaseNumber, setSpreadMessages: true);

            if (fixedAltData.DealRemark != null && fixedAltData.DealRemark != string.Empty)
            {
                await _sharedModel.OverwriteDealRemark(fixedAltData.DealRemark);
            }
        }

        [Then("the fixed offer email table should match the expected table")]
        public async Task ThenTheFixedOfferEmailTableShouldMatchTheExpectedTable()
        {
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];
            string expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.Offermail, customerRate);

            // REFACTOR zodat deze specflow property weg kan uit examples table:
            //      var pricefinalTransformed = await _basePageObject.TransformSpreadDataToStringWithThreeDecimalPlaces(VarAltData.PriceFinal);
            var (formattedStartDate, formattedMaturityDate) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, fixedAltData.StartDate, fixedAltData.MaturityDate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            actualTable.Should().NotContainAny("Roll-over lening met vaste hoofdsom", "Roll-over lening met variabele hoofdsom", "Euribor", "Kredietopslag (in bps)", "48uurs clausule", "Bereidstellingsprovisie (bps)", "Kasgeldfaciliteit", "Kasgeld", "Spreadherzieningsdatum");
            actualTable.Should().Contain($"Geldnemer\t{fixedCaseOfferData.CounterParty}");

            if (loanType == "INTERESTSPREADRESET")
            {
                // ActualTable.Should().Contain($"Hoofdsom (in eur)\t€ {FixedCaseOfferData.Notional}");
                actualTable.Should().ContainAny($"Uitstaande hoofdsom (in eur)\t{fixedCaseOfferData.ExpectedNotional}");

                string expectedFormattedOriginalSecID = _sharedDataTransforms.TransformOriginalSecID(fixedCaseOfferData.ExpectedOriginalSecID);
                actualAddresseesEmailText.Should().Contain($"Offerte Vastrentende lening - {expectedFormattedOriginalSecID} ({lalCaseNumber})");
            }
            else
            {
                actualTable.Should().Contain($"Hoofdsom (in eur)\t{fixedCaseOfferData.Notional}");
                actualAddresseesEmailText.Should().Contain($"Offerte Vastrentende lening ({lalCaseNumber})");
            }

            actualTable.Should().Contain($"Ingangsdatum\t{formattedStartDate}");
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDate}");
            actualTable.Should().Contain($"Rente%\t{expectedCustomerRate}");

            if (fixedAltData.ExpectedDayCount != null && fixedAltData.ExpectedDayCount != string.Empty)
            {
                var assertDayCountLoanUpdate = fixedAltData.ExpectedDayCount switch
                {
                    "A360" => actualTable.Should().Contain($"Renteconventie\tAct/360"),
                    "A365" => actualTable.Should().Contain($"Renteconventie\tAct/365"),
                    "ACT365" => actualTable.Should().Contain($"Renteconventie\tAct/Act ISDA"),
                    "ACTUAL" => actualTable.Should().Contain($"Renteconventie\tAct/Act"),
                    "EBOND" => actualTable.Should().Contain($"Renteconventie\t30/360"),
                    _ => throw new Exception("Unknown option chosen for switch expression AssertDayCountLoanUpdate"),
                };
            }
            else
            {
                actualTable.Should().Contain($"Renteconventie\t{fixedAltData.DayCount}");
            }

            var switchAssertAmortisationType = fixedAltData.AmortType switch
            {
                "Bullet" => (actualTable.Should().Contain($"Aflossing\tFixe"), actualTable.Should().NotContainAny($"Aflossing\tLineair", "Aflossing\tAnnuiteit", "Aflossing\tLineair ballon", "Aflossing\tAnnuiteit ballon", "Aflossing\tHandmatig", "Aflossing\tZero")),
                "Linear" => (actualTable.Should().Contain($"Aflossing\tLineair"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tAnnuiteit", "Aflossing\tLineair ballon", "Aflossing\tAnnuiteit ballon", "Aflossing\tHandmatig", "Aflossing\tZero")),
                "Annuity" => (actualTable.Should().Contain($"Aflossing\tAnnuiteit"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tLineair", "Aflossing\tLineair ballon", "Aflossing\tAnnuiteit ballon", "Aflossing\tHandmatig", "Aflossing\tZero")),
                "Linear balloon" => (actualTable.Should().Contain($"Aflossing\tLineair ballon"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tAnnuiteit ballon", "Aflossing\tHandmatig", "Aflossing\tZero").And.NotMatch("Aflossing\tAnnuiteit").And.NotMatch("Aflossing\tLineair")),
                "Annuity balloon" => (actualTable.Should().Contain($"Aflossing\tAnnuiteit ballon"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tLineair ballon", "Aflossing\tHandmatig", "Aflossing\tZero").And.NotMatch("Aflossing\tAnnuiteit").And.NotMatch("Aflossing\tLineair")),
                "Manual" => (actualTable.Should().Contain($"Aflossing\tHandmatig"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tLineair", "Aflossing\tAnnuiteit", "Aflossing\tLineair ballon", "Aflossing\tAnnuiteit ballon", "Aflossing\tZero")),
                "Zero" => (actualTable.Should().Contain($"Aflossing\tZero"), actualTable.Should().NotContainAny($"Aflossing\tFixe", "Aflossing\tLineair", "Aflossing\tAnnuiteit ballon", "Aflossing\tLineair ballon", "Aflossing\tAnnuiteit ballon", "Aflossing\tHandmatig")),
                _ => throw new Exception("Assertion failed due to unrecognized chosen Amortisation type. Response origin from switchAssertAmortisationType"),
            };

            if (fixedAltData.CouponFreq == "Weekly")
            {
                var switchTranslateCouponWeekday = fixedAltData.CouponDay switch
                {
                    "Monday" => (actualTable.Should().Contain($"Coupondag\tMaandag"), actualTable.Should().NotContainAny($"Coupondag\tDinsdag", "Coupondag\tWoensdag", "Coupondag\tDonderdag", "Coupondag\tVrijdag")),
                    "Tuesday" => (actualTable.Should().Contain($"Coupondag\tDinsdag"), actualTable.Should().NotContainAny($"Coupondag\tMaandag", "Coupondag\tWoensdag", "Coupondag\tDonderdag", "Coupondag\tVrijdag")),
                    "Wednesday" => (actualTable.Should().Contain($"Coupondag\tWoensdag"), actualTable.Should().NotContainAny($"Coupondag\tMaandag", "Coupondag\tDinsdag", "Coupondag\tDonderdag", "Coupondag\tVrijdag")),
                    "Thursday" => (actualTable.Should().Contain($"Coupondag\tDonderdag"), actualTable.Should().NotContainAny($"Coupondag\tMaandag", "Coupondag\tDinsdag", "Coupondag\tWoensdag", "Coupondag\tVrijdag")),
                    "Friday" => (actualTable.Should().Contain($"Coupondag\tVrijdag"), actualTable.Should().NotContainAny($"Coupondag\tMaandag", "Coupondag\tDinsdag", "Coupondag\tWoensdag", "Coupondag\tDonderdag")),
                    _ => throw new Exception("Assertion failed due to unrecognized Weekday. Did you forgot capitals?"),
                };
            }
            else
            {
                actualTable.Should().Contain($"Coupondag\t{fixedAltData.CouponDay}");
            }

            var switchTranslateCouponFreq = fixedAltData.CouponFreq switch
            {
                "Annual" => (actualTable.Should().Contain($"Rentefrequentie\tJaarlijks"), actualTable.Should().NotContainAny($"Rentefrequentie\tHalf Jaarlijks", "Rentefrequentie\tPer kwartaal", "Rentefrequentie\tMaandelijks", "Rentefrequentie\tWekelijks")),
                "Semi annual" => (actualTable.Should().Contain($"Rentefrequentie\tHalf Jaarlijks"), actualTable.Should().NotContainAny($"Rentefrequentie\tJaarlijks", "Rentefrequentie\tPer kwartaal", "Rentefrequentie\tMaandelijks", "Rentefrequentie\tWekelijks")),
                "Quarterly" => (actualTable.Should().Contain($"Rentefrequentie\tPer kwartaal"), actualTable.Should().NotContainAny($"Rentefrequentie\tJaarlijks", "Rentefrequentie\tHalf Jaarlijks", "Rentefrequentie\tMaandelijks", "Rentefrequentie\tWekelijks")),
                "Monthly" => (actualTable.Should().Contain($"Rentefrequentie\tMaandelijks"), actualTable.Should().NotContainAny($"Rentefrequentie\tJaarlijks", "Rentefrequentie\tHalf Jaarlijks", "Rentefrequentie\tPer kwartaal", "Rentefrequentie\tWeekelijks")),
                "Weekly" => (actualTable.Should().Contain($"Rentefrequentie\tWekelijks"), actualTable.Should().NotContainAny($"Rentefrequentie\tJaarlijks", "Rentefrequentie\tHalf Jaarlijks", "Rentefrequentie\tPer kwartaal", "Rentefrequentie\tMaandelijks")),
                _ => throw new Exception("Assertion failed due to unrecognized CouponFreq. Did you forgot capitals?"),
            };
            if (fixedAltData.FirstCouponDate != null && fixedAltData.FirstCouponDate != string.Empty)
            {
                var formattedFirstCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, fixedAltData.FirstCouponDate);
                actualTable.Should().Contain($"Eerste coupondatum\t{formattedFirstCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Eerste coupondatum");
            }

            if (fixedAltData.LastCouponDate != null && fixedAltData.LastCouponDate != string.Empty)
            {
                var formattedLastCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, fixedAltData.LastCouponDate);
                actualTable.Should().Contain($"Laatste coupondatum\t{formattedLastCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Laatste coupondatum");
            }

            if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
            {
                var formattedInterestRRDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, fixedAltData.InterestRRDate);
                actualTable.Should().Contain($"Renteherziening\t{formattedInterestRRDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Renteherziening");
            }

            if (fixedAltData.AmortType != "Bullet")
            {
                if (fixedAltData.NumbOfAmort.Length > 3)
                {
                    // Adding a dot to string when number length is longer than three.
                    var formattedNumbOfAmort = fixedAltData.NumbOfAmort.Substring(0, 1) + "." + fixedAltData.NumbOfAmort.Substring(1, 3);
                    actualTable.Should().Contain($"Aantal aflossingen\t{formattedNumbOfAmort}");
                }
                else
                {
                    actualTable.Should().Contain($"Aantal aflossingen\t{fixedAltData.NumbOfAmort}");
                }

                var switchTranslateAmortFreq = fixedAltData.AmortFreq switch
                {
                    "Annual" => (actualTable.Should().Contain($"Aflossingsfrequentie\tJaarlijks"), actualTable.Should().NotContainAny($"Aflossingsfrequentie\tHalf Jaarlijks", "Aflossingsfrequentie\tPer kwartaal", "Aflossingsfrequentie\tMaandelijks", "Aflossingsfrequentie\tWeekelijks")),
                    "Semi annual" => (actualTable.Should().Contain($"Aflossingsfrequentie\tHalf Jaarlijks"), actualTable.Should().NotContainAny($"Aflossingsfrequentie\tJaarlijks", "Aflossingsfrequentie\tPer kwartaal", "Aflossingsfrequentie\tMaandelijks", "Aflossingsfrequentie\tWeekelijks")),
                    "Quarterly" => (actualTable.Should().Contain($"Aflossingsfrequentie\tPer kwartaal"), actualTable.Should().NotContainAny($"Aflossingsfrequentie\tJaarlijks", "Aflossingsfrequentie\tHalf Jaarlijks", "Aflossingsfrequentie\tMaandelijks", "Aflossingsfrequentie\tWeekelijks")),
                    "Monthly" => (actualTable.Should().Contain($"Aflossingsfrequentie\tMaandelijks"), actualTable.Should().NotContainAny($"Aflossingsfrequentie\tJaarlijks", "Aflossingsfrequentie\tHalf Jaarlijks", "Aflossingsfrequentie\tPer kwartaal", "Aflossingsfrequentie\tWeekelijks")),
                    "Weekly" => (actualTable.Should().Contain($"Aflossingsfrequentie\tWekelijks"), actualTable.Should().NotContainAny($"Aflossingsfrequentie\tJaarlijks", "Aflossingsfrequentie\tHalf Jaarlijks", "Aflossingsfrequentie\tPer kwartaal", "Aflossingsfrequentie\tMaandelijks")),
                    _ => throw new Exception("Assertion failed due to unrecognized AmortFreq. Did you forgot capitals?"),
                };
            }
            else
            {
                actualTable.Should().NotContain($"Aflossingsfrequentie");
                actualTable.Should().NotContain($"Aantal aflossingen");
            }

            if (sicCode == "0030000000")
            {
                actualTable.Should().Contain($"Zekerheid\tStichting Waarborgfonds Sociale Woningbouw");
            }
            else if (sicCode == "0031000000")
            {
                actualTable.Should().Contain($"Zekerheid\tWaarborgfonds voor de Zorgsector");
            }
            else
            {
                if (fixedCaseOfferData.Guarantor1 != null && fixedCaseOfferData.Guarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{fixedCaseOfferData.Guarantor1}");
                }
                else if (fixedCaseOfferData.ExpectedLoanUpdateGuarantor != null && fixedCaseOfferData.ExpectedLoanUpdateGuarantor != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{fixedCaseOfferData.ExpectedLoanUpdateGuarantor}");
                }
                else
                {
                    actualTable.Should().NotContain($"Zekerheid");
                }
            }

            if (fixedAltData.DealRemark != null && fixedAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{fixedAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
            }
        }

        [Then("the displayed fixed email text should match the expected text")]
        public async Task ThenTheDisplayedFixedEmailTextShouldMatchTheExpectedText()
        {
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            actualEmailText.Should().Contain("Hierbij prijzen wij de gevraagde offerte als volgt:");
            actualEmailText.Should().Contain($"Offerte Vastrentende lening ({lalCaseNumber})");
            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.");

            Console.Write("Mark wil de loanType weten: " + loanType);
            Console.Write("Mark wil de sicCode weten: " + sicCode);

            if (loanType == "INTERESTSPREADRESET")
            {
                // ActualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.");
                actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd");
                actualEmailText.Should().Contain("Indien u niet akkoord gaat met de offerte dient de lening te worden afgelost op de herzieningsdatum. Naast de restant hoofdsom is uiteraard de opgelopen rente + de eventuele reguliere aflossing verschuldigd. Van onze afdeling Operations ontvangt u hiervan een schriftelijke bevestiging en een aflosnota.");
                actualEmailText.Should().Contain("Het aflosbedrag dient op de herzieningsdatum op de rekening van de NWB Bank bijgeschreven te staan. Indien het bedrag later wordt bijgeschreven zal hier een extra vergoeding voor in rekening worden gebracht. Gelieve uw reactie middels een reply op deze e-mail.");

                // na uat moet deze tekst niet meer in loan update mails voorkomen:
                actualEmailText.Should().NotContain("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden." +
            " NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing," +
            " de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");

                if (sicCode == "0030000000")
                {
                    // WSW template
                    actualEmailText.Should().Contain("Op deze lening zullen de “Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021” van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie.");
                    actualEmailText.Should().NotContain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
                else if (sicCode == "0031000000")
                {
                    // WFZ template
                    actualEmailText.Should().Contain("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
                    actualEmailText.Should().NotContain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
                else
                {
                    // NORMAL template
                    actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                    actualEmailText.Should().Contain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }

                actualEmailText.Should().Contain("Gelieve uw reactie middels een reply op deze e-mail.");
            }
            else
            {
                actualEmailText.Should().Contain("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden." +
            " NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing," +
            " de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");

                if (sicCode == "0030000000")
                {
                    // WSW template
                    actualEmailText.Should().Contain("Op deze lening zullen de “Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021” van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie.");
                    actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WSW standaard geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                    actualEmailText.Should().NotContain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
                else if (sicCode == "0031000000")
                {
                    // WFZ template
                    actualEmailText.Should().Contain("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
                    actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WfZ standaard geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                    actualEmailText.Should().NotContain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
                else
                {
                    // NORMAL template
                    actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                    actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen en daarbij het bankrekeningnummer te vermelden waarop het door u gevraagde bedrag dient te worden overgemaakt." +
                        " Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                    actualEmailText.Should().Contain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
            }

            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.");
            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.");
            actualEmailText.Should().Contain("Bovenstaande is onder voorbehoud van typefouten.");
            actualEmailText.Should().Contain("Met vriendelijke groet,");

            actualEmailText.Should().Contain("Nederlandse Waterschapsbank N.V.");
            actualEmailText.Should().Contain("Rooseveltplantsoen 3");
            actualEmailText.Should().Contain("2517 KR Den Haag");
            Debug.WriteLine(Regex.Escape(actualEmailText));

            actualEmailText.Should().Contain("pf@nwbbank.com");

            actualEmailText.Should().Contain("w www.nwbbank.com");
        }
    }
}