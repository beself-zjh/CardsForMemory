using CardsForMemoryLibrary.IServices;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.Services {
    class GlobalVariableService : IGlobalVariableService {
        private Dictionary<string, object> m;
        private GlobalVariableService() {
            m = new Dictionary<string, object>();
        }
        public object this[string index] {
            get {
                return m[index];
            }
            set {
                m[index] = value;
            }
        }

        private static GlobalVariableService instance = null;
        private static readonly object instanceLock = new object();
        public static GlobalVariableService getInstance() {
            if (instance == null) {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new GlobalVariableService();
                    }
                }
            }
            return instance;
        }
    }
}
