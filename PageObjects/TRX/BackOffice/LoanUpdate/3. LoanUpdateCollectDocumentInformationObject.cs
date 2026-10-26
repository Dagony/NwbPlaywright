using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #3                                                    #3
    /// Third flow of BO renteherzieng flow for fixed/wp only
    /// #3                                                    #3
    public interface ILoanUpdateCollectDocumentInformationObject
    {
        Task SetRepaymentAmount(string repaymentAmount);

        Task ClickSubmitLoanUpdateCollectDocumentInformation();
    }

    public class LoanUpdateCollectDocumentInformationObject : ILoanUpdateCollectDocumentInformationObject
    {
        public LoanUpdateCollectDocumentInformationObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;

        public async Task SetRepaymentAmount(string repaymentAmount)
        {
            await pageIpage1.GetByPlaceholder("Enter an amount").WaitForAsync();
            await pageIpage1.GetByPlaceholder("Enter an amount").FillAsync(repaymentAmount);
        }

        public async Task ClickSubmitLoanUpdateCollectDocumentInformation()
        {
            var submitLoanUpdateCollectDocumentInformationLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitLoanUpdateCollectDocumentInformationLocator.WaitForAsync();
            await submitLoanUpdateCollectDocumentInformationLocator.ClickAsync();
        }

        /// /////////////////////////////////////////       WORD docx TEMPLATE METHODS  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    }
}