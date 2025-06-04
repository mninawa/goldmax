using System.Xml.Linq;

namespace RestService.Util
{
    public class XmlHandler
    {
        //dohvaćanje sadržaja XML datoteke => String
        public string getCarrierXML()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Air_carriers.xml");
            return XDocument.Load(path).ToString();
        }
    }
}
