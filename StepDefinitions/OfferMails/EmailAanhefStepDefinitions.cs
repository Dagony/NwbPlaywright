using Reqnroll;
using System;
using NwbPlaywright.Config;
using NwbPlaywright.DataTransforms;
using NwbPlaywright.Drivers;
using NwbPlaywright.Extensions;
using NwbPlaywright.Models;
using NwbPlaywright.PageObjects;
using static NwbPlaywright.Fields.EnumsForTRX;

namespace NwbPlaywright.StepDefinitions
{
    [Binding]
    public class EmailAanhefStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestSettings _testSettings;
        private readonly IBasePageObject _basePageObject;
        private readonly ISharedDataTransforms _sharedDataTransforms;
        private readonly ILoginPageObject _loginPageObject;
        private readonly INewCaseObject _newCaseObject;
        private readonly IFinalSpreadObject _finalSpreadObject;
        private readonly IWinLostCorrectObject _winLostCorrectObject;
        private readonly ILocator _includeInMailBoxLocator;
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly ISharedDataIndividuals _sharedDataIndividuals;
        private readonly ILoanUpdateFinalSpreadObject _loanUpdateFinalSpreadObject;
        private readonly IShortModel _shortModel;
        private readonly IFixedModel _fixedModel;
        private readonly IVariableModel _variableModel;
        private readonly ISharedModel _sharedModel;
        private readonly string _setCDDandDealDateTime;
        private readonly string _currentFormattedDate;
        private readonly string _notionalWithoutDouble;
        private readonly Enum _randomReferenceRate;
        private readonly Task<string> _formattedMaturityDate;
        private readonly Enum _randomAmortisationType;
        private readonly Enum _randomDayCount;
        private readonly Enum _randomAmortFrequency;
        private readonly Enum _randomCouponFreqType;

