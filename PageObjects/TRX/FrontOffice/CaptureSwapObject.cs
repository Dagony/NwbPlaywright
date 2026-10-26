using Castle.Components.DictionaryAdapter;
using Castle.Facilities.TypedFactory.Internal;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using NwbPlaywright.Fields.FrontOffice.Long;
using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.PageObjects.TRX.FrontOffice
{
    public interface ICaptureSwapObject
    {
        Task OpenCaptureSwapLink();

        Task<List<SwapCaptureFields>> GetValuesFromCaptureSWAPPage(bool amortisationIsBullet);
    }

    public class CaptureSwapObject : ICaptureSwapObject
    {
        public CaptureSwapObject(
            IPlaywrightDriver playwrightDriver,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            ScenarioContext scenarioContext)
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

        public Task OpenCaptureSwapLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Capture swap details (Capture)" }).ClickAsync();

        public async Task<List<SwapCaptureFields>> GetValuesFromCaptureSWAPPage(bool amortisationIsBullet)
        {
            string[] fieldLocators = new[]
            {
                "Notional", "AmortisationType", "StartDate", "MaturityDate", "Comments", "ClearingHouse",
                "DayCountConvention", "BusinessDayConvention", "FirstCouponDate", "CouponLastStartDate", "CouponDay", "CouponFrequency",
            };

            // LCH = 0636901440
            // EUREX = 0636901439
            List<string> fieldValues = new List<string>();
            foreach (var locatorName in fieldLocators)
            {
                var generalLocatorInput = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pSwap\\$p{locatorName}\"]");
                var generalLocatorSelect = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$p{locatorName}\"]");
                var generalLocatorFixedLegInput = pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pSwap\\$pFixedLeg\\$p{locatorName}\"]");
                var generalLocatorFixedLegSelect = pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$pFixedLeg\\$p{locatorName}\"]");

                var valueOfLocator = await generalLocatorInput.Or(generalLocatorSelect).Or(generalLocatorFixedLegInput).Or(generalLocatorFixedLegSelect).InputValueAsync();
                fieldValues.Add(valueOfLocator);
            }

            SwapCaptureFields obj = new SwapCaptureFields();
            PropertyInfo[] properties = typeof(SwapCaptureFields).GetProperties();

            for (int i = 0; i < fieldValues.Count && i < properties.Length; i++)
            {
                properties[i].SetValue(obj, fieldValues[i]);
            }

            // Adding floating and optional fields, because of locator conflicts in loop.
            var valueReferenceRate = await pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$pFloatingLeg\\$pReferenceRate\"]").InputValueAsync();
            var valueDayCountConvention = await pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$pFloatingLeg\\$pDayCountConvention\"]").InputValueAsync();
            var valueBusinessDayConvention = await pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$pFloatingLeg\\$pBusinessDayConvention\"]").InputValueAsync();
            properties[12].SetValue(obj, valueReferenceRate);
            properties[13].SetValue(obj, valueDayCountConvention);
            properties[14].SetValue(obj, valueBusinessDayConvention);

            if (amortisationIsBullet == false)
            {
                var valueNumbOfAmort = await pageIpage1.Locator($"input[name=\"\\$PpyWorkPage\\$pSwap\\$pAmortisationNumberOf\"]").InputValueAsync();
                var valueAmortFreq = await pageIpage1.Locator($"select[name=\"\\$PpyWorkPage\\$pSwap\\$pAmortisationFrequency\"]").InputValueAsync();
                properties[15].SetValue(obj, valueNumbOfAmort);
                properties[16].SetValue(obj, valueAmortFreq);
            }

            var swapFieldsList = new List<SwapCaptureFields> { obj };
            return swapFieldsList;
        }
    }
}