using Aspose.Words.Fields;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.TRX;
using static NUnit.Framework.Internal.OSPlatform;
using static TA.Playwright.Fields.EnumsForCLM;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.StepDefinitions
{
    [Binding]
    public class GeneralStepDefinitions
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
        private readonly ISendConfirmationObject _sendConfirmationObject;
        private readonly IFixedModel _fixedModel;
        private readonly IVariableModel _variableModel;
        private readonly IShortModel _shortModel;
        private readonly ISharedModel _sharedModel;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly ILoanUpdateWinLostCorrectObject _loanUpdateWinLostCorrectObject;
        private readonly ILoanUpdateBillOfClosureObject _loanUpdateBillOfClosureObject;
        private readonly ILoanUpdateClientApprovalObject _loanUpdateClientApprovalObject;
        private readonly ILoanUpdateAgreementReviewObject _loanUpdateAgreementReviewObject;
        private readonly ILoanUpdateCreateContractObject _loanUpdateCreateContractObject;
        private readonly ILoanUpdateLoanDataApprovalObject _loanUpdateLoanDataApprovalObject;
        private readonly ILoanUpdateValidateRequestObject _loanUpdateValidateRequestObject;
        private readonly ILoanUpdateCollectDocumentInformationObject _loanUpdateCollectDocumentInformationObject;
        private readonly IRejectionObject _rejectionObject;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividual;

        public GeneralStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ISharedDataIndividuals sharedDataIndividual,
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
            ISendConfirmationObject sendConfirmationObject,
            IFixedModel fixedModel,
            IVariableModel variableModel,
            IShortModel shortModel,
            ISharedModel sharedModel,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject,
            ILoanUpdateWinLostCorrectObject loanUpdateWinLostCorrectObject,
            ILoanUpdateBillOfClosureObject loanUpdateBillOfClosureObject,
            ILoanUpdateLoanDataApprovalObject loanUpdateLoanDataApprovalObject,
            ILoanUpdateClientApprovalObject loanUpdateClientApprovalObject,
            ILoanUpdateAgreementReviewObject loanUpdateAgreementReviewObject,
            ILoanUpdateCreateContractObject loanUpdateCreateContractObject,
            ILoanUpdateValidateRequestObject loanUpdateValidateRequestObject,
            ILoanUpdateCollectDocumentInformationObject loanUpdateCollectDocumentInformationObject,
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
            _sharedDataIndividual = sharedDataIndividual;
            _checkDossierCompleteObject = checkDossierCompleteObject;
            _sendConfirmationObject = sendConfirmationObject;
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _shortModel = shortModel;
            _sharedModel = sharedModel;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataTransforms = sharedDataTransforms;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _loanUpdateWinLostCorrectObject = loanUpdateWinLostCorrectObject;
            _loanUpdateBillOfClosureObject = loanUpdateBillOfClosureObject;
            _loanUpdateClientApprovalObject = loanUpdateClientApprovalObject;
            _loanUpdateAgreementReviewObject = loanUpdateAgreementReviewObject;
            _loanUpdateCreateContractObject = loanUpdateCreateContractObject;
            _loanUpdateLoanDataApprovalObject = loanUpdateLoanDataApprovalObject;
            _loanUpdateValidateRequestObject = loanUpdateValidateRequestObject;
            _loanUpdateCollectDocumentInformationObject = loanUpdateCollectDocumentInformationObject;
            _rejectionObject = rejectionObject;
        }

        [Given("I select PEGA task {string}")]
        public async Task GivenISelectPEGAProduct(string chosenProductType)
        {
            Task chooseLoanType = chosenProductType switch
            {
                "Loan" => _basePageObject.SelectLoanRequest(LoanRequest.Loan),
                "Deposit" => _basePageObject.SelectLoanRequest(LoanRequest.Deposit),
                "Other" => _basePageObject.SelectLoanRequest(LoanRequest.Other),
                "Interest spread reset" => _basePageObject.SelectLoanRequest(LoanRequest.InterestSpreadReset),
                "Other update" => _basePageObject.SelectLoanRequest(LoanRequest.OtherUpdate),
                _ => throw new Exception("Unknown option chosen for switch expression chooseLoanType"),
            };
            await chooseLoanType;
        }

        [Given("I select PEGA product {string} and event {string}")]
        public async Task GivenISelectPEGAProductAndEvent(string loanProduct, string loanEvent)
        {
            Task chosenProduct = loanProduct switch
            {
                "Traditional fixed rate loan long" => _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanLong),
                "Floating rate loan fixed notional" => _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFixedNotional),
                "Floating rate loan floating notional" => _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanFloatingNotional),
                "Traditional fixed rate loan short" => _basePageObject.SelectLoanProduct(SelectProduct.TraditionalFixedRateLoanShort),
                "Base rate loan" => _basePageObject.SelectLoanProduct(SelectProduct.BaseRateLoan),
                "MTN local authority" => _basePageObject.SelectLoanProduct(SelectProduct.MTNLocalAuthority),
                "Sustainable linked loan" => _basePageObject.SelectLoanProduct(SelectProduct.SustainableLinkedLoan),
                "Export Credit Guarantee Fixed rate Fixed notional" => _basePageObject.SelectLoanProduct(SelectProduct.ExportCreditGuaranteeFixedrateFixedNotional),
                "Fixed rate loan with pre-financing period" => _basePageObject.SelectLoanProduct(SelectProduct.FixedRateLoanWithPrefinancingPeriod),
                "Short term facility" => _basePageObject.SelectLoanProduct(SelectProduct.ShortTermFacility),
                "Floating rate loan cap floor" => _basePageObject.SelectLoanProduct(SelectProduct.FloatingRateLoanCapFloor),
                "WSW Obligo" => _basePageObject.SelectLoanProduct(SelectProduct.WSWObligo),
                "VAT Facility" => _basePageObject.SelectLoanProduct(SelectProduct.VATFacility),
                "Revolving credit facility" => _basePageObject.SelectLoanProduct(SelectProduct.RevolvingCreditFacility),
                "Debt service reserve facility" => _basePageObject.SelectLoanProduct(SelectProduct.DebtServiceReserveFacility),
                "Term loan facility" => _basePageObject.SelectLoanProduct(SelectProduct.TermLoanFacility),
                "Export Credit Guarantee Floating rate Fixed notional" => _basePageObject.SelectLoanProduct(SelectProduct.ExportCreditGuaranteeFloatingRateFixedNotional),
                "Export Credit Guarantee Floating rate Variable notional" => _basePageObject.SelectLoanProduct(SelectProduct.ExportCreditGuaranteeFloatingRateVariableNotional),
                "Miscellaneous" => _basePageObject.SelectLoanProduct(SelectProduct.Miscellaneous),
                _ => throw new Exception("Unknown option chosen for switch expression chosenProduct"),
            };
            await chosenProduct;

            Task chosenEvent = loanEvent switch
            {
                "Primary" => _basePageObject.SelectLoanEvent(SelectEvent.Primary),
                "Secondary" => _basePageObject.SelectLoanEvent(SelectEvent.Secondary),
                "Doorzak" => _basePageObject.SelectLoanEvent(SelectEvent.Doorzak),
                "Interest reset" => _basePageObject.SelectLoanEvent(SelectEvent.InterestReset),
                "Spread reset" => _basePageObject.SelectLoanEvent(SelectEvent.SpreadReset),
                "Restructuring" => _basePageObject.SelectLoanEvent(SelectEvent.Restructuring),
                "Extendable option" => _basePageObject.SelectLoanEvent(SelectEvent.ExtendableOption),
                "Drawdown facility" => _basePageObject.SelectLoanEvent(SelectEvent.DrawdownFacility),
                "Early repayment restructuring" => _basePageObject.SelectLoanEvent(SelectEvent.EarlyRepaymentRestructuring),
                "Early repayment market value" => _basePageObject.SelectLoanEvent(SelectEvent.EarlyRepaymentMarketValue),
                "Early repayment reset date" => _basePageObject.SelectLoanEvent(SelectEvent.EarlyRepaymentResetDate),
                "Early repayment doorzak" => _basePageObject.SelectLoanEvent(SelectEvent.EarlyRepaymentDoorzak),
                "Miscellaneous" => _basePageObject.SelectLoanEvent(SelectEvent.Miscellaneous),
                _ => throw new Exception("Unknown option chosen for switch expression chosenEvent"),
            };

            await chosenEvent;
            await _basePageObject.ClickSubmitButton();
        }

        [Given("I select a loanupdate case with prefix SECID {string}")]
        public async Task GivenISelectALoanupdateCaseWithPrefixSECID(string secID)
        {
            _scenarioContext["savedLoanUpdateSecID"] = secID;
            await Task.Delay(1000);
            var loanUpdateID = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByText(secID, new() { Exact = true }).First;
            var nextButtonTable = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByRole(AriaRole.Button, new() { Name = ">", Exact = true });
            await Task.Delay(1000);
            while (true)
            {
                // Check if row exists on the current page
                if (await loanUpdateID.CountAsync() > 0)
                {
                    break;
                }

                // Go to next page
                await nextButtonTable.ClickAsync();
                await Task.Delay(500);
            }

            await loanUpdateID.DblClickAsync();
        }

        [When(@"I press submit an email is generated")]
        public async Task WhenIPressSubmitAEmailIsGenerated()
        {
            var loanType = (string)_scenarioContext["savedLoanType"];
            EnumsForTRX.SelectProduct productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            string productTypeString = productType.ToString();

            // var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            var environment = _testSettings.ApplicationEnvironment;

            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filePath = _testSettings.DownloadFilePath;

            var popupWindowOfferEmail = await _playwrightDriver.Page.Result.RunAndWaitForPopupAsync(async () =>
            {
                await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("xpath=(//a[contains(text(),'Offerte')])[1]").First.ClickAsync();
            });
            var (actualAddresseesEmailText, actualAanhef, actualTable, actualFootTextEmailBasic, actualTableENG, actualAanhefENG) = await _winLostCorrectObject.GetEmailBodyTextBasic(popupWindowOfferEmail);
            _scenarioContext["savedActualTable"] = actualTable;
            _scenarioContext["savedActualAanhef"] = actualAanhef;
            _scenarioContext["savedActualAddressees"] = actualAddresseesEmailText;
            _scenarioContext["savedActualEmailText"] = actualFootTextEmailBasic;
            _scenarioContext["savedActualTableENG"] = actualTableENG;
            _scenarioContext["savedActualAanhefENG"] = actualAanhefENG;

            Console.WriteLine($"Email successfully generated for: {prefixName} on {currentDateTime}");
            Console.WriteLine($">>PRINTING ACTUAL ADDRESSEES BELOW FOR CASE: {lalCaseNumber}<<");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualAddresseesEmailText);
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine($">>PRINTING ACTUAL EMAIL BELOW FOR {loanType} {productTypeString} CASE: {lalCaseNumber}<<");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(actualFootTextEmailBasic);
            Console.WriteLine("----------------------------------------------------------------------------------");

            var filepathScreenshot = @$"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{lalCaseNumber}_EmailContent.png";
            string downloadPath = System.IO.Path.Combine(_testSettings.DownloadFilePath, filepathScreenshot);

            await popupWindowOfferEmail.SetViewportSizeAsync(2400, 2200);
            await popupWindowOfferEmail.Locator("(//body)[1]").ScrollIntoViewIfNeededAsync();
            await popupWindowOfferEmail.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.NetworkIdle);
            await Task.Delay(3000);
            await popupWindowOfferEmail.ScreenshotAsync(new()
            {
                Path = downloadPath,
                FullPage = true,
                Type = Microsoft.Playwright.ScreenshotType.Png,

                // Caret = Microsoft.Playwright.ScreenshotCaret.Initial
            });
            await popupWindowOfferEmail.CloseAsync();
        }

        [Given(@"I create a VARIABLE offer case with guarantor """"([^""""]*)"""""" and the following fields")]
        public async Task GivenICreateAVARIABLEOfferCaseWithGuarantorAndTheFollowingFields(string guarantor, Reqnroll.Table table)
        {
            await _basePageObject.SelectLoanRequest(EnumsForTRX.LoanRequest.Variable);
            VariableFinalSpreadFields data = table.CreateInstance<VariableFinalSpreadFields>();
            var formattedTransactionDate = DateTime.Now.ToString("d MMMMM yyyy", _testSettings.CultureInfo);
            var dateYesterday = _sharedDataTransforms.GetDateYesterday();
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();

            _scenarioContext["savedTC"] = data.TC;
            _scenarioContext["savedTableData"] = data;
            _scenarioContext["savedTransactionDate"] = formattedTransactionDate;

            if (data.CDDReviewDate != null && data.CDDReviewDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(data.InterestSpreadRRDate);
            }
            else if (data.CDDReviewDate == null && data.CDDReviewDate == string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(dateYesterday);
            }

            if (guarantor != null && guarantor != string.Empty)
            {
                await _finalSpreadObject.CheckGuarantorBox();
                await _finalSpreadObject.ClickAddGuarantorButton();
                await _finalSpreadObject.SetGuarantorField(guarantor);
                await _finalSpreadObject.SetGuarantPercentage(" 55");
            }

            if (data.SpreadCheckBy == false)
            {
                await _finalSpreadObject.SpreadCheckBy(data.SpreadCheckBy);
                await _finalSpreadObject.SpreadCheckSetRemarks(data.SpreadCheckRemarks);
            }

            if (data.DealMoment != null && data.DealMoment != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(data.InterestSpreadRRDate);
            }
            else if (data.DealMoment == null && data.DealMoment == string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(dateYesterday);
            }

            await _finalSpreadObject.SetNotional(data.Notional);
            if (data.SetNotionalTypeToFixed == "Variable")
            {
                await _finalSpreadObject.SetMinimalWithdrawalPercent(data.MinimalWithdrawal);
                await _finalSpreadObject.SetCommitmentFee(data.CommitmentFee);
            }

            if (data.FirstCouponDate != null && data.FirstCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetFirstCouponDate(data.FirstCouponDate);
            }

            if (data.LastCouponDate != null && data.LastCouponDate != string.Empty)
            {
                await _finalSpreadObject.SetLastCouponDate(data.LastCouponDate);
            }

            if (data.InterestSpreadRRDate != null && data.InterestSpreadRRDate != string.Empty)
            {
                await _finalSpreadObject.SetInterestSpreadRRDate(data.InterestSpreadRRDate);
            }

            if (data.CouponDay != null && data.CouponDay != string.Empty)
            {
                await _finalSpreadObject.SetCouponDay(data.CouponDay);
            }
            else if (data.CouponDay == null && data.CouponDay == string.Empty)
            {
                await _finalSpreadObject.SetCouponDay("99");
            }
        }

        [When(@"I open the bill of closure phase")]
        public async Task WhenIOpenTheBillOfClosurePhase()
        {
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var guarantor1 = (string)_scenarioContext["savedGuarantor1"];
            var chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();

            if (loanType == "LOANUPDATEFIXED" || loanType == "LOANUPDATEVARIABLE" || loanType == "LOANUPDATEBASERATE" || loanType == "DEPOSIT")
            {
                await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();
                await _winLostCorrectObject.ClickWinLostCorrect("Won");

                await _winLostCorrectObject.SelectWonAlternative();
                await _winLostCorrectObject.UploadDocuments();

                await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();

                await _basePageObject.LogOutSystem();
                bool spreadcheck = (bool)_scenarioContext["savedSpreadCheck"];

                if (spreadcheck == true)
                {
                    if (loanType == "DEPOSIT")
                    {
                        var chooseLogin = chosenTestUser switch
                        {
                            TestUser.AccountDesk1 => _loginPageObject.LoginAccountDeskUser1(),
                            TestUser.Treasury1 => _loginPageObject.LoginTreasury1(),
                            TestUser.TreasuryManager1 => _loginPageObject.LoginTreasuryManager1(),
                            _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
                        };
                        await chooseLogin;
                    }
                    else
                    {
                        await _loginPageObject.LoginAccountDeskUser1();
                    }
                }
                else
                {
                    if (loanType == "DEPOSIT")
                    {
                        var chooseLogin = chosenTestUser switch
                        {
                            TestUser.AccountDesk1 => _loginPageObject.LoginAccountDeskUser2(),
                            TestUser.Treasury1 => _loginPageObject.LoginTreasury2(),
                            TestUser.TreasuryManager1 => _loginPageObject.LoginTreasuryManager2(),
                            _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
                        };
                        await chooseLogin;
                    }
                    else
                    {
                        await _loginPageObject.LoginAccountDeskUser2();
                    }
                }

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseID);

                await _billOfClosureObject.ApprovedRejectOfferBoC(true);
                await _loanUpdateBillOfClosureObject.ClickSubmitBillOfClosureCases();
                await Task.Delay(1000);
                var aLUCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
                _scenarioContext["savedALLCaseNumber"] = aLUCaseNumber;
                await Task.Delay(1000);
            }
            else
            {
                await _finalSpreadObject.ClickSubmitFinalSpreadPage();
                await _winLostCorrectObject.ClickWinLostCorrect("Won");
                await _winLostCorrectObject.SelectWonAlternative();

                // Indien WFZ Flow worden er extra velden gevuld op het WinLostCorrect scherm i.v.m. het WFZ Contract
                if (sICCode == "0031000000" || guarantor1 == "Waarborgfonds voor de Zorgsector")
                {
                    if (productType != EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                    {
                        var suretyDate = _sharedDataTransforms.GetDateYesterday();
                        await _winLostCorrectObject.SetSuretyStatementNumberAndDate("99999999999999999911", suretyDate);
                    }
                    else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
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

                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, lalCaseID);
            }
        }

        [When("I set the case to lost and continue to rejection resolved stage with rejection comment {string}")]
        public async Task WhenISetTheCaseToLostAndContinueToRejectionResolvedStageWithRejectionComment(string rejectionComment)
        {
            var lalCaseID = (string)_scenarioContext["savedlalCaseID"];

            await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect("Lost");

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();
            await _basePageObject.PageWaitForUrlLoaded();

            await _rejectionObject.OpenConfirmOfferRejection();
            await _rejectionObject.ConfirmOrRejectOfferRejection(true);

            if (rejectionComment != null && rejectionComment != string.Empty)
            {
                await _rejectionObject.SetRejectionReason(rejectionComment);
                _scenarioContext["savedRejectionComment"] = rejectionComment;
            }
            else
            {
                _scenarioContext["savedRejectionComment"] = string.Empty;
            }

            await _rejectionObject.SubmitRejectionPage();

            await _rejectionObject.OpenAttachRejectOfferConfirmation();
            await _rejectionObject.SelectAttachedDocument();
            await _rejectionObject.SubmitRejectionPage();
        }

        [When(@"I choose won and complete the bill of closure phase")]
        public async Task WhenIChooseWonAndCompleteTheBillOfClosurePhase()
        {
            var caseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();
            var guarantor1 = (string)_scenarioContext["savedGuarantor1"];
            TestUser loggedInUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();

            // Als het een WFZ Flow betreft zijn er extra velden op het WinLostCorrect scherm i.v.m. het WFZ Contract
            if (sICCode == "0031000000" || guarantor1 == "Stichting Waarborgfonds voor de Zorgsector")
            {
                if (productType != EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                {
                    var suretyDate = _sharedDataTransforms.GetDateYesterday();
                    await Task.Delay(150);
                    await _winLostCorrectObject.SetSuretyStatementNumberAndDate("99999999999999999911", suretyDate);
                    await Task.Delay(150);
                }
                else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
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

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.Loan, caseNumber);
            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();

            var aLLCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALLCaseNumber"] = aLLCaseNumber;

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully finished front office case: " + caseNumber);
            Console.WriteLine("Succesfully created a BackOffice case: " + aLLCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }

        [When(@"I continue the case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheCaseToBOFirstFlowLoanDataApproval()
        {
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var guarantor1 = (string)_scenarioContext["savedGuarantor1"];
            var sicCode = props[16].GetValue(fieldsObject).ToString();
            bool setswap;

            if (_scenarioContext.ContainsKey("savedBoolSwap"))
            {
                setswap = (bool)_scenarioContext["savedBoolSwap"];
            }
            else
            {
                setswap = false;
            }

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully offer mail sent for case: " + lalCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect("Won");

            // Indien WFZ Flow worden er extra velden gevuld op het WinLostCorrect scherm i.v.m. het WFZ Contract
            if (sicCode == "0031000000" || guarantor1 == "Waarborgfonds voor de Zorgsector")
            {
                if (productType != EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                {
                    var suretyDate = _sharedDataTransforms.GetDateYesterday();
                    await _winLostCorrectObject.SetSuretyStatementNumberAndDate("99999999999999999911", suretyDate);
                }
                else if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                {
                    await _winLostCorrectObject.SetSuretyStatementNumber("99999999999999999933");
                }
            }

            // new in new class structure you have to select won offer.
            await _winLostCorrectObject.SelectWonAlternative();
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
            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();

            string aLLCaseNumber;
            string swapNumber;

            if (setswap == true)
            {
                (aLLCaseNumber, swapNumber) = await _billOfClosureObject.GetSWAPandALLCaseNumber();
            }
            else
            {
                aLLCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            }

            _scenarioContext["savedALLCaseNumber"] = aLLCaseNumber;
            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                await _billOfClosureObject.ClickALLCaseNumber(aLLCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(1000);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, aLLCaseNumber);

            string secID;
            if (loanType == "INTERESTSPREADRESET")
            {
                secID = await _loanDataApprovalObject.GetSECIDLoanUpdate();
            }
            else
            {
                secID = await _loanDataApprovalObject.GetSECIDNormalLoan();
            }

            _scenarioContext["savedSecID"] = secID;

            // nieuw toegevoegd in new class structure: stap is extra toegevoegd tov oude implemnt:
            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _sendConfirmationObject.ClickSubmitSendConfirmation();
            if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
            {
                await _sendConfirmationObject.SetConfirmationSentToCustomer("Yes");
                if (sicCode == "0031000000" || sicCode == "0030000000")
                {
                    await _sendConfirmationObject.SetConfirmationSentToGuarantor("Yes");
                }

                await _sendConfirmationObject.ClickSubmitSendConfirmation();
            }
            else
            {
                await _validateRequestObject.SetCounterPartyConfirmReceived(true);
                await _validateRequestObject.SetIsTheLoanGuaranteed(true);
                await _validateRequestObject.SetWhatIsTheLoanRequest("Split");
                await _validateRequestObject.SetBillOfClosureConfirmedByPF(true);
                await _validateRequestObject.ClickSubmitValidationPage();
                await _createContractObject.ClickSubmitButtonCreateAgreement();
                await Task.Delay(1000);

                try
                {
                    await Task.Delay(1000);
                    var recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                    while (!recentContentFrameBOtext.Contains("No more data to load"))
                    {
                        await Task.Delay(1000);
                        await _basePageObject.ClickLoadMore();
                        await Task.Delay(1000);
                        recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                        await Task.Delay(1000);
                    }
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully created a BackOffice case for ALL-Case: " + aLLCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }

        [When("I continue the case to BO first flow LoanDataApproval for the new PEGA class structure")]
        public async Task WhenIContinueTheCaseToBOFirstFlowLoanDataApprovalForTheNewPEGAClassStructure()
        {
            var frontOfficeCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var guarantor1 = (string)_scenarioContext["savedGuarantor1"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();
            var chosenTestUser = (TestUser)_scenarioContext["savedLoggedInTestUser"];

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully offer mail sent for case: " + frontOfficeCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");

            await _loanUpdateFinalSpreadObject.ClickSubmitFinalSpreadPage();

            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();

            await _basePageObject.LogOutSystem();
            bool spreadcheck = (bool)_scenarioContext["savedSpreadCheck"];
            bool setswap;

            if (_scenarioContext.ContainsKey("savedBoolSwap"))
            {
                setswap = (bool)_scenarioContext["savedBoolSwap"];
            }
            else
            {
                setswap = false;
            }

            if (spreadcheck == true)
            {
                if (loanType == "DEPOSIT")
                {
                    var chooseLogin = chosenTestUser switch
                    {
                        TestUser.AccountDesk1 => _loginPageObject.LoginAccountDeskUser1(),
                        TestUser.Treasury1 => _loginPageObject.LoginTreasury1(),
                        TestUser.TreasuryManager1 => _loginPageObject.LoginTreasuryManager1(),
                        _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
                    };
                    await chooseLogin;
                }
                else
                {
                    await _loginPageObject.LoginAccountDeskUser1();
                }
            }
            else
            {
                if (loanType == "DEPOSIT")
                {
                    var chooseLogin = chosenTestUser switch
                    {
                        TestUser.AccountDesk1 => _loginPageObject.LoginAccountDeskUser2(),
                        TestUser.Treasury1 => _loginPageObject.LoginTreasury2(),
                        TestUser.TreasuryManager1 => _loginPageObject.LoginTreasuryManager2(),
                        _ => throw new Exception("Unknown caseType was passed into specflow step: I login to the environment with test user {string} and start a {string} case"),
                    };
                    await chooseLogin;
                }
                else
                {
                    await _loginPageObject.LoginAccountDeskUser2();
                }
            }

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdate, frontOfficeCaseNumber);

            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _loanUpdateBillOfClosureObject.ClickSubmitBillOfClosureCases();
            await Task.Delay(1000);

            var backOfficeCaseNumber = string.Empty;

            if (setswap == true)
            {
                var (aLUCaseNumber, sWAPCaseNumber) = await _billOfClosureObject.GetSWAPandALLCaseNumber();
                _scenarioContext["savedSWAPCaseNumber"] = sWAPCaseNumber;
                _scenarioContext["savedALLCaseNumber"] = aLUCaseNumber;
                backOfficeCaseNumber = aLUCaseNumber;
            }
            else
            {
                var aLUCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
                _scenarioContext["savedALLCaseNumber"] = aLUCaseNumber;
                backOfficeCaseNumber = aLUCaseNumber;
            }

            await Task.Delay(1000);

            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                await _loanUpdateBillOfClosureObject.ClickALLCaseNumber(backOfficeCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanUpdateLoanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanUpdateLoanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(1000);

            if (loanType == "DEPOSIT")
            {
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.DepositAdministration, backOfficeCaseNumber);
                await _loanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();

                await _loanUpdateLoanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
                await _loanUpdateLoanDataApprovalObject.ClickSubmitButtonCheckProductData();
                await Task.Delay(1500);
            }
            else
            {
                await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, backOfficeCaseNumber);
                await _loanDataApprovalObject.OpenGetApprovalOpenLoanDataLink();
                await _loanUpdateLoanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);

                if (productType == EnumsForTRX.SelectProduct.BaseRateLoan)
                {
                    await _loanUpdateLoanDataApprovalObject.SetHasTheSwapBeenUpdated(true);
                }

                await _loanUpdateLoanDataApprovalObject.ClickSubmitButtonCheckProductData();

                await _basePageObject.WaitForResponse();

                string secID;
                if (loanType == "INTERESTSPREADRESET")
                {
                    secID = await _loanDataApprovalObject.GetSECIDLoanUpdate();
                }
                else
                {
                    secID = await _loanDataApprovalObject.GetSECIDNormalLoan();
                }

                _scenarioContext["savedSecID"] = secID;
            }
        }

        [When("I continue the loan update case to BO first flow LoanDataApproval")]
        public async Task WhenIContinueTheLoanUpdateCaseToBOFirstFlowLoanDataApproval()
        {
            var rLUCaseNumber = await _finalSpreadObject.GetCaseID();
            var loanType = (string)_scenarioContext["savedLoanType"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            await _finalSpreadObject.ClickSubmitFinalSpreadPage();
            await _winLostCorrectObject.ClickWinLostCorrect("Won");
            await Task.Delay(1000);
            await _winLostCorrectObject.SelectWonAlternative();
            await _winLostCorrectObject.UploadDocuments();

            await _loanUpdateWinLostCorrectObject.ClickSubmitWinLostCorrectPage();

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

            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdate, rLUCaseNumber);
            await _billOfClosureObject.ApprovedRejectOfferBoC(true);
            await _billOfClosureObject.ClickSubmitBillOfClosureCases();
            var aLUCaseNumber = await _loanUpdateBillOfClosureObject.GetALLCaseNumber();
            _scenarioContext["savedALLCaseNumber"] = aLUCaseNumber;

            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                await _loanUpdateBillOfClosureObject.ClickALLCaseNumber(aLUCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanUpdateLoanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanUpdateLoanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(1000);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanUpdateAdministration, aLUCaseNumber);

            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);

            if (productType == EnumsForTRX.SelectProduct.BaseRateLoan)
            {
                await _loanUpdateLoanDataApprovalObject.SetHasTheSwapBeenUpdated(true);
            }

            await _sendConfirmationObject.ClickSubmitSendConfirmation();

            // onderstaande uitgezet voor CLONE ENVIRONMENT pega upgrade//
            await _loanUpdateValidateRequestObject.SetCounterPartyConfirmReceived(true);

            // /////////////////// //   await _validateRequestObject.SetOtherTypeOfGuaranty("No");
            await _loanUpdateValidateRequestObject.SetIsTheLoanGuaranteed(true);
            await _loanUpdateValidateRequestObject.SetWhatIsTheLoanRequest("Split");
            await _loanUpdateValidateRequestObject.SetBillOfClosureConfirmedByPF(true);

            // await _validateRequestObject.SetMunicipalDecisionForGuaranty("Yes");
            await _loanUpdateValidateRequestObject.ClickSubmitValidationPage();

            // nieuwe stap alleen bij normal clients en waterschappen!
            if ((loanType == "INTERESTSPREADRESET" && productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
                || (loanType == "INTERESTSPREADRESET" && productType == EnumsForTRX.SelectProduct.BaseRateLoan))
            {
                await _loanUpdateCollectDocumentInformationObject.SetRepaymentAmount("250125100,95");
                await _basePageObject.PressTab();
                await _loanUpdateCollectDocumentInformationObject.ClickSubmitLoanUpdateCollectDocumentInformation();
            }

            // await _loanUpdateCreateContractObject.OpenCreateContractLink();
            await Task.Delay(2000);

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully created a BackOffice case for LUA-Case: " + aLUCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }

        [When(@"download the three csv files")]
        public async Task WhenDownloadTheThreeCsvFiles()
        {
            var aLLCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            string secID;

            // new implementation due to new class structure 1.85
            secID = await _loanDataApprovalObject.GetSECIDNormalLoan();

            _scenarioContext["savedSecID"] = secID;

            _scenarioContext["savedFilePathSECMNWB"] = await _createContractObject.DownloadFile("SECMNWB", prefixName, aLLCaseNumber);
            _scenarioContext["savedFilePathSECM"] = await _createContractObject.DownloadFile("SECM", prefixName, aLLCaseNumber);
            _scenarioContext["savedFilePathBRED"] = await _createContractObject.DownloadFile("BRED", prefixName, aLLCaseNumber);
            await Task.Delay(1500);

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully downloaded .csv files for BackOffice case: " + aLLCaseNumber);
            Console.WriteLine("With SECID: " + secID);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }

        [When(@"I download the ""([^""]*)"" file")]
        public async Task WhenIDownloadTheFile(string fileName)
        {
            var tC = (string)_scenarioContext["savedTC"];
            var aLLCaseNumber = (string?)_scenarioContext["savedALLCaseNumber"];
            var props = (IList<PropertyInfo>)_scenarioContext["savedCustomerProps"];
            var fieldsObject = (object)_scenarioContext["savedCustomerObject"];
            var sICCode = props[15].GetValue(fieldsObject).ToString();
            var textcifNumber = (string)_scenarioContext["savedCIFNumber"];

            if (fileName == "NWB Contract Annex Guarantor")
            {
                var guarantor = (string)_scenarioContext["savedGuarantor1"];
                if (guarantor != null && guarantor != string.Empty)
                {
                    var filepathVarNWBAnnexGuarantor = await _createContractObject.DownloadFile("NWB Contract Annex Guarantor", tC, aLLCaseNumber);
                    _scenarioContext["savedFilePathNWBAnnexGuarantor"] = filepathVarNWBAnnexGuarantor;
                }
            }
            else if (fileName == "NWB Contract Annex Conditions Precedent")
            {
                if (sICCode == "0020000000" || sICCode == "0021000000" || sICCode == "0022000000" || sICCode == "0023000000" ||
                     sICCode == "0024000000" || sICCode == "0024100000" || sICCode == "0039000000")
                {
                    var guarantor = (string)_scenarioContext["savedGuarantor1"];
                    if (guarantor != null && guarantor != string.Empty)
                    {
                    }
                    else
                    {
                        var filepathVarNWBAnnexPublic = await _createContractObject.DownloadFile("NWB Contract Annex Conditions Precedent Public", tC, aLLCaseNumber);
                        _scenarioContext["savedFilePathNWBAnnexConditions"] = filepathVarNWBAnnexPublic;
                    }
                }
                else
                {
                    var guarantor = (string)_scenarioContext["savedGuarantor1"];
                    if (guarantor != null && guarantor != string.Empty)
                    {
                        if (textcifNumber == "0636914607" || textcifNumber == "0636960641" || textcifNumber == "0636914739")
                        {
                            var filepathVarNWBAnnexExceptional = await _createContractObject.DownloadFile("NWB Contract Annex Conditions Precedent Exceptional", tC, aLLCaseNumber);
                            _scenarioContext["savedFilePathNWBAnnexConditions"] = filepathVarNWBAnnexExceptional;
                        }
                        else
                        {
                            var filepathVarNWBAnnexPrivate = await _createContractObject.DownloadFile("NWB Contract Annex Conditions Precedent Private", tC, aLLCaseNumber);
                            _scenarioContext["savedFilePathNWBAnnexConditions"] = filepathVarNWBAnnexPrivate;
                        }
                    }
                }
            }
            else
            {
                var selectDownload = fileName switch
                {
                    "Cover Letter" => _scenarioContext["savedFilePathCoverLetter"] = await _createContractObject.DownloadFile("Cover Letter", tC, aLLCaseNumber),
                    "WSW Contract" => _scenarioContext["savedFilePathWSWContract"] = await _createContractObject.DownloadFile("WSW Contract", tC, aLLCaseNumber),
                    "WSW Summary" => _scenarioContext["savedFilePathWSWSummary"] = await _createContractObject.DownloadFile("WSW Summary", tC, aLLCaseNumber),
                    "NWB Contract" => _scenarioContext["savedFilePathNWBContract"] = await _createContractObject.DownloadFile("NWB Contract", tC, aLLCaseNumber),
                    "DEPLO.csv" => _scenarioContext["savedFilePathIDNL"] = await _createContractObject.DownloadFile("DEPLO", tC, aLLCaseNumber),
                    "SECMNWB.csv" => _scenarioContext["savedFilePathSECMNWB"] = await _createContractObject.DownloadFile("SECMNWB", tC, aLLCaseNumber),
                    "SECM.csv" => _scenarioContext["savedFilePathSECM"] = await _createContractObject.DownloadFile("SECM", tC, aLLCaseNumber),
                    "BRED.csv" => _scenarioContext["savedFilePathBRED"] = await _createContractObject.DownloadFile("BRED", tC, aLLCaseNumber),
                    "WfZ Contract" => _scenarioContext["savedFilePathBRED"] = await _createContractObject.DownloadFile("WfZ Contract", tC, aLLCaseNumber),
                    "Cover Letter RH" => _scenarioContext["savedFilePathLoanUpdateCoverLetter"] = await _createContractObject.DownloadFile("Cover Letter RH", tC, aLLCaseNumber),
                    "WSW General Conditions" => _scenarioContext["savedFilePathGeneralConditions"] = await _createContractObject.DownloadFile("WSW General Conditions", tC, aLLCaseNumber),
                    "NWB Contract Annex Model Adjustment Request" => _scenarioContext["savedFilePathAnnexModelAdjustment"] = await _createContractObject.DownloadFile("NWB Contract Annex Model Adjustment Request", tC, aLLCaseNumber),
                    _ => throw new Exception("Unknown option chosen for switch expression selectDownload"),
                };
            }
        }

        [When("I forward the case to backoffice and generate all contracts, documents and files in stage CreateContract")]
        public async Task WhenIForwardTheCaseToBackofficeAndGenerateAllContractsDocumentsAndFilesInStageCreateContract()
        {
            await Task.Delay(500);
            var aLLCaseNumber = await _billOfClosureObject.GetALLCaseNumber();
            var loanType = (string)_scenarioContext["savedLoanType"];
            _scenarioContext["savedALLCaseNumber"] = aLLCaseNumber;
            var savedGuarantor1 = (string)_scenarioContext["savedGuarantor1"];

            if (_testSettings.RunModePRODUCTIONLIKE == true)
            {
                await Task.Delay(500);
                await _basePageObject.LogOutSystem();
                await Task.Delay(1000);
                await _loginPageObject.LoginBackOffice1();
            }
            else if (_testSettings.RunModePRODUCTIONLIKE != true)
            {
                await _billOfClosureObject.ClickALLCaseNumber(aLLCaseNumber);
                await _basePageObject.PageWaitForUrlLoaded();
                await _loanDataApprovalObject.OpenWaitInitiateDataLink();
                await _loanDataApprovalObject.ClickYesContinueButton();
                await _basePageObject.WaitForPageLoadedState();

                await _basePageObject.LogOutSystem();
                await _loginPageObject.LoginBackOffice1();
                await _basePageObject.WaitForPageLoadedState();
            }

            await Task.Delay(1000);
            await _basePageObject.SearchCaseNumberMainScreen(WorkListType.LoanAdministration, aLLCaseNumber);

            await _loanDataApprovalObject.SetHasCorrectDataBeenUploadedToOpics(true);
            await _sendConfirmationObject.ClickSubmitSendConfirmation();

            if (loanType == "SHORT")
            {
                await _basePageObject.ClickOpenSendConfirmation();
                await _basePageObject.WaitForResponse();
                await _sendConfirmationObject.SetConfirmationSentToCustomer("Yes");
                if (savedGuarantor1 != null && savedGuarantor1 != string.Empty)
                {
                    await _sendConfirmationObject.SetConfirmationSentToGuarantor("Yes");
                }

                await _sendConfirmationObject.ClickSubmitSendConfirmation();
            }
            else
            {
                await _validateRequestObject.SetCounterPartyConfirmReceived(true);
                await _validateRequestObject.SetIsTheLoanGuaranteed(true);
                await _validateRequestObject.SetWhatIsTheLoanRequest("Split");
                await _validateRequestObject.SetBillOfClosureConfirmedByPF(true);
                await _validateRequestObject.ClickSubmitValidationPage();
                await _createContractObject.ClickSubmitButtonCreateAgreement();
                await Task.Delay(1000);

                try
                {
                    await Task.Delay(1000);
                    var recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                    while (!recentContentFrameBOtext.Contains("No more data to load"))
                    {
                        await Task.Delay(1000);
                        await _basePageObject.ClickLoadMore();
                        await Task.Delay(1000);
                        recentContentFrameBOtext = await _basePageObject.GetTextRecentContentBO();
                        await Task.Delay(1000);
                    }
                }
                catch (PlaywrightException ex)
                {
                    // Handle any potential errors, such as element not found
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine("Succesfully created a BackOffice case for ALL-Case: " + aLLCaseNumber);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }

        [When("I open the sluitbrief")]
        public async Task WhenIOpenTheSluitbrief()
        {
            var backOfficeCaseNumber = (string)_scenarioContext["savedALLCaseNumber"];
            var frontOfficeCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var loanType = (string)_scenarioContext["savedLoanType"];
            var environment = _testSettings.ApplicationEnvironment;
            EnumsForTRX.SelectProduct productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            string productTypeString = productType.ToString();

            await Task.Delay(2000);

            IPage popupSluitbrief;
            if (loanType == "INTERESTSPREADRESET")
            {
                popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefLoanUpdate);
            }
            else
            {
                popupSluitbrief = await _winLostCorrectObject.OpenPopupWindow(EnumsForTRX.PopupWindow.SluitbriefNew);
            }

            var sluitBriefText = await _loanDataApprovalObject.GetSluitBriefBodyText(popupSluitbrief);
            _scenarioContext["savedSluitBriefTable"] = sluitBriefText;
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var filepathScreenshot = @$"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{backOfficeCaseNumber}_Sluitbrief.png";
            string downloadPath = System.IO.Path.Combine(_testSettings.DownloadFilePath, filepathScreenshot);
            _scenarioContext["savedEmailScreenshot"] = downloadPath;

            await popupSluitbrief.ScreenshotAsync(new()
            {
                Path = downloadPath,
                FullPage = true,

                // Type = Microsoft.Playwright.ScreenshotType.Png,
                // Caret = Microsoft.Playwright.ScreenshotCaret.Initial
            });
            await popupSluitbrief.CloseAsync();

            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
            Console.WriteLine($"Succesfully finished Front Office case for {loanType}: {frontOfficeCaseNumber}");
            Console.WriteLine($"Succesfully created a {loanType} sluitbrief for Back Office: {backOfficeCaseNumber}");
            Console.WriteLine($">>PRINTING ACTUAL SLUITBRIEF BELOW <<");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine(sluitBriefText);
            Console.WriteLine(">>>>>>>>>>----------------------------------------------------------<<<<<<<<<<");
        }
    }
}