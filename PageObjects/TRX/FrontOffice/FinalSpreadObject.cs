using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.PageObjects
{
    public interface IFinalSpreadObject
    {
        Task CheckAdditionalEmailBox();

        Task CheckGuarantorBox();

        Task CheckIncludeInMailBox();

        Task<bool> GuarantorCheckboxStatus();

        Task<ILocator> GetIncludeInMailBoxLocator();

        Task CheckIncludeRemarkInMailBox();

        Task ClickGuarantorField(int? indexOfGuarantor = null);

        Task<bool> CheckIfGuarantorFieldIsAlreadyPresent();

        Task DeleteGuarantorField();

        Task SetCounterpartyUpdateComment(string updateComment);

        Task ClickAddAlternative(int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task ClickAddEmailButton(int? clickCountEmail = null);

        Task ClickAddGuarantorButton(int? clickCountGuarantor = null);

        Task ClickAddOffer(int? offerNumbInCase = null);

        Task ClickCopyAlternative(int numberOfClicks);

        Task ClickDeleteAlternative(int numberOfClicks, int? altNumbInCase = null);

        Task ClickDeleteOffer();

        Task ClickRefresh();

        Task ClickSaveFinalSpreadButton(int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task ClickSaveH1Button(int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task ClickSaveH2Button(int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task ClickSaveMarketRate(int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task ClickSaveMarketRate2();

        Task ClickSaveOffer();

        Task ClickOfferReference();

        Task ClickSubmitFinalSpreadPage();

        Task<string> GetCaseID();

        Task ClickSaveMargin();

        Task ClickSubmitFinalSpreadPageClosedOff();

        Task Set48HoursClause(bool? set48HoursClause, int? alternativeNumbIncase = null);

        Task SetAmortisationFrequency(string amortisationfrequency, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetAmortisationType(string amortisationtype, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCDDCategory(EnumsForTRX.CDDcategory options);

        Task SetCDDReviewDate(string setCDDDate);

        Task OverwriteSpreadMisMatch(int? totalAltSections = null);

        Task ClearNotional(int? offerNumbInCase = null);

        Task ClickSaveBaseRate();

        Task OpenEmailSection();

        Task SetNegativeInterestRate(bool negativeInterestRate);

        Task<string> GetMinimalWithdrawalPercentageValue();

        Task SetCommitmentFee(string setCommitmentFee, int? offerNumbInCase = null);

        Task SetCounterparty(string counterParty);

        Task SetCouponDay(string setCouponDay, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCouponFrequency(string couponfrequency, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCreditLimit(string setCreditLimit);

        Task SetDayCount(string dayCount, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetDealMoment(string dealMoment, int? offerNumbInCase = null);

        Task SetDealRemark(string dealremark, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetEmailAddress(string setEmailAddress, int? indexOfEmailAddress = null);

        Task SetFinalSpread(string setFinalSpread, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetFinalSpreadBasis(string setFinalSpreadEuribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetFinalSpreadComment(string setFinalSpreadComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetFirstCouponDate(string firstCouponDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetGuarantorField(string guarantorOne, int? indexOfGuarantor = null);

        Task ClearGuarantorField(int? indexOfGuarantor = null);

        Task SetGuarantPercentage(string guarantorPercentage, int? indexOfGuarantorPercentage = null);

        Task SetInterestSpreadRRDate(string interestSpreadRRDate1, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetLastCouponDate(string lastCouponDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetMarketRate(string setMarketRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetMaturityDate(string maturityDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetMinimalWithdrawalPercent(string setMinimalWithdrawal, int? offerNumbInCase = null);

        Task SetNotificationMinutes(string setNotifcationMinutes, int? offerNumbInCase = null);

        Task SetNotional(string notional1, int? offerNumbInCase = null);

        Task SetNumberOfAmortisations(string numberofamortisations, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetOutstandingPrincipleAmount(string setPrincipleAmount);

        Task SetReferenceRate(string setReferenceRate, int? alternativeNumbIncase = null);

        Task SetSpreadH1(string setH1spread, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadH1Basis(string setH1Euribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadH1Comment(string setH1Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadH2(string setH2spread, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadH2Basis(string setH2Euribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadH2Comment(string setH2Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadMisMatchComment(string setSpreadMisMatchComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetStartDate(string startdate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSwap(bool setswap, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCouponDayOptionSet(CouponDay? options, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCustomerRateH1(string setCustomerRateH1, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCustomerRateH2(string setCustomerRateH2, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCustomerH1Comment(string setCustomerH1Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetCustomerH2Comment(string setCustomerH2Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSpreadMisMatchCustomerRateComment(string setSpreadMisMatchCustomerRateComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetFinalCustomerRate(string setFinalCustomerRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetFinalCustomerRateComment(string setFinalCustomerRateComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetMargin(string setMargin, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetMarginComment(string setMarginComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SpreadCheckBy(bool spreadcheck);

        Task SpreadCheckSetRemarks(string remarks);

        Task StartNewCase();

        Task UnCheckAdditionalEmailBox();

        Task UncheckGuarantorBox();

        Task SaveAndCloseFinalSpread();

        Task<string> GetMinimalWithdrawalAmount();

        Task HighNotionalDealApproved(bool setApproval);

        Task HighNotionalProcurationMail();

        Task HighNotionalCheckOverwrite();

        Task HighNotionalUncheckOverwrite();

        Task HighNotionalSetReasonForOverwite(string overWriteReasonMessage);

        Task HighNotionalSetNameOfApprover(string setNameOfApprover);

        Task ClickCopySwap(int? alternativeNumbIncase = null);

        Task<string> CopyDataFromSWAPCopyfunction();

        Task SetBusinessDayConvention(string setBusinessDayConvention, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetClearingHouse(string setClearingHouse, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSwapReferenceRate(string setSwapReferenceRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null);
    }

    public class FinalSpreadObject : IFinalSpreadObject
    {
        public FinalSpreadObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IBasePageObject basePageObject, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            _testSettings = testSettings;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _counterpartyFieldLocator = pageIpage1.GetByPlaceholder("Enter counterparty name. Searches from 3 characters");
            _basePageObject = basePageObject;
            _includeInMailBoxLocator = pageIpage1.GetByLabel("Include in mail");
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly TestSettings _testSettings;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly ILocator _counterpartyFieldLocator;
        private readonly IBasePageObject _basePageObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly ScenarioContext _scenarioContext;

        // OPENING SCREEN - WORK LIST //
        public async Task StartNewCase()
        {
            await page.Context.GrantPermissionsAsync(new[] { "geolocation" });

            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).WaitForAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "New" }).ClickAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Phone call" }).WaitForAsync();
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Phone call" }).ClickAsync();
            await _basePageObject.PageWaitForUrlLoaded();
        }

        // COUNTERPARTY SCREEN //
        public async Task SetCounterparty(string counterParty)
        {
            await _counterpartyFieldLocator.WaitForAsync();
            await _counterpartyFieldLocator.FillAsync(counterParty, options: new LocatorFillOptions
            {
                Force = true,
            });
            await _basePageObject.PressEnter();
            await _counterpartyFieldLocator.FocusAsync();
            await _basePageObject.PressArrowDown();
            await _basePageObject.PressEnter();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////Screen Final Spread ////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<string> GetCaseID()
        {
            var locatorCaseID = pageIpage1.Locator("//span[@class='workarea_header_id']");

            // var locatorCaseID = PageIpage1.Locator("div[class='content-item content-field item-1 remove-top-spacing remove-bottom-spacing remove-left-spacing'] span[class='case_id']");
            await locatorCaseID.WaitForAsync();
            var caseNumberFully = await locatorCaseID.TextContentAsync();
            if (caseNumberFully is not null)
            {
                var caseNumber = caseNumberFully.Remove(0, 1).Remove(caseNumberFully.Length - 2);
                return caseNumber;
            }
            else
            {
                throw new NullReferenceException("caseNumberFully appears to be null, call origin from GetCaseID()");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////// Offer data /////// CASE LEVEL//////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task SetCreditLimit(string setCreditLimit)
        {
            var creditLimitLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Credit limit*" });
            await creditLimitLocator.FillAsync(setCreditLimit);
            await _basePageObject.PressTab();
        }

        public async Task SetOutstandingPrincipleAmount(string setPrincipleAmount)
        {
            var principleAmountLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Outstanding Principle Amount" });
            await principleAmountLocator.FillAsync(setPrincipleAmount);
        }

        public async Task SetCounterpartyUpdateComment(string updateComment)
        {
            var counterpartyUpdateCommentLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Counterparty update comment*" });
            await counterpartyUpdateCommentLocator.FillAsync(updateComment);
        }

        public async Task SetCDDReviewDate(string setCDDDate)
        {
            var cDDReviewDateLocator = pageIpage1.GetByLabel("CDD review date");
            do
            {
                await cDDReviewDateLocator.WaitForAsync();
                await cDDReviewDateLocator.FillAsync(setCDDDate);
                await _basePageObject.PressTab();
            }
            while (await cDDReviewDateLocator.InputValueAsync() == null ||
          await cDDReviewDateLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetCDDCategory(EnumsForTRX.CDDcategory options)
        {
            string optionText = options.ToString();
            var cDDReviewDateLocator = pageIpage1.GetByLabel("CDD category");
            await cDDReviewDateLocator.SelectOptionAsync(new[] { optionText });
        }

        public Task<bool> GuarantorCheckboxStatus() => pageIpage1.GetByLabel("Guarantor needed?").IsCheckedAsync();

        public Task<bool> CheckIfGuarantorFieldIsAlreadyPresent()
        {
            var boolIsvisible = pageIpage1.Locator("input[name=\"$PpyWorkPage$pOffer$pGuarantors$l1$pGuarantor\"]").IsVisibleAsync();
            return boolIsvisible;
        }

        public async Task DeleteGuarantorField()
        {
            var guarantorFieldLocator = pageIpage1.Locator("[data-test-id=\"202404241624350497903\"] [data-test-id=\"201507160305400976873\"]");
            await guarantorFieldLocator.ClickAsync();
        }

        public async Task CheckGuarantorBox()
        {
            await pageIpage1.GetByLabel("Guarantor needed?").WaitForAsync();
            await pageIpage1.GetByLabel("Guarantor needed?").CheckAsync();
        }

        public async Task UncheckGuarantorBox()
        {
            await pageIpage1.GetByLabel("Guarantor needed?").WaitForAsync();
            await pageIpage1.GetByLabel("Guarantor needed?").UncheckAsync();
        }

        public Task CheckAdditionalEmailBox() => pageIpage1.GetByLabel("Additional email needed?").CheckAsync();

        public Task UnCheckAdditionalEmailBox() => pageIpage1.GetByLabel("Additional email needed?").UncheckAsync();

        public async Task ClickAddGuarantorButton(int? clickCountGuarantor = null)
        {
            int clicks = clickCountGuarantor ?? 1;
            var guarantorButtonLocator = pageIpage1.Locator("[data-test-id=\"202404241624350497903\"] [data-test-id=\"2015071603054009744126\"]");
            await guarantorButtonLocator.ClickAsync();
        }

        public async Task ClickAddEmailButton(int? clickCountEmail = null)
        {
            int clicks = clickCountEmail ?? 1;
            var addEmailButton = pageIpage1.Locator("[data-test-id=\"20240411132713001973\"] [data-test-id=\"2015071603054009744126\"]");
            await addEmailButton.ClickAsync(new LocatorClickOptions { Delay = 200, ClickCount = clicks });
        }

        public async Task SetGuarantorField(string guarantorOne, int? indexOfGuarantor = null)
        {
            int indexGuarantor = indexOfGuarantor ?? 1;
            _scenarioContext["savedGuarantor1"] = guarantorOne;

            var guarantorFieldLocator = pageIpage1.Locator($"[data-test-id=\"202404241624350497903-R{indexGuarantor}\"] [data-test-id=\"2016072109335505834280\"]");
            await guarantorFieldLocator.FillAsync(guarantorOne);

            await _basePageObject.PressArrowDown();
            await pageIpage1.GetByRole(AriaRole.Cell, new() { Name = guarantorOne, Exact = true }).GetByText(guarantorOne).First.ClickAsync();
        }

        public async Task ClickGuarantorField(int? indexOfGuarantor = null)
        {
            int indexGuarantor = indexOfGuarantor ?? 1;
            var guarantorFieldLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pGuarantors$l{indexGuarantor}$pGuarantor\"]");
            await guarantorFieldLocator.ClickAsync();
        }

        public async Task ClearGuarantorField(int? indexOfGuarantor = null)
        {
            int indexGuarantor = indexOfGuarantor ?? 1;
            var guarantorFieldLocator = pageIpage1.Locator($"input[name=\\\"$PpyWorkPage$pOffer$pGuarantors$l{indexGuarantor}$pGuarantor\"]").First;

            await guarantorFieldLocator.WaitForAsync();
            await Task.Delay(1000);
            await guarantorFieldLocator.SelectTextAsync();
            await Task.Delay(1000);
            await _basePageObject.PressBackSpace();
            await Task.Delay(2000);
        }

        public async Task SetGuarantPercentage(string guarantorPercentage, int? indexOfGuarantorPercentage = null)
        {
            int indexGuarantorPercentage = indexOfGuarantorPercentage ?? 1;
            var guarantorPercentageLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pGuarantors$l{indexGuarantorPercentage}$pGuarantyPercentage\"]");
            await guarantorPercentageLocator.FillAsync(guarantorPercentage);
        }

        public async Task OpenEmailSection()
        {
            var guarantorPercentageLocator = pageIpage1.GetByRole(AriaRole.Heading, new() { Name = "Additional email addresses", Exact = true });
            await guarantorPercentageLocator.ClickAsync();
        }

        public async Task SetEmailAddress(string setEmailAddress, int? indexOfEmailAddress = null)
        {
            int indexOfEmail = indexOfEmailAddress ?? 1;
            var guarantorPercentageLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAdditionalEmailList$l{indexOfEmail}$pContactString\"]");
            await guarantorPercentageLocator.FillAsync(setEmailAddress);
        }

        public async Task SpreadCheckBy(bool spreadcheck)
        {
            _scenarioContext["savedSpreadCheck"] = spreadcheck;
            var colleagueCheck = pageIpage1.GetByRole(AriaRole.Checkbox, new() { Name = "Colleague check needed" });

            await (spreadcheck
                ? colleagueCheck.CheckAsync()
                : colleagueCheck.UncheckAsync());
        }

        public Task SpreadCheckSetRemarks(string remarks) => pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Remarks" }).FillAsync(remarks);
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////// Offers  ///////////////OFFER LEVEL/////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task ClickAddOffer(int? offerNumbInCase = null)
        {
            int caseOffer = offerNumbInCase ?? 1;

            var addOfferButtonLocator = pageIpage1.Locator($"button[name='OfferOptions_pyWorkPage.Offers({caseOffer})_2'] i[class='pi pi-copy']");
            await addOfferButtonLocator.FocusAsync();
            await addOfferButtonLocator.ClickAsync();
        }

        public async Task ClickSaveOffer()
        {
            var saveButtonLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Save" });
            await saveButtonLocator.FocusAsync();
            await saveButtonLocator.ClickAsync();
        }

        public async Task ClickRefresh()
        {
            var saveButtonLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Refresh" }).First;
            await saveButtonLocator.FocusAsync();
            await saveButtonLocator.ClickAsync();
        }

        public async Task ClickDeleteOffer()
        {
            var saveButtonLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Delete offer" });
            await saveButtonLocator.FocusAsync();
            await saveButtonLocator.ClickAsync();
        }

        public async Task ClickAddAlternative(int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var saveButtonLocator = pageIpage1.Locator($"button[name=\"OptionHeader_pyWorkPage.Offer.AlternativeProductList({altOffer})_3\"]");
            await saveButtonLocator.FocusAsync();
            await saveButtonLocator.ClickAsync();
        }

        public async Task ClickOfferReference()
        {
            var offerReferenceLocator = pageIpage1.Locator("[id=\"\\35 4415998\"]");
            await offerReferenceLocator.ClickAsync();
        }

        public async Task SetNotional(string notional1, int? offerNumbInCase = null)
        {
            int offerCase = offerNumbInCase ?? 1;

            var notionalLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Notional*" });
            await notionalLocator.ClearAsync();
            await notionalLocator.FillAsync(notional1);

            // TODO: method parameter can be removed.
        }

        public async Task ClearNotional(int? offerNumbInCase = null)
        {
            var notionalLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Notional*" });
            await notionalLocator.ClearAsync();
        }

        public async Task<string> GetMinimalWithdrawalPercentageValue() => await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Minimum withdrawal (%)" }).InputValueAsync();

        public async Task SetMinimalWithdrawalPercent(string setMinimalWithdrawal, int? offerNumbInCase = null)
        {
            var minimalWithdrawalLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Minimum withdrawal (%)" });
            await minimalWithdrawalLocator.FocusAsync();
            await minimalWithdrawalLocator.FillAsync(setMinimalWithdrawal);
        }

        public async Task SetCommitmentFee(string setCommitmentFee, int? offerNumbInCase = null)
        {
            var setCommitmentFeeLocator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Commitment fee" });
            await setCommitmentFeeLocator.FocusAsync();
            await setCommitmentFeeLocator.FillAsync(setCommitmentFee);
        }

        public async Task<string> GetMinimalWithdrawalAmount()
        {
            var minimalwithdrawalLocator = pageIpage1.Locator("//div[contains(@class,'content-item content-layout item-2 remove-right-spacing flex flex-row')]//span//span[1]");

            await minimalwithdrawalLocator.WaitForAsync();
            var minimalWithdrawalAmount = await minimalwithdrawalLocator.TextContentAsync();
            if (minimalWithdrawalAmount is not null)
            {
                return minimalWithdrawalAmount;
            }
            else
            {
                throw new Exception("GetMinimalWithDrawalAmount is null!");
            }
        }

        public async Task SetDealMoment(string dealMoment, int? offerNumbInCase = null)
        {
            var dealMoment1Locator = pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Deal moment*" });
            await dealMoment1Locator.FillAsync(dealMoment);

            // TODO: remove parameter
        }

        public async Task SetNotificationMinutes(string setNotifcationMinutes, int? offerNumbInCase = null)
        {
            var notificationMinutesLocator = pageIpage1.GetByLabel("Notification (minutes)");
            await notificationMinutesLocator.SelectOptionAsync(new[] { setNotifcationMinutes });

            // TODO: remove parameter.
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////// Alternative  ///////////STANDARD FIELDS ////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task ClickCopyAlternative(int numberOfClicks)
        {
            var copyButtonLocator = pageIpage1.Locator("button[name=\"OptionHeader_pyWorkPage.Offer.AlternativeProductList(1)_3\"]");

            await copyButtonLocator.ClickAsync(new LocatorClickOptions
            {
                ClickCount = numberOfClicks,
                Delay = 100,
            });
        }

        public async Task ClickDeleteAlternative(int numberOfClicks, int? altNumbInCase = null)
        {
            int altCase = altNumbInCase ?? 1;
            var refreshButtonLocator = pageIpage1.Locator($"button[name=\"OptionHeader_pyWorkPage.Offer.AlternativeProductList({altCase})_4\"]");
            await refreshButtonLocator.ClickAsync(new LocatorClickOptions
            {
                ClickCount = numberOfClicks,
            });
        }

        public async Task SetStartDate(string startdate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var setStartDateLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pStartDate\"]");

            await setStartDateLocator.WaitForAsync();
            await setStartDateLocator.FocusAsync();
            await setStartDateLocator.FillAsync(startdate);
            await _basePageObject.PressTab();
            await this.page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(1500);
        }

        public async Task SetMaturityDate(string maturityDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var maturityDateLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pMaturityDate\"]");
            do
            {
                await maturityDateLocator.WaitForAsync();
                await maturityDateLocator.FocusAsync();
                await maturityDateLocator.ClickAsync();
                await maturityDateLocator.FillAsync(maturityDate);
                await _basePageObject.PressTab();
            }
            while (await maturityDateLocator.InputValueAsync() == null ||
           await maturityDateLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetFirstCouponDate(string firstCouponDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var firstCouponDate1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pFirstCouponDate\"]");
            await firstCouponDate1Locator.FillAsync(firstCouponDate);
        }

        public async Task SetLastCouponDate(string lastCouponDate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var lastCouponDate1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pLastCouponStartDate\"]");
            await lastCouponDate1Locator.FillAsync(lastCouponDate);
        }

        public async Task SetCouponDay(string? setCouponDay, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var couponDayLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCouponDay\"]");
            await couponDayLocator.FocusAsync();
            await couponDayLocator.FillAsync(setCouponDay);
        }

        public async Task SetCouponDayOptionSet(CouponDay? options, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var couponDayLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCouponDayOfWeek\"]");
            await couponDayLocator.FocusAsync();
            await couponDayLocator.SelectOptionAsync(new[] { options.ToString() });
        }

        public async Task SetInterestSpreadRRDate(string interestSpreadRRDate1, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;

            var interestSpreadRRDate1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pRateResetDate\"]");
            await interestSpreadRRDate1Locator.ClearAsync();
            await interestSpreadRRDate1Locator.FillAsync(interestSpreadRRDate1);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////// Alternative  ///////////LOAN SPECIFIC FIELDS ////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task SetAmortisationType(string amortisationtype, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var amortisationTypeLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pAmortisationType\"]");
            await amortisationTypeLocator.FocusAsync();
            await amortisationTypeLocator.SelectOptionAsync(new[] { amortisationtype });
        }

        public async Task SetCouponFrequency(string couponfrequency, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var couponFrequencyLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCouponFrequency\"]");
            await couponFrequencyLocator.FocusAsync();
            await couponFrequencyLocator.SelectOptionAsync(new[] { couponfrequency });
        }

        public async Task SetSwap(bool setswap, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var setSwapLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pSwapNeeded\"]");

            await setSwapLocator.FocusAsync();

            await setSwapLocator.SelectOptionAsync(new[] { setswap ? "Yes" : "No" });
            _scenarioContext["savedBoolSwap"] = setswap;
        }

        public async Task ClickCopySwap(int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var swapInfoLocator = pageIpage1.Locator($"[data-test-id=\"202511040850470794955-R{altOfferCase}\"] [data-test-id=\"202511111412170431170\"]");
            await swapInfoLocator.FocusAsync();
            await swapInfoLocator.ClickAsync();
        }

        public async Task<string> CopyDataFromSWAPCopyfunction()
        {
            var locaterUGdata = pageIpage1.Locator("[data-test-id=\"202403181414080952718\"]");
            await locaterUGdata.WaitForAsync();
            var textData = await locaterUGdata.TextContentAsync();
            return textData;
        }

        public Task SetDayCount(string dayCount, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var dayCountLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pDayCountConvention\"]");

            return dayCount switch
            {
                "Act/360" => dayCountLocator.SelectOptionAsync(new[] { "Act/360" }),
                "Act/Act" => dayCountLocator.SelectOptionAsync(new[] { "Act/Act" }),
                "30/360" => dayCountLocator.SelectOptionAsync(new[] { "30/360" }),
                _ => throw new Exception("Unknown option chosen for method public Task SetDayCount2 method"),
            };
        }

        public async Task SetNumberOfAmortisations(string numberofamortisations, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var amortisationFrequencyLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pAmortisation\"]");
            await amortisationFrequencyLocator.FillAsync(numberofamortisations);
        }

        public Task SetAmortisationFrequency(string amortisationfrequency, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var amortisationFrequencyLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOfferCase}$pAmortisationFrequency\"]");

            return amortisationfrequency switch
            {
                "Annual" => amortisationFrequencyLocator.SelectOptionAsync(new[] { "Annual" }),
                "Semi annual" => amortisationFrequencyLocator.SelectOptionAsync(new[] { "Semi annual" }),
                "Quarterly" => amortisationFrequencyLocator.SelectOptionAsync(new[] { "Quarterly" }),
                "Monthly" => amortisationFrequencyLocator.SelectOptionAsync(new[] { "Monthly" }),
                "Weekly" => amortisationFrequencyLocator.SelectOptionAsync(new[] { "Weekly" }),
                _ => throw new Exception("Unknown option chosen for method public Task SetAmortisationFrequency method"),
            };
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////// Alternative  ///////////SPREAD FIELDS ////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task SetSpreadH1(string setH1spread, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH1FieldLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH1$pRate\"]");
            await spreadH1FieldLocator.FocusAsync();
            await spreadH1FieldLocator.FillAsync(setH1spread);
            await _basePageObject.PressTab();
            await Assertions.Expect(spreadH1FieldLocator).ToHaveValueAsync(setH1spread, new LocatorAssertionsToHaveValueOptions { Timeout = 10000 });
        }

        public async Task SetSpreadH2(string setH2spread, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH2FieldLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH2$pRate\"]");
            await spreadH2FieldLocator.FillAsync(setH2spread);
        }

        public async Task SetSpreadH1Comment(string setH1Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH1FieldLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH1$pComment\"]");
            await spreadH1FieldLocator.FillAsync(setH1Comment);
        }

        public async Task SetSpreadH2Comment(string setH2Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH2FieldLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH2$pComment\"]");
            await spreadH2FieldLocator.FillAsync(setH2Comment);
        }

        public async Task OverwriteSpreadMisMatch(int? totalAltSections = null)
        {
            int totalAltSectionsInForm = totalAltSections ?? 0;

            var spreadMisMatchCheckBoxLocator = pageIpage1.Locator("[data-test-id=\"20230503152239070053\"]");

            await spreadMisMatchCheckBoxLocator.FocusAsync();
            await spreadMisMatchCheckBoxLocator.CheckAsync();
        }

        public async Task SetSpreadMisMatchComment(string setSpreadMisMatchComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var setSpreadMisMatchCommentLocator = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-{altOffer}\"] [data-test-id=\"202305031531440948398\"]");

            await setSpreadMisMatchCommentLocator.FocusAsync();
            await setSpreadMisMatchCommentLocator.FillAsync(setSpreadMisMatchComment);
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
        }

        public async Task SetFinalSpread(string setFinalSpread, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSpreadLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadFinal$pRate\"]");
            await finalSpreadLocator.FillAsync(setFinalSpread);
        }

        public async Task SetFinalSpreadComment(string setFinalSpreadComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSpreadCommentLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadFinal$pComment\"]");
            await finalSpreadCommentLocator.FillAsync(setFinalSpreadComment);
        }

        public async Task SetMarketRate(string setMarketRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setMarketRate1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pMarketInterestRateData$pRate\"]");
            do
            {
                await setMarketRate1Locator.WaitForAsync();
                await setMarketRate1Locator.FocusAsync();
                await setMarketRate1Locator.FillAsync(setMarketRate);
                await _basePageObject.PressTab();
            }
            while (await setMarketRate1Locator.InputValueAsync() == null || await setMarketRate1Locator.InputValueAsync() == string.Empty);
        }

        public async Task SetReferenceRate(string setReferenceRate, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setRefRateLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pReferenceRate\"]");
            await setRefRateLocator.FocusAsync();
            await setRefRateLocator.SelectOptionAsync([setReferenceRate]);
        }

        public async Task SetSpreadH1Basis(string setH1Euribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setEuriborH1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH1$pBasisSwap\"]");
            await setEuriborH1Locator.FocusAsync();
            await setEuriborH1Locator.FillAsync(setH1Euribor);
        }

        public async Task SetSpreadH2Basis(string setH2Euribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setEuriborH2Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadH2$pBasisSwap\"]");
            await setEuriborH2Locator.FocusAsync();
            await setEuriborH2Locator.FillAsync(setH2Euribor);
        }

        public async Task SetFinalSpreadBasis(string setFinalSpreadEuribor, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setFinalSpreadEuriborLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSpreadFinal$pBasisSwap\"]");
            await setFinalSpreadEuriborLocator.FocusAsync();
            await setFinalSpreadEuriborLocator.FillAsync(setFinalSpreadEuribor);
        }

        public async Task Set48HoursClause(bool? set48HoursClause, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            if (set48HoursClause is not null)
            {
                await pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pHours48Clause\"]").FocusAsync();
                await pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pHours48Clause\"]").SelectOptionAsync(new[] { (bool)set48HoursClause ? "Yes" : "No" });
            }
            else
            {
                throw new NullReferenceException("Set48HoursClause(): Seems that parameter set48HoursClause is null!");
            }
        }

        public async Task SetCustomerRateH1(string setCustomerRateH1, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setCustomerRateH1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateH1$pRate\"]");
            await setCustomerRateH1Locator.FocusAsync();
            await setCustomerRateH1Locator.FillAsync(setCustomerRateH1);
            await _basePageObject.PressTab();
            // $PpyWorkPage$pOffer$pAlternativeProductList$l1$pCustomerRateH1$pRate
        }

        public async Task SetCustomerRateH2(string setCustomerRateH2, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setCustomerRateH2Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateH2$pRate\"]");
            await setCustomerRateH2Locator.FocusAsync();
            await setCustomerRateH2Locator.FillAsync(setCustomerRateH2);
            await _basePageObject.PressTab();
        }

        public async Task SetCustomerH1Comment(string setCustomerH1Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var customerH1CommentLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateH1$pComment\"]");
            await customerH1CommentLocator.FocusAsync();
            await customerH1CommentLocator.FillAsync(setCustomerH1Comment);
            await _basePageObject.PressTab();

            // $PpyWorkPage$pOffer$pAlternativeProductList$l1$pCustomerRateH1$pComment
        }

        public async Task SetCustomerH2Comment(string setCustomerH2Comment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var customerH2CommentLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateH2$pComment\"]");

            await customerH2CommentLocator.FocusAsync();
            await customerH2CommentLocator.FillAsync(setCustomerH2Comment);
            await _basePageObject.PressTab();
        }

        public async Task SetSpreadMisMatchCustomerRateComment(string setSpreadMisMatchCustomerRateComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadMismatchCustomerRateLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pOverwriteMismatchedRateCommentError\"]");
            await spreadMismatchCustomerRateLocator.FocusAsync();
            await spreadMismatchCustomerRateLocator.FillAsync(setSpreadMisMatchCustomerRateComment);
            await _basePageObject.PressTab();
        }

        public async Task SetFinalCustomerRate(string setFinalCustomerRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalCustomerRateLocator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateFinal$pRate\"]");
            await finalCustomerRateLocator.FocusAsync();
            await finalCustomerRateLocator.FillAsync(setFinalCustomerRate);
            await _basePageObject.PressTab();
        }

        public async Task SetFinalCustomerRateComment(string setFinalCustomerRateComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalCustomerRateCommentLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pCustomerRateFinal$pComment\"]");
            await finalCustomerRateCommentLocator.FocusAsync();
            await finalCustomerRateCommentLocator.FillAsync(setFinalCustomerRateComment);
        }

        public async Task SetMargin(string setMargin, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            await Task.Delay(2000);
            var setMarketRate1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pMarginRate$pRate\"]");
            await setMarketRate1Locator.WaitForAsync();

            await setMarketRate1Locator.FillAsync(setMargin);
        }

        public async Task SetMarginComment(string setMarginComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSpreadCommentLocator = pageIpage1.Locator($"textarea[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pMarginRate$pComment\"]");
            await finalSpreadCommentLocator.FillAsync(setMarginComment);
        }

        public async Task SetSwapReferenceRate(string setSwapReferenceRate, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var swapReferenceLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSwapRequestInformation$pReferenceRate\"]");
            await swapReferenceLocator.SelectOptionAsync(new[] { setSwapReferenceRate });
        }

        public async Task SetClearingHouse(string setClearingHouse, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var clearingHouseLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSwapRequestInformation$pClearingHouse\"]");
            await clearingHouseLocator.SelectOptionAsync(new[] { setClearingHouse });
        }

        public async Task SetBusinessDayConvention(string setBusinessDayConvention, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var businessDayConventionLocator = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pSwapRequestInformation$pBusinessDayConvention\"]");
            var choseOption = setBusinessDayConvention switch
            {

                "Mod Foll Unadj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Modified Following Unadjusted" }),
                "Mod Foll Adj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Modified Following Adjusted" }),
                "Foll Unadj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Following Unadjusted" }),
                "Foll Adj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Following Adjusted" }),
                _ => throw new Exception("Unknown option chosen for switch expression counterParty switch,origin: GetRandomindividual()"),
            };

            await choseOption;
        }

        public async Task ClickSaveH1Button(int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var h1ButtonLocator = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-{altOffer}\"] [data-test-id=\"H1Confirm\"]");

            await h1ButtonLocator.FocusAsync();
            await h1ButtonLocator.ClickAsync();
        }

        public async Task ClickSaveH2Button(int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            // SHORT LOAN template
            var h2ButtonLocator = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-{altOffer}\"] [data-test-id=\"H2Confirm\"]");

            // data-test-id="20251023162812026116"
            await h2ButtonLocator.FocusAsync();
            await h2ButtonLocator.ClickAsync();
        }

        public async Task ClickSaveFinalSpreadButton(int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSaveButtonLocator = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-{altOffer}\"] [data-test-id=\"FinalConfirm\"]");

            await finalSaveButtonLocator.FocusAsync();
            await finalSaveButtonLocator.ClickAsync();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////// END SPREAD SECTION /////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task ClickSaveMarketRate(int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var saveMarketRateButtonLocator1 = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-{altOffer}\"] [data-test-id=\"MarketInterestConfirm\"]");
            await saveMarketRateButtonLocator1.WaitForAsync();
            await saveMarketRateButtonLocator1.FocusAsync();
            await saveMarketRateButtonLocator1.ClickAsync();
        }

        public async Task ClickSaveMargin()
        {
            var newSaveMarginLocator = pageIpage1.Locator("button[name=\"MarginRate_pyWorkPage.Offer.AlternativeProductList(1).MarginRate_6\"]");
            await newSaveMarginLocator.FocusAsync();
            await newSaveMarginLocator.ClickAsync();
        }

        public async Task ClickSaveBaseRate()
        {
            await Task.Delay(400);

            var baseRateLocator = pageIpage1.Locator($"[data-test-id=\"202208041643100167106-1\"] [data-test-id=\"MarketInterestConfirm\"]");
            await baseRateLocator.FocusAsync();
            await baseRateLocator.ClickAsync();
            await Task.Delay(400);
            await _basePageObject.PageWaitForUrlLoaded();
        }

        public async Task ClickSaveMarketRate2()
        {
            // await Task.Delay(400);
            var saveMarketRateButtonLocator1 = pageIpage1.Locator("fieldset[name=\"main\"] #pyFlowActionHTML a").Nth(1);

            // var SaveMarketRateButtonLocator1 = PageIpage1.Locator("xpath=/html[1]/body[1]/div[2]/form[1]/div[3]/div[1]/section[1]/div[1]/span[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/span[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[5]/div[1]/div[2]/div[1]/div[1]/div[4]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/span[1]/a[1]/i[1]");
            // var SaveMarketRateButtonLocator1 = PageIpage1.Locator(" (//i[contains(@class,'pi pi-document-check-solid')])[2]");

            // var waitForRightScreenTextOpenAssigments = PageIpage1.GetByText("Open assignments", new() { Exact = true });
            await saveMarketRateButtonLocator1.FocusAsync();
            await saveMarketRateButtonLocator1.ClickAsync();
            await _basePageObject.WaitForResponse();

            // await Task.Delay(400);
            //  await _basePageObject.PageWaitForUrlLoaded();
        }

        public Task CheckIncludeInMailBox() => _includeInMailBoxLocator.First.CheckAsync();

        public Task CheckIncludeRemarkInMailBox() => pageIpage1.Locator("[id=\"\\39 feeefcc\"]").UncheckAsync();

        public Task<ILocator> GetIncludeInMailBoxLocator()
        {
            var locator = _includeInMailBoxLocator;
            return Task.FromResult(locator);
        }

        public async Task SetDealRemark(string dealremark, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            // int caseOffer = offerNumbInCase ?? 1;
            //  int altOffer = alternativeNumbIncase ?? 1;
            //  var DealRemark1Locator = PageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffers\\$l{caseOffer}\\$pAlternatives\\$l{altOffer}\\$pRemarkDeal\"]");
            //  do
            //  {
            //      await DealRemark1Locator.FillAsync(dealremark);
            //      await _basePageObject.PressTab();
            //  }
            //  while (await DealRemark1Locator.InputValueAsync() == null ||
            // await DealRemark1Locator.InputValueAsync() == string.Empty);
            int altOffer = alternativeNumbIncase ?? 1;
            var dealRemark1Locator = pageIpage1.Locator($"input[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l{altOffer}$pRemarkDeal\"]");
            await dealRemark1Locator.FocusAsync();
            await dealRemark1Locator.FillAsync(dealremark);
        }

        public async Task ClickSubmitFinalSpreadPage()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });

            // await Task.Delay(500);
            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync();

            // await SubmitButton.ClickAsync(new() { Force = false, Delay = 100, });
            await page.WaitForURLAsync("**pega.net/**", new PageWaitForURLOptions
            {
                WaitUntil = WaitUntilState.Commit,
            });
        }

        public async Task ClickSubmitFinalSpreadPageClosedOff()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });

            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync(new() { Force = false, Delay = 100, });
        }

        public async Task SaveAndCloseFinalSpread()
        {
            await Task.Delay(250);
            var otherActionsLocator = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Other actions" });
            var saveAndCloseLocator = pageIpage1.GetByRole(AriaRole.Menuitem, new() { Name = "Save and close" });

            await otherActionsLocator.WaitForAsync();
            await otherActionsLocator.ClickAsync();
            page.Dialog += async (_, dialog) =>
            {
                await dialog.DismissAsync();
            };

            await saveAndCloseLocator.WaitForAsync();
            await saveAndCloseLocator.ClickAsync();
            await _basePageObject.PageWaitForUrlLoaded();
        }

        public Task ClickProfileMenu() => page.Locator("[data-test-id=\"px-opr-image-ctrl\"]").ClickAsync();

        public Task ClickLogOut() => page.GetByRole(AriaRole.Menuitem, new() { Name = "Log out" }).ClickAsync(new LocatorClickOptions
        {
            Delay = 200,
        });

        ///////////////////////////////////////
        // HIGH NOTIONAL SECTION //
        ///////////////////////////////////////

        public async Task HighNotionalDealApproved(bool setApproval)
        {
            if (setApproval == true)
            {
                await pageIpage1.GetByRole(AriaRole.Radiogroup).GetByText("Yes").WaitForAsync();
                await pageIpage1.GetByRole(AriaRole.Radiogroup).GetByText("Yes").ClickAsync();
            }
            else if (setApproval == false)
            {
                await pageIpage1.GetByRole(AriaRole.Radiogroup).GetByText("No").WaitForAsync();
                await pageIpage1.GetByRole(AriaRole.Radiogroup).GetByText("No").ClickAsync();
            }
        }

        public async Task SetNegativeInterestRate(bool negativeInterestRate)
        {
            if (negativeInterestRate == false)
            {
                await pageIpage1.GetByLabel("Allow negative interest").UncheckAsync();
            }

            _scenarioContext["savedNegativeInterestRate"] = negativeInterestRate;
        }

        public async Task HighNotionalProcurationMail()
        {
            // updated locator because text on button changed in new class structure (1.85):
            // await PageIpage1.GetByRole(AriaRole.Button, new() { Name = "Save and ask approval through email" }).FocusAsync();
            await pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Save and ask approval through email" }).ClickAsync();
        }

        public async Task HighNotionalCheckOverwrite()
        {
            await pageIpage1.GetByRole(AriaRole.Checkbox, new() { Name = "Overwrite?" }).FocusAsync();
            await pageIpage1.GetByRole(AriaRole.Checkbox, new() { Name = "Overwrite?" }).CheckAsync();
        }

        public async Task HighNotionalUncheckOverwrite()
        {
            await pageIpage1.GetByRole(AriaRole.Checkbox, new() { Name = "Overwrite?" }).FocusAsync();
            await pageIpage1.GetByRole(AriaRole.Checkbox, new() { Name = "Overwrite?" }).UncheckAsync();
        }

        public async Task HighNotionalSetReasonForOverwite(string overWriteReasonMessage)
        {
            await pageIpage1.GetByLabel("Reason For Overwrite", new() { Exact = true }).FocusAsync();
            await pageIpage1.GetByLabel("Reason For Overwrite", new() { Exact = true }).FillAsync(overWriteReasonMessage);
        }

        public async Task HighNotionalSetNameOfApprover(string setNameOfApprover)
        {
            await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Name of the person who" }).FocusAsync();
            await pageIpage1.GetByRole(AriaRole.Textbox, new() { Name = "Name of the person who" }).FillAsync(setNameOfApprover);
        }
    }
}