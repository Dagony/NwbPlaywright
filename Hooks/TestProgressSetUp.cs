using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using TA.Playwright.Hooks;

/// <summary>
/// Counts the tests that will run in this test run. Declared outside any namespace so NUnit
/// runs it once for the whole assembly, before any feature.
/// </summary>
[SetUpFixture]
public class TestProgressSetUp
{
    [OneTimeSetUp]
    public void CountTests()
    {
        TestProgress.Total = CountScheduledTests() ?? CountRunnableTests(TestExecutionContext.CurrentContext.CurrentTest);
        TestProgress.WriteLine($"{TestProgress.Cyan}Progress: 0/{TestProgress.Total} tests run{TestProgress.Reset}");
    }

    // The work item tree only holds the tests that passed the run filter (e.g. --filter, or the tests
    // selected in Test Explorer). NUnit doesn't expose the current work item publicly, so it's read via reflection.
    private static int? CountScheduledTests()
    {
        var worker = TestExecutionContext.CurrentContext.TestWorker;
        var field = typeof(TestWorker).GetField("_currentWorkItem", BindingFlags.NonPublic | BindingFlags.Instance);

        return worker is not null && field?.GetValue(worker) is CompositeWorkItem workItem
            ? CountRunnableWorkItems(workItem)
            : null;
    }

    private static int CountRunnableWorkItems(WorkItem workItem) => workItem is CompositeWorkItem composite
        ? composite.Children.Sum(CountRunnableWorkItems)
        : workItem.Test.RunState == RunState.Runnable ? 1 : 0;

    // Fallback when running without parallel workers: counts every runnable test, ignoring the run filter.
    private static int CountRunnableTests(ITest test) => test.IsSuite
        ? test.Tests.Sum(CountRunnableTests)
        : test.RunState == RunState.Runnable ? 1 : 0;
}