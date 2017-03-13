using System;

namespace SettingsService.Api.Models
{
    public class HostSettingUpdateModel : HostSettingCreateModel
    {
        public Guid Id { get; set; }
    }
}