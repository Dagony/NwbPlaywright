using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;
using static TA.Playwright.Fields.EnumsForTRX;

namespace TA.Playwright.DataTransforms
{
    public interface IFixedDataTransforms
    {
        double TransformFixedCustomerRateToDouble(string finalSpread, string marketInterestRate);

        string TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions option, double customerRate);

        string TransformFixedCustomerRateToStringWithRequiredDecimalPlacesShort(DataTransformOptions option, double customerRate);
    }

    public class FixedDataTransforms : IFixedDataTransforms
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public FixedDataTransforms(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
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

        public string TransformFixedCustomerRateToStringWithRequiredDecimalPlacesShort(DataTransformOptions option, double customerRate)
        {
            CultureInfo standardCultureInfo = _testSettings.CultureInfo;

            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            switch (option)
            {
                case DataTransformOptions.Offermail:
                    if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong ||
                    productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional ||
                    productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional ||
                    productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                    {
                        double roundedFixedCustomerRate = Math.Round(customerRate, 4, MidpointRounding.ToEven);
                        var formattedFixedCustomerRate = roundedFixedCustomerRate.ToString("G", standardCultureInfo);
                        return formattedFixedCustomerRate;
                    }
                    else
                    {
                        var formattedFixedCustomerRate = customerRate.ToString("G", standardCultureInfo);
                        return formattedFixedCustomerRate;
                    }

                case DataTransformOptions.UGsheet:
                    return customerRate.ToString();

                case DataTransformOptions.OpicsFiles:
                    var roundedRate = Math.Round(customerRate, 8, MidpointRounding.AwayFromZero);
                    var formattedRate = roundedRate.ToString("0.########", CultureInfo.InvariantCulture);
                    return formattedRate;

                case DataTransformOptions.Sluitbrief:
                    var roundedRate2 = Math.Round(customerRate, 8, MidpointRounding.AwayFromZero);
                    var formattedRate2 = roundedRate2.ToString("0.######", CultureInfo.InvariantCulture);
                    if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
                    {
                        return formattedRate2.Replace(".", ",");
                    }

                    return formattedRate2;

                default:
                    throw new Exception("Unknown option chosen for DataTransformOptions, call origin from TransformCustomerRateToStringWithRequiredDecimalPlaces()");
            }
        }

        public string TransformFixedCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions option, double customerRate)
        {
            CultureInfo standardCultureInfo = _testSettings.CultureInfo;

            var loanType = (string)_scenarioContext["savedLoanType"];
            var productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];

            switch (option)
            {
                case DataTransformOptions.Offermail:
                    if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong ||
                    productType == EnumsForTRX.SelectProduct.FloatingRateLoanFloatingNotional ||
                    productType == EnumsForTRX.SelectProduct.FloatingRateLoanFixedNotional ||
                    productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanShort)
                    {
                        double roundedFixedCustomerRate = Math.Round(customerRate, 4, MidpointRounding.ToEven);
                        var formattedFixedCustomerRate = roundedFixedCustomerRate.ToString("G", standardCultureInfo);
                        return formattedFixedCustomerRate;
                    }
                    else
                    {
                        var formattedFixedCustomerRate = customerRate.ToString("G", standardCultureInfo);
                        return formattedFixedCustomerRate;
                    }

                case DataTransformOptions.UGsheet:
                    return customerRate.ToString();

                case DataTransformOptions.OpicsFiles:
                    var roundedRate = Math.Round(customerRate, 8, MidpointRounding.AwayFromZero);
                    var formattedRate = roundedRate.ToString("0.00######", CultureInfo.InvariantCulture);
                    return formattedRate;

                case DataTransformOptions.Sluitbrief:
                    var roundedRate2 = Math.Round(customerRate, 8, MidpointRounding.AwayFromZero);
                    var formattedRate2 = roundedRate2.ToString("0.00####", CultureInfo.InvariantCulture);
                    if (productType == EnumsForTRX.SelectProduct.TraditionalFixedRateLoanLong)
                    {
                        return formattedRate2.Replace(".", ",");
                    }

                    return formattedRate2;

                default:
                    throw new Exception("Unknown option chosen for DataTransformOptions, call origin from TransformCustomerRateToStringWithRequiredDecimalPlaces()");
            }
        }

        public double TransformFixedCustomerRateToDouble(string finalSpread, string marketInterestRate)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");
            var insertFinalSpread = finalSpread;
            var insertMarketInterestRate = marketInterestRate;

            if (finalSpread.Contains(","))
            {
                insertFinalSpread = finalSpread.Replace(",", ".");
            }

            if (marketInterestRate.Contains(","))
            {
                insertMarketInterestRate = marketInterestRate.Replace(",", ".");
            }

            double doubleFinalSpread = double.Parse(insertFinalSpread);
            double doubleFinalSpreadBPS = doubleFinalSpread / 100;
            double doubleMarketInterestRate = double.Parse(insertMarketInterestRate);
            double doubleFixedCustomerRate = doubleFinalSpreadBPS + doubleMarketInterestRate;
            return doubleFixedCustomerRate;
        }
    }
}