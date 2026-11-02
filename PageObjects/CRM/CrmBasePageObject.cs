using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.PageObjects.CRM.Components;
using static Microsoft.Playwright.Assertions;

namespace NwbPlaywright.PageObjects.CRM
{
    public class CrmBasePageObject : ICrmBasePageObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;

        private readonly ICrmLoginPageObject _crmLoginPageObject;
        private readonly ICrmMenuComponent _menuComponent;

        public CrmBasePageObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, ICrmMenuComponent menuComponent)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
            _menuComponent = menuComponent;
            pageIpage0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            pageIpage1 = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
        }

        public Task WaitForSpinnerToDisappear() => pageIpage1.Locator("span[id=\"pega_ui_load\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden, Timeout = 60000 });

        public async Task WaitForUrlLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 15000 });
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 15000 });
        }

        public async Task WaitForPageToBeLoaded()
        {
            var el = page.Locator("h1:has-text(\"NWB CRM - DEV\")");
            await Expect(el).ToBeVisibleAsync(new() { Timeout = 10_000 });
        }

        public async Task NavigateToMenuItem(string menuItem)
        {
            await _menuComponent.OpenMenuItem(menuItem);
        }
    }

    public interface ICrmBasePageObject
    {
        Task WaitForSpinnerToDisappear();

        Task WaitForUrlLoaded();

        Task WaitForPageToBeLoaded();

        Task NavigateToMenuItem(string menuItem);
    }
}
