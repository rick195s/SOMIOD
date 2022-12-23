using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomiodAPI.Models
{
    public class Subscription_Data
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss"); 
        public int Parent { get; set; }
        public string Event { get; set; } = "CREATION";
        public string Endpoint { get; set; }
        public string Res_type { get; set; }
    }
}