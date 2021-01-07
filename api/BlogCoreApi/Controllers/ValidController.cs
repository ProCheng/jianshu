using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.RegularExpressions;

namespace BlogCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public ValidController(IDistributedCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 邮箱验证控制器
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("Email")]
        public IActionResult Email(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(new {message= "缺少邮箱",state=400 });
            StringBuilder code = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
               code.Append(random.Next(0, 10));
            }
            // 判断邮箱是否正确
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (regex.IsMatch(email) && EmailService.SendEmail(code.ToString(), email))
            {
                var guid = Guid.NewGuid();
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.SetString($"EmailCode:{guid.ToString()}",$"{code}",options);
                return Ok(new {state=200,validId=guid,message="验证码已发送到你的邮箱" });
            }
            else
            {
                return BadRequest(new {message= "邮箱格式错误",state=400 });
            }
        }
    }
}
