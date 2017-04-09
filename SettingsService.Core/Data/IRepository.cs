using System;
using System.Collections.Generic;

namespace SettingsService.Core.Data
{
    public interface IRepository<T>
    {
        IList<T> Get();
        T Get(Guid id);
        Guid Add(T item);
        void Update(T item);
        void Remove(Guid id);
    }
}