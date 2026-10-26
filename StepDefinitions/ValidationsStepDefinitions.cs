using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.PageObjects;

namespace NwbPlaywright.StepDefinitions
{
    public class ValidationsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly IBillOfClosureObject _billOfClosureObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;

        public ValidationsStepDefinitions(IPlaywrightDriver playwrightDriver, ScenarioContext scenarioContext, TestSettings testSettings, IBasePageObject basePageObject, ILoginPageObject loginPageObject, INewCaseObject newCaseObject, IFinalSpreadObject finalSpreadObject, IWinLostCorrectObject winLostCorrectObject, IBillOfClosureObject billOfClosureObject)
        {
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _playwrightDriver = playwrightDriver;
            _winLostCorrectObject = winLostCorrectObject;
            _billOfClosureObject = billOfClosureObject;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
        }

        [Given(@"I empty credit limit and chose for a linear type loan")]
        public async Task WhenIEmptyCreditLimitAndChoseForALinearTypeLoanAndPress()
        {
            await _basePageObject.PageWaitForUrlLoaded();
            await _finalSpreadObject.CheckGuarantorBox();
            await Task.Delay(500);
            await _finalSpreadObject.ClickAddGuarantorButton();
            await _finalSpreadObject.SetAmortisationType("Linear");
            await _finalSpreadObject.SetCreditLimit(" ");
        }

        [When(@"I press SaveAndClose in the menu")]
        public async Task WhenIPressSaveAndCloseInTheMenu()
        {
            await _finalSpreadObject.ClickSaveOffer();
            await _finalSpreadObject.SaveAndCloseFinalSpread();
        }

        [Then(@"Credit Limit, CDD review date, Spread Check by, Notional, Deal moment, Startdate, Maturity date, Coupon day, Swap needed, NumbOfAmort, AmortFreq, Spread H(.*), Finalspread, Market interest rate will highlight red\.")]
        public async Task ThenCreditLimitCDDReviewDateSpreadCheckByNotionalDealMomentStartdateMaturityDateCouponDaySwapNeededNumbOfAmortAmortFreqSpreadHFinalspreadMarketInterestRateWillHighlightRed_(int p0)
        {
            var creditLimitError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pCounterparty\\$pCreditLimitError\"]").GetByText("Value cannot be blank").TextContentAsync();
            var cDDreviewError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pCounterparty\\$pCDDReviewDateError\"]").GetByText("Value cannot be blank").TextContentAsync();
            var colleagueCheckError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pColleagueCheckError\"]").GetByText("Value cannot be blank").TextContentAsync();
            var notionalError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pNotionalError\"] > .iconError").TextContentAsync();
            var dealMomentError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pDealMomentError\"] > .iconError").TextContentAsync();
            var startDateError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pStartDateError\"] > .iconError").TextContentAsync();
            var couponDayError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pCouponDayError\"] > .iconError").TextContentAsync();
            var numbOfAmortError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pAmortisationError\"] > .iconError").TextContentAsync();
            var amortFreqError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pAmortisationFrequencyError\"] > .iconError").TextContentAsync();
            var spreadH1Error = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pSpreadRate1stManError\"] > .iconError").TextContentAsync();
            var finalSpreadError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pSpreadRateFinalError\"] > .iconError").TextContentAsync();
            var marketIRateError = await _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").Locator("[id=\"\\$PpyWorkPage\\$pOffers\\$l1\\$pAlternatives\\$l1\\$pMarketInterestRateError\"] > .iconError").TextContentAsync();

            Console.WriteLine(creditLimitError);
            Console.WriteLine(cDDreviewError);
            Console.WriteLine(colleagueCheckError);
            Console.WriteLine(notionalError);
            Console.WriteLine(dealMomentError);
            Console.WriteLine(startDateError);
            Console.WriteLine(couponDayError);
            Console.WriteLine(numbOfAmortError);
            Console.WriteLine(amortFreqError);
            Console.WriteLine(spreadH1Error);
            Console.WriteLine(finalSpreadError);
            Console.WriteLine(marketIRateError);
        }
    }
}