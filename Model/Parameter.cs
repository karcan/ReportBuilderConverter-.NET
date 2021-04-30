using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public class Parameter
    {
        private string _name;
        private string _dataType;
        private string _prompt;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
        public string Prompt
        {
            get { return _prompt; }
            set { _prompt = value; }
        }



    }
}
