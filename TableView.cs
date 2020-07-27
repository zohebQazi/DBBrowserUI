using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itecx.Util.DBBrowser
{
    class TableView
    {
        public TableView()
        {
            Columns = new Dictionary<string, Type>();
            PrimaryColumns = new List<string>();
            Data = new List<List<object>>();
            ActionPerformed = new Dictionary<string, string>();
            DataChoosers = new List<DataChooser>();
        }
        public string TableName { get; set; }
        public Dictionary<string, Type> Columns { get; set; }
        public List<string> PrimaryColumns { get; set; }
        public List<List<object>> Data { get; set; }
        public Dictionary<string, string> ActionPerformed { get; set; }
        public List<DataChooser> DataChoosers { get; set; }
    }
}
