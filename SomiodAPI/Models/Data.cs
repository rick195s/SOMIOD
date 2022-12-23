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
            //TODO - penso que isto tenha de ir par ao construtor quando recebe o res_type do Subscription_Data
            this.Res_type = this.Res_type.ToUpper() ?? "DATA";
        }

        public Data(Subscription_Data subscription_Data)
        {
            Id = subscription_Data.Id;
            Content = subscription_Data.Content;
            Creation_dt = subscription_Data.Creation_dt;
            Parent = subscription_Data.Parent;
            Res_type = subscription_Data.Res_type;
        }

        public int Id { get; set; }
        public string Content { get; set; }

        //TODO - uma vez que isto está no modelo Subscrition_Data penso que aqui não faça muito
        public string Creation_dt { get; set; } = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
        public int Parent { get; set; }
        public string Res_type { get; set;}
    }
}