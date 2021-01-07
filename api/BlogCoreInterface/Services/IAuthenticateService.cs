using System;
using System.Collections.Generic;
using System.Text;
using BlogCoreDomain.Model;

namespace BlogCoreInterface.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(User user,out string token);
    }
}
