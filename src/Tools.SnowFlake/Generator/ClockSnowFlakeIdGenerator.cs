namespace ClockSnowFlake
{
    /// <summary>
    /// 基于时钟序列的 雪花算法ID 生成器
    /// </summary>
    public class ClockSnowFlakeIdGenerator : ILongGenerator
    {
        public ClockSnowFlakeIdGenerator()
        {
            var workerId = SnowFakeOptionsConst.WorkId;
            _id = new ClockSnowflakeId(workerId);
        }

        /// <summary>
        /// 基于时钟序列的雪花算法ID
        /// </summary>
        private readonly ClockSnowflakeId _id;

        /// <summary>
        /// 获取<see cref="ClockSnowFlakeIdGenerator"/>类型的实例
        /// </summary>
        public static ClockSnowFlakeIdGenerator Current { get; set; } = new ClockSnowFlakeIdGenerator();

        /// <summary>
        /// 创建ID
        /// </summary>
        /// <returns></returns>
        public long Create()
        {
            return _id.NextId();
        }
    }
}
