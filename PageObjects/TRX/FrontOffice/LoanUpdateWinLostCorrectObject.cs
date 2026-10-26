using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.PageObjects.TRX
{
    public interface ILoanUpdateWinLostCorrectObject
    {
        Task ClickDealWonLost(bool dealIsWon);

        Task ClickSubmitWinLostCorrectPage();

        Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailBasic)> GetEmailBodyTextBasic(IPage page1);

        Task<string> GetEmailBodyTextProcurationMail(IPage ipage1);

        Task<bool> IsAttachmentVisible();

        Task OpenAttachment();

        Task OpenLoanUpdateWinLost();
    }

    public class LoanUpdateWinLostCorrectObject : ILoanUpdateWinLostCorrectObject
    {
        public LoanUpdateWinLostCorrectObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IBasePageObject basePageObject, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _basePageObject = basePageObject;
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly IBasePageObject _basePageObject;
        private readonly ScenarioContext _scenarioContext;

        public async Task OpenLoanUpdateWinLost() => await pageIpage1.GetByRole(AriaRole.Link, new() { Name = "CaptureOfferResults (Offer)" }).ClickAsync();

        public async Task ClickDealWonLost(bool dealIsWon)
        {
            if (dealIsWon == true)
            {
                await pageIpage1.GetByLabel("Yes").WaitForAsync();
                await pageIpage1.GetByLabel("Yes").CheckAsync();
            }
            else if (dealIsWon == false)
            {
                await pageIpage1.GetByLabel("No", new() { Exact = true }).WaitForAsync();
                await pageIpage1.GetByLabel("No", new() { Exact = true }).CheckAsync();
            }
        }

        public async Task SelectWonAlternative(string alternativeID)
        {
            if (alternativeID == "0")
            {
                throw new Exception("SelectWonAlternative must be 1 or higher! Index does not start at 0.");
            }

            await pageIpage1.GetByRole(AriaRole.Row, new() { Name = alternativeID, Exact = false }).GetByRole(AriaRole.Radio).CheckAsync();
        }

        public async Task ClickSubmitWinLostCorrectPage()
        {
            var submitWinLostCorrectButtonLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await Task.Delay(300);
            await submitWinLostCorrectButtonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitWinLostCorrectButtonLocator.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });

            await submitWinLostCorrectButtonLocator.DblClickAsync(new() { Delay = 200 });
            await _basePageObject.WaitForPageLoadedState();
        }

        public async Task OpenAttachment() => await pageIpage1.GetByText("Nederlandse Waterschapsbank", new() { Exact = false }).First.ClickAsync();

        public async Task<bool> IsAttachmentVisible() => await pageIpage1.GetByText("Nederlandse Waterschapsbank", new() { Exact = false }).First.IsVisibleAsync();

        public async Task<string> GetEmailBodyTextProcurationMail(IPage ipage1)
        {
            await Task.Delay(1000);
            var textEmailProcuration = await ipage1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("xpath=(//body)[1]").InnerTextAsync();
            return textEmailProcuration;
        }

        public async Task<(string addresseesEmail, string aanhefEmail, string tableEmail, string footTextEmailBasic)> GetEmailBodyTextBasic(IPage page1)
        {
            var addresseesEmail = await page1.Locator("(//tbody)[4]").InnerTextAsync();
            var aanhefEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("//span[contains(text(),'Beste')]").TextContentAsync();
            var tableEmail = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("(//table)[1]").InnerTextAsync();

            var footTextEmailBasic = await page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("xpath=(//body)[1]").InnerTextAsync();

            return (addresseesEmail, aanhefEmail, tableEmail.Trim(), footTextEmailBasic);
        }
    }
}