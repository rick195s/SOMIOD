using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;

namespace SomiodAPI.Helpers
{
    public class MosquittoHelper
    {

        // static MqttClient mClient = new MqttClient(Dns.GetHostAddresses("test.mosquitto.org")[0]);
        // static MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); 
    
        public static int PublishData(IPAddress ipAddress, string subEvent, string channelName, Data data)
        {
            MqttClient mClient = new MqttClient(ipAddress);
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                throw new Exception("Error connecting to message broker...");
            }

            data.Event = subEvent;
            mClient.Publish(channelName, Encoding.UTF8.GetBytes(serializeObjectToXML(data)));
            data.Event = null;
            return 0;
        }

        private static string serializeObjectToXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, obj);
            ms.Position = 0;

            using (StreamReader r = new StreamReader(ms))
            {
                return r.ReadToEnd();
            };
        }
    }
}