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
    public interface ILoanUpdateValidateRequestObject
    {
        Task SetBillOfClosureConfirmedByPF(bool setBillOfClosureConfirmedByPF);

        Task SetCounterPartyConfirmReceived(bool setCounterpartyConfirmReceived);

        Task SetIsTheLoanGuaranteed(bool setIsTheLoanGuaranteed);

        Task SetMunicipalDecisionForGuaranty(string setMunicipalDecisionForGuaranty);

        Task SetOrganisationType(string organisationType);

        Task SetOtherTypeOfGuaranty(string otherTypeOfGuaranty);

        Task SetWhatIsTheLoanRequest(string setWhatIsTheLoanRequest);

        Task ClickSubmitValidationPage();
    }

    public class LoanUpdateValidateRequestObject : ILoanUpdateValidateRequestObject
    {
        public LoanUpdateValidateRequestObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;

        public Task SetOrganisationType(string organisationType)
        {
            return pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "The organisation type of the counterparty" }).SelectOptionAsync(new[] { organisationType });
        }

        public Task SetCounterPartyConfirmReceived(bool setCounterpartyConfirmReceived) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Counterparty confirmation received?" }).SelectOptionAsync(new[] { setCounterpartyConfirmReceived ? "Yes" : "No" });

        public Task SetOtherTypeOfGuaranty(string otherTypeOfGuaranty) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Other type of guaranty?" }).SelectOptionAsync(new[] { otherTypeOfGuaranty });

        public Task SetIsTheLoanGuaranteed(bool setIsTheLoanGuaranteed) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Is the loan guaranteed?" }).SelectOptionAsync(new[] { setIsTheLoanGuaranteed ? "Yes" : "No" });

        public Task SetWhatIsTheLoanRequest(string setWhatIsTheLoanRequest) => pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "What is the loan request?" }).SelectOptionAsync(new[] { setWhatIsTheLoanRequest });

        public async Task SetBillOfClosureConfirmedByPF(bool setBillOfClosureConfirmedByPF)
        {
            await pageIpage1.GetByLabel("Bill of Closure confirmed by PF?").SelectOptionAsync(new[] { setBillOfClosureConfirmedByPF ? "Yes" : "No" });
        }

        public async Task SetMunicipalDecisionForGuaranty(string setMunicipalDecisionForGuaranty)
        {
            if (_testSettings.ApplicationUrl.Contains("dt1.pega"))
            {
                await pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Municipal decision for guaranty?" }).SelectOptionAsync(new[] { setMunicipalDecisionForGuaranty });
            }
            else
            {
                await pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "Municipal decision for guaranty? #" }).SelectOptionAsync(new[] { setMunicipalDecisionForGuaranty });
            }
        }

        public Task ClickSubmitValidationPage() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).DblClickAsync(new()
        {
            Delay = 200,
        });
    }
}