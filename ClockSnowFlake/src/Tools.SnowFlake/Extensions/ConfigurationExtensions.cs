using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using ClockSnowFlake;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationExtensions
    {
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
