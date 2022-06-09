using PNB.Domain.Configuration;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Linq.Expressions;
using System.Text;

namespace PNB.Service.sSetting
{
    public interface ISettingService
    {
        IPagedList<Settings> GetAll(string key,int start = 0, int take = 15);
        void InsertSetting(Settings setting);
        void UpdateSetting(Settings setting);
        void DeleteSetting(Settings setting);
        Settings GetSettingByKey(string key);
        T GetSettingByKey<T>(string key, T defaultValue = default);

        T LoadSetting<T>() where T : ISettings, new();
        ISettings LoadSetting(Type type);
        void SetSetting<T>(string key, T value);
        void SaveSetting<T>(T settings) where T : ISettings, new();

        void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();
    }
}
