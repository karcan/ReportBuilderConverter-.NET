using ReportBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReportBuilder
{
    public class ReportReader : IReportReader
    {
        private XmlNodeList _childNodes;
        public ReportReader(byte[] reportFile) 
        {
            XmlDocument doc = new XmlDocument();
            MemoryStream ms = new MemoryStream(reportFile);
            doc.Load(ms);
            XmlNode root = doc.DocumentElement;
            _childNodes = root.ChildNodes;
        }
        public List<DataSet> DataSets()
        {
            List<DataSet> dataSets = new List<DataSet>();

            foreach (XmlNode Elements in _childNodes)
            {
                if (Elements.Name == "DataSets")
                {
                    foreach (XmlNode DataSet in Elements.ChildNodes)
                    {
                        if (DataSet.Name == "DataSet")
                        {
                            if (DataSet.HasChildNodes)
                            {
                                string Name = DataSet.Attributes.GetNamedItem("Name").Value;
                                XmlNode d = DataSet.ChildNodes.Item(0);
                                string DS = d.ChildNodes.Item(0).InnerText;
                                DataSource DataSource = DataSources().FirstOrDefault(i => i.Name == DS);
                                string CommandText = d.ChildNodes.Item(2).InnerText;
                                List<Parameter> temp = new List<Parameter>();

                                foreach (XmlNode param in d.ChildNodes.Item(1))
                                {
                                    string p = param.Attributes.GetNamedItem("Name").Value.Replace("@", string.Empty);
                                    Parameter parameter = Parameters().FirstOrDefault(i => i.Prompt == p);
                                    temp.Add(parameter);

                                }

                                dataSets.Add(new DataSet
                                {
                                    Name = Name,
                                    DataSource = DataSource,
                                    CommandText = CommandText,
                                    Parameters = temp
                                });
                            }

                        }
                    }
                }
            }

            return dataSets.ToList();
        }

        public List<DataSource> DataSources()
        {
            List<DataSource> dataSources = new List<DataSource>();

            foreach (XmlNode Elements in _childNodes)
            {
                if (Elements.Name == "DataSources")
                {
                    foreach (XmlNode DataSource in Elements.ChildNodes)
                    {
                        if (DataSource.Name == "DataSource")
                        {
                            if (DataSource.HasChildNodes)
                            {
                                string Name = DataSource.Attributes.GetNamedItem("Name").Value;
                                XmlNode d = DataSource.ChildNodes.Item(0);
                                string DataProvider = d.ChildNodes.Item(0).InnerText;
                                string ConnectionString = d.ChildNodes.Item(1).InnerText;

                                dataSources.Add(new DataSource
                                {
                                    Name = Name,
                                    DataProvider = DataProvider,
                                    ConnectionString = ConnectionString
                                });
                            }

                        }
                    }
                }
            }

            return dataSources.ToList();
        }

        public List<Parameter> Parameters()
        {
            List<Parameter> parameters = new List<Parameter>();

            foreach (XmlNode Elements in _childNodes)
            {
                if (Elements.Name == "ReportParameters")
                {
                    foreach (XmlNode Parameter in Elements.ChildNodes)
                    {
                        if (Parameter.Name == "ReportParameter")
                        {
                            if (Parameter.HasChildNodes)
                            {
                                string Name = Parameter.Attributes.GetNamedItem("Name").Value;
                                XmlNodeList d = Parameter.ChildNodes;
                                string DataType = d.Item(0).InnerText;
                                string Prompt = d.Item(01).InnerText;

                                parameters.Add(new Parameter
                                {
                                    Name = Name,
                                    DataType = DataType,
                                    Prompt = Prompt
                                });
                            }

                        }

                    }
                }
            }

            return parameters.ToList();
        }
    }
}
