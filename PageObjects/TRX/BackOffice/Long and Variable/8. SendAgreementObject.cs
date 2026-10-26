using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects
{
    /// #8                          #8
    /// Eigth' flow of BO process
    /// #8                          #8
    public interface ISendAgreementObject
    {
        Task ClickSubmitSendAgreementPage();

        Task OpenSendAgreementLink();
    }

    public class SendAgreementObject : ISendAgreementObject
    {
        public SendAgreementObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;

        public async Task ClickSubmitSendAgreementPage()
        {
            var submitButtonSendAgreementPageLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonSendAgreementPageLocator.WaitForAsync();
            await submitButtonSendAgreementPageLocator.ClickAsync();
        }

        public async Task OpenSendAgreementLink()
        {
            // var OpenSendAgreementLinkLocator1 = PageIpage1.GetByRole(AriaRole.Link, new() { Name = "Send agreement (Sign agreement)", Exact = true });
            var openSendAgreementLinkLocator1 = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Send and sign agreement (Agreement (Long))", Exact = true });

            // var OpenSendAgreementLinkLocator2 = PageIpage2.GetByRole(AriaRole.Link, new() { Name = "Send agreement (Sign agreement)" });
            await openSendAgreementLinkLocator1.WaitForAsync();
            await openSendAgreementLinkLocator1.ClickAsync();
        }
    }
}