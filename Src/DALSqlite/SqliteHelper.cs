using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace CarsInfo.DALSqlite_ns
{
    public static class SqliteHelper
    {
        private static string ConnString = "Data Source = ..\\..\\..\\..\\Database\\db.sqlite;";

        private static SQLiteConnection connection;

        static SqliteHelper()
        {
            connection = new SQLiteConnection(ConnString);
        }

        public static DataTable GetDataTable(string selectFromBrand)
        {
            var dt = new DataTable();
            var command = new SQLiteCommand("Select * from brand", connection);
            var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dt);
            return dt;
        }
    }
}
