using System.Globalization;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.Config
{
    public class TestSettings
    {
        public float? Timeout = PlaywrightDriverInitializer.DEFAULTTIMEOUT;

        public string? ApplicationUrl { get; set; }

        public string? ApplicationEnvironment { get; set; }

        public bool? Headless { get; set; }

        public float? SlowMo { get; set; }

        public string[]? Args { get; set; }

        public bool? DeleteFilesAfterTestRun { get; set; }

        public int ViewSizePortLength { get; set; }

        public int ViewSizePortWidth { get; set; }

        public bool? RunModePRODUCTIONLIKE { get; set; }

        public string DownloadFilePath { get; set; }

        public bool TracerEnabled { get; set; }

        public DriverType DriverType { get; set; }

        public CultureInfo CultureInfo { get; } = new CultureInfo("nl-NL");

        public string? AcceptTestAccountDesk1 { get; set; }

        public string? AcceptTestAccountDesk2 { get; set; }

        public string? AcceptTestAccountDesk1PrevRelease { get; set; }

        public string? AcceptTestAccountDesk2PrevRelease { get; set; }

        public string? TestAccountDesk1PasswordPrevRelease { get; set; }

        public string? TestAccountDesk2PasswordPrevRelease { get; set; }

        public string? DevAccountDesk1 { get; set; }

        public string? DevAccountDesk2 { get; set; }

        public string? AcceptTestBackOffice1 { get; set; }

        public string? AcceptTestBackOffice2 { get; set; }

        public string? DevBackOffice1 { get; set; }

        public string? DevBackOffice2 { get; set; }

        public string? TestAccountDesk1Password { get; set; }

        public string? TestAccountDesk2Password { get; set; }

        public string? AcceptAccountDesk1Password { get; set; }

        public string? AcceptTestBackOffice1PrevRelease { get; set; }

        public string? AcceptTestBackOffice2PrevRelease { get; set; }

        public string? TestBackOffice1PasswordPrevRelease { get; set; }

        public string? TestBackOffice2PasswordPrevRelease { get; set; }

        public string? AcceptAccountDesk2Password { get; set; }

        public string? DevAccountDesk1Password { get; set; }

        public string? DevAccountDesk2Password { get; set; }

        public string? TestBackOffice1Password { get; set; }

        public string? TestBackOffice2Password { get; set; }

        public string? AcceptBackOffice1Password { get; set; }

        public string? AcceptBackOffice2Password { get; set; }

        public string? DevBackOffice1Password { get; set; }

        public string? DevBackOffice2Password { get; set; }

        public string? AcceptTestCreditManagement1 { get; set; }

        public string? AcceptTestCreditManagement2 { get; set; }

        public string? DevCreditManagement1 { get; set; }

        public string? DevCreditManagement2 { get; set; }

        public string? DevCreditManagement1Password { get; set; }

        public string? DevCreditManagement2Password { get; set; }

        public string? TestCreditManagement1Password { get; set; }

        public string? TestCreditManagement2Password { get; set; }

        public string? AcceptCreditManagement1Password { get; set; }

        public string? AcceptCreditManagement2Password { get; set; }

        public string? DevBOFundingDerivates1 { get; set; }

        public string? DevBOFundingDerivates2 { get; set; }

        public string? AcceptTestBOFundingDerivates1 { get; set; }

        public string? AcceptTestBOFundingDerivates2 { get; set; }

        public string? DevBOFundingDerivates1Password { get; set; }

        public string? DevBOFundingDerivates2Password { get; set; }

        public string? TestBOFundingDerivates1Password { get; set; }

        public string? TestBOFundingDerivates2Password { get; set; }

        public string? AcceptBOFundingDerivates1Password { get; set; }

        public string? AcceptBOFundingDerivates2Password { get; set; }

        public string? DevTreasury1 { get; set; }

        public string? DevTreasury2 { get; set; }

        public string? AcceptTestTreasury1 { get; set; }

        public string? AcceptTestTreasury2 { get; set; }

        public string? DevTreasury1Password { get; set; }

        public string? DevTreasury2Password { get; set; }

        public string? TestTreasury1Password { get; set; }

        public string? TestTreasury2Password { get; set; }

        public string? AcceptTreasury1Password { get; set; }

        public string? AcceptTreasury2Password { get; set; }

        public string? DevTreasuryManager1 { get; set; }

        public string? DevTreasuryManager2 { get; set; }

        public string? DevTreasuryManager1Password { get; set; }

        public string? DevTreasuryManager2Password { get; set; }

        public string? AcceptTestTreasuryManager1 { get; set; }

        public string? AcceptTestTreasuryManager2 { get; set; }

        public string? TestTreasuryManager1Password { get; set; }

        public string? TestTreasuryManager2Password { get; set; }

        public string? AcceptTreasuryManager1Password { get; set; }

        public string? AcceptTreasuryManager2Password { get; set; }

        public string? DevSpecializedFinanceTRX1 { get; set; }

        public string? DevSpecializedFinanceTRX1Password { get; set; }

        public string? AcceptTestSpecializedFinanceTRX1 { get; set; }

        public string? TestSpecializedFinanceTRX1Password { get; set; }
    }

    public enum DriverType
    {
        Chrome,
        Firefox,
        Edge,
        Chromium,
        Opera,
    }
}