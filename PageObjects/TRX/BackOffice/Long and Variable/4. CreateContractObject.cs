using Aspose.Words;
using Aspose.Words.Saving;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System;
using System.Text;
using System.Text.RegularExpressions;
using TA.Playwright.Config;
using TA.Playwright.Drivers;
using TA.Playwright.Fields;

namespace TA.Playwright.PageObjects
{
    public interface ICreateContractObject
    {
        Task OpenReviewAgreementLink();

        Task<string> ConvertWordToHtml(string filePath);

        Task ClickSubmitButtonCreateAgreement();

        Task<(string bodyText, string footerText)> ExtractBodyandFooterFromDocxFile(string filePath);

        Task<string> DownloadFile(string pegaFile, string prefixName, string allNumber);
    }

    public class CreateContractObject : ICreateContractObject
    {
        public CreateContractObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings, ScenarioContext scenarioContext)
        {
            this.page = playwrightDriver.Page.Result;
            this.context = playwrightDriver.BrowserContext.Result;
            this.pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            this.pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
            _scenarioContext = scenarioContext;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;
        private readonly ScenarioContext _scenarioContext;

        public Task OpenReviewAgreementLink() => pageIpage2.GetByRole(AriaRole.Link, new() { Name = "Review agreement documents (Create agreement)" }).ClickAsync();

        public Task ClickSubmitButtonCreateAgreement() => pageIpage1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public async Task<string> ConvertWordToHtml(string filePath)
        {
            // Load the Word document
            Aspose.Words.Document doc = new Aspose.Words.Document(filePath);

            // Specify the folder where images will be saved
            string imagesFolder = System.IO.Path.GetDirectoryName(filePath);

            // Set the option to save images to the specified folder
            HtmlSaveOptions saveOptions = new HtmlSaveOptions(SaveFormat.Html);
            saveOptions.ImagesFolder = imagesFolder;

            MemoryStream htmlStream = new MemoryStream();
            doc.Save(htmlStream, saveOptions);

            // Read the HTML content
            htmlStream.Position = 0;
            StreamReader reader = new StreamReader(htmlStream);
            string htmlContent = reader.ReadToEnd();

            // Load HTML content into HtmlDocument
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Filter out HTML code and print each word text on a line
            var textBuilder = new StringBuilder();
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
            {
                string text = node.InnerText.Trim();
                if (text.Contains("&#xa0;") ||
                       text.Contains("Created with an evaluation copy of Aspose.Words. To discover the full versions of our APIs please visit: https://products.aspose.com/words/") ||
                       text.Contains("Evaluation Only. Created with Aspose.Words. Copyright 2003-2024 Aspose Pty Ltd."))
                {
                }
                else if (!string.IsNullOrEmpty(text))
                {
                    textBuilder.AppendLine(text);
                }
            }

            // Close streams
            reader.Close();
            htmlStream.Close();

            return textBuilder.ToString();
        }

        public async Task<(string bodyText, string footerText)> ExtractBodyandFooterFromDocxFile(string filePath)
        {
            string bodyText = string.Empty;
            string footerText = string.Empty;
            string htmlText = string.Empty;

            int paragraphIndex = 1;
            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                var bodyStringBuilder = new StringBuilder();
                var footerStringBuilder = new StringBuilder();

                if (document.MainDocumentPart != null)
                {
                    foreach (var paragraph in document.MainDocumentPart.Document.Body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                    {
                        bodyStringBuilder.AppendLine($"{paragraphIndex} +{paragraph.InnerText}");
                        paragraphIndex++;
                    }

                    foreach (var footer in document.MainDocumentPart.Document.Body.Elements<Footer>())
                    {
                        footerStringBuilder.Append(footer.InnerText);
                    }
                }
                else
                {
                    throw new NullReferenceException("document in CreateContractObject.cs is not initialized in method ExtractBodyandFooterFromDocxFile");
                }

                bodyText = bodyStringBuilder.ToString();
                footerText = footerStringBuilder.ToString();
            }

            await Task.CompletedTask;
            return (bodyText, footerText);
        }

        public async Task<string> DownloadFile(string pegaFile, string prefixName, string allNumber)
        {
            var currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = string.Empty;
            var environment = _testSettings.ApplicationEnvironment;
            var loanType = (string)_scenarioContext["savedLoanType"];
            EnumsForTRX.SelectProduct productType = (EnumsForTRX.SelectProduct)_scenarioContext["savedProductType"];
            string productTypeString = productType.ToString();
            var setFileName = pegaFile switch
            {
                "Cover Letter" => fileName = @$"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_Cover_Letter.docx",

                // "WSW Contract" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} WSW Contract {currentDateTime} {environment}.docx",
                "WSW Contract" => fileName = @$"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_WSW_Contract.docx",

                // "WSW Summary" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} WSW Summary {currentDateTime} {environment}.docx",
                "WSW Summary" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_Summary.docx",

                // "NWB Contract" => fileName = @$"{filePath}TC {prefixName} {allNumber} NWB Contract {currentDateTime} {environment}.docx",
                "NWB Contract" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract.docx",

                // "NWB Contract Annex Guarantor" => fileName = @$"{filePath}TC {prefixName} {allNumber} NWB Contract Guarantor {currentDateTime} {environment}.docx",
                "NWB Contract Annex Guarantor" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract_Annex_Guarantor.docx",

                // "NWB Contract Annex Conditions Precedent Public" => fileName = @$"{filePath}TC {prefixName} {allNumber} NWB Annex Conditions Precedent (public) {currentDateTime} {environment}.docx",
                "NWB Contract Annex Conditions Precedent Public" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract_Annex_Conditions_Precedent_Public.docx",

                // "NWB Contract Annex Conditions Precedent Private" => fileName = @$"{filePath}TC {prefixName} {allNumber} NWB Annex Conditions Precedent (private) {currentDateTime} {environment}.docx",
                "NWB Contract Annex Conditions Precedent Private" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract_Annex_Conditions_Precedent_Private.docx",
                "NWB Contract Annex Conditions Precedent Exceptional" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract_Annex_Conditions_Precedent_Exceptional.docx",

                // "DEPLO" => fileName = @$"{filePath}TC {prefixName} {allNumber} DEPLO {currentDateTime} {environment}.csv",
                "DEPLO" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_DEPLO.csv",

                // "SECMNWB" => fileName = @$"{filePath}TC {prefixName} {allNumber} SECMNWB {currentDateTime} {environment}.csv"
                "SECMNWB" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_SECMNWB.csv",

                // "SECM" => fileName = @$"{filePath}TC {prefixName} {allNumber} SECM {currentDateTime} {environment}.csv",
                "SECM" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_SECM.csv",

                // "BRED" => fileName = @$"{filePath}TC {prefixName} {allNumber} BRED {currentDateTime} {environment}.csv",
                "BRED" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_BRED.csv",

                // "WfZ Contract" => fileName = @$"{filePath}TC {prefixName} {allNumber} WfZ Contract {currentDateTime} {environment}.docx",
                "WfZ Contract" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_WfZ_Contract.docx",
                "Cover Letter RH" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_Cover_Letter_RH.docx",
                "WSW General Conditions" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_WSW_General_Conditions.pdf",
                "NWB Contract Annex Model Adjustment Request" => fileName = $@"{currentDateTime}_{environment}_{loanType}_{productTypeString}_TC_{prefixName}_{allNumber}_NWB_Contract_Annex_Model_Adjustment_Request.docx",

                _ => throw new Exception("Unknown option chosen for switch expression setFilePath"),
            };

            string downloadPath = System.IO.Path.Combine(_testSettings.DownloadFilePath, fileName);

            var downloadFile = await page.RunAndWaitForDownloadAsync(async () =>
            {
                (Task emptyTask, Task emptyTask2) chooseDownload = pegaFile switch
                {
                    "Cover Letter" => (pageIpage1.GetByText("NWB contract cover sheet.docx").WaitForAsync(), pageIpage1.GetByText("NWB contract cover sheet.docx").ClickAsync()),
                    "Cover Letter RH" => (pageIpage1.GetByText("Coverletter.docx").WaitForAsync(), pageIpage1.GetByText("Coverletter.docx").ClickAsync()),
                    "WSW Contract" => (pageIpage1.GetByText("WSW Contract.docx").WaitForAsync(), pageIpage1.GetByText("WSW Contract.docx").ClickAsync()),
                    "WSW Summary" => (pageIpage1.GetByText("WSW Summary.docx").WaitForAsync(), pageIpage1.GetByText("WSW Summary.docx").ClickAsync()),
                    "NWB Contract" => (pageIpage1.GetByText("NWB Contract.docx").WaitForAsync(), pageIpage1.GetByText("NWB Contract.docx").ClickAsync()),
                    "NWB Contract Annex Guarantor" => (pageIpage1.GetByText("NWB Contract Annex Guarantor.docx").WaitForAsync(), pageIpage1.GetByText("NWB Contract Annex Guarantor.docx").ClickAsync()),
                    "NWB Contract Annex Conditions Precedent Public" => (pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (public law entity).docx").WaitForAsync(), pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (public law entity).docx", new FrameLocatorGetByTextOptions { Exact = true }).ClickAsync()),
                    "NWB Contract Annex Conditions Precedent Private" => (pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (private law entity).docx").WaitForAsync(), pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (private law entity).docx", new FrameLocatorGetByTextOptions { Exact = true }).ClickAsync()),
                    "NWB Contract Annex Conditions Precedent Exceptional" => (pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (except. counterparty).docx").WaitForAsync(), pageIpage1.GetByRole(AriaRole.Link, new() { Name = "NWB Contract Annex Conditions Precedent (except. counterparty).docx" }).ClickAsync()),
                    "DEPLO" => (pageIpage1.GetByText(new Regex("DEPLO_")).First.WaitForAsync(), pageIpage1.GetByText(new Regex("DEPLO_")).First.ClickAsync()),
                    "SECMNWB" => (pageIpage1.GetByText(new Regex("SECM(NWB)")).First.WaitForAsync(), pageIpage1.GetByText(new Regex("SECM(NWB)")).First.ClickAsync()),
                    "SECM" => (pageIpage1.GetByText(new Regex("SECM_")).WaitForAsync(), pageIpage1.GetByText(new Regex("SECM_")).ClickAsync()),
                    "BRED" => (pageIpage1.GetByText(new Regex("BRED")).WaitForAsync(), pageIpage1.GetByText(new Regex("BRED")).ClickAsync()),
                    "WfZ Contract" => (pageIpage1.GetByText("WfZ Contract.docx").WaitForAsync(), pageIpage1.GetByText("WfZ Contract.docx").ClickAsync()),
                    "WSW General Conditions" => (pageIpage1.GetByText("WSW General Conditions").First.WaitForAsync(), pageIpage1.GetByText("WSW General Conditions").First.ClickAsync()),
                    "NWB Contract Annex Model Adjustment Request" => (pageIpage1.GetByText("NWB Contract Annex Model Adjustment Request").First.WaitForAsync(), pageIpage1.GetByText("NWB Contract Annex Model Adjustment Request").First.ClickAsync()),
                    _ => throw new Exception("Unknown option chosen for switch expression chooseDownload"),
                };
                await chooseDownload.emptyTask;
                await chooseDownload.emptyTask2;
            });
            await downloadFile.SaveAsAsync(downloadPath);
            return downloadPath;
        }
    }
}