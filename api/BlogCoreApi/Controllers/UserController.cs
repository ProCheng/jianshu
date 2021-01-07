using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCoreDomain.Model;
using BlogCoreImplement.Services;
using BlogCoreInterface.Services ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace BlogCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 颁发token
        /// </summary>
        private readonly IAuthenticateService _Authenticate;
        private readonly IUserService _UserService;
        private readonly IDistributedCache _Cache;
        public UserController(IAuthenticateService authenticate,IUserService userService,IDistributedCache cache)
        { 
            _Authenticate = authenticate;
            _UserService = userService;
            _Cache = cache;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = "";
            if (user != null && user.Account != null && user.Pwd != null)
            {
                _Authenticate.IsAuthenticated(user, out token);
                if (string.IsNullOrEmpty(token))
                    return new JsonResult(new {message="账号密码错误",state=400});
                return new JsonResult(new { message = "登录成功", state = 200,token});
            }
            return new JsonResult(new {message="你输入的有误！",state=400 });
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="validId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("register/{validId}/{code}")]
        public IActionResult Register([FromBody] User user,string validId,string code)
        {
            if (!ModelState.IsValid)
                return Ok(new { message = "字段错误", state = 400 });
            var CacheCode = _Cache.GetString($"EmailCode:{validId}");
            if (CacheCode != code)
                return BadRequest(new { message = "验证码有误", state = 400 });
            _Cache.Remove(validId);
            var state = _UserService.Register(user).Result;
            if (state)
                return Ok(new { message = "注册成功", state = 200 });
            return Ok(new {message="账号已经存在",state=400});
        }
    }
}