        public EmailAanhefStepDefinitions(
            IPlaywrightDriver playwrightDriver,
            ISharedDataIndividuals sharedDataIndividuals,
            ScenarioContext scenarioContext,
            TestSettings testSettings,
            IBasePageObject basePageObject,
            IShortModel shortModel,
            IFixedModel fixedModel,
            IVariableModel variableModel,
            ISharedModel sharedModel,
            ILoginPageObject loginPageObject,
            INewCaseObject newCaseObject,
            IFinalSpreadObject finalSpreadObject,
            IWinLostCorrectObject winLostCorrectObject,
            IBillOfClosureObject billOfClosureObject,
            ICheckDossierCompleteObject checkDossierCompleteObject,
            ISendConfirmationObject sendConfirmationObject,
            ISharedDataTransforms sharedDataTransforms,
            ILoanUpdateFinalSpreadObject loanUpdateFinalSpreadObject)
        {
            _playwrightDriver = playwrightDriver;
            _scenarioContext = scenarioContext;
            _testSettings = testSettings;
            _basePageObject = basePageObject;
            _sharedDataTransforms = sharedDataTransforms;
            _loginPageObject = loginPageObject;
            _newCaseObject = newCaseObject;
            _finalSpreadObject = finalSpreadObject;
            _winLostCorrectObject = winLostCorrectObject;
            _fixedModel = fixedModel;
            _variableModel = variableModel;
            _shortModel = shortModel;
            _sharedModel = sharedModel;
            _includeInMailBoxLocator = _playwrightDriver.Page.Result.FrameLocator("iframe[name=\"PegaGadget1Ifr\"]").GetByLabel("Include in mail");
            _sharedDataIndividuals = sharedDataIndividuals;
            _loanUpdateFinalSpreadObject = loanUpdateFinalSpreadObject;
            _setCDDandDealDateTime = _sharedDataTransforms.GetDateYesterday();
            _currentFormattedDate = _sharedDataTransforms.GetFormattedCurrentDate();
            _randomAmortisationType = EnumHelper.GetRandomEnumValue(typeof(AmortisationType));
            _randomDayCount = EnumHelper.GetRandomEnumValue(typeof(DayCount));
            _randomAmortFrequency = EnumHelper.GetRandomEnumValue(typeof(AmortisationFrequency));
            _randomCouponFreqType = EnumHelper.GetRandomEnumValue(typeof(CouponFrequency));
            _notionalWithoutDouble = _basePageObject.GetRandomNotional(100, 63900000);
            _randomReferenceRate = EnumHelper.GetRandomEnumValue(typeof(ReferenceRate));
            _formattedMaturityDate = _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, 45, false);
        }

        /// Parameters for testcases
        private string firstCouponDate = "15-06-2027";
        private string lastCouponDate = "11-01-2033";
        private string interestRRDate = "12-01-2034";
        private string couponday = "22";
        private string numbOfAmort = "10";

        [Given("I create a random fixed case with all obligatory fields filled with TC {string}")]
        public async Task GivenICreateARandomFixedCaseWithAllObligatoryFieldsFilledWithTC(string tC)
        {
            string couponFreq = EnumHelper.GetEnumDisplayName(_randomCouponFreqType);
            string amortisationType = EnumHelper.GetEnumDisplayName(_randomAmortisationType);
            bool swap = false;

            string daycount = EnumHelper.GetEnumDisplayName(_randomDayCount);
            string amortFreq = EnumHelper.GetEnumDisplayName(_randomAmortFrequency);

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = _notionalWithoutDouble;
            _scenarioContext["savedTC"] = tC;

            await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, _notionalWithoutDouble);
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for short case: {lalCaseNumber} ");

            if (couponFreq == "Weekly")
            {
                await _fixedModel.FixedCaseSetDateFields(_currentFormattedDate, await _formattedMaturityDate, couponFreq: couponFreq, firstCouponDate, lastCouponDate, interestRRDate, couponDayOption: CouponDay.Wednesday);
            }
            else
            {
                await _fixedModel.FixedCaseSetDateFields(_currentFormattedDate, await _formattedMaturityDate, couponFreq: couponFreq, firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponday);
            }

            // implement other methods!!  with randoms
            if (amortisationType != "Bullet")
            {
                await _fixedModel.FixedCaseSetAlternativeFields(daycount, swap, amortisationType, numbOfAmort, amortFreq);
            }
            else
            {
                await _fixedModel.FixedCaseSetAlternativeFields(daycount, swap, amortisationType);
            }

            await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("11", "11", "3", "FIXED", lalCaseNumber, setSpreadMessages: true);
        }

        [Given("I create a random loan update fixed case with all obligatory fields filled with TC {string}")]
        public async Task GivenICreateARandomLoanUpdateFixedCaseWithAllObligatoryFieldsFilledWithTC(string tC)
        {
            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, 45, false);

            string couponFreq = await _loanUpdateFinalSpreadObject.GetValueOfCouponFrequency();
            string amortisationType = EnumHelper.GetEnumDisplayName(_randomAmortisationType);
            bool swap = false;

            string daycount = EnumHelper.GetEnumDisplayName(_randomDayCount);
            string amortFreq = EnumHelper.GetEnumDisplayName(_randomAmortFrequency);

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = _notionalWithoutDouble;
            _scenarioContext["savedTC"] = tC;

            await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, _notionalWithoutDouble);
            await _finalSpreadObject.SpreadCheckBy(false);
            await _finalSpreadObject.SpreadCheckSetRemarks($"Test text for SpreadCheck for short case: {lalCaseNumber} ");

            if (couponFreq == "Weekly")
            {
                await _fixedModel.LoanUpdateFixedCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, couponFreq: couponFreq, firstCouponDate, lastCouponDate, interestRRDate, couponDayOption: CouponDay.Wednesday);
            }
            else
            {
                await _fixedModel.LoanUpdateFixedCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, couponFreq: couponFreq, firstCouponDate, lastCouponDate, interestRRDate, couponDay: couponday);
            }

            // implement other methods!!  with randoms
            if (amortisationType != "Bullet")
            {
                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(swap, amortisationType, numbOfAmort, amortFreq);
            }
            else
            {
                await _fixedModel.LoanUpdateFixedCaseSetAlternativeFields(swap, amortisationType);
            }

            await _fixedModel.FixedCaseSetSpreadFieldsTwoEyes("11", "11", "3", "FIXED", lalCaseNumber, setSpreadMessages: true);
        }

        [Given("I create a random variable case with all obligatory fields filled with TC {string}")]
        public async Task GivenICreateARandomVariableCaseWithAllObligatoryFieldsFilledWithTC(string tC)
        {
            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, 41, false);

            string referenceRate = EnumHelper.GetEnumDisplayName(_randomReferenceRate);

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = _notionalWithoutDouble;
            _scenarioContext["savedTC"] = tC;

            await _sharedModel.CaseSetOfferFields(_setCDDandDealDateTime, _setCDDandDealDateTime, _notionalWithoutDouble);

            if (referenceRate == "1wE")
            {
                await _variableModel.VariableCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, referenceRate, true, firstCouponDate, lastCouponDate, interestRRDate, couponDayOption: CouponDay.Thursday);
            }
            else
            {
                await _variableModel.VariableCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, referenceRate, firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: couponday);
            }

            await _variableModel.VariableSetSpreadFieldsTwoEyes(referenceRate, "VARIABLE", lalCaseNumber, "22", "22", setSpreadMessages: true, "22", "22");
        }

        [Given("I create a random deposit case with all obligatory fields filled with TC {string}")]
        public async Task GivenICreateARandomDepositCaseWithAllObligatoryFieldsFilledWithTC(string tC)
        {
            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, 1, subtractDaysIfWeekendDay: true);
            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = _notionalWithoutDouble;
            _scenarioContext["savedTC"] = tC;

            await _sharedModel.DepositCaseSetOfferFields(_setCDDandDealDateTime, _notionalWithoutDouble);

            await _shortModel.LoanUpdateShortCaseSetDateFields(_currentFormattedDate, formattedMaturityDate);

            await _shortModel.ShortCaseSetSpreadFieldsTwoEyes("5,759", "3,759", "3,5", "DEPOSIT", lalCaseNumber, setSpreadMessages: true);
        }

        [Given("I create a random loan update variable case with all obligatory fields filled with TC {string}")]
        public async Task GivenICreateARandomLoanUpdateVariableCaseWithAllObligatoryFieldsFilledWithTC(string tC)
        {
            var formattedMaturityDate = await _sharedDataTransforms.CalculateFormattedMaturityDate(_currentFormattedDate, 40, false);

            string referenceRate = EnumHelper.GetEnumDisplayName(_randomReferenceRate);

            var lalCaseNumber = await _finalSpreadObject.GetCaseID();
            _scenarioContext["savedlalCaseID"] = lalCaseNumber;
            _scenarioContext["savedNotional"] = _notionalWithoutDouble;
            _scenarioContext["savedTC"] = tC;

            await _sharedModel.LoanUpdateCaseSetOfferFields(_setCDDandDealDateTime, _notionalWithoutDouble);

            if (referenceRate == "1wE")
            {
                await _variableModel.LoanUpdateVariableCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, referenceRate, true, firstCouponDate, lastCouponDate, interestRRDate, couponDayOption: CouponDay.Thursday);
            }
            else
            {
                await _variableModel.LoanUpdateVariableCaseSetDateFields(_currentFormattedDate, formattedMaturityDate, referenceRate, firstCouponDate: firstCouponDate, lastCouponDate: lastCouponDate, interestRRDate: interestRRDate, couponDay: couponday);
            }

            await _variableModel.LoanUpdateVariableSetSpreadFieldsTwoEyes(referenceRate, "VARIABLE", lalCaseNumber, "22", "22", setSpreadMessages: true, "22", "22");
        }

        [Then("the email aanhef should be the name of the selected random individual")]
        public async Task ThenTheEmailAanhefShouldBeTheNameOfTheSelectedRandomIndividual()
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var filePath = _testSettings.DownloadFilePath;
            var loanType = (string)_scenarioContext["savedLoanType"];
            var actualTable = (string)_scenarioContext["savedActualTable"];
            var actualAanhef = (string)_scenarioContext["savedActualAanhef"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];
            var numberOfCounterpartyIndividuals = (int)_scenarioContext["savedNumbOfIndividuals"];

            for (int i = 0; i < _sharedDataIndividuals.Customers.Count; i++)
            {
                if (_sharedDataIndividuals.Customers[i].EmailIndividual is not null)
                {
                    string emailAdress = _sharedDataIndividuals.Customers[i].EmailIndividual!.ToString();
                    actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
                }
                else
                {
                    throw new Exception($"Emailadress of individual (origin: _sharedDataIndividual.Customers[{i}].EmailIndividual)");
                }
            }

            _sharedDataIndividuals.Customers.Count.Should().Be(1);
            actualAanhef.Should().ContainEquivalentOf($"Beste {_sharedDataIndividuals.Customers[0].FullName},");

            Console.WriteLine($"Test excuted for TC: {prefixName} on {currentDateTime}");
            Console.WriteLine($"LAL-case in PEGA: {prefixName} succesfully executed.");
            Console.WriteLine($"Printing Aanhef below:");
            Console.WriteLine($"----------------------------------------------------");
            Console.WriteLine(actualAanhef);
            Console.WriteLine($"----------------------------------------------------");
            Console.WriteLine($"Printing actual email addressees below:");
            Console.WriteLine($"----------------------------------------------------");
            Console.WriteLine(actualAddresseesEmailText);
            Console.WriteLine($"----------------------------------------------------");

            await Task.CompletedTask;
        }

        [Then("the email aanhef should be {string}")]
        public async Task ThenTheEmailAanhefShouldBe(string expectedAanhef)
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var actualAanhef = (string)_scenarioContext["savedActualAanhef"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];

            for (int i = 0; i < _sharedDataIndividuals.Customers.Count; i++)
            {
                if (_sharedDataIndividuals.Customers[i].EmailIndividual is not null)
                {
                    string emailAdress = _sharedDataIndividuals.Customers[i].EmailIndividual!.ToString();
                    actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
                }
                else
                {
                    throw new Exception($"Emailadress of individual (origin: _sharedDataIndividual.Customers[{i}].EmailIndividual)");
                }
            }

            actualAanhef.Should().Contain(expectedAanhef);
            await Task.CompletedTask;
        }

        [Then("the deposit email aanhef should be {string}")]
        public async Task ThenTheDepositEmailAanhefShouldBeAndTheEnglishAanhefShouldBe(string expectedAanhefNL)
        {
            var lalCaseNumber = (string)_scenarioContext["savedlalCaseID"];
            var prefixName = (string)_scenarioContext["savedTC"];
            var currentDateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var actualAanhef = (string)_scenarioContext["savedActualAanhef"];
            var actualAddresseesEmailText = (string)_scenarioContext["savedActualAddressees"];

            for (int i = 0; i < _sharedDataIndividuals.Customers.Count; i++)
            {
                if (_sharedDataIndividuals.Customers[i].EmailIndividual is not null)
                {
                    string emailAdress = _sharedDataIndividuals.Customers[i].EmailIndividual!.ToString();
                    actualAddresseesEmailText.Should().ContainEquivalentOf(emailAdress);
                }
                else
                {
                    throw new Exception($"Email address of individual (origin: _sharedDataIndividual.Customers[{i}].EmailIndividual)");
                }
            }

            actualAanhef.Should().Contain(expectedAanhefNL);
            await Task.CompletedTask;
        }

        [When("I add in an extra emailadress with name {string}")]
        public async Task WhenIAddInAnExtraEmailadressWithName(string emailadress)
        {
            var loanType = (string)_scenarioContext["savedLoanType"];
            await _sharedModel.SetExtraEmailFields(emailadress);
        }
    }
}