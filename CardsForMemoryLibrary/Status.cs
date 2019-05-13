using System.Collections.Generic;

namespace CardsForMemoryLibrary {
    public class Status {
        private static readonly object mLock = new object();
        private static Dictionary<string, object> m;

        public static Status s=new Status();
        public Status() {
            if (m == null) {
                lock (mLock) {
                    if (m == null) {
                        m = new Dictionary<string, object>();
                    }
                }
            }
        }

        public object this[string index] {
            get {
                if (m.ContainsKey(index))
                    return m[index];
                else
                    return null;
            }
            set {
                m[index] = value;
            }
        }
    }
}
