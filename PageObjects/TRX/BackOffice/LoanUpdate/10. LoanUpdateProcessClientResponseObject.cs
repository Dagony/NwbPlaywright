using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;
using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;

namespace NwbPlaywright.PageObjects
{
    /// #10                          #10
    /// Tenth flow of BO process
    /// #10                          #10
    public interface ILoanUpdateProcessClientResponseObject
    {
        Task AreTheStandardCDDActivitiesExecuted(string setAreTheStandardCDDActivitiesExecuted);

        Task AreTheSubscriberInCDD(bool isSubscriberinCDD);

        Task ClickSubmitButtonCDDPage();

        Task IsPFNotifiedToExecuteCDD(string setPFNotifiedToExecuteCDD);

        Task IsTheGuarantorAuthenticatedAndVerified(string setIsTheGuarantorAuthenticatedAndVerified);

        Task OpenCheckCDDLink();
    }

    public class LoanUpdateProcessClientResponseObject : ILoanUpdateProcessClientResponseObject
    {
        public LoanUpdateProcessClientResponseObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task OpenCheckCDDLink()
        {
            var openCheckCDDLinkLocator = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Customer due dilligence (Administration)" });

            await openCheckCDDLinkLocator.WaitForAsync();
            await openCheckCDDLinkLocator.ClickAsync();
        }

        public async Task AreTheSubscriberInCDD(bool isSubscriberinCDD)
        {
            // var AreTheSubscriberInCDDLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Are the subscriber in CDD? #" });
            var areTheSubscriberInCDDLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Are the subscriber in CDD?" });
            await areTheSubscriberInCDDLocator.WaitForAsync();
            await areTheSubscriberInCDDLocator.SelectOptionAsync(new[] { isSubscriberinCDD ? "Yes" : "No" });
        }

        public async Task IsPFNotifiedToExecuteCDD(string setPFNotifiedToExecuteCDD)
        {
            // var ISPFNotifiedToExecuteCDDLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is PF notified to execute CDD? #" });
            var iSPFNotifiedToExecuteCDDLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is PF notified to execute CDD?" });
            await iSPFNotifiedToExecuteCDDLocator.WaitForAsync();
            await iSPFNotifiedToExecuteCDDLocator.SelectOptionAsync(new[] { setPFNotifiedToExecuteCDD });
        }

        public async Task AreTheStandardCDDActivitiesExecuted(string setAreTheStandardCDDActivitiesExecuted)
        {
            // var AreTheStandardCDDActivitiesExecutedLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Are the standard CDD activities executed? #" });
            var areTheStandardCDDActivitiesExecutedLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Are the standard CDD activities executed?" });
            await areTheStandardCDDActivitiesExecutedLocator.WaitForAsync();
            await areTheStandardCDDActivitiesExecutedLocator.SelectOptionAsync(new[] { setAreTheStandardCDDActivitiesExecuted });
        }

        public async Task IsTheGuarantorAuthenticatedAndVerified(string setIsTheGuarantorAuthenticatedAndVerified)
        {
            // var IsTheGuarantorAuthenticatedAndVerifiedLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the guarantor authenticated and verified? #" });
            var isTheGuarantorAuthenticatedAndVerifiedLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the guarantor authenticated and verified?" });
            await isTheGuarantorAuthenticatedAndVerifiedLocator.WaitForAsync();
            await isTheGuarantorAuthenticatedAndVerifiedLocator.SelectOptionAsync(new[] { setIsTheGuarantorAuthenticatedAndVerified });
        }

        public async Task ClickSubmitButtonCDDPage()
        {
            var submitButtonCDDPageLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonCDDPageLocator.WaitForAsync();
            await submitButtonCDDPageLocator.ClickAsync();
        }
    }
}