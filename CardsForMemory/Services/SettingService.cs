using CardsForMemoryLibrary.IServices;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace CardsForMemory.Services {
    public class SettingService : ISettingService {
        private IPropertySet ls;
        public SettingService() => ls = ApplicationData.Current.LocalSettings.Values;

        public object this[string key] {
            get => ls.ContainsKey(key) ? ls[key] : null;
            set => ls[key] = value;
        }

        public void clearAll() {
            ls.Clear();
        }

        public void delete(string key) {
            if (ls.ContainsKey(key)) {
                ls.Remove(key);
            }
        }
    }
}
