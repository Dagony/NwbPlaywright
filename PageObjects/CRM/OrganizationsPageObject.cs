using System.Diagnostics.CodeAnalysis;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using static Microsoft.Playwright.Assertions;

namespace NwbPlaywright.PageObjects.CRM
{
    public class CrmOrganizationsPageObject : ICrmOrganizationsPageObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;

        private readonly ICrmBasePageObject _crmBasePageObject;
        ILocator pageTitle;
        ILocator organizationsTable;
        ILocator organizationsTableHeader;
        ILocator organizationsTableRows;
        private ILocator searchFieldOpenButton;
        private ILocator searchFieldInput;
        private ILocator searchFieldSearchButton;
        private ILocator searchFieldResultCount;
        
        public CrmOrganizationsPageObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, ICrmBasePageObject crmBasePageObject)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            _crmBasePageObject = crmBasePageObject;
            pageTitle = page.Locator("h1");
            organizationsTable = page.Locator("div.container[role='grid']");
            organizationsTableHeader = organizationsTable.Locator("div.sticky-header[role='rowgroup'] div[role='row']").First;
            organizationsTableRows = organizationsTable.Locator("div[role='rowgroup']").GetByRole(AriaRole.Listitem);
            searchFieldOpenButton = page.Locator("button[data-testid=':list-toolbar:search-button']");
            searchFieldInput = page.Locator(".pega-search-input input[data-testid=':list-toolbar:search-input']");
            searchFieldSearchButton =
                page.GetByTestId(":list-toolbar:").GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Search" });
            searchFieldResultCount = page.Locator("span[data-testid=':list-toolbar:count']");
        }

        public async Task IsOrganizationsPageShowing()
        {
            await Expect(pageTitle).ToHaveTextAsync("Organizations");
        }

        public async Task<string> GetPageTitle()
        {
            await pageTitle.WaitForAsync(new LocatorWaitForOptions { Timeout = 10000, State = WaitForSelectorState.Visible });
            return await pageTitle.InnerTextAsync();
        }

        public async Task SearchForOrganization(string searchTerm, string? expectedResultCount = "0 results")
        {
            await searchFieldOpenButton.ClickAsync();
            await searchFieldInput.FillAsync(searchTerm);
            await searchFieldSearchButton.ClickAsync();
            await Expect(searchFieldResultCount).ToHaveTextAsync(expectedResultCount);
        }

        public async Task AddOrganization()
        {
            // click Plus button in menu
            // Select Organization
            // In popup, provide all necessary fields
            // Click Submit
        }
    }

    public interface ICrmOrganizationsPageObject
    {
        Task<string> GetPageTitle();

        Task IsOrganizationsPageShowing();

        Task SearchForOrganization(string searchTerm, string? expectedResultCount);

        Task AddOrganization();
    }
}

