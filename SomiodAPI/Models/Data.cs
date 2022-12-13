using SomiodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomiodAPI
{
    public class Data
    {

        public Data()
        {
            this.Res_type = this.Res_type.ToUpper() ?? "DATA";
        }

        public Data(Subscription_Data subscription_Data)
        {
            Id = subscription_Data.Id;
            Name = subscription_Data.Name;
            Creation_dt = subscription_Data.Creation_dt;
            Parent = subscription_Data.Parent;
            Res_type = subscription_Data.Res_type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
        public int Parent { get; set; }
        public string Res_type { get; set;}
    }
}