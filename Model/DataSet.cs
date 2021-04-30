using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public class DataSet
    {
        private string _name;
        private DataSource _dataSource;
        private string _commandText;
        private List<Parameter> _parameters;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public DataSource DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }
        public string CommandText
        {
            get { return _commandText; }
            set { _commandText = value; }
        }
        public List<Parameter> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

    }
}
