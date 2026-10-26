using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;

namespace NwbPlaywright.PageObjects.TRX.Treasury
{
    public interface IUpdatePricingGridObject
    {
        Task<string> GetFTP(int index);

        Task<string> GetFundingCurve(int index);

        Task<string> GetMinSpreadUG(int index);

        Task SetFTP(string value, int index);

        Task SetFundingCurve(string value, int index);

        Task<string> GetCaseID();

        Task<string> GetCommentMessage();

        Task<List<PricingGridFields>> GetPricingGridData();

        Task SetComments(string comment);

        Task SetRejectionComment(string rejectionComment);

        Task ClickSubmitPricingGrid();

        Task ClickTreasuryWorkqueue();

        Task SearchPricingGridCaseNumberQueueScreen(string casePricingGrid);

        Task ClickSubmitReviewPricingGridPage();

        Task SetApprovalDecision(bool? setApprovalDecision);
    }

    public class UpdatePricingGridObject : IUpdatePricingGridObject
    {
        public UpdatePricingGridObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IBasePageObject basePageObject, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            this.pageIpage0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _counterpartyFieldLocator = pageIpage1.GetByPlaceholder("Enter counterparty name. Searches from 3 characters");
            _basePageObject = basePageObject;
            _includeInMailBoxLocator = pageIpage1.GetByLabel("Include in mail");
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly ILocator _counterpartyFieldLocator;
        private readonly IBasePageObject _basePageObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly ScenarioContext _scenarioContext;

        public async Task<string> GetCaseID()
        {
            var locatorCaseID = pageIpage1.Locator("//span[@class='workarea_header_id']");
            await locatorCaseID.WaitForAsync();
            var caseNumberFully = await locatorCaseID.TextContentAsync();
            if (caseNumberFully is not null)
            {
                var caseNumber = caseNumberFully.Remove(0, 1).Remove(caseNumberFully.Length - 2);
                return caseNumber;
            }
            else
            {
                throw new NullReferenceException("caseNumberFully appears to be null, call origin from GetCaseID()");
            }
        }

        public async Task<List<PricingGridFields>> GetPricingGridData()
        {
            var tableData = new List<PricingGridFields>();

            // Loop over 50 rows
            for (int i = 1; i < 51; i++)
            {
                var fudingCurveCellSelector = pageIpage1.Locator($"(//tr[contains(@name,'BASE_REF')])[{i}]/td[3]");
                var ftpCurveCellSelector = pageIpage1.Locator($"(//tr[contains(@name,'BASE_REF')])[{i}]/td[4]");
                var minSpreadUgCellSelector = pageIpage1.Locator($"(//tr[contains(@name,'BASE_REF')])[{i}]/td[5]");

                var maturityColumn = i.ToString();
                var fundingCurveColumn = await fudingCurveCellSelector.TextContentAsync();
                var fTPColumn = await ftpCurveCellSelector.TextContentAsync();
                var minSpreadUgCellColumn = await minSpreadUgCellSelector.TextContentAsync();

                var tableRow = new PricingGridFields(maturityColumn, fundingCurveColumn, fTPColumn, minSpreadUgCellColumn);
                tableData.Add(tableRow);
            }

            return tableData;
        }

        public async Task<string> GetFundingCurve(int index)
        {
            var fundingCurveLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pNewPricingRecords\\$l{index}\\$pFundingCurve\"]");

            await fundingCurveLocator.WaitForAsync();
            var value = await fundingCurveLocator.GetAttributeAsync("data-value");
            if (value is not null)
            {
                return value;
            }
            else
            {
                throw new Exception("Value of GetFundingCurve() is null!");
            }
        }

        public async Task SetFundingCurve(string value, int index)
        {
            var fundingCurveLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pNewPricingRecords\\$l{index}\\$pFundingCurve\"]");

            await fundingCurveLocator.WaitForAsync();
            await fundingCurveLocator.ClearAsync();
            await fundingCurveLocator.PressSequentiallyAsync(value);

