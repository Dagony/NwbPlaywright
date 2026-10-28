using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using System.Text.RegularExpressions;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;

namespace NwbPlaywright.PageObjects
{
    /// #1                          #1
    /// Flow of BackOffice starts here
    /// #1                          #1
    public interface ILoanUpdateLoanDataApprovalObject
    {
        Task ClickApproveButtonOpenLoanData();

        Task ClickRejectButtonOpenLoanData();

        Task OpenGetApprovalOpenLoanDataLink();

        Task<string> GetSluitBriefBodyText(IPage page1);

        Task PrintDataRowsForCSVFile<T>(IEnumerable<T> datarow);

        Task ClickSubmitButtonCheckProductData();

        Task SetHasTheSwapBeenUpdated(bool setHasTheSwapBeenUpdated);

        Task<string> GetSECID();

        Task SetHasCorrectDataBeenUploadedToOpics(bool setHasCorrectDataBeenUploadedToOpics);

        Task OpenWaitInitiateDataLink();

        Task<string> DownloadCSVFileSECMNWB(string prefixName, string allNumber);

        Task<string> DownloadCSVFileSECM(string prefixName, string allNumber);

        Task<string> DownloadCSVFileBRED(string prefixName, string allNumber);

        Task<(string[] headerRow, IEnumerable<BREDfields> records)> GetCSVDataBRED(string name);

        Task<(string[] headerRow, IEnumerable<SECMNWBfields> records)> GetCSVDataSECMNWB(string pathName);

        Task<(string[] headerRow, IEnumerable<SECMfields> records)> GetCSVDataSECM(string pathName);

        Task<(string[] headerRow, IEnumerable<DEPLOfields> records)> GetCSVDataIDNL(string pathName);

        Task<int> CalculateBusinessDaysFromCurrentDateToStartDate(DateTime startDate);

        Task ClickYesContinueButton();

        Task Debug(IPage? pageToDebug = null);
    }

    public class LoanUpdateLoanDataApprovalObject : ILoanUpdateLoanDataApprovalObject
    {
        public LoanUpdateLoanDataApprovalObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _basePageObject = basePageObject;
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly IBasePageObject _basePageObject;
        private readonly TestSettings _testSettings;

        public async Task<string> GetSECID()
        {
            // var SecIDLocator = PageIpage1.Locator("xpath=(//*[@id=\"RULE_KEY\"]/div/div[1]/div/div[3])").Nth(4);
            var secIDLocator = pageIpage1.Locator("[class='content-item content-field item-3 remove-left-spacing flex'] div[class='field-item dataValueRead'] span");

            await secIDLocator.WaitForAsync();
            var secIDText = await secIDLocator.InnerTextAsync();
            return secIDText;
        }

        public Task ClickApproveButtonOpenLoanData() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();

        public Task ClickSubmitButtonCheckProductData() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        public async Task SetHasCorrectDataBeenUploadedToOpics(bool setHasCorrectDataBeenUploadedToOpics)
        {
            if (setHasCorrectDataBeenUploadedToOpics)
            {
                await pageIpage1.Locator("#IsInOpicstrue").CheckAsync();
            }
            else
            {
                await pageIpage1.Locator("#IsInOpicsfalse").CheckAsync();
            }
        }

        public async Task SetHasTheSwapBeenUpdated(bool setHasTheSwapBeenUpdated)
        {
            if (setHasTheSwapBeenUpdated)
            {
                await pageIpage1.Locator("#IsSwapUpdatedtrue").CheckAsync();
            }
            else
            {
                await pageIpage1.Locator("#IsSwapUpdatedfalse").CheckAsync();
            }
        }

        public Task ClickYesContinueButton() => pageIpage2.GetByRole(AriaRole.Button, new() { Name = "Yes (continue)" }).ClickAsync();

        public Task ClickRejectButtonOpenLoanData() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Reject", Exact = false }).ClickAsync();

        public Task OpenGetApprovalOpenLoanDataLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Check product data (Validate)" }).ClickAsync();

        public async Task PrintDataRowsForCSVFile<T>(IEnumerable<T> datarow)
        {
            var fieldsName = typeof(T).Name;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Printing header- and datarow below for the {fieldsName}:");
            Console.WriteLine("~~~~~~");
            foreach (var item in datarow)
            {
                if (item is not null)
                {
                    var properties = item.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var propertyName = property.Name;
                        var propertyValue = property.GetValue(item);
                        Console.WriteLine("{0}= {1}", propertyName, propertyValue);
                    }
                }
                else
                {
                    throw new NullReferenceException($"{item} appears to be null, call origin from PrintDataRowsForCsvFile()");
                }
            }

            Console.WriteLine("~~~~~~");
            Console.WriteLine("---------------------------------------------------");
            await Task.CompletedTask;
        }

        public async Task OpenWaitInitiateDataLink()
        {
            var openWaitInitiateDataLocator = pageIpage2.GetByRole(AriaRole.Link, new() { Name = "Wait (Create)" }).First;

            // await OpenCheckGuarantorDocumentLinkLocator.WaitForAsync();
            await openWaitInitiateDataLocator.ClickAsync();
            await Task.Delay(800);
        }

        public async Task<int> CalculateBusinessDaysFromCurrentDateToStartDate(DateTime startDate)
        {
            int totalBusinessDays = 0;
            DateTime currentDate = DateTime.Now;

            while (currentDate <= startDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    totalBusinessDays++;
                }

                currentDate = currentDate.AddDays(1);
            }

            await Task.CompletedTask;
            return totalBusinessDays;
        }

        // Pauses the test and opens the Playwright Inspector, so you can step through and inspect the page.
        // Only works with a visible browser: set "Headless": false in appsettings.json. Press "Resume" in the Inspector to continue.
        public async Task Debug(IPage? pageToDebug = null)
        {
            // Playwright runs headless when the setting is missing.
            if (_testSettings.Headless ?? true)
            {
                Console.WriteLine("Debug() is skipped: the browser runs headless. Set \"Headless\": false in appsettings.json to pause here.");
                return;
            }

            await (pageToDebug ?? page).PauseAsync();
        }

        public async Task<string> GetSluitBriefBodyText(IPage page1)
        {
            await Debug(page1);
            var locatorBodySluitbrief = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("body");
            var tableText = await locatorBodySluitbrief.InnerTextAsync();
            return tableText;
        }

        public async Task<string> DownloadCSVFileSECMNWB(string prefixName, string allNumber)
        {
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");

            var filepathSECMNWB = _testSettings.DownloadFilePath;
            var contentFrameLocator = pageIpage1.Locator("xpath=(//div[@class='rdlWrapperDiv'])[5]");
            var downloadSECMNWB = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await pageIpage1.GetByText(new Regex("SECM(NWB)")).ClickAsync();
            });
            var fileNameSECMNWB = prefixName + " " + downloadSECMNWB.SuggestedFilename;
            await downloadSECMNWB.SaveAsAsync(filepathSECMNWB + fileNameSECMNWB);
            return filepathSECMNWB + fileNameSECMNWB;
        }

        public async Task<string> DownloadCSVFileSECM(string prefixName, string allNumber)
        {
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");

            var filepathSECM = _testSettings.DownloadFilePath;
            var contentFrameLocator = pageIpage1.Locator("xpath=(//div[@class='rdlWrapperDiv'])[5]");
            var downloadSECM = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await pageIpage1.GetByText(new Regex("SECM_")).ClickAsync();
            });
            var fileNameSECM = prefixName + " " + downloadSECM.SuggestedFilename;
            await downloadSECM.SaveAsAsync(filepathSECM + fileNameSECM);
            return filepathSECM + fileNameSECM;
        }

        public async Task<string> DownloadCSVFileBRED(string prefixName, string allNumber)
        {
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");

            var filepathBRED = _testSettings.DownloadFilePath;
            var contentFrameLocator = pageIpage1.Locator("xpath=(//div[@class='rdlWrapperDiv'])[5]");

            var downloadBRED = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await pageIpage1.GetByText(new Regex("BRED")).ClickAsync();
            });
            var fileNameBRED = prefixName + " " + downloadBRED.SuggestedFilename;
            await downloadBRED.SaveAsAsync(filepathBRED + fileNameBRED);
            return filepathBRED + fileNameBRED;
        }

        public async Task<(string[] headerRow, IEnumerable<BREDfields> records)> GetCSVDataBRED(string pathName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                Delimiter = ";",
            };
            using var reader = new StreamReader(pathName);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            string[] headerRow = csv.Context.Reader.HeaderRecord;

            // If datafiles gets too large do not use ToList(), too much memory usage.
            var records = csv.GetRecords<BREDfields>().ToList();

            return (headerRow, records);
        }

        public async Task<(string[] headerRow, IEnumerable<SECMfields> records)> GetCSVDataSECM(string pathName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                Delimiter = ";",
            };
            using var reader = new StreamReader(pathName);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            string[] headerRow = csv.Context.Reader.HeaderRecord;

            // If datafiles gets too large do not use ToList(), too much memory usage.
            var records = csv.GetRecords<SECMfields>().ToList();

            return (headerRow, records);
        }

        public async Task<(string[] headerRow, IEnumerable<SECMNWBfields> records)> GetCSVDataSECMNWB(string pathName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                Delimiter = ";",
            };
            using var reader = new StreamReader(pathName);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            string[] headerRow = csv.Context.Reader.HeaderRecord;

            // If datafiles gets too large do not use ToList(), too much memory usage.
            var records = csv.GetRecords<SECMNWBfields>().ToList();

            return (headerRow, records);
        }

        public async Task<(string[] headerRow, IEnumerable<DEPLOfields> records)> GetCSVDataIDNL(string pathName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                Delimiter = ";",
            };
            using var reader = new StreamReader(pathName);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            string[] headerRow = csv.Context.Reader.HeaderRecord;

            // If datafiles gets too large do not use ToList(), too much memory usage.
            var records = csv.GetRecords<DEPLOfields>().ToList();
            return (headerRow, records);
        }
    }
}