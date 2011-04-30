using System;
using System.Xml;
using NHibernate.Cfg;

namespace Standings.Web.Tests.Helpers
{
    public class NHibernateHelper
    {
        public static Configuration GenerateStubConfiguration(string testDirPath)
        {
            return new Configuration().Configure(GetNHibernateXmlConfig(testDirPath));
        }

        private static XmlTextReader GetNHibernateXmlConfig(string testDirPath)
        {
            var webConfigPath = String.Format(@"{0}\..\..\Standings.Web\web.config", testDirPath);
            var xdoc = new XmlDocument();
            xdoc.Load(webConfigPath);
            return new XmlTextReader(xdoc.DocumentElement.ChildNodes[1].OuterXml, XmlNodeType.Document, null);
        }
    }
}
