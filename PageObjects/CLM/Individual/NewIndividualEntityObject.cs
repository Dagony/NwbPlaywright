using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects.CLM.Individual
{
    public interface INewIndividualEntityObject
    {
        Task SetCountryOfCitizenship(string countryOfCitizenship);

        Task SetDateOfBirth(string dateOfBirth);

        Task SetFirstName(string firstName);

        Task SetGender(string gender);

        Task SetLastName(string lastName);

        Task SetMiddleName(string middleName);

        Task SetPrefix(string prefix);

        Task SetPreposition(string preposition);
    }

    public class NewIndividualEntityObject : INewIndividualEntityObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly IBasePageObject _basePageObject;

        public NewIndividualEntityObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            pageIpage0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            pageIpage1 = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
            _playwrightDriver = playwrightDriver;
            _basePageObject = basePageObject;
        }

        public async Task SetFirstName(string firstName)
        {
            // await Page.GetByLabel("First name").ClickAsync();
            await page.GetByLabel("First name").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("First name").FillAsync(firstName);
            await _basePageObject.PressTab();
        }

        public async Task SetPreposition(string preposition)
        {
            // await Page.GetByLabel("Preposition").ClickAsync();
            await page.GetByLabel("Preposition").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Preposition").FillAsync(preposition);
            await _basePageObject.PressTab();
        }

        public async Task SetLastName(string lastName)
        {
            // await Page.GetByLabel("Last name").ClickAsync();
            await page.GetByLabel("Last name").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Last name").FillAsync(lastName);
            await _basePageObject.PressTab();
        }

        public async Task SetMiddleName(string middleName)
        {
            // await Page.GetByLabel("Middle name").ClickAsync();
            await page.GetByLabel("Middle name").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Middle name").FillAsync(middleName);
            await _basePageObject.PressTab();
        }

        public async Task SetDateOfBirth(string dateOfBirth)
        {
            // await Page.GetByLabel("Date of birth").ClickAsync();
            await page.GetByLabel("Date of birth").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Date of birth").FillAsync(dateOfBirth);
            await _basePageObject.PressTab();
        }

        public async Task SetCountryOfCitizenship(string countryOfCitizenship)
        {
            // await Page.GetByLabel("Primary citizenship country name\r\n", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Primary citizenship country name\r\n", new() { Exact = true }).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Primary citizenship country name\r\n", new() { Exact = true }).FillAsync(countryOfCitizenship);
            await _basePageObject.PressTab();
        }

        public async Task SetPrefix(string prefix)
        {
            await page.GetByLabel("Prefix:").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByLabel("Prefix:").SelectOptionAsync(new[] { prefix });
        }

        public async Task SetGender(string gender)
        {
            await page.GetByText(gender, new() { Exact = true }).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await page.GetByText(gender, new() { Exact = true }).ClickAsync();
        }
    }
}