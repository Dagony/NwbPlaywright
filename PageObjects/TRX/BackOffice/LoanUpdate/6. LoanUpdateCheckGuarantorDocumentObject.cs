using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #6                          #6
    /// Sith flow of BO process
    /// #6                          #6 <summary>
    /// #6                          #6
    /// </summary>
    public interface ILoanUpdateCheckGuarantorDocumentObject
    {
        Task ClickSubmitOpenCheckGuarantorDocumentPage();

        Task OpenCheckGuarantorDocumentLink();
    }

    public class LoanUpdateCheckGuarantorDocumentObject : ILoanUpdateCheckGuarantorDocumentObject
    {
        public LoanUpdateCheckGuarantorDocumentObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            page = playwrightDriver.Page.Result;
            pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task OpenCheckGuarantorDocumentLink()
        {
            var openCheckGuarantorDocumentLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Check guarantor documents (Agreement)" });

            // await OpenCheckGuarantorDocumentLinkLocator.WaitForAsync();
            await openCheckGuarantorDocumentLinkLocator.ClickAsync();
            await Task.Delay(800);
        }

        public async Task ClickSubmitOpenCheckGuarantorDocumentPage()
        {
            var submitOpenCheckGuarantorDocumentPageLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitOpenCheckGuarantorDocumentPageLocator.WaitForAsync();
            await submitOpenCheckGuarantorDocumentPageLocator.ClickAsync();
        }
    }
}