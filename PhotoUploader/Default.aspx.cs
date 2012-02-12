using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace PhotoUploader
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["photoBase"];
            var provider = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            var connection = provider.CreateConnection();
            connection.ConnectionString = connectionStringSettings.ConnectionString;
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                _resultLabel.Text = "I Connect to (" + connection.ConnectionString + ")!!!!";
                connection.Close();
            }
            catch (Exception)
            {
                _resultLabel.Text = "Can't Connect to (" + connection.ConnectionString + ")!!!!";
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
