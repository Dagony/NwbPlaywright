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
    /// #3                          #3
    /// Third flow of BO process
    /// #3                          #3
    public interface ISurePayCheckObject
    {
        Task ClickSubmitButtonSurePayCheckPage();

        Task SetIsTheIbanInOPICS(string isTheIbanInOPICS);

        Task SetIstheIBANverifiedInSurePay(string isTheIbanVerifiedInSurePay);

        Task OpenSurepayLink();
    }

    public class SurePayCheckObject : ISurePayCheckObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;

        public SurePayCheckObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
        }

        // aanpassing nav pega infinity upgrade, staat momenteel ingesteld voor: OTA
        public Task SetIsTheIbanInOPICS(string isTheIbanInOPICS)
        {
            // if (_testSettings.ApplicationUrl.Contains("dt1.pega"))
            // {
            //    return PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN in OPICS equal with the agreement?" }).SelectOptionAsync(new[] { isTheIbanInOPICS });
            // }
            // else if (_testSettings.ApplicationUrl.Contains("dt2.pega"))
            // {
            return pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN in OPICS equal with the agreement?" }).SelectOptionAsync(new[] { isTheIbanInOPICS });

            // }
            // else
            // {
            //    return PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN in OPICS equal with the agreement? #" }).SelectOptionAsync(new[] { isTheIbanInOPICS });
            // }
        }

        public Task SetIstheIBANverifiedInSurePay(string isTheIbanVerifiedInSurePay)
        {
            // if (_testSettings.ApplicationUrl.Contains("dt1.pega"))
            // {
            //    return PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN verified in SurePay?" }).SelectOptionAsync(new[] { isTheIbanVerifiedInSurePay });
            // }
            // else if (_testSettings.ApplicationUrl.Contains("dt2.pega"))
            // {
            return pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN verified in SurePay?" }).SelectOptionAsync(new[] { isTheIbanVerifiedInSurePay });

            // }
            // else
            // {
            //    return PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the IBAN verified in SurePay? #" }).SelectOptionAsync(new[] { isTheIbanVerifiedInSurePay });
            // }
        }

        public async Task ClickSubmitButtonSurePayCheckPage()
        {
            var submitButtonSurePayCheckPageLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonSurePayCheckPageLocator.WaitForAsync();
            await submitButtonSurePayCheckPageLocator.ClickAsync();
        }

        public async Task OpenSurepayLink()
        {
            var openSurePaytLinkLocator1 = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Surepay check (Create agreement)", Exact = true });

            // var OpenSendAgreementLinkLocator2 = PageIpage2.GetByRole(AriaRole.Link, new() { Name = "Send agreement (Sign agreement)" });
            await openSurePaytLinkLocator1.WaitForAsync();
            await openSurePaytLinkLocator1.ClickAsync();
        }
    }
}