using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogCoreDomain;
using BlogCoreDomain.Model;

namespace BlogCoreInterface.Services
{
    // 用户操作的服务接口
    public interface IUserService
    {
        Task<bool> Login(User user);     //登录
        Task<bool> Register(User user);     // 注册
    }
}
