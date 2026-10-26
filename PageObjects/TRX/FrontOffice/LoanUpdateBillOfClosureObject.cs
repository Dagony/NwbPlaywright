using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects
{
    public interface ILoanUpdateBillOfClosureObject
    {
        // Task ApprovedRejectOfferBoC(bool isDealApproved);
        Task ClickALLCaseNumber(string locator);

        Task ClickSubmitBillOfClosureCases();

        Task<string> CopyDataFromUGfunction();

        Task CreateNewExcelOutputFileUGCopySheetFixedLoan(string data, string nameTC, string lALnumber);

        Task CreateNewExcelOutputFileUGCopySheetVariableLoan(string data, string nameTC, string lALnumber);

        Task<string> GetALLCaseNumber();

        Task OpenBillOfClosure();

        Task SetRejectionComment(string rejectionComment);

        Task WaitForEmailIcon();

        // Task OpenBillOfClosureDeposit();
        Task ClickCopyUGSheet();
    }

    public class LoanUpdateBillOfClosureObject : ILoanUpdateBillOfClosureObject
    {
        public LoanUpdateBillOfClosureObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;

        /// Bill Of Closure Screen methods and buttons //
        public async Task OpenBillOfClosure()
        {
            // var LinkBillOfClosureLocator = PageIpage1.GetByRole(AriaRole.Link, new() { Name = "Four eyes approval (Bill of Closure)" }).First;
            var linkBillOfClosureLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Four eyes approval (Approval)" }).First;
            await linkBillOfClosureLocator.DblClickAsync();

            await Task.Delay(2000);

            // await WaitForEmailIcon() so BoC is fully loaded! //
            // await PageIpage1.Locator("//body/div[@id='PEGA_HARNESS']/form[contains(@name,'main')]/div[@id='HARNESS_CONTENT']/div[contains(@class,'workarea-view-scroll-wrapper')]/section[contains(@aria-label,'Center Panel')]/div[@id='RULE_KEY']/span[contains(@class,'inspector-span')]/div[@id='RULE_KEY']/div[contains(@class,'two-col-main-sidebar')]/div[contains(@class,'content-item content-layout column-1')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-1 remove-all-spacing flex flex-row')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-sub_section item-1 remove-all-spacing flex flex-row no-margin-bottom')]/div[@id='RULE_KEY']/div[contains(@class,'layout layout-noheader layout-noheader-case_action_area')]/div[contains(@class,'')]/div[contains(@class,'content-simple_list')]/div[contains(@class,'content-item content-layout item-2 flex flex-row')]/div[contains(@class,'padding-lr-2x padding-tb-1x form-max-width')]/div[contains(@class,'content-item content-sub_section item-2 flex flex-row')]/div[@id='pyFlowActionHTML']/div[@id='RULE_KEY']/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-3 remove-left-spacing remove-right-spacing flex flex-row')]/div[contains(@class,'rdlWrapperDiv')]/div[contains(@class,'layout-content-default content-default')]/div[contains(@name,'BASE_REF')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-3 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_main_footer')]/div[contains(@class,'')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-2 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-6 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_second_layer_footer')]/div[1]").First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        }

        // public async Task OpenBillOfClosureDeposit()
        // {
        //    var LinkBillOfClosureLocator = PageIpage1.GetByRole(AriaRole.Link, new() { Name = "Four eyes approval (Approval)" }).First;
        //    await LinkBillOfClosureLocator.DblClickAsync();

        // await Task.Delay(2000);
        //    //  await WaitForEmailIcon() so BoC is fully loaded! //
        //    // await PageIpage1.Locator("//body/div[@id='PEGA_HARNESS']/form[contains(@name,'main')]/div[@id='HARNESS_CONTENT']/div[contains(@class,'workarea-view-scroll-wrapper')]/section[contains(@aria-label,'Center Panel')]/div[@id='RULE_KEY']/span[contains(@class,'inspector-span')]/div[@id='RULE_KEY']/div[contains(@class,'two-col-main-sidebar')]/div[contains(@class,'content-item content-layout column-1')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-1 remove-all-spacing flex flex-row')]/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-sub_section item-1 remove-all-spacing flex flex-row no-margin-bottom')]/div[@id='RULE_KEY']/div[contains(@class,'layout layout-noheader layout-noheader-case_action_area')]/div[contains(@class,'')]/div[contains(@class,'content-simple_list')]/div[contains(@class,'content-item content-layout item-2 flex flex-row')]/div[contains(@class,'padding-lr-2x padding-tb-1x form-max-width')]/div[contains(@class,'content-item content-sub_section item-2 flex flex-row')]/div[@id='pyFlowActionHTML']/div[@id='RULE_KEY']/div[contains(@class,'content-stacked')]/div[contains(@class,'content-item content-layout item-3 remove-left-spacing remove-right-spacing flex flex-row')]/div[contains(@class,'rdlWrapperDiv')]/div[contains(@class,'layout-content-default content-default')]/div[contains(@name,'BASE_REF')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-3 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_main_footer')]/div[contains(@class,'')]/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-2 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div/div[@id='RULE_KEY']/div[contains(@class,'content-items-maxwidth')]/div[contains(@class,'content-item content-sub_section item-6 remove-left-spacing remove-right-spacing flex flex-row margin-b-0')]/div[contains(@class,'layout layout-noheader layout-noheader-nwb_second_layer_footer')]/div[1]").First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        // }

        // public async Task ApprovedRejectOfferBoC(bool isDealApproved)
        // {
        //    if (isDealApproved == true)
        //    {
        //        await PageIpage1.GetByLabel("Approve").CheckAsync();
        //    }
        //    else if (isDealApproved == false)
        //    {
        //        await PageIpage1.GetByLabel("Reject").CheckAsync();
        //    }
        // }
        public async Task SetRejectionComment(string rejectionComment)
        {
            await pageIpage1.GetByLabel("Rejection Comment").FillAsync(rejectionComment);
        }

        public async Task WaitForEmailIcon()
        {
            await pageIpage1.Locator("//img[@class='attach_Icon']").First.WaitForAsync();
        }

        public async Task ClickSubmitBillOfClosureCases()
        {
            var linkApproveBillOfClosureLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await linkApproveBillOfClosureLocator.WaitForAsync();
            await linkApproveBillOfClosureLocator.DblClickAsync();
            await page.WaitForLoadStateAsync();
        }

        public async Task<string> GetALLCaseNumber()
        {
            // if (_testSettings.ApplicationUrl.Contains("dt1"))
            // {
            var aLLCaseNumberLocator = pageIpage1.Locator("span[class='case_description'] span[class='leftJustifyStyle']");
            await aLLCaseNumberLocator.WaitForAsync();
            var allCaseNumberText = await aLLCaseNumberLocator.InnerTextAsync();
            return allCaseNumberText;

            // var ALLCaseNumberLocator = PageIpage1.Locator("xpath=(//td[@data-importance='secondary'])[1]");
            // await ALLCaseNumberLocator.WaitForAsync();
            // var AllCaseNumberText = await ALLCaseNumberLocator.InnerTextAsync();
            // return AllCaseNumberText;
        }

        public async Task ClickALLCaseNumber(string locator)
        {
            var aLLCaseNumberLocator = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]").Locator($"xpath=(//span[contains(text(),'{locator}')])[1]");

            // if (setswap == true)
            // {
            //    ALLCaseNumberLocator = Page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]").Locator($"xpath=(//span[contains(text(),'{locator}')])[2]");
            // }
            // else
            // {
            //    ALLCaseNumberLocator = Page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]").Locator($"xpath=(//span[contains(text(),'{locator}')])[1]");
            // }
            await aLLCaseNumberLocator.ClickAsync();
        }

        public async Task ClickCopyUGSheet()
        {
            var uGsheetButtonLocator = pageIpage1.Locator("xpath=(//button[normalize-space()='Production sheet'])[1]");
            await uGsheetButtonLocator.WaitForAsync();
            await uGsheetButtonLocator.ClickAsync();
        }

        public async Task<string> CopyDataFromUGfunction()
        {
            var locaterUGdata = pageIpage1.Locator("[name='$PpyWorkPage$pProductionSheetInformation']");
            await locaterUGdata.WaitForAsync();
            var textData = await locaterUGdata.TextContentAsync() ?? string.Empty;
            return textData;
        }

        public async Task CreateNewExcelOutputFileUGCopySheetFixedLoan(string data, string nameTC, string lalNumber)
        {
            string[] sheet2Headers = new string[]
            {
                "Datum", "Door", "Pega", "Via", "Soort", "Geldnemer", "Beslismodel", "Contactpersoon", "Hoofdsom", "Start", "Eind", "Renteherziening",
                "Amortizatie", "Bijzonderheden", "Gemiddelde looptijd", "Rente", "Coupon datum", "Conventie", "Zekerheid", "Score", "Fee", "Basis", "Spread 3mE", "Spread 6mE", "Reguliere funding FTP", "Korting TLTRO",
                "Funding TLTRO", "Marge tov reg funding", "Marge tov TLTRO funding", "3e korting", "Opmerking",
            };
            string[] dataList = data.Split("\t");
            string filePath = $"C:/Users/Maarten/Downloads/TC {nameTC} UG Sheet Copy function {lalNumber}.xlsx";

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "SampleData",
            };
            sheets.Append(sheet);
            Worksheet worksheet = new Worksheet();
            SheetData sheetData = new SheetData();
            Row r = new Row();
            for (int i = 0; i < sheet2Headers.Length; i++)
            {
                r.InsertAt<Cell>(
                    new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new InlineString() { Text = new Text(sheet2Headers[i]) },
                    },
                    i);
            }

            sheetData.InsertAt(r, 0);
            r = new Row();
            for (int i = 0; i < dataList.Length; i++)
            {
                r.InsertAt<Cell>(
                    new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new InlineString() { Text = new Text(dataList[i]) },
                    },
                    i);
            }

            sheetData.InsertAt(r, 1);

            worksheet.Append(sheetData);
            worksheetPart.Worksheet = worksheet;
            worksheetPart.Worksheet.Save();
            spreadsheetDocument.Dispose();
            await Task.CompletedTask;
        }

        public async Task CreateNewExcelOutputFileUGCopySheetVariableLoan(string data, string nameTC, string lalNumber)
        {
            string[] sheet2Headers = new string[]
            {
                "Datum", "Door", "Pega", "Via", "Soort", "Geldnemer", "Beslismodel", "Contactpersoon", "Hoofdsom", "Start", "Eind", "Renteherziening",
                "Vast of variabele hoofdsom", "Gemiddelde looptijd", "Euribor", "Rente", "Coupondatum", "Conventie", "Zekerheid", "Credit assessment", "Fee", "Basisswap 3mE vs 6mE", "Spread 3mE", "Spread 6mE", "Reguliere funding FTP", "Korting TLTRO",
                "Funding TLTRO", "Marge tov reg funding", "Marge tov TLTRO funding", "Opmerking",
            };
            string[] dataList = data.Split("\t");
            string filePath = $"C:/Users/Maarten/Downloads/TC {nameTC} UG Sheet Copy function {lalNumber}.xlsx";

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "SampleData",
            };
            sheets.Append(sheet);
            Worksheet worksheet = new Worksheet();
            SheetData sheetData = new SheetData();
            Row r = new Row();
            for (int i = 0; i < sheet2Headers.Length; i++)
            {
                r.InsertAt<Cell>(
                    new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new InlineString() { Text = new Text(sheet2Headers[i]) },
                    },
                    i);
            }

            sheetData.InsertAt(r, 0);
            r = new Row();
            for (int i = 0; i < dataList.Length; i++)
            {
                r.InsertAt<Cell>(
                    new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new InlineString() { Text = new Text(dataList[i]) },
                    },
                    i);
            }

            sheetData.InsertAt(r, 1);

            worksheet.Append(sheetData);
            worksheetPart.Worksheet = worksheet;
            worksheetPart.Worksheet.Save();
            spreadsheetDocument.Dispose();
            await Task.CompletedTask;
        }
    }
}