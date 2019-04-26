using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using CardsForMemory.IServices;

namespace CardsForMemory.Services {
    public class SettingService : ISettingService {
        /// <summary>
        ///     配置服务单件
        /// </summary>
        public static readonly SettingService Instance = new SettingService();

        /// <summary>
        ///     本地配置
        /// </summary>
        private ApplicationDataContainer _localSetting;

        /// <summary>
        ///     私有构造函数
        /// </summary>
        private SettingService() {
            _localSetting = ApplicationData.Current.LocalSettings;
        }

        /// <inheritdoc />
        public void Init() {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(string key, string value) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(string key, ApplicationDataCompositeValue compositeValue) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string Value(string key) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ApplicationDataCompositeValue CompositeValue(string key) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RestoreSettings() {
            throw new NotImplementedException();
        }
    }
}
