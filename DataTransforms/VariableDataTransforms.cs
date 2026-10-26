using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.DataTransforms
{
    public interface IVariableDataTransforms
    {
        Task<string> TransformSpreadDataVariableStringWithThreeDecimalPlaces(string finalSpread, string? basisFinalSpread = "");

        Task<string> TransformSingleSpreadToStringWithRequiredDecimalPlaces(DataTransformOptions option, string spread);

        Task<string> TransformCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions option, double customerRate);

        string TransformBaseRateAndFinalSpreadandBaseRateToCustomerRate(DataTransformOptions option, double finalSpread, string baseRate);
    }

    public class VariableDataTransforms : IVariableDataTransforms
    {
        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public VariableDataTransforms(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
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

        public async Task<string> TransformSpreadDataVariableStringWithThreeDecimalPlaces(string finalSpread, string? basisFinalSpread = "")
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var insertFinalSpread = finalSpread;
            var insertBasisFinalSpread = basisFinalSpread;

            if (finalSpread.Contains(","))
            {
                insertFinalSpread = finalSpread.Replace(",", ".");
            }

            if (basisFinalSpread != null && basisFinalSpread != string.Empty)
            {
                if (basisFinalSpread.Contains(","))
                {
                    insertBasisFinalSpread = basisFinalSpread.Replace(",", ".");
                }

                double doubleFinalSpread = double.Parse(insertFinalSpread);
                double doubleFinalSpreadBPS = doubleFinalSpread;

                double doubleBasisFinalSpread = double.Parse(insertBasisFinalSpread);
                double doubleBasisFinalSpreadBPS = doubleBasisFinalSpread;

                double doubleVariableCustomerRate = doubleFinalSpreadBPS + doubleBasisFinalSpreadBPS;
                var roundedVariableCustomerRate = Math.Round(doubleVariableCustomerRate, 2, MidpointRounding.ToEven);

                var formattedFixedCustomerRate = roundedVariableCustomerRate.ToString("G", dutchCulture);
                await Task.CompletedTask;

                // var x = string.Format("{0:0.00}", formattedFixedCustomerRate);
                return formattedFixedCustomerRate;
            }
            else
            {
                double doubleFinalSpread = double.Parse(insertFinalSpread);
                double doubleFinalSpreadBPS = doubleFinalSpread;
                var roundedVariableCustomerRate = Math.Round(doubleFinalSpreadBPS, 2, MidpointRounding.ToEven);
                var formattedFixedCustomerRate = roundedVariableCustomerRate.ToString("G", dutchCulture);
                await Task.CompletedTask;
                return formattedFixedCustomerRate;
            }
        }

        public async Task<string> TransformSingleSpreadToStringWithRequiredDecimalPlaces(DataTransformOptions option, string spread)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");
            double doubleSpread;

            if (spread.Contains(","))
            {
                var spreadWithoutComma = spread.Replace(",", ".");
                doubleSpread = double.Parse(spreadWithoutComma);
            }
            else
            {
                doubleSpread = double.Parse(spread);
            }

            double formattedDoubleSpread = double.Round(doubleSpread, 2, MidpointRounding.AwayFromZero);
            await Task.CompletedTask;

            if (option == DataTransformOptions.Offermail)
            {
                // IMPLEMENTATIE MOET HIERVOOR NOG GEBEUREN INDIEN NODIG!!
                return formattedDoubleSpread.ToString("F");
            }
            else if (option == DataTransformOptions.UGsheet)
            {
                // String with two decimals after comma
                return formattedDoubleSpread.ToString("F2");
            }
            else
            {
                throw new Exception("Unknown option chosen for DataTransformOptions, call origin from TransformSingleSpreadToStringWithRequiredDecimalPlaces()");
            }
        }

        public string TransformBaseRateAndFinalSpreadandBaseRateToCustomerRate(DataTransformOptions option, double finalSpread, string baseRate)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var insertBaseRate = baseRate;

            if (baseRate.Contains(","))
            {
                insertBaseRate = baseRate.Replace(",", ".");
            }

            double doubleBaseRate = double.Parse(insertBaseRate);
            var formattedFinalspread = finalSpread / 100;
            var customerRate = formattedFinalspread + doubleBaseRate;

            if (option == DataTransformOptions.OpicsFiles)
            {
                // Wordt gebruikt voor COUPRATE, afronding hiervan is anders dan de offertemail!
                // var roundedFixedCustomerRate = Math.Round(CustomerRate, 4, MidpointRounding.ToZero);
                var roundedFixedCustomerRate = Math.Round(customerRate, 4, MidpointRounding.AwayFromZero);
                var formattedFixedCustomerRate = roundedFixedCustomerRate.ToString("0.00000000");
                return formattedFixedCustomerRate;
            }
            else
            {
                throw new Exception("Unknown option chosen for DataTransformOptions, call origin from TransformBaseRateAndFinalSpreadandBaseRateToCustomerRate()");
            }
        }

        public async Task<string> TransformCustomerRateToStringWithRequiredDecimalPlaces(DataTransformOptions option, double customerRate)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");

            var loanType = (string)_scenarioContext["savedLoanType"];

            if (option == DataTransformOptions.Offermail)
            {
                var roundedVariableCustomerRate = Math.Round(customerRate, 2, MidpointRounding.ToEven);
                var formattedVariableCustomerRate = roundedVariableCustomerRate.ToString("G", dutchCulture);
                return formattedVariableCustomerRate;
            }
            else if (option == DataTransformOptions.UGsheet)
            {
                // deprecated in ug sheet since 1.83 release.
                // var percentageCustomerRate = customerRate / 100;
                return customerRate.ToString("F2");
            }
            else if (option == DataTransformOptions.Sluitbrief)
            {
                if (loanType == "LONG"  || loanType == "SHORT")
                {
                    var roundedRate2 = Math.Round(customerRate, 2, MidpointRounding.AwayFromZero);
                    var formattedRate2 = roundedRate2.ToString("F2", CultureInfo.InvariantCulture);
                    return formattedRate2;
                }
                else if (loanType == "NORMAL" || loanType == "INTERESTSPREADRESET")
                {
                    var roundedRate2 = Math.Round(customerRate, 2, MidpointRounding.AwayFromZero);
                    var formattedRate2 = roundedRate2.ToString("F2", CultureInfo.InvariantCulture);
                    return formattedRate2.Replace(",", ".");
                }
                else
                {
                    var stringPercentageCustomerRate = customerRate.ToString(dutchCulture);
                    return stringPercentageCustomerRate;
                }
            }
            else
            {
                throw new Exception("Unknown option chosen for DataTransformOptions, call origin from TransformCustomerRateToStringWithRequiredDecimalPlaces()");
            }
        }
    }
}