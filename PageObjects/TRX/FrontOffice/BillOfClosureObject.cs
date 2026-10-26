using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Features.Variable;
using NwbPlaywright.Fields;
using static NwbPlaywright.Fields.EnumsForTRX;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;

namespace NwbPlaywright.PageObjects
{
    public interface IBillOfClosureObject
    {
        Task ClickSubmitBillOfClosureCases();

        Task ClickCopyUGSheet();

        Task ClickALLCaseNumber(string locator);

        Task<string> CopyDataFromUGfunction();

        Task ApprovedRejectOfferBoC(bool isDealApproved);

        Task<string> GetALLCaseNumber();

        Task OpenBillOfClosure();

        Task OpenCollectOfferConfirmation();

        Task<string[]> GetUGSheetDataToArray(LoanRequest option, bool? printUGDataToConsole);

        Task<(string swapCaseNumber, string allCaseNumber)> GetSWAPandALLCaseNumber();

        Task PrintUGSheet<T>(IEnumerable<T> datarow);

        Task<(string[] headerRow, IEnumerable<FixedUGSheetFields> records)> GetUGSheetExcelData(string pathName);

        Task PrintDataRowsForUGSHEET<T>(IEnumerable<T> datarow);

        Task<string> CreateNewCSVOutputFileUGCopy<T>(string data, string nameTC, string lALnumber)
            where T : class, new();

        Task<(string[] headerRow, IEnumerable<T> records)> GetUGSheetDataFromCSVFile<T>(string pathName)
            where T : class;
    }

