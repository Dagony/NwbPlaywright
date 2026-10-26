using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.PageObjects
{
    public interface INewCaseObject
    {
        Task ClickCounterpartyField();

        Task ClickSubmitCustomerAdvisorPage();

        Task ClickSubmitOperatingPage();

        Task<string> GetRandomIndividual(string counterParty);

        Task StartNewPricingGridCase();

        Task<string> GetRandomAdvisorIndividual(string counterParty);

        Task<string> GetLegalNameAdvisoryLoanUpdateAuto();

        Task StartNewResearchInteraction();

        Task<string> GetLegalNameAdvisory();

        // Task SelectCounterparty();
        Task SelectCustomerNew(string customerName);

        // Task SelectAdvisorDev(string advisorName);
        Task<string> GetRandomCustomer();

        Task SelectFirstIndividual();

        Task SetCounterparty(CaseType option, string counterParty);

        Task StartNewCase();

        Task SelectCustomerNewLoanUpdateAutomatic(string customerName);

        Task<bool> User1AccountDesk1IsVisible();
    }

    public class NewCaseObject : INewCaseObject
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIframe0;
        private readonly IFrameLocator pageIframe1;
        private readonly IFrameLocator pageIframe2;
        private readonly IBasePageObject _basePageObject;
        private readonly ISharedDataIndividuals _sharedDataIndividual;
        private readonly ScenarioContext _scenarioContext;

        public NewCaseObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, ISharedDataIndividuals sharedDataIndividuals, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            this.pageIframe0 = page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            this.pageIframe1 = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            this.pageIframe2 = page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
            _basePageObject = basePageObject;
            _sharedDataIndividual = sharedDataIndividuals;
            _scenarioContext = scenarioContext;
        }

        private List<string> greenDale = new List<string>
        {
            "Trx Jeff Winger",
            "Trx Britta Perry",
            "Trx Abed Nadir",
            "Trx Annie Edison",
            "Trx Shirley Bennet",
            "Trx Troy Barnes",
            "Trx Pierce Hawthorne",
            "Trx Benjamin Hoon von Chang",
        };

        private List<string> heiligHart = new List<string>
        {
            "Trx John Dorian",
            "Trx Elliot Reid",
            "Trx Christopher Turk",
            "Trx Perry Cox",
            "Trx Carla Espinosa",
            "Trx Bob Kelso",
            "Trx Todd Benjamin von Quinlan",
        };

        private List<string> meerdijk = new List<string>
        {
            "Trx Ludo Sanders",
            "Trx Janine Elschot",
            "Trx Nina Sanders",
            "Trx Maxime Sanders",
            "Trx Anton Bouwhuis",
            "Trx Bing Mauricius",
            "Trx Sjoerd Bouwhuis",
            "Trx Jessica Maria de Harmsen",
            "Trx Arnie Alberts",
        };

        private List<string> grootWaterland = new List<string>
        {
            "Trx Alfred Jodocus Kwak",
            "Trx Henk de Mol",
            "Trx Dolf de Kraai",
            "Trx Winnie Wanna",
            "Trx Pikkie de Ekster",
            "Trx Ollie de Ooievaar",
            "Trx Professor Paljas von Pinguïn",
        };

        private List<string> dunderMifflin = new List<string>
        {
            "Trx Michael Scott",
            "Trx Jim Halpert",
            "Trx Dwight Schrute",
            "Trx Pam Beesly",
            "Trx Ryan Howard",
            "Trx Angela Martin",
            "Trx Stanley Hudson",
            "Trx Kelly Chrissy van der Kapoor",
            "Trx Kevin Malone",
        };

        private List<string> groenFonds = new List<string>
        {
            "Trx Daan Janssen",
            "Trx Lotte van der Berg",
            "Trx Thijs de Vries",
            "Trx Sanne van Dijk",
        };

        private List<string> hVC = new List<string>
        {
            "Trx Bram Bakker",
            "Trx Sophie Mulder",
            "Trx Eva van Leeuwen",
            "Trx Ruben de Jong",
        };

        private List<string> restauratieFonds = new List<string>
        {
            "Trx Julia van den Heuvel",
            "Trx Lisa Smit",
            "Trx Tom van Dijk",
            "Trx Lars van der Meer",
        };

        private List<string> counterparties = new List<string>
        {
            "Trx Stichting Greendale Wonen",
            "Trx Stichting Zorggroep Heilig Hart",
            "Trx Gemeente Meerdijk",
            "Trx Waterschap Groot Waterland",
            "Trx Dunder Mifflin Paper Company",
            "Trx Stichting Groenfonds",
            "Trx HVC N.V.",
            "Trx Stichting nationaal restauratiefonds",
        };

        private List<string> samenwerkingsverbandNul = new List<string> { "Bennie Bondje" };

        private List<string> samenwerkingsverbandTwintig = new List<string> { "Freddy Luijten" };

        private List<string> saulGoodman = new List<string>
        {
            "Trx Nacho Varga",
            "Trx Mike Ermantraut",
            "Trx Jimmy McGill",
            "Trx Chuck McGill",
            "Trx Howard Hamlin",
            "Trx Hector Salamanca",
            "Trx Kim Wexler",
            "Trx Lalo Fernando Alejandro de los Santos Salamanca",
        };

        private List<string> casterlyRock = new List<string>
        {
            "Trx Tommen Robert van der Baratheon",
            "Trx Cersei Lannister",
            "Trx Jaime Lannister",
            "Trx Tywin Lannister",
            "Trx Joffrey Baratheon",
            "Trx Myrcella Baratheon",
            "Trx Tyrion Lannister",
        };

        // OPENING SCREEN - WORK LIST //
        public async Task StartNewCase()
        {
            await Task.Delay(1500);
            await page.Locator("[data-test-id=\"2014100609491604293426\"]").FocusAsync();
            await page.Locator("[data-test-id=\"2014100609491604293426\"]").ClickAsync();

            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Phone call" }).FocusAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Phone call" }).ClickAsync();
        }

        public async Task StartNewResearchInteraction()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).WaitForAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).ClickAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Research" }).WaitForAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Research" }).ClickAsync();
            await _basePageObject.WaitForPageLoadedState();
        }

        public async Task StartNewPricingGridCase()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).WaitForAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).ClickAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Update Pricing Grid" }).WaitForAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Update Pricing Grid" }).ClickAsync();
            await _basePageObject.WaitForPageLoadedState();
        }

        public Task<bool> User1AccountDesk1IsVisible() => pageIframe0.GetByText("TestUser AccountDesk1").IsVisibleAsync();

        // COUNTERPARTY SCREEN //
        public Task ClickCounterpartyField() => pageIframe1.GetByPlaceholder("Enter counterparty name. Searches from 3 characters").ClickAsync();

        public Task SelectFirstIndividual() => pageIframe1.Locator("(//td[@name='BASE_REF'])[1]").ClickAsync();

        public async Task SetCounterparty(CaseType option, string counterParty)
        {
            ILocator counterPartySearchLocator;

            if (option == CaseType.LoanRequest)
            {
                counterPartySearchLocator = pageIframe1.GetByPlaceholder("Enter counterparty name. Searches from 3 characters");
            }
            else if (option == CaseType.ResearchInteraction)
            {
                counterPartySearchLocator = pageIframe1.GetByLabel("Customer name");
            }
            else
            {
                throw new Exception("CaseType enum not set in method SetCounterparty()");
            }

            await counterPartySearchLocator.FillAsync(counterParty, options: new LocatorFillOptions { Force = false, Timeout = 60000 });
            await _basePageObject.WaitForPageLoadedState();
            await _basePageObject.PressArrowDown();
            await pageIframe1.GetByText(counterParty, new() { Exact = true }).First.WaitForAsync();
            await pageIframe1.GetByText(counterParty, new() { Exact = true }).First.ClickAsync(new LocatorClickOptions { Delay = 200 });
        }

        public async Task<string> GetLegalNameAdvisory()
        {
            var legalNameLocator = pageIframe1.Locator("xpath=(//div[contains(@class,'field-item dataValueRead')])[11]");
            await legalNameLocator.WaitForAsync();
            var legalName = await legalNameLocator.TextContentAsync() ?? string.Empty;
            return legalName;
        }

        public async Task<string> GetLegalNameAdvisoryLoanUpdateAuto()
        {
            var legalNameLocator = pageIframe1.Locator("xpath=(//div[contains(@class,'field-item dataValueRead')])[15]");
            await legalNameLocator.WaitForAsync();
            var legalName = await legalNameLocator.TextContentAsync() ?? string.Empty;
            return legalName;
        }

        public async Task<string> GetRandomCustomer()
        {
            Random r = new Random();
            int index = r.Next(counterparties.Count);
            string randomCounterparty = counterparties[index];
            await Task.CompletedTask;
            return randomCounterparty;
        }

        public async Task<string> GetRandomIndividual(string counterParty)
        {
            Random r = new Random();
            int index = 0;
            string randomIndividual = string.Empty;
            var tasks = counterParty switch
            {
                "Trx Stichting Greendale Wonen" => (index = r.Next(greenDale.Count), randomIndividual = greenDale[index], greenDale.Remove(randomIndividual)),
                "Trx Stichting Zorggroep Heilig Hart" => (index = r.Next(heiligHart.Count), randomIndividual = heiligHart[index], heiligHart.Remove(randomIndividual)),
                "Trx Gemeente Meerdijk" => (index = r.Next(meerdijk.Count), randomIndividual = meerdijk[index], meerdijk.Remove(randomIndividual)),
                "Trx Waterschap Groot Waterland" => (index = r.Next(grootWaterland.Count), randomIndividual = grootWaterland[index], grootWaterland.Remove(randomIndividual)),
                "Trx Dunder Mifflin Paper Company" => (index = r.Next(dunderMifflin.Count), randomIndividual = dunderMifflin[index], dunderMifflin.Remove(randomIndividual)),
                "Trx Stichting Groenfonds" => (index = r.Next(groenFonds.Count), randomIndividual = groenFonds[index], groenFonds.Remove(randomIndividual)),
                "Trx HVC N.V." => (index = r.Next(hVC.Count), randomIndividual = hVC[index], hVC.Remove(randomIndividual)),
                "Trx Stichting nationaal restauratiefonds" => (index = r.Next(restauratieFonds.Count), randomIndividual = restauratieFonds[index], restauratieFonds.Remove(randomIndividual)),
                "Trx Samenwerkingsverband Nul" => (index = r.Next(samenwerkingsverbandNul.Count), randomIndividual = samenwerkingsverbandNul[index], samenwerkingsverbandNul.Remove(randomIndividual)),
                "Trx Samenwerkingsverband Twintig" => (index = r.Next(samenwerkingsverbandTwintig.Count), randomIndividual = samenwerkingsverbandTwintig[index], samenwerkingsverbandTwintig.Remove(randomIndividual)),
                _ => throw new Exception("Unknown option chosen for switch expression counterParty switch,origin: GetRandomindividual()"),
            };

            await Task.CompletedTask;
            return randomIndividual;
        }

        public async Task<string> GetRandomAdvisorIndividual(string counterParty)
        {
            Random r = new Random();
            int index = 0;
            string randomAdvisorIndividual = string.Empty;
            var tasks = counterParty switch
            {
                "Trx Stichting Greendale Wonen" => (index = r.Next(saulGoodman.Count), randomAdvisorIndividual = saulGoodman[index], saulGoodman.Remove(randomAdvisorIndividual)),
                "Trx Stichting Zorggroep Heilig Hart" => (index = r.Next(saulGoodman.Count), randomAdvisorIndividual = saulGoodman[index], saulGoodman.Remove(randomAdvisorIndividual)),
                "Trx Gemeente Meerdijk" => (index = r.Next(casterlyRock.Count), randomAdvisorIndividual = casterlyRock[index], casterlyRock.Remove(randomAdvisorIndividual)),
                "Trx Waterschap Groot Waterland" => (index = r.Next(casterlyRock.Count), randomAdvisorIndividual = casterlyRock[index], casterlyRock.Remove(randomAdvisorIndividual)),
                "Trx Dunder Mifflin Paper Company" => (index = r.Next(casterlyRock.Count), randomAdvisorIndividual = casterlyRock[index], casterlyRock.Remove(randomAdvisorIndividual)),
                "Trx Stichting Groenfonds" => (index = r.Next(saulGoodman.Count), randomAdvisorIndividual = saulGoodman[index], saulGoodman.Remove(randomAdvisorIndividual)),
                "Trx HVC N.V." => (index = r.Next(saulGoodman.Count), randomAdvisorIndividual = saulGoodman[index], saulGoodman.Remove(randomAdvisorIndividual)),
                "Trx Stichting nationaal restauratiefonds" => (index = r.Next(saulGoodman.Count), randomAdvisorIndividual = saulGoodman[index], saulGoodman.Remove(randomAdvisorIndividual)),
                _ => throw new Exception("Unknown option chosen for switch expression counterParty switch,origin: GetRandomindividual()"),
            };
            await Task.CompletedTask;
            return randomAdvisorIndividual;
        }

        public async Task SelectCustomerNew(string customerName)
        {
            var customerSelectLocator = pageIframe1.Locator($"xpath=//*[@id=\"bodyTbl_right\"]//*[text()[contains(.,'{customerName}')]]/parent::div/parent::td/parent::tr/td[4]").GetByRole(AriaRole.Link);
            var customerPhoneLocator = pageIframe1.Locator($"xpath=//*[@id=\"bodyTbl_right\"]//*[text()[contains(.,'{customerName}')]]/parent::div/parent::td/parent::tr/td[3]");
            var customerEmailLocator = pageIframe1.Locator($"xpath=//*[@id=\"bodyTbl_right\"]//*[text()[contains(.,'{customerName}')]]/parent::div/parent::td/parent::tr/td[2]");
            await customerPhoneLocator.WaitForAsync();
            await customerEmailLocator.WaitForAsync();

            var phoneElement = await customerPhoneLocator.TextContentAsync();
            var emailElement = await customerEmailLocator.TextContentAsync();

            phoneElement.Should().NotBeNullOrEmpty();
            emailElement.Should().NotBeNullOrEmpty();

            var customer = new IndividualFields
            {
                PhoneIndividual = phoneElement,
                EmailIndividual = emailElement,
                FullName = customerName,
            };
            _sharedDataIndividual.Customers.Add(customer);
            await customerSelectLocator.ClickAsync();
        }

        public async Task SelectCustomerNewLoanUpdateAutomatic(string customerName)
        {
            var customerSelectLocator = pageIframe1.Locator($"xpath=//tr[contains(@class, 'cellCont') and .//span[text()='{customerName}']]//input[@type='checkbox']");

            var customerPhoneLocator = pageIframe1.Locator($"xpath=//tr[contains(@class, 'cellCont') and .//span[text()='{customerName}']]//td[@data-attribute-name='Phone']//span");
            var customerEmailLocator = pageIframe1.Locator($"xpath=//tr[contains(@class, 'cellCont') and .//span[text()='{customerName}']]//td[@data-attribute-name='Email']//span");
            await customerPhoneLocator.WaitForAsync();
            await customerEmailLocator.WaitForAsync();

            var phoneElement = await customerPhoneLocator.TextContentAsync();
            var emailElement = await customerEmailLocator.TextContentAsync();

            var customer = new IndividualFields
            {
                PhoneIndividual = phoneElement,
                EmailIndividual = emailElement,
                FullName = customerName,
            };
            _sharedDataIndividual.Customers.Add(customer);
            await customerSelectLocator.ClickAsync();
        }

        public async Task ClickSubmitCustomerAdvisorPage()
        {
            var submitCustomerAdvisorButton = pageIframe1.GetByRole(AriaRole.Button, new() { Name = "Submit" });

            await submitCustomerAdvisorButton.WaitForAsync();
            await submitCustomerAdvisorButton.ClickAsync();
            await _basePageObject.PageWaitForUrlLoaded();
        }

        public async Task ClickSubmitOperatingPage()
        {
            var submitOperatingPage = page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByRole(AriaRole.Button, new() { Name = "Submit" });

            await submitOperatingPage.WaitForAsync();
            await submitOperatingPage.ClickAsync();
            await _basePageObject.PageWaitForUrlLoaded();
        }
    }
}