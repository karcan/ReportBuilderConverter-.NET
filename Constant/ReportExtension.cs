using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBuilder
{
    public class ReportExtension
    {
        private ReportExtension(string value) { Value = value; }

        public string Value { get; set; }

        public static ReportExtension Excel { get { return new ReportExtension("Excel"); } }
        public static ReportExtension PDF { get { return new ReportExtension("PDF"); } }
        public static ReportExtension Word { get { return new ReportExtension("Word"); } }
        public static ReportExtension Image { get { return new ReportExtension("Image"); } }

    }
}
