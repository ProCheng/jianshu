using System;
using System.Collections.Generic;
using System.Text;
using BlogCoreDomain.Model;
using BlogCoreInterface.Services;
using BlogCoreInterface.Repository;
using System.Threading.Tasks;

namespace BlogCoreImplement.Services
{
    #region 用户操作的实现类

    /// <summary>
    /// 用户操作的实现类
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _Repository;
        public UserService(IRepository<User> repository)
        {
            _Repository = repository;
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<bool> Login(User user)
        {
            var result = await _Repository.GetEntityList(x=>x.Account==user.Account&&x.Pwd==user.Pwd);
            return result.Count != 0;
        }
        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="user"></param>
        public async Task<bool> Register(User user)
        {
            var result = false;
            try
            {
                var list = await _Repository.GetEntityList(x=>x.Account==user.Account);
                if(list.Count==0)
                result = await _Repository.InsertAsync(user).Result.Save()!= 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
    #endregion
}
