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

namespace ApplicationA
{
    public partial class Form1 : Form
    {

        string baseURI = @"http://localhost:55390/api/somiod"; //TODO: needs to be updated!
        RestClient client = null;

        public Form1()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
        }

        private void btnCreateApplication_Click(object sender, EventArgs e)
        {
            var request = new RestRequest("/", Method.Post);
            Models.Application application = new Models.Application
            {
                Name = textBoxApplicationName.Text,
            };

            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(application);

            RestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode.ToString());
        }
    }
}
