using CsvHelper;
using CsvHelper.Configuration;
using DBBrowser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Itecx.Util.DBBrowser
{
    class DBBrowserController
    {
        private SqlConnection conn;
        private string connStr;
        private Dictionary<string, Type> columnDataTypeMap;
        private Random random = new Random();
        private static DBBrowserController controller;
        public string ServerName { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Dictionary<string, List<DataChooser>> DataChooserMap { get; set; }

        private DBBrowserController()
        {
            DataChooserMap = new Dictionary<string, List<DataChooser>>();

            string dataChooser = DBBrowserConstant.LOCATION_RESOURCE + DBBrowserConstant.DATA_CHOOSER;
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataChooser);
            if(stream == null)
            {
                throw new Exception("Resource " + dataChooser + " not found");
            }
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    DataChooser data = csv.GetRecord<DataChooser>();
                    if (!DataChooserMap.ContainsKey(data.Table))
                    {
                        List<DataChooser> dataChoosers = new List<DataChooser>();
                        DataChooserMap.Add(data.Table, dataChoosers);
                    }
                    DataChooserMap[data.Table].Add(data);
                }
            }
        }
        public static DBBrowserController GetInstance()
        {
            if (controller == null)
            {
                controller = new DBBrowserController();
            }
            return controller;
        }

        public SqlConnection SetSqlConnection(string connStr, string user, string password)
        {
            if (string.IsNullOrWhiteSpace(this.connStr))
            {
                this.connStr = "";
            }
            if (string.IsNullOrWhiteSpace(connStr) || !(string.IsNullOrWhiteSpace(connStr) || this.connStr.Equals(connStr)))
            {
                if (conn != null)
                {
                    conn.Close();
                }
                //Data Source=Zoheb;Initial Catalog=orchestrauat;Integrated Security=SSPI;
                conn = new SqlConnection();
                string connectionString = "";
                if (!string.IsNullOrWhiteSpace(user))
                {
                    connectionString = connStr + "User id=" + user + ";" +
                                            "Password=" + password + ";";
                }
                else
                {
                    connectionString = connStr;
                }
                conn.ConnectionString = connectionString;
                this.connStr = connStr;
            }
            return conn;
        }


        public SqlConnection SetSqlConnection(string serverName, string database, string user, string password)
        {
            if (string.IsNullOrWhiteSpace(serverName))
            {
                throw new Exception("Server Name cannot be blank");
            }

            if (conn != null)
            {
                conn.Close();
            }
            //Data Source=Zoheb;Initial Catalog=orchestrauat;Integrated Security=SSPI;
            conn = new SqlConnection();
            string connectionString = "Data Source=" + serverName + ";";

            if (!string.IsNullOrWhiteSpace(database))
            {
                connectionString = connectionString + "Initial Catalog=" + database + ";";
            }
            if (!string.IsNullOrWhiteSpace(user))
            {
                connectionString = connectionString + "User id=" + user + ";";
            }
            if (!string.IsNullOrWhiteSpace(password))
            {
                connectionString = connectionString + "Password=" + password + ";";
            }
            if (string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(password))
            {
                connectionString = connectionString + "Integrated Security=SSPI;";
            }
            this.connStr = connectionString;
            conn.ConnectionString = connectionString;

            ServerName = serverName;
            Database = database;
            User = user;
            Password = password;

            return conn;
        }

        public List<string> GetDataBaseList()
        {
            List<string> dataBases = new List<string>();
            try
            {
                conn.Open();
                DataTable tblDatabases = conn.GetSchema("Databases");
                foreach (DataRow row in tblDatabases.Rows)
                {
                    dataBases.Add(row["database_name"].ToString());
                }
            }
            finally
            {
                conn.Close();
            }
            return dataBases;
        }

        public List<string> GetTableList()
        {
            List<string> tables = new List<string>();
            try
            {
                conn.Open();
                DataTable dt = conn.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    if (DataChooserMap.Count == 0 || DataChooserMap.ContainsKey(tablename))
                    {
                        tables.Add(tablename);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return tables;

            //List<string> tables = new List<string>();
            //List<List<object>> result = ExecuteSelectSQL(DBBrowserConstant.sqlTableList, false);
            //if (result != null)
            //{
            //    foreach (List<object> row in result)
            //    {
            //        tables.Add((string)row[0]);
            //    }
            //}
            //return tables;
        }

        public string[] ExecuteSQL(TableView view, string sqlText)
        {
            try
            {
                string[] sqlStrs = sqlText.Split(new[] { '\r', '\n' });
                conn.Open();
                foreach (string sql in sqlStrs)
                {
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        SqlCommand command = new SqlCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                }
                return sqlStrs;
            }
            finally
            {
                conn.Close();
            }
        }

        public TableView ExecuteSelectSQL(string tableName, string sql)
        {
            TableView result = new TableView();

            if (conn != null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        result.TableName = tableName;
                        result.PrimaryColumns = GetPrimaryKey(tableName);
                    }

                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        System.Data.DataSet ds = new System.Data.DataSet();
                        da.Fill(ds);
                        columnDataTypeMap = new Dictionary<string, Type>();

                        foreach (DataColumn column in ds.Tables[0].Columns)
                        {
                            result.Columns.Add(column.ColumnName, column.DataType);
                        }
                        result.Columns.Add(DBBrowserConstant.DBBROWSER_ACTION, null);
                        result.Data = new List<List<object>>();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                List<object> rowObj = new List<object>();
                                foreach (DataColumn column in ds.Tables[0].Columns)
                                {
                                    object obj = row[column];
                                    rowObj.Add(obj);
                                }
                                rowObj.Add(DBBrowserConstant.FROM_TABLE);
                                result.Data.Add(rowObj);
                            }
                        };
                    }
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                if (DataChooserMap.ContainsKey(tableName))
                {
                    result.DataChoosers = DataChooserMap[tableName];
                }
            }
            else
            {
                throw new Exception("Please connect to DB");
            }
            return result;
        }

        public List<List<object>> ExecuteSelectSQL(string sql, bool addColumn)
        {

            List<List<object>> result = new List<List<object>>();
            List<object> columns = new List<object>();
            if (conn != null)
            {
                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        System.Data.DataSet ds = new System.Data.DataSet();
                        da.Fill(ds);
                        columnDataTypeMap = new Dictionary<string, Type>();

                        if (addColumn)
                        {
                            foreach (DataColumn column in ds.Tables[0].Columns)
                            {
                                columns.Add(column.ColumnName);
                            }
                            result.Add(columns);
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                List<object> rowObj = new List<object>();
                                foreach (DataColumn column in ds.Tables[0].Columns)
                                {
                                    object obj = row[column];
                                    rowObj.Add(obj);
                                }
                                result.Add(rowObj);
                            }
                        };
                    }
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                throw new Exception("Please connect to DB");
            }
            return result;
        }

        public List<string> GetPrimaryKey(string table)
        {
            List<string> primaryCols = new List<string>();
            string sql = "select col.[name] " +
                        "from sys.tables tab, sys.indexes pk, sys.index_columns ic, sys.columns col " +
                        "where " +
                        "tab.object_id = pk.object_id " +
                        "and ic.index_id = pk.index_id " +
                        "and ic.object_id = tab.object_id " +
                        "and ic.object_id = col.object_id " +
                        "and ic.column_id = col.column_id " +
                        "and pk.is_primary_key = 1 " +
                        "and tab.name = '" + table + "'";
            List<List<object>> result = ExecuteSelectSQL(sql, false);
            foreach (List<object> row in result)
            {
                foreach (object obj in row)
                {
                    if (obj != null)
                    {
                        primaryCols.Add(obj.ToString());
                    }
                }
            }

            return primaryCols;
        }

        internal string UpdateDeleteSql(Dictionary<string, object> colObjectDict, TableView view)
        {
            string sql = "";
            foreach (string col in colObjectDict.Keys)
            {
                if ((view.PrimaryColumns == null || view.PrimaryColumns.Count <= 0) || view.PrimaryColumns.Contains(col))
                {
                    object obj = colObjectDict[col];
                    if (string.IsNullOrWhiteSpace(sql))
                    {
                        sql = " " + col + " = '" + obj + "'";
                    }
                    else
                    {
                        sql = sql + " AND " + col + " = '" + obj + "'";
                    }
                }
            }
            sql = "DELETE FROM " + view.TableName + " WHERE " + sql;
            string deleteKey = DBBrowserConstant.DELETE + random.Next();
            updateSQL(view, deleteKey, sql);
            return deleteKey;
        }

        internal string UpdateInsertSql(Dictionary<string, object> colObjectDict, TableView view)
        {
            string sql = "INSERT INTO " + view.TableName;
            string colNames = "";
            string values = "";
            string dbAction = "";
            foreach (string col in colObjectDict.Keys)
            {
                if (DBBrowserConstant.DBBROWSER_ACTION.Equals(col))
                {
                    if (colObjectDict[col] != null)
                    {
                        dbAction = colObjectDict[col].ToString();
                    }
                    continue;
                }
                if (string.IsNullOrWhiteSpace(colNames))
                {
                    colNames = col;
                    values = "'" + colObjectDict[col] + "'";
                }
                else
                {
                    colNames = colNames + "," + col;
                    values = values + ",'" + colObjectDict[col] + "'";
                }
            }
            sql = sql + "(" + colNames + ")VALUES (" + values + ")";
            if (string.IsNullOrWhiteSpace(dbAction))
            {
                dbAction = DBBrowserConstant.INSERT + random.Next();
            }
            updateSQL(view, dbAction, sql);

            return dbAction;
        }

        internal string UpdateUpdateSql(Dictionary<string, object> colObjectDict, Dictionary<string, bool> colChangeDict, TableView view)
        {
            string sql = "UPDATE " + view.TableName;
            string set = "";
            string where = "";
            string dbAction = "";
            foreach (string col in colObjectDict.Keys)
            {
                if (DBBrowserConstant.DBBROWSER_ACTION.Equals(col))
                {
                    if (colObjectDict[col] != null)
                    {
                        dbAction = colObjectDict[col].ToString();
                    }
                    continue;
                }
                if (view.PrimaryColumns.Contains(col))
                {
                    if (string.IsNullOrWhiteSpace(where))
                    {
                        where = col + "='" + colObjectDict[col] + "' ";
                    }
                    else
                    {
                        where = where + "AND " + col + "='" + colObjectDict[col] + "' ";
                    }
                }
                else
                {
                    if (colChangeDict[col])
                    {
                        if (string.IsNullOrWhiteSpace(set))
                        {
                            set = col + "='" + colObjectDict[col] + "' ";
                        }
                        else
                        {
                            set = set + ", " + col + "='" + colObjectDict[col] + "' ";
                        }
                    }
                }
            }
            sql = sql + " SET " + set + " WHERE " + where;
            string updatetKey = dbAction;
            if (DBBrowserConstant.FROM_TABLE.Equals(dbAction))
            {
                updatetKey = DBBrowserConstant.UPDATE + random.Next();
            }

            updateSQL(view, updatetKey, sql);
            return updatetKey;
        }

        private void updateSQL(TableView view, string key, string value)
        {
            if (view.ActionPerformed.ContainsKey(key))
            {
                view.ActionPerformed.Remove(key);
            }
            view.ActionPerformed.Add(key, value);
        }
        public List<string> FilterBasedOnDataChooser(List<string> tableList)
        {
            if(DataChooserMap == null)
            {
                return null;
            }
            List<string> output = new List<string>();
            foreach(string table in tableList)
            {
                if (DataChooserMap.ContainsKey(table))
                {
                    output.Add(table);
                }
            }
            return output;
        }
    }
}
