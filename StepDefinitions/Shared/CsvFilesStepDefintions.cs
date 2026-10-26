using Aspose.Words.Fields;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class CsvFilesStepDefinitions
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
        private readonly IVariableModel _variableModel;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly IFixedDataTransforms _fixedDataTransforms;
        private readonly IVariableDataTransforms _variableDataTransforms;
        private readonly IPlaywrightDriver _playwrightDriver;

        public CsvFilesStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ILoanDataApprovalObject loanDataApprovalObject,
            IValidateRequestObject validateRequestObject,
            ISurePayCheckObject surePayCheckObject,
            ICreateContractObject createContractObject,
            IAgreementReviewObject agreementReviewObject,
            ICheckGuarantorDocumentObject checkGuarantorDocumentObject,
            IGuarantorApprovalObject guarantorApprovalObject,
            ISendAgreementObject sendAgreementObject,
            IClientApprovalObject clientApprovalObject,
            IProcessClientResponseObject processClientResponseObject,
            ILegalReviewObject legalReviewObject,
            ICheckDossierCompleteObject checkDossierCompleteObject,
            IVariableModel variableModel,
            ISharedDataTransforms sharedDataTransforms,
            IFixedDataTransforms fixedDataTransforms,
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
            _loanDataApprovalObject = loanDataApprovalObject;
            _validateRequestObject = validateRequestObject;

            // _specFlowOutputHelper = outputHelper;
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
            _variableModel = variableModel;
            _sharedDataTransforms = sharedDataTransforms;
            _fixedDataTransforms = fixedDataTransforms;
            _variableDataTransforms = variableDataTransforms;
        }

        private string formattedStartDate = string.Empty;

        [Then(@"the assertions for each testcase for SECM fixed loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMFixedLoanCaseIsValid()
        {
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var filePathSECM = (string)_scenarioContext["savedFilePathSECM"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];

            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            string expectedCouprate8 = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.OpicsFiles, customerRate);

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECM(filePathSECM);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            var loanType = (string)_scenarioContext["savedLoanType"];

            var formattedStartDate = string.Empty;
            if (fixedAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                var dtCurrentStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtCurrentStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                var dtStartDate = DateTime.ParseExact(fixedAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            foreach (var item in dataRow)
            {
                item.SECID.Should().StartWith("1");
                item.SECID.Should().Be(expectedSecID);

                var assertBASIS = fixedAltData.DayCount switch
                {
                    "Act/Act" => item.BASIS.Should().Be("ACTUAL"),
                    "Act/360" => item.BASIS.Should().Be("A360"),
                    "30/360" => item.BASIS.Should().Be("EBOND"),
                    "ACT365" => item.BASIS.Should().Be("ACT365"),
                    "A365" => item.BASIS.Should().Be("A365"),
                    "ACTUAL" => item.BASIS.Should().Be("ACTUAL"),
                    "BOND" => item.BASIS.Should().Be("BOND"),
                    "EBOND" => item.BASIS.Should().Be("EBOND"),
                    _ => throw new Exception("Unknown option chosen for switch expression assertBASIS"),
                };
                item.CCY.Should().Be("EUR");
                item.COUPRATE_8.Should().Be(expectedCouprate8);

                var assertEXCHCNOandFINCENT = sicCode switch
                {
                    "0030000000" => (item.EXCHCNO.Should().Be("0697332241"), item.FINCENT.Should().BeNullOrEmpty()), // WSW
                    "0031000000" => (item.EXCHCNO.Should().Be("0636907813"), item.FINCENT.Should().BeNullOrEmpty()), // WFZ
                    "0023000000" => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().Be("RC")), // Waterschap
                    _ => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().BeNullOrEmpty()),
                };

                if (fixedAltData.FirstCouponDate != null && fixedAltData.FirstCouponDate != string.Empty)
                {
                    var dtFirstCouponDate = DateTime.ParseExact(fixedAltData.FirstCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedCouponDate = dtFirstCouponDate.ToString("yyyy-MM-dd");
                    item.FIRSTIPAYDATE.Should().Be(formattedCouponDate);
                }
                else if (fixedAltData.FirstCouponDate == null || fixedAltData.FirstCouponDate == string.Empty)
                {
                    item.FIRSTIPAYDATE.Should().BeNullOrEmpty();
                }

                if (fixedAltData.LastCouponDate != null && fixedAltData.LastCouponDate != string.Empty)
                {
                    var dtLastCouponDate = DateTime.ParseExact(fixedAltData.LastCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedLastCouponDate = dtLastCouponDate.ToString("yyyy-MM-dd");
                    item.LASTIPAYDATE.Should().Be(formattedLastCouponDate);
                }
                else if (fixedAltData.LastCouponDate == null || fixedAltData.LastCouponDate == string.Empty)
                {
                    item.LASTIPAYDATE.Should().BeNullOrEmpty();
                }

                item.INTENDDATERULE.Should().Be("D");
                var assertPMTFREQandINTPAYCYCLE = fixedAltData.CouponFreq switch
                {
                    "Annual" => (item.PMTFREQ.Should().Be("A"), item.INTPAYCYCLE.Should().Be("A")),
                    "Semi annual" => (item.PMTFREQ.Should().Be("S"), item.INTPAYCYCLE.Should().Be("S")),
                    "Quarterly" => (item.PMTFREQ.Should().Be("Q"), item.INTPAYCYCLE.Should().Be("Q")),
                    "Monthly" => (item.PMTFREQ.Should().Be("M"), item.INTPAYCYCLE.Should().Be("M")),
                    "Weekly" => (item.PMTFREQ.Should().Be("W"), item.INTPAYCYCLE.Should().Be("W")),
                    _ => throw new Exception("Unknown option chosen for switch expression assertPMTFREQandINTPAYCYCLE"),
                };

                var assertSECUNITandINTCALCRULEandINTPAYRULE = fixedAltData.AmortType switch
                {
                    "Annuity" => (item.SECUNIT.Should().Be("FMT"), item.INTCALCRULE.Should().Be("ANN"), item.INTPAYRULE.Should().Be("D")),
                    "Annuity balloon" => (item.SECUNIT.Should().Be("FMT"), item.INTCALCRULE.Should().Be("ANN"), item.INTPAYRULE.Should().Be("D")),
                    _ => (item.SECUNIT.Should().Be("BON"), item.INTCALCRULE.Should().Be("NPV"), item.INTPAYRULE.Should().NotBe("D")),
                };

                if (fixedAltData.AmortType != "Annuity" && fixedAltData.AmortType != "Annuity balloon")
                {
                    if (sicCode == "0030000000")
                    {
                        item.INTPAYRULE.Should().Be("M"); // WSW
                    }
                    else
                    {
                        item.INTPAYRULE.Should().Be("S");
                    }
                }
                else
                {
                    item.INTPAYRULE.Should().Be("D");
                }

                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.ISSDATE.Should().Be("2020-12-01");
                }
                else
                {
                    item.ISSDATE.Should().Be(formattedStartDate);
                }

                if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
                {
                    var dtInterestSpreadRRDate = DateTime.ParseExact(fixedAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattednterestSpreadRRDate);
                }
                else if (fixedAltData.InterestRRDate == null || fixedAltData.InterestRRDate == string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(fixedAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattedMaturityDate);
                }

                if (fixedAltData.AmortType != "Bullet")
                {
                    item.PRINPMTFREQ.Should().NotBeNullOrEmpty();
                    var assertPRINPMTFREQ = fixedAltData.AmortFreq switch
                    {
                        "Annual" => item.PRINPMTFREQ.Should().Be("A"),
                        "Semi annual" => item.PRINPMTFREQ.Should().Be("S"),
                        "Quarterly" => item.PRINPMTFREQ.Should().Be("Q"),
                        "Monthly" => item.PRINPMTFREQ.Should().Be("M"),
                        "Weekly" => item.PRINPMTFREQ.Should().Be("W"),
                        _ => throw new Exception("Unknown option chosen for switch expression asserPRINPMTFREQ"),
                    };
                }
                else if (fixedAltData.AmortType == "Bullet")
                {
                    item.NOAMORTS.Should().BeNullOrEmpty();
                    item.PRINPMTFREQ.Should().BeNullOrEmpty();
                }

                item.PARAMT.Should().Be("100");
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.RATECODE.Should().BeNullOrEmpty();
                item.SETTCCY.Should().Be("EUR");
                item.SPREADRATE_8.Should().BeNullOrEmpty();
                item.VDATE.Should().Be(formattedStartDate);

                // item.ALLOWNEGATIVINT.Should().Be("N");
            }
        }

        [Then(@"the assertions for each testcase for SECMNWB fixed loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMNWBFixedLoanCaseIsValid()
        {
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];

            var filePathSECMNWB = (string)_scenarioContext["savedFilePathSECMNWB"];
            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECMNWB(filePathSECMNWB);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            var loanType = (string)_scenarioContext["savedLoanType"];

            foreach (var item in dataRow)
            {
                var currentDateTime = _sharedDataTransforms.GetCurrentTransactionDate(DataTransformOptions.OpicsFiles);
                item.SECID.Should().StartWith("1");
                item.SECID.Should().Be(expectedSecID);

                if (loanType == "INTERESTSPREADRESET")
                {
                    if (fixedCaseOfferData.Guarantor2 != null && fixedCaseOfferData.Guarantor2 != string.Empty)
                    {
                        item.Achtervang.Should().Be(fixedCaseOfferData.Guarantor2);
                    }
                    else
                    {
                        item.Achtervang.Should().BeNullOrEmpty();
                    }
                }
                else
                {
                    item.Achtervang.Should().BeNullOrEmpty();
                }

                if (sicCode == "0030000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else if (sicCode == "0031000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else
                {
                    if (fixedCaseOfferData.Guarantor1 != null && fixedCaseOfferData.Guarantor1 != string.Empty)
                    {
                        item.GUARANTOR.Should().NotBeNullOrEmpty();
                    }
                    else
                    {
                        item.GUARANTOR.Should().BeNullOrEmpty();
                    }
                }

                if (fixedCaseOfferData.GuarantorRefNumber != null && fixedCaseOfferData.GuarantorRefNumber != string.Empty)
                {
                    item.GUARANTOR_REF_NO.Should().Be(fixedCaseOfferData.GuarantorRefNumber);
                }
                else
                {
                    item.GUARANTOR_REF_NO.Should().BeNullOrEmpty();
                }

                item.AFBRF.Should().Be(currentDateTime);
                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.MINVARNOTIONAL.Should().BeNullOrEmpty();
                item.MAXVARNOTIONAL.Should().BeNullOrEmpty();
                item.LOANTYPE.Should().BeOneOf("NORMAAL", "RENTEHERZIENING", "EXTENDABLE");

                if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(fixedAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

                    var dtInterestSpreadRRDate = DateTime.ParseExact(fixedAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");

                    item.CE_DATUM.Should().Be(formattedMaturityDate);
                    item.RH_DATUM.Should().Be(formattednterestSpreadRRDate);
                }
                else if (fixedAltData.InterestRRDate == null || fixedAltData.InterestRRDate == string.Empty)
                {
                    item.CE_DATUM.Should().BeNullOrEmpty();
                    item.RH_DATUM.Should().BeNullOrEmpty();
                }

                var assertSCHEMATYPE = fixedAltData.AmortType switch
                {
                    "Bullet" => item.SCHEMATYPE.Should().Be("BULLET"),
                    "Linear" => item.SCHEMATYPE.Should().Be("LINEAIR"),
                    "Annuity" => item.SCHEMATYPE.Should().Be("ANNUITEIT"),
                    "Linear balloon" => item.SCHEMATYPE.Should().Be("LINEAIR BALLON"),
                    "Annuity balloon" => item.SCHEMATYPE.Should().Be("ANNUITEIT BALLON"),
                    "Manual" => item.SCHEMATYPE.Should().Be("HANDMATIG"),
                    "Zero" => item.SCHEMATYPE.Should().Be("ZERO"),
                    _ => throw new Exception("Unknown option chosen for switch expression assertSCHEMATYPE"),
                };
            }
        }

        [Then(@"the assertions for each testcase for BRED fixed loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForBREDFixedLoanCaseIsValid()
        {
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var filePathBRED = (string)_scenarioContext["savedFilePathBRED"];
            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataBRED(filePathBRED);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            var expectedSecID = (string)_scenarioContext["savedSecID"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            var formattedStartDate = string.Empty;
            DateTime dtStartDate;
            if (fixedAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                dtStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                dtStartDate = DateTime.ParseExact(fixedAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            foreach (var item in dataRow)
            {
                item.SECID.Should().StartWith("1");
                item.SECID.Should().Be(expectedSecID);

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.BROK.Should().Be("0000000998");
                }
                else
                {
                    item.BROK.Should().Be("0000000999");
                }

                item.CNO.Should().NotBeNullOrEmpty();
                item.CNO.Should().Be(cifNumber);

                item.COST.Should().BeOneOf("100", "200", "300");

                var totalBusinessDays = await _loanDataApprovalObject.CalculateBusinessDaysFromCurrentDateToStartDate(dtStartDate);
                if (totalBusinessDays <= 5)
                {
                    item.DELAYDELIVIND.Should().Be("N");
                }
                else if (totalBusinessDays > 5)
                {
                    item.DELAYDELIVIND.Should().Be("Y");
                }

                item.INVTYPE.Should().Be("H");

                if (fixedCaseOfferData.Notional != null && fixedCaseOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = fixedCaseOfferData.Notional.Replace(",", ".");
                    var roundedNotional = Math.Round(decimal.Parse(notionalWithoutComma), 0, MidpointRounding.AwayFromZero);
                    var formattedNotional = roundedNotional.ToString();
                    item.ORIGQTY.Should().Be(formattedNotional);
                }

                if (fixedCaseOfferData.ExpectedNotional != null && fixedCaseOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = fixedCaseOfferData.ExpectedNotional.Replace(",", ".");
                    var expectedroundedNotional = Math.Round(decimal.Parse(expectednotionalWithoutComma), 0, MidpointRounding.AwayFromZero);
                    var expectedformattedNotional = expectedroundedNotional.ToString();
                    item.ORIGQTY.Should().Be(expectedformattedNotional);
                }

                item.PORT.Should().Be("PORT");
                item.PRICE_8.Should().Be("100");
                item.PS.Should().Be("P");
                item.SECSACCT.Should().Be("NWB");
                item.SETTDATE.Should().Be(formattedStartDate);

                var chooseTraderAssertion1 = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => item.TRAD.Should().Be("TADA"),
                    TestUser.AccountDesk2 => item.TRAD.Should().Be("TADB"),
                    TestUser.Treasury1 => item.TRAD.Should().Be("TTRA"),
                    TestUser.Treasury2 => item.TRAD.Should().Be("TTRB"),
                    TestUser.TreasuryManager1 => item.TRAD.Should().Be("TTMA"),
                    TestUser.TreasuryManager2 => item.TRAD.Should().Be("TTMB"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT DEPLO csv specflow step"),
                };

                item.TRAD.Should().NotBeNullOrEmpty();
            }
        }

        [Then("the assertions for each testcase for SECM baserate loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMBaserateLoanCaseIsValid()
        {
            var baseRateCaseOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            var filePathSECM = (string)_scenarioContext["savedFilePathSECM"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];

            string expectedCouprate8 = _variableDataTransforms.TransformBaseRateAndFinalSpreadandBaseRateToCustomerRate(DataTransformOptions.OpicsFiles, customerRate, baseRateAltData.BaseRate);
            var loanType = (string)_scenarioContext["savedLoanType"];

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECM(filePathSECM);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);

            var formattedStartDate = string.Empty;
            if (baseRateAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                var dtCurrentStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtCurrentStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                var dtStartDate = DateTime.ParseExact(baseRateAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            foreach (var item in dataRow)
            {
                item.SECID.Should().Be(expectedSecID);

                var assertBASIS = baseRateAltData.ExpectedDayCount switch
                {
                    "Act/Act" => item.BASIS.Should().Be("ACTUAL"),
                    "Act/360" => item.BASIS.Should().Be("A360"),
                    "30/360" => item.BASIS.Should().Be("EBOND"),
                    "ACT365" => item.BASIS.Should().Be("ACT365"),
                    "A365" => item.BASIS.Should().Be("A365"),
                    "ACTUAL" => item.BASIS.Should().Be("ACTUAL"),
                    "BOND" => item.BASIS.Should().Be("BOND"),
                    "EBOND" => item.BASIS.Should().Be("EBOND"),
                    _ => throw new Exception("Unknown option chosen for switch expression assertBASIS"),
                };
                item.CCY.Should().Be("EUR");
                item.COUPRATE_8.Should().Be(expectedCouprate8);

                var assertEXCHCNOandFINCENT = sicCode switch
                {
                    "0030000000" => (item.EXCHCNO.Should().Be("0697332241"), item.FINCENT.Should().BeNullOrEmpty()), // WSW
                    "0031000000" => (item.EXCHCNO.Should().Be("0636907813"), item.FINCENT.Should().BeNullOrEmpty()), // WFZ
                    "0023000000" => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().Be("RC")), // Waterschap
                    _ => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().BeNullOrEmpty()),
                };

                if (baseRateAltData.FirstCouponDate != null && baseRateAltData.FirstCouponDate != string.Empty)
                {
                    var dtFirstCouponDate = DateTime.ParseExact(baseRateAltData.FirstCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedCouponDate = dtFirstCouponDate.ToString("yyyy-MM-dd");
                    item.FIRSTIPAYDATE.Should().Be(formattedCouponDate);
                }
                else if (baseRateAltData.FirstCouponDate == null || baseRateAltData.FirstCouponDate == string.Empty)
                {
                    item.FIRSTIPAYDATE.Should().BeNullOrEmpty();
                }

                if (baseRateAltData.LastCouponDate != null && baseRateAltData.LastCouponDate != string.Empty)
                {
                    var dtLastCouponDate = DateTime.ParseExact(baseRateAltData.LastCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedLastCouponDate = dtLastCouponDate.ToString("yyyy-MM-dd");
                    item.LASTIPAYDATE.Should().Be(formattedLastCouponDate);
                }
                else if (baseRateAltData.LastCouponDate == null || baseRateAltData.LastCouponDate == string.Empty)
                {
                    item.LASTIPAYDATE.Should().BeNullOrEmpty();
                }

                item.INTENDDATERULE.Should().Be("D");
                var assertPMTFREQandINTPAYCYCLE = baseRateAltData.ExpectedCouponFreq switch
                {
                    "Annual" => (item.PMTFREQ.Should().Be("A"), item.INTPAYCYCLE.Should().Be("A")),
                    "Semi annual" => (item.PMTFREQ.Should().Be("S"), item.INTPAYCYCLE.Should().Be("S")),
                    "Quarterly" => (item.PMTFREQ.Should().Be("Q"), item.INTPAYCYCLE.Should().Be("Q")),
                    "Monthly" => (item.PMTFREQ.Should().Be("M"), item.INTPAYCYCLE.Should().Be("M")),
                    "Weekly" => (item.PMTFREQ.Should().Be("W"), item.INTPAYCYCLE.Should().Be("W")),
                    _ => throw new Exception("Unknown option chosen for switch expression assertPMTFREQandINTPAYCYCLE"),
                };

                var assertSECUNITandINTCALCRULEandINTPAYRULE = baseRateAltData.ExpectedAmortType switch
                {
                    "Annuity" => (item.SECUNIT.Should().Be("FMT"), item.INTCALCRULE.Should().Be("ANN"), item.INTPAYRULE.Should().Be("D")),
                    "Annuity balloon" => (item.SECUNIT.Should().Be("FMT"), item.INTCALCRULE.Should().Be("ANN"), item.INTPAYRULE.Should().Be("D")),
                    _ => (item.SECUNIT.Should().Be("BON"), item.INTCALCRULE.Should().Be("NPV"), item.INTPAYRULE.Should().NotBe("D")),
                };

                if (baseRateAltData.ExpectedAmortType != "Annuity" && baseRateAltData.ExpectedAmortType != "Annuity balloon")
                {
                    if (sicCode == "0030000000")
                    {
                        item.INTPAYRULE.Should().Be("M"); // WSW
                    }
                    else
                    {
                        item.INTPAYRULE.Should().Be("S");
                    }
                }
                else
                {
                    item.INTPAYRULE.Should().Be("D");
                }

                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);
                if (loanType == "INTERESTSPREADRESET")
                {
                    item.ISSDATE.Should().Be("2020-12-01");
                }
                else
                {
                    item.ISSDATE.Should().Be(formattedStartDate);
                }

                if (baseRateAltData.InterestRRDate != null && baseRateAltData.InterestRRDate != string.Empty)
                {
                    var dtInterestSpreadRRDate = DateTime.ParseExact(baseRateAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattednterestSpreadRRDate);
                }
                else if (baseRateAltData.InterestRRDate == null || baseRateAltData.InterestRRDate == string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(baseRateAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattedMaturityDate);
                }

                if (baseRateAltData.ExpectedAmortType != "Bullet")
                {
                    item.NOAMORTS.Should().Be(baseRateAltData.ExpectedNumbOfAmort);
                    item.PRINPMTFREQ.Should().NotBeNullOrEmpty();
                    var assertPRINPMTFREQ = baseRateAltData.ExpectedAmortFreq switch
                    {
                        "Annual" => item.PRINPMTFREQ.Should().Be("A"),
                        "Semi annual" => item.PRINPMTFREQ.Should().Be("S"),
                        "Quarterly" => item.PRINPMTFREQ.Should().Be("Q"),
                        "Monthly" => item.PRINPMTFREQ.Should().Be("M"),
                        "Weekly" => item.PRINPMTFREQ.Should().Be("W"),
                        _ => throw new Exception("Unknown option chosen for switch expression asserPRINPMTFREQ"),
                    };
                }
                else if (baseRateAltData.ExpectedAmortType == "Bullet")
                {
                    item.NOAMORTS.Should().BeNullOrEmpty();
                    item.PRINPMTFREQ.Should().BeNullOrEmpty();
                }

                item.PARAMT.Should().Be("100");
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.RATECODE.Should().BeNullOrEmpty();

                item.SETTCCY.Should().Be("EUR");
                item.SPREADRATE_8.Should().BeNullOrEmpty();
                item.VDATE.Should().Be(formattedStartDate);
                item.ALLOWNEGATIVINT.Should().Be("N");
            }
        }

        [Then("the assertions for each testcase for SECMNWB baserate loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMNWBBaserateLoanCaseIsValid()
        {
            var baseRateCaseOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];

            var filePathSECMNWB = (string)_scenarioContext["savedFilePathSECMNWB"];
            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECMNWB(filePathSECMNWB);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            var loanType = (string)_scenarioContext["savedLoanType"];

            foreach (var item in dataRow)
            {
                var currentDateTime = _sharedDataTransforms.GetCurrentTransactionDate(DataTransformOptions.OpicsFiles);
                item.SECID.Should().Be(expectedSecID);

                if (loanType == "INTERESTSPREADRESET")
                {
                    if (baseRateCaseOfferData.Guarantor2 != null && baseRateCaseOfferData.Guarantor2 != string.Empty)
                    {
                        item.Achtervang.Should().Be(baseRateCaseOfferData.Guarantor2);
                    }
                    else
                    {
                        item.Achtervang.Should().BeNullOrEmpty();
                    }
                }
                else
                {
                    if (sicCode == "0030000000")
                    {
                        item.Achtervang.Should().Be("0636999777");
                    }
                    else
                    {
                        item.Achtervang.Should().BeNullOrEmpty();
                    }
                }

                if (sicCode == "0030000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else if (sicCode == "0031000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else
                {
                    if (baseRateCaseOfferData.Guarantor1 != null && baseRateCaseOfferData.Guarantor1 != string.Empty)
                    {
                        item.GUARANTOR.Should().NotBeNullOrEmpty();
                    }
                    else
                    {
                        item.GUARANTOR.Should().BeNullOrEmpty();
                    }
                }

                if (baseRateCaseOfferData.GuarantorRefNumber != null && baseRateCaseOfferData.GuarantorRefNumber != string.Empty)
                {
                    item.GUARANTOR_REF_NO.Should().Be(baseRateCaseOfferData.GuarantorRefNumber);
                }
                else
                {
                    item.GUARANTOR_REF_NO.Should().BeNullOrEmpty();
                }

                item.AFBRF.Should().Be(currentDateTime);
                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.MINVARNOTIONAL.Should().BeNullOrEmpty();
                item.MAXVARNOTIONAL.Should().BeNullOrEmpty();

                item.LOANTYPE.Should().BeOneOf("BASISRENTE", "BASIS EXTENDABLE");

                if (baseRateAltData.InterestRRDate != null && baseRateAltData.InterestRRDate != string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(baseRateAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

                    var dtInterestSpreadRRDate = DateTime.ParseExact(baseRateAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");

                    item.CE_DATUM.Should().Be(formattedMaturityDate);
                    item.RH_DATUM.Should().Be(formattednterestSpreadRRDate);
                }
                else if (baseRateAltData.InterestRRDate == null || baseRateAltData.InterestRRDate == string.Empty)
                {
                    item.CE_DATUM.Should().BeNullOrEmpty();
                    item.RH_DATUM.Should().BeNullOrEmpty();
                }

                var assertSCHEMATYPE = baseRateAltData.ExpectedAmortType switch
                {
                    "Bullet" => item.SCHEMATYPE.Should().Be("BULLET"),
                    "Linear" => item.SCHEMATYPE.Should().Be("LINEAIR"),
                    "Annuity" => item.SCHEMATYPE.Should().Be("ANNUITEIT"),
                    "Linear balloon" => item.SCHEMATYPE.Should().Be("LINEAIR BALLON"),
                    "Annuity balloon" => item.SCHEMATYPE.Should().Be("ANNUITEIT BALLON"),
                    "Manual" => item.SCHEMATYPE.Should().Be("HANDMATIG"),
                    "Zero" => item.SCHEMATYPE.Should().Be("ZERO"),
                    _ => throw new Exception("Unknown option chosen for switch expression assertSCHEMATYPE"),
                };
            }
        }

        [Then("the assertions for each testcase for BRED baserate loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForBREDBaserateLoanCaseIsValid()
        {
            var baseRateCaseOfferData = (BaseRateLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var baseRateAltData = (BaseRateAlternativeFields)_scenarioContext["savedAltData"];
            var filePathBRED = (string)_scenarioContext["savedFilePathBRED"];
            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataBRED(filePathBRED);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            var expectedSecID = (string)_scenarioContext["savedSecID"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            var formattedStartDate = string.Empty;
            DateTime dtStartDate;
            if (baseRateAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                dtStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                dtStartDate = DateTime.ParseExact(baseRateAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            foreach (var item in dataRow)
            {
                item.SECID.Should().Be(expectedSecID);

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.BROK.Should().Be("0000000998");
                }
                else
                {
                    item.BROK.Should().Be("0000000999");
                }

                item.CNO.Should().NotBeNullOrEmpty();
                item.CNO.Should().Be(cifNumber);

                item.COST.Should().BeOneOf("100", "200", "300");

                var totalBusinessDays = await _loanDataApprovalObject.CalculateBusinessDaysFromCurrentDateToStartDate(dtStartDate);
                if (totalBusinessDays <= 5)
                {
                    item.DELAYDELIVIND.Should().Be("N");
                }
                else if (totalBusinessDays > 5)
                {
                    item.DELAYDELIVIND.Should().Be("Y");
                }

                item.INVTYPE.Should().Be("H");

                if (baseRateCaseOfferData.Notional != null && baseRateCaseOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = baseRateCaseOfferData.Notional.Replace(",", ".");
                    var roundedNotional = Math.Round(decimal.Parse(notionalWithoutComma), 0, MidpointRounding.AwayFromZero);
                    var formattedNotional = roundedNotional.ToString();
                    item.ORIGQTY.Should().Be(formattedNotional);
                }

                if (baseRateCaseOfferData.ExpectedNotional != null && baseRateCaseOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = baseRateCaseOfferData.ExpectedNotional.Replace(",", ".");
                    var expectedroundedNotional = Math.Round(decimal.Parse(expectednotionalWithoutComma), 0, MidpointRounding.AwayFromZero);
                    var expectedformattedNotional = expectedroundedNotional.ToString();
                    item.ORIGQTY.Should().Be(expectedformattedNotional);
                }

                item.PORT.Should().Be("PORT");
                item.PRICE_8.Should().Be("100");
                item.PS.Should().Be("P");
                item.SECSACCT.Should().Be("NWB");
                item.SETTDATE.Should().Be(formattedStartDate);

                var chooseTraderAssertion1 = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => item.TRAD.Should().Be("TADA"),
                    TestUser.AccountDesk2 => item.TRAD.Should().Be("TADB"),
                    TestUser.Treasury1 => item.TRAD.Should().Be("TTRA"),
                    TestUser.Treasury2 => item.TRAD.Should().Be("TTRB"),
                    TestUser.TreasuryManager1 => item.TRAD.Should().Be("TTMA"),
                    TestUser.TreasuryManager2 => item.TRAD.Should().Be("TTMB"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT DEPLO csv specflow step"),
                };

                item.TRAD.Should().NotBeNullOrEmpty();
            }
        }

        [Then(@"the assertions for each testcase for SECM variable loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMVariableLoanCaseIsValid()
        {
            var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var filePathSECM = (string)_scenarioContext["savedFilePathSECM"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECM(filePathSECM);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);

            var loanType = (string)_scenarioContext["savedLoanType"];

            var formattedStartDate = string.Empty;
            if (varAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                var dtCurrentStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtCurrentStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                var dtStartDate = DateTime.ParseExact(varAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            foreach (var item in dataRow)
            {
                if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == false)
                {
                    item.SECID.Should().StartWith("2");
                    item.SECID.Should().Be(expectedSecID);
                    item.INTENDDATERULE.Should().Be("M");
                }
                else if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == true)
                {
                    item.SECID.Should().StartWith("1");
                    item.SECID.Should().Be(expectedSecID);
                    item.INTENDDATERULE.Should().Be("D");
                }

                var assertBasis = varAltData.ExpectedDayCount switch
                {
                    "A360" => item.BASIS.Should().Be("A360"),
                    "EBOND" => item.BASIS.Should().Be("EBOND"),
                    _ => throw new Exception("Unknown option chosen for switch expression AssertBasis in Variable Loan Opics SECM.csv"),
                };

                item.CCY.Should().Be("EUR");
                item.COUPRATE_8.Should().BeNullOrEmpty();

                var assertEXCHCNOandFINCENT = sicCode switch
                {
                    "0030000000" => (item.EXCHCNO.Should().Be("0697332241"), item.FINCENT.Should().BeNullOrEmpty()), // WSW
                    "0031000000" => (item.EXCHCNO.Should().Be("0636907813"), item.FINCENT.Should().BeNullOrEmpty()), // WFZ
                    "0023000000" => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().Be("RC")), // Waterschap
                    _ => (item.EXCHCNO.Should().BeNullOrEmpty(), item.FINCENT.Should().BeNullOrEmpty()),
                };

                if (varAltData.FirstCouponDate != null && varAltData.FirstCouponDate != string.Empty)
                {
                    var dtFirstCouponDate = DateTime.ParseExact(varAltData.FirstCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedCouponDate = dtFirstCouponDate.ToString("yyyy-MM-dd");
                    item.FIRSTIPAYDATE.Should().Be(formattedCouponDate);
                }
                else if (varAltData.FirstCouponDate == null || varAltData.FirstCouponDate == string.Empty)
                {
                    item.FIRSTIPAYDATE.Should().BeNullOrEmpty();
                }

                if (varAltData.LastCouponDate != null && varAltData.LastCouponDate != string.Empty)
                {
                    var dtLastCouponDate = DateTime.ParseExact(varAltData.LastCouponDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedLastCouponDate = dtLastCouponDate.ToString("yyyy-MM-dd");
                    item.LASTIPAYDATE.Should().Be(formattedLastCouponDate);
                }
                else if (varAltData.LastCouponDate == null || varAltData.LastCouponDate == string.Empty)
                {
                    item.LASTIPAYDATE.Should().BeNullOrEmpty();
                }

                item.INTCALCRULE.Should().Be("NPV");

                var assertINTPAYCYCLEandRATECODEandPMTFREQ = varAltData.ReferenceRate switch
                {
                    "12mE" => (item.INTPAYCYCLE.Should().Be("A"), item.RATECODE.Should().Be("EUR12M"), item.PMTFREQ.Should().Be("A")),
                    "6mE" => (item.INTPAYCYCLE.Should().Be("S"), item.RATECODE.Should().Be("EUR6ME"), item.PMTFREQ.Should().Be("S")),
                    "3mE" => (item.INTPAYCYCLE.Should().Be("Q"), item.RATECODE.Should().Be("EUR3MD"), item.PMTFREQ.Should().Be("Q")),
                    "1mE" => (item.INTPAYCYCLE.Should().Be("M"), item.RATECODE.Should().Be("EUR1M"), item.PMTFREQ.Should().Be("M")),
                    "1wE" => (item.INTPAYCYCLE.Should().Be("W"), item.RATECODE.Should().Be("EUR1W"), item.PMTFREQ.Should().Be("W")),
                    _ => throw new Exception("Unknown option chosen for switch expression assertINTPAYCYCLEandRATECODEandPMTFREQ"),
                };

                item.INTPAYRULE.Should().Be("M");
                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.ISSDATE.Should().Be("2020-12-01");
                }
                else
                {
                    item.ISSDATE.Should().Be(formattedStartDate);
                }

                if (varAltData.InterestRRDate != null && varAltData.InterestRRDate != string.Empty)
                {
                    var dtInterestSpreadRRDate = DateTime.ParseExact(varAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattednterestSpreadRRDate);
                }
                else if (varAltData.InterestRRDate == null || varAltData.InterestRRDate == string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(varAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");
                    item.MDATE.Should().Be(formattedMaturityDate);
                }

                item.NOAMORTS.Should().BeNullOrEmpty();
                item.PRINPMTFREQ.Should().BeNullOrEmpty();
                item.PARAMT.Should().Be("100");
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.SPREADRATE_8.Should().Be(varAltData.CustomerRate);
                item.VDATE.Should().Be(formattedStartDate);
                item.SECUNIT.Should().Be("BON");

                if (varAltData.NegativeInterestRate != null && varAltData.NegativeInterestRate != string.Empty)
                {
                    bool negativeInterestRate = (bool)_scenarioContext["savedNegativeInterestRate"];

                    if (negativeInterestRate == false)
                    {
                        item.ALLOWNEGATIVINT.Should().Be("N");
                    }
                    else
                    {
                        item.ALLOWNEGATIVINT.Should().Be("Y");
                    }
                }
                else
                {
                    item.ALLOWNEGATIVINT.Should().Be("N");
                }
            }
        }

        [Then(@"the assertions for each testcase for SECMNWB variable loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForSECMNWBVariableLoanCaseIsValid()
        {
            var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var filePathSECMNWB = (string)_scenarioContext["savedFilePathSECMNWB"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            var expectedSecID = (string)_scenarioContext["savedSecID"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataSECMNWB(filePathSECMNWB);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);

            foreach (var item in dataRow)
            {
                var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd");

                if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == false)
                {
                    item.SECID.Should().StartWith("2");
                    item.SECID.Should().Be(expectedSecID);
                    item.LOANTYPE.Should().Be("VARIABELE HOOFDSOM");
                    item.MINVARNOTIONAL.Should().NotBeNullOrEmpty();
                    item.MAXVARNOTIONAL.Should().NotBeNullOrEmpty();
                }
                else if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == true)
                {
                    item.SECID.Should().StartWith("1");
                    item.SECID.Should().Be(expectedSecID);
                    item.LOANTYPE.Should().BeOneOf("NORMAAL", "RENTEHERZIENING");
                    item.MINVARNOTIONAL.Should().BeNullOrEmpty();
                    item.MAXVARNOTIONAL.Should().BeNullOrEmpty();
                }

                if (loanType == "INTERESTSPREADRESET")
                {
                    if (varCaseOfferData.Guarantor2 != null && varCaseOfferData.Guarantor2 != string.Empty)
                    {
                        item.Achtervang.Should().Be(varCaseOfferData.Guarantor2);
                    }
                    else
                    {
                        item.Achtervang.Should().BeNullOrEmpty();
                    }
                }
                else
                {
                    item.Achtervang.Should().BeNullOrEmpty();
                }

                if (sicCode == "0030000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else if (sicCode == "0031000000")
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else
                {
                    if (varCaseOfferData.Guarantor1 != null && varCaseOfferData.Guarantor1 != string.Empty)
                    {
                        item.GUARANTOR.Should().NotBeNullOrEmpty();
                    }
                    else
                    {
                        item.GUARANTOR.Should().BeNullOrEmpty();
                    }
                }

                if (varCaseOfferData.GuarantorRefNumber != null && varCaseOfferData.GuarantorRefNumber != string.Empty)
                {
                    item.GUARANTOR_REF_NO.Should().Be(varCaseOfferData.GuarantorRefNumber);
                }
                else
                {
                    item.GUARANTOR_REF_NO.Should().BeNullOrEmpty();
                }

                item.AFBRF.Should().Be(currentDateTime);
                item.ISSUER.Should().NotBeNullOrEmpty();
                item.ISSUER.Should().Be(cifNumber);
                item.PRODTYPE.Should().Be("UG");
                item.PRODUCT.Should().Be("OHLE");
                item.SCHEMATYPE.Should().Be("HANDMATIG");

                if (varAltData.InterestRRDate != null && varAltData.InterestRRDate != string.Empty)
                {
                    var dtMaturityDate = DateTime.ParseExact(varAltData.MaturityDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

                    var dtInterestSpreadRRDate = DateTime.ParseExact(varAltData.InterestRRDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                    var formattednterestSpreadRRDate = dtInterestSpreadRRDate.ToString("yyyy-MM-dd");

                    item.CE_DATUM.Should().Be(formattedMaturityDate);
                    item.RH_DATUM.Should().Be(formattednterestSpreadRRDate);
                }
                else if (varAltData.InterestRRDate == null || varAltData.InterestRRDate == string.Empty)
                {
                    item.CE_DATUM.Should().BeNullOrEmpty();
                    item.RH_DATUM.Should().BeNullOrEmpty();
                }
            }
        }

        [Then(@"the assertions for each testcase for BRED variable loan case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForBREDVariableLoanCaseIsValid()
        {
            var varCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var varAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var filePathBRED = (string)_scenarioContext["savedFilePathBRED"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var expectedSecID = (string)_scenarioContext["savedSecID"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            var formattedStartDate = string.Empty;
            DateTime dtStartDate;
            if (varAltData.StartDate == "today")
            {
                var currentStartDate = (string)_scenarioContext["savedStartDate"];
                dtStartDate = DateTime.ParseExact(currentStartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }
            else
            {
                dtStartDate = DateTime.ParseExact(varAltData.StartDate, "dd-M-yyyy", CultureInfo.InvariantCulture);
                formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            }

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataBRED(filePathBRED);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            foreach (var item in dataRow)
            {
                if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == false)
                {
                    item.SECID.Should().StartWith("2");
                    item.SECID.Should().Be(expectedSecID);
                }
                else if (bool.Parse(varCaseOfferData.SetNotionalTypeToFixed) == true)
                {
                    item.SECID.Should().StartWith("1");
                    item.SECID.Should().Be(expectedSecID);
                }

                if (loanType == "INTERESTSPREADRESET")
                {
                    item.BROK.Should().Be("0000000998");
                }
                else
                {
                    item.BROK.Should().Be("0000000999");
                }

                item.CNO.Should().NotBeNullOrEmpty();
                item.CNO.Should().Be(cifNumber);

                item.COST.Should().BeOneOf("100", "200", "300");

                var totalBusinessDays = await _loanDataApprovalObject.CalculateBusinessDaysFromCurrentDateToStartDate(dtStartDate);
                if (totalBusinessDays <= 5)
                {
                    item.DELAYDELIVIND.Should().Be("N");
                }
                else if (totalBusinessDays > 5)
                {
                    item.DELAYDELIVIND.Should().Be("Y");
                }

                item.INVTYPE.Should().Be("H");

                if (varCaseOfferData.Notional != null && varCaseOfferData.Notional != string.Empty)
                {
                    var notionalWithoutComma = varCaseOfferData.Notional.Replace(",", ".");
                    var roundedNotional = Math.Round(decimal.Parse(notionalWithoutComma));
                    var formattedNotional = roundedNotional.ToString();
                    item.ORIGQTY.Should().Be(formattedNotional);
                }

                if (varCaseOfferData.ExpectedNotional != null && varCaseOfferData.ExpectedNotional != string.Empty)
                {
                    var expectednotionalWithoutComma = varCaseOfferData.ExpectedNotional.Replace(",", ".");
                    var expectedroundedNotional = Math.Round(decimal.Parse(expectednotionalWithoutComma));
                    var expectedformattedNotional = expectedroundedNotional.ToString();
                    item.ORIGQTY.Should().Be(expectedformattedNotional);
                }

                item.PORT.Should().Be("PORT");
                item.PRICE_8.Should().Be("100");
                item.PS.Should().Be("P");
                item.SECSACCT.Should().Be("NWB");
                item.SETTDATE.Should().Be(formattedStartDate);
                item.TRAD.Should().NotBeNullOrEmpty();
            }
        }

        [Then(@"the assertions for each testcase for DEPLO short loan or deposit case is valid")]
        public async Task ThenTheAssertionsForEachTestcaseForDEPLOShortLoanOrDepositCaseIsValid()
        {
            var expectedOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var expectedAlternativeData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var filePathIDNL = (string)_scenarioContext["savedFilePathIDNL"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataIDNL(filePathIDNL);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            double doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            string expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions.OpicsFiles, doubleCustomerRate);
            var notionalWithoutComma = expectedOfferData.Notional.Replace(",", ".");

            foreach (var item in dataRow)
            {
                TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
                var dtMaturityDate = DateTime.ParseExact(expectedAlternativeData.MaturityDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

                var dtStartDate = DateTime.ParseExact(expectedAlternativeData.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");

                var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
                var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
                Console.WriteLine("Mark wil de props zien: ");
                for (int i = 0; i < props.Count; i++)
                {
                    Console.WriteLine($"[{i}] {props[i].Name} = {props[i].GetValue(fieldsObject)}");
                }

                var sicCode = props[16].GetValue(fieldsObject).ToString();
                var totalDays = (dtMaturityDate - dtStartDate).Days;
                if (loanType == "DEPOSIT")
                {
                    item.PRODTYPE.Should().Be("OG");
                    if (totalDays <= 7)
                    {
                        item.PRODUCT.Should().Be("CALLOG");
                    }
                    else if (totalDays > 7)
                    {
                        item.PRODUCT.Should().Be("KORTLE");
                    }
                }
                else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                {
                    item.PRODTYPE.Should().Be("UG");
                    if (totalDays <= 7)
                    {
                        item.PRODUCT.Should().Be("CALLUG");
                    }
                    else if (totalDays > 7)
                    {
                        item.PRODUCT.Should().Be("KORTLE");
                    }
                }
                else
                {
                    throw new Exception("Unknown loantype for item.PRODTYPE!");
                }

                var chooseTraderAssertion1 = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => item.TRAD.Should().Be("TADA"),
                    TestUser.AccountDesk2 => item.TRAD.Should().Be("TADB"),
                    TestUser.Treasury1 => item.TRAD.Should().Be("TTRA"),
                    TestUser.Treasury2 => item.TRAD.Should().Be("TTRB"),
                    TestUser.TreasuryManager1 => item.TRAD.Should().Be("TTMA"),
                    TestUser.TreasuryManager2 => item.TRAD.Should().Be("TTMB"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT DEPLO csv specflow step"),
                };

                item.VDATE.Should().Be(formattedStartDate);
                item.MDATE.Should().Be(formattedMaturityDate);
                item.CNO.Should().Be(cifNumber);
                item.CCY.Should().Be("EUR");
                item.CCYAMT.Should().Be(notionalWithoutComma);
                item.RATECODE.Should().Be("FIXED");
                item.BASIS.Should().Be("A360");

                item.SPREADRATE_8.Should().BeNullOrEmpty();
                item.BROK.Should().Be("BRZONDER");
                item.COST.Should().Be("300");
                item.ALLOWNEGINT.Should().Be("Y");

                if (expectedOfferData.Guarantor1 != null && expectedOfferData.Guarantor1 != string.Empty)
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else
                {
                    item.GUARANTOR.Should().BeNullOrEmpty();
                }

                Console.Write("Mark wil de sicCode weten: " + sicCode);
                if (sicCode == "0023000000")
                {
                    item.FINCAL.Should().Be("RC");
                }
                else
                {
                    item.FINCAL.Should().BeNullOrEmpty();
                }
            }
        }

        [Then(@"the assertions for each SHORT testcase for DEPLO short loan or deposit case is valid")]
        public async Task ThenTheAssertionsForEachShortTestcaseForDEPLOShortLoanOrDepositCaseIsValid()
        {
            var expectedOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var expectedAlternativeData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var filePathIDNL = (string)_scenarioContext["savedFilePathIDNL"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            (var headerRow, var dataRow) = await _loanDataApprovalObject.GetCSVDataIDNL(filePathIDNL);
            await _loanDataApprovalObject.PrintDataRowsForCSVFile(dataRow);
            double doubleCustomerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            string expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlacesShort(DataTransformOptions.OpicsFiles, doubleCustomerRate);
            var notionalWithoutComma = expectedOfferData.Notional.Replace(",", ".");

            foreach (var item in dataRow)
            {
                TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
                var dtMaturityDate = DateTime.ParseExact(expectedAlternativeData.MaturityDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

                var dtStartDate = DateTime.ParseExact(expectedAlternativeData.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");

                var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
                var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
                var sicCode = props[16].GetValue(fieldsObject).ToString();

                var totalDays = (dtMaturityDate - dtStartDate).Days;
                if (loanType == "DEPOSIT")
                {
                    item.PRODTYPE.Should().Be("OG");
                    if (totalDays <= 7)
                    {
                        item.PRODUCT.Should().Be("CALLOG");
                    }
                    else if (totalDays > 7)
                    {
                        item.PRODUCT.Should().Be("KORTLE");
                    }
                }
                else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                {
                    item.PRODTYPE.Should().Be("UG");
                    if (totalDays <= 7)
                    {
                        item.PRODUCT.Should().Be("CALLUG");
                    }
                    else if (totalDays > 7)
                    {
                        item.PRODUCT.Should().Be("KORTLE");
                    }
                }
                else
                {
                    throw new Exception("Unknown loantype for item.PRODTYPE!");
                }

                var chooseTraderAssertion1 = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => item.TRAD.Should().Be("TADA"),
                    TestUser.AccountDesk2 => item.TRAD.Should().Be("TADB"),
                    TestUser.Treasury1 => item.TRAD.Should().Be("TTRA"),
                    TestUser.Treasury2 => item.TRAD.Should().Be("TTRB"),
                    TestUser.TreasuryManager1 => item.TRAD.Should().Be("TTMA"),
                    TestUser.TreasuryManager2 => item.TRAD.Should().Be("TTMB"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT DEPLO csv specflow step"),
                };

                item.VDATE.Should().Be(formattedStartDate);
                item.MDATE.Should().Be(formattedMaturityDate);
                item.CNO.Should().Be(cifNumber);
                item.CCY.Should().Be("EUR");
                item.CCYAMT.Should().Be(notionalWithoutComma);
                item.RATECODE.Should().Be("FIXED");
                item.BASIS.Should().Be("A360");

                item.SPREADRATE_8.Should().BeNullOrEmpty();
                item.BROK.Should().Be("BRZONDER");
                item.COST.Should().Be("300");
                item.ALLOWNEGINT.Should().Be("Y");

                if (expectedOfferData.Guarantor1 != null && expectedOfferData.Guarantor1 != string.Empty)
                {
                    item.GUARANTOR.Should().NotBeNullOrEmpty();
                }
                else
                {
                    item.GUARANTOR.Should().BeNullOrEmpty();
                }

                if (sicCode == "0023000000")
                {
                    item.FINCAL.Should().Be("RC");
                }
                else
                {
                    item.FINCAL.Should().BeNullOrEmpty();
                }
            }
        }
    }
}