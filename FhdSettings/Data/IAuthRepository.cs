using FhdSettings.Data.Models;

namespace FhdSettings.Data
{
    public interface IAuthRepository
    {
        AuthToken GetToken(string serviceCode, string password);
    }
}