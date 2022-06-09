using PNB.Domain;
using PNB.Domain.Configuration;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PNB.Service.sSetting
{
   public partial class SettingService : ISettingService
    {
        private readonly IRepository<Settings> _repositorySetting;
        public SettingService(IRepository<Settings> repositorySetting)
        {
            _repositorySetting = repositorySetting;
        }
        #region Utilities 
        protected virtual IDictionary<string, IList<Settings>> GetAllSettings()
        {
                var query = from s in _repositorySetting.Table
                            orderby s.Key
                            select s;
                var settings = query.ToList();
                var dictionary = new Dictionary<string, IList<Settings>>();
                foreach (var s in settings)
                {
                    var resourceName = s.Key.ToLowerInvariant();
                    var settingForCaching = new Settings
                    {
                        Key = s.Key,
                        Value = s.Value,
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, new List<Settings>
                        {
                            settingForCaching
                     });
                    }
                    else
                    {
                        //already added
                        //most probably it's the setting with the same name but for some certain store (storeId > 0)
                        dictionary[resourceName].Add(settingForCaching);
                    }
                }
                return dictionary;
        }
        protected virtual void SetSetting(Type type, string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var allSettings = GetAllSettings();
            var settings = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settings != null)
            {
                //update
                var setting = GetSettingByKey(settings.Key);
                setting.Value = valueStr;
                UpdateSetting(setting);
            }
            else
            {
                //insert
                var setting = new Settings
                {
                    Key = key,
                    Value = valueStr
                };
                InsertSetting(setting);
            }
        }

        #endregion
        public IPagedList<Settings> GetAll(string key, int start = 0, int take = 15)
        {
            var query = from h in _repositorySetting.Table select h;
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(s => s.Key.Contains(key));
            }
            return new PagedList<Settings>(query, start, take);
        }
        public virtual Settings GetSettingByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            return _repositorySetting.Table.FirstOrDefault(x=>x.Key== key);
        }

        public virtual void InsertSetting(Settings setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _repositorySetting.Insert(setting);
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void UpdateSetting(Settings setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _repositorySetting.Update(setting);

         
        }

        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        public virtual void DeleteSetting(Settings setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _repositorySetting.Delete(setting);

        }

        /// <summary>
        /// Deletes settings
        /// </summary>
        /// <param name="settings">Settings</param>
        public virtual void DeleteSettings(IList<Settings> settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _repositorySetting.Delete(settings);

        }
        public T GetSettingByKey<T>(string key, T defaultValue = default)
        {
            var query = from s in _repositorySetting.Table where s.Key == key select s;
            var setting = query.FirstOrDefault();
            return setting.Value!=null?  CommonHelper.To<T>(setting.Value):defaultValue;
        }
      
        public virtual T LoadSetting<T>() where T : ISettings, new()
        {
            return (T)LoadSetting(typeof(T));
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="storeId">Store identifier for which settings should be loaded</param>
        public virtual ISettings LoadSetting(Type type)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }
        public virtual void SetSetting<T>(string key, T value)
        {
            SetSetting(typeof(T), key, value);
        }
        public virtual void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(prop.PropertyType, key, value);
                else
                    SetSetting(key, string.Empty);
            }

        }

    
        public virtual void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            var key = GetSettingKey(settings, keySelector);
            var value = (TPropType)propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value);
            else
                SetSetting(key, string.Empty);
        }
        public virtual string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

            return key;
        }
    }
}
