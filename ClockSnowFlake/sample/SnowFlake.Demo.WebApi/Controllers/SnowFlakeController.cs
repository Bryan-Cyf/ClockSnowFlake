using Microsoft.AspNetCore.Mvc;
using ClockSnowFlake;

namespace ClockSnowFlake.Demo.WebApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SnowFlakeController : ControllerBase
    {
        public SnowFlakeController()
        {

        }

        /// <summary>
        /// ��ȡId
        /// </summary>
        [HttpGet]
        public string GetId()
        {
            return IdGener.GetLong().ToString();
        }
    }
}