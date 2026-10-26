using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects
{
    /// #12                          #12
    /// Twelfth flow of BO process
    /// #12                          #12
    public interface ILoanUpdateCheckDossierCompleteObject
    {
        Task<string> AgreementOfLoanValidation();

        Task AuthenticityofAgreementChecked(bool isAuthenticityofAgreementChecked);

        Task<string> AuthenticityOfDocumentsValidation();

        Task ClickSubmitButtonOpenCheckDossierCompletePage();

        Task FullSignedAgreementInDossier(bool isFullSignedAgreementInDossier);

        Task FullSignedAgreementSendOutToAllParties(bool isFullSignedAgreementSendOutToAllParties);

        Task<string> SignedAgreementSendValidation();

        Task SetPaid(bool? setPaid);

        Task SetPaymentDate(string paymentDate);

        Task ClickSubmitPaymentCasePage();
    }

    public class LoanUpdateCheckDossierCompleteObject : ILoanUpdateCheckDossierCompleteObject
    {
        public LoanUpdateCheckDossierCompleteObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject)
        {
            this.page = playwrightDriver.Page.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
        }

        private readonly IPage page;
        private readonly IFrameLocator pageIpage1;

        public async Task FullSignedAgreementInDossier(bool isFullSignedAgreementInDossier)
        {
            // var FullSignedAgreementInDossierLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Full signed argreement in dossier? #" });
            var fullSignedAgreementInDossierLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Full signed argreement in dossier?" });
            await fullSignedAgreementInDossierLocator.WaitForAsync();
            await fullSignedAgreementInDossierLocator.SelectOptionAsync(new[] { isFullSignedAgreementInDossier ? "Yes" : "No" });
        }

        public async Task AuthenticityofAgreementChecked(bool isAuthenticityofAgreementChecked)
        {
            // var AuthenticityofAgreementCheckedLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Authenticity of agreement checked? #" });
            var authenticityofAgreementCheckedLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Authenticity of agreement checked?" });
            await authenticityofAgreementCheckedLocator.WaitForAsync();
            await authenticityofAgreementCheckedLocator.SelectOptionAsync(new[] { isAuthenticityofAgreementChecked ? "Yes" : "No" });
        }

        public async Task FullSignedAgreementSendOutToAllParties(bool isFullSignedAgreementSendOutToAllParties)
        {
            // var FullSignedAgreementSendOutToAllPartiesLocator = PageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is full signed agreement send out to all parties? #" });
            var fullSignedAgreementSendOutToAllPartiesLocator = pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is full signed agreement send out to all parties?" });
            await fullSignedAgreementSendOutToAllPartiesLocator.WaitForAsync();
            await fullSignedAgreementSendOutToAllPartiesLocator.SelectOptionAsync(new[] { isFullSignedAgreementSendOutToAllParties ? "Yes" : "No" });
        }

        public async Task ClickSubmitButtonOpenCheckDossierCompletePage()
        {
            var submitButtonOpenCheckDossierCompletePage = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonOpenCheckDossierCompletePage.WaitForAsync();
            await submitButtonOpenCheckDossierCompletePage.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task<string> AuthenticityOfDocumentsValidation()
        {
            var authenticityOfDocumentsValidationLocator = pageIpage1.GetByRole(AriaRole.Banner, new() { Name = "Top Panel" }).Locator("[id=\"Error_\\.Checklist\\.Dossier\\.AuthenticityOfDocuments\"]");
            await authenticityOfDocumentsValidationLocator.WaitForAsync();
            var authenticityOfDocumentsValidationText = await authenticityOfDocumentsValidationLocator.InnerTextAsync();
            return authenticityOfDocumentsValidationText;
        }

        public async Task<string> AgreementOfLoanValidation()
        {
            var agreementOfLoanValidationLocator = pageIpage1.GetByRole(AriaRole.Banner, new() { Name = "Top Panel" }).Locator("[id=\"Error_\\.Checklist\\.Dossier\\.AgreementOfLoan\"]");
            await agreementOfLoanValidationLocator.WaitForAsync();
            var agreementOfLoanValidationText = await agreementOfLoanValidationLocator.InnerTextAsync();
            return agreementOfLoanValidationText;
        }

        public async Task<string> SignedAgreementSendValidation()
        {
            var signedAgreementSendValidationLocator = pageIpage1.GetByRole(AriaRole.Banner, new() { Name = "Top Panel" }).Locator("[id=\"Error_\\.Checklist\\.Dossier\\.SignedAgreementSend\"]");
            await signedAgreementSendValidationLocator.WaitForAsync();
            var signedAgreementSendValidationText = await signedAgreementSendValidationLocator.InnerTextAsync();
            return signedAgreementSendValidationText;
        }

        public async Task SetPaid(bool? setPaid)
        {
            if (setPaid is not null)
            {
                await pageIpage1.GetByLabel("Paid?").FocusAsync();
                await pageIpage1.GetByLabel("Paid?").SelectOptionAsync(new[] { (bool)setPaid ? "Yes" : "No" });
            }
            else
            {
                throw new NullReferenceException("SetPaid(): Seems that parameter setPaid is null!");
            }
        }

        public async Task SetPaymentDate(string paymentDate)
        {
            var paymentDateLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "PaymentDate*" });
            await paymentDateLocator.FocusAsync();
            await paymentDateLocator.FillAsync(paymentDate);
        }

        public async Task ClickSubmitPaymentCasePage()
        {
            var submitButtonOpenCheckDossierCompletePage = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await submitButtonOpenCheckDossierCompletePage.WaitForAsync();
            await submitButtonOpenCheckDossierCompletePage.ClickAsync();
            await Task.Delay(1000);
        }
    }
}