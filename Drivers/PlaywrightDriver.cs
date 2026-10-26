using DocumentFormat.OpenXml.InkML;
using TA.Playwright.Config;

namespace TA.Playwright.Drivers
{
    public interface IPlaywrightDriver
    {
        Task<IBrowser> Browser { get; }

        Task<IBrowserContext> BrowserContext { get; }

        Task<IPage> Page { get; }

        void Dispose();
    }

    public class PlaywrightDriver : IDisposable, IPlaywrightDriver
    {
        private readonly AsyncLazy<IBrowser> _browser;
        private readonly AsyncLazy<IBrowserContext> _browserContext;
        private readonly AsyncLazy<IPage> _page;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly TestSettings _testSettings;
        private bool _isDisposed;

        public PlaywrightDriver(TestSettings testSettings, ScenarioContext scenarioContext, IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = testSettings;
            _playwrightDriverInitializer = playwrightDriverInitializer;

            _browser = new AsyncLazy<IBrowser>(InitializePlaywright);
            _browserContext = new AsyncLazy<IBrowserContext>(CreateBrowserContext);
            _page = new AsyncLazy<IPage>(CreatePageAsync);
            _scenarioContext = scenarioContext;
        }

        public Task<IPage> Page => _page.Value;

        public Task<IBrowser> Browser => _browser.Value;

        public Task<IBrowserContext> BrowserContext => _browserContext.Value;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_browser.IsValueCreated)
            {
                Task.Run(async () =>
                {
                    await (await Browser).CloseAsync();
                    await (await Browser).DisposeAsync();
                });
            }

            _isDisposed = true;
        }

        private async Task<IBrowser> InitializePlaywright()
        {
            return _testSettings.DriverType switch
            {
                DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
                DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
                DriverType.Edge => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
                DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
                _ => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
            };
        }

        private async Task<IPage> CreatePageAsync()
        {
            // return await (await _browser).NewPageAsync();
            // change voor implementatie tracing.
            return await (await _browserContext).NewPageAsync();
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            // return await (await _browser).NewContextAsync();
            // change voor implementatie tracing.
            var context = await (await _browser).NewContextAsync();

            if (_testSettings.TracerEnabled)
            {
                await context.Tracing.StartAsync(new TracingStartOptions
                {
                    Title = $"{_scenarioContext.ScenarioInfo.Title}",
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true,
                });
            }

            return context;
        }
    }
}