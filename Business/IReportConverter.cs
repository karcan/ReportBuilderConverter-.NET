using ReportBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public interface IReportConverter
    {
        byte[] toByte(string reportFilePath, ReportExtension fileExtension);
        byte[] toByte(string reportFilePath, ReportExtension fileExtension, IDictionary<string, string> paramValues); 
    }
}
