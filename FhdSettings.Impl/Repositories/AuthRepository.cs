using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    class AuthRepository : IAuthRepository
    {
        public AuthToken GetToken(string serviceCode, string password)
        {
            throw new NotImplementedException();
        }
    }
}
