using ReportBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public interface IReportReader
    {
        List<Parameter> Parameters();
        List<DataSource> DataSources();
        List<DataSet> DataSets();
    }
}
