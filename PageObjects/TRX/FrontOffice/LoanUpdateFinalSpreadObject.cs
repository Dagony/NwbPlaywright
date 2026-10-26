using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.PageObjects
{
    public interface ILoanUpdateFinalSpreadObject
    {
        Task CheckIncludeInMailBox();

        Task CheckIncludeRemarkInMailBox();

        Task ClearGuarantorField(int? indexOfGuarantor = null);

        Task ClickAddEmailButton(int? clickCountEmail = null);

        Task ClickAddGuarantorButton(int? clickCountGuarantor = null);

        Task ClickSaveFinalSpreadButton(int? alternativeNumbIncase = null);

        Task ClickSaveH1Button(int? alternativeNumbIncase = null);

        Task ClickSaveH2Button(int? alternativeNumbIncase = null);

        Task ClickSaveMarketRate(int? alternativeNumbIncase = null);

        Task ClickSubmitFinalSpreadPage();

        Task CloseExtraEmailSection();

        Task CloseGuarantorSection();

        Task<ILocator> GetIncludeInMailBoxLocator();

        Task OpenExtraEmailSection();

        Task OpenGuarantorSection();

        Task<string> GetValueOfCouponFrequency();

        Task<string> GetMinimalWithdrawalAmount();

        Task OverwriteSpreadMisMatch(int? totalAltSections = null);

        Task Set48HoursClause(bool? set48HoursClause);

        Task SetAmortisationFrequency(string amortisationfrequency, int? alternativeNumbIncase = null);

        Task SetAmortisationType(string amortisationtype, int? alternativeNumbIncase = null);

        Task SetCDDCategory(CDDcategory options);

        Task SetCDDReviewDate(string setCDDDate);

        Task SetCommitmentFee(string setCommitmentFee, int? offerNumbInCase = null);

        Task SetCouponDay(string? setCouponDay, int? alternativeNumbIncase = null);

        Task SetCouponDayOptionSet(CouponDay? options, int? alternativeNumbIncase = null);

        Task SetCouponFrequency(string couponfrequency, int? alternativeNumbIncase = null);

        Task SetCreditLimit(string setCreditLimit);

        Task SetDayCount(string dayCount, int? alternativeNumbIncase = null);

        Task SetDealMoment(string dealMoment);

        // deprecated in new class structure.
        // Task ClickCopySwap();
        Task<bool> GetGuarantorSectionStatus();

        Task SetDealRemark(string dealremark, int? alternativeNumbIncase = null);

        Task SetEmailAddress(string setEmailAddress, int? indexOfEmailAddress = null);

        Task SetFinalSpread(string setFinalSpread, int? alternativeNumbIncase = null);

        Task SetFinalSpreadBasis(string setFinalSpreadEuribor, int? alternativeNumbIncase = null);

        Task SetFinalSpreadComment(string setFinalSpreadComment, int? alternativeNumbIncase = null);

        Task SetFirstCouponDate(string firstCouponDate, int? alternativeNumbIncase = null);

        Task SetGuarantorField(string guarantorOne, int? indexOfGuarantor = null);

        Task SetGuarantPercentage(string guarantorPercentage, int? indexOfGuarantorPercentage = null);

        Task SetInterestSpreadRRDate(string interestSpreadRRDate1, int? alternativeNumbIncase = null);

        Task SetLastCouponDate(string lastCouponDate, int? alternativeNumbIncase = null);

        Task SetMarketRate(string setMarketRate, int? alternativeNumbIncase = null);

        Task SetMaturityDate(string maturityDate, int? alternativeNumbIncase = null);

        Task SetMinimalWithdrawalPercent(string setMinimalWithdrawal, int? offerNumbInCase = null);

        Task SetNotificationMinutes(string setNotifcationMinutes);

        Task SetNotional(string notional);

        Task SetNumberOfAmortisations(string numberofamortisations, int? alternativeNumbIncase = null);

        // Task SetOfferReference(string offerreference, int? alternativeNumbIncase = null);
        Task SetOutstandingPrincipleAmount(string setPrincipleAmount);

        Task SetReferenceRate(string setReferenceRate, int? alternativeNumbIncase = null);

        Task SetSpreadH1(string setH1spread, int? alternativeNumbIncase = null);

        Task SetSpreadH1Basis(string setH1Euribor, int? alternativeNumbIncase = null);

        Task SetSpreadH1Comment(string setH1Comment, int? alternativeNumbIncase = null);

        Task SetSpreadH2(string setH2spread, int? alternativeNumbIncase = null);

        Task SetSpreadH2Basis(string setH2Euribor, int? alternativeNumbIncase = null);

        Task SetSpreadH2Comment(string setH2Comment, int? alternativeNumbIncase = null);

        Task SetStartDate(string startdate, int? alternativeNumbIncase = null);

        Task SetSwap(bool setswap, int? alternativeNumbIncase = null);

        Task SpreadCheckBy(bool spreadcheck);

        Task SpreadCheckSetRemarks(string remarks);

        Task<string> GetMaximumWithdrawalAmount();

        Task SetSpreadMisMatchComment(string setSpreadMisMatchComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null);

        Task SetSwapReferenceRate(string setSwapReferenceRate, int? alternativeNumbIncase = null);

        Task SetClearingHouse(string setClearingHouse, int? alternativeNumbIncase = null);

        Task SetBusinessDayConvention(string setBusinessDayConvention, int? alternativeNumbIncase = null);

        Task SetMargin(string setMargin, int? alternativeNumbIncase = null);

        Task SetMarginComment(string setMarginComment, int? alternativeNumbIncase = null);

        Task ClickSaveMargin(int? alternativeNumbIncase = null);
    }

    public class LoanUpdateFinalSpreadObject : ILoanUpdateFinalSpreadObject
    {
        public LoanUpdateFinalSpreadObject(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IBasePageObject basePageObject, ScenarioContext scenarioContext)
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

        public async Task SetCreditLimit(string setCreditLimit)
        {
            var creditLimitLocator = pageIpage1.GetByLabel("Credit limit");
            await creditLimitLocator.ClearAsync();
            await creditLimitLocator.FillAsync(setCreditLimit);
            await _basePageObject.PressTab();
        }

        public async Task SetOutstandingPrincipleAmount(string setPrincipleAmount)
        {
            var principleAmountLocator = pageIpage1.GetByLabel("Outstanding principle amount");
            await principleAmountLocator.FillAsync(setPrincipleAmount);
        }

        public async Task SetCDDCategory(EnumsForTRX.CDDcategory options)
        {
            string optionText = options.ToString();
            var cDDReviewDateLocator = pageIpage1.GetByLabel("CDD category");
            await cDDReviewDateLocator.SelectOptionAsync(new[] { optionText });
        }

        public async Task<bool> GetGuarantorSectionStatus()
        {
            var locatorGuarantorText = pageIpage1.GetByText("Guaranty percentage");
            var isGuarantorTextVisible = await locatorGuarantorText.IsVisibleAsync();
            return isGuarantorTextVisible;
        }

        public async Task SetCDDReviewDate(string setCDDDate)
        {
            var loanType = (string)_scenarioContext["savedLoanType"];
            var cDDReviewDateLocator = pageIpage1.GetByLabel("CDD review date");
            await cDDReviewDateLocator.ClickAsync();
            await cDDReviewDateLocator.FillAsync(setCDDDate);
        }

        public async Task OpenGuarantorSection()
        {
            await pageIpage1.GetByLabel("Disclose Guarantors").First.WaitForAsync();
            await pageIpage1.GetByLabel("Disclose Guarantors").First.ClickAsync();
            await _basePageObject.DebounceDom(pageIpage1.GetByLabel("Hide Guarantors").First);
        }

        public async Task CloseGuarantorSection()
        {
            await pageIpage1.GetByLabel("Hide Guarantors").First.WaitForAsync();
            await pageIpage1.GetByLabel("Hide Guarantors").First.ClickAsync();
            await _basePageObject.DebounceDom(pageIpage1.GetByLabel("Disclose Guarantors").First);
        }

        public async Task OpenExtraEmailSection()
        {
            await _basePageObject.DebounceDom(pageIpage1.GetByLabel("Disclose Additional email").First);
            await pageIpage1.GetByLabel("Disclose Additional email").First.ClickAsync();
            await _basePageObject.DebounceDom(pageIpage1.GetByLabel("Hide Additional email").First);
        }

        public async Task CloseExtraEmailSection()
        {
            await pageIpage1.GetByLabel("Hide Additional email").First.WaitForAsync();
            await pageIpage1.GetByLabel("Hide Additional email").First.ClickAsync();
            await _basePageObject.DebounceDom(pageIpage1.GetByLabel("Disclose Additional email").First);
        }

        public async Task ClickAddGuarantorButton(int? clickCountGuarantor = null)
        {
            int clicks = clickCountGuarantor ?? 1;
            var guarantorButtonLocator = pageIpage1.Locator("(//a[contains(@name,'pzPegaDefaultGridIcons_pyWorkPage_4')][normalize-space()='Add item'])[1]");
            await guarantorButtonLocator.ClickAsync(new LocatorClickOptions { Delay = 200, ClickCount = clicks });
        }

        public async Task ClickAddEmailButton(int? clickCountEmail = null)
        {
            int clicks = clickCountEmail ?? 1;

            if (await pageIpage1.GetByLabel("Hide Guarantors").First.IsVisibleAsync() == true)
            {
                var extraEmailButtonLocator = pageIpage1.GetByText("Add item").Nth(1);
                var emailFieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pAdditionalEmailList\\$l1\\$pContactString\"]");

                do
                {
                    await extraEmailButtonLocator.ClickAsync(new LocatorClickOptions { ClickCount = clickCountEmail });
                    await Task.Delay(200);
                }
                while ((await emailFieldLocator.IsVisibleAsync()) == false);
            }
            else if (await pageIpage1.GetByLabel("Hide Guarantors").First.IsVisibleAsync() == false)
            {
                var extraEmailButtonLocator = pageIpage1.GetByText("Add item").First;
                var emailFieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pAdditionalEmailList\\$l1\\$pContactString\"]");
                do
                {
                    await extraEmailButtonLocator.ClickAsync(new LocatorClickOptions { ClickCount = clickCountEmail });
                    await Task.Delay(200);
                }
                while ((await emailFieldLocator.IsVisibleAsync()) == false);
            }
        }

        public async Task SetGuarantorField(string guarantorOne, int? indexOfGuarantor = null)
        {
            int indexGuarantor = indexOfGuarantor ?? 1;
            var guarantorFieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pGuarantorList\\$l{indexOfGuarantor}\\$pLegalName\"]");

            await guarantorFieldLocator.WaitForAsync();
            await guarantorFieldLocator.FillAsync(guarantorOne);
            await _basePageObject.PressEnter();
            await guarantorFieldLocator.FocusAsync();
            await _basePageObject.PressArrowDown();
            await pageIpage1.GetByLabel("Task list menu").GetByText(guarantorOne).ClickAsync();
        }

        public async Task ClearGuarantorField(int? indexOfGuarantor = null)
        {
            int indexGuarantor = indexOfGuarantor ?? 1;
            var guarantorFieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pGuarantorList\\$l{indexGuarantor}\\$pLegalName\"]");

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
            var guarantorPercentageLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pGuarantorList\\$l{indexGuarantorPercentage}\\$pGuaranteePercentage\"]");
            await guarantorPercentageLocator.WaitForAsync();
            await guarantorPercentageLocator.FillAsync(guarantorPercentage);
            await _basePageObject.PressTab();
        }

        public async Task SetEmailAddress(string setEmailAddress, int? indexOfEmailAddress = null)
        {
            int indexOfEmail = indexOfEmailAddress ?? 1;
            var emailFieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pAdditionalEmailList\\$l{indexOfEmail}\\$pContactString\"]");
            await emailFieldLocator.WaitForAsync();
            await emailFieldLocator.FillAsync(setEmailAddress);
            await _basePageObject.PressTab();
        }

        public async Task SpreadCheckBy(bool spreadcheck)
        {
            await pageIpage1.GetByLabel("Colleague check needed").SelectOptionAsync(new[] { spreadcheck ? "Yes" : "No" });
        }

        public Task SpreadCheckSetRemarks(string remarks) => pageIpage1.GetByLabel("Remarks").FillAsync(remarks);

        public async Task SetNotional(string notional)
        {
            var notionalLocator = pageIpage1.GetByLabel("Notional");
            await notionalLocator.ClickAsync();
            await notionalLocator.FillAsync(notional);
        }

        public async Task SetDealMoment(string dealMoment)
        {
            var dealMoment1Locator = pageIpage1.GetByLabel("Deal moment").First;
            await dealMoment1Locator.ClickAsync();
            await dealMoment1Locator.PressSequentiallyAsync(dealMoment, new() { Delay = 0 });
            await Task.Delay(500);
            await _basePageObject.PressTab();
        }

        public async Task SetNotificationMinutes(string setNotifcationMinutes)
        {
            var notificationMinutesLocator = pageIpage1.GetByLabel("Deal notification in");
            await notificationMinutesLocator.SelectOptionAsync(new[] { setNotifcationMinutes });
        }

        public async Task SetMinimalWithdrawalPercent(string setMinimalWithdrawal, int? offerNumbInCase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            var minimalWithdrawalLocator = pageIpage1.GetByLabel("Minimal withdrawal (%)");
            await minimalWithdrawalLocator.ClickAsync();
            await minimalWithdrawalLocator.ClearAsync();
            await minimalWithdrawalLocator.PressSequentiallyAsync(setMinimalWithdrawal, new() { Delay = 20 });
            await _basePageObject.PressTab();
        }

        public async Task SetCommitmentFee(string setCommitmentFee, int? offerNumbInCase = null)
        {
            int offerCase = offerNumbInCase ?? 1;
            var setCommitmentFeeLocator = pageIpage1.GetByLabel("Commitment fee");
            do
            {
                await setCommitmentFeeLocator.WaitForAsync();
                await setCommitmentFeeLocator.ClearAsync();
                await setCommitmentFeeLocator.FocusAsync();
                await setCommitmentFeeLocator.ClickAsync();
                await setCommitmentFeeLocator.FillAsync(setCommitmentFee);
                await _basePageObject.PressTab();
            }
            while (await setCommitmentFeeLocator.InputValueAsync() == null ||
          await setCommitmentFeeLocator.InputValueAsync() == string.Empty);
        }

        public async Task<string> GetMinimalWithdrawalAmount()
        {
            var minimalwithdrawalLocator = pageIpage1.Locator("div[class='content-item content-field item-2 flex'] div[class='field-item dataValueRead'] span span[class='leftJustifyStyle']");

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

        public async Task<string> GetMaximumWithdrawalAmount()
        {
            var maximumwithdrawalLocator = pageIpage1.Locator("div[class='content-item content-field item-3 flex'] div[class='field-item dataValueRead'] span span[class='leftJustifyStyle']").First;

            await maximumwithdrawalLocator.WaitForAsync();
            var maximumWithdrawalAmount = await maximumwithdrawalLocator.TextContentAsync();
            if (maximumWithdrawalAmount is not null)
            {
                return maximumWithdrawalAmount;
            }
            else
            {
                throw new Exception("GetMinimalWithDrawalAmount is null!");
            }
        }

        public async Task<string> GetMinimalWithdrawalAmountLoanUpdateAuto()
        {
            var minimalwithdrawalLocator = pageIpage1.Locator("xpath=(//div[contains(@class,'field-item dataValueRead')])[2]");

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

        public async Task SetStartDate(string startdate, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var setStartDateLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOfferCase}\\$pStartDate\"]");

            do
            {
                await setStartDateLocator.WaitForAsync();
                await WaitForClearance(setStartDateLocator);
                await setStartDateLocator.FocusAsync();
                await setStartDateLocator.FillAsync(startdate);
                await _basePageObject.PressTab();
            }
            while (await setStartDateLocator.InputValueAsync() == null ||
           await setStartDateLocator.InputValueAsync() == string.Empty);
        }

        private async Task WaitForClearance(ILocator inputLocator)
        {
            do
            {
                await inputLocator.ClearAsync();
                await Task.Delay(100);
            }
            while (await inputLocator.InputValueAsync() != string.Empty);
        }

        public async Task SetMaturityDate(string maturityDate, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var maturityDateLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOfferCase}\\$pMaturityDate\"]");
            do
            {
                await maturityDateLocator.WaitForAsync();
                await maturityDateLocator.ClickAsync();

                await WaitForClearance(maturityDateLocator);
                await maturityDateLocator.FocusAsync();

                await maturityDateLocator.FillAsync(maturityDate);
                await _basePageObject.PressTab();
            }
            while (await maturityDateLocator.InputValueAsync() == null ||
           await maturityDateLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetFirstCouponDate(string firstCouponDate, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var firstCouponDate1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOfferCase}\\$pFirstCouponDate\"]");
            await firstCouponDate1Locator.WaitForAsync();
            await firstCouponDate1Locator.ClearAsync();
            await firstCouponDate1Locator.ClickAsync();
            await firstCouponDate1Locator.FillAsync(firstCouponDate);
            await _basePageObject.PressTab();
        }

        public async Task SetLastCouponDate(string lastCouponDate, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var lastCouponDate1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOfferCase}\\$pLastCouponStartDate\"]");
            await lastCouponDate1Locator.WaitForAsync();
            await lastCouponDate1Locator.ClearAsync();
            await lastCouponDate1Locator.ClickAsync();
            await lastCouponDate1Locator.FillAsync(lastCouponDate);
            await _basePageObject.PressTab();
        }

        public async Task SetCouponDay(string? setCouponDay, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            if (setCouponDay is not null)
            {
                var couponDayLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pCouponDay\"]");
                await couponDayLocator.WaitForAsync();
                await couponDayLocator.ClearAsync();
                await couponDayLocator.ClickAsync();
                await couponDayLocator.FillAsync(setCouponDay);
                await _basePageObject.PressTab();
            }
            else
            {
                throw new Exception("SetCouponDay(): parameter setCouponDay seems to be null!");
            }
        }

        public async Task SetCouponDayOptionSet(CouponDay? options, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var couponDayLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pCouponDayOfWeek\"]");
            await couponDayLocator.WaitForAsync();
            await couponDayLocator.SelectOptionAsync(new[] { options.ToString() });
            await Task.Delay(500);
        }

        public async Task SetInterestSpreadRRDate(string interestSpreadRRDate1, int? alternativeNumbIncase = null)
        {
            int altOfferCase = alternativeNumbIncase ?? 1;
            var interestSpreadRRDate1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOfferCase}\\$pRateResetDate\"]");

            await interestSpreadRRDate1Locator.WaitForAsync();
            await interestSpreadRRDate1Locator.ClearAsync();
            await interestSpreadRRDate1Locator.ClickAsync();
            await interestSpreadRRDate1Locator.FillAsync(interestSpreadRRDate1);
            await _basePageObject.PressTab();
        }

        /// <summary>
        ///
        ///  ALTERNATIVE FIELDS
        ///
        /// <returns></returns>
        public async Task SetAmortisationType(string amortisationtype, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var amortisationTypeLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pAmortisationType\"]");
            await amortisationTypeLocator.SelectOptionAsync(new[] { amortisationtype });
            await _basePageObject.WaitForPageLoadedState();
            await _basePageObject.DebounceDom(amortisationTypeLocator);
            var isNumbOfAmortVisible = await pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pAmortisationQuantity\"]").IsVisibleAsync();

            if (amortisationtype == "Bullet")
            {
                isNumbOfAmortVisible.Should().BeFalse();
            }
            else
            {
                isNumbOfAmortVisible.Should().BeTrue();
            }
        }

        public async Task SetCouponFrequency(string couponfrequency, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var couponFrequencyLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pCouponFrequency\"]");
            await couponFrequencyLocator.WaitForAsync();
            await couponFrequencyLocator.SelectOptionAsync(new[] { couponfrequency });
        }

        public async Task SetSwap(bool setswap, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setSwapLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSwapNeeded\"]");
            await setSwapLocator.WaitForAsync();
            await setSwapLocator.SelectOptionAsync(new[] { setswap ? "Yes" : "No" });
            _scenarioContext["savedBoolSwap"] = setswap;
        }

        public async Task<string> GetValueOfCouponFrequency()
        {
            var generalLocatorSelect = pageIpage1.Locator($"select[name=\"$PpyWorkPage$pOffer$pAlternativeProductList$l1$pCouponFrequency\"]");
            await generalLocatorSelect.WaitForAsync();
            var couponFrequencyValue = await generalLocatorSelect.InputValueAsync();
            return couponFrequencyValue;
        }

        public async Task SetDayCount(string dayCount, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var dayCountLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pDayCountConvention\"]");
            await dayCountLocator.WaitForAsync();

            Task selectDayCount = dayCount switch
            {
                "Act/360" => dayCountLocator.SelectOptionAsync(new[] { "Act/360" }),
                "Act/Act" => dayCountLocator.SelectOptionAsync(new[] { "Act/Act" }),
                "30/360" => dayCountLocator.SelectOptionAsync(new[] { "30/360" }),
                _ => throw new Exception("Unknown option chosen for method public Task LoanUpdate.SetDayCount method"),
            };
            await selectDayCount;
        }

        public async Task SetNumberOfAmortisations(string numberofamortisations, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var amortisationFrequencyLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pAmortisationQuantity\"]");
            do
            {
                await amortisationFrequencyLocator.WaitForAsync();
                await amortisationFrequencyLocator.ClearAsync();
                await amortisationFrequencyLocator.ClickAsync();
                await amortisationFrequencyLocator.FillAsync(numberofamortisations);
                await _basePageObject.PressTab();
            }
            while (await amortisationFrequencyLocator.InputValueAsync() == null ||
          await amortisationFrequencyLocator.InputValueAsync() == string.Empty);
        }

        public Task SetAmortisationFrequency(string amortisationfrequency, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var amortisationFrequencyLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pAmortisationFrequency\"]");

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

        public async Task SetSpreadH1(string setH1spread, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH1FieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRate1stMan\"]");
            do
            {
                await spreadH1FieldLocator.WaitForAsync();
                await spreadH1FieldLocator.FocusAsync();
                await spreadH1FieldLocator.ClickAsync();
                await spreadH1FieldLocator.FillAsync(setH1spread);
                await _basePageObject.PressTab();
            }
            while (await spreadH1FieldLocator.InputValueAsync() == null || await spreadH1FieldLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetSpreadH2(string setH2spread, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH2FieldLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRate2ndMan\"]");
            do
            {
                await spreadH2FieldLocator.WaitForAsync();
                await spreadH2FieldLocator.FocusAsync();
                await spreadH2FieldLocator.ClickAsync();
                await spreadH2FieldLocator.FillAsync(setH2spread);
                await _basePageObject.PressTab();
            }
            while (await spreadH2FieldLocator.InputValueAsync() == null || await spreadH2FieldLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetSpreadH1Comment(string setH1Comment, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH1FieldLocator = pageIpage1.Locator($"textarea[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateComment1stMan\"]").First;
            await spreadH1FieldLocator.WaitForAsync();
            await spreadH1FieldLocator.FocusAsync();
            await spreadH1FieldLocator.ClickAsync();
            await spreadH1FieldLocator.FillAsync(setH1Comment);
            await _basePageObject.PressTab();
        }

        public async Task SetSpreadH2Comment(string setH2Comment, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var spreadH2CommentLocator = pageIpage1.Locator($"textarea[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateComment2ndMan\"]").First;
            await spreadH2CommentLocator.WaitForAsync();
            await spreadH2CommentLocator.FocusAsync();
            await spreadH2CommentLocator.ClickAsync();
            await spreadH2CommentLocator.FillAsync(setH2Comment);
            await _basePageObject.PressTab();
        }

        public async Task OverwriteSpreadMisMatch(int? totalAltSections = null)
        {
            int totalAltSectionsInForm = totalAltSections ?? 0;
            var spreadMisMatchCheckBoxLocator = pageIpage1.GetByLabel("Overwrite rate mismatch").Nth(totalAltSectionsInForm);
            await spreadMisMatchCheckBoxLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await spreadMisMatchCheckBoxLocator.CheckAsync();
        }

        public async Task SetFinalSpread(string setFinalSpread, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSpreadLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateFinal\"]");

            await finalSpreadLocator.WaitForAsync();
            do
            {
                await finalSpreadLocator.WaitForAsync();
                await finalSpreadLocator.FocusAsync();
                await finalSpreadLocator.ClickAsync();
                await finalSpreadLocator.FillAsync(setFinalSpread);
                await _basePageObject.PressTab();
            }
            while (await finalSpreadLocator.InputValueAsync() == null ||
            await finalSpreadLocator.InputValueAsync() == string.Empty);
            await _basePageObject.PressTab();
        }

        public async Task SetFinalSpreadComment(string setFinalSpreadComment, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var finalSpreadCommentLocator = pageIpage1.Locator($"textarea[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateCommentFinal\"]");
            do
            {
                await finalSpreadCommentLocator.WaitForAsync();
                await finalSpreadCommentLocator.FocusAsync();
                await finalSpreadCommentLocator.ClickAsync();
                await finalSpreadCommentLocator.FillAsync(setFinalSpreadComment);
                await _basePageObject.PressTab();
            }
            while (await finalSpreadCommentLocator.InputValueAsync() == null || await finalSpreadCommentLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetMarketRate(string setMarketRate, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setMarketRate1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pMarketInterestRate\"]");

            do
            {
                await setMarketRate1Locator.WaitForAsync();
                await setMarketRate1Locator.FocusAsync();
                await setMarketRate1Locator.ClickAsync();
                await setMarketRate1Locator.FillAsync(setMarketRate);
                await _basePageObject.PressTab();
            }
            while (await setMarketRate1Locator.InputValueAsync() == null || await setMarketRate1Locator.InputValueAsync() == string.Empty);
        }

        public async Task ClickSaveH1Button(int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var h1ButtonLocator = pageIpage1.Locator($"a[name='SetFirstSpreadAlt_pyWorkPage.Offer.AlternativeList({altOffer})_6db8018e-fd23-46ba-aeae-b1150b324440'] i[class='pi pi-document-check-solid']");
            await h1ButtonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await h1ButtonLocator.ClickAsync(new() { Force = true });
            await _basePageObject.WaitForLoadFinalSpreadPage();
        }

        public async Task ClickSaveH2Button(int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var h2ButtonLocator = pageIpage1.Locator($"a[name='SetSecondSpreadAlt_pyWorkPage.Offer.AlternativeList({altOffer})_6db8018e-fd23-46ba-aeae-b1150b324440'] i[class='pi pi-document-check-solid']");
            await h2ButtonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
            await h2ButtonLocator.ClickAsync(new() { Force = true });
            await _basePageObject.WaitForResponse();
        }

        public async Task ClickSaveFinalSpreadButton(int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var saveFinalSpreadButtonLocator = pageIpage1.Locator($"a[name='SetFinalSpreadAlt_pyWorkPage.Offer.AlternativeList({altOffer})_6db8018e-fd23-46ba-aeae-b1150b324440'] i[class='pi pi-document-check-solid']");

            do
            {
                await saveFinalSpreadButtonLocator.ClickAsync();
            }
            while ((await pageIpage1.GetByText("Treasury1").Nth(1).IsVisibleAsync()) == false && (await pageIpage1.GetByText("Treasury2").Nth(1).IsVisibleAsync()) == false
            && (await pageIpage1.GetByText("TreasuryManager1").Nth(1).IsVisibleAsync()) == false && (await pageIpage1.GetByText("TreasuryManager2").Nth(1).IsVisibleAsync()) == false
            && (await pageIpage1.GetByText("AccountDesk1").Nth(1).IsVisibleAsync()) == false && (await pageIpage1.GetByText("AccountDesk2").Nth(1).IsVisibleAsync()) == false);
        }

        public async Task ClickSaveMarketRate(int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var saveMarketRateButtonLocator1 = pageIpage1.Locator($"a[name='SetMarketInterestAlt_pyWorkPage.Offer.AlternativeList({altOffer})_6db8018e-fd23-46ba-aeae-b1150b324440'] i[class='pi pi-document-check-solid']");
            await saveMarketRateButtonLocator1.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            do
            {
                await saveMarketRateButtonLocator1.ClickAsync();
            }
            while ((await pageIpage1.GetByText("User AccountDesk2Test.").Nth(2).IsVisibleAsync()) == false && (await pageIpage1.GetByText("User AccountDesk1Test.").Nth(2).IsVisibleAsync()) == false);
        }

        public Task CheckIncludeInMailBox() => _includeInMailBoxLocator.First.CheckAsync();

        public Task CheckIncludeRemarkInMailBox() => pageIpage1.Locator("[id=\"\\39 feeefcc\"]").UncheckAsync();

        public Task<ILocator> GetIncludeInMailBoxLocator()
        {
            var locator = _includeInMailBoxLocator;
            return Task.FromResult(locator);
        }

        public async Task SetDealRemark(string dealremark, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var dealRemark1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pRemarkDeal\"]");
            do
            {
                await dealRemark1Locator.FillAsync(dealremark);
                await _basePageObject.PressTab();
                await _basePageObject.PressTab();
            }
            while (await dealRemark1Locator.InputValueAsync() == null ||
          await dealRemark1Locator.InputValueAsync() == string.Empty);
        }

        public async Task ClickSubmitFinalSpreadPage()
        {
            var submitButton = pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            await Task.Delay(500);
            await submitButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 5000 });
            await submitButton.ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 2000 });
            await submitButton.ClickAsync(new() { Force = false, Delay = 100, });

            await Task.Delay(5000);
        }

        public async Task SetReferenceRate(string setReferenceRate, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var referenceRateLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pReferenceRate\"]");
            await referenceRateLocator.SelectOptionAsync(new[] { setReferenceRate });
        }

        public async Task Set48HoursClause(bool? set48HoursClause)
        {
            if (set48HoursClause is not null)
            {
                await pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "48 hours clause" }).WaitForAsync();
                await pageIpage1.GetByRole(AriaRole.Combobox, new() { Name = "48 hours clause" }).SelectOptionAsync(new[] { (bool)set48HoursClause ? "Yes" : "No" });
            }
            else
            {
                throw new NullReferenceException("Set48HoursClause(): Seems that parameter set48HoursClause is null!");
            }
        }

        public async Task SetSpreadH1Basis(string setH1Euribor, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setEuriborH1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRate1stManEuribor\"]");
            await setEuriborH1Locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            await setEuriborH1Locator.FillAsync(setH1Euribor);
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
            await _basePageObject.PressTab();
        }

        public async Task SetSpreadH2Basis(string setH2Euribor, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setEuriborH2Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRate1stManEuribor\"]");
            await setEuriborH2Locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await setEuriborH2Locator.FillAsync(setH2Euribor);
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
            await _basePageObject.PressTab();
        }

        public async Task SetFinalSpreadBasis(string setFinalSpreadEuribor, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var setFinalSpreadEuriborLocator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateFinalEuribor\"]");

            await setFinalSpreadEuriborLocator.WaitForAsync();
            do
            {
                await setFinalSpreadEuriborLocator.WaitForAsync();
                await setFinalSpreadEuriborLocator.FocusAsync();
                await setFinalSpreadEuriborLocator.ClickAsync();
                await setFinalSpreadEuriborLocator.FillAsync(setFinalSpreadEuribor);
                await _basePageObject.PressTab();
            }
            while (await setFinalSpreadEuriborLocator.InputValueAsync() == null ||
            await setFinalSpreadEuriborLocator.InputValueAsync() == string.Empty);
        }

        public async Task SetMargin(string setMargin, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var setMarketRate1Locator = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pMargin\"]");

            await setMarketRate1Locator.WaitForAsync();
            do
            {
                await setMarketRate1Locator.WaitForAsync();
                await setMarketRate1Locator.FocusAsync();
                await setMarketRate1Locator.ClickAsync();
                await setMarketRate1Locator.FillAsync(setMargin);
                await _basePageObject.PressTab();
            }
            while (await setMarketRate1Locator.InputValueAsync() == null ||
            await setMarketRate1Locator.InputValueAsync() == string.Empty);
        }

        public async Task ClickSaveMargin(int? alternativeNumbIncase = null)
        {
            await Task.Delay(400);
            int altOffer = alternativeNumbIncase ?? 1;
            var newSaveMarginLocator = pageIpage1.Locator($"a[name='SetMargin_pyWorkPage.Offer.AlternativeList({altOffer})_6db8018e-fd23-46ba-aeae-b1150b324440'] i[class='pi pi-document-check-solid']");
            await newSaveMarginLocator.FocusAsync();
            await newSaveMarginLocator.ClickAsync();
            await Task.Delay(400);
            await _basePageObject.PageWaitForUrlLoaded();
        }

        public async Task SetMarginComment(string setMarginComment, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var finalSpreadCommentLocator = pageIpage1.Locator($"textarea[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pMarginComment\"]");

            await finalSpreadCommentLocator.WaitForAsync();
            await finalSpreadCommentLocator.FocusAsync();
            await finalSpreadCommentLocator.ClickAsync();
            await finalSpreadCommentLocator.FillAsync(setMarginComment);
            await _basePageObject.PressTab();
        }

        public async Task SetSpreadMisMatchComment(string setSpreadMisMatchComment, int? offerNumbInCase = null, int? alternativeNumbIncase = null)
        {
            int caseOffer = offerNumbInCase ?? 1;
            int altOffer = alternativeNumbIncase ?? 1;

            var setSpreadMisMatchCommentLocator = pageIpage1.Locator($"textarea[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSpreadRateAdjustedComment\"]");
            await setSpreadMisMatchCommentLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await setSpreadMisMatchCommentLocator.ClickAsync();
            await setSpreadMisMatchCommentLocator.FillAsync(setSpreadMisMatchComment);
            await page.WaitForLoadStateAsync(LoadState.Load, new PageWaitForLoadStateOptions { Timeout = 15000 });
        }

        public async Task SetSwapReferenceRate(string setSwapReferenceRate, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var swapReferenceLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSwapRequestInformation\\$pFloatingLeg\\$pReferenceRate\"]");

            await swapReferenceLocator.SelectOptionAsync(new[] { setSwapReferenceRate });
            await _basePageObject.WaitForPageLoadedState();
        }

        public async Task SetClearingHouse(string setClearingHouse, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;

            var clearingHouseLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSwapRequestInformation\\$pClearingHouse\"]");
            await clearingHouseLocator.SelectOptionAsync(new[] { setClearingHouse });
            await _basePageObject.WaitForPageLoadedState();
        }

        public async Task SetBusinessDayConvention(string setBusinessDayConvention, int? alternativeNumbIncase = null)
        {
            int altOffer = alternativeNumbIncase ?? 1;
            var businessDayConventionLocator = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pOffer\\$pAlternativeList\\$l{altOffer}\\$pSwapRequestInformation\\$pFixedLeg\\$pBusinessDayConvention\"]");
            var choseOption = setBusinessDayConvention switch
            {
                "Mod Foll Unadj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Modified Following Unadjusted" }),
                "Mod Foll Adj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Modified Following Adjusted" }),
                "Foll Unadj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Following Unadjusted" }),
                "Foll Adj" => businessDayConventionLocator.SelectOptionAsync(new[] { "Following Adjusted" }),
                _ => throw new Exception("Unknown option chosen for switch expression counterParty switch,origin: SetBusinessDayConvention()"),
            };

            await choseOption;

            await _basePageObject.WaitForPageLoadedState();
        }
    }
}