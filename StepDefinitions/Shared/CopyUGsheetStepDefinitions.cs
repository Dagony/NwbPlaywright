using Aspose.Words.Fields;
using Castle.DynamicProxy.Generators;
using CsvHelper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Wordprocessing;
using Reqnroll.CommonModels;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Fields.FrontOffice.Deposit;
using TA.Playwright.PageObjects;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class CopyUGsheetStepDefinitions
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
        private readonly IVariableDataTransforms _variableDataTransforms;
        private readonly ILoanUpdateBillOfClosureObject _loanUpdateBillOfClosureObject;
        private readonly IFixedDataTransforms _fixedDataTransforms;
        private readonly IRejectionObject _rejectionObject;
        private readonly IPlaywrightDriver _playwrightDriver;

        public CopyUGsheetStepDefinitions(
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
            IVariableDataTransforms variableDataTransforms,
            ILoanUpdateBillOfClosureObject loanUpdateBillOfClosureObject,
            IFixedDataTransforms fixedDataTransforms,
            IRejectionObject rejectionObject)
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
            _sharedDataTransforms = sharedDataTransforms;
            _variableDataTransforms = variableDataTransforms;
            _loanUpdateBillOfClosureObject = loanUpdateBillOfClosureObject;
            _fixedDataTransforms = fixedDataTransforms;
            _rejectionObject = rejectionObject;
        }

        [Reqnroll.When(@"I make a CopyUGsheet fixed loan case with the following Offer Data")]
        public async Task WhenIMakeACopyUGsheetFixedLoanCaseWithTheFollowingOfferData(Reqnroll.DataTable table)
        {
            await _basePageObject.SelectLoanRequest(LoanRequest.Fixed);
            string dateYesterday = _sharedDataTransforms.GetDateYesterday();
            LongFinalSpreadFields data = table.CreateInstance<LongFinalSpreadFields>();
            var lalCaseID = await _finalSpreadObject.GetCaseID();

            _scenarioContext["savedlalCaseID"] = lalCaseID;
            await _finalSpreadObject.SetCreditLimit("250000000");
            await _finalSpreadObject.SetCDDReviewDate(dateYesterday);
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"This is a spreadcheck remark for case : " + lalCaseID);

            if (data.Guarantor1 != "empty")
            {
                await _finalSpreadObject.CheckGuarantorBox();
                await _finalSpreadObject.ClickAddGuarantorButton();
                await _finalSpreadObject.SetGuarantorField(data.Guarantor1);
            }
        }

        [Reqnroll.When(@"I fill the CopyUGsheet case with the following Offer information")]
        public async Task WhenIFillTheCopyUGsheetCaseWithTheFollowingOfferInformation(Reqnroll.DataTable table)
        {
            LongFinalSpreadFields data = table.CreateInstance<LongFinalSpreadFields>();
            string dateYesterday = _sharedDataTransforms.GetDateYesterday();
            _scenarioContext["savedTC"] = data.TC;
            await _finalSpreadObject.SetNotional(data.Notional);
            await _finalSpreadObject.SetDealMoment(dateYesterday);
        }

        [Reqnroll.When(@"I fill the CopyUGsheet case with the following Alternative standard fields")]
        public async Task WhenIFillTheCopyUGsheetCaseWithTheFollowingAlternativeStandardFields(Reqnroll.DataTable table)
        {
            LongFinalSpreadFields data = table.CreateInstance<LongFinalSpreadFields>();
            await _finalSpreadObject.SetStartDate(data.StartDate);
            await _finalSpreadObject.SetMaturityDate(data.MaturityDate);
            if (data.FirstCouponDate != null && data.FirstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(data.FirstCouponDate);
            }

            await _finalSpreadObject.SetCouponDay("27");
            if (data.InterestSpreadRRDate != null && data.InterestSpreadRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(data.InterestSpreadRRDate);
            }
        }

        [Reqnroll.When(@"I fill the CopyUGsheet case with the following Alternative Loan type fields")]
        public async Task WhenIFillTheCopyUGsheetCaseWithTheFollowingAlternativeLoanTypeFields(Reqnroll.DataTable table)
        {
            LongFinalSpreadFields data = table.CreateInstance<LongFinalSpreadFields>();
            await _finalSpreadObject.SetAmortisationType(data.AmortType);
            await _finalSpreadObject.SetCouponFrequency(data.CouponFreq);
            await _finalSpreadObject.SetSwap(false);
            await _finalSpreadObject.SetDayCount(data.DayCount);
            if (data.AmortType != "Bullet")
            {
                await _finalSpreadObject.SetNumberOfAmortisations(data.NumbOfAmort);
                await _finalSpreadObject.SetAmortisationFrequency("Quarterly");
            }
        }

        [Reqnroll.When(@"I fill the CopyUGsheet case with the following Alternative Spread fields for Fixed loan")]
        public async Task WhenIFillTheCopyUGsheetCaseWithTheFollowingAlternativeSpreadFieldsForFixedLoan(Reqnroll.DataTable table)
        {
            LongFinalSpreadFields data = table.CreateInstance<LongFinalSpreadFields>();
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            await _finalSpreadObject.SetSpreadH1("11");
            await _finalSpreadObject.ClickSaveH1Button();

            await _finalSpreadObject.SetFinalSpread(data.FinalSpread);
            await _finalSpreadObject.SetFinalSpreadComment($"FinalSpreadComment for case: {lalCaseID}");
            await _finalSpreadObject.ClickSaveFinalSpreadButton();
            await _finalSpreadObject.SetDealRemark(data.DealRemark);
            await _finalSpreadObject.SetMarketRate(data.MarketIRate);
            await _finalSpreadObject.ClickSaveMarketRate();
        }

        [Reqnroll.Then(@"The case will be paused at BoC phase to exert copy function manually")]
        public async Task ThenTheCaseWillBePausedAtBoCPhaseToExertCopyFunctionManually()
        {
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            var tCID = (string)_scenarioContext["savedTC"];
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _winLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await Task.Delay(600);
            await _basePageObject.LogOutSystem();
            await _loginPageObject.LoginAccountDeskUser2();

            await _basePageObject.SearchCaseNumberMainScreen(Fields.EnumsForTRX.WorkListType.Loan, lalCaseID);
            await _billOfClosureObject.OpenBillOfClosure();
            await Task.Delay(2000);
            await _billOfClosureObject.ClickCopyUGSheet();

            var text = await _billOfClosureObject.CopyDataFromUGfunction();
            await _billOfClosureObject.CreateNewCSVOutputFileUGCopy<FixedUGSheetFields>(text, tCID, lalCaseID);

            Console.WriteLine(text);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Test output succesful for TC: {tCID}, for case: {lalCaseID}");
            Console.WriteLine("-----------------------------------------");
        }

        [When("I click copy UG sheet and I insert it into an excel file for {string} loan type")]
        public async Task WhenIClickCopyUGSheetAndIInsertItIntoAnExcelFileForLoanType(string loanType)
        {
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            var tCID = (string)_scenarioContext["savedTC"];

            string valuesUGSheet;

            // per 1-1-2026 UG sheet rechtgetrokken naar Production sheet over alle loan types.
            await _loanUpdateBillOfClosureObject.ClickCopyUGSheet();
            valuesUGSheet = await _loanUpdateBillOfClosureObject.CopyDataFromUGfunction();

            if (loanType == "SHORT" || loanType == "DEPOSIT")
            {
                var filePath = await _billOfClosureObject.CreateNewCSVOutputFileUGCopy<ShortProductionSheetFields>(valuesUGSheet, tCID, lalCaseID);
                (string[] headerRow, IEnumerable<ShortProductionSheetFields> listVariableUGSheet) = await _billOfClosureObject.GetUGSheetDataFromCSVFile<ShortProductionSheetFields>(filePath);
                _scenarioContext["savedUGSheetValues"] = listVariableUGSheet;
                _scenarioContext["savedUGSheetHeaders"] = headerRow;
            }
            else if (loanType != "SHORT" || loanType != "DEPOSIT")
            {
                var filePath = await _billOfClosureObject.CreateNewCSVOutputFileUGCopy<LongProductionSheetFields>(valuesUGSheet, tCID, lalCaseID);
                (string[] headerRow, IEnumerable<LongProductionSheetFields> listFixedUGSheet) = await _billOfClosureObject.GetUGSheetDataFromCSVFile<LongProductionSheetFields>(filePath);
                _scenarioContext["savedUGSheetValues"] = listFixedUGSheet;
                _scenarioContext["savedUGSheetHeaders"] = headerRow;
            }
            else
            {
                throw new Exception("Unknown option chosen for loantype in Reqnroll step method WhenIClickCopyUGSheetAndIInsertItIntoAnExcelFileForLoanType(string loanType)");
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Test output succesful for TC: {tCID}, for case: {lalCaseID}");
            Console.WriteLine("-----------------------------------------");
        }

        [When("I click the production sheet button and insert the string into an excel file for a rejected loan")]
        public async Task WhenIClickTheProductionSheetButtonAndInsertTheStringIntoAnExcelFileForARejectedLoan()
        {
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            var tCID = (string)_scenarioContext["savedTC"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            string valuesUGSheet;
            await _rejectionObject.ClickProductionSheet();
            valuesUGSheet = await _loanUpdateBillOfClosureObject.CopyDataFromUGfunction();

            var filePath = await _billOfClosureObject.CreateNewCSVOutputFileUGCopy<RepaymentProductionSheetFields>(valuesUGSheet, tCID, lalCaseID);
            (string[] headerRow, IEnumerable<RepaymentProductionSheetFields> listRejectionUGSheet) = await _billOfClosureObject.GetUGSheetDataFromCSVFile<RepaymentProductionSheetFields>(filePath);
            _scenarioContext["savedUGSheetValues"] = listRejectionUGSheet;
            _scenarioContext["savedUGSheetHeaders"] = headerRow;

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Test output succesful for TC: {tCID}, for {loanType} case: {lalCaseID}");
            Console.WriteLine("-----------------------------------------");
        }

        [Reqnroll.When(@"I click copy UG sheet and I insert it into an excel file for Variable loan type")]
        public async Task WhenIClickCopyUGSheetAndIInsertItIntoAnExcelFileForVariableLoanType()
        {
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            var tCID = (string)_scenarioContext["savedTC"];
            await _billOfClosureObject.ClickCopyUGSheet();

            var text = await _billOfClosureObject.CopyDataFromUGfunction();
            await _billOfClosureObject.CreateNewCSVOutputFileUGCopy<VariableUGSheetFields>(text, tCID, lalCaseID);

            Console.WriteLine(text);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Test output succesful for TC: {tCID}, for case: {lalCaseID}");
            Console.WriteLine("-----------------------------------------");
        }

        [Then("the values should match the expected values for the FIXED UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheFIXEDUGSHEET()
        {
            var fixedOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            string currentDate = _sharedDataTransforms.GetCurrentTransactionDate(EnumsForTRX.DataTransformOptions.UGsheet);

            var numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedAdvisoryName = (string)_scenarioContext["savedLegalNameAdvisory"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var expectedDealMoment = (string)_scenarioContext["savedDealMoment"];

            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, customerRate);

            IEnumerable<LongProductionSheetFields> listUGSHEET = (IEnumerable<LongProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];

            await _billOfClosureObject.PrintDataRowsForUGSHEET(listUGSHEET);

            if (expectedCustomerRate.Contains(","))
            {
                expectedCustomerRate = expectedCustomerRate.Replace(",", ".");
            }

            string formattedFinalSpread;

            if (fixedAltData.FinalSpread.Contains(","))
            {
                formattedFinalSpread = fixedAltData.FinalSpread.Replace(",", ".");
            }
            else
            {
                formattedFinalSpread = fixedAltData.FinalSpread + ".00";
            }

            string expectedGuarantor;
            if (fixedOfferData.Guarantor1 != null && fixedOfferData.Guarantor1 != string.Empty)
            {
                expectedGuarantor = fixedOfferData.Guarantor1;
            }
            else if (fixedOfferData.ExpectedGuarantor1 != null && fixedOfferData.ExpectedGuarantor1 != string.Empty)
            {
                expectedGuarantor = fixedOfferData.ExpectedGuarantor1;
            }
            else
            {
                expectedGuarantor = string.Empty;
            }

            foreach (var item in listUGSHEET)
            {
                item.CaseNumber.Should().Be(lalCaseNumber);
                item.Afdeling.Should().BeNullOrEmpty();

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.Product.Should().Be("Traditional fixed rate loan Long");
                    item.Event.Should().Be("Interest reset");
                }
                else
                {
                    item.Product.Should().Be("Traditional fixed rate loan Long");
                    item.Event.Should().Be("Primary");
                }

                item.DatumDealMoment.Should().Be(expectedDealMoment);

                Console.WriteLine("Mark wil de sicCode zien: " + sicCode);
                Console.WriteLine("Mark wil de expectedGuarantor zien: " + expectedGuarantor);
                Console.WriteLine("Mark wil de item.Sector zien: " + item.Sector);

                if (sicCode == "0024000000")
                {
                    item.Sector.Should().Be("gr-0%");
                }
                else if (sicCode == "0024100000")
                {
                    item.Sector.Should().Be("gr-20%");
                }
                else if (expectedGuarantor == "Stichting Waarborgfonds Sociale Woningbouw")
                {
                    item.Sector.Should().Be("wsw");
                }
                else if (expectedGuarantor == "Waarborgfonds voor de Zorgsector")
                {
                    item.Sector.Should().Be("wfz");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor == string.Empty && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != string.Empty)
                {
                    item.Sector.Should().Be("garantie");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                if (fixedOfferData.Notional != null && fixedOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = fixedOfferData.Notional.Replace(",", ".");
                    item.Hoofdsom.Should().Be(notionalWithoutComma);
                }

                if (fixedOfferData.ExpectedNotional != null && fixedOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = fixedOfferData.ExpectedNotional.Replace(",", ".");
                    item.Hoofdsom.Should().Be(expectednotionalWithoutComma);
                }

                item.Start.Should().Be(fixedAltData.StartDate);
                item.Eind.Should().Be(fixedAltData.MaturityDate);

                if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
                {
                    item.RenteSpreadherziening.Should().Be(fixedAltData.InterestRRDate);
                }
                else
                {
                    item.RenteSpreadherziening.Should().BeNullOrEmpty();
                }

                item.Amortizatie.Should().Be(fixedAltData.AmortType);

                if (fixedAltData.AmortType != "Bullet")
                {
                    item.AmortFrequency.Should().Be(fixedAltData.AmortFreq);
                }
                else
                {
                    item.AmortFrequency.Should().BeNullOrEmpty();
                }

                item.Euribor.Should().BeNullOrEmpty();
                item.Spread.Should().BeNullOrEmpty();
                item.Fee.Should().BeNullOrEmpty();
                item.BasisSwap3mEvs6mE.Should().BeNullOrEmpty();
                item.Spread3mE.Should().BeNullOrEmpty();

                item.Funding6mE.Should().BeNullOrEmpty();
                item.MargeTovFunding6mE.Should().BeNullOrEmpty();
                item.Sweging.Should().BeNullOrEmpty();
                item.ISF.Should().BeNullOrEmpty();
                item.RWA.Should().BeNullOrEmpty();

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.OriginalLoanNumber.Should().Be(fixedOfferData.ExpectedOriginalSecID);
                }
                else
                {
                    item.OriginalLoanNumber.Should().BeNullOrEmpty();
                }

                if (fixedAltData.DealRemark != null && fixedAltData.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().ContainEquivalentOf(fixedAltData.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }
            }
        }

        [Then("the values should match the expected values for the BASERATE UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheBASERATEUGSHEET()
        {
            var baseRateOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            string currentDate = _sharedDataTransforms.GetCurrentTransactionDate(EnumsForTRX.DataTransformOptions.UGsheet);

            var numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedAdvisoryName = (string)_scenarioContext["savedLegalNameAdvisory"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var expectedDealMoment = (string)_scenarioContext["savedDealMoment"];

            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, customerRate);
            var formattedExpectedSecID = _sharedDataTransforms.TransformOriginalSecID(baseRateOfferData.ExpectedOriginalSecID);
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            IEnumerable<LongProductionSheetFields> listUGSHEET = (IEnumerable<LongProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];

            await _billOfClosureObject.PrintDataRowsForUGSHEET(listUGSHEET);
            string formattedFinalSpread;

            if (baseRateAltData.FinalSpread.Contains(","))
            {
                formattedFinalSpread = baseRateAltData.FinalSpread.Replace(",", ".");
            }
            else
            {
                formattedFinalSpread = baseRateAltData.FinalSpread + ".00";
            }

            string expectedGuarantor;

            if (baseRateOfferData.Guarantor1 != null && baseRateOfferData.Guarantor1 != string.Empty)
            {
                expectedGuarantor = baseRateOfferData.Guarantor1;
            }
            else if (baseRateOfferData.ExpectedGuarantor1 != null && baseRateOfferData.ExpectedGuarantor1 != string.Empty)
            {
                expectedGuarantor = baseRateOfferData.ExpectedGuarantor1;
            }
            else
            {
                expectedGuarantor = string.Empty;
            }

            foreach (var item in listUGSHEET)
            {
                item.CaseNumber.Should().Be(lalCaseNumber);
                item.Afdeling.Should().BeNullOrEmpty();

                // huidig template alleen voor loan update - base rate
                item.Product.Should().Be("Base rate loan");
                item.Event.Should().Be("Spread reset");

                item.DatumDealMoment.Should().Be(expectedDealMoment);

                if (sicCode == "0024000000")
                {
                    item.Sector.Should().Be("gr-0%");
                }
                else if (sicCode == "0024100000")
                {
                    item.Sector.Should().Be("gr-20%");
                }
                else if (expectedGuarantor == "Stichting Waarborgfonds Sociale Woningbouw")
                {
                    item.Sector.Should().Be("wsw");
                }
                else if (expectedGuarantor == "Waarborgfonds voor de Zorgsector")
                {
                    item.Sector.Should().Be("wfz");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor == string.Empty && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != string.Empty)
                {
                    item.Sector.Should().Be("garantie");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                if (baseRateOfferData.Notional != null && baseRateOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = baseRateOfferData.Notional.Replace(",", ".");
                    decimal decimalNotional = decimal.Parse(notionalWithoutComma, CultureInfo.InvariantCulture);
                    var roundedNotional = Math.Round(decimalNotional);
                    var formattedNotional = roundedNotional.ToString();
                    item.Hoofdsom.Should().Be(formattedNotional);
                }

                if (baseRateOfferData.ExpectedNotional != null && baseRateOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = baseRateOfferData.ExpectedNotional.Replace(",", ".");
                    item.Hoofdsom.Should().Be(expectednotionalWithoutComma);
                }

                item.Start.Should().Be(baseRateAltData.StartDate);
                item.Eind.Should().Be(baseRateAltData.MaturityDate);

                if (baseRateAltData.InterestRRDate != null && baseRateAltData.InterestRRDate != string.Empty)
                {
                    item.RenteSpreadherziening.Should().Be(baseRateAltData.InterestRRDate);
                }
                else
                {
                    item.RenteSpreadherziening.Should().BeNullOrEmpty();
                }

                item.Amortizatie.Should().Be(baseRateAltData.ExpectedAmortType);

                if (baseRateAltData.ExpectedAmortType != "Bullet")
                {
                    item.AmortFrequency.Should().Be(baseRateAltData.ExpectedAmortFreq);
                }
                else
                {
                    item.AmortFrequency.Should().BeNullOrEmpty();
                }

                item.MaturityInYears.Should().BeNullOrEmpty();
                item.Customerrate.Should().Be(baseRateAltData.BaseRate);
                item.Euribor.Should().BeNullOrEmpty();
                item.Spread.Should().BeNullOrEmpty();
                item.Fee.Should().BeNullOrEmpty();
                item.BasisSwap3mEvs6mE.Should().BeNullOrEmpty();
                item.Spread3mE.Should().BeNullOrEmpty();
                item.Spread6mE.Should().Be(formattedFinalSpread);

                item.Funding6mE.Should().BeNullOrEmpty();
                item.MargeTovFunding6mE.Should().BeNullOrEmpty();
                item.Sweging.Should().BeNullOrEmpty();
                item.ISF.Should().BeNullOrEmpty();
                item.RWA.Should().BeNullOrEmpty();
                item.OriginalLoanNumber.Should().Be(baseRateOfferData.ExpectedOriginalSecID);

                if (baseRateAltData.DealRemark != null && baseRateAltData.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().ContainEquivalentOf(baseRateAltData.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }
            }
        }

        [Then("the values should match the expected values for the VARIABLE UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheVARIABLEUGSHEET()
        {
            var variableOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var variabledAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];

            var numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            var doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedAdvisoryName = (string)_scenarioContext["savedLegalNameAdvisory"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var expectedDealMoment = (string)_scenarioContext["savedDealMoment"];

            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            var expectedFinalSpread = await _variableDataTransforms.TransformSingleSpreadToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, variabledAltData.FinalSpread);
            var expectedCustomerRate = await _variableDataTransforms.TransformCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, doubleCustomerRate);

            var listUGSHEET = (IEnumerable<LongProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];
            await _billOfClosureObject.PrintDataRowsForUGSHEET(listUGSHEET);
            string expectedGuarantor;

            if (variableOfferData.Guarantor1 != null && variableOfferData.Guarantor1 != string.Empty)
            {
                expectedGuarantor = variableOfferData.Guarantor1;
            }
            else if (variableOfferData.ExpectedGuarantor1 != null && variableOfferData.ExpectedGuarantor1 != string.Empty)
            {
                expectedGuarantor = variableOfferData.ExpectedGuarantor1;
            }
            else
            {
                expectedGuarantor = string.Empty;
            }

            foreach (var item in listUGSHEET)
            {
                item.CaseNumber.Should().Be(lalCaseNumber);
                item.Afdeling.Should().BeNullOrEmpty();

                if (bool.Parse(variableOfferData.SetNotionalTypeToFixed) == true)
                {
                    item.Product.Should().BeEquivalentTo("Floating rate loan Fixed notional");
                }
                else
                {
                    item.Product.Should().BeEquivalentTo("Floating rate loan Floating notional");
                }

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.Event.Should().Be("Spread reset");
                }
                else
                {
                    item.Event.Should().Be("Primary");
                }

                item.DatumDealMoment.Should().Be(expectedDealMoment);

                if (sicCode == "0024000000")
                {
                    item.Sector.Should().Be("gr-0%");
                }
                else if (sicCode == "0024100000")
                {
                    item.Sector.Should().Be("gr-20%");
                }
                else if (expectedGuarantor == "Stichting Waarborgfonds Sociale Woningbouw")
                {
                    item.Sector.Should().Be("wsw");
                }
                else if (expectedGuarantor == "Waarborgfonds voor de Zorgsector")
                {
                    item.Sector.Should().Be("wfz");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != string.Empty)
                {
                    item.Sector.Should().Be("garantie");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                if (variableOfferData.Notional != null && variableOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = variableOfferData.Notional.Replace(",", ".");
                    item.Hoofdsom.Should().Be(notionalWithoutComma);
                }

                if (variableOfferData.ExpectedNotional != null && variableOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = variableOfferData.ExpectedNotional.Replace(",", ".");
                    item.Hoofdsom.Should().Be(expectednotionalWithoutComma);
                }

                item.Start.Should().Be(variabledAltData.StartDate);
                item.Eind.Should().Be(variabledAltData.MaturityDate);

                if (variabledAltData.InterestRRDate != null && variabledAltData.InterestRRDate != string.Empty)
                {
                    item.RenteSpreadherziening.Should().Be(variabledAltData.InterestRRDate);
                }
                else
                {
                    item.RenteSpreadherziening.Should().BeNullOrEmpty();
                }

                item.Amortizatie.Should().Be("Manual");

                if (loanType == "INTERESTSPREADRESET")
                {
                    var chooseAmortFreq = variabledAltData.ReferenceRate switch
                    {
                        "1wE" => item.AmortFrequency.Should().Be("Weekly"),
                        "1mE" => item.AmortFrequency.Should().Be("Monthly"),
                        "3mE" => item.AmortFrequency.Should().Be("Quarterly"),
                        "6mE" => item.AmortFrequency.Should().Be("Semi annual"),
                        "12mE" => item.AmortFrequency.Should().Be("Annual"),
                        _ => item.Amortizatie.Should().BeNullOrEmpty(),
                    };
                }
                else
                {
                    item.AmortFrequency.Should().BeNullOrEmpty();
                }

                item.MaturityInYears.Should().BeNullOrEmpty();
                item.Customerrate.Should().BeNullOrEmpty();
                item.Euribor.Should().Be(variabledAltData.ReferenceRate);
                item.Spread.Should().Be(expectedCustomerRate);
                item.Fee.Should().BeNullOrEmpty();
                item.BasisSwap3mEvs6mE.Should().BeNullOrEmpty();
                item.Spread3mE.Should().BeNullOrEmpty();
                item.Spread6mE.Should().Be(expectedFinalSpread);
                item.Funding6mE.Should().BeNullOrEmpty();
                item.MargeTovFunding6mE.Should().BeNullOrEmpty();
                item.Sweging.Should().BeNullOrEmpty();
                item.ISF.Should().BeNullOrEmpty();
                item.RWA.Should().BeNullOrEmpty();

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.OriginalLoanNumber.Should().Be(variableOfferData.ExpectedOriginalSecID);
                }
                else
                {
                    item.OriginalLoanNumber.Should().BeNullOrEmpty();
                }

                if (variabledAltData.DealRemark != null && variabledAltData.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().ContainEquivalentOf(variabledAltData.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }
            }
        }

        [Then("the values should match the expected values for loantype {string} for the REJECTION UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForLoantypeForTheREJECTIONUGSHEET(string loantype)
        {
            var listUGSHEET = (IEnumerable<RepaymentProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];
            await _billOfClosureObject.PrintDataRowsForUGSHEET(listUGSHEET);

            var numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            var doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedAdvisoryName = (string)_scenarioContext["savedLegalNameAdvisory"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            var rejectionComment = (string)_scenarioContext["savedRejectionComment"];
            var secID = (string)_scenarioContext["savedLoanUpdateSecID"];

            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedTransactionDate = _sharedDataTransforms.GetCurrentTransactionDate(DataTransformOptions.UGsheet);
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];

            var dealMoment = (string)_scenarioContext["savedDealMoment"];

            dynamic offerData;
            dynamic altdata;

            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
            {
                offerData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                altdata = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            }
            else if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional || productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                offerData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                altdata = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            }
            else
            {
                offerData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
                altdata = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            }

            string expectedGuarantor;
            if (offerData.Guarantor1 != null && offerData.Guarantor1 != string.Empty)
            {
                expectedGuarantor = offerData.Guarantor1;
            }
            else if (offerData.ExpectedGuarantor1 != null && offerData.ExpectedGuarantor1 != string.Empty)
            {
                expectedGuarantor = offerData.ExpectedGuarantor1;
            }
            else
            {
                expectedGuarantor = string.Empty;
            }

            var notionalWithoutComma = offerData.Notional.Replace(",", ".");
            var expectedNotionalWithoutComma = offerData.ExpectedNotional.Replace(",", ".");

            foreach (var item in listUGSHEET)
            {
                item.CaseNumber.Should().Be(lalCaseNumber);
                item.Afdeling.Should().BeNullOrEmpty();
                item.Product.Should().BeOneOf("Base rate loan", "Traditional fixed rate loan Long", "Floating rate loan Floating notional", "Floating rate loan Fixed notional");

                item.Event.Should().BeOneOf("Spread reset", "Interest reset");

                item.DatumDealMoment.Should().Be(dealMoment);

                if (sicCode == "0024000000")
                {
                    item.Sector.Should().Be("gr-0%");
                }
                else if (sicCode == "0024100000")
                {
                    item.Sector.Should().Be("gr-20%");
                }
                else if (expectedGuarantor == "Stichting Waarborgfonds Sociale Woningbouw")
                {
                    item.Sector.Should().Be("wsw");
                }
                else if (expectedGuarantor == "Waarborgfonds voor de Zorgsector")
                {
                    item.Sector.Should().Be("wfz");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (expectedGuarantor != "Waarborgfonds voor de Zorgsector" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (expectedGuarantor != string.Empty)
                {
                    item.Sector.Should().Be("garantie");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                item.Hoofdsom.Should().BeOneOf(notionalWithoutComma, expectedNotionalWithoutComma);
                item.Aflosbedrag.Should().BeNullOrEmpty();
                item.AflosDatum.Should().Be(altdata.StartDate);
                item.LeningNummer.Should().Be(offerData.ExpectedOriginalSecID);

                if (altdata.DealRemark != null && altdata.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().Be(altdata.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }

                item.TegenPartij.Should().BeNullOrEmpty();
                item.Rente.Should().BeNullOrEmpty();
                item.VariabelePoot.Should().BeNullOrEmpty();
                item.UnwindBedrag.Should().BeNullOrEmpty();
                item.OpmerkingenHedge.Should().BeNullOrEmpty();
            }
        }

        [Then("the values should match the expected values for the SHORT UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheSHORTUGSHEET()
        {
            var shortOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var shortedAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var caseNumber = (string)_scenarioContext["savedlalCaseID"];

            var numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            var doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedAdvisoryName = (string)_scenarioContext["savedLegalNameAdvisory"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var expectedDealMoment = (string)_scenarioContext["savedDealMoment"];

            var sicCode = props[16].GetValue(fieldsObject).ToString();

            var expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, doubleCustomerRate);

            var listProductionSheet = (IEnumerable<ShortProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];
            await _billOfClosureObject.PrintDataRowsForUGSHEET(listProductionSheet);

            var notionalWithoutComma = shortOfferData.Notional.Replace(",", ".");
            string formattedMargin;

            if (shortedAltData.Margin.Contains(","))
            {
                formattedMargin = shortedAltData.Margin.Replace(",", ".");
            }
            else
            {
                formattedMargin = shortedAltData.Margin;
            }

            string maturityInYears = _sharedDataTransforms.CalculateMaturityInYears(shortedAltData.StartDate, shortedAltData.MaturityDate);
            var expectedCDDreviewDate = (string)_scenarioContext["savedCDDreviewDate"];

            foreach (var item in listProductionSheet)
            {
                item.CaseNumber.Should().Be(caseNumber);
                item.Afdeling.Should().BeNullOrEmpty();
                item.Product.Should().Be("Traditional fixed rate loan Short");
                item.Event.Should().Be("Primary");
                item.TransactionDate.Should().Be(expectedDealMoment);

                if (sicCode == "0024000000")
                {
                    item.Sector.Should().Be("gr-0%");
                }
                else if (sicCode == "0024100000")
                {
                    item.Sector.Should().Be("gr-20%");
                }
                else if (shortOfferData.Guarantor1 == "Stichting Waarborgfonds Sociale Woningbouw")
                {
                    item.Sector.Should().Be("wsw");
                }
                else if (shortOfferData.Guarantor1 == "Waarborgfonds voor de Zorgsector")
                {
                    item.Sector.Should().Be("wfz");
                }
                else if (shortOfferData.Guarantor1 != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (shortOfferData.Guarantor1 != "Waarborgfonds voor de Zorgsector" && sicCode == "0023000000")
                {
                    item.Sector.Should().Be("wp");
                }
                else if (shortOfferData.Guarantor1 != "Waarborgfonds voor de Zorgsector" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (shortOfferData.Guarantor1 != "Stichting Waarborgfonds Sociale Woningbouw" && sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else if (shortOfferData.Guarantor1 != string.Empty)
                {
                    item.Sector.Should().Be("garantie");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                item.Hoofdsom.Should().Be(notionalWithoutComma);
                item.Start.Should().Be(shortedAltData.StartDate);
                item.Eind.Should().Be(shortedAltData.MaturityDate);
                item.CustomerRate.Should().Be(expectedCustomerRate);
                item.MargeInPunten.Should().Be(formattedMargin);

                if (shortedAltData.DealRemark != null && shortedAltData.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().ContainEquivalentOf(shortedAltData.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }
            }
        }

        [Then("the values should match the expected values for the DEPOSIT UGSHEET")]
        public async Task ThenTheValuesShouldMatchTheExpectedValuesForTheDEPOSITUGSHEET()
        {
            var depositOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var depositAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var expectedCounterParty = (string)_scenarioContext["savedCounterParty"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var expectedDealMoment = (string)_scenarioContext["savedDealMoment"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            var expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.UGsheet, doubleCustomerRate);
            var caseNumber = (string)_scenarioContext["savedlalCaseID"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();

            var listProductionSheet = (IEnumerable<ShortProductionSheetFields>)_scenarioContext["savedUGSheetValues"];
            string[] headerRow = (string[])_scenarioContext["savedUGSheetHeaders"];
            await _billOfClosureObject.PrintDataRowsForUGSHEET(listProductionSheet);

            var notionalWithoutComma = depositOfferData.Notional.Replace(",", ".");
            string formattedMargin;

            if (depositAltData.Margin.Contains(","))
            {
                formattedMargin = depositAltData.Margin.Replace(",", ".");
            }
            else
            {
                formattedMargin = depositAltData.Margin;
            }

            foreach (var item in listProductionSheet)
            {
                item.CaseNumber.Should().Be(caseNumber);
                item.Afdeling.Should().BeNullOrEmpty();
                item.Product.Should().Be("Deposit");
                item.Event.Should().Be("Primary");
                item.TransactionDate.Should().Be(expectedDealMoment);

                if (sicCode == "0022000000")
                {
                    item.Sector.Should().Be("gem");
                }
                else
                {
                    item.Sector.Should().BeNullOrEmpty();
                }

                item.Geldnemer.Should().Be(expectedCounterParty);
                item.Hoofdsom.Should().Be(notionalWithoutComma);
                item.Start.Should().Be(depositAltData.StartDate);
                item.Eind.Should().Be(depositAltData.MaturityDate);
                item.CustomerRate.Should().Be(expectedCustomerRate);
                item.MargeInPunten.Should().Be(formattedMargin);

                if (depositAltData.DealRemark != null && depositAltData.DealRemark != string.Empty)
                {
                    item.Opmerking.Should().ContainEquivalentOf(depositAltData.DealRemark);
                }
                else
                {
                    item.Opmerking.Should().BeNullOrWhiteSpace();
                }
            }
        }
    }
}