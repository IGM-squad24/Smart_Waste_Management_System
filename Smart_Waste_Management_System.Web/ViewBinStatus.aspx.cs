using smart_waste_management.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Waste_Management_System.Web
{
    public partial class ViewBinStatus : System.Web.UI.Page
    {
        private readonly BinService binService = new BinService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBinData();
            }
        }

        private void LoadBinData()
        {
            try
            {
                var bins = binService.GetAllBins(); 

                if (bins != null && bins.Count > 0)
                {
                    gvBins.DataSource = bins;
                    gvBins.DataBind();
                    lblMessage.Text = "";
                }
                else
                {
                    lblMessage.Text = "No bin data available.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading bin data: " + ex.Message;
            }
        }
    }
}

