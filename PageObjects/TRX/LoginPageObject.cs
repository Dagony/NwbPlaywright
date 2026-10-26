using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using Task = System.Threading.Tasks.Task;

namespace NwbPlaywright.PageObjects
{
    public interface ILoginPageObject
    {
        Task<string> GetErrorMessage();

        Task SetLoginData(string userName, string passWord);

        Task LoginAccountDeskUser1();

        Task LoginAccountDeskUser2();

        Task LoginBackOffice1();

        Task LoginBackOffice2();

        Task LoginCreditManagement1();

        Task LoginCreditManagement2();

        Task LoginTreasury1();

        Task LoginTreasury2();

        Task LoginTreasuryManager1();

        Task LoginTreasuryManager2();

        Task LoginBOFundingDerivates1();

        Task LoginBOFundingDerivates2();

        Task LoginUser(EnumsForTRX.TestUser options);
    }

    public class LoginPageObject : ILoginPageObject
    {
        public LoginPageObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;

        private static bool AllNotNull(params object?[] values) => values.All(v => v is not null);

        public async Task SetLoginData(string userName, string passWord)
        {
            await page.WaitForLoadStateAsync();

            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });

            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            await usernameLocator.FillAsync(userName);
            await passwordLocator.FillAsync(passWord);
            await loginButtonLocator.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions
            {
                Timeout = 15000,
            });
        }

        public async Task LoginUser(EnumsForTRX.TestUser options)
        {
            await page.WaitForLoadStateAsync();
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await loginButtonLocator.WaitForAsync();

            if (_testSettings.ApplicationUrl.Contains("dt1"))
            {
                Console.WriteLine("Tests are running on: DT1");
                Task setTestUserDEV = options switch
                {
                    EnumsForTRX.TestUser.AccountDesk1 => FillCredentialsAsync(_testSettings.DevAccountDesk1, _testSettings.DevAccountDesk1Password),
                    EnumsForTRX.TestUser.AccountDesk2 => FillCredentialsAsync(_testSettings.DevAccountDesk2, _testSettings.DevAccountDesk2Password),
                    EnumsForTRX.TestUser.BackOffice1 => FillCredentialsAsync(_testSettings.DevBackOffice1, _testSettings.DevBackOffice1Password),
                    EnumsForTRX.TestUser.BackOffice2 => FillCredentialsAsync(_testSettings.DevBackOffice2, _testSettings.DevBackOffice2Password),
                    EnumsForTRX.TestUser.CreditManagement1 => FillCredentialsAsync(_testSettings.DevCreditManagement1, _testSettings.DevCreditManagement1Password),
                    EnumsForTRX.TestUser.CreditManagement2 => FillCredentialsAsync(_testSettings.DevCreditManagement2, _testSettings.DevCreditManagement2Password),
                    EnumsForTRX.TestUser.Treasury1 => FillCredentialsAsync(_testSettings.DevTreasury1, _testSettings.DevTreasury1Password),
                    EnumsForTRX.TestUser.Treasury2 => FillCredentialsAsync(_testSettings.DevTreasury2, _testSettings.DevTreasury2Password),
                    EnumsForTRX.TestUser.TreasuryManager1 => FillCredentialsAsync(_testSettings.DevTreasuryManager1, _testSettings.DevTreasuryManager1Password),
                    EnumsForTRX.TestUser.TreasuryManager2 => FillCredentialsAsync(_testSettings.DevTreasuryManager2, _testSettings.DevTreasuryManager2Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates1 => FillCredentialsAsync(_testSettings.DevBOFundingDerivates1, _testSettings.DevBOFundingDerivates1Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates2 => FillCredentialsAsync(_testSettings.DevBOFundingDerivates2, _testSettings.DevBOFundingDerivates2Password),
                    EnumsForTRX.TestUser.SpecializedFinanceTRX1 => FillCredentialsAsync(_testSettings.DevSpecializedFinanceTRX1, _testSettings.DevSpecializedFinanceTRX1Password),
                    _ => throw new Exception("Unknown option chosen for switch expression setTestUserDEV in class LoginPageObject Task LoginUser"),
                };
                await setTestUserDEV;
            }

            if (_testSettings.ApplicationUrl.Contains("dt2"))
            {
                Console.WriteLine("Tests are running on: DT2");
                Task setTestUserTEST = options switch
                {
                    EnumsForTRX.TestUser.AccountDesk1 => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk1, _testSettings.TestAccountDesk1Password),
                    EnumsForTRX.TestUser.AccountDesk2 => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk2, _testSettings.TestAccountDesk2Password),
                    EnumsForTRX.TestUser.BackOffice1 => FillCredentialsAsync(_testSettings.AcceptTestBackOffice1, _testSettings.TestBackOffice1Password),
                    EnumsForTRX.TestUser.BackOffice2 => FillCredentialsAsync(_testSettings.AcceptTestBackOffice2, _testSettings.TestBackOffice2Password),
                    EnumsForTRX.TestUser.CreditManagement1 => FillCredentialsAsync(_testSettings.AcceptTestCreditManagement1, _testSettings.TestCreditManagement1Password),
                    EnumsForTRX.TestUser.CreditManagement2 => FillCredentialsAsync(_testSettings.AcceptTestCreditManagement2, _testSettings.TestCreditManagement2Password),
                    EnumsForTRX.TestUser.Treasury1 => FillCredentialsAsync(_testSettings.AcceptTestTreasury1, _testSettings.TestTreasury1Password),
                    EnumsForTRX.TestUser.Treasury2 => FillCredentialsAsync(_testSettings.AcceptTestTreasury2, _testSettings.TestTreasury2Password),
                    EnumsForTRX.TestUser.TreasuryManager1 => FillCredentialsAsync(_testSettings.AcceptTestTreasuryManager1, _testSettings.TestTreasuryManager1Password),
                    EnumsForTRX.TestUser.TreasuryManager2 => FillCredentialsAsync(_testSettings.AcceptTestTreasuryManager2, _testSettings.TestTreasuryManager2Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates1 => FillCredentialsAsync(_testSettings.AcceptTestBOFundingDerivates1, _testSettings.TestBOFundingDerivates1Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates2 => FillCredentialsAsync(_testSettings.AcceptTestBOFundingDerivates2, _testSettings.TestBOFundingDerivates2Password),
                    EnumsForTRX.TestUser.SpecializedFinanceTRX1 => FillCredentialsAsync(_testSettings.AcceptTestSpecializedFinanceTRX1, _testSettings.TestSpecializedFinanceTRX1Password),
                    EnumsForTRX.TestUser.BackOffice1PrevRelease => FillCredentialsAsync(_testSettings.AcceptTestBackOffice1PrevRelease, _testSettings.TestBackOffice1PasswordPrevRelease),
                    EnumsForTRX.TestUser.BackOffice2PrevRelease => FillCredentialsAsync(_testSettings.AcceptTestBackOffice2PrevRelease, _testSettings.TestBackOffice2PasswordPrevRelease),
                    EnumsForTRX.TestUser.AccountDesk1PrevRelease => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk1PrevRelease, _testSettings.TestAccountDesk1PasswordPrevRelease),
                    EnumsForTRX.TestUser.AccountDesk2PrevRelease => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk2PrevRelease, _testSettings.TestAccountDesk2PasswordPrevRelease),
                    _ => throw new Exception("Unknown option chosen for switch expression setTestUserTEST in class LoginPageObject Task LoginUser"),
                };
                await setTestUserTEST;
            }

            if (_testSettings.ApplicationUrl.Contains("stg1"))
            {
                Console.WriteLine("Tests are running on: STG1");
                Task setTestUserACCEPT = options switch
                {

                    EnumsForTRX.TestUser.AccountDesk1 => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk1, _testSettings.AcceptAccountDesk1Password),
                    EnumsForTRX.TestUser.AccountDesk2 => FillCredentialsAsync(_testSettings.AcceptTestAccountDesk2, _testSettings.AcceptAccountDesk2Password),
                    EnumsForTRX.TestUser.BackOffice1 => FillCredentialsAsync(_testSettings.AcceptTestBackOffice1, _testSettings.AcceptBackOffice1Password),
                    EnumsForTRX.TestUser.BackOffice2 => FillCredentialsAsync(_testSettings.AcceptTestBackOffice2, _testSettings.AcceptBackOffice2Password),
                    EnumsForTRX.TestUser.CreditManagement1 => FillCredentialsAsync(_testSettings.AcceptTestCreditManagement1, _testSettings.AcceptCreditManagement1Password),
                    EnumsForTRX.TestUser.CreditManagement2 => FillCredentialsAsync(_testSettings.AcceptTestCreditManagement2, _testSettings.AcceptCreditManagement2Password),
                    EnumsForTRX.TestUser.Treasury1 => FillCredentialsAsync(_testSettings.AcceptTestTreasury1, _testSettings.AcceptTreasury1Password),
                    EnumsForTRX.TestUser.Treasury2 => FillCredentialsAsync(_testSettings.AcceptTestTreasury2, _testSettings.AcceptTreasury2Password),
                    EnumsForTRX.TestUser.TreasuryManager1 => FillCredentialsAsync(_testSettings.AcceptTestTreasuryManager1, _testSettings.AcceptTreasuryManager1Password),
                    EnumsForTRX.TestUser.TreasuryManager2 => FillCredentialsAsync(_testSettings.AcceptTestTreasuryManager2, _testSettings.AcceptTreasuryManager2Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates1 => FillCredentialsAsync(_testSettings.AcceptTestBOFundingDerivates1, _testSettings.AcceptBOFundingDerivates1Password),
                    EnumsForTRX.TestUser.BackOfficeFundingDerivates2 => FillCredentialsAsync(_testSettings.AcceptTestBOFundingDerivates2, _testSettings.AcceptBOFundingDerivates2Password),
                    _ => throw new Exception("Unknown option chosen for switch expression setTestUserACCEPT in class LoginPageObject Task LoginUser"),
                };
                await setTestUserACCEPT;
            }

            await loginButtonLocator.ClickAsync();
            await _basePageObject.WaitForResponse();
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

        public async Task LoginAccountDeskUser1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevAccountDesk1,
                _testSettings.DevAccountDesk1Password,
                _testSettings.AcceptTestAccountDesk1,
                _testSettings.TestAccountDesk1Password,
                _testSettings.AcceptAccountDesk1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevAccountDesk1);
                    await passwordLocator.FillAsync(_testSettings.DevAccountDesk1Password);
                    await loginButtonLocator.ClickAsync();
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestAccountDesk1);
                    await passwordLocator.FillAsync(_testSettings.TestAccountDesk1Password);
                    await loginButtonLocator.ClickAsync();
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestAccountDesk1);
                    await passwordLocator.FillAsync(_testSettings.AcceptAccountDesk1Password);
                    await loginButtonLocator.ClickAsync();
                }
            }
            else
            {
                throw new Exception("LoginAccountDeskUser1(): Passwords and/or ApplicationUrl not initialized in _testsettings!");
            }

            await Task.Delay(1000);
        }

        public async Task LoginAccountDeskUser2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevAccountDesk2,
                _testSettings.DevAccountDesk2Password,
                _testSettings.AcceptTestAccountDesk2,
                _testSettings.TestAccountDesk2Password,
                _testSettings.AcceptAccountDesk2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevAccountDesk2!);
                    await passwordLocator.FillAsync(_testSettings.DevAccountDesk2Password!);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestAccountDesk2);
                    await passwordLocator.FillAsync(_testSettings.TestAccountDesk2Password);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestAccountDesk2);
                    await passwordLocator.FillAsync(_testSettings.AcceptAccountDesk2Password);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
            }
            else
            {
                throw new Exception("LoginAccountDeskUser2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginBackOffice1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevBackOffice1,
                _testSettings.DevBackOffice1Password,
                _testSettings.AcceptTestBackOffice1,
                _testSettings.TestBackOffice1Password,
                _testSettings.AcceptBackOffice1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevBackOffice1);
                    await passwordLocator.FillAsync(_testSettings.DevBackOffice1Password);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBackOffice1);
                    await passwordLocator.FillAsync(_testSettings.TestBackOffice1Password);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBackOffice1);
                    await passwordLocator.FillAsync(_testSettings.AcceptBackOffice1Password);
                    await loginButtonLocator.ClickAsync();
                    await _basePageObject.WaitForResponse();
                }
            }
            else
            {
                throw new Exception("LoginBackOffice1(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginBackOffice2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevBackOffice2,
                _testSettings.DevBackOffice2Password,
                _testSettings.AcceptTestBackOffice2,
                _testSettings.TestBackOffice2Password,
                _testSettings.AcceptBackOffice2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevBackOffice2);
                    await passwordLocator.FillAsync(_testSettings.DevBackOffice2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBackOffice2);
                    await passwordLocator.FillAsync(_testSettings.TestBackOffice2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBackOffice2);
                    await passwordLocator.FillAsync(_testSettings.AcceptBackOffice2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginBackOffice2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginCreditManagement1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevCreditManagement1,
                _testSettings.DevCreditManagement1Password,
                _testSettings.AcceptTestCreditManagement1,
                _testSettings.TestCreditManagement1Password,
                _testSettings.AcceptCreditManagement1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevCreditManagement1);
                    await passwordLocator.FillAsync(_testSettings.DevCreditManagement1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestCreditManagement1);
                    await passwordLocator.FillAsync(_testSettings.TestCreditManagement1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestCreditManagement1);
                    await passwordLocator.FillAsync(_testSettings.AcceptCreditManagement1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginCreditManagement1(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginCreditManagement2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevCreditManagement2,
                _testSettings.DevCreditManagement2Password,
                _testSettings.AcceptTestCreditManagement2,
                _testSettings.TestCreditManagement2Password,
                _testSettings.AcceptCreditManagement2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevCreditManagement2);
                    await passwordLocator.FillAsync(_testSettings.DevCreditManagement2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestCreditManagement2);
                    await passwordLocator.FillAsync(_testSettings.TestCreditManagement2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestCreditManagement2);
                    await passwordLocator.FillAsync(_testSettings.AcceptCreditManagement2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginCreditManagement2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginTreasury1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevTreasury1,
                _testSettings.DevTreasury1Password,
                _testSettings.AcceptTestTreasury1,
                _testSettings.TestTreasury1Password,
                _testSettings.AcceptTreasury1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevTreasury1);
                    await passwordLocator.FillAsync(_testSettings.DevTreasury1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasury1);
                    await passwordLocator.FillAsync(_testSettings.TestTreasury1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasury1);
                    await passwordLocator.FillAsync(_testSettings.AcceptTreasury1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginTreasury2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginTreasury2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevTreasury2,
                _testSettings.DevTreasury2Password,
                _testSettings.AcceptTestTreasury2,
                _testSettings.TestTreasury2Password,
                _testSettings.AcceptTreasury2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevTreasury2);
                    await passwordLocator.FillAsync(_testSettings.DevTreasury2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasury2);
                    await passwordLocator.FillAsync(_testSettings.TestTreasury2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasury2);
                    await passwordLocator.FillAsync(_testSettings.AcceptTreasury2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginTreasury2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginTreasuryManager1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevTreasuryManager1,
                _testSettings.DevTreasuryManager1Password,
                _testSettings.AcceptTestTreasuryManager1,
                _testSettings.TestTreasuryManager1Password,
                _testSettings.AcceptTreasuryManager1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevTreasuryManager1);
                    await passwordLocator.FillAsync(_testSettings.DevTreasuryManager1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasuryManager1);
                    await passwordLocator.FillAsync(_testSettings.TestTreasuryManager1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasuryManager1);
                    await passwordLocator.FillAsync(_testSettings.AcceptTreasuryManager1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginTreasuryManager1(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginTreasuryManager2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevTreasuryManager2,
                _testSettings.DevTreasuryManager2Password,
                _testSettings.AcceptTestTreasuryManager2,
                _testSettings.TestTreasuryManager2Password,
                _testSettings.AcceptTreasuryManager2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevTreasuryManager2);
                    await passwordLocator.FillAsync(_testSettings.DevTreasuryManager2Password);
                    await loginButtonLocator.ClickAsync();
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasuryManager2);
                    await passwordLocator.FillAsync(_testSettings.TestTreasuryManager2Password);
                    await loginButtonLocator.ClickAsync();
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestTreasuryManager2);
                    await passwordLocator.FillAsync(_testSettings.AcceptTreasuryManager2Password);
                    await loginButtonLocator.ClickAsync();
                }
            }
            else
            {
                throw new Exception("LoginTreasuryManager2(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginBOFundingDerivates1()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevBOFundingDerivates1,
                _testSettings.DevBOFundingDerivates1Password,
                _testSettings.AcceptTestBOFundingDerivates1,
                _testSettings.TestBOFundingDerivates1Password,
                _testSettings.AcceptBOFundingDerivates1Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevBOFundingDerivates1);
                    await passwordLocator.FillAsync(_testSettings.DevBOFundingDerivates1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBOFundingDerivates1);
                    await passwordLocator.FillAsync(_testSettings.TestBOFundingDerivates1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBOFundingDerivates1);
                    await passwordLocator.FillAsync(_testSettings.AcceptBOFundingDerivates1Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginBOFundingDerivates1(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public async Task LoginBOFundingDerivates2()
        {
            await page.WaitForLoadStateAsync();
            var usernameLocator = page.GetByPlaceholder("User name");
            var passwordLocator = page.GetByPlaceholder("Password", new() { Exact = true });
            var loginButtonLocator = page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
            await usernameLocator.WaitForAsync();
            await passwordLocator.WaitForAsync();
            await loginButtonLocator.WaitForAsync();

            if (AllNotNull(
                _testSettings.ApplicationUrl,
                _testSettings.DevBOFundingDerivates2,
                _testSettings.DevBOFundingDerivates2Password,
                _testSettings.AcceptTestBOFundingDerivates2,
                _testSettings.TestBOFundingDerivates2Password,
                _testSettings.AcceptBOFundingDerivates2Password))
            {
                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    await usernameLocator.FillAsync(_testSettings.DevBOFundingDerivates2);
                    await passwordLocator.FillAsync(_testSettings.DevBOFundingDerivates2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBOFundingDerivates2);
                    await passwordLocator.FillAsync(_testSettings.TestBOFundingDerivates2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    await usernameLocator.FillAsync(_testSettings.AcceptTestBOFundingDerivates2);
                    await passwordLocator.FillAsync(_testSettings.AcceptBOFundingDerivates2Password);
                    await loginButtonLocator.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                }
            }
            else
            {
                throw new Exception("LoginBOFundingDerivates1(): Passwords and/or ApplicationUrl not initialized in TestSettings!");
            }
        }

        public Task<string> GetErrorMessage() => page.InnerTextAsync("#error");
    }
}