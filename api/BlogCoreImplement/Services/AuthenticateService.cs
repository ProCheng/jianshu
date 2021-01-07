using System;
using System.Collections.Generic;
using System.Text;
using BlogCoreInterface.Services;
using Microsoft.Extensions.Options;
using BlogCoreDomain.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogCoreImplement.Services
{
    /// <summary>
    /// 验证服务
    /// </summary>
    public class AuthenticateService : IAuthenticateService
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService _UserService;
        /// <summary>
        /// token模型
        /// </summary>
        private readonly TokenManagement _tokenManagement;
        public AuthenticateService(IUserService Userservice,IOptions<TokenManagement> tokenmanagement)
        {
            _UserService = Userservice;
            _tokenManagement = tokenmanagement.Value;
        }
        /// <summary>
        /// 颁发token
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="token">用来接收token</param>
        /// <returns></returns>
        public bool IsAuthenticated(User user, out string token)
        {
            token = string.Empty;
            if (!_UserService.Login(user).Result)
            {
                return false;
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer:  _tokenManagement.Issuer,
                audience: _tokenManagement.Audience,
                claims: new[] {new Claim(ClaimTypes.Name,user.Account.ToString()) },
                notBefore: DateTime.Now.AddHours(_tokenManagement.AccessExpiration),
                expires: DateTime.Now.AddHours(_tokenManagement.RefreshExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