            string currentValue = await fundingCurveLocator.InputValueAsync();
            if (currentValue != value)
            {
                // Retry setting the value if it was reverted
                await fundingCurveLocator.ClearAsync();
                await fundingCurveLocator.PressSequentiallyAsync(value);
            }
        }

        public async Task<string> GetCommentMessage()
        {
            var commentMessageLocator = pageIpage1.Locator("div[class='content-item content-field item-1 flex flex-row dataValueRead'] span");
            await commentMessageLocator.WaitForAsync();
            var message = await commentMessageLocator.TextContentAsync();
            return message == null ? string.Empty : message;
        }

        public async Task<string> GetFTP(int index)
        {
            var ftpLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pNewPricingRecords\\$l{index}\\$pFTP\"]");
            await ftpLocator.WaitForAsync();
            var value = await ftpLocator.GetAttributeAsync("data-value");
            if (value is not null)
            {
                return value;
            }
            else
            {
                throw new Exception("Value of GetFTP() seemed to be null or empty");
            }
        }

        public async Task SetFTP(string value, int index)
        {
            var ftpLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pNewPricingRecords\\$l{index}\\$pFTP\"]");

            await ftpLocator.WaitForAsync();
            await ftpLocator.ClearAsync();
            await ftpLocator.PressSequentiallyAsync(value);

            string currentValue = await ftpLocator.InputValueAsync();
            if (currentValue != value)
            {
                // Retry setting the value if it was reverted
                await ftpLocator.ClearAsync();
                await ftpLocator.PressSequentiallyAsync(value);
            }
        }

        public async Task<string> GetMinSpreadUG(int index)
        {
            var minSpreadUGLocator = pageIpage1.Locator($"tr[id = '$PpyWorkPage$pNewPricingRecords$l{index}'] td[class='dataValueRead gridCell ']");

            await minSpreadUGLocator.WaitForAsync();
            var valueUntrimmed = await minSpreadUGLocator.TextContentAsync();
            var value = valueUntrimmed.Substring(1);

            if (value is not null)
            {
                return value;
            }
            else
            {
                throw new Exception("Value of GetMinSpreadUG() seemed to be null or empty");
            }
        }

        public async Task SetComments(string comment)
        {
            var commentsLocator = pageIpage1.Locator($"xpath=(//textarea[@id='94c75a38'])[1]");
            await commentsLocator.WaitForAsync();
            await commentsLocator.FillAsync(comment);
        }

        public async Task SetRejectionComment(string rejectionComment)
        {
            var rejectionCommentLocator = pageIpage1.GetByLabel("Rejection comment");
            await rejectionCommentLocator.WaitForAsync();
            await rejectionCommentLocator.FillAsync(rejectionComment);
        }

        public async Task ClickSubmitPricingGrid()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await Task.Delay(300);
            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync(new() { Force = false, Delay = 100, });
        }

        public async Task ClickSubmitReviewPricingGridPage()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await Task.Delay(300);
            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync(new() { Force = false, Delay = 100, });
        }

        public async Task ClickTreasuryWorkqueue()
        {
            await pageIpage0.GetByText("Treasury", new() { Exact = true }).WaitForAsync();
            await pageIpage0.GetByText("Treasury", new() { Exact = true }).ClickAsync();
        }

        public async Task SearchPricingGridCaseNumberQueueScreen(string casePricingGrid)
        {
            var caseFilterLocator = pageIpage0.Locator("xpath=(//a[contains(@title,'Click to filter')])[4]");
            await caseFilterLocator.WaitForAsync();
            await caseFilterLocator.ClickAsync();

            var caseFilterSearchFieldLocator = pageIpage0.GetByLabel("Search Text");
            var caseFilterApplyLocator = pageIpage0.GetByRole(AriaRole.Button, new() { Name = "Apply" });
            var clickCaseFromSearchNEWNEW = pageIpage0.GetByRole(AriaRole.Link, new() { Name = casePricingGrid, Exact = false }).First;

            await caseFilterSearchFieldLocator.WaitForAsync();
            await caseFilterSearchFieldLocator.FillAsync(casePricingGrid);
            await caseFilterApplyLocator.WaitForAsync();
            await caseFilterApplyLocator.ClickAsync();
            await page.WaitForLoadStateAsync();
            await clickCaseFromSearchNEWNEW.ClickAsync();
        }

        public async Task SetApprovalDecision(bool? setApprovalDecision)
        {
            bool approvalDecision = setApprovalDecision ?? true;

            if (approvalDecision == true)
            {
                await pageIpage1.GetByText("Approve", new() { Exact = true }).WaitForAsync();
                await pageIpage1.GetByText("Approve", new() { Exact = true }).ClickAsync();
            }
            else if (approvalDecision == false)
            {
                await pageIpage1.GetByText("Reject", new() { Exact = true }).WaitForAsync();
                await pageIpage1.GetByText("Reject", new() { Exact = true }).ClickAsync();
            }
        }
    }
}