﻿using System.Xml;
using System.Xml.Serialization;

namespace ApplicationA.Models
{
    public class Data
    {
        public Data() { }

        public Data(Subscription_Data subscription_Data)
        {
            Id = subscription_Data.Id;
            if (subscription_Data.Content.FirstChild != null)
            {
                Content = subscription_Data.Content.FirstChild.InnerXml;
                Content = Content.Trim();
                Content = Content.Replace("\n", "");
                Content = Content.Replace(" ", "");
            }
            Creation_dt = subscription_Data.Creation_dt;
            Parent = subscription_Data.Parent;
            Res_type = subscription_Data.Res_type;
        }

        public int Id { get; set; }
        public string Content { get; set; } = "";

        [XmlAttribute("event")]
        public string Event { get; set; }

        public string Creation_dt { get; set; }
        public int Parent { get; set; }
        public string Res_type { get; set; } = "data";
    }
}