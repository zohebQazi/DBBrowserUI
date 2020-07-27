using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itecx.Util.DBBrowser
{
    class DBBrowserConstant
    {
        public static string DBBROWSER_ACTION = "DBBROWSER_ACTION";
        public static string FROM_TABLE = "FromTable";
        public static string sqlTableList = "SELECT TABLE_NAME FROM information_schema.tables";
        public static string INSERT = "INSERT";
        public static string UPDATE = "UPDATE";
        public static string DELETE = "DELETE";
        public static string COL_TABLE = "TABLE";
        public static string COL_COLUMN = "COLUMN";
        public static string COL_SOURCE = "SOURCE";
        public static string COL_SOURCE_TABLE = "SOURCE TABLE";
        public static string COL_SOURCE_COLUMN = "SOURCE COLUMN";
        public static string COL_SQL = "SQL";
        public static string COL_CHOOSER = "CHOOSER";
        public static string LOCATION_RESOURCE = "DBBrowser.Resource.";
        public static string DATA_CHOOSER = "DataChooser.csv";
        public static string DATA_CHOOSER_TYPE_SQL = "SQL";
        public static string DATA_CHOOSER_TYPE_CHOOSER = "CHOOSER";
        public static string DATA_CHOOSER_TYPE_TABLE = "TABLE";
        public static string DATA_CHOOSER_PACKAGE = "Itecx.Util.Chooser";
    }
}
