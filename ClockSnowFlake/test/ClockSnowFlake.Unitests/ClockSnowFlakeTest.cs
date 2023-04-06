using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClockSnowFlake.Unitests
{
    public class ClockSnowFlakeTest
    {

        public ClockSnowFlakeTest()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var config = configurationBuilder.Build();
            //注意：工作机器ID不能重复
            config.AddSnowFlakeId(x => x.WorkId = 2);
        }

        [Fact]
        public void Id_Generate_Should_Be_Succeed()
        {
            var id = IdGener.GetLong();
            Assert.True(id > 0);
        }
    }
}
