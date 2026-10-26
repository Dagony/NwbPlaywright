using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects
{
    /// #9                          #9
    /// Ninth flow of BO process
    /// #9                          #9
    public interface IClientApprovalObject
    {
        Task ClickApproveButtonClientApprovalPage();

        Task ClickRejectButtonClientApprovalPage();

        Task OpenGetApprovalPendingClientApprovalLink();
    }

    public class ClientApprovalObject : IClientApprovalObject
    {
        public ClientApprovalObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task OpenGetApprovalPendingClientApprovalLink()
        {
            var openGetApprovalPendingClientApprovalLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Get Approval" });
            await openGetApprovalPendingClientApprovalLinkLocator.WaitForAsync();
            await openGetApprovalPendingClientApprovalLinkLocator.ClickAsync();
        }

        public async Task ClickApproveButtonClientApprovalPage()
        {
            var approveButtonClientApprovalLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Approve" });
            await approveButtonClientApprovalLocator.WaitForAsync();
            await approveButtonClientApprovalLocator.ClickAsync();
        }

        public async Task ClickRejectButtonClientApprovalPage()
        {
            var rejectButtonClientApprovalLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Reject...", Exact = false });
            await rejectButtonClientApprovalLocator.WaitForAsync();
            await rejectButtonClientApprovalLocator.ClickAsync();
        }
    }
}