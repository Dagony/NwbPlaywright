using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using System.IO;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using NwbPlaywright.PageObjects;
using NwbPlaywright.Support;
using NwbPlaywright.Extensions;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(6)]

namespace NwbPlaywright.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly Task<IPage> _page;

        private readonly ISharedDataIndividuals _sharedDataIndividuals;

        private readonly IBasePageObject _basePageObject;

        private readonly Task<IBrowserContext> _browserContext;

        private readonly TestSettings _testSettings;

        private static int _retryCount = 0;

        private const int MaxRetries = 2;

        public Hooks(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext, ISharedDataIndividuals sharedDataIndividuals, IBasePageObject pageObject)
        {
            _testSettings = testSettings;
            _page = playwrightDriver.Page;
            _sharedDataIndividuals = sharedDataIndividuals;
            _scenarioContext = scenarioContext;
            _basePageObject = pageObject;
            _browserContext = playwrightDriver.BrowserContext;
        }

        [AfterScenario(Order = 0)]
        public async Task AfterScenario()
        {
            _sharedDataIndividuals.ClearIndividuals();
            await _basePageObject.DeleteFilesAfterDownload(_testSettings.DeleteFilesAfterTestRun);

            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "playwright-logs");
            Directory.CreateDirectory(logDirectory);
            var logFileName = Path.Combine(logDirectory, _scenarioContext.ScenarioInfo.Title + ".log");
            _scenarioContext.ScenarioContainer.Resolve<IReqnrollOutputHelper>().AddAttachmentAsLink(logFileName);

            if (_testSettings.TracerEnabled)
            {
                var failed = _scenarioContext.TestError != null;

                if (failed)
                {
                    var traceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "playwright-traces");
                    Directory.CreateDirectory(traceDirectory); // Ensure directory exists
                    var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), "playwright-screenshots");
                    Directory.CreateDirectory(screenshotDirectory); // Ensure directory exists

                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var guid = Guid.NewGuid().ToString("N").Substring(0, 8);

                    var traceFilePath = Path.Combine(traceDirectory, $"{_scenarioContext.ScenarioInfo.Title}_{timestamp}.zip");
                    Console.WriteLine($"Trace file path: {traceFilePath}");

                    // Add scenario title to console for reference
                    Console.WriteLine($"Saving trace for: {_scenarioContext.ScenarioInfo.Title}");

                    var screenshotPath = Path.Combine(screenshotDirectory, $"{_scenarioContext.ScenarioInfo.Title}_{timestamp}_screenshot.png");
                    await (await _page).ScreenshotAsync(new() { Path = screenshotPath, FullPage = true, Type = ScreenshotType.Png });
                    _scenarioContext.ScenarioContainer.Resolve<IReqnrollOutputHelper>().AddScreenshotAsLink(screenshotPath);
                    await _browserContext.Result.Tracing.StopAsync(new TracingStopOptions { Path = traceFilePath });
                }
                else
                {
                    await _browserContext.Result.Tracing.StopAsync();
                }

                await _browserContext.Result.CloseAsync();
            }
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _retryCount = 0;

            if (_testSettings.ApplicationUrl is not null)
            {
                await (await _page).GotoAsync(_testSettings.ApplicationUrl);
                await (await _page).SetViewportSizeAsync(_testSettings.ViewSizePortLength, _testSettings.ViewSizePortWidth);

                if (_testSettings.ApplicationUrl.Contains("dt1"))
                {
                    _testSettings.ApplicationEnvironment = "DEV";
                }
                else if (_testSettings.ApplicationUrl.Contains("dt2"))
                {
                    _testSettings.ApplicationEnvironment = "TEST";
                }
                else if (_testSettings.ApplicationUrl.Contains("stg1"))
                {
                    _testSettings.ApplicationEnvironment = "ACCEPT";
                }
            }
            else
            {
                throw new Exception("_testSettings.ApplicationUrl is not initialized. Call from Hooks1.cs, public async Task BeforeScenario()");
            }
        }
    }
}