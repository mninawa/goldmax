using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace RestServis.Models
{
    [DataContract]
    [XmlRoot("carriers")]
    public class Carriers
    {
        [DataMember(Name = "status")]
        [XmlElement("status")]
        public string Status { get; set; }

        [DataMember(Name = "carriers")]
        [XmlElement("carrier")]
        public Dictionary<string, Carrier> CarrierDictionary { get; set; }
        public object AllCarriers { get; internal set; }
    }
    [DataContract]
    public class Carrier
    {
        [DataMember(Name = "name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [DataMember(Name = "iata")]
        [XmlElement("iata")]
        public string IATA { get; set; }
    }

}