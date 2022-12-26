using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ApplicationA
{
    public partial class Form1 : Form
    {

        string baseURI = @"http://localhost:55390/api/somiod"; //TODO: needs to be updated!
        RestClient client = null;

        private List<Models.Application> applications = new List<Models.Application>();
        private List<Models.Module> modules = new List<Models.Module>();
        private string[] subType =
        {
            "creation",
            "deletion"
        };

        public Form1()
        {
            InitializeComponent();
            client = new RestClient(baseURI);

           
            populateApplicationsList();
            populateModulesList();

            foreach (string type in subType)
            {
                checkedListBoxSubType.Items.Add(type);
            }
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
            populateApplicationsList();
        }

        private void btnCreateModule_Click(object sender, EventArgs e)
        {
            Models.Application selectedApplication = (Models.Application) applicationsList.SelectedItem;

            if (selectedApplication == null)
            {
                return;
            }
            var request = new RestRequest("/" + selectedApplication.Name, Method.Post);
            Models.Module module = new Models.Module
            {
                Name = textBoxModuleName.Text,
            };

            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(module);

            RestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode.ToString());
            populateModulesList();
        }


        private void btnCreateSub_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSubType.CheckedItems.Count == 0)
            {
                MessageBox.Show("You need to selected at least one sub type");
                return;
            }

            Models.Module selectedModule = (Models.Module)modulesList.SelectedItem;

            if (selectedModule == null)
            {
                MessageBox.Show("You need to selected one Module to subscribe");
                return;
            }

            Models.Application application = GetModuleApplication(selectedModule.Parent);
            if (application == null)
            {
                MessageBox.Show("No application related to module found");
                return;
            }

            foreach (string type in checkedListBoxSubType.CheckedItems)
            {
                Models.Subscription_Data subscription = new Models.Subscription_Data
                {
                    Name = textBoxSubName.Text,
                    Endpoint = textBoxEndpoint.Text,
                    Event = type,
                    Res_type = "subscription"
                };

                RestResponse response = CreateSubscription(subscription, application.Name, selectedModule.Name);
                MessageBox.Show(response.StatusCode.ToString());
            }

        }

        private RestResponse CreateSubscription(Models.Subscription_Data subscription, string applicationName, string moduleName)
        {
            var request = new RestRequest("/" + applicationName + "/" + moduleName, Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddXmlBody(subscription);

            return client.Execute(request);
        }

        private Models.Application GetModuleApplication(int parentId)
        {
            foreach (Models.Application application in applications)
            {
                if (application.Id == parentId)
                {
                    return application;
                }
            }

            return null;
        }

        private void populateApplicationsList()
        {
            var request = new RestRequest("/applications", Method.Get);
            request.RequestFormat = DataFormat.Xml;
            
            List<Models.Application> response = client.Execute<List<Models.Application>>(request).Data;
            applications = response;
            applicationsList.DataSource = applications;
        }

        private void populateModulesList()
        {
            var request = new RestRequest("/modules", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            List<Models.Module> response = client.Execute<List<Models.Module>>(request).Data;
            modules = response;
            modulesList.DataSource = modules;
        }
    }
}
