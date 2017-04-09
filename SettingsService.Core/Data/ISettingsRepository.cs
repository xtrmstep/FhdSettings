using System;
using System.Collections.Generic;
using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface ISettingsRepository : IRepository<Setting>
    {
    }
}