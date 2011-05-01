using System.IO;
using System.Reflection;
using System.Xml;
using NHibernate.Cfg;

namespace Standings.Web.Tests.Helpers
{
    public class NHibernateHelper
    {
        public static Configuration GenerateStubConfiguration()
        {
            var manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Standings.Web.Tests.Helpers.StubNHibernateApp.config");
            return new Configuration().Configure(new XmlTextReader(new StreamReader(manifestResourceStream)));
        }

    }
}
