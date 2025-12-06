using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Smart_Waste_Management_System.Web
{
    public partial class RequestPickup : System.Web.UI.Page
    {
        private readonly PickupService pickupService = new PickupService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBins();
            }
        }

        private void LoadBins()
        {
            try
            {
                BinService binService = new BinService();
                var bins = binService.GetAllBins();

                ddlBins.DataSource = bins;
                ddlBins.DataTextField = "Location";   // what user sees
                ddlBins.DataValueField = "BinID";     // actual ID passed to backend
                ddlBins.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading bins: " + ex.Message;
            }
        }


        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string location = txtLocation.Text.Trim();
            string wasteType = txtWasteType.Text.Trim();
            string weightText = txtWeight.Text.Trim();
            string comments = txtComments.Text.Trim();

            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(wasteType) || string.IsNullOrEmpty(weightText))
            {
                lblMessage.Text = "Please fill all required fields.";
                return;
            }

            if (!double.TryParse(weightText, out double weight))
            {
                lblMessage.Text = "Please enter a valid number for weight.";
                return;
            }

            try
            {
                DateTime pickupDate;
                if (!DateTime.TryParse(txtPickupDate.Text, out pickupDate))
                {
                    lblMessage.Text = "Please enter a valid pickup date.";
                    return;
                }

                if (pickupDate.Date < DateTime.Now.Date)
                {
                    lblMessage.Text = "Pickup date cannot be in the past.";
                    return;
                }

                PickupRequest request = new PickupRequest
                {
                    UserID = Convert.ToInt32(Session["UserID"]),
                    BinID = Convert.ToInt32(ddlBins.SelectedValue),
                    Location = location,
                    WasteType = wasteType,
                    Weight = weight,
                    Comments = comments,
                    RequestDate = DateTime.Now,
                    RequestedPickupDate = pickupDate.Date, 
                    Status = "Pending"
                };

                int result = pickupService.CreatePickupRequest(request); 

                if (result > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Pickup request submitted successfully!";
                    txtLocation.Text = txtWasteType.Text = txtWeight.Text = txtComments.Text = "";
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Failed to submit pickup request. Try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}

