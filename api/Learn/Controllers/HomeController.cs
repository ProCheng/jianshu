using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.Controllers
{
    /// <summary>
    /// 主页
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]   //用这个特性来隐藏接口
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 第一个方法
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpGet]
        // 方法
        public IActionResult Get(int i)
        {
            
            return Ok();
        }
        [HttpPost("a")]
        public IActionResult Post([FromBody] Student stu)
        {
            return Ok();
        }
    }
}
