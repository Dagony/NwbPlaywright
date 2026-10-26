using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects
{
    /// #5                          #5
    /// Fifth flow of BO process
    /// #5                          #5
    public interface ILoanUpdateAgreementReviewObject
    {
        Task<string> AgreementReviewAgreementReviewedValidation();

        Task AgreementReviewed(bool isAgreementReviewed);

        Task<string> AgreementReviewSideLetterValidation();

        Task ClickSubmitButtonAgreementReview();

        Task SideLetterInDossier(bool isSideLetterInDossier);

        Task OpenAgreementReviewLink();
    }

    public class LoanUpdateAgreementReviewObject : ILoanUpdateAgreementReviewObject
    {
        public LoanUpdateAgreementReviewObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
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

        public async Task SideLetterInDossier(bool isSideLetterInDossier)
        {
            var sideLetterInDossierLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Side letter in dossier" });
            await sideLetterInDossierLocator.WaitForAsync();
            await sideLetterInDossierLocator.SelectOptionAsync(new[] { isSideLetterInDossier ? "Yes" : "No" });
        }

        public async Task AgreementReviewed(bool isAgreementReviewed)
        {
            var sideLetterInDossierLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Agreement reviewed" });
            await sideLetterInDossierLocator.WaitForAsync();
            await sideLetterInDossierLocator.SelectOptionAsync(new[] { isAgreementReviewed ? "Yes" : "No" });
        }

        public async Task ClickSubmitButtonAgreementReview()
        {
            var submitButtonAgreementReviewLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonAgreementReviewLocator.WaitForAsync();
            await submitButtonAgreementReviewLocator.ClickAsync();
            await Task.Delay(800);
        }

        public async Task<string> AgreementReviewSideLetterValidation()
        {
            var agreementReviewSideLetterBannerLocator = pageIpage1.GetByRole(AriaRole.Banner, new() { Name = "Top Panel" }).Locator("[id=\"Error_\\.Checklist\\.Dossier\\.SideLetter\"]");
            await agreementReviewSideLetterBannerLocator.WaitForAsync();
            var agreementReviewSideLetterValidation = await agreementReviewSideLetterBannerLocator.InnerTextAsync();
            return agreementReviewSideLetterValidation;
        }

        public async Task<string> AgreementReviewAgreementReviewedValidation()
        {
            var agreementReviewSideLetterBannerLocator = pageIpage1.GetByRole(AriaRole.Banner, new() { Name = "Top Panel" }).Locator("[id=\"Error_\\.AgreementReviewed\"]");
            await agreementReviewSideLetterBannerLocator.WaitForAsync();
            var agreementReviewAgreementReviewedValidation = await agreementReviewSideLetterBannerLocator.InnerTextAsync();
            return agreementReviewAgreementReviewedValidation;
        }

        public async Task OpenAgreementReviewLink()
        {
            var openAgreementReviewLinkLocator1 = pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Review agreement (Agreement)", Exact = true });

            // var OpenSendAgreementLinkLocator2 = PageIpage2.GetByRole(AriaRole.Link, new() { Name = "Send agreement (Sign agreement)" });
            await openAgreementReviewLinkLocator1.WaitForAsync();
            await openAgreementReviewLinkLocator1.ClickAsync();
        }
    }
}