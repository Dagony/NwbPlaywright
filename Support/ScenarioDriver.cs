using Microsoft.Playwright;
using Reqnroll;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.PageObjects;

namespace TA.Playwright.Support
{
    public class ScenarioDriver
    {
        public Task<IPage> Page { get; }

        public Task<IBrowserContext> BrowserContext { get; }

        public ScenarioContext ScenarioContext { get; }

        public TestSettings TestSettings { get; }

        public IBasePageObject BasePageObject { get; }

        public IPlaywrightDriver PlaywrightDriver { get; }

        public ISharedDataIndividuals SharedDataIndividuals { get; }

        public ScenarioDriver(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ISharedDataIndividuals sharedDataIndividuals)
        {
            PlaywrightDriver = playwrightDriver;
            Page = playwrightDriver.Page;
            BrowserContext = playwrightDriver.BrowserContext;
            ScenarioContext = scenarioContext;
            TestSettings = testSettings;
            BasePageObject = basePageObject;
            SharedDataIndividuals = sharedDataIndividuals;
        }
    }
}