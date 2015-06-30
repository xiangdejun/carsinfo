using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using CarsInfo.DataEntitySqlite;

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

        public static DataTable GetTable(string tableName)
        {
            var dt = new DataTable();
            var command = new SQLiteCommand("Select * from " + tableName , connection);
            var dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dt);
            return dt;
        }

        public static void InsertParamGroup(List<string> list)
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in list)
            {
                strBuilder.Append("Insert into paramgroup values(null, '" + item + "');");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }

        public static void InsertParams(IEnumerable<param> paramNames )
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in paramNames)
            {
                strBuilder.Append("Insert into param values(null, '" + item.pname + "', "+ item.pgid +");");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }

        public static void ExcuteInsert(SQLiteCommand command)
        {
            connection.Open();
            var trans = connection.BeginTransaction();

            try
            {
                command.Connection = connection;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                connection.Close();
            }
        }

        public static void InsertBrand(List<string> list)
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in list)
            {
                strBuilder.Append("Insert into brand values(null, '" + item + "');");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }

        public static void InsertFactory(IEnumerable<factory> factories)
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in factories)
            {
                strBuilder.Append("Insert into factory values(null, '" + item.fname + "', " + item.bid + ");");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }

        public static void InsertSeries(IEnumerable<series> carseries)
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in carseries)
            {
                strBuilder.Append("Insert into series values(null, '" + item.sname + "', " + item.fid + " , " + item.classid + ", '" + item.price + "');");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }
        public static void InsertCar(IEnumerable<car> cars)
        {
            var command = new SQLiteCommand();
            var strBuilder = new StringBuilder();
            foreach (var item in cars)
            {
                strBuilder.Append("Insert into car values(null, '" + item.cname + "', " + item.sid + ");");
            }
            command.CommandText = strBuilder.ToString();

            ExcuteInsert(command);
        }
    }

    //public struct Param
    //{
    //    public int groupid { get; set; }
    //    public string paramname { get; set; }
    //}
}
