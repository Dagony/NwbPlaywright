using Reqnroll;
using System;
using System.Reflection;
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
    public class EmailContentVariableStepDefinitions
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
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly IVariableDataTransforms _variableDataTransforms;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly IVariableModel _variableModel;
        private readonly ISharedModel _sharedModel;
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;
        private readonly string _currentCouponDay;

        public string ChosenStartDate { get; set; }

        public string ChosenCouponDay { get; set; }

        public EmailContentVariableStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IVariableModel variableModel,
            ISharedModel sharedModel,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            IVariableDataTransforms variableDataTransforms)
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
            _variableModel = variableModel;
            _sharedModel = sharedModel;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _variableDataTransforms = variableDataTransforms;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            _currentCouponDay = _sharedDataTransforms.GetCurrentDateDDCouponDay();
            ChosenStartDate = _sharedModel.SharedEmptyString;
            ChosenCouponDay = _sharedModel.SharedEmptyString;
        }

        [Given("I create a variable loan with the following case and offer specifics")]
        public async Task GivenICreateAVariableLoanWithTheFollowingCaseAndOfferSpecifics(DataTable table)
        {
            VariableLoanCaseOfferFields variableOfferData = table.CreateInstance<VariableLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = variableOfferData;

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = variableOfferData.TC;

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for variable case: {lalCaseNumber} ");
            await _sharedModel.SetGuarantorIfPresent(variableOfferData.Guarantor1);
            await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, variableOfferData.Notional);

            if (bool.Parse(variableOfferData.SetNotionalTypeToFixed) == false)
            {
                await _finalSpreadObject.SetMinimalWithdrawalPercent(variableOfferData.MinimalWithdrawal);
                await _finalSpreadObject.SetCommitmentFee(variableOfferData.CommitmentFee);

                await Task.Delay(1000);
                var minimalWithdrawalAmount = await _finalSpreadObject.GetMinimalWithdrawalAmount();
                _scenarioContext["savedMinimalWithdrawalPercentageValue"] = minimalWithdrawalAmount;
            }
        }

        [Given("I create a variable loan with the following alternative specifics")]
        public async Task GivenICreateAVariableLoanWithTheFollowingAlternativeSpecifics(DataTable table)
        {
            VariableLoanAlternativeFields variableAltData = table.CreateInstance<VariableLoanAlternativeFields>();
            var variableOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            _scenarioContext["savedAltData"] = variableAltData;

            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var setChosenStartDateAndCouponDay = variableAltData.StartDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = variableAltData.StartDate, ChosenCouponDay = variableAltData.CouponDay, _scenarioContext["savedStartDate"] = variableAltData.StartDate),
            };

            if (variableAltData.CouponDay.Length > 2)
            {
                Enum.TryParse(variableAltData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                await _variableModel.VariableCaseSetDateFields(ChosenStartDate, variableAltData.MaturityDate, variableAltData.ReferenceRate, bool.Parse(variableAltData.HoursClause), couponDayOption: couponDayOptionFromDateTime);
            }
            else
            {
                await _variableModel.VariableCaseSetDateFields(ChosenStartDate, variableAltData.MaturityDate, variableAltData.ReferenceRate, couponDay: ChosenCouponDay);
            }

            await _sharedModel.SetFirstCouponLastCouponInterestRRDatesIfPresent(variableAltData.FirstCouponDate, variableAltData.LastCouponDate, variableAltData.InterestRRDate);
            await _variableModel.VariableSetSpreadFieldsTwoEyes(variableAltData.ReferenceRate, "VARIABLE", lalCaseNumber, variableAltData.SpreadH1, variableAltData.FinalSpread, setSpreadMessages: true, variableAltData.SpreadH1Basis, variableAltData.FinalSpreadBasis);
            if (variableAltData.DealRemark != null && variableAltData.DealRemark != string.Empty)
            {
                await _finalSpreadObject.SetDealRemark(variableAltData.DealRemark);
            }

            if (variableAltData.NegativeInterestRate != null && variableAltData.NegativeInterestRate != string.Empty)
            {
                await _finalSpreadObject.SetNegativeInterestRate(bool.Parse(variableAltData.NegativeInterestRate));
            }
        }

        [Given("I create a variable loan update with the following case and offer specifics")]
        public async Task GivenICreateAVariableLoanUpdateWithTheFollowingCaseAndOfferSpecifics(DataTable table)
        {
            VariableLoanCaseOfferFields variableOfferData = table.CreateInstance<VariableLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = variableOfferData;
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = variableOfferData.TC;

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for variable case: {lalCaseNumber} ");
            await _sharedModel.LoanUpdateSetGuarantorIfPresent(variableOfferData.Guarantor1);
            await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, variableOfferData.Notional);

            if (variableOfferData.MinimalWithdrawal != null && variableOfferData.MinimalWithdrawal != string.Empty)
            {
                var minimalWithdrawalAmount = await _loanUpdateFinalSpreadObject.GetMinimalWithdrawalAmount();
                _scenarioContext["savedMinimalWithdrawalPercentageValue"] = minimalWithdrawalAmount;

                var maximumWithdrawalAmount = await _loanUpdateFinalSpreadObject.GetMaximumWithdrawalAmount();
                _scenarioContext["savedMaximumWithdrawalValue"] = maximumWithdrawalAmount;
            }

            if (variableOfferData.CommitmentFee != null && variableOfferData.CommitmentFee != string.Empty)
            {
                await _loanUpdateFinalSpreadObject.SetCommitmentFee(variableOfferData.CommitmentFee);
            }
        }

        [Given("I create a variable loan update with the following alternative specifics")]
        public async Task GivenICreateAVariableLoanUpdateWithTheFollowingAlternativeSpecifics(DataTable table)
        {
            VariableLoanAlternativeFields variableAltData = table.CreateInstance<VariableLoanAlternativeFields>();
            _scenarioContext["savedAltData"] = variableAltData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            var setChosenStartDateAndCouponDay = variableAltData.StartDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = variableAltData.StartDate, ChosenCouponDay = variableAltData.CouponDay, _scenarioContext["savedStartDate"] = variableAltData.StartDate),
            };

            if (variableAltData.CouponDay.Length > 2)
            {
                Enum.TryParse(variableAltData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                await _variableModel.LoanUpdateVariableCaseSetDateFields(ChosenStartDate, variableAltData.MaturityDate, variableAltData.ReferenceRate, bool.Parse(variableAltData.HoursClause), couponDayOption: couponDayOptionFromDateTime);
            }
            else
            {
                await _variableModel.LoanUpdateVariableCaseSetDateFields(ChosenStartDate, variableAltData.MaturityDate, variableAltData.ReferenceRate, couponDay: ChosenCouponDay);
            }

            await _sharedModel.SetFirstCouponLastCouponInterestRRDatesIfPresent(variableAltData.FirstCouponDate, variableAltData.LastCouponDate, variableAltData.InterestRRDate);
            await _variableModel.LoanUpdateVariableSetSpreadFieldsTwoEyes(variableAltData.ReferenceRate, "VARIABLE", lalCaseNumber, variableAltData.SpreadH1, variableAltData.FinalSpread, setSpreadMessages: true, variableAltData.SpreadH1Basis, variableAltData.FinalSpreadBasis);

            if (variableAltData.DealRemark != null && variableAltData.DealRemark != string.Empty)
            {
                await _finalSpreadObject.SetDealRemark(variableAltData.DealRemark);
            }

            if (variableAltData.NegativeInterestRate != null && variableAltData.NegativeInterestRate != string.Empty)
            {
                await _finalSpreadObject.SetNegativeInterestRate(bool.Parse(variableAltData.NegativeInterestRate));
            }
        }

        [Given("I create a baserate loan update with the following case and offer specifics")]
        public async Task GivenICreateABaserateLoanUpdateWithTheFollowingCaseAndOfferSpecifics(DataTable table)
        {
            BaseRateLoanCaseOfferFields baserateOfferData = table.CreateInstance<BaseRateLoanCaseOfferFields>();
            _scenarioContext["savedCaseOfferData"] = baserateOfferData;
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedTC"] = baserateOfferData.TC;

            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for variable case: {lalCaseNumber} ");
            await _sharedModel.LoanUpdateSetGuarantorIfPresent(baserateOfferData.Guarantor1);
            await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, baserateOfferData.Notional);
        }

        [Given("I create a baserate loan update with the following alternative specifics")]
        public async Task GivenICreateABaserateLoanUpdateWithTheFollowingAlternativeSpecifics(DataTable table)
        {
            BaseRateAlternativeFields baserateAltData = table.CreateInstance<BaseRateAlternativeFields>();
            _scenarioContext["savedAltData"] = baserateAltData;
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            var setChosenStartDateAndCouponDay = baserateAltData.StartDate switch
            {
                "today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "Today" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                "TODAY" => (ChosenStartDate = _currentFormattedDate, ChosenCouponDay = _currentCouponDay, _scenarioContext["savedStartDate"] = _currentFormattedDate),
                _ => (ChosenStartDate = baserateAltData.StartDate, ChosenCouponDay = baserateAltData.CouponDay, _scenarioContext["savedStartDate"] = baserateAltData.StartDate),
            };

            if (baserateAltData.CouponDay.Length > 2)
            {
                Enum.TryParse(baserateAltData.CouponDay, out CouponDay couponDayOptionFromDateTime);
                await _variableModel.LoanUpdateVariableCaseSetDateFields(ChosenStartDate, baserateAltData.MaturityDate, baserateAltData.ReferenceRate, couponDayOption: couponDayOptionFromDateTime);
            }
            else
            {
                await _variableModel.LoanUpdateVariableCaseSetDateFields(ChosenStartDate, baserateAltData.MaturityDate, baserateAltData.ReferenceRate, couponDay: ChosenCouponDay);
            }

            await _sharedModel.SetFirstCouponLastCouponInterestRRDatesIfPresent(baserateAltData.FirstCouponDate, baserateAltData.LastCouponDate, baserateAltData.InterestRRDate);
            await _variableModel.LoanUpdateBaseRateSetSpreadFieldsTwoEyes(baserateAltData.ReferenceRate, "BASE RATE", lalCaseNumber, baserateAltData.SpreadH1, baserateAltData.FinalSpread, setSpreadMessages: true, baserateAltData.SpreadH1Basis, baserateAltData.FinalSpreadBasis);

            if (baserateAltData.DealRemark != null && baserateAltData.DealRemark != string.Empty)
            {
                await _finalSpreadObject.SetDealRemark(baserateAltData.DealRemark);
            }
        }

        [Then("the variable offer email table should match the expected table")]
        public async Task ThenTheVariableOfferEmailTableShouldMatchTheExpectedTable()
        {
            var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            double doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];

            // REFACTOR zodat deze specflow property weg kan uit examples table:
            //      var pricefinalTransformed = await _basePageObject.TransformSpreadDataToStringWithThreeDecimalPlaces(VarAltData.PriceFinal);
            var (formattedStartDate, formattedMaturityDate) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, varAltData.StartDate, varAltData.MaturityDate);
            var expectedCustomerRate = await _variableDataTransforms.TransformCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.Offermail, doubleCustomerRate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            actualTable.Should().NotContainAny("Rente%\"", "Aflossing", "Aantal Aflossingen", "Aflossingsfrequentie", "Kasgeldfaciliteit", "Kasgeld", "Renteherziening");

            if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                var minimalWithdrawalAmountUntrimmed2 = (string)_scenarioContext["savedMinimalWithdrawalPercentageValue"];
                if (loanType == "INTERESTSPREADRESET")
                {
                    string expectedFormattedOriginalSecID = _sharedDataTransforms.TransformOriginalSecID(varCaseOfferData.ExpectedOriginalSecID);
                    actualAddresseesEmailText.Should().Contain($"Offerte Roll-over lening met variabele hoofdsom - {expectedFormattedOriginalSecID} ({lalCaseNumber})");
                }
                else
                {
                    actualAddresseesEmailText.Should().Contain($"Offerte Roll-over lening met variabele hoofdsom ({lalCaseNumber})");
                }

                var minimalWithdrawalTrimmedEuroSign = minimalWithdrawalAmountUntrimmed2.Substring(1);
                actualTable.Should().Contain($"Minimale opname (in eur)\t{minimalWithdrawalTrimmedEuroSign}");

                if (varCaseOfferData.CommitmentFee != null && varCaseOfferData.CommitmentFee != string.Empty)
                {
                    actualTable.Should().Contain($"Bereidstellingsprovisie (in eur)\t{varCaseOfferData.CommitmentFee}");
                }
                else
                {
                    actualTable.Should().NotContain($"Bereidstellingsprovisie (in eur)");
                }
            }
            else
            {
                if (loanType == "INTERESTSPREADRESET")
                {
                    string expectedFormattedOriginalSecID = _sharedDataTransforms.TransformOriginalSecID(varCaseOfferData.ExpectedOriginalSecID);
                    actualAddresseesEmailText.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom - {expectedFormattedOriginalSecID} ({lalCaseNumber})");
                }
                else
                {
                    actualAddresseesEmailText.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom ({lalCaseNumber})");
                }

                actualTable.Should().NotContain($"Bereidstellingsprovisie (in eur)");
                actualTable.Should().NotContain($"Minimale opname (in eur)");
            }

            var switchAssertReferenceRate = varAltData.ReferenceRate switch
            {
                "1wE" => (actualTable.Should().Contain($"Euribor\t1 weeks"), actualTable.Should().NotContainAny($"Euribor\t1 maands", "Euribor\t3 maands", "Euribor\t6 maands", "Euribor\t12 maands")),
                "1mE" => (actualTable.Should().Contain($"Euribor\t1 maands"), actualTable.Should().NotContainAny($"Euribor\t1 weeks", "Euribor\t3 maands", "Euribor\t6 maands", "Euribor\t12 maands")),
                "3mE" => (actualTable.Should().Contain($"Euribor\t3 maands"), actualTable.Should().NotContainAny($"Euribor\t1 weeks", "Euribor\t1 maands", "Euribor\t6 maands", "Euribor\t12 maands")),
                "6mE" => (actualTable.Should().Contain($"Euribor\t6 maands"), actualTable.Should().NotContainAny($"Euribor\t1 weeks", "Euribor\t1 maands", "Euribor\t3 maands", "Euribor\t12 maands")),
                "12mE" => (actualTable.Should().Contain($"Euribor\t12 maands"), actualTable.Should().NotContainAny($"Euribor\t1 weeks", "Euribor\t1 maands", "Euribor\t3 maands", "Euribor\t6 maands")),
                _ => throw new Exception("Assertion failed due to unrecognized Weekday. Did you forgot capitals?"),
            };

            actualTable.Should().Contain($"Geldnemer\t{varCaseOfferData.CounterParty}");

            // init comment
            if (loanType == "INTERESTSPREADRESET")
            {
                // ActualTable.Should().Contain$"Hoofdsom (in eur)\t� {VarCaseOfferData.Notional}");
                actualTable.Should().Contain($"Uitstaande hoofdsom (in eur)\t{varCaseOfferData.ExpectedNotional}");
            }
            else
            {
                actualTable.Should().Contain($"Hoofdsom (in eur)\t{varCaseOfferData.Notional}");
            }

            actualTable.Should().Contain($"Ingangsdatum\t{formattedStartDate}");
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDate}");

            if (varAltData.ReferenceRate == "1wE")
            {
                var switchTranslateCouponWeekday = varAltData.CouponDay switch
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
                actualTable.Should().Contain($"Coupondag\t{varAltData.CouponDay}");
            }

            // ActualTable.Should().Contain($"Kredietopslag (in bps)\t{expectedCustomerRate}�\n");
            actualTable.Should().Contain($"Kredietopslag (in bps)\t{expectedCustomerRate}");

            if (varAltData.ExpectedDayCount != null && varAltData.ExpectedDayCount != string.Empty)
            {
                var assertDayCountLoanUpdate = varAltData.ExpectedDayCount switch
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
                actualTable.Should().Contain($"Renteconventie\tAct/360");
            }

            if (varAltData.FirstCouponDate != null && varAltData.FirstCouponDate != string.Empty)
            {
                var formattedFirstCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, varAltData.FirstCouponDate);
                actualTable.Should().Contain($"Eerste coupondatum\t{formattedFirstCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Eerste coupondatum");
            }

            if (varAltData.LastCouponDate != null && varAltData.LastCouponDate != string.Empty)
            {
                var formattedLastCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, varAltData.LastCouponDate);
                actualTable.Should().Contain($"Laatste coupondatum\t{formattedLastCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Laatste coupondatum");
            }

            if (varAltData.InterestRRDate != null && varAltData.InterestRRDate != string.Empty)
            {
                var formattedInterestRRDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, varAltData.InterestRRDate);
                actualTable.Should().Contain($"Spreadherzieningsdatum\t{formattedInterestRRDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Spreadherzieningsdatum");
            }

            if (varAltData.ReferenceRate == "1wE")
            {
                actualTable.Should().Contain("Euribor\t1 weeks");
                if (bool.Parse(varAltData.HoursClause) == true)
                {
                    actualTable.Should().ContainEquivalentOf("48uurs clausule\tJa");
                }
                else if (bool.Parse(varAltData.HoursClause) != true)
                {
                    actualTable.Should().ContainEquivalentOf("48uurs clausule\tNee");
                }
            }
            else if (varAltData.ReferenceRate != "1wE")
            {
                actualTable.Should().NotContain("48uurs clausule");
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
                if (varCaseOfferData.Guarantor1 != null && varCaseOfferData.Guarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{varCaseOfferData.Guarantor1}");
                }
                else if (varCaseOfferData.ExpectedGuarantor1 != null && varCaseOfferData.ExpectedGuarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{varCaseOfferData.ExpectedGuarantor1}");
                }
                else
                {
                    actualTable.Should().NotContain($"Zekerheid");
                }
            }

            if (varAltData.DealRemark != null && varAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{varAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
            }
        }

        [Then("the displayed variable email text should match the expected text")]
        public async Task ThenTheDisplayedVariableEmailTextShouldMatchTheExpectedText()
        {
            var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            actualEmailText.Should().Contain("Hierbij prijzen wij de gevraagde offerte als volgt:");
            if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                actualEmailText.Should().Contain($"Offerte Roll-over lening met variabele hoofdsom ({lalCaseNumber})");
            }
            else if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional)
            {
                actualEmailText.Should().Contain($"Offerte Roll-over lening met vaste hoofdsom ({lalCaseNumber})");
            }

            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.");

            if (loanType == "INTERESTSPREADRESET")
            {
                // na uat moet deze tekst niet meer in loan update mails voorkomen:
                actualEmailText.Should().NotContain("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden." +
         " NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing," +
         " de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");

                // ActualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze backoffice aan u zal worden toegestuurd.");
                actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd");
                actualEmailText.Should().Contain("Indien u niet akkoord gaat met de offerte dient de lening te worden afgelost op de herzieningsdatum. Naast de restant hoofdsom is uiteraard de opgelopen rente + de eventuele reguliere aflossing verschuldigd. Van onze afdeling Operations ontvangt u hiervan een schriftelijke bevestiging en een aflosnota.");
                actualEmailText.Should().Contain("Het aflosbedrag dient op de herzieningsdatum op de rekening van de NWB Bank bijgeschreven te staan. Indien het bedrag later wordt bijgeschreven zal hier een extra vergoeding voor in rekening worden gebracht. Gelieve uw reactie middels een reply op deze e-mail.");

                // ActualEmailText.Should().Contain("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden. NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing, de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");
                if (sicCode == "0030000000")
                {
                    // WSW template
                    // ActualEmailText.Should().Contain("Op deze lening zullen de �Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021� van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie");
                    actualEmailText.Should().Contain("Op deze lening zullen de “Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021” van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie.");
                }
                else if (sicCode == "0031000000")
                {
                    // WFZ template
                    actualEmailText.Should().Contain("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
                }
                else
                {
                    actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                    actualEmailText.Should().Contain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }

                // ActualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.");
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
                }
                else if (sicCode == "0031000000")
                {
                    // WFZ template
                    actualEmailText.Should().Contain("Op deze lening zullen de \"Algemene bepalingen voor geldleningen onder borging van Stichting Waarborgfonds voor de Zorgsector gedateerd oktober 2023\" van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wfz.nl/het-wfz/overzicht-documenten.");
                    actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Gelieve uw acceptatie middels een reply op deze e-mail te bevestigen. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een WfZ standaard geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd.");
                }
                else
                {
                    // Normal template
                    actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                    actualEmailText.Should().Contain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
                }
            }

            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.");
            actualEmailText.Should().Contain("Bovenstaande is onder voorbehoud van typefouten.");
            actualEmailText.Should().Contain("Met vriendelijke groet,");

            actualEmailText.Should().Contain("Nederlandse Waterschapsbank N.V.");
            actualEmailText.Should().Contain("Rooseveltplantsoen 3");
            actualEmailText.Should().Contain("2517 KR Den Haag");
            actualEmailText.Should().Contain("t");

            // ActualEmailText.Should().Contain("e pf@nwbbank.com");
            //  ActualEmailText.Should().Contain("e�pf@nwbbank.com");
            actualEmailText.Should().Contain("pf@nwbbank.com");
            actualEmailText.Should().Contain("w www.nwbbank.com");
        }

        [Then("the base rate offer email table should match the expected table")]
        public async Task ThenTheBaseRateOfferEmailTableShouldMatchTheExpectedTable()
        {
            var baseRateCaseOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            double doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];

            // REFACTOR zodat deze specflow property weg kan uit examples table:
            //      var pricefinalTransformed = await _basePageObject.TransformSpreadDataToStringWithThreeDecimalPlaces(VarAltData.PriceFinal);
            var (formattedStartDate, formattedMaturityDate) = await _sharedDataTransforms.TransformStartAndMaturityDate(DataTransformOptions.Offermail, LanguageSetting.NL, baseRateAltData.StartDate, baseRateAltData.MaturityDate);
            var expectedBPSRate = await _variableDataTransforms.TransformCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.Offermail, doubleCustomerRate);

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            string expectedFormattedOriginalSecID = _sharedDataTransforms.TransformOriginalSecID(baseRateCaseOfferData.ExpectedOriginalSecID);
            actualAddresseesEmailText.Should().Contain($"Offerte Basisrentelening - {expectedFormattedOriginalSecID} ({lalCaseNumber})");

            actualTable.Should().NotContainAny("Rente%", "Aantal Aflossingen", "Aflossingsfrequentie", "Kasgeldfaciliteit", "Kasgeld", "Renteherziening");

            actualTable.Should().Contain($"Offerte Basisrentelening ({lalCaseNumber})");
            actualTable.Should().NotContain("Roll-over lening met vaste hoofdsom");
            actualTable.Should().NotContain("Vastrentende lening");

            // WEER AANZETTEN!
            actualTable.Should().Contain($"Geldnemer\t{baseRateCaseOfferData.CounterParty}");
            actualTable.Should().Contain($"Uitstaande hoofdsom (in eur)\t{baseRateCaseOfferData.ExpectedNotional}");

            actualTable.Should().Contain($"Ingangsdatum\t{formattedStartDate}");
            actualTable.Should().Contain($"Einddatum\t{formattedMaturityDate}");

            if (baseRateAltData.ExpectedCouponFreq == "Weekly")
            {
                var switchTranslateCouponWeekday = baseRateAltData.CouponDay switch
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
                actualTable.Should().Contain($"Coupondag\t{baseRateAltData.CouponDay}");
            }

            actualTable.Should().Contain($"Basisrente%\t{baseRateAltData.BaseRate}");
            actualTable.Should().Contain($"Kredietopslag (in bps)\t{expectedBPSRate}");
            actualTable.Should().NotContain("Renteconventie");

            if (baseRateAltData.FirstCouponDate != null && baseRateAltData.FirstCouponDate != string.Empty)
            {
                var formattedFirstCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, baseRateAltData.FirstCouponDate);
                actualTable.Should().Contain($"Eerste coupondatum\t{formattedFirstCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Eerste coupondatum");
            }

            if (baseRateAltData.LastCouponDate != null && baseRateAltData.LastCouponDate != string.Empty)
            {
                var formattedLastCouponDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, baseRateAltData.LastCouponDate);
                actualTable.Should().Contain($"Laatste coupondatum\t{formattedLastCouponDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Laatste coupondatum");
            }

            if (baseRateAltData.InterestRRDate != null && baseRateAltData.InterestRRDate != string.Empty)
            {
                var formattedInterestRRDate = await _sharedDataTransforms.TransformDateToWrittenDate(DataTransformOptions.Offermail, baseRateAltData.InterestRRDate);
                actualTable.Should().Contain($"Spreadherzieningsdatum\t{formattedInterestRRDate}");
            }
            else
            {
                actualTable.Should().NotContain($"Spreadherzieningsdatum");
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
                if (baseRateCaseOfferData.Guarantor1 != null && baseRateCaseOfferData.Guarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{baseRateCaseOfferData.Guarantor1}");
                }
                else if (baseRateCaseOfferData.ExpectedGuarantor1 != null && baseRateCaseOfferData.ExpectedGuarantor1 != string.Empty)
                {
                    actualTable.Should().Contain($"Zekerheid\t{baseRateCaseOfferData.ExpectedGuarantor1}");
                }
                else
                {
                    actualTable.Should().NotContain($"Zekerheid");
                }
            }

            if (baseRateAltData.DealRemark != null && baseRateAltData.DealRemark != string.Empty)
            {
                actualTable.Should().Contain($"Opmerkingen\t{baseRateAltData.DealRemark}");
            }
            else
            {
                actualTable.Should().NotContain($"Opmerkingen");
            }
        }

        [Then("the displayed base rate email text should match the expected text")]
        public async Task ThenTheDisplayedBaseRateEmailTextShouldMatchTheExpectedText()
        {
            var baseRateCaseOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualEmailText = (string)_scenarioContext["savedActualEmailText"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            // ActualEmailText.Should().Contain("Hierbij ontvangt u onze offerte voor de herziening van uw huidige lening bij ons.");
            actualEmailText.Should().Contain("Hierbij prijzen wij de gevraagde offerte als volgt:");

            actualEmailText.Should().Contain($"Offerte Basisrentelening ({lalCaseNumber})");

            actualEmailText.Should().Contain("Wij horen graag of u hiervan gebruik wilt maken.");

            if (sicCode == "0030000000")
            {
                // WSW template
                // ActualEmailText.Should().Contain("Op deze lening zullen de �Algemene bepalingen voor geldleningen onder garantie van het Waarborgfonds Sociale Woningbouw d.d. 1 augustus 2021� van toepassing zijn. Deze bepalingen zijn te vinden op: https://www.wsw.nl/corporaties/over-deelnemerschap/leningdocumentatie");
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
                actualEmailText.Should().Contain("Op deze lening zullen de Algemene Voorwaarden NWB Bank en de Algemene Kredietvoorwaarden NWB Bank (13 juli 2023) van toepassing zijn. Deze voorwaarden zijn te vinden op: https://nwbbank.com/public-finance/algemene-voorwaarden.");
                actualEmailText.Should().Contain("Bij NWB Bank werken we continu aan het verbeteren en moderniseren van onze dienstverlening. Om onze producten en processen nog beter aan te laten sluiten op de huidige wet- en regelgeving, hebben we per 20 maart 2026 onze leningdocumentatie geactualiseerd. Nadere informatie hierover kunt u desgewenst vinden op https://nwbbank.com/public-finance/algemene-voorwaarden.");
            }

            actualEmailText.Should().Contain("Indien bovenstaand aanbod door u wordt geaccepteerd zal de transactie zijn gesloten. Bovenstaande modaliteiten zullen vervolgens worden uitgewerkt in een wijzigingsovereenkomst op de geldleningsovereenkomst die door onze afdeling Operations aan u zal worden toegestuurd. Indien u niet akkoord gaat met de offerte dient u contact op te nemen met de afdeling Public Finance om de afwikkeling verder af te stemmen.");
            actualEmailText.Should().Contain("Gelieve uw reactie middels een reply op deze e-mail.");
            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen. Bovenstaande is onder voorbehoud van typefouten.");

            // na uat moet deze tekst niet meer in loan update mails voorkomen:
            actualEmailText.Should().NotContain("NWB Bank kan pas tot storting van de lening overgaan nadat aan de toepasselijke voorwaarden voor uitbetaling is voldaan. Mocht op de relevante stortingsdatum niet aan de toepasselijke voorwaarden voor uitbetaling zijn voldaan, dan kan dit tot vertraagde storting van de hoofdsom leiden." +
           " NWB Bank kan in een dergelijk geval ook besluiten om de lening door te halen. In beide situaties bent u rente verschuldigd over het gehele bedrag van de lening die op de originele stortingsdatum had moeten worden uitbetaald vanaf die stortingsdatum tot aan de latere stortingsdatum dan wel, zoals van toepassing," +
           " de datum waarop door NWB Bank besloten wordt om de lening door te halen. Eventuele kosten die gemoeid zijn met het afwikkelen van deze transactie (inclusief eventuele kosten die zijn gemaakt door NWB Bank in het kader van afdekkingstransacties), zullen bij u in rekening worden gebracht.");

            actualEmailText.Should().Contain("Met de informatie in deze e-mail bedoelt de NWB Bank op generlei wijze een advies of een aanbeveling te geven. U bent zelf gehouden tot en verantwoordelijk voor het maken van de voor u relevante afwegingen (juridisch, financieel of anderszins) bij de keuze tot het al dan niet aangaan van deze transactie en om daarvoor zo nodig deskundig advies in te winnen.");

            // ActualEmailText.Should().Contain("Bovenstaande is onder voorbehoud van typefouten.");
            actualEmailText.Should().Contain("Met vriendelijke groet,");

            actualEmailText.Should().Contain("Nederlandse Waterschapsbank N.V.");
            actualEmailText.Should().Contain("Rooseveltplantsoen 3");
            actualEmailText.Should().Contain("2517 KR Den Haag");
            actualEmailText.Should().Contain("t");

            // ActualEmailText.Should().Contain("e pf@nwbbank.com");
            //  ActualEmailText.Should().Contain("e�pf@nwbbank.com");
            actualEmailText.Should().Contain("pf@nwbbank.com");
            actualEmailText.Should().Contain("w www.nwbbank.com");
        }
    }
}