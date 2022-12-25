using SomiodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomiodAPI
{
    public class Subscription
    {
        public Subscription() { }

        public Subscription(Subscription_Data subscription_Data)
        {
            Id = subscription_Data.Id;
            Name = subscription_Data.Name;
            Creation_dt = subscription_Data.Creation_dt;
            Parent = subscription_Data.Parent;
            Event = subscription_Data.Event;
            Endpoint = subscription_Data.Endpoint;
            Res_type = subscription_Data.Res_type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Creation_dt { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; } = "creation";
        public string Endpoint { get; set; }
        public string Res_type { get; set; } = "subscription";
    }
}