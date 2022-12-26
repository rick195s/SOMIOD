﻿using Newtonsoft.Json;
using System;
using System.Web.Http.ModelBinding;
using System.Xml;
using System.Xml.Serialization;

namespace SomiodAPI.Models
{
    public class Subscription_Data 
    {

        public int Id { get; set; }
        public string Name { get; set; }
        [XmlAnyElement]// possivel enviar xml invalido 
        public XmlDocument Content { get; set; } = new XmlDocument();
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss"); 
        public int Parent { get; set; }
        public string Event { get; set; } = "creation";
        public string Endpoint { get; set; }
        public string Res_type { get; set; }
    }
}