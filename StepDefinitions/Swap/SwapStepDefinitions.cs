using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Gherkin;
using Reqnroll;
using System;
using System.Globalization;
using System.Reflection;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Fields.FrontOffice.Long;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.TRX.FrontOffice;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class SwapStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly IFixedModel _fixedModel;
        private readonly ISharedModel _sharedModel;
        private readonly ICaptureSwapObject _captureSwapObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;

        public SwapStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            IFixedModel fixedModel,
            ISharedModel sharedModel,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ICaptureSwapObject captureSwapObject,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject)
        {
            _playwrightDriver = playwrightDriver;
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _winLostCorrectObject = winLostCorrectObject;
            _fixedModel = fixedModel;
            _sharedModel = sharedModel;
            _captureSwapObject = captureSwapObject;
            _billOfClosureObject = billOfClosureObject;
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
        }

        [When("I approve the case and continue to the CaptureSwap page")]
        public async Task WhenIApproveTheCaseAndContinueToTheCaptureSwapPage()
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var guarantor1 = (string)_scenarioContext["savedGuarantor1"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var sicCode = props[15].GetValue(fieldsObject).ToString();

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully offer mail sent for case: " + lalCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();

            // Indien WFZ Flow worden er extra velden gevuld op het WinLostCorrect scherm i.v.m. het WFZ Contract
            if (sicCode == "0031000000" || guarantor1 == "Stichting Waarborgfonds voor de Zorgsector")
            {
                if (loanType != "SHORT")
                {
                    var suretyDate = _sharedDataTransforms.GetDateYesterday();
                    await _winLostCorrectObject.SetSuretyStatementNumberAndDate("99999999999999999911", suretyDate);
                }
                else if (loanType == "SHORT")
                {
                    await _winLostCorrectObject.SetSuretyStatementNumber("99999999999999999933");
                }
            }

            await _winLostCorrectObject.UploadDocuments();
            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await _basePageObject.LogOutSystem();
            bool spreadcheck = (bool)_scenarioContext["savedSpreadCheck"];

            if (spreadcheck == true)
            {
                await _loginPageObject.LoginAccountDeskUser1();
            }
            else
            {
                await _loginPageObject.LoginAccountDeskUser2();
            }

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseNumber);
            await _billOfClosureObject.OpenBillOfClosure();
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            await _basePageObject.OpenCaseOnScreen(lalCaseNumber);
            await Task.Delay(1000);

            var (swapCaseNumber, allCaseNumber) = await _billOfClosureObject.GetSWAPandALLCaseNumber();
            _scenarioContext["savedSWAPCaseNumber"] = swapCaseNumber;
            _scenarioContext["savedALLCaseNumber"] = allCaseNumber;
            await _basePageObject.LogOutSystem();

            if (spreadcheck == true)
            {
                await _loginPageObject.LoginAccountDeskUser2();
            }
            else
            {
                await _loginPageObject.LoginAccountDeskUser1();
            }

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.SWAP, swapCaseNumber);
            await _captureSwapObject.OpenCaptureSwapLink();
        }

        [When("I click the Swap Copy button and copy the string for {string} loan type")]
        public async Task WhenIClickTheSwapCopyButtonAndCopyTheStringForLoanType(string loanType)
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var tcID = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var filePath = _testSettings.DownloadFilePath;
            string swapString;
            await Task.Delay(500);

            var stringSavedNotional = (string)_scenarioContext["savedNotional"];
            if (stringSavedNotional != string.Empty)
            {
                if (int.Parse((string)_scenarioContext["savedNotional"]) > 100000000)
                {
                    await Task.Delay(500);
                    await _finalSpreadObject.HighNotionalDealApproved(true);
                    await _finalSpreadObject.HighNotionalSetNameOfApprover("Playwright approved this deal!");
                    await _finalSpreadObject.HighNotionalProcurationMail();
                }
            }

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _finalSpreadObject.ClickCopySwap();
            swapString = await _finalSpreadObject.CopyDataFromSWAPCopyfunction();
            _scenarioContext["savedSWAPValues"] = swapString;

            Console.WriteLine($"SWAP values succesfully outputted to Excel file for: {tcID} on {currentDateTime}");
            Console.WriteLine($">>PRINTING ACTUAL SWAP TEXT BELOW FOR CASE: {lalCaseNumber}<<");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(swapString);
            Console.WriteLine("----------------------------------------------------------------------------------");

            var filepathScreenshot = @$"{filePath}//TC {tcID} {loanType} CASE {lalCaseNumber} SWAPCopyFunction {currentDateTime}.png";
            _scenarioContext["savedEmailScreenshot"] = filepathScreenshot;

            await _basePageObject.CreateScreenshotSWAP(filepathScreenshot);
        }

        [Then("the values should match the expected values for the Swap Copy string")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheSwapCopyString()
        {
            string swapString = (string)_scenarioContext["savedSWAPValues"];
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var eventType = (string)_scenarioContext["savedEventType"];
            var cultureInfo = new CultureInfo("en-US");
            var expectedFormattedStartDate = _sharedDataTransforms.TransformDate(cultureInfo, fixedAltData.StartDate);
            string expectedFormattedEndDate;

            if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
            {
                expectedFormattedEndDate = _sharedDataTransforms.TransformDate(cultureInfo, fixedAltData.InterestRRDate);
            }
            else
            {
                expectedFormattedEndDate = _sharedDataTransforms.TransformDate(cultureInfo, fixedAltData.MaturityDate);
            }

            string expectedFormattedNotional;
            string expectedDayCount;

            if (eventType == "Interest reset")
            {
                if (fixedOfferData.ExpectedNotional.EndsWith("000000"))
                {
                    expectedFormattedNotional = fixedOfferData.ExpectedNotional.Substring(0, fixedOfferData.ExpectedNotional.Length - 6) + " mln";
                }
                else
                {
                    expectedFormattedNotional = fixedOfferData.ExpectedNotional;
                    expectedFormattedNotional = _sharedDataTransforms.FormatNotional(cultureInfo, expectedFormattedNotional);
                }

                expectedDayCount = fixedAltData.ExpectedDayCount switch
                {
                    "Act/Act" => "Act/Act",
                    "Act/360" => "Act/360",
                    "30/360" => "30/360",
                    "ACT365" => "Act/Act ISDA",
                    "ACTUAL" => "Act/Act",
                    "A365" => "Act/365",
                    _ => throw new Exception("Unknown option chosen for switch expression ExpectedDayCount from origin Swap Copy assertion specflow class"),
                };
            }
            else
            {
                if (fixedOfferData.Notional.EndsWith("000000"))
                {
                    expectedFormattedNotional = fixedOfferData.Notional.Substring(0, fixedOfferData.Notional.Length - 6) + " mln";
                }
                else
                {
                    expectedFormattedNotional = fixedOfferData.Notional;
                    expectedFormattedNotional = _sharedDataTransforms.FormatNotional(cultureInfo, expectedFormattedNotional);
                }

                expectedDayCount = fixedAltData.DayCount switch
                {
                    "Act/Act" => "Act/Act",
                    "Act/360" => "Act/360",
                    "30/360" => "30/360",
                    _ => throw new Exception("Unknown option chosen for switch expression ExpectedDayCount from origin Swap Copy assertion specflow class"),
                };
            }

            string expectedAmortType;

            if (fixedAltData.AmortType == "Bullet")
            {
                expectedAmortType = "bullet";
            }
            else if (fixedAltData.AmortType == "Linear")
            {
                expectedAmortType = "linear to zero";
            }
            else
            {
                expectedAmortType = string.Empty;
            }

            var couponFreqOption = fixedAltData.CouponFreq switch
            {
                "Annual" => CouponFrequency.Annual,
                "Semi annual" => CouponFrequency.Semiannual,
                "Quarterly" => CouponFrequency.Quarterly,
                "Monthly" => CouponFrequency.Monthly,
                "Weekly" => CouponFrequency.Weekly,
                _ => throw new Exception("Unknown option chosen for switch expression ExpectedDayCount from origin Swap Copy assertion specflow class"),
            };

            // first en last coupondate gevuld
            if (fixedAltData.FirstCouponDate != null && fixedAltData.FirstCouponDate != string.Empty && fixedAltData.LastCouponDate != null && fixedAltData.LastCouponDate != string.Empty)
            {
                string expectedFirstCouponText = _sharedDataTransforms.ConvertDateToChosenFrequencyStringMonths(cultureInfo, DataTransformOptions.SwapCopyFunction, couponFreqOption, fixedAltData.FirstCouponDate);
                string expectedOutcome = $"pls quote: \nEUR {expectedFormattedNotional}, start {expectedFormattedStartDate}, end {expectedFormattedEndDate}, {expectedAmortType}\nRolling on {expectedFirstCouponText}, ??? last stub\nNWB pays fixed {fixedAltData.CouponFreq.ToLower()} {expectedDayCount} {fixedAltData.BusinessDayConvention} vs. {fixedAltData.SwapReferenceRate}\n{fixedAltData.ClearingHouse} cleared";
                swapString.Should().Be(expectedOutcome);
            }

            // firstcoupondate gevuld, last leeg
            else if ((fixedAltData.FirstCouponDate != null && fixedAltData.FirstCouponDate != string.Empty) && (fixedAltData.LastCouponDate == null || fixedAltData.LastCouponDate == string.Empty))
            {
                string expectedFirstCouponText = _sharedDataTransforms.ConvertDateToChosenFrequencyStringMonths(cultureInfo, DataTransformOptions.SwapCopyFunction, couponFreqOption, fixedAltData.FirstCouponDate);
                swapString.Should().Be($"pls quote: \nEUR {expectedFormattedNotional}, start {expectedFormattedStartDate}, end {expectedFormattedEndDate}, {expectedAmortType}\nRolling on {expectedFirstCouponText}\nNWB pays fixed {fixedAltData.CouponFreq.ToLower()} {expectedDayCount} {fixedAltData.BusinessDayConvention} vs. {fixedAltData.SwapReferenceRate}\n{fixedAltData.ClearingHouse} cleared");
            }

            // firstcoupondate leeg, lastcoupondate gevuld
            else if ((fixedAltData.FirstCouponDate == null || fixedAltData.FirstCouponDate == string.Empty) && (fixedAltData.LastCouponDate != null && fixedAltData.LastCouponDate != string.Empty))
            {
                swapString.Should().Be($"pls quote: \nEUR {expectedFormattedNotional}, start {expectedFormattedStartDate}, end {expectedFormattedEndDate}, {expectedAmortType}\nRolling on ??? last stub\nNWB pays fixed {fixedAltData.CouponFreq.ToLower()} {expectedDayCount} {fixedAltData.BusinessDayConvention} vs. {fixedAltData.SwapReferenceRate}\n{fixedAltData.ClearingHouse} cleared");
            }

            // first en last leeg
            else
            {
                swapString.Should().Be($"pls quote: \nEUR {expectedFormattedNotional}, start {expectedFormattedStartDate}, end {expectedFormattedEndDate}, {expectedAmortType}\nNWB pays fixed {fixedAltData.CouponFreq.ToLower()} {expectedDayCount} {fixedAltData.BusinessDayConvention} vs. {fixedAltData.SwapReferenceRate}\n{fixedAltData.ClearingHouse} cleared");
            }
        }
    }
}