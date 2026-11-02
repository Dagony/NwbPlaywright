using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;

namespace NwbPlaywright.PageObjects.CRM
{
    public class CrmLoginPageObject(
        IPlaywrightDriver playwrightDriver,
        TestSettings testSettings,
        IBasePageObject basePageObject,
        ICrmBasePageObject crmBasePageObject)
        : ICrmLoginPageObject
    {
        private readonly IPage page = playwrightDriver.Page.Result;
        private readonly IBrowserContext context = playwrightDriver.BrowserContext.Result;

        public async Task LoginCrmUser(EnumsForCRM.TestCrmUser options)
        {
            await crmBasePageObject.WaitForUrlLoaded();
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await loginButtonLocator.WaitForAsync();

            Task setTestUserDEV = options switch
            {
                EnumsForCRM.TestCrmUser.DevMarkdeWit => FillCredentialsAsync(testSettings.DevMarkdeWit, testSettings.DevMarkdeWitPassword),
                _ => throw new Exception("Unknown option chosen for switch expression setTestUserDEV in class LoginPageObject Task LoginCrmUser"),
            };
            await setTestUserDEV;

            await loginButtonLocator.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions
            {
                Timeout = 15000,
            });
            await basePageObject.WaitForResponse();
        }

        public async Task LoginToCrm()
        {
            Console.WriteLine("Login to CRM");
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new PageGetByPlaceholderOptions { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            await FillCredentialsAsync(testSettings.DevMarkdeWit, testSettings.DevMarkdeWitPassword);
            await loginButtonLocator.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
        }

        private async Task FillCredentialsAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("username or password is empty");
            }

            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await usernameLocator.FillAsync(username);
            await passwordLocator.FillAsync(password);
        }

        public async Task WaitForUrlLoaded()
        {
            if (testSettings.CrmApplicationUrl is not null)
            {
                if (testSettings.CrmApplicationUrl.Contains("dt1.pega.net/prweb/app/nwbcrmdev"))
                {
                    await page.WaitForURLAsync("https://nwbbnk-nwb-dt1.pega.net/prweb/app/nwbcrmdev/**", new PageWaitForURLOptions { WaitUntil = WaitUntilState.Load });
                }
            }
            else
            {
                throw new Exception("PageWaitForUrlLoaded(): TestSettings.CrmApplicationUrl is not initialized!");
            }
        }

        public Task<bool> IsPrivacyDialogVisible() => page.Locator("[data-testid=':privacy-dialog:']").IsVisibleAsync();

        public Task ClickPrivacyDialogAcceptButton() => page.Locator("button[data-testid=':privacy-dialog:accept']").ClickAsync();

        public Task<string> GetErrorMessage() => page.InnerTextAsync("#error");
    }

    public interface ICrmLoginPageObject
    {
        Task<string> GetErrorMessage();

        Task LoginToCrm();

        Task WaitForUrlLoaded();

        Task<bool> IsPrivacyDialogVisible();

        Task ClickPrivacyDialogAcceptButton();
    }
}

