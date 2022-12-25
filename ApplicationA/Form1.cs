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
        string baseURI = @"http://localhost:55390";
        RestClient client = null;

        public Form1()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
        }

        private void btnCreateApplication_Click(object sender, EventArgs e)
        {
            var request = new RestRequest("api/applications", Method.Post);
            request.RequestFormat = DataFormat.Json;

        }
    }
}
