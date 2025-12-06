<%@ Page Title="View Schedules" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSchedules.aspx.cs" Inherits="Smart_Waste_Management_System.Web.ViewSchedules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="schedules-container">
        <h2>Collection Schedules</h2>
        
        <!-- Filter Options -->
        <div class="filter-section">
            <asp:Label ID="lblFilter" runat="server" Text="Filter by Status:"></asp:Label>
            <asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlStatusFilter_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="All Schedules"></asp:ListItem>
                <asp:ListItem Value="Scheduled" Text="Scheduled"></asp:ListItem>
                <asp:ListItem Value="In Progress" Text="In Progress"></asp:ListItem>
                <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                <asp:ListItem Value="Cancelled" Text="Cancelled"></asp:ListItem>
            </asp:DropDownList>
            
            <asp:Label ID="lblDateFilter" runat="server" Text="Date Range:"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="date-picker" TextMode="Date" placeholder="From Date"></asp:TextBox>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="date-picker" TextMode="Date" placeholder="To Date"></asp:TextBox>
            
            <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filters" CssClass="btn btn-primary" OnClick="BtnApplyFilter_Click" />
            <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filters" CssClass="btn btn-secondary" OnClick="BtnClearFilter_Click" CausesValidation="false" />
        </div>

        <!-- Schedule Statistics -->
        <div class="schedule-stats">
            <div class="stat-card">
                <div class="stat-icon">📅</div>
                <div class="stat-info">
                    <span class="stat-number" id="totalSchedules" runat="server">0</span>
                    <span class="stat-label">Total Schedules</span>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon">⏳</div>
                <div class="stat-info">
                    <span class="stat-number" id="scheduledCount" runat="server">0</span>
                    <span class="stat-label">Scheduled</span>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon">🔄</div>
                <div class="stat-info">
                    <span class="stat-number" id="inProgressCount" runat="server">0</span>
                    <span class="stat-label">In Progress</span>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon">✅</div>
                <div class="stat-info">
                    <span class="stat-number" id="completedCount" runat="server">0</span>
                    <span class="stat-label">Completed</span>
                </div>
            </div>
        </div>

        <!-- Schedules GridView -->
        <div class="schedules-grid">
            <asp:GridView ID="gvSchedules" runat="server" AutoGenerateColumns="false" CssClass="schedules-table"
                EmptyDataText="No schedules found" ShowHeaderWhenEmpty="true" OnRowDataBound="GvSchedules_RowDataBound"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="GvSchedules_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="ScheduleID" HeaderText="Schedule ID" />
                    <asp:BoundField DataField="Location" HeaderText="Bin Location" />
                    <asp:BoundField DataField="AssignedStaff" HeaderText="Assigned Staff" NullDisplayText="Unassigned" />
                    <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                    <asp:BoundField DataField="CompletedDate" HeaderText="Completed Date" DataFormatString="{0:MM/dd/yyyy HH:mm}" 
                        ConvertEmptyStringToNull="true" NullDisplayText="-" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class="status-badge" id="statusBadge" runat="server">
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnViewDetails" runat="server" Text="View Details" CssClass="btn btn-small" 
                                CommandArgument='<%# Eval("ScheduleID") %>' OnClick="BtnViewDetails_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="grid-pager" />
            </asp:GridView>
        </div>

        <!-- Upcoming Schedules -->
        <div class="upcoming-schedules">
            <h3>Upcoming Pickups (Next 7 Days)</h3>
            <asp:Repeater ID="rptUpcoming" runat="server">
                <ItemTemplate>
                    <div class="upcoming-item">
                        <div class="upcoming-date">
                            <span class="date-day"><%# ((DateTime)Eval("ScheduledDate")).ToString("dd") %></span>
                            <span class="date-month"><%# ((DateTime)Eval("ScheduledDate")).ToString("MMM") %></span>
                        </div>
                        <div class="upcoming-details">
                            <div class="location"><%# Eval("Location") %></div>
                            <div class="staff">Assigned: <%# Eval("AssignedStaff") ?? "Unassigned" %></div>
                            <div class="time"><%# ((DateTime)Eval("ScheduledDate")).ToString("hh:mm tt") %></div>
                        </div>
                        <div class="upcoming-status">
                            <span class='status-badge status-<%# Eval("Status").ToString().ToLower() %>'>
                                <%# Eval("Status") %>
                            </span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <style>
        .schedules-container { padding: 20px; }
        .filter-section { 
            margin-bottom: 20px; 
            padding: 15px; 
            background-color: #f5f5f5; 
            border-radius: 5px; 
            display: flex; 
            align-items: center; 
            flex-wrap: wrap; 
            gap: 10px; 
        }
        .filter-section label { font-weight: bold; white-space: nowrap; }
        .filter-section select, .filter-section .date-picker, .filter-section .btn { 
            padding: 5px 10px; 
        }
        .date-picker { width: 150px; }
        
        .schedule-stats { 
            display: grid; 
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); 
            gap: 15px; 
            margin-bottom: 25px; 
        }
        .stat-card { 
            background: white; 
            padding: 20px; 
            border-radius: 8px; 
            box-shadow: 0 2px 4px rgba(0,0,0,0.1); 
            display: flex; 
            align-items: center; 
            gap: 15px; 
        }
        .stat-icon { font-size: 24px; }
        .stat-number { 
            display: block; 
            font-size: 24px; 
            font-weight: bold; 
            color: #2E8B57; 
        }
        .stat-label { 
            display: block; 
            font-size: 14px; 
            color: #666; 
            margin-top: 5px; 
        }
        
        .schedules-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
        .schedules-table th { background-color: #2E8B57; color: white; padding: 12px; text-align: left; }
        .schedules-table td { padding: 10px; border-bottom: 1px solid #ddd; }
        .schedules-table tr:nth-child(even) { background-color: #f9f9f9; }
        
        .status-badge { 
            padding: 4px 8px; 
            border-radius: 12px; 
            font-size: 12px; 
            font-weight: bold; 
            color: white; 
        }
        .status-scheduled { background-color: #17a2b8; }
        .status-inprogress { background-color: #ffc107; color: #000; }
        .status-completed { background-color: #28a745; }
        .status-cancelled { background-color: #dc3545; }
        
        .btn { 
            padding: 8px 12px; 
            border: none; 
            border-radius: 4px; 
            cursor: pointer; 
            font-size: 12px; 
        }
        .btn-small { padding: 4px 8px; font-size: 11px; }
        .btn-primary { background-color: #2E8B57; color: white; }
        .btn-primary:hover { background-color: #3CB371; }
        .btn-secondary { background-color: #6c757d; color: white; }
        .btn-secondary:hover { background-color: #5a6268; }
        
        .upcoming-schedules { margin-top: 30px; }
        .upcoming-item { 
            display: flex; 
            align-items: center; 
            padding: 15px; 
            margin-bottom: 10px; 
            background-color: #f8f9fa; 
            border-radius: 8px; 
            border-left: 4px solid #2E8B57; 
        }
        .upcoming-date { 
            text-align: center; 
            padding: 10px; 
            background-color: #2E8B57; 
            color: white; 
            border-radius: 6px; 
            min-width: 60px; 
        }
        .date-day { display: block; font-size: 20px; font-weight: bold; }
        .date-month { display: block; font-size: 12px; text-transform: uppercase; }
        .upcoming-details { flex: 1; padding: 0 15px; }
        .location { font-weight: bold; color: #333; }
        .staff, .time { font-size: 12px; color: #666; }
        .upcoming-status { min-width: 100px; text-align: center; }
        
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