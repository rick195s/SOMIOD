using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ApplicationB
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:55390/api/somiod"; //TODO: needs to be updated!
        RestClient client = null;
        public Form1()
        {
            client = new RestClient(baseURI);

            InitializeComponent();
        }

        private void btnCreateApplication_Click(object sender, EventArgs e)
        {
            var nameApp = textBoxApplicationName.Text;
            var request = new RestRequest("/" + nameApp, Method.Post);
            Models.Module module = new Models.Module
            {
                Name = textBox1.Text,
            };

            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(module);

            RestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = new RestRequest("/" + textBox2.Text + "/" + textBox3.Text, Method.Post);
            Models.Subscription_Data data;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Content></Content>");

            if (checkBox1.Checked)
            {
                doc.DocumentElement.AppendChild(doc.CreateTextNode("ON"));
            }
            else {
                doc.DocumentElement.AppendChild(doc.CreateTextNode("OFF"));
            }

            data = new Models.Subscription_Data
            {
                Content = doc,
                Res_type = "data"
            };

            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(data);

            RestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode.ToString());
        }

    }
}