    public class BillOfClosureObject : IBillOfClosureObject
    {
        public BillOfClosureObject(
            IPlaywrightDriver playwrightDriver,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ScenarioContext _scenarioContext;

        /// Bill Of Closure Screen methods and buttons //
        private const int WIN1252CP = 1252; // Windows ANSI codepage 1252

        public async Task OpenBillOfClosure()
        {
            var linkBillOfClosureLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Approve bill of closure (Offer)" }).First;
            await linkBillOfClosureLocator.DblClickAsync();
            await pageIpage1.Locator("//body/div[@id='PEGA_HARNESS']/form[contains(@name,'main')]/div[@id='HARNESS_CONTENT']/div[contains(@class,'workarea-view-scroll-wrapper')]/section[contains(@aria-label,'Center Panel')]/div[@id='RULE_KEY']/span[contains(@class,'inspector-span')]/div[@id='RULE_KEY']/div[contains(@class,'two-col-main-sidebar')]/div[contains(@class,'content-item content-layout column-1')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-1 remove-all-spacing flex flex-row')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-sub_section item-1 remove-all-spacing flex flex-row no-margin-bottom')]/div[@id='RULE_KEY']/div[contains(@class,'layout layout-noheader layout-noheader-case_action_area')]/div[contains(@class,'')]/div[contains(@class,'content-simple_list')]/div[contains(@class,'content-item content-layout item-2 flex flex-row')]/div[contains(@class,'padding-lr-2x padding-tb-1x form-max-width')]/div[contains(@class,'content-item content-sub_section item-2 flex flex-row')]/div[@id='pyFlowActionHTML']/div[@id='RULE_KEY']/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-3 remove-left-spacing remove-right-spacing flex flex-row')]/div[contains(@class,'rdlWrapperDiv')]/div[contains(@class,'layout-content-default content-default')]/div[contains(@name,'BASE_REF')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-3 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_main_footer')]/div[contains(@class,'')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-2 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-6 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_second_layer_footer')]/div[1]").First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        }

        public async Task OpenCollectOfferConfirmation()
        {
            var linkOpenCollectOfferConfirmationLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Collect offer confirmation (Offer)" }).First;
            await linkOpenCollectOfferConfirmationLocator.DblClickAsync();
            await page.WaitForLoadStateAsync();
        }

        public async Task<string[]> GetUGSheetDataToArray(LoanRequest option, bool? printUGDataToConsole)
        {
            bool printData = printUGDataToConsole ?? false;

            if (option == LoanRequest.Deposit)
            {
                await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Production sheet" }).WaitForAsync();
                await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Production sheet" }).ClickAsync(new LocatorClickOptions { Delay = 200 });
            }
            else
            {
                await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "UG" }).WaitForAsync();
                await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "UG" }).ClickAsync(new LocatorClickOptions { Delay = 200 });
            }

            await Task.Delay(1500);
            await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = string.Empty }).First.WaitForAsync();
            var data = await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = string.Empty }).First.TextContentAsync();
            string[] values = data.Split("\t").Select(sValue => sValue.Trim()).ToArray();

            char column = 'A';

            foreach (var value in values)
            {
                Console.WriteLine($"Data for column {column}= {value}");
                column++;
            }

            await pageIpage1.GetByLabel("Close modal").ClickAsync();

            return values;
        }

        public async Task PrintUGSheet<T>(IEnumerable<T> datarow)
        {
            var fieldsName = typeof(T).Name;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Printing header- and datarow below for the {fieldsName}:");
            Console.WriteLine("~~~~~~");

            var columnEnumerator = _basePageObject.Generate().GetEnumerator();
            foreach (var item in datarow)
            {
                if (item is not null)
                {
                    if (!columnEnumerator.MoveNext())
                    {
                        // Handle the case where there are more values than columns generated
                        throw new InvalidOperationException("Not enough columns generated for the values.");
                    }

                    var charColumn = columnEnumerator.Current;

                    var properties = item.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var propertyName = property.Name;
                        var propertyValue = property.GetValue(item);
                        Console.WriteLine("{0}|{1}= {2}", charColumn, propertyName, propertyValue);
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

        public async Task<(string[] headerRow, IEnumerable<FixedUGSheetFields> records)> GetUGSheetExcelData(string pathName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                Delimiter = ",",
            };
            using var reader = new StreamReader(pathName);
            using var csv = new CsvReader(reader, config);
            csv.Read();
            csv.ReadHeader();
            string[] headerRow = csv.Context.Reader.HeaderRecord;

            // If datafiles gets too large do not use ToList(), too much memory usage.
            var records = csv.GetRecords<FixedUGSheetFields>().ToList();

            return (headerRow, records);
        }

        public async Task ApprovedRejectOfferBoC(bool isDealApproved)
        {
            if (isDealApproved)
            {
                await pageIpage1.GetByLabel("Approve").CheckAsync();
            }
        }

        public async Task ClickSubmitBillOfClosureCases()
        {
            var linkApproveBillOfClosureLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await linkApproveBillOfClosureLocator.WaitForAsync();
            await linkApproveBillOfClosureLocator.FocusAsync();
            await linkApproveBillOfClosureLocator.ClickAsync();

            // await Page.WaitForLoadStateAsync();
        }

        public async Task<string> GetALLCaseNumber()
        {
            var aLLCaseNumberLocator = pageIpage1.Locator("span[class='case_description'] span[class='leftJustifyStyle']");
            await aLLCaseNumberLocator.WaitForAsync();
            var allCaseNumberText = await aLLCaseNumberLocator.InnerTextAsync();
            return allCaseNumberText;
        }

        public async Task<(string swapCaseNumber, string allCaseNumber)> GetSWAPandALLCaseNumber()
        {
            var sWAPCaseNumberLocator = pageIpage1.Locator("span[class='case_description'] span[class='leftJustifyStyle']").Nth(0);
            var aLLCaseNumberLocator = pageIpage1.Locator("span[class='case_description'] span[class='leftJustifyStyle']").Nth(1);
            await sWAPCaseNumberLocator.WaitForAsync();
            await aLLCaseNumberLocator.WaitForAsync();
            var sWAPCaseNumberText = await sWAPCaseNumberLocator.InnerTextAsync();
            var aLLCaseNumberText = await aLLCaseNumberLocator.InnerTextAsync();
            return (sWAPCaseNumberText, aLLCaseNumberText);
        }

        public async Task ClickALLCaseNumber(string locator)
        {
            var aLLCaseNumberLocator = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]").Locator($"xpath=(//span[contains(text(),'{locator}')])[1]");
            await aLLCaseNumberLocator.ClickAsync();
        }

        public async Task ClickCopyUGSheet()
        {
            var uGsheetButtonLocator = pageIpage1.Locator("xpath= (//button[normalize-space()='UG'])[1]");
            await uGsheetButtonLocator.WaitForAsync();
            await uGsheetButtonLocator.ClickAsync();
        }

        public async Task<string> CopyDataFromUGfunction()
        {
            var locatorUGdata = pageIpage1.Locator("[name='$PpyWorkPage$pUGString']");
            await locatorUGdata.WaitForAsync();
            var textData = await locatorUGdata.TextContentAsync() ?? string.Empty;
            return textData;
        }

        public async Task PrintDataRowsForUGSHEET<T>(IEnumerable<T> datarow)
        {
            var fieldsName = typeof(T).Name;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Printing header- and datarow below for the {fieldsName}:");
            Console.WriteLine("~~~~~~");

            var columnEnumerator = _basePageObject.Generate().GetEnumerator();

            foreach (var item in datarow)
            {
                if (item is not null)
                {
                    var properties = item.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        columnEnumerator.MoveNext();
                        var columnChar = columnEnumerator.Current;
                        var propertyName = property.Name;
                        var propertyValue = property.GetValue(item);
                        Console.WriteLine($"{columnChar}|{propertyName}= {propertyValue}");
                    }
                }
                else
                {
                    throw new NullReferenceException($"{item} appears to be null, call origin from PrintDataRowsForUGSHEET()");
                }
            }

            Console.WriteLine("~~~~~~");
            Console.WriteLine("---------------------------------------------------");
            await Task.CompletedTask;
        }

        public async Task<(string[] headerRow, IEnumerable<T> records)> GetUGSheetDataFromCSVFile<T>(string pathName)
            where T : class
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
            var records = csv.GetRecords<T>().ToList();

            return (headerRow, records);
        }

        public async Task<string> CreateNewCSVOutputFileUGCopy<T>(string data, string prefixName, string lalNumber)
            where T : class, new()
        {
            var currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var environment = _testSettings.ApplicationEnvironment;
            string[] dataList = data.Split("\t");
            var loanType = (string)_scenarioContext["savedLoanType"];
            string fileName = @$"{currentDateTime}_{environment}_{loanType}_TC_{prefixName}_{lalNumber}_UG.csv";
            string filePath = System.IO.Path.Combine(_testSettings.DownloadFilePath, fileName);

            _scenarioContext["savedUGSheetFilepath"] = filePath;

            T obj = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < dataList.Length && i < properties.Length; i++)
            {
                properties[i].SetValue(obj, dataList[i]);
            }

            var records = new List<T> { obj };
            using (var streamWriter = new StreamWriter(filePath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";",
                }))
                {
                    csvWriter.WriteHeader<T>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(records);
                }
            }

            return filePath;
        }
    }
}