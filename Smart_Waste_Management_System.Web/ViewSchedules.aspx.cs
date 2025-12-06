using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Smart_Waste_Management_System.Web
{
    public partial class ViewSchedules : System.Web.UI.Page
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

                SetDefaultDateRange();
                LoadSchedulesData();
                LoadUpcomingSchedules();
                UpdateStatistics();
            }
        }

        private void SetDefaultDateRange()
        {
            // Set default date range to next 30 days
            txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtToDate.Text = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");
        }

        private void LoadSchedulesData()
        {
            try
            {
                ScheduleService scheduleService = new ScheduleService();
                var schedules = scheduleService.GetAllSchedules();

                // Apply status filter
                string statusFilter = ddlStatusFilter.SelectedValue;
                if (statusFilter != "All")
                {
                    schedules = schedules.Where(s => s.Status == statusFilter).ToList();
                }

                // Apply date filter
                if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                {
                    DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime toDate = Convert.ToDateTime(txtToDate.Text).AddDays(1).AddSeconds(-1); // Include entire end day

                    schedules = schedules.Where(s => s.ScheduledDate >= fromDate && s.ScheduledDate <= toDate).ToList();
                }

                // Sort by scheduled date
                schedules = schedules.OrderBy(s => s.ScheduledDate).ToList();

                gvSchedules.DataSource = schedules;
                gvSchedules.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading schedules: {ex.Message}");
            }
        }

        private void LoadUpcomingSchedules()
        {
            try
            {
                ScheduleService scheduleService = new ScheduleService();
                var allSchedules = scheduleService.GetAllSchedules();

                // Get schedules for next 7 days
                var upcoming = allSchedules
                    .Where(s => s.ScheduledDate >= DateTime.Now && s.ScheduledDate <= DateTime.Now.AddDays(7))
                    .Where(s => s.Status == "Scheduled" || s.Status == "In Progress")
                    .OrderBy(s => s.ScheduledDate)
                    .Take(10) // Limit to 10 upcoming schedules
                    .ToList();

                rptUpcoming.DataSource = upcoming;
                rptUpcoming.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading upcoming schedules: {ex.Message}");
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                ScheduleService scheduleService = new ScheduleService();
                var allSchedules = scheduleService.GetAllSchedules();

                totalSchedules.InnerText = allSchedules.Count.ToString();
                scheduledCount.InnerText = allSchedules.Count(s => s.Status == "Scheduled").ToString();
                inProgressCount.InnerText = allSchedules.Count(s => s.Status == "In Progress").ToString();
                completedCount.InnerText = allSchedules.Count(s => s.Status == "Completed").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating statistics: {ex.Message}");
            }
        }

        protected void DdlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchedulesData();
        }

        protected void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadSchedulesData();
        }

        protected void BtnClearFilter_Click(object sender, EventArgs e)
        {
            ddlStatusFilter.SelectedValue = "All";
            SetDefaultDateRange();
            LoadSchedulesData();
        }

        protected void GvSchedules_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSchedules.PageIndex = e.NewPageIndex;
            LoadSchedulesData();
        }

        protected void GvSchedules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Schedule schedule = (Schedule)e.Row.DataItem;

                // Find status badge control
                HtmlGenericControl statusBadge = (HtmlGenericControl)e.Row.FindControl("statusBadge");

                if (statusBadge != null)
                {
                    // Set status badge color
                    statusBadge.Attributes["class"] = "status-badge status-" + schedule.Status.ToLower().Replace(" ", "");
                }
            }
        }

        protected void BtnViewDetails_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string scheduleId = btn.CommandArgument;

            // You can implement a modal or redirect to details page
            // For now, we'll show a simple alert
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('Schedule Details for ID: {scheduleId}');", true);
        }
    }
}