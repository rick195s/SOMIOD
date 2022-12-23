using SomiodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomiodAPI
{
    public class Subscription
    {
        public Subscription()
        {
            //TODO - penso que isto tenha de ir par ao construtor quando recebe o res_type do Subscription_Data
            this.Res_type = this.Res_type.ToUpper() ?? "SUBSCRIPTION";
        }

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
        public string Event { get; set; } = "CREAION";
        public string Endpoint { get; set; }
        public string Res_type { get; set; } = "SUBSCRIPTION";
    }
}