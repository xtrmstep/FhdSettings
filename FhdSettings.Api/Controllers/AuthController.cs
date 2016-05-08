using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FhdSettings.Api.Models.Auth;
using FhdSettings.Api.Types;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Controllers
{
    public class AuthController : ApiController
    {
        IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public AuthTokenViewModel Register(string serviceCode, string password)
        {
            var authToken = authRepository.GetToken(serviceCode, password);
            var model = ViewModelMapper.Map<AuthToken, AuthTokenViewModel>(authToken);
            return model;
        }
    }
}
