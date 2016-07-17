using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface IAuthRepository
    {
        AuthToken GetToken(string serviceCode, string password);
    }
}