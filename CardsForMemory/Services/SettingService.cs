using CardsForMemory.IServices;
using Windows.Storage;

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
            //throw new NotImplementedException();
            string first = "yes";
            if (_localSetting.Values.ContainsKey(first)) {

            } else
                RestoreSettings();
        }

        /// <inheritdoc />
        public void Save(string key, string value) {
            // throw new NotImplementedException();
            _localSetting.Values[key] = value;
        }

        /// <inheritdoc />
        public void Save(string key, ApplicationDataCompositeValue compositeValue) {
            //throw new NotImplementedException();
            _localSetting.Values[key] = compositeValue;
        }

        /// <inheritdoc />
        public string Value(string key) {
            //throw new NotImplementedException();
            return _localSetting.Values[key].ToString();
        }

        /// <inheritdoc />
        public ApplicationDataCompositeValue CompositeValue(string key) {
            return (ApplicationDataCompositeValue)_localSetting.Values[key];
        }

        /// <inheritdoc />
        public void RestoreSettings() {
            //throw new NotImplementedException();

        }
    }
}
