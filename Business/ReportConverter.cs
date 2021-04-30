
using Microsoft.Reporting.WebForms;
using ReportBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ReportBuilder
{
    public class ReportConverter : IReportConverter
    {
        private LocalReport _reportViewer;
        private IReportReader _reportBuilderReader;

        public ReportConverter()
        {
            this._reportViewer = new LocalReport();
        }
        private byte[] _toByte(byte[] reportFile, ReportExtension fileExtension, IDictionary<string,string> paramValues = null)
        {
            this._reportBuilderReader = new ReportReader(reportFile);

            if (paramValues != null) {
                this._setReportParameters(paramValues);
            }

            this._setDataSets();

            return this._reportViewer.Render(fileExtension.Value);
        }
        private void _setReportPath(string reportFilePath)
        {
            this._reportViewer.ReportPath = @reportFilePath;
        }
        private void _setReportParameters(IDictionary<string, string> paramValues)
        {
            List<Parameter> parameters = _reportBuilderReader.Parameters();
            List<ReportParameter> reportParameters = new List<ReportParameter>();

            foreach (var parameter in parameters)
            {
                reportParameters.Add(new ReportParameter(parameter.Prompt, paramValues.FirstOrDefault(p=>p.Key == parameter.Prompt).Value));
            }

            this._reportViewer.SetParameters(reportParameters);

            this._reportViewer.Refresh();

        }
        private void _setDataSets() {
            this._reportViewer.DataSources.Clear();

            foreach (var item in _reportBuilderReader.DataSets())
            {
                SqlConnection connect = new SqlConnection(item.DataSource.ConnectionString);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(item.CommandText, connect);

                foreach (var parameter in item.Parameters)
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@" + parameter.Prompt, "1");
                }

                DataTable dataTable = new DataTable();
                dataTable.BeginLoadData();
                dataAdapter.Fill(dataTable);
                dataTable.EndLoadData();

                ReportDataSource reportDataSource = new ReportDataSource();

                reportDataSource.Name = item.Name;
                reportDataSource.Value = dataTable;
                this._reportViewer.DataSources.Add(reportDataSource);
            }

            this._reportViewer.Refresh();
        }

        public byte[] toByte(string reportFilePath, ReportExtension fileExtension)
        {
            this._setReportPath(reportFilePath);
            return _toByte(File.ReadAllBytes(reportFilePath), fileExtension);
        }
        public byte[] toByte(string reportFilePath, ReportExtension fileExtension, IDictionary<string, string> paramValues)
        {
            this._setReportPath(reportFilePath);
            return _toByte(File.ReadAllBytes(reportFilePath), fileExtension, paramValues);
        }
    }
}
