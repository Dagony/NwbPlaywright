using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #11                         #11
    /// Eleventh flow of BO process
    /// #11                          #11
    public interface ILoanUpdateLegalReviewObject
    {
        Task ClickSubmitButtonPendingLegalReviewPage();
    }

    public class LoanUpdateLegalReviewObject : ILoanUpdateLegalReviewObject
    {
        public LoanUpdateLegalReviewObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;

            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task ClickSubmitButtonPendingLegalReviewPage()
        {
            var submitButtonPendingLegalReviewPageLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonPendingLegalReviewPageLocator.WaitForAsync();
            await submitButtonPendingLegalReviewPageLocator.ClickAsync();
        }
    }
}