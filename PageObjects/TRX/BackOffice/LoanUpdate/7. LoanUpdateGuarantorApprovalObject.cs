using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #7                          #7
    /// Seventh flow of BO process
    /// #7                          #7
    public interface ILoanUpdateGuarantorApprovalObject
    {
        Task ClickApproveButtonGuarantorApprovalPage();

        Task ClickRejectButtonGuarantorApprovalPage();

        Task OpenGetApprovalGuarantorApprovalLink();
    }

    public class LoanUpdateGuarantorApprovalObject : ILoanUpdateGuarantorApprovalObject
    {
        public LoanUpdateGuarantorApprovalObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task OpenGetApprovalGuarantorApprovalLink()
        {
            var openGetApprovalGuarantorApprovalLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Get Approval" });
            await openGetApprovalGuarantorApprovalLinkLocator.WaitForAsync();
            await openGetApprovalGuarantorApprovalLinkLocator.ClickAsync();
        }

        public async Task ClickApproveButtonGuarantorApprovalPage()
        {
            var approveButtonGuarantorkLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Approve" });
            await approveButtonGuarantorkLocator.WaitForAsync();
            await approveButtonGuarantorkLocator.ClickAsync();
        }

        public async Task ClickRejectButtonGuarantorApprovalPage()
        {
            var rejectButtonGuarantorLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Reject...", Exact = false });
            await rejectButtonGuarantorLocator.WaitForAsync();
            await rejectButtonGuarantorLocator.ClickAsync();
        }
    }
}