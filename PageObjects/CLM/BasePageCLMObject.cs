using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects.CLM
{
    public interface IBasePageCLMObject
    {
        Task CreateNewIndividual();

        Task CreateNewOrganisation();
    }

    public class BasePageCLMObject : IBasePageCLMObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public BasePageCLMObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
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

        public async Task CreateNewIndividual()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "New onboarding" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await page.GetByPlaceholder("Search for customer...").FillAsync("test");
            await page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create new" }).ClickAsync();
            await page.WaitForLoadStateAsync();
        }

        public async Task CreateNewOrganisation()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "New onboarding" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "v", Exact = true }).ClickAsync();
            await page.GetByPlaceholder("Search for customer...").ClickAsync();
            await page.GetByPlaceholder("Search for customer...").FillAsync("test");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search in CRM database" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search CompanyInfo" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create new" }).ClickAsync();
        }
    }
}