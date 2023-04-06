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
        /// ªÒ»°Id
        /// </summary>
        [HttpGet]
        public string GetId()
        {
            return IdGener.GetLong().ToString();
        }
    }
}