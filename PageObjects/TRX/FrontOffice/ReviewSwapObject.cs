using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects.TRX.FrontOffice
{
    public class ReviewSwapObject
    {
        public ReviewSwapObject(
            IPlaywrightDriver playwrightDriver,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ScenarioContext scenarioContext)
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
        private readonly ILocator _includeInMailBoxLocator;
        private readonly ScenarioContext _scenarioContext;
    }
}