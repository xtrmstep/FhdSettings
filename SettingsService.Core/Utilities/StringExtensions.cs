using System.Text.RegularExpressions;

namespace SettingsService.Core.Utilities
{
    public static class StringExtensions
    {
        public static string GetHost(this string url)
        {
            const string urlPattern = @"^(http://|https://)?(www.)?((?<domain>[a-zA-Z0-9.\-_]+)\/)";
            var matchedGroups = Regex.Match(url, urlPattern).Groups;
            if (matchedGroups.Count > 0)
            {
                var domainGroup = matchedGroups["domain"];
                if (domainGroup != null)
                    return domainGroup.Value;
            }
            return string.Empty;
        }
    }
}