using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Windsor;

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using TA.Playwright.Config;
using TA.Playwright.DataTransforms;
using TA.Playwright.Drivers;
using TA.Playwright.Extensions;
using TA.Playwright.Models;
using TA.Playwright.PageObjects;
using TA.Playwright.PageObjects.CLM;
using TA.Playwright.PageObjects.CLM.Individual;
using TA.Playwright.PageObjects.TRX;
using TA.Playwright.PageObjects.TRX.FrontOffice;
using TA.Playwright.PageObjects.TRX.Treasury;
using TA.Playwright.Support;

namespace TA.Playwright
{
    public class Startup
    {
        private static readonly IConfiguration Config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .AddEnvironmentVariables()
                    .Build();

        [ScenarioDependencies]
        public static IWindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();

            // container.AddTestSettings2(config);
            container.AddTestSettings2(Config);
            container.Register(Castle.MicroKernel.Registration.Component.For<IPlaywrightDriver>().ImplementedBy<PlaywrightDriver>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IPlaywrightDriverInitializer>().ImplementedBy<PlaywrightDriverInitializer>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISharedDataIndividuals>().ImplementedBy<SharedDataIndividuals>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IBasePageObject>().ImplementedBy<BasePageObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoginPageObject>().ImplementedBy<LoginPageObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<INewCaseObject>().ImplementedBy<NewCaseObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IFixedModel>().ImplementedBy<FixedModel>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IFinalSpreadObject>().ImplementedBy<FinalSpreadObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IWinLostCorrectObject>().ImplementedBy<WinLostCorrectObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IBillOfClosureObject>().ImplementedBy<BillOfClosureObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanDataApprovalObject>().ImplementedBy<LoanDataApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IValidateRequestObject>().ImplementedBy<ValidateRequestObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISurePayCheckObject>().ImplementedBy<SurePayCheckObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ICreateContractObject>().ImplementedBy<CreateContractObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IAgreementReviewObject>().ImplementedBy<AgreementReviewObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ICheckGuarantorDocumentObject>().ImplementedBy<CheckGuarantorDocumentObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IGuarantorApprovalObject>().ImplementedBy<GuarantorApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISendAgreementObject>().ImplementedBy<SendAgreementObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IClientApprovalObject>().ImplementedBy<ClientApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IProcessClientResponseObject>().ImplementedBy<ProcessClientResponseObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILegalReviewObject>().ImplementedBy<LegalReviewObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ICheckDossierCompleteObject>().ImplementedBy<CheckDossierCompleteObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISendConfirmationObject>().ImplementedBy<SendConfirmationObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISharedModel>().ImplementedBy<SharedModel>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IVariableModel>().ImplementedBy<VariableModel>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IShortModel>().ImplementedBy<ShortModel>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ISharedDataTransforms>().ImplementedBy<SharedDataTransforms>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IFixedDataTransforms>().ImplementedBy<FixedDataTransforms>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateFinalSpreadObject>().ImplementedBy<LoanUpdateFinalSpreadObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateWinLostCorrectObject>().ImplementedBy<LoanUpdateWinLostCorrectObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateBillOfClosureObject>().ImplementedBy<LoanUpdateBillOfClosureObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateLoanDataApprovalObject>().ImplementedBy<LoanUpdateLoanDataApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateValidateRequestObject>().ImplementedBy<LoanUpdateValidateRequestObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateCollectDocumentInformationObject>().ImplementedBy<LoanUpdateCollectDocumentInformationObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateCreateContractObject>().ImplementedBy<LoanUpdateCreateContractObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateAgreementReviewObject>().ImplementedBy<LoanUpdateAgreementReviewObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateCheckGuarantorDocumentObject>().ImplementedBy<LoanUpdateCheckGuarantorDocumentObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateGuarantorApprovalObject>().ImplementedBy<LoanUpdateGuarantorApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateSendAgreementObject>().ImplementedBy<LoanUpdateSendAgreementObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateClientApprovalObject>().ImplementedBy<LoanUpdateClientApprovalObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateProcessClientResponseObject>().ImplementedBy<LoanUpdateProcessClientResponseObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateLegalReviewObject>().ImplementedBy<LoanUpdateLegalReviewObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ILoanUpdateCheckDossierCompleteObject>().ImplementedBy<LoanUpdateCheckDossierCompleteObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IUpdatePricingGridObject>().ImplementedBy<UpdatePricingGridObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IVariableDataTransforms>().ImplementedBy<VariableDataTransforms>());
            container.Register(Castle.MicroKernel.Registration.Component.For<ICaptureSwapObject>().ImplementedBy<CaptureSwapObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IRejectionObject>().ImplementedBy<RejectionObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<IBasePageCLMObject>().ImplementedBy<BasePageCLMObject>());
            container.Register(Castle.MicroKernel.Registration.Component.For<INewIndividualEntityObject>().ImplementedBy<NewIndividualEntityObject>());

            return container;
        }

        // public static IServiceCollection CreateServices()
        // {
        //    var services = new ServiceCollection();
        //    services
        //        .AddTestSettings(config)
        //        //.AddSingleton(ConfigReader.Readconfig())
        //        .AddSingleton<ISharedDataIndividuals, SharedDataIndividuals>()
        //        .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
        //        .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
        //       // .AddScoped<ITestFixtureBase, TestFixtureBase>()
        //        .AddScoped<IBasePageObject, BasePageObject>()
        //        .AddScoped<IFixedModel, FixedModel>()
        //        .AddScoped<ILoginPageObject, LoginPageObject>()
        //        .AddScoped<INewCaseObject, NewCaseObject>()

        // .AddScoped<IFinalSpreadObject, FinalSpreadObject>()
        //        .AddScoped<IWinLostCorrectObject, WinLostCorrectObject>()
        //        .AddScoped<IBillOfClosureObject, BillOfClosureObject>()
        //        .AddScoped<ILoanDataApprovalObject, LoanDataApprovalObject>()
        //        .AddScoped<IValidateRequestObject, ValidateRequestObject>()
        //        .AddScoped<ISurePayCheckObject, SurePayCheckObject>()
        //        .AddScoped<ICreateContractObject, CreateContractObject>()
        //        .AddScoped<IAgreementReviewObject, AgreementReviewObject>()
        //        .AddScoped<ICheckGuarantorDocumentObject, CheckGuarantorDocumentObject>()
        //        .AddScoped<IGuarantorApprovalObject, GuarantorApprovalObject>()
        //        .AddScoped<ISendAgreementObject, SendAgreementObject>()

        // .AddScoped<IClientApprovalObject, ClientApprovalObject>()

        // .AddScoped<IProcessClientResponseObject, ProcessClientResponseObject>()
        //        .AddScoped<ILegalReviewObject, LegalReviewObject>()
        //        .AddScoped<ICheckDossierCompleteObject, CheckDossierCompleteObject>()
        //        .AddScoped<ISendConfirmationObject, SendConfirmationObject>();

        // return services;
        // }
    }
}