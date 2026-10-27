using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Extensions;
using NwbPlaywright.Fields;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.PageObjects
{
    public interface IWinLostCorrectObject
    {
        Task AssertAllObligatoryFieldsInTable(IPage page1, string caseNumber, string expectedGeldnemer, string expectedHoofdsom, string expectedIngangsDatum, string expectedEindDatum, string expectedCouponDag, string expectedAflossing, string expectedRente, string expectedRenteConventie, string expectedRenteFreq, string? optionalRenteHerziening = null, string? optionalEersteCouponDatum = null, string? optionalLaatsteCouponDatum = null, string? expectedAantalAflossingen = null, string? expectedAflossingsFreq = null, string? optionalZekerHeid = null, string? optionalOpmerkingen = null);

        Task AssertFooterEmailTextBasic(string actualBodyTextEmail);

        Task AssertFooterEmailTextWSW(string actualBodyTextEmail);

        Task AssertFooterTextOfferEmail(IPage page1, string expectedFooterContentEmail);

        Task AssertIfAanhefIsCorrectOfferEmail(IPage page1, string expectedAanhef);

        Task ClickLoanRequestLink();

        Task<IPage> OpenPopupWindow(PopupWindow options);

        Task OpenAttachment();

        Task UploadDocuments();

        Task<string> GetNameAttachmentOfferMail();

        Task SelectWonAlternative(int? alternativeNumbInCase = null);

        Task ClickSubmitWinLostCorrectPage();

        Task WaitForWinLostCorrect(string winLostCorrectOption, int? offerNumbInCase = null);

        Task ClickWinLostCorrect(string winLostCorrectOption, int? offerNumbInCase = null);

        Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailBasic, string tableEmailEng, string aanhefEng)> GetEmailBodyTextBasic(IPage page1);

        Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailWsw)> GetEmailBodyTextWsw(IPage page1);

        Task<int> GetNumberOfTableColumnOfferEmail(IPage page1);

        Task<int> GetNumberOfTableRowsOfferEmail(IPage page1);

        Task SetOutcomeBps(string outcomebps);

        Task SetOutcomeRemark(string outcomeremark);

        Task<bool> IsAttachmentVisible();

        Task<string> GetNumberOfAttachmentsOnPage();

        Task SetOutcomeRemarkAndBps(string outcomeRemark, string outcomeBps);

        Task SetSuretyStatementNumber(string suretyStatementNumber);

        Task SetSuretyStatementDate(string suretyStatementDate);

        Task SetSuretyStatementNumberAndDate(string suretyStatementNumber, string suretyStatementDate);

        Task<string> GetEmailBodyTextProcurationMail(IPage ipage1);
    }

    public class WinLostCorrectObject : IWinLostCorrectObject
    {
        public WinLostCorrectObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _counterpartyFieldLocator = pageIpage1.GetByPlaceholder("Enter counterparty name. Searches from 3 characters");
            _basePageObject = basePageObject;
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly ILocator _counterpartyFieldLocator;
        private readonly IBasePageObject _basePageObject;
        private readonly ScenarioContext _scenarioContext;

        /// Win Lost Correct Screen methods and buttons //
        public Task SetOutcomeRemark(string outcomeremark) => pageIpage1.GetByLabel("Outcome Remark").FillAsync(outcomeremark);

        public Task SetOutcomeBps(string outcomebps) => pageIpage1.GetByLabel("Outcome bps").FillAsync(outcomebps);

        public async Task SetOutcomeRemarkAndBps(string outcomeRemark, string outcomeBps)
        {
            await SetOutcomeRemark(outcomeRemark);
            await SetOutcomeBps(outcomeBps);
        }

        public async Task SetSuretyStatementNumber(string suretyStatementNumber)
        {
            await pageIpage1.GetByLabel("Surety statement number").WaitForAsync();
            await pageIpage1.GetByLabel("Surety statement number").FillAsync(suretyStatementNumber);
        }

        public async Task SetSuretyStatementDate(string suretyStatementDate)
        {
            await pageIpage1.GetByLabel("Surety statement date").WaitForAsync();
            await pageIpage1.GetByLabel("Surety statement date").FillAsync(suretyStatementDate);
        }

        public async Task SetSuretyStatementNumberAndDate(string suretyStatementNumber, string suretyStatementDate)
        {
            await SetSuretyStatementNumber(suretyStatementNumber);
            await SetSuretyStatementDate(suretyStatementDate);
        }

        public Task WaitForWinLostCorrect(string winLostCorrectOption, int? offerNumbInCase = null)
        {
            int caseOffer = offerNumbInCase ?? 1;

            return winLostCorrectOption switch
            {
                "Won" => pageIpage1.Locator($"(//label[normalize-space()='Won'])[{caseOffer}]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached }),
                "Lost" => pageIpage1.Locator($"(//label[normalize-space()='Lost'])[{caseOffer}]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached }),
                "Correct" => pageIpage1.Locator($"(//label[normalize-space()='Correct'])[{caseOffer}]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached }),
                _ => throw new Exception("Unknown option chosen for method public Task WaitForWinLostCorrect method"),
            };
        }

        public async Task ClickWinLostCorrect(string winLostCorrectOption, int? offerNumbInCase = null)
        {
            int caseOffer = offerNumbInCase ?? 1;

            if (new[] { "Won", "Lost", "Correct" }.Contains(winLostCorrectOption) == false)
            {
                throw new Exception("Unknown option chosen for method public Task ClickWinLostCorrect method");
            }
            else
            {
                await pageIpage1.Locator($"(//label[normalize-space()='{winLostCorrectOption}'])[{caseOffer}]").ClickAsync();
                await Assertions.Expect(pageIpage1.Locator($"(//label[normalize-space()='{winLostCorrectOption}'])[{caseOffer}]")).ToBeCheckedAsync();
            }
        }

        public async Task SelectWonAlternative(int? alternativeNumbInCase = null)
        {
            int altNumber = alternativeNumbInCase ?? 1;
            await pageIpage1.Locator($"[data-test-id=\"202511040850470794955-R{altNumber}\"] [data-test-id=\"2016072109335505834280\"]").CheckAsync();
            await Assertions.Expect(pageIpage1.Locator($"[data-test-id=\"202511040850470794955-R{altNumber}\"] [data-test-id=\"2016072109335505834280\"]")).ToBeCheckedAsync();
        }

        public async Task UploadDocuments()
        {
            string dir1 = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(dir1)?.Parent?.Parent;
            if (projectRoot == null)
            {
                throw new InvalidOperationException("Unable to determine the project root directory.");
            }

            string filePath = Path.Combine(projectRoot.FullName, "dummy.txt");

            await pageIpage1.Locator("[data-test-id=\"202511041102470571159\"]").ClickAsync();

            await pageIpage1.GetByLabel("Select file(s)").WaitForAsync();
            await pageIpage1.GetByLabel("Select file(s)").SetInputFilesAsync(new[] { filePath });

            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Attach" }).WaitForAsync();
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Attach" }).ClickAsync();
        }

        public Task ClickLoanRequestLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Create loan request (Creating loan request)" }).ClickAsync(new() { Force = true });

        public async Task ClickSubmitWinLostCorrectPage()
        {
            var submitWinLostCorrectButtonLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await Task.Delay(300);
            await submitWinLostCorrectButtonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitWinLostCorrectButtonLocator.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });

            await submitWinLostCorrectButtonLocator.DblClickAsync(new() { Delay = 200 });
            await _basePageObject.WaitForPageLoadedState();
        }

        public async Task<string> GetNumberOfAttachmentsOnPage()
        {
            var amountOfMailsLocator = pageIpage1.Locator("//span[contains(@class,'attachment-count text-bold force-height-auto force-width-auto')]//nobr[1]");
            await amountOfMailsLocator.WaitForAsync();
            var amountOfMailsText = await amountOfMailsLocator.TextContentAsync();

            if (amountOfMailsText is not null)
            {
                return amountOfMailsText;
            }
            else
            {
                throw new NullReferenceException("Variable 'amountOfMailsText' is null! Origin call from WinLostCorrectObject.GetNumberOfAttachmentsOnPage()");
            }
        }

        /// EMAIL BIJLAGE ////
        public Task<int> GetNumberOfTableRowsOfferEmail(IPage page1)
        {
            var tableRowsLocator = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("tr");
            var tableRows = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("tr").CountAsync();
            return tableRows;
        }

        public Task<int> GetNumberOfTableColumnOfferEmail(IPage page1)
        {
            var tableRowsLocator = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("tr");
            var tableColumns = tableRowsLocator.First.Locator("td").CountAsync();
            return tableColumns;
        }

        public async Task AssertIfAanhefIsCorrectOfferEmail(IPage page1, string expectedAanhef)
        {
            var aanhefInOfferEmailContent = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//span[contains(text(),'Beste')]").TextContentAsync();
            aanhefInOfferEmailContent.Should().Contain(expectedAanhef);
        }

        public async Task AssertFooterTextOfferEmail(IPage page1, string expectedFooterContentEmail)
        {
            var generalFootTextEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("xpath=//html[1]/body[1]/span[2]/span[1]").InnerTextAsync();

            var actual = _basePageObject.RemoveWhitespaceAndNewLines(generalFootTextEmail).ToLower();
            var expected = _basePageObject.RemoveWhitespaceAndNewLines(expectedFooterContentEmail).ToLower();

            Console.WriteLine($"Actual Text: {actual}");
            Console.WriteLine($"Expected Text: {expected}");

            actual.Should().Be(expected);
        }

        public async Task AssertAllObligatoryFieldsInTable(IPage page1, string caseNumber, string expectedGeldnemer, string expectedHoofdsom, string expectedIngangsDatum, string expectedEindDatum, string expectedCouponDag, string expectedAflossing, string expectedRente, string expectedRenteConventie, string expectedRenteFreq, string? optionalRenteHerziening = null, string? optionalEersteCouponDatum = null, string? optionalLaatsteCouponDatum = null, string? expectedAantalAflossingen = null, string? expectedAflossingsFreq = null, string? optionalZekerHeid = null, string? optionalOpmerkingen = null)
        {
            var aanhef = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//span[contains(text(),'Beste')]").TextContentAsync();
            var table = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("(//table)[1]").InnerTextAsync();
            var tableRowsLocator = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("tr");
            var stringEmptyTemp = caseNumber;

            var rowsCount = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("tr").CountAsync();
            var columnsCount = await tableRowsLocator.First.Locator("td").CountAsync();

            table.Trim().Should().ContainEquivalentOf(expectedGeldnemer);

            table.Trim().Should().ContainEquivalentOf(expectedIngangsDatum);
            table.Trim().Should().ContainEquivalentOf("Einddatum" + "\t" + expectedEindDatum);
            table.Trim().Should().ContainEquivalentOf(expectedCouponDag);
            table.Trim().Should().ContainEquivalentOf(expectedAflossing);

            table.Trim().Should().ContainEquivalentOf(expectedRente);
            table.Trim().Should().ContainEquivalentOf(expectedRenteConventie);
            table.Trim().Should().ContainEquivalentOf(expectedRenteFreq);
            table.Trim().Should().ContainEquivalentOf(expectedHoofdsom);
        }

        public async Task<IPage> OpenPopupWindow(PopupWindow options)
        {
            var popupWindow = await page.RunAndWaitForPopupAsync(async () =>
            {
                (Task emptyTask1, Task emptyTask2) choosePopup = options switch
                {
                    PopupWindow.Offertemail => (pageIpage1.Locator("xpath=(//a[contains(text(),'Offerte')])[1]").First.WaitForAsync(), pageIpage1.Locator("xpath=(//a[contains(text(),'Offerte')])[1]").First.ClickAsync()),

                    PopupWindow.SluitbriefLoanUpdate => (pageIpage1.GetByText("Een update lening", new() { Exact = false }).First.WaitForAsync(), pageIpage1.GetByText("Een update lening", new() { Exact = false }).First.ClickAsync()),

                    PopupWindow.SluitbriefNew => (pageIpage1.GetByText("Een nieuwe", new() { Exact = false }).First.WaitForAsync(), pageIpage1.GetByText("Een nieuwe", new() { Exact = false }).First.ClickAsync()),

                    _ => throw new Exception("Unknown option chosen for switch expression popupName switch expression"),
                };

                await choosePopup.emptyTask1;
                await choosePopup.emptyTask2;
            });
            return popupWindow;
        }

        public async Task OpenAttachment() => await pageIpage1.Locator("xpath=(//a[contains(text(),'Nederlandse Waterschapsbank N.V. - Algemene Kredie')])[1]").First.ClickAsync();

        public async Task<bool> IsAttachmentVisible()
        {
            return await pageIpage1.Locator("xpath=(//a[contains(text(),'Nederlandse Waterschapsbank N.V. - Algemene Kredie')])[1]").First.IsVisibleAsync();
        }

        public async Task<string> GetNameAttachmentOfferMail()
        {
            var name = await pageIpage1.Locator("div[id = 'ATTACHMENT-PREVIEW-HEADER-INFO-TITLE'] h1").TextContentAsync() ?? string.Empty;
            return name;
        }

        public async Task<string> GetEmailBodyTextProcurationMail(IPage ipage1)
        {
            await Task.Delay(1000);
            var textEmailProcuration = await ipage1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("xpath=(//body)[1]").InnerTextAsync();
            return textEmailProcuration;
        }

        public async Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailBasic, string tableEmailEng, string aanhefEng)> GetEmailBodyTextBasic(IPage page1)
        {
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 15000 });
            await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//body").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            var addresseesEmail = await page1.Locator("(//table)[4]").InnerTextAsync();

            var aanhefEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//body").InnerTextAsync();

            var tableEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//body").InnerTextAsync();
            var loanType = (string)_scenarioContext["savedLoanType"];

            var footTextEmailBasic = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("xpath=(//body)[1]").InnerTextAsync();
            string tableEmailEng = string.Empty;
            string aanhefEng = string.Empty;

            if (loanType != "DEPOSIT")
            {
                tableEmailEng = "English Table should not be present due to different loanType. Call origin from GetEmailBodyTextBasic(IPage page1)";
                aanhefEng = "English Aanhef should not be present due to different loanType. Call origin from GetEmailBodyTextBasic(IPage page1)";
            }

            return (addresseesEmail, aanhefEmail, tableEmail.Trim(), footTextEmailBasic, tableEmailEng, aanhefEng);
        }

        public async Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailWsw)> GetEmailBodyTextWsw(IPage page1)
        {
            var addresseesEmail = await page1.Locator("(//tbody)[4]").InnerTextAsync();

            var aanhefEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//html/body/span[1]").TextContentAsync();
            var tableEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("(//table)[1]").InnerTextAsync();
            var footTextEmailWsw = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//html[1]/body[1]/span[3]/span[1]").InnerTextAsync();

            return (addresseesEmail, aanhefEmail, tableEmail.Trim(), footTextEmailWsw);
        }

        public async Task AssertFooterEmailTextBasic(string actualBodyTextEmail)
        {
            EmailContentFields expectedEmailData = new EmailContentFields();

            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceOne);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceTwo);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicSentenceThree);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicSentenceFour);

            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceOne);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceTwo);
        }

        public async Task AssertFooterEmailTextWSW(string actualBodyTextEmail)
        {
            EmailContentFields expectedEmailData = new EmailContentFields();

            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextWSWSentenceTwo);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextWSWSentenceThree);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextWSWSentenceFour);

            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceOne);
            actualBodyTextEmail.Should().ContainEquivalentOf(expectedEmailData.FootTextBasicFinalSentenceTwo);
        }
    }
}