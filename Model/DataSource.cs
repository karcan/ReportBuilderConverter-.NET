using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public class DataSource
    {
        private string _name;
        private string _dataProivder;
        private string _connnectionString;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string DataProvider
        {
            get { return _dataProivder; }
            set { _dataProivder = value; }
        }
        public string ConnectionString
        {
            get { return _connnectionString; }
            set { _connnectionString = value; }
        }



    }
}
