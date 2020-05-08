using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;


namespace Twins.Persistence
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        private static readonly Lazy<Database> instance = new Lazy<Database>(() => new Database(Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "database.db3"))); 

        private Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            //_database.CreateTableAsync<T>().Wait();
        }

        public static Database Instance
        {
            get 
            {
                return instance.Value;
            }
        }
    }
}