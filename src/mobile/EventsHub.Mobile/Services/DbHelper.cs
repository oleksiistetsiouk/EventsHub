using System;
using System.IO;

namespace EventsHub.Mobile.Services
{
    public class DbHelper
    {
        public DbHelper() { }
        public static string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            return path;
        }
    }
}
