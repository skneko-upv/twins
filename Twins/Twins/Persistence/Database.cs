using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Twins.Models;
using Twins.Components;

namespace Twins.Persistence
{
    public class Database
    {
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;
        private readonly SQLiteAsyncConnection _database;
        private static readonly Lazy<Database> instance = new Lazy<Database>(() => new Database(Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "database.db3"))); 

        private Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath, Flags);
            _database.CreateTableAsync<PlayerInfo>().Wait();
            //_database.CreateTableAsync<DefaultConfiguration>().Wait();
            this.InitializeDB();
        }

        public static Database Instance
        {
            get 
            {
                return instance.Value;
            }
        }

        public async void InitializeDB() 
        {
            int numberOfPlayers = await _database.Table<PlayerInfo>().CountAsync();
            if(numberOfPlayers == 0)
                _database.InsertOrReplaceAsync(new PlayerInfo()).Wait();

        }

        public async Task<PlayerInfo> GetPlayerInfo()
        {
            return await _database.GetAsync<PlayerInfo>(1); ;
        }
        public async Task<int> SavePlayerInfo(PlayerInfo playerInfo) 
        {
            //return await _database.InsertOrReplaceAsync(playerInfo);
            await _database.DeleteAsync<PlayerInfo>(1);
            return await _database.InsertAsync(playerInfo);
        }
    }
}