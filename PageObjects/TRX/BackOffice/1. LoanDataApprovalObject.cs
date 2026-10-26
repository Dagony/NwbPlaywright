using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;

namespace TA.Playwright.PageObjects
{
    public interface ILoanDataApprovalObject
    {
        Task ClickApproveButtonOpenLoanData();

        Task ClickRejectButtonOpenLoanData();

        Task OpenGetApprovalOpenLoanDataLink();

        Task<string> GetSluitBriefBodyText(IPage page1);

        Task PrintDataRowsForCSVFile<T>(IEnumerable<T> datarow);

        Task<string> GetSECIDNormalLoan();

        Task<string> GetSECIDLoanUpdate();

        Task OpenWaitInitiateDataLink();

        Task SetHasCorrectDataBeenUploadedToOpics(bool setHasCorrectDataBeenUploadedToOpics);

        Task<string> DownloadCSVFileSECMNWB(string prefixName, string allNumber);

        Task<string> DownloadCSVFileSECM(string prefixName, string allNumber);

        Task<string> DownloadCSVFileBRED(string prefixName, string allNumber);

        Task OpenGetApprovalOpenLoanDataLinkOld();

        Task<(string[] headerRow, IEnumerable<BREDfields> records)> GetCSVDataBRED(string name);

        Task<(string[] headerRow, IEnumerable<SECMNWBfields> records)> GetCSVDataSECMNWB(string pathName);

        Task<(string[] headerRow, IEnumerable<SECMfields> records)> GetCSVDataSECM(string pathName);

        Task<(string[] headerRow, IEnumerable<DEPLOfields> records)> GetCSVDataIDNL(string pathName);

        Task<int> CalculateBusinessDaysFromCurrentDateToStartDate(DateTime startDate);

        Task ClickYesContinueButton();
    }

    public class LoanDataApprovalObject : ILoanDataApprovalObject
    {
        public LoanDataApprovalObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _basePageObject = basePageObject;
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly IBasePageObject _basePageObject;
        private readonly TestSettings _testSettings;

        public async Task<string> GetSECIDNormalLoan()
        {
            var secIDLocator = pageIpage1.Locator(".content-item.content-layout.item-1 .content-item.content-field.item-4.remove-left-spacing.flex .dataValueRead span");
            await secIDLocator.WaitForAsync();
            var secIDText = await secIDLocator.InnerTextAsync();
            return secIDText;
        }

        public async Task<string> GetSECIDLoanUpdate()
        {
            var secIDLocator = pageIpage1.Locator(".content-item.content-layout.item-1 .content-item.content-field.item-4.remove-left-spacing.flex .dataValueRead span");
            await secIDLocator.WaitForAsync();
            var secIDText = await secIDLocator.InnerTextAsync();
            return secIDText;
        }

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

        public Task ClickApproveButtonOpenLoanData() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();

        public Task ClickYesContinueButton() => pageIpage2.GetByRole(AriaRole.Button, new() { Name = "Yes (continue)" }).ClickAsync();

        public Task ClickRejectButtonOpenLoanData() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Reject", Exact = false }).ClickAsync();

        public Task OpenGetApprovalOpenLoanDataLinkOld() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Get Approval" }).ClickAsync();

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

        public async Task<string> GetSluitBriefBodyText(IPage page1)
        {
            var locatorBodySluitbrief = page1.FrameLocator("iframe[name=\"ShowIFrame\"]").Locator("body");
            var bodyText = await locatorBodySluitbrief.InnerTextAsync();
            return bodyText;
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