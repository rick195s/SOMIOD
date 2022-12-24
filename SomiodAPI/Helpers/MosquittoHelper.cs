using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using uPLibrary.Networking.M2Mqtt;

namespace SomiodAPI.Helpers
{
    public class MosquittoHelper
    {

        // static MqttClient mClient = new MqttClient(Dns.GetHostAddresses("test.mosquitto.org")[0]);
        // static MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); 
    
        public static int PublishData(IPAddress ipAddress, string channelName, Data data)
        {
            MqttClient mClient = new MqttClient(ipAddress);
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                throw new Exception("Error connecting to message broker...");
            }
            mClient.Publish(channelName, Encoding.UTF8.GetBytes(data.Content.ToString()));
            return 0;
        }
    }
}