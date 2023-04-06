using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using ClockSnowFlake;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSnowFlakeId(this IServiceCollection services, IConfiguration configuration, Action<SnowFakeOptions> configure = null, string sectionName = null)
        {
            sectionName ??= SnowFakeOptions.SectionName;
            services.AddOptions<SnowFakeOptions>();
            services.PostConfigure<SnowFakeOptions>(x =>
            {
                configure?.Invoke(x);
            });
            var options = configuration.GetSection(sectionName).Get<SnowFakeOptions>() ?? new SnowFakeOptions();
            configure?.Invoke(options);
            SnowFakeOptionsConst.WorkId = options.WorkId;
            Console.WriteLine($"SnowWorkId:{SnowFakeOptionsConst.WorkId}");
            return services;
        }

        public static IServiceCollection AddSnowFlakeId(this IServiceCollection services, Action<SnowFakeOptions> configure)
        {
            services.AddOptions<SnowFakeOptions>().Configure(configure);
 
            var options = new SnowFakeOptions();
            configure.Invoke(options);

            SnowFakeOptionsConst.WorkId = options.WorkId;
            Console.WriteLine($"SnowWorkId:{SnowFakeOptionsConst.WorkId}");
            return services;
        }

        public static void AddSnowFlakeId(this IConfiguration configuration, Action<SnowFakeOptions> configure = null, string sectionName = null)
        {
            sectionName ??= SnowFakeOptions.SectionName;
            var options = configuration.GetSection(sectionName).Get<SnowFakeOptions>() ?? new SnowFakeOptions();
            configure?.Invoke(options);
            SnowFakeOptionsConst.WorkId = options.WorkId;
            Console.WriteLine($"SnowWorkId:{SnowFakeOptionsConst.WorkId}");
        }
    }
}
