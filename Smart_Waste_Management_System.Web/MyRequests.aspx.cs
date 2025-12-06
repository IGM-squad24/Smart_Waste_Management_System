using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using Smart_Waste_Management_System.Web;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Smart_Waste_Management_System.Web
{
    public partial class MyRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                ddlStatusFilter.SelectedValue = "All";
                LoadMyRequests();
                UpdateRequestStatistics();
            }
        }
        private void LoadMyRequests()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                PickupService pickupService = new PickupService();
                var requests = pickupService.GetUserPickupRequests(userId);

                // Apply status filter

                string statusFilter = ddlStatusFilter.SelectedValue;
                if (statusFilter != "All")
                {
                    requests = requests
                        .Where(r => r.Status.Trim().Equals(statusFilter, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }


                // Sort by request date (newest first)
                requests = requests.OrderByDescending(r => r.RequestDate).ToList();

                gvMyRequests.DataSource = requests;
                gvMyRequests.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading requests: {ex.Message}");
            }
        }

        private void UpdateRequestStatistics()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                PickupService pickupService = new PickupService();
                var allRequests = pickupService.GetUserPickupRequests(userId);

                totalRequests.InnerText = allRequests.Count.ToString();
                pendingRequests.InnerText = allRequests.Count(r => r.Status == "Pending").ToString();
                approvedRequests.InnerText = allRequests.Count(r => r.Status == "Approved").ToString();
                completedRequests.InnerText = allRequests.Count(r => r.Status == "Completed").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating statistics: {ex.Message}");
            }
        }

        protected void DdlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMyRequests();
            UpdateRequestStatistics();
        }

        protected void BtnNewRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestPickup.aspx");
        }

        protected void GvMyRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyRequests.PageIndex = e.NewPageIndex;
            LoadMyRequests();
        }

        protected void GvMyRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PickupRequest request = (PickupRequest)e.Row.DataItem;

                // Find status badge control
                HtmlGenericControl statusBadge = (HtmlGenericControl)e.Row.FindControl("statusBadge");

                if (statusBadge != null)
                {
                    // Set status badge color
                    statusBadge.Attributes["class"] = "status-badge status-" + request.Status.ToLower().Replace(" ", "");
                }
            }
        }

        protected void BtnViewDetails_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string requestId = btn.CommandArgument;

            // Show request details - you can implement a modal or details page
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('Request Details for ID: {requestId}\\n\\nYou can implement a detailed view here.');", true);
        }

        protected void BtnCancelRequest_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int requestId = Convert.ToInt32(btn.CommandArgument);

            try
            {
                PickupService pickupService = new PickupService();
                bool success = pickupService.UpdateRequestStatus(requestId, "Cancelled", "Cancelled by user.");

                if (success)
                {
                    // Reload the data
                    LoadMyRequests();
                    UpdateRequestStatistics();

                    // Show success message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Request cancelled successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Failed to cancel request. Please try again.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"alert('Error cancelling request: {ex.Message}');", true);
            }
        }
    }
}