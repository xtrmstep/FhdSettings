using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FhdSettings.Data.Models;

namespace FhdSettings.Data
{
    public interface IAuthRepository
    {
        AuthToken GetToken(string serviceCode, string password);
    }
}
