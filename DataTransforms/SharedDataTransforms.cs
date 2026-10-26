using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Humanizer;
using Reqnroll.CommonModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NwbPlaywright.Config;
using NwbPlaywright.Drivers;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.DataTransforms
{
    public interface ISharedDataTransforms
    {
        string GetFormattedCurrentDate();

        Task<string> CalculateFormattedMaturityDate(string startDate, int years, bool subtractDaysIfWeekendDay = false);

        Task<string> TransformDateToWrittenDate(DataTransformOptions option, string date);

        Task<(string formattedStartDate, string formattedMaturityDate)> TransformStartAndMaturityDate(DataTransformOptions optionTransform, LanguageSetting optionLanguage, string startDate, string maturityDate);

        string GetDateYesterday();

        string GetCurrentTransactionDate(DataTransformOptions option);

        string GetCurrentDateDDCouponDay();

        string CalculateMaturityInYears(string startDate, string maturityDate);

        string ConvertNumberToWords(string number);

        string ConvertDateToChosenFrequencyStringMonths(CultureInfo cultureInfo, DataTransformOptions optionDataTransform, CouponFrequency optionCouponFrequency, string inputDate);

        string FormatNotional(CultureInfo culture, string notional);

        string TransformOriginalSecID(string originalSecID);

        string TransformDate(CultureInfo cultureInfo, string inputDate);

        double? ParseAndCheckNotional(string notional);
    }

    public class SharedDataTransforms : ISharedDataTransforms
    {
        private readonly IPage _page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage0;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IPlaywrightDriver _playwrightDriver;

        public SharedDataTransforms(IPlaywrightDriver playwrightDriver, TestSettings testSettings, ScenarioContext scenarioContext)
        {
            this._page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            pageIpage0 = this._page.FrameLocator("iframe[name=\"PegaGadget0Ifr\"]");
            pageIpage1 = this._page.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]");
            pageIpage2 = this._page.FrameLocator("iframe[name=\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
            _playwrightDriver = playwrightDriver;
        }

        public string TransformOriginalSecID(string originalSecID)
        {
            if (originalSecID.Length > 3)
            {
                var originalSecIDWithoutZeros = originalSecID.Remove(2, 2);

                // Replace index 1 with '-' (change W or space).
                var originalSecIDWithoutZerosAndWithoutSpace = originalSecIDWithoutZeros.Substring(0, 1) + '-' + originalSecIDWithoutZeros.Substring(2);
                return originalSecIDWithoutZerosAndWithoutSpace;
            }
            else
            {
                throw new Exception("Inputted originalSecID parameter should have a larger length than 3. Origin call from TransformOriginalSecID");
            }
        }

        public string TransformDate(CultureInfo cultureInfo, string inputDate)
        {
            DateTime date;
            if (!DateTime.TryParseExact(inputDate, "dd-MM-yyyy", cultureInfo, DateTimeStyles.None, out date))
            {
                throw new ArgumentException("Invalid date format. Please use 'dd-MM-yyyy'.");
            }

            string formattedDate = date.ToString("dd MMM yy", cultureInfo);

            return formattedDate;
        }

        public string GetCurrentTransactionDate(DataTransformOptions option)
        {
            DateTime transactionDate = DateTime.Now;
            var stringTransactionDate = string.Empty;
            var chooseOption = option switch
            {
                DataTransformOptions.UGsheet => stringTransactionDate = transactionDate.ToString("dd-MM-yyyy"),
                DataTransformOptions.OpicsFiles => stringTransactionDate = transactionDate.ToString("yyyy-MM-dd"),
                _ => throw new Exception("Unknown option chosen for switch expression assertSCHEMATYPE"),
            };
            return stringTransactionDate;
        }

        public string CalculateMaturityInYears(string startDate, string maturityDate)
        {
            string dateFormat = "dd-MM-yyyy";
            DateTime dtStartDate = DateTime.ParseExact(startDate, dateFormat, CultureInfo.InvariantCulture);
            DateTime dtMaturityDate = DateTime.ParseExact(maturityDate, dateFormat, CultureInfo.InvariantCulture);
            TimeSpan differenceInDays = dtMaturityDate - dtStartDate;

            // Calculate the difference in years, considering the fractional part
            double differenceInYears = differenceInDays.TotalDays / 365.2425; // Considering leap years approximately
            int roundedYears = (int)Math.Floor(differenceInYears);
            return roundedYears.ToString();
        }

        public string ConvertDateToChosenFrequencyStringMonths(CultureInfo cultureInfo, DataTransformOptions optionDataTransform, CouponFrequency optionCouponFrequency, string inputDate)
        {
            // CultureInfo dutchCulture = new CultureInfo("nl-NL");
            DateTime date;
            if (!DateTime.TryParseExact(inputDate, "dd-MM-yyyy", cultureInfo, DateTimeStyles.None, out date))
            {
                throw new ArgumentException("Invalid date format. Please use 'dd-MM-yyyy'.");
            }

            string day = date.ToString("dd", cultureInfo);
            string month = date.ToString("MMM", cultureInfo).Substring(0, 3);

            // Calculate the quarterly months
            string monthPlus3 = date.AddMonths(3).ToString("MMM", cultureInfo).Substring(0, 3);
            string monthPlus6 = date.AddMonths(6).ToString("MMM", cultureInfo).Substring(0, 3);
            string monthPlus9 = date.AddMonths(9).ToString("MMM", cultureInfo).Substring(0, 3);

            var annualString = string.Empty;
            var semiAnnualString = string.Empty;
            var quarterlyString = string.Empty;
            var monthlyString = string.Empty;
            var weeklyString = string.Empty;

            if (optionCouponFrequency == CouponFrequency.Annual)
            {
                var returnAnnualString = optionDataTransform switch
                {
                    DataTransformOptions.UGsheet => annualString = $"{day}-{month}",
                    DataTransformOptions.SwapCopyFunction => annualString = $"{day}-{month} ??? first stub",
                    DataTransformOptions.Sluitbrief => annualString = string.Empty,
                    _ => throw new Exception("Unknown option chosen for returnAnnualString switch, origin call from ConvertDateToChosenFrequencyStringMonths method"),
                };

                return annualString;
            }
            else if (optionCouponFrequency == CouponFrequency.Semiannual)
            {
                var returnSemiAnnualString = optionDataTransform switch
                {
                    DataTransformOptions.UGsheet => semiAnnualString = $"{day}-{month}/{monthPlus6}",
                    DataTransformOptions.SwapCopyFunction => semiAnnualString = $"{day}-{month}/{monthPlus6} ??? first stub",
                    DataTransformOptions.Sluitbrief => semiAnnualString = string.Empty,
                    _ => throw new Exception("Unknown option chosen for returnSemiAnnualString switch, origin call from ConvertDateToChosenFrequencyStringMonths method"),
                };

                return semiAnnualString;
            }
            else if (optionCouponFrequency == CouponFrequency.Quarterly)
            {
                var returnQuarterlyString = optionDataTransform switch
                {
                    DataTransformOptions.UGsheet => quarterlyString = $"{day}-{month}/{monthPlus3}/{monthPlus6}/{monthPlus9}",
                    DataTransformOptions.SwapCopyFunction => quarterlyString = $"{day}-{month}/{monthPlus3}/{monthPlus6}/{monthPlus9} ??? first stub",
                    DataTransformOptions.Sluitbrief => quarterlyString = string.Empty,
                    _ => throw new Exception("Unknown option chosen for returnQuarterlyString switch, origin call from ConvertDateToChosenFrequencyStringMonths method"),
                };

                return quarterlyString;
            }
            else if (optionCouponFrequency == CouponFrequency.Monthly)
            {
                var returnMonthlyString = optionDataTransform switch
                {
                    DataTransformOptions.UGsheet => monthlyString = $"maandelijks over de {day}e",
                    DataTransformOptions.SwapCopyFunction => monthlyString = $"Monthly ??? ??? first stub",
                    DataTransformOptions.Sluitbrief => monthlyString = string.Empty,
                    _ => throw new Exception("Unknown option chosen for returnMonthlyString switch, origin call from ConvertDateToChosenFrequencyStringMonths method"),
                };

                return monthlyString;
            }
            else if (optionCouponFrequency == CouponFrequency.Weekly)
            {
                var returnWeeklyString = optionDataTransform switch
                {
                    DataTransformOptions.UGsheet => weeklyString = string.Empty,
                    DataTransformOptions.SwapCopyFunction => weeklyString = " ??? first stub",
                    DataTransformOptions.Sluitbrief => weeklyString = string.Empty,
                    _ => throw new Exception("Unknown option chosen for returnWeeklyyString switch, origin call from ConvertDateToChosenFrequencyStringMonths method"),
                };

                return weeklyString;
            }
            else
            {
                throw new Exception("Unknown option chosen for CouponFrequency enum, origin call from ConvertDateToChosenFrequencyStringMonths method");
            }
        }

        public double? ParseAndCheckNotional(string notional)
        {
            CultureInfo dutchCulture = new CultureInfo("nl-NL");
            if (double.TryParse(notional, NumberStyles.Number, dutchCulture, out double notionalInDouble))
            {
                return notionalInDouble;
            }
            else
            {
                Console.WriteLine($"Failed to parse: {notional}");
                return null;
            }
        }

        public string FormatNotional(CultureInfo culture, string notional)
        {
            if (notional.Contains(","))
            {
                notional = notional.Replace(",", ".");
            }

            if (double.TryParse(notional, NumberStyles.Any, culture, out double parsedAmount))
            {
                return parsedAmount.ToString("N2", culture);
            }
            else
            {
                throw new ArgumentException("Invalid notional format for the specified culture.");
            }
        }

        public string GetFormattedCurrentDate()
        {
            DateTime currentDate = DateTime.Now;

            DayOfWeek day = currentDate.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                currentDate = currentDate.AddDays(2);
            }

            return currentDate.ToString("dd-MM-yyyy");
        }

        public string GetCurrentDateDDCouponDay()
        {
            DateTime currentDate = DateTime.Now;

            DayOfWeek day = currentDate.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                currentDate = currentDate.AddDays(2);
            }

            return currentDate.ToString("dd");
        }

        public string GetDateYesterday()
        {
            DateTime dateTimeYesterday = DateTime.Now.AddDays(-1);
            return dateTimeYesterday.ToString("dd-MM-yyyy");
        }

        public async Task<(string formattedStartDate, string formattedMaturityDate)> TransformStartAndMaturityDate(DataTransformOptions optionTransform, LanguageSetting optionLanguage, string startDate, string maturityDate)
        {
            string[] inputDateFormats = { "dd-MM-yyyy", "d-M-yyyy" };
            CultureInfo standardCultureInfo = _testSettings.CultureInfo;

            if (optionTransform == DataTransformOptions.Offermail)
            {
                string outputFormatOffermail = "dddd dd MMMM yyyy";
                if (optionLanguage == LanguageSetting.ENG)
                {
                    CultureInfo usCulture = new CultureInfo("en-US");
                    standardCultureInfo = usCulture;
                }

                var dtStartDate = DateTime.ParseExact(startDate, inputDateFormats, standardCultureInfo);
                var formattedStartDate = dtStartDate.ToString(outputFormatOffermail, standardCultureInfo);

                var dtMaturityDate = DateTime.ParseExact(maturityDate, inputDateFormats, standardCultureInfo);
                var formattedMaturityDate = dtMaturityDate.ToString(outputFormatOffermail, standardCultureInfo);
                await Task.CompletedTask;
                return (formattedStartDate, formattedMaturityDate);
            }
            else if (optionTransform == DataTransformOptions.Contracts)
            {
                string outputFormatContracts = "dddd d MMMM yyyy";

                var dtStartDate = DateTime.ParseExact(startDate, inputDateFormats, standardCultureInfo);
                var formattedStartDate = dtStartDate.ToString(outputFormatContracts, standardCultureInfo);

                var dtMaturityDate = DateTime.ParseExact(maturityDate, "dd-M-yyyy", standardCultureInfo);
                var formattedMaturityDate = dtMaturityDate.ToString(outputFormatContracts, standardCultureInfo);
                await Task.CompletedTask;
                return (formattedStartDate, formattedMaturityDate);
            }
            else
            {
                throw new ArgumentException("Unknown DataTransformOptions enum chosen, origin call from TransformStartAndMaturityDate()");
            }
        }

        public async Task<string> TransformDateToWrittenDate(DataTransformOptions option, string date)
        {
            if (option == DataTransformOptions.Offermail)
            {
                var dtDate = DateTime.ParseExact(date, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedDate = dtDate.ToString("dddd dd MMMM yyyy", _testSettings.CultureInfo);
                await Task.CompletedTask;
                return formattedDate;
            }
            else if (option == DataTransformOptions.Contracts)
            {
                var dtDate = DateTime.ParseExact(date, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedDate = dtDate.ToString("d MMMM yyyy", _testSettings.CultureInfo);
                await Task.CompletedTask;
                return formattedDate;
            }
            else if (option == DataTransformOptions.Sluitbrief)
            {
                var dtDate = DateTime.ParseExact(date, "dd-M-yyyy", _testSettings.CultureInfo);
                var formattedDate = dtDate.ToString("dddd dd MMMM yyyy", _testSettings.CultureInfo);
                await Task.CompletedTask;
                return formattedDate;
            }
            else
            {
                throw new ArgumentException("Unknown DataTransformOptions enum chosen, origin call from TransformDateToWrittenDate()");
            }
        }

        public async Task<string> CalculateFormattedMaturityDate(string startDate, int years, bool subtractDaysIfWeekendDay = false)
        {
            string[] dateFormats = { "dd-MM-yyyy", "d-M-yyyy" };

            DateTime currentStartDate = DateTime.ParseExact(startDate, dateFormats, CultureInfo.CurrentCulture);
            DateTime maturityDate = currentStartDate.AddYears(years);

            // Validating for TARGET2 dates
            List<DateTime> datesToValidate = new List<DateTime>
            {
                // Validate New Years day, labor day, christmas
                new DateTime(DateTime.Now.Year + years, 1, 1),
                new DateTime(DateTime.Now.Year + years, 5, 1),
                new DateTime(DateTime.Now.Year + years, 12, 25),
                new DateTime(DateTime.Now.Year + years, 12, 26),
            };

            foreach (DateTime date in datesToValidate)
            {
                if (maturityDate == date)
                {
                    maturityDate = maturityDate.AddDays(2);
                    break; // No need to continue looping once a match is found
                }
            }

            DayOfWeek dayMaturity = maturityDate.DayOfWeek;
            if (dayMaturity == DayOfWeek.Saturday || dayMaturity == DayOfWeek.Sunday)
            {
                int daysToAddOrSubtract = subtractDaysIfWeekendDay ? -2 : 2;
                maturityDate = maturityDate.AddDays(daysToAddOrSubtract);
            }

            string formattedMaturityDate = maturityDate.ToString("dd-MM-yyyy");
            await Task.CompletedTask;
            return formattedMaturityDate;
        }

        public string ConvertNumberToWords(string number)
        {
            long n = long.Parse(number);
            return n.ToWords(new CultureInfo("nl-NL"));
        }
    }
}