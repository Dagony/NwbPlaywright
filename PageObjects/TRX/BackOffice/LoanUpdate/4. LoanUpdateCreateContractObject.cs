using Aspose.Words;
using Aspose.Words.Saving;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;
using TA.Playwright.Config;
using TA.Playwright.Drivers;

namespace TA.Playwright.PageObjects
{
    public interface ILoanUpdateCreateContractObject
    {
        Task OpenCreateContractLink();

        Task<string> ConvertWordToHtml(string filePath);

        Task ClickSubmitButtonCreateAgreement();

        Task<(string bodyText, string footerText)> ExtractBodyandFooterFromDocxFile(string filePath);

        Task<string> DownloadFile(string pegaFile, string prefixName, string allNumber);
    }

    public class LoanUpdateCreateContractObject : ILoanUpdateCreateContractObject
    {
        public LoanUpdateCreateContractObject(IPlaywrightDriver playwrightDriver, IBasePageObject basePageObject, TestSettings testSettings)
        {
            page = playwrightDriver.Page.Result;
            context = playwrightDriver.BrowserContext.Result;
            pageIpage1 = page.FrameLocator("iframe[name =\"PegaGadget1Ifr\"]");
            pageIpage2 = page.FrameLocator("iframe[name =\"PegaGadget2Ifr\"]");
            _testSettings = testSettings;
        }

        private readonly IPage page;
        private readonly IBrowserContext context;
        private readonly IFrameLocator pageIpage1;
        private readonly IFrameLocator pageIpage2;
        private readonly TestSettings _testSettings;

        public Task OpenCreateContractLink() => pageIpage1.GetByRole(AriaRole.Link, new() { Name = "Create contract manually (Agreement)" }).ClickAsync();

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
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var filepathDownloadedFile = string.Empty;
            var filePath = _testSettings.DownloadFilePath;

            var setFilePath = pegaFile switch
            {
                "Cover Letter" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} Cover Letter {currentDateTime}.docx",
                "WSW Contract" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} WSW Contract {currentDateTime}.docx",
                "WSW Summary" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} WSW Summary {currentDateTime}.docx",
                "NWB Contract" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} NWB Contract {currentDateTime}.docx",
                "NWB Contract Annex Guarantor" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} NWB Contract Guarantor {currentDateTime}.docx",
                "NWB Contract Annex Conditions Precedent Public" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} NWB Annex Conditions Precedent (public) {currentDateTime}.docx",
                "NWB Contract Annex Conditions Precedent Private" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} NWB Annex Conditions Precedent (private) {currentDateTime}.docx",
                "DEPLO" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} DEPLO {currentDateTime}.csv",
                "SECMNWB" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} SECMNWB {currentDateTime}.csv",
                "SECM" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} SECM {currentDateTime}.csv",
                "BRED" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} BRED {currentDateTime}.csv",
                "WfZ Contract" => filepathDownloadedFile = @$"{filePath}TC {prefixName} {allNumber} WfZ Contract {currentDateTime}.docx",
                _ => throw new Exception("Unknown option chosen for switch expression setFilePath"),
            };

            var downloadFile = await page.RunAndWaitForDownloadAsync(async () =>
            {
                var chooseDownload = pegaFile switch
                {
                    "Cover Letter" => pageIpage1.GetByText("NWB contract cover sheet.docx").ClickAsync(),
                    "WSW Contract" => pageIpage1.GetByText("WSW Contract.docx").ClickAsync(),
                    "WSW Summary" => pageIpage1.GetByText("WSW Summary.docx").ClickAsync(),
                    "NWB Contract" => pageIpage1.GetByText("NWB Contract.docx").ClickAsync(),
                    "NWB Contract Annex Guarantor" => pageIpage1.GetByText("NWB Contract Annex Guarantor.docx").ClickAsync(),
                    "NWB Contract Annex Conditions Precedent Public" => pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (public law entity).docx", new FrameLocatorGetByTextOptions { Exact = true }).ClickAsync(),
                    "NWB Contract Annex Conditions Precedent Private" => pageIpage1.GetByText("NWB Contract Annex Conditions Precedent (private law entity).docx", new FrameLocatorGetByTextOptions { Exact = true }).ClickAsync(),
                    "DEPLO" => pageIpage1.GetByText(new Regex("DEPLO_")).ClickAsync(),
                    "SECMNWB" => pageIpage1.GetByText(new Regex("SECM(NWB)")).ClickAsync(),
                    "SECM" => pageIpage1.GetByText(new Regex("SECM_")).ClickAsync(),
                    "BRED" => pageIpage1.GetByText(new Regex("BRED")).ClickAsync(),
                    "WfZ Contract" => pageIpage1.GetByText("WfZ Contract.docx").ClickAsync(),
                    _ => throw new Exception("Unknown option chosen for switch expression chooseDownload"),
                };
                await chooseDownload;
            });
            await downloadFile.SaveAsAsync(filepathDownloadedFile);
            //////// TODO: check if file exists
            File.Exists(filepathDownloadedFile).Should().BeTrue();
            return filepathDownloadedFile;
        }
    }
}