using System.IO;
using Microsoft.Data.Sqlite;
using robotManager.Helpful;

namespace WoWBot.Client.Database
{
    public class MainDatabase
    {
        private static readonly string ConnectionString = "Data Source=database.db";
        private static readonly string CreateDbQuery = "CREATE DATABASE IF NOT EXISTS wowmasterdb";
        private static readonly string ItemsSqlFile = @"all-items-filtered.sql";

        private MainDatabase() {
            using (var db = new SqliteConnection($"Filename=database.db"))
            {
                db.Open();

                var createDb = new SqliteCommand(CreateDbQuery, db);

                createDb.ExecuteReader();

                var createTable = new SqliteCommand(File.ReadAllText(@"database.db"), db);

                createTable.ExecuteReader();
            }
        }
        public static void GetWeaponById(int id)
        {
            SqliteConnection connection = new SqliteConnection(ConnectionString);

            // open connection
            connection.Open();
            
            using (SqliteCommand command = new SqliteCommand(CreateDbQuery, connection))
            {
                command.CommandText = "SELECT * FROM items WHERE item_id = @itemId";

                command.Parameters.AddWithValue("@itemId", id);
                command.Prepare();

                using (SqliteDataReader rdr = command.ExecuteReader())
                {
                    int itemClass = rdr.GetInt32(2);
                    int itemSubClass = rdr.GetInt32(3);
                    int inventoryType = rdr.GetInt32(3);

                    Logging.Write("[Database] " + id + " " + inventoryType + " " + itemClass + " " + itemSubClass);
                }
            }
        }
    }
}
