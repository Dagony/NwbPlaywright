using Aspose.Words.Fields;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.PageObjects
{
    public interface IBasePageObject
    {
        Task PageWaitForUrlLoaded();

        Task WaitForPageLoadedState();

        Task PressBackSpace();

        Task WaitForResponse();

        Task ClickBackOfficeOperations();

        Task ClickDashboardButton();

        Task ClickLogOut();

        Task CloseTabInPega();

        Task ClickLoadMore();

        Task ReopenLoanUpdateCase();

        Task OpenCreateOffer();

        Task SelectLoanProduct(EnumsForTRX.SelectProduct option);

        Task SelectLoanEvent(EnumsForTRX.SelectEvent option);

        Task ClickOpenCreateOfferLink();

        Task ClickSubmitButton();

        Task ClickOpenSendConfirmationDeposit();

        Task DebounceDom(ILocator locator, int pollDelay = 50, int stableDelay = 500);

        Task<string> TransformSpreadDataToStringWithThreeDecimalPlaces(string spread);

        Task WaitForLoadFinalSpreadPage();

        Task<string> CalculateExpectedMinSpreadUG(string fundingCurve, string fTP);

        Task ClickLoanUpdateOpenValidateRequestLink();

        Task<string> GetTextRecentContentBO();

        Task CreateScreenshotSWAP(string filepath);

        Task SearchCaseNumberMainScreen(WorkListType worklist, string caseNumber);

        Task ClickOpenCaseButton();

        Task ClickOpenValidateRequestLink();

        Task<string> GetFOCaseStatus();

        Task<string> CompareFOCaseStatus(string expectedCaseStatus);

        Task ClickOtherActions();

        public string GetRandomNotional(int mimimum, int maximum);

        Task ClickProfileMenu();

        Task ClickSearchButtonMagnify();

        Task ClickPricingGridButton();

        Task OpenCaptureOfferResultsLoanUpdateLink();

        Task ClickSubmitTaskMenu();

        Task SelectLoanRequest(EnumsForTRX.LoanRequest option);

        Task EnterSearchPEGA();

        Task<string> GetCaseStatus();

        Task<string> TransformSpreadDataMarketRateToStringWithThreeDecimalPlaces(string finalspread, string marketIRate);

        Task PauseAsync();

        Task<string> GetUserTextTopPage();

        Task GrantGeoLocationPermission();

        Task LogOutSystem();

        Task GetCustomerInformationLoanUpdateAutomatic();

        Task SearchSecIDMainScreen(WorkListType worklist, string secID);

        Task OpenCaseOnScreen(string caseNumber);

        Task OpenCreateLoanRequestLink();

        Task OpenTaskMenu();

        Task PressArrowDown();

        Task PressEnter();

        Task PressPageDown();

        Task PressTab();

        Task ClickOpenCheckSurepayLink();

        Task CloseAttachmentFile();

        Task ClickOpenSendConfirmation();

        Task PressTextSmallerChromeEdge();

        string RemoveWhitespaceAndNewLines(string text);

        Task SearchPEGA(string searchQuery);

        Task GetCustomerInformation();

        Task SelectFadedOutLoanRequestFixed();

        double TransformVariableCustomerRateToDouble(string finalSpread, string? basisFinalSpread = "");

        IEnumerable<string> Generate();

        Task ClickSelectContactInitializationLink();

        Task<bool> User1AccountDesk1IsVisible();

        Task DeleteFilesAfterDownload(bool? deleteFiles);

        Task WaitForLoginPageLoad();

        Task WaitForSpinnerToDisappear();

        /// <summary>
        /// Reloading the page. This is sometimes needed to fetch the current state of the backend.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task ReloadPage();
    }

    public class BasePageObject : IBasePageObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public BasePageObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            pageIpage0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            pageIpage1 = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
            _playwrightDriver = playwrightDriver;
        }

        public IEnumerable<string> Generate()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                yield return new string(c, 1);
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                for (char d = 'A'; d <= 'Z'; d++)
                {
                    yield return new string(new[] { c, d });
                }
            }
        }

        public Task ReloadPage() => page.ReloadAsync();

        public Task WaitForPageLoadedState() => page.WaitForLoadStateAsync();

        public Task WaitForResponse() => page.WaitForResponseAsync(response => response.Status == 200, new PageWaitForResponseOptions { Timeout = 60000 });

        public Task WaitForLoadFinalSpreadPage() => page.Locator("#pyDisplayHarnessWithoutImage").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        public string RemoveWhitespaceAndNewLines(string text)
        {
            return Regex.Replace(text, @"\s", string.Empty);
        }

        public Task PressArrowDown() => page.Keyboard.PressAsync("ArrowDown", new KeyboardPressOptions { Delay = 100 });

        public Task PressEnter() => page.Keyboard.PressAsync("Enter", new KeyboardPressOptions { Delay = 100 });

        public Task PressPageDown() => page.Keyboard.PressAsync("PageDown");

        public Task PressBackSpace() => page.Keyboard.PressAsync("Backspace");

        public Task PressTab() => page.Keyboard.PressAsync("Tab", new KeyboardPressOptions { Delay = 100 });

        public Task PressTextSmallerChromeEdge() => page.Keyboard.PressAsync("Control+-");

        public Task PauseAsync() => _playwrightDriver.Page.Result.PauseAsync();

        public Task WaitForLoginPageLoad() => page.WaitForResponseAsync(response => response.Url == "https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,400;0,600;0,700;1,400;1,600;1,700&display=swap" && response.Status == 200 && response.StatusText == "OK", new PageWaitForResponseOptions { Timeout = 60000 });

        public Task WaitForSpinnerToDisappear() => page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("span[id=\"pega_ui_load\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden, Timeout = 60000 });

        public async Task DebounceDom(ILocator locator, int pollDelay = 100, int stableDelay = 1000)
        {
            var markupPrevious = string.Empty;
            var timerStart = DateTime.Now;
            var isStable = false;
            while (!isStable)
            {
                var markupCurrent = await locator.InnerHTMLAsync();
                if (markupCurrent == markupPrevious)
                {
                    var elapsed = (DateTime.Now - timerStart).TotalMilliseconds;
                    isStable = stableDelay <= elapsed;
                }
                else
                {
                    markupPrevious = markupCurrent;
                }

                if (!isStable)
                {
                    await Task.Delay(pollDelay);
                }
            }
        }

        public async Task CreateScreenshotSWAP(string filepath)
        {
            await pageIpage1.Locator("#modalOverlay div").Filter(new() { HasText = "Swap Request Swap Request pls" }).Nth(1).ScreenshotAsync(new LocatorScreenshotOptions
            {
                Path = filepath,
                Type = Microsoft.Playwright.ScreenshotType.Png,
            });
        }

        public async Task GetCustomerInformation()
        {
            var organisationPropertiesObject = new OrganisationFields();
            PropertyInfo[] properties = typeof(OrganisationFields).GetProperties();

            await pageIpage1.Locator("i[title='NoCommunity']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await pageIpage1.Locator("div[class='content-item content-field item-1 remove-top-spacing flex'] div[class='field-item dataValueRead'] span").WaitForAsync();

            for (int i = 4; i < 21; i++)
            {
                var propertyLocators = pageIpage1.Locator($"(//div[contains(@class,'field-item dataValueRead')])[{i}]");
                await propertyLocators.WaitForAsync();
                var textValue = await propertyLocators.TextContentAsync();
                properties[i - 4].SetValue(organisationPropertiesObject, textValue);
            }

            // index 0 = CustomerType
            // index 1 = Country of Incorporation
            // index 2 = Date of Incorporation
            // index 3 = status
            // index 4 = Opics customer type
            // index 5 = Risk classsification approval date
            // index 6 = Risk code
            // index 7 = counterparty
            // index 8 = legal form
            // index 9 = legal entity identifier
            // index 10 = CoC Number
            // index 11 = TaxID
            // index 12 = SBI number
            // index 13 = SBI description
            // index 14 = SICCode
            // index 15 = SICCODE description
            Type myType = organisationPropertiesObject.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            var textcifNumber = await pageIpage1.Locator("div[class='content-item content-field item-1 remove-top-spacing flex'] div[class='field-item dataValueRead'] span").TextContentAsync();

            _scenarioContext["savedCustomerProps"] = props;
            _scenarioContext["savedCustomerObject"] = organisationPropertiesObject;
            _scenarioContext["savedCIFNumber"] = textcifNumber;
            _scenarioContext["savedCDDreviewDate"] = props[5].GetValue(organisationPropertiesObject).ToString();
        }

        public async Task GetCustomerInformationLoanUpdateAutomatic()
        {
            var organisationPropertiesObject = new OrganisationFields();
            PropertyInfo[] properties = typeof(OrganisationFields).GetProperties();
            await pageIpage1.Locator("div[class='content-item content-field item-1 remove-top-spacing flex'] div[class='field-item dataValueRead'] span").WaitForAsync();

            for (int i = 21; i < 37; i++)
            {
                var propertyLocators = pageIpage1.Locator($"(//div[contains(@class,'field-item dataValueRead')])[{i}]");
                await propertyLocators.WaitForAsync();
                var textValue = await propertyLocators.TextContentAsync();
                properties[i - 21].SetValue(organisationPropertiesObject, textValue);
            }

            // index 0 = CustomerType
            // index 1 = Country of Incorporation
            // index 2 = Date of Incorporation
            // index 3 = status
            // index 4 = Opics customer type
            // index 5 = Risk classsification approval date
            // index 6 = Risk code
            // index 7 = counterparty
            // index 8 = legal form
            // index 9 = legal entity identifier
            // index 10 = CoC Number
            // index 11 = TaxID
            // index 12 = SBI number
            // index 13 = SBI description
            // index 14 = SICCode
            // index 15 = SICCODE description
            Type myType = organisationPropertiesObject.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            var textcifNumber = await pageIpage1.Locator("div[class='content-item content-field item-1 remove-top-spacing flex'] div[class='field-item dataValueRead'] span").TextContentAsync();

            _scenarioContext["savedCustomerProps"] = props;
            _scenarioContext["savedCustomerObject"] = organisationPropertiesObject;
            _scenarioContext["savedCIFNumber"] = textcifNumber;
            _scenarioContext["savedCDDreviewDate"] = props[5].GetValue(organisationPropertiesObject).ToString();
        }

        public async Task<string> TransformSpreadDataToStringWithThreeDecimalPlaces(string spread)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            if (spread.Contains(","))
            {
                var spreadWithPointInsteadOfComma = spread.Replace(",", ".");
                double doubleFinalSpread = double.Parse(spreadWithPointInsteadOfComma, CultureInfo.InvariantCulture);

                string formattedNumber = doubleFinalSpread.ToString("F3", dutchCulture);
                await Task.CompletedTask;
                return formattedNumber;
            }
            else
            {
                double doubleFinalSpread = double.Parse(spread, CultureInfo.InvariantCulture);
                string formattedNumber = doubleFinalSpread.ToString("F3", dutchCulture);
                await Task.CompletedTask;
                return formattedNumber;
            }
        }

        public async Task<string> TransformSpreadDataMarketRateToStringWithThreeDecimalPlaces(string finalSpread, string marketIRate)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var insertFinalSpread = finalSpread;
            var insertMarketIRate = marketIRate;

            if (finalSpread.Contains(","))
            {
                insertFinalSpread = finalSpread.Replace(",", ".");
            }

            if (marketIRate.Contains(","))
            {
                insertMarketIRate = marketIRate.Replace(",", ".");
            }

            double doubleFinalSpread = double.Parse(insertFinalSpread);
            double doubleFinalSpreadBPS = doubleFinalSpread / 100;
            double doublemarketIRate = double.Parse(insertMarketIRate);

            double doubleFixedCustomerRate = doubleFinalSpreadBPS + doublemarketIRate;
            var roundedFixedCustomerRate = Math.Round(doubleFixedCustomerRate, 3, MidpointRounding.ToEven);
            var formattedFixedCustomerRate = roundedFixedCustomerRate.ToString("F3", dutchCulture);
            await Task.CompletedTask;
            return formattedFixedCustomerRate;
        }

        public async Task<string> CalculateExpectedMinSpreadUG(string fundingCurve, string ftp)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var insertFundingCurve = fundingCurve;
            var insertFTP = ftp;

            if (fundingCurve.Contains(","))
            {
                insertFundingCurve = fundingCurve.Replace(",", ".");
            }

            if (ftp.Contains(","))
            {
                insertFTP = ftp.Replace(",", ".");
            }

            double doubleFundingCurve = double.Parse(insertFundingCurve);
            double doubleFPS = double.Parse(insertFTP);

            double minSpreadUG = doubleFundingCurve + doubleFPS;
            var roundedMinSpreadUG = Math.Round(minSpreadUG, 1, MidpointRounding.ToEven);
            await Task.CompletedTask;
            return roundedMinSpreadUG.ToString("F1", dutchCulture);
        }

        public double TransformVariableCustomerRateToDouble(string finalSpread, string? basisFinalSpread = "")
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var insertFinalSpread = finalSpread;
            var insertBasisFinalSpread = basisFinalSpread;

            if (finalSpread.Contains(","))
            {
                insertFinalSpread = finalSpread.Replace(",", ".");
            }

            if (basisFinalSpread != null && basisFinalSpread != string.Empty)
            {
                if (basisFinalSpread.Contains(","))
                {
                    insertBasisFinalSpread = basisFinalSpread.Replace(",", ".");
                }

                double doubleFinalSpread = double.Parse(insertFinalSpread);
                double doubleBasisFinalSpread = double.Parse(insertBasisFinalSpread);

                double doubleVariableCustomerRate = doubleFinalSpread + doubleBasisFinalSpread;
                return doubleVariableCustomerRate;
            }
            else
            {
                double doubleFinalSpread = double.Parse(insertFinalSpread, CultureInfo.InvariantCulture);
                return doubleFinalSpread;
            }
        }

        // Hoofdscherm knoppen //
        public async Task OpenTaskMenu()
        {
            var imagePhoneTopLeftCorner = pageIpage1.Locator("img[title='Phone call']");
            var openTaskMenu = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Add task" }).First;
            await imagePhoneTopLeftCorner.WaitForAsync(new LocatorWaitForOptions { Timeout = 10000, State = WaitForSelectorState.Visible });
            await openTaskMenu.WaitForAsync(new LocatorWaitForOptions { Timeout = 10000, State = WaitForSelectorState.Visible });
            await openTaskMenu.FocusAsync();
            await openTaskMenu.ClickAsync(new LocatorClickOptions { Delay = 200 });
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
        }

        public async Task SelectLoanRequest(EnumsForTRX.LoanRequest option)
        {
            ILocator? locatorLoanRequest = null;
            _scenarioContext["savedLoanType"] = string.Empty;
            var chooseDownload = option switch
            {
                EnumsForTRX.LoanRequest.Loan => (locatorLoanRequest = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Loan" }).First, _scenarioContext["savedLoanType"] = "LOAN"),
                EnumsForTRX.LoanRequest.Deposit => (locatorLoanRequest = pageIpage1.GetByText("Deposit", new() { Exact = true }).First, _scenarioContext["savedLoanType"] = "DEPOSIT"),
                EnumsForTRX.LoanRequest.Other => (locatorLoanRequest = pageIpage1.GetByText("Other", new() { Exact = true }).First, _scenarioContext["savedLoanType"] = "OTHER"),
                EnumsForTRX.LoanRequest.InterestSpreadReset => (locatorLoanRequest = pageIpage1.GetByText("Interest / spread reset", new() { Exact = true }).First, _scenarioContext["savedLoanType"] = "INTERESTSPREADRESET"),
                EnumsForTRX.LoanRequest.OtherUpdate => (locatorLoanRequest = pageIpage1.GetByText("Other update", new() { Exact = true }).First, _scenarioContext["savedLoanType"] = "OTHERUPDATE"),
                _ => throw new Exception("Unknown option chosen for switch expression chooseLoanRequestOption"),
            };

            await locatorLoanRequest.WaitForAsync();
            await locatorLoanRequest.DblClickAsync();
            await WaitForSpinnerToDisappear();
            await WaitForLoadFinalSpreadPage();

            if (option == EnumsForTRX.LoanRequest.Deposit)
            {
                await ClickOpenCreateOfferLink();
                _scenarioContext["savedProductType"] = EnumsForTRX.SelectProduct.Deposit;
            }
        }

        public async Task SelectLoanProduct(EnumsForTRX.SelectProduct option)
        {
            string? chosenProductOption = null;
            var chooseProductOption = option switch
            {

                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong => chosenProductOption = "Traditional fixed rate loan Long",
                EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional => chosenProductOption = "Floating rate loan Fixed notional",
                EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional => chosenProductOption = "Floating rate loan Floating notional",
                EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort => chosenProductOption = "Traditional fixed rate loan Short",
                EnumsForTRX.SelectProduct.BaseRateLoan => chosenProductOption = "Base rate loan",
                EnumsForTRX.SelectProduct.MTNLocalAuthority => chosenProductOption = "MTN local authority",
                EnumsForTRX.SelectProduct.SustainableLinkedLoan => chosenProductOption = "Sustainable linked loan",
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFixedrateFixedNotional => chosenProductOption = "Export Credit Guarantee Fixed rate Fixed notional",
                EnumsForTRX.SelectProduct.FixedRateLoanWithPrefinancingPeriod => chosenProductOption = "Fixed rate loan with pre-financing period",
                EnumsForTRX.SelectProduct.ShortTermFacility => chosenProductOption = "Short term facility",
                EnumsForTRX.SelectProduct.FloatingRateLoanCapFloor => chosenProductOption = "Floating rate loan cap floor",
                EnumsForTRX.SelectProduct.WSWObligo => chosenProductOption = "WSW Obligo",
                EnumsForTRX.SelectProduct.VATFacility => chosenProductOption = "VAT Facility",
                EnumsForTRX.SelectProduct.RevolvingCreditFacility => chosenProductOption = "Revolving credit facility",
                EnumsForTRX.SelectProduct.DebtServiceReserveFacility => chosenProductOption = "Debt service reserve facility",
                EnumsForTRX.SelectProduct.TermLoanFacility => chosenProductOption = "Term loan facility",
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateFixedNotional => chosenProductOption = "Export Credit Guarantee Floating rate Fixed notional",
                EnumsForTRX.SelectProduct.ExportCreditGuaranteeFloatingRateVariableNotional => chosenProductOption = "Export Credit Guarantee Floating rate Variable notional",
                EnumsForTRX.SelectProduct.Miscellaneous => chosenProductOption = "Miscellaneous",
                _ => throw new Exception("Unknown option chosen for switch expression chooseProductOption"),
            };

            _scenarioContext["savedProductType"] = option;

            await pageIpage1.Locator("[data-test-id=\"202510151544200071497\"]").WaitForAsync();
            await pageIpage1.Locator("[data-test-id=\"202510151544200071497\"]").SelectOptionAsync(new[] { chosenProductOption });
        }

        public async Task SelectLoanEvent(EnumsForTRX.SelectEvent option)
        {
            string? chosenEventOption = null;
            var chooseEventOption = option switch
            {

                EnumsForTRX.SelectEvent.Primary => chosenEventOption = "Primary",
                EnumsForTRX.SelectEvent.Secondary => chosenEventOption = "Secondary",
                EnumsForTRX.SelectEvent.Doorzak => chosenEventOption = "Doorzak",
                EnumsForTRX.SelectEvent.InterestReset => chosenEventOption = "Interest reset",
                EnumsForTRX.SelectEvent.SpreadReset => chosenEventOption = "Spread reset",
                EnumsForTRX.SelectEvent.Restructuring => chosenEventOption = "Restructuring",
                EnumsForTRX.SelectEvent.ExtendableOption => chosenEventOption = "Extendable option",
                EnumsForTRX.SelectEvent.DrawdownFacility => chosenEventOption = "Drawdown facility",
                EnumsForTRX.SelectEvent.EarlyRepaymentRestructuring => chosenEventOption = "Early repayment restructuring",
                EnumsForTRX.SelectEvent.EarlyRepaymentMarketValue => chosenEventOption = "Early repayment market value",
                EnumsForTRX.SelectEvent.EarlyRepaymentResetDate => chosenEventOption = "Early repayment reset date",
                EnumsForTRX.SelectEvent.EarlyRepaymentDoorzak => chosenEventOption = "Early repayment doorzak",
                EnumsForTRX.SelectEvent.Miscellaneous => chosenEventOption = "Miscellaneous",
                _ => throw new Exception("Unknown option chosen for switch expression chooseEventOption"),
            };

            _scenarioContext["savedEventType"] = chosenEventOption;
            await pageIpage1.GetByLabel("Event").WaitForAsync();
            await pageIpage1.GetByLabel("Event").SelectOptionAsync(new[] { chooseEventOption });
        }

        public async Task ClickSubmitButton()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync(new() { Force = false, Delay = 100, });
        }

        public async Task ReopenLoanUpdateCase()
        {
            await pageIpage2.GetByRole(AriaRole.Link, new() { Name = "Loan Update" }).ClickAsync();
        }

        public async Task SelectFadedOutLoanRequestFixed()
        {
            var selectFadedOutLoanRequestLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Loan request (long) Fixed" }).First;
            await selectFadedOutLoanRequestLocator.WaitForAsync(new LocatorWaitForOptions { Timeout = 10000, State = WaitForSelectorState.Visible });
            await selectFadedOutLoanRequestLocator.DblClickAsync();
            await WaitForLoadFinalSpreadPage();
        }

        public async Task ClickSubmitTaskMenu()
        {
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Add tasks" }).
                WaitForAsync(new LocatorWaitForOptions { Timeout = 10000, State = WaitForSelectorState.Visible });
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Add tasks" }).ClickAsync();
            await PageWaitForUrlLoaded();
        }

        public async Task LogOutSystem()
        {
            await Task.Delay(250);
            var profileMenuLocator = page.Locator("[data-test-id=\"px-opr-image-ctrl\"]");
            var logOutOption = page.GetByRole(AriaRole.Menuitem, new() { Name = "Logout" });

            await profileMenuLocator.WaitForAsync();
            await profileMenuLocator.ClickAsync();

            void Page_Dialog_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= Page_Dialog_EventHandler;
            }

            page.Dialog += Page_Dialog_EventHandler;
            await logOutOption.WaitForAsync();
            await logOutOption.ClickAsync(new LocatorClickOptions
            {
                Delay = 200,
            });
            await PageWaitForUrlLoaded();
        }

        public Task ClickProfileMenu() => page.Locator("[data-test-id=\"px-opr-image-ctrl\"]").ClickAsync();

        public Task ClickLogOut() => page.GetByRole(AriaRole.Menuitem, new() { Name = "Log out" }).ClickAsync(new LocatorClickOptions
        {
            Delay = 200,
        });

        // FRONT OFFICE BASE PAGE //
        public Task SearchPEGA(string searchQuery) => page.GetByPlaceholder("Search...").FillAsync(searchQuery);

        public Task EnterSearchPEGA() => page.GetByRole(AriaRole.Button, new() { Name = "Search for an item" }).ClickAsync();

        public Task ClickSearchButtonMagnify() => pageIpage1.GetByRole(AriaRole.Menuitem, new() { Name = "Start research" }).ClickAsync();

        public Task ClickOtherActions() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Other actions" }).GetByRole(AriaRole.Button).ClickAsync();

        public Task ClickOpenCaseButton() => pageIpage1.GetByRole(AriaRole.Menuitem, new() { Name = "Start research" }).ClickAsync();

        public async Task SearchCaseNumberMainScreen(WorkListType worklist, string caseNumber)
        {
            ILocator caseFilterLocator;
            var chosenTestUser = (EnumsForTRX.TestUser)_scenarioContext["savedLoggedInTestUser"];

            if (chosenTestUser == TestUser.Treasury1 || chosenTestUser == TestUser.Treasury2 || chosenTestUser == TestUser.TreasuryManager1 || chosenTestUser == TestUser.TreasuryManager2)
            {
                var setFilterLocator = worklist switch
                {
                    WorkListType.Loan => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[9]"),
                    WorkListType.Deposit => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[9]"),
                    WorkListType.SWAP => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[15]"),
                    WorkListType.LoanUpdate => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[9]"),
                    WorkListType.LoanUpdateAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.LoanAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.DepositAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.SWAPAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[1]"),
                    _ => throw new Exception("Unknown option chosen for setFilterLocator, origin call from SearchCaseNumberMainScreen()"),
                };
            }
            else
            {
                var setFilterLocator = worklist switch
                {
                    WorkListType.Loan => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[28]"),
                    WorkListType.SWAP => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[15]"),
                    WorkListType.LoanUpdate => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[28]"),
                    WorkListType.Deposit => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[28]"),

                    WorkListType.LoanAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.LoanUpdateAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.DepositAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[13]"),
                    WorkListType.SWAPAdministration => caseFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[1]"),
                    _ => throw new Exception("Unknown option chosen for setFilterLocator, origin call from SearchCaseNumberMainScreen()"),
                };
            }

            var caseFilterSearchFieldLocator = pageIpage0.GetByLabel("Search Text");
            var caseFilterApplyLocator = pageIpage0.GetByRole(AriaRole.Button, new() { Name = "Apply" });
            var clickCaseFromSearch = pageIpage0.GetByRole(AriaRole.Cell, new() { Name = caseNumber, Exact = true }).First;

            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await caseFilterLocator.WaitForAsync();
            await caseFilterLocator.ClickAsync();
            await Task.Delay(1000);
            await Assertions.Expect(caseFilterSearchFieldLocator).ToBeVisibleAsync();
            await caseFilterSearchFieldLocator.WaitForAsync();
            await caseFilterSearchFieldLocator.FillAsync(caseNumber);
            await this.PressEnter();
            await clickCaseFromSearch.ClickAsync();
        }

        public async Task SearchSecIDMainScreen(WorkListType worklist, string secID)
        {
            ILocator secIDFilterLocator;

            var setFilterLocator = worklist switch
            {
                WorkListType.LoanUpdate => secIDFilterLocator = pageIpage0.Locator("xpath=(//a[@id='pui_filter'])[32]"),
                _ => throw new Exception("Unknown option chosen for setFilterLocator, origin call from SearchCaseNumberMainScreen()"),
            };

            var caseFilterSearchFieldLocator = pageIpage0.GetByLabel("Search Text");
            var caseFilterApplyLocator = pageIpage0.GetByRole(AriaRole.Button, new() { Name = "Apply" });
            var clickCaseFromSearch = pageIpage0.GetByRole(AriaRole.Cell, new() { Name = secID, Exact = true }).First;
            await secIDFilterLocator.WaitForAsync();
            await secIDFilterLocator.ClickAsync();
            await caseFilterSearchFieldLocator.WaitForAsync();
            await caseFilterSearchFieldLocator.FillAsync(secID);
            await caseFilterApplyLocator.ClickAsync();
            await clickCaseFromSearch.WaitForAsync();
            await clickCaseFromSearch.ClickAsync();
        }

        public async Task<string> GetTextRecentContentBO()
        {
            var textRecentContentBOLocator = pageIpage1.Locator("xpath=(//div[contains(@class,'progressive-onclick')])[1]");

            await textRecentContentBOLocator.WaitForAsync();
            var textRecentContentBO = await textRecentContentBOLocator.TextContentAsync();
            if (textRecentContentBO is not null)
            {
                return textRecentContentBO;
            }
            else
            {
                throw new Exception("textRecentContentBO seemed to be null or empty!");
            }
        }

        public async Task ClickLoadMore()
        {
            var loadMoreLocator = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByRole(AriaRole.Link, new FrameLocatorGetByRoleOptions { Name = "Load more" });
            await loadMoreLocator.ClickAsync(new LocatorClickOptions { Delay = 200 });
        }

        public async Task ClickBackOfficeOperations()
        {
            var backOfficeOperationsButtonLocator = pageIpage0.GetByText("BackOffice Operations");
            await backOfficeOperationsButtonLocator.WaitForAsync();
            await backOfficeOperationsButtonLocator.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public Task OpenCreateLoanRequestLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Create loan request (Creating loan request)" }).ClickAsync();

        public Task OpenCreateOffer() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Create offer (Offer)" }).ClickAsync();

        public Task OpenCaptureOfferResultsLoanUpdateLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "CaptureOfferResults (Offer)" }).ClickAsync();

        public async Task ClickDashboardButton()
        {
            var backOfficeDashboardButton = pageIpage0.GetByText("Dashboard");
            await backOfficeDashboardButton.WaitForAsync();
            await backOfficeDashboardButton.ClickAsync();
        }

        public async Task ClickPricingGridButton()
        {
            var backOfficeDashboardButton = pageIpage0.GetByLabel("Pricing grid");

            await backOfficeDashboardButton.WaitForAsync();
            await backOfficeDashboardButton.ClickAsync();
        }

        public string GetRandomNotional(int mimimum, int maximum)
        {
            Random random = new Random();
            string notional = random.Next(mimimum, maximum).ToString();
            string formattedNotional = string.Format(CultureInfo.GetCultureInfo("nl-NL"), "{0:#,0}", long.Parse(notional));
            return formattedNotional;
        }

        public async Task OpenCaseOnScreen(string caseNumber)
        {
            var caseNumberRowLocatorNEWNEWNEW = pageIpage0.GetByRole(AriaRole.Cell, new() { Name = caseNumber, Exact = true }).First;
            await caseNumberRowLocatorNEWNEWNEW.WaitForAsync();
            await caseNumberRowLocatorNEWNEWNEW.ClickAsync();
        }

        public Task CloseTabInPega() => page.Locator("//span[@id='close']").ClickAsync();

        public Task CloseAttachmentFile() => pageIpage1.Locator("//i[@class='pi pi-times']").ClickAsync();

        public Task ClickOpenCreateOfferLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Create offer (Offer)" }).ClickAsync();

        public Task ClickOpenValidateRequestLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Validate request (Validate request)" }).ClickAsync();

        public Task ClickSelectContactInitializationLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Select contact (Create)" }).ClickAsync();

        public Task ClickLoanUpdateOpenValidateRequestLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Validate request (Validate)" }).ClickAsync();

        public Task ClickOpenCheckSurepayLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Check Surepay (Validate request)" }).ClickAsync();

        public Task ClickOpenSendConfirmation() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "SendConfirmation (Validate request)" }).ClickAsync();

        public Task ClickOpenSendConfirmationDeposit() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Send confirmation (Administration)" }).ClickAsync();

        public async Task<string> GetCaseStatus()
        {
            var caseStatusALLLocator = pageIpage1.Locator("(//span[normalize-space()='Resolved-Completed'])[1]");

            await caseStatusALLLocator.WaitForAsync();
            var textCaseStatus = await caseStatusALLLocator.InnerTextAsync();
            return textCaseStatus;
        }

        public async Task<string> GetFOCaseStatus()
        {
            var caseStatusALLLocator = pageIpage1.Locator(".content-item.content-field.item-1.remove-left-spacing.flex.custom-control .field-item.dataValueRead");
            await caseStatusALLLocator.WaitForAsync();
            var textCaseStatus = await caseStatusALLLocator.InnerTextAsync();
            return textCaseStatus;
        }

        public async Task<string> CompareFOCaseStatus(string caseStatus)
        {
            const string caseStatusSelector = ".content-item.content-field.item-1.remove-left-spacing.flex.custom-control .field-item.dataValueRead";

            // TODO: Research why pageIpage2 does not always exist
            var parent = caseStatus == "Open-OrderConfirmation" && await pageIpage2.Locator(caseStatusSelector).IsVisibleAsync()
                ? pageIpage2
                : pageIpage1;

            var caseStatusALLLocator = parent.Locator(caseStatusSelector);
            await caseStatusALLLocator.WaitForAsync();
            return await caseStatusALLLocator.InnerTextAsync();
        }

        public Task<bool> User1AccountDesk1IsVisible() => pageIpage0.GetByText("TestUser AccountDesk1").First.IsVisibleAsync();

        public Task<string> GetUserTextTopPage() => pageIpage0.Locator("div[class='content-item content-field item-2 remove-top-spacing remove-bottom-spacing remove-left-spacing heading_4_dataLabelRead custom-control'] div[class='field-item dataValueRead']").InnerTextAsync();

        public async Task GrantGeoLocationPermission()
        {
            await page.Context.GrantPermissionsAsync(new[] { "geolocation" });
        }

        public async Task PageWaitForUrlLoaded()
        {
            if (_testSettings.ApplicationUrl is not null)
            {
                if (_testSettings.ApplicationUrl.Contains("dt1.pega"))
                {
                    await page.WaitForURLAsync("https://nwbbnk-nwb-dt1.pega.net/**", new PageWaitForURLOptions { WaitUntil = WaitUntilState.Load });
                }

                if (_testSettings.ApplicationUrl.Contains("dt2.pega"))
                {
                    await page.WaitForURLAsync("https://nwbbnk-nwb-dt2.pega.net/**", new PageWaitForURLOptions { WaitUntil = WaitUntilState.Load });
                }

                if (_testSettings.ApplicationUrl.Contains("stg1.pega"))
                {
                    await page.WaitForURLAsync("https://nwbbnk-nwb-stg1.pega.net/**", new PageWaitForURLOptions { WaitUntil = WaitUntilState.Load });
                }

                if (_testSettings.ApplicationUrl.Contains("dt1-clone1"))
                {
                    await page.WaitForURLAsync("https://nwbbnk-nwb-dt1-clone1.pega.net/**", new PageWaitForURLOptions { WaitUntil = WaitUntilState.Load });
                }
            }
            else
            {
                throw new Exception("PageWaitForUrlLoaded(): TestSettings.ApplicationUrl is not initialized!");
            }
        }

        public Task<bool> ReviewAgreementAuthorizationError() => pageIpage2.GetByText("You are not authorized to perform this assignment.").IsVisibleAsync();

        public async Task DeleteFilesAfterDownload(bool? deleteFiles)
        {
            if (deleteFiles == true)
            {
                string[] keys =
                {
                    "savedUGSheetFilepath", "savedEmailScreenshot", "savedFilePathNWBAnnexConditions", "savedFilePathNWBAnnexGuarantor", "savedFilePathCoverLetter", "savedFilePathWSWContract",
                    "savedFilePathWSWSummary", "savedFilePathNWBContract", "savedFilePathIDNL", "savedFilePathSECM", "savedFilePathSECMNWB", "savedFilePathBRED",
                };
                foreach (var key in keys)
                {
                    if (_scenarioContext.ContainsKey(key))
                    {
                        var fileLocation = (string)_scenarioContext[key];
                        if (File.Exists(fileLocation))
                        {
                            File.Delete(fileLocation);
                        }
                        else
                        {
                            Console.WriteLine($"File to be deleted not found! Call from Hooks.cs AfterScenario(), filepath was: {fileLocation}");
                        }
                    }
                }

                await Task.CompletedTask;
            }
        }
    }
}