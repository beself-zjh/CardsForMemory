using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public interface iSqliteConnectionService {
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
