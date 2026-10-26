using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;

namespace NwbPlaywright.PageObjects.CLM.Organisation
{
    public class NewOrganisationEntityObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public NewOrganisationEntityObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            pageIpage0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            pageIpage1 = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
            _playwrightDriver = playwrightDriver;
        }

        public async Task SetFullNameOrg(string orgName)
        {
            await page.GetByLabel("Full name").WaitForAsync();
            await page.GetByLabel("Full name").FillAsync(orgName);
        }

        public async Task SetLegalNameOrg(string legalOrgName)
        {
            await page.GetByLabel("legalName").WaitForAsync();
            await page.GetByLabel("legalName").FillAsync(legalOrgName);
        }

        public async Task SetCustomerMnemonic(string mnemonic)
        {
            // max 10 characters!
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pMnemonic\"]").WaitForAsync();
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pMnemonic\"]").FillAsync(mnemonic);
        }

        public async Task SetTaxIDNumber(string taxIDnumber)
        {
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pTaxIDNbr\"]").WaitForAsync();
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pTaxIDNbr\"]").FillAsync(taxIDnumber);
        }

        public async Task SetDateOfFormation(string dateOfFormation)
        {
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pEntityIncorporationDate\"]").WaitForAsync();
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pEntityIncorporationDate\"]").FillAsync(dateOfFormation);
        }

        public async Task SetLEInumber(string numberLEI)
        {
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pLegalEntityIdentifier\"]").WaitForAsync();
            await page.Locator("input[name=\"\\$PpyWorkPage\\$ppyWorkParty\\$gCustomer\\$pLegalEntityIdentifier\"]").FillAsync(numberLEI);
        }

        public async Task SetCOCnumber(string numberCOC)
        {
            await page.GetByLabel("COC number").WaitForAsync();
            await page.GetByLabel("COC number").FillAsync(numberCOC);
        }

        public async Task SetNWBSIC(string codeSIC)
        {
            await page.GetByLabel("NWB SIC").WaitForAsync();
            await page.GetByLabel("NWB SIC").FillAsync(codeSIC);
            await page.GetByLabel("NWB SIC").PressAsync("Tab");
        }

        public async Task SelectLegalForm()
        {
            await page.GetByRole(AriaRole.Cell, new() { Name = "Eenmanszaak", Exact = true }).Locator("div").First.ClickAsync();
        }
    }
}