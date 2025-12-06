using Microsoft.Reporting.WebForms;
using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Smart_Waste_Management_System.Web
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check authentication only
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
        }

        // Report Generation Methods
        protected void BtnBinReport_Click(object sender, EventArgs e)
        {
            LoadBinReport();
        }

        protected void BtnPickupReport_Click(object sender, EventArgs e)
        {
            LoadPickupReport();
        }

        protected void BtnScheduleReport_Click(object sender, EventArgs e)
        {
            LoadScheduleReport();
        }

        // RDLC Report Methods

        private void LoadBinReport()
        {
            try
            {
                BinService binService = new BinService();
                var bins = binService.GetAllBins();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/BinStatusReport.rdlc");

                ReportDataSource dataSource = new ReportDataSource("DataSetBins", bins);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSource);

                pnlReportViewer.Visible = true;
                //pnlDataPreview.Visible = false;
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                // If RDLC fails, fall back to data preview
                System.Diagnostics.Debug.WriteLine($"Error generating bin report: {ex.Message}");

            }
        }

        private void LoadPickupReport()
        {
            try
            {
                PickupService pickupService = new PickupService();
                var pickups = pickupService.GetAllPickupRequests();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/PickupRequestsReport.rdlc");

                ReportDataSource dataSource = new ReportDataSource("PickupRequestDataSet", pickups);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSource);

                pnlReportViewer.Visible = true;
                //pnlDataPreview.Visible = false;
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating pickup report: {ex.Message}");

            }
        }

        private void LoadScheduleReport()
        {
            try
            {
                ScheduleService scheduleService = new ScheduleService();
                var schedules = scheduleService.GetAllSchedules();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/SchedulesReport.rdlc");

                ReportDataSource dataSource = new ReportDataSource("ScheduleDataSet1", schedules);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(dataSource);

                pnlReportViewer.Visible = true;
                //pnlDataPreview.Visible = false;
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating schedule report: {ex.Message}");

            }
        }
    }
}

