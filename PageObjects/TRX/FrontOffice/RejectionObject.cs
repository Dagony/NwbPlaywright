using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Extensions;
using TA.Playwright.Fields;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.PageObjects
{
    public interface IRejectionObject
    {
        Task ClickProductionSheet();

        Task OpenConfirmOfferRejection();

        Task ConfirmOrRejectOfferRejection(bool rejectYesNo);

        Task SelectAttachedDocument();

        Task SetRejectionReason(string rejectionComment);

        Task SubmitRejectionPage();

        Task OpenAttachRejectOfferConfirmation();
    }

    public class RejectionObject : IRejectionObject
    {
        public RejectionObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, IBasePageObject basePageObject)
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

        public async Task SelectAttachedDocument()
        {
            string dir1 = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(dir1)?.Parent?.Parent ?? throw new InvalidOperationException("Unable to determine the project root directory.");
            string filePath = Path.Combine(projectRoot.FullName, "dummy.txt");

            await pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Attach content" }).WaitForAsync();
            await pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Attach content" }).ClickAsync();

            await pageIpage1.GetByLabel("Select file(s)").WaitForAsync();
            await pageIpage1.GetByLabel("Select file(s)").SetInputFilesAsync(new[] { filePath });

            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Attach" }).First.WaitForAsync();
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Attach" }).First.ClickAsync();
        }

        public async Task SubmitRejectionPage()
        {
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).WaitForAsync();
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        }

        public async Task ClickProductionSheet()
        {
            var uGsheetButtonLocator = pageIpage1.Locator("xpath=(//button[normalize-space()='Production sheet'])[1]");
            await uGsheetButtonLocator.WaitForAsync();
            await uGsheetButtonLocator.ClickAsync();
        }

        public async Task OpenConfirmOfferRejection()
        {
            var openConfirmOfferRejectionLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Confirm offer rejection (Rejection)" });

            await openConfirmOfferRejectionLinkLocator.ClickAsync();
            await Task.Delay(800);
        }

        public async Task OpenAttachRejectOfferConfirmation()
        {
            var openConfirmOfferRejectionLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Attach reject offer confirmation (Rejection)" });

            await openConfirmOfferRejectionLinkLocator.ClickAsync();
            await Task.Delay(800);
        }

        public async Task ConfirmOrRejectOfferRejection(bool rejectYesNo)
        {
            if (rejectYesNo)
            {
                await pageIpage1.GetByText("Yes").First.ClickAsync();
            }
            else
            {
                await pageIpage1.GetByText("No").First.ClickAsync();
            }
        }

        public async Task SetRejectionReason(string rejectionComment)
        {
            await pageIpage1.GetByLabel("Reason").WaitForAsync();
            await pageIpage1.GetByLabel("Reason").FillAsync(rejectionComment);
        }
    }
}