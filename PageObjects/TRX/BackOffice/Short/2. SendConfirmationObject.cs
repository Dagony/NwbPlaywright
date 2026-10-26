using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #2                          #2
    /// Second flow of BO process short
    /// #2                          #2
    public interface ISendConfirmationObject
    {
        Task SetIsTheIbanInOPICS(string isConfirmationSentToCustomer);

        Task ReasonField(string reason);

        Task ClickSubmitSendConfirmation();

        public Task SetConfirmationSentToCustomer(string isConfirmationSentToCustomer);

        public Task SetConfirmationSentToGuarantor(string isConfirmationSentToGuarantor);
    }

    public class SendConfirmationObject : ISendConfirmationObject
    {
        public SendConfirmationObject(IPlaywrightDriver playwrightDriver)
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

        public Task SetIsTheIbanInOPICS(string isConfirmationSentToCustomer) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Confirmation sent to customer?" }).SelectOptionAsync(new[] { isConfirmationSentToCustomer });

        public async Task SetConfirmationSentToCustomer(string isConfirmationSentToCustomer)
        {
            var confirmationSentLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Confirmation sent to customer?" });
            await confirmationSentLocator.WaitForAsync();
            await confirmationSentLocator.SelectOptionAsync(new[] { isConfirmationSentToCustomer });
        }

        public Task SetConfirmationSentToGuarantor(string isConfirmationSentToGuarantor) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Confirmation sent to guarantor?" }).SelectOptionAsync(new[] { isConfirmationSentToGuarantor });

        public async Task ReasonField(string reason) => await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Reason" }).FillAsync(reason);

        public Task ClickSubmitSendConfirmation() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).DblClickAsync(new()
        {
            Delay = 200,
        });
    }
}