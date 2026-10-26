using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using NwbPlaywright.Config;
using Component = Castle.MicroKernel.Registration.Component;

namespace NwbPlaywright.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestSettings(this IServiceCollection services, IConfiguration configuration)
        {
            TestSettings testSettings = configuration.GetRequiredSection("TestSettings").Get<TestSettings>()!;

            services.AddSingleton(testSettings);

            return services;
        }

        public static IWindsorContainer AddTestSettings2(this IWindsorContainer container, IConfiguration configuration)
        {
            TestSettings testSettings = configuration.GetRequiredSection("TestSettings").Get<TestSettings>()!;

            container.Register(Component.For<TestSettings>().Instance(testSettings));

            return container;
        }
    }
}