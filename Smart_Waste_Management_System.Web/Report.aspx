<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Smart_Waste_Management_System.Web.Reports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="reports-container">
        <h2>📊 System Reports</h2>

        <!-- Report Buttons -->
        <div class="report-buttons">
            <div class="button-group">
                <h4>Generate Reports</h4>
                <asp:Button ID="btnBinReport" runat="server" Text="📦 Bin Status Report" 
                    CssClass="btn btn-report btn-bin" OnClick="BtnBinReport_Click" />
                <asp:Button ID="btnPickupReport" runat="server" Text="🔄 Pickup Requests Report" 
                    CssClass="btn btn-report btn-pickup" OnClick="BtnPickupReport_Click" />
                <asp:Button ID="btnScheduleReport" runat="server" Text="📅 Collection Schedules Report" 
                    CssClass="btn btn-report btn-schedule" OnClick="BtnScheduleReport_Click" />
            </div>
        </div>

        <!-- Report Viewer -->
        <div class="report-viewer">
            <asp:Panel ID="pnlReportViewer" runat="server" Visible="false">
                <h4>Report Preview</h4>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="600px"
                    ZoomMode="Percent" SizeToReportContent="false">
                </rsweb:ReportViewer>
            </asp:Panel>
        </div>
    </div>

    <style>
        .reports-container { padding: 20px; max-width: 1200px; margin: 0 auto; }
        
        .report-buttons { 
            display: grid; 
            grid-template-columns: 1fr 1fr; 
            gap: 30px; 
            margin-bottom: 30px; 
        }
        .button-group { 
            background: #f8f9fa; 
            padding: 20px; 
            border-radius: 8px; 
            border-left: 4px solid #2E8B57; 
        }
        .button-group h4 { margin-top: 0; color: #2E8B57; }
        
        .btn { 
            padding: 12px 20px; 
            margin: 8px; 
            border: none; 
            border-radius: 6px; 
            cursor: pointer; 
            font-size: 14px; 
            font-weight: bold;
            display: block;
            width: 100%;
            text-align: left;
        }
        .btn-report { background: #e8f5e8; color: #2E8B57; border: 2px solid #2E8B57; }
        .btn-report:hover { background: #2E8B57; color: white; }
        .btn-export { background: #fff3cd; color: #856404; border: 2px solid #ffc107; }
        .btn-export:hover { background: #ffc107; color: black; }
        
        .btn-bin { border-left: 4px solid #28a745; }
        .btn-pickup { border-left: 4px solid #17a2b8; }
        .btn-schedule { border-left: 4px solid #6f42c1; }
        .btn-csv { border-left: 4px solid #fd7e14; }
        
        .report-viewer, .data-preview { 
            margin-top: 30px; 
            border: 1px solid #ddd; 
            padding: 15px; 
            background: white; 
            border-radius: 8px; 
        }
        
        .preview-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
        .preview-table th { background-color: #2E8B57; color: white; padding: 12px; text-align: left; }
        .preview-table td { padding: 10px; border-bottom: 1px solid #ddd; }
        .preview-table tr:nth-child(even) { background-color: #f9f9f9; }
        
        .data-summary { 
            padding: 15px; 
            background: #e8f5e8; 
            border-radius: 6px; 
            font-weight: bold; 
            color: #2E8B57; 
        }
        
        .grid-pager { padding: 10px; text-align: center; }
        .grid-pager a { 
            padding: 5px 10px; 
            margin: 0 2px; 
            border: 1px solid #ddd; 
            text-decoration: none; 
            color: #2E8B57; 
        }
        .grid-pager span { 
            padding: 5px 10px; 
            margin: 0 2px; 
            background-color: #2E8B57; 
            color: white; 
            border: 1px solid #2E8B57; 
        }
    </style>
</asp:Content>