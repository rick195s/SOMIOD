using System;
using System.Xml;
using System.Xml.Serialization;

namespace SomiodAPI.Models
{
    public class Subscription_Data 
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public XmlDocument Content { get; set; }
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss"); 
        public int Parent { get; set; }
        public string Event { get; set; } = "CREATION";
        public string Endpoint { get; set; }
        public string Res_type { get; set; }
    }
}