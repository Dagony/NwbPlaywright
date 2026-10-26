using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Features;
using Reqnroll;
using System;
using System.Globalization;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.Support;
using static NUnit.Framework.Internal.OSPlatform;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TA.Playwright.Fields.EnumsForCLM;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class SluitbriefDepositStepDefinitions
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
        private readonly string _currentFormattedDate;
        private readonly string _setCDDDate;

        public SluitbriefDepositStepDefinitions(
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IPlaywrightDriver playwrightDriver,
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
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _testSettings = testSettings;
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
            _variableModel = variableModel;
            _sharedDataTransforms = sharedDataTransforms;
            _fixedDataTransforms = fixedDataTransforms;
            _variableDataTransforms = variableDataTransforms;
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            _setCDDDate = _sharedDataTransforms.GetDateYesterday();
        }

        private string formattedStartDate = string.Empty;

        [Then("the displayed sluitbrief for fixed loan should match the expected table")]
        public async Task ThenTheDisplayedSluitbriefForFixedLoanShouldMatchTheExpectedTable()
        {
            var sluitBriefTable = (string)_scenarioContext["savedSluitBriefTable"];
            var fixedCaseOfferData = (FixedLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var fixedAltData = (FixedLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var aLLcaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var counterPartyName = (string)_scenarioContext["savedCounterParty"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            int numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            string expectedCDDreviewDate = (string)_scenarioContext["savedCDDreviewDate"];

            var secID = (string)_scenarioContext["savedSecID"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var eventType = (string)_scenarioContext["savedEventType"];
            var loanType = (string)_scenarioContext["savedLoanType"];

            string expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(EnumsForTRX.DataTransformOptions.Sluitbrief, customerRate);

            if (loanType == "INTERESTSPREADRESET")
            {
                sluitBriefTable.Should().Contain($"Original Security ID\n{fixedCaseOfferData.ExpectedOriginalSecID}");
                sluitBriefTable.Should().Contain($"New Security ID\n{secID}");
            }
            else
            {
                sluitBriefTable.Should().NotContain($"Original Security ID");
                sluitBriefTable.Should().Contain($"Security ID: {secID}");
            }

            var chooseProductOption = productType switch
            {

                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => sluitBriefTable.Should().ContainEquivalentOf($"Traditional fixed rate loan Long - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan Floating notional - {eventType}"),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => sluitBriefTable.Should().ContainEquivalentOf($"Traditional fixed rate loan Short - {eventType}"),
                EnumsForTRX.SelectProduct.BaseRateLoan => sluitBriefTable.Should().ContainEquivalentOf($"Base rate loan - {eventType}"),
                EnumsForTRX.SelectProduct.MTNLocalAuthority => sluitBriefTable.Should().ContainEquivalentOf($"MTN local authority - {eventType}"),
                EnumsForTRX.SelectProduct.SustainableLinkedLoan => sluitBriefTable.Should().ContainEquivalentOf($"Sustainable linked loan - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFixedrateFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Fixed rate Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.FixedRateLoanWithPrefinancingPeriod => sluitBriefTable.Should().ContainEquivalentOf($"Fixed rate loan with pre-financing period - {eventType}"),
                EnumsForTRX.SelectProduct.ShortTermFacility => sluitBriefTable.Should().ContainEquivalentOf($"Short term facility - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanCapFloor => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan cap floor - {eventType}"),
                EnumsForTRX.SelectProduct.WSWObligo => sluitBriefTable.Should().ContainEquivalentOf($"WSW Obligo - {eventType}"),
                EnumsForTRX.SelectProduct.VATFacility => sluitBriefTable.Should().ContainEquivalentOf($"VAT Facility - {eventType}"),
                EnumsForTRX.SelectProduct.RevolvingCreditFacility => sluitBriefTable.Should().ContainEquivalentOf($"Revolving credit facility - {eventType}"),
                EnumsForTRX.SelectProduct.DebtServiceReserveFacility => sluitBriefTable.Should().ContainEquivalentOf($"Debt service reserve facility - {eventType}"),
                EnumsForTRX.SelectProduct.TermLoanFacility => sluitBriefTable.Should().ContainEquivalentOf($"Term loan facility - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Floating rate Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateVariableNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Floating rate Variable notional - {eventType}"),
                EnumsForTRX.SelectProduct.Deposit => sluitBriefTable.Should().ContainEquivalentOf($"Deposit - {eventType}"),
                EnumsForTRX.SelectProduct.Miscellaneous => sluitBriefTable.Should().ContainEquivalentOf($"Miscellaneous - {eventType}"),
                _ => throw new Exception("Unknown option chosen for switch expression chooseProductOption"),
            };

            sluitBriefTable.Should().Contain($"Counterparty\n{counterPartyName} ({cifNumber})");
            sluitBriefTable.Should().Contain($"CDD Review date\n{expectedCDDreviewDate}");
            if (_testSettings.ApplicationUrl.Contains("dt1"))
            {
                var chooseDT1TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.AccountDesk1@testapp een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.AccountDesk2@testapp een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.Treasury1@testapp een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.Treasury2@testapp een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.TreasuryManager1@testapp een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.TreasuryManager2@testapp een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }
            else
            {
                var chooseDT2TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.AccountDesk1@nwbbank.com een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.AccountDesk2@nwbbank.com een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.Treasury1@nwbbank.com een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.Treasury2@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.TreasuryManager1@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lalCaseNumber} heeft trader Test.TreasuryManager2@nwbbank.com een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }

            sluitBriefTable.Should().Contain($"Transaction date\n{_currentFormattedDate}");
            if (fixedCaseOfferData.ExpectedGuarantor1 != null && fixedCaseOfferData.ExpectedGuarantor1 != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Guarantor\n{fixedCaseOfferData.Guarantor1}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Guarantor");
            }

            if (fixedAltData.DealRemark != null && fixedAltData.DealRemark != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n{fixedAltData.DealRemark}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n\n");
            }

            // TODO: INBOUWEN comment for backoffice.
            if (loanType == "INTERESTSPREADRESET")
            {
                sluitBriefTable.Should().Contain($"Notional\n€ {fixedCaseOfferData.ExpectedNotional}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Notional\n€ {fixedCaseOfferData.Notional}");
            }

            sluitBriefTable.Should().Contain("Product: OHLE");
            if (productType == EnumsForTRX.SelectProduct.Deposit)
            {
                sluitBriefTable.Should().Contain("Product type: OG");
            }
            else
            {
                sluitBriefTable.Should().Contain("Product type: UG");
            }

            sluitBriefTable.Should().Contain("Loan type: Normaal");
            sluitBriefTable.Should().Contain("Notional type: Fixed");
            sluitBriefTable.Should().Contain($"Security ID: {secID}");
            sluitBriefTable.Should().Contain($"Start date\n{fixedAltData.StartDate}");
            sluitBriefTable.Should().Contain($"Maturity date\n{fixedAltData.MaturityDate}");
            sluitBriefTable.Should().Contain($"Day count\n{fixedAltData.DayCount}");
            sluitBriefTable.Should().Contain($"Amortisation type\n{fixedAltData.AmortType}");
            if (fixedAltData.FirstCouponDate != null && fixedAltData.FirstCouponDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"First coupon date\n{fixedAltData.FirstCouponDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"First coupon date\n\n");
            }

            if (fixedAltData.LastCouponDate != null && fixedAltData.LastCouponDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Last coupon start date\n{fixedAltData.LastCouponDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Last coupon start date\n\n");
            }

            sluitBriefTable.Should().Contain($"Coupon frequency\n{fixedAltData.CouponFreq}");
            sluitBriefTable.Should().Contain($"Coupon day\n{fixedAltData.CouponDay}");

            if (fixedAltData.Swap == true)
            {
                sluitBriefTable.Should().Contain($"Swap needed\nYes");
            }
            else if (fixedAltData.Swap == false)
            {
                sluitBriefTable.Should().Contain($"Swap needed\nNo");
            }

            if (fixedAltData.InterestRRDate != null && fixedAltData.InterestRRDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Spread rate reset date\n{fixedAltData.InterestRRDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Spread rate reset date\n\n");
            }

            if (fixedAltData.AmortType == "Bullet")
            {
                sluitBriefTable.Should().NotContain($"Amortisation frequency");
                sluitBriefTable.Should().NotContain($"Number of amortisations");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Amortisation frequency\n{fixedAltData.AmortFreq}");
                sluitBriefTable.Should().Contain($"Number of amortisations\n{fixedAltData.NumbOfAmort}");
            }

            sluitBriefTable.Should().Contain($"Customer rate\n{expectedCustomerRate}");
        }

        [Then("the displayed sluitbrief for deposito should match the expected table")]
        public async Task ThenTheDisplayedSluitbriefForDepositoShouldMatchTheExpectedTable()
        {
            var sluitBriefTable = (string)_scenarioContext["savedSluitBriefTable"];
            var depositCaseOfferData = (ShortLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var depositAltData = (ShortLoanAlternativeFields)_scenarioContext["savedAltData"];
            var dCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var dACaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var counterPartyName = (string)_scenarioContext["savedCounterParty"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            int numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];
            string expectedCDDreviewDate = (string)_scenarioContext["savedCDDreviewDate"];

            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            string expectedCustomerRate = _fixedDataTransforms.TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(EnumsForTRX.DataTransformOptions.Offermail, customerRate);

            sluitBriefTable.Should().ContainEquivalentOf($"Deposit - Primary");
            sluitBriefTable.Should().Contain($"Counterparty\n{counterPartyName} ({cifNumber})");

            if (_testSettings.ApplicationUrl.Contains("dt1"))
            {
                var chooseDT1TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.AccountDesk1@testapp een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.AccountDesk2@testapp een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.Treasury1@testapp een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.Treasury2@testapp een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.TreasuryManager1@testapp een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.TreasuryManager2@testapp een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }
            else
            {
                var chooseDT2TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.AccountDesk1@nwbbank.com een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.AccountDesk2@nwbbank.com een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.Treasury1@nwbbank.com een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.Treasury2@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.TreasuryManager1@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {dCaseNumber} heeft trader Test.TreasuryManager2@nwbbank.com een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }

            sluitBriefTable.Should().NotContain($"Guarantor");

            if (depositAltData.DealRemark != null && depositAltData.DealRemark != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n{depositAltData.DealRemark}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n\n");
            }

            // todo: INBOUWEN comment for backoffice.
            sluitBriefTable.Should().Contain($"Notional\n€ {depositCaseOfferData.Notional}");

            var dtMaturityDate = DateTime.ParseExact(depositAltData.MaturityDate, "d-M-yyyy", CultureInfo.InvariantCulture);
            var formattedMaturityDate = dtMaturityDate.ToString("yyyy-MM-dd");

            var dtStartDate = DateTime.ParseExact(depositAltData.StartDate, "d-M-yyyy", CultureInfo.InvariantCulture);

            var formattedStartDate = dtStartDate.ToString("yyyy-MM-dd");
            var totalDays = (dtMaturityDate - dtStartDate).Days;

            if (totalDays <= 7)
            {
                // Is it correct that I changed these values to this?
                // SluitBriefTable.Should().Contain($"Product: CALLOG");
                sluitBriefTable.Should().Contain($"Product: CALLOG");
            }
            else if (totalDays > 7)
            {
                sluitBriefTable.Should().Contain($"Product: KORTLE");

                // Is it correct that I changed these values to this?
                sluitBriefTable.Should().Contain("Product type: OG"); // I moved this from outside of the if/elseif
            }

            sluitBriefTable.Should().NotContain("Loan type: Normaal");
            sluitBriefTable.Should().NotContain("Notional type: Fixed");
            sluitBriefTable.Should().NotContain($"Security ID");
            sluitBriefTable.Should().NotContain($"Amortisation type");

            sluitBriefTable.Should().Contain($"Start date\n{depositAltData.StartDate}");
            sluitBriefTable.Should().Contain($"Maturity date\n{depositAltData.MaturityDate}");
            sluitBriefTable.Should().Contain($"Day count\nAct/360");

            sluitBriefTable.Should().Contain($"Customer rate\n{expectedCustomerRate}");
        }

        [Then("the displayed sluitbrief for variable loan update should match the expected table")]
        public async Task ThenTheDisplayedSluitbriefForVariableLoanUpdateShouldMatchTheExpectedTable()
        {
            var sluitBriefTable = (string)_scenarioContext["savedSluitBriefTable"];
            var vARLoanUpdateCaseOfferData = (VariableLoanCaseOfferFields)_scenarioContext["savedCaseOfferData"];
            var vARLoanUpdateAltData = (VariableLoanAlternativeFields)_scenarioContext["savedAltData"];
            var lUCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            double customerRate = (double)_scenarioContext["savedExpectedCustomerRate"];
            var cifNumber = (string)_scenarioContext["savedCIFNumber"];
            var lUACaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var counterPartyName = (string)_scenarioContext["savedCounterParty"];
            int numberOfAdvisorIndividuals = (int)_scenarioContext["savedSelectedAdvisorIndividuals"];

            string expectedCustomerRate = await _variableDataTransforms.TransformCustomerRateToStringWithRequiredDecimalPlaces(EnumsForTRX.DataTransformOptions.Sluitbrief, customerRate);
            var secID = (string)_scenarioContext["savedSecID"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var eventType = (string)_scenarioContext["savedEventType"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            TestUser chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            string expectedCDDreviewDate = (string)_scenarioContext["savedCDDreviewDate"];

            if (loanType == "INTERESTSPREADRESET")
            {
                sluitBriefTable.Should().Contain($"Original Security ID\n{vARLoanUpdateCaseOfferData.ExpectedOriginalSecID}");
                sluitBriefTable.Should().Contain($"New Security ID\n{secID}");
            }
            else
            {
                sluitBriefTable.Should().NotContain($"Original Security ID");
                sluitBriefTable.Should().Contain($"Security ID: {secID}");
            }

            if (productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional)
            {
                if (vARLoanUpdateCaseOfferData.CommitmentFee != null && vARLoanUpdateCaseOfferData.CommitmentFee != string.Empty)
                {
                    sluitBriefTable.Should().Contain($"Commitment fee\n€ {vARLoanUpdateCaseOfferData.CommitmentFee}");
                }
                else
                {
                    sluitBriefTable.Should().NotContain($"Commitment fee");
                }

                sluitBriefTable.Should().Contain("Minimum withdrawal amount");
                sluitBriefTable.Should().Contain("Maximum withdrawal amount");
                sluitBriefTable.Should().Contain("Loan type: Variabele hoofdsom");
                sluitBriefTable.Should().Contain("Notional type: Variable");
            }
            else
            {
                sluitBriefTable.Should().Contain("Loan type: Normaal");
                sluitBriefTable.Should().Contain("Notional type: Fixed");

                sluitBriefTable.Should().NotContain("Minimum withdrawal amount");
                sluitBriefTable.Should().NotContain("Maximum withdrawal amount");
            }

            var chooseProductOption = productType switch
            {

                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => sluitBriefTable.Should().ContainEquivalentOf($"Traditional fixed rate loan Long - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan Floating notional - {eventType}"),
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => sluitBriefTable.Should().ContainEquivalentOf($"Traditional fixed rate loan Short - {eventType}"),
                EnumsForTRX.SelectProduct.BaseRateLoan => sluitBriefTable.Should().ContainEquivalentOf($"Base rate loan - {eventType}"),
                EnumsForTRX.SelectProduct.MTNLocalAuthority => sluitBriefTable.Should().ContainEquivalentOf($"MTN local authority - {eventType}"),
                EnumsForTRX.SelectProduct.SustainableLinkedLoan => sluitBriefTable.Should().ContainEquivalentOf($"Sustainable linked loan - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFixedrateFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Fixed rate Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.FixedRateLoanWithPrefinancingPeriod => sluitBriefTable.Should().ContainEquivalentOf($"Fixed rate loan with pre-financing period - {eventType}"),
                EnumsForTRX.SelectProduct.ShortTermFacility => sluitBriefTable.Should().ContainEquivalentOf($"Short term facility - {eventType}"),
                EnumsForTRX.SelectProduct.FloatingRateLoanCapFloor => sluitBriefTable.Should().ContainEquivalentOf($"Floating rate loan cap floor - {eventType}"),
                EnumsForTRX.SelectProduct.WSWObligo => sluitBriefTable.Should().ContainEquivalentOf($"WSW Obligo - {eventType}"),
                EnumsForTRX.SelectProduct.VATFacility => sluitBriefTable.Should().ContainEquivalentOf($"VAT Facility - {eventType}"),
                EnumsForTRX.SelectProduct.RevolvingCreditFacility => sluitBriefTable.Should().ContainEquivalentOf($"Revolving credit facility - {eventType}"),
                EnumsForTRX.SelectProduct.DebtServiceReserveFacility => sluitBriefTable.Should().ContainEquivalentOf($"Debt service reserve facility - {eventType}"),
                EnumsForTRX.SelectProduct.TermLoanFacility => sluitBriefTable.Should().ContainEquivalentOf($"Term loan facility - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateFixedNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Floating rate Fixed notional - {eventType}"),
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateVariableNotional => sluitBriefTable.Should().ContainEquivalentOf($"Export Credit Guarantee Floating rate Variable notional - {eventType}"),
                EnumsForTRX.SelectProduct.Deposit => sluitBriefTable.Should().ContainEquivalentOf($"Deposit - {eventType}"),
                EnumsForTRX.SelectProduct.Miscellaneous => sluitBriefTable.Should().ContainEquivalentOf($"Miscellaneous - {eventType}"),
                _ => throw new Exception("Unknown option chosen for switch expression chooseProductOption"),
            };

            sluitBriefTable.Should().Contain($"Counterparty\n{counterPartyName} ({cifNumber})");
            sluitBriefTable.Should().Contain($"CDD Review date\n{expectedCDDreviewDate}");
            if (_testSettings.ApplicationUrl.Contains("dt1"))
            {
                var chooseDT1TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.AccountDesk1@testapp een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.AccountDesk2@testapp een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.Treasury1@testapp een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.Treasury2@testapp een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.TreasuryManager1@testapp een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.TreasuryManager2@testapp een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }
            else
            {
                var chooseDT2TraderAssertion = chosenTestUser switch
                {
                    TestUser.AccountDesk1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.AccountDesk1@nwbbank.com een nieuwe"),
                    TestUser.AccountDesk2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.AccountDesk2@nwbbank.com een nieuwe"),
                    TestUser.Treasury1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.Treasury1@nwbbank.com een nieuwe"),
                    TestUser.Treasury2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.Treasury2@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager1 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.TreasuryManager1@nwbbank.com een nieuwe"),
                    TestUser.TreasuryManager2 => sluitBriefTable.Should().ContainEquivalentOf($"In offer case {lUCaseNumber} heeft trader Test.TreasuryManager2@nwbbank.com een nieuwe"),
                    _ => throw new Exception("Unknown Assertion for chooseTraderAssertion1, DEPOSIT Sluitbrief specflow step"),
                };
            }

            sluitBriefTable.Should().Contain($"Transaction date\n{_currentFormattedDate}");
            if (vARLoanUpdateCaseOfferData.ExpectedGuarantor1 != null && vARLoanUpdateCaseOfferData.ExpectedGuarantor1 != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Guarantor\n{vARLoanUpdateCaseOfferData.ExpectedGuarantor1}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Guarantor\n\n");
            }

            if (vARLoanUpdateAltData.DealRemark != null && vARLoanUpdateAltData.DealRemark != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n{vARLoanUpdateAltData.DealRemark}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Comment to customer\n\n");
            }

            // TODO: INBOUWEN comment for backoffice.
            if (loanType == "INTERESTSPREADRESET")
            {
                sluitBriefTable.Should().Contain($"Notional\n€ {vARLoanUpdateCaseOfferData.ExpectedNotional}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Notional\n€ {vARLoanUpdateCaseOfferData.Notional}");
            }

            sluitBriefTable.Should().Contain("Product: OHLE");
            if (productType == EnumsForTRX.SelectProduct.Deposit)
            {
                sluitBriefTable.Should().Contain("Product type: OG");
            }
            else
            {
                sluitBriefTable.Should().Contain("Product type: UG");
            }

            sluitBriefTable.Should().Contain($"Security ID: {secID}");
            sluitBriefTable.Should().Contain($"Start date\n{vARLoanUpdateAltData.StartDate}");
            sluitBriefTable.Should().Contain($"Maturity date\n{vARLoanUpdateAltData.MaturityDate}");

            if (vARLoanUpdateAltData.ExpectedDayCount != null && vARLoanUpdateAltData.ExpectedDayCount != string.Empty)
            {
                var assertDayCountLoanUpdate = vARLoanUpdateAltData.ExpectedDayCount switch
                {
                    "A360" => sluitBriefTable.Should().Contain($"Day count\nAct/360"),
                    "A365" => sluitBriefTable.Should().Contain($"Day count\nAct/365"),
                    "ACT365" => sluitBriefTable.Should().Contain($"Day count\nAct/Act ISDA"),
                    "ACTUAL" => sluitBriefTable.Should().Contain($"Day count\nAct/Act"),
                    "EBOND" => sluitBriefTable.Should().Contain($"Day count\n30/360"),
                    _ => throw new Exception("Unknown option chosen for switch expression AssertDayCountLoanUpdate"),
                };
            }
            else
            {
                sluitBriefTable.Should().Contain($"Day count\tAct/360");
            }

            if (vARLoanUpdateAltData.FirstCouponDate != null && vARLoanUpdateAltData.FirstCouponDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"First coupon date\n{vARLoanUpdateAltData.FirstCouponDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"First coupon date\n\n");
            }

            if (vARLoanUpdateAltData.LastCouponDate != null && vARLoanUpdateAltData.LastCouponDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Last coupon start date\n{vARLoanUpdateAltData.LastCouponDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Last coupon start date\n\n");
            }

            sluitBriefTable.Should().NotContain($"Coupon frequency");
            sluitBriefTable.Should().Contain($"Coupon day\n{vARLoanUpdateAltData.CouponDay}");
            sluitBriefTable.Should().NotContain($"Swap needed");

            if (vARLoanUpdateAltData.InterestRRDate != null && vARLoanUpdateAltData.InterestRRDate != string.Empty)
            {
                sluitBriefTable.Should().Contain($"Spread rate reset date\n{vARLoanUpdateAltData.InterestRRDate}");
            }
            else
            {
                sluitBriefTable.Should().Contain($"Spread rate reset date\n\n");
            }

            sluitBriefTable.Should().NotContain($"Amortisation frequency");
            sluitBriefTable.Should().NotContain($"Number of amortisations");
            sluitBriefTable.Should().NotContain($"Amortisation type");

            sluitBriefTable.Should().ContainAny($"Customer spread\n{vARLoanUpdateAltData.ReferenceRate} +{expectedCustomerRate}", $"Customer spread\n{vARLoanUpdateAltData.ReferenceRate} {expectedCustomerRate}");
        }
    }
}