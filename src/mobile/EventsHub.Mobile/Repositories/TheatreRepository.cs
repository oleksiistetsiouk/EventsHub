using EventsHub.Mobile.Models;
using SQLite;
using System.Collections.Generic;

namespace EventsHub.Mobile.Repositories
{
    public class TheatreRepository
    {
        SQLiteConnection database;
        public TheatreRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<TheatrePlay>();
        }

        public IEnumerable<TheatrePlay> GetItems()
        {
            return database.Table<TheatrePlay>().ToList();
        }

        public TheatrePlay GetItem(int id)
        {
            return database.Get<TheatrePlay>(id);
        }
    }
}
