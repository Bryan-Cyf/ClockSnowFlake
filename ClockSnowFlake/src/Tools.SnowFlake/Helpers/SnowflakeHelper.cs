using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ClockSnowFlake
{
    internal class SnowflakeHelper
    {
        /// <summary>
        /// 获取随机工作Id
        /// </summary>
        /// <returns></returns>
        public static long GetRandomWorkerId(long maxWorkerId)
        {
            //long? workerId = Math.Abs(Environment.MachineName.GetHashCode()) % maxWorkerId;
            var workerId = GetLastIpNum();

            if (workerId == null)
            {
                /**
                 * 本系统SnowflakeIdWorker的workerId范围为0-1023，ip最后一段数字最大为255
                 * 一旦获取本机ip失败，就取300-1023之间的随机数做为workerId
                 */
                workerId = CreateNumber(300, (int)(maxWorkerId % int.MaxValue));
            }

            return workerId.Value;
        }

        /// <summary>
        /// 生成固定范围的随机数
        /// </summary>
        private static int CreateNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(max) % (max - min + 1) + min;
        }

        /// <summary>
        /// 获取ip串中最后的数字
        /// </summary>
        private static long? GetLastIpNum()
        {
            string ip = "";

            try
            {
                ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();
            }
            catch
            {

            }

            if (ip == "")
            {
                return null;
            }


            string lastNum = ip.Substring(ip.LastIndexOf('.') + 1);
            return long.Parse(lastNum);
        }
    }
}
