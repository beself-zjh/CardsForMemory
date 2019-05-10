using System.Collections.Generic;

namespace CardsForMemoryLibrary {
    public class Status {
        private Dictionary<string, object> m;
        private Status() {
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

        private static Status instance = null;
        private static readonly object instanceLock = new object();
        public static Status getInstance() {
            if (instance == null) {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new Status();
                    }
                }
            }
            return instance;
        }
    }
}
