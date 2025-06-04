using Microsoft.AspNetCore.Mvc;
using RestServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RestServis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private string nameSpace = "http://schemas.datacontract.org/2004/07/RestService.Models";
        private string path = "\\Users\\paige\\cooper\\Skyscanner-app-RestService\\RestService\\Air_carriers_XSD.xsd";
        private Carriers carriers;

        public CarrierController(Carriers carriers)
        {
            this.carriers = carriers;
        }

       // public Carriers Get() => carriers;

        // XSD Validation
        [HttpPost("XSD")]
        public IActionResult Post(string xaml)
        {
            try
            {
                
                XmlTextReader reader = new XmlTextReader(xaml);
                XmlDocument doc = new XmlDocument();
                XmlNode node = doc.ReadNode(reader);
                
                var attribute = node.Attributes["Name"];
                if (attribute != null){
                    string employeeName = attribute.Value;
                    // Process the value here
                }
                

                if (IsValid)
                {
                    //SaveXmlAndAddCarrier(xmlCarrier);
                    return Accepted();
                }
                else
                {
                    return StatusCode(206); // PartialContent
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // RNG Validation
        [HttpPost("RNG")]
        public IActionResult XmlAgainstRNG(XmlElement xmlCarrier)
        {
            XmlDocument document = xmlCarrier.OwnerDocument;
            document.AppendChild(xmlCarrier);
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);

            memoryStream.Flush();
            memoryStream.Position = 0;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(nameSpace, path);
            settings.ValidationEventHandler += XmlValid;

            using (XmlReader reader = XmlReader.Create(memoryStream, settings))
            {
                try
                {
                    while (reader.Read()) { }

                    SaveXmlAndAddCarrier(xmlCarrier);
                    return Accepted();
                }
                catch (Exception)
                {
                    return StatusCode(206); // PartialContent
                }
            }
        }

        // Save xml file and add new Carrier after validating XSD or RNG
        private void SaveXmlAndAddCarrier(XmlElement xmlCarrier)
        {
            string ns = xmlCarrier.OuterXml;
            string strXMLPattern = @"xmlns(:\w+)?=""([^""]+)""|xsi(:\w+)?=""([^""]+)""";
            ns = Regex.Replace(ns, strXMLPattern, "");
            using (StreamWriter writer = new StreamWriter("Air_carriers.xml"))
            {
                writer.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                writer.Write(ns);
            }

            // Add new Carrier to list
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Carrier));
            using (StringReader stringReader = new StringReader(xmlCarrier.OuterXml))
            {
                Carrier addedCarrier = (Carrier)xmlSerializer.Deserialize(stringReader);
                //carriers.AllCarriers.Add(addedCarrier);
            }
        }

        private void XmlValid(object sender, ValidationEventArgs e)
        {
            IsValid = false;
        }

        private bool IsValid { get; set; } = true;
    }
}