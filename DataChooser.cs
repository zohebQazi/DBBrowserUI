using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itecx.Util.DBBrowser
{
    class DataChooser
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string ChooserType { get; set; }
        public string SourceTable { get; set; }
        public string SourceColumn { get; set; }
        public string Sql { get; set; }
        public string Chooser { get; set; }
        public bool IsSQL()
        {
            if (DBBrowserConstant.DATA_CHOOSER_TYPE_SQL.Equals(ChooserType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsChooser()
        {
            if (DBBrowserConstant.DATA_CHOOSER_TYPE_CHOOSER.Equals(ChooserType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsTable()
        {
            if (DBBrowserConstant.DATA_CHOOSER_TYPE_TABLE.Equals(ChooserType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
