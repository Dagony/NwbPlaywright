namespace NwbPlaywright.Hooks
{
    [Binding]
    public class TestProgress
    {
        // Scenarios run in parallel, so the counters are updated and read under a lock.
        private static readonly object CounterLock = new();

        // NUnit escapes ANSI color codes in TestContext output, so the colored lines go straight to the process stdout.
        private static readonly StreamWriter ConsoleOut = new(Console.OpenStandardOutput()) { AutoFlush = true };

        internal const string Reset = "\u001b[0m";

        internal const string Cyan = "\u001b[1;36m";

        private const string Green = "\u001b[1;32m";

        private const string Red = "\u001b[1;31m";

        private static int _passed;

        private static int _failed;

        public static int Total { get; set; }

        [AfterScenario(Order = 10)]
        public void ReportProgress(ScenarioContext scenarioContext)
        {
            var passed = scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK;

            lock (CounterLock)
            {
                if (passed)
                {
                    _passed++;
                }
                else
                {
                    _failed++;
                }

                var failedColor = _failed > 0 ? Red : Green;
                var result = passed ? $"{Green}PASSED{Reset}" : $"{Red}FAILED{Reset}";

                WriteLine(
                    $"{Cyan}Progress: {_passed + _failed}/{Total} tests run{Reset} | {Green}Passed: {_passed}{Reset} | " +
                    $"{failedColor}Failed: {_failed}{Reset} | {result}: {scenarioContext.ScenarioInfo.Title}");
            }
        }

        public static void WriteLine(string message)
        {
            lock (CounterLock)
            {
                ConsoleOut.WriteLine(message);
            }
        }
    }
}