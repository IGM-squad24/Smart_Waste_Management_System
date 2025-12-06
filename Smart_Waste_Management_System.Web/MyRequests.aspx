<%@ Page Title="My Requests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyRequests.aspx.cs" Inherits="Smart_Waste_Management_System.Web.MyRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-requests-container">
        <h2>My Pickup Requests</h2>
        
        <!-- Filter and Actions -->
        <div class="actions-section">
            <div class="filter-controls">
                <asp:Label ID="lblFilter" runat="server" Text="Filter by Status:"></asp:Label>
                <asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlStatusFilter_SelectedIndexChanged">
                  <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
<asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
<asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
<asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
<asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
<asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
<asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>

                </asp:DropDownList>
                
                <asp:Button ID="btnNewRequest" runat="server" Text="New Request" CssClass="btn btn-primary" OnClick="BtnNewRequest_Click" />
            </div>
        </div>

        <!-- Request Statistics -->
        <div class="request-stats">
            <div class="stat-card">
                <div class="stat-icon">📋</div>
                <div class="stat-info">
                    <span class="stat-number" id="totalRequests" runat="server">0</span>
                    <span class="stat-label">Total Requests</span>
                </div>
            </div>
            <div class="stat-card pending">
                <div class="stat-icon">⏳</div>
                <div class="stat-info">
                    <span class="stat-number" id="pendingRequests" runat="server">0</span>
                    <span class="stat-label">Pending</span>
                </div>
            </div>
            <div class="stat-card approved">
                <div class="stat-icon">✅</div>
                <div class="stat-info">
                    <span class="stat-number" id="approvedRequests" runat="server">0</span>
                    <span class="stat-label">Approved</span>
                </div>
            </div>
            <div class="stat-card completed">
                <div class="stat-icon">🏁</div>
                <div class="stat-info">
                    <span class="stat-number" id="completedRequests" runat="server">0</span>
                    <span class="stat-label">Completed</span>
                </div>
            </div>
        </div>

        <!-- Requests GridView -->
        <div class="requests-grid">
            <asp:GridView ID="gvMyRequests" runat="server" AutoGenerateColumns="false" CssClass="requests-table"
                EmptyDataText="You haven't made any pickup requests yet." ShowHeaderWhenEmpty="true"
                OnRowDataBound="GvMyRequests_RowDataBound" AllowPaging="true" PageSize="8" 
                OnPageIndexChanging="GvMyRequests_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="Location" HeaderText="Bin Location" />
                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="RequestedPickupDate" HeaderText="Requested Pickup" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class="status-badge" id="statusBadge" runat="server">
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Notes" HeaderText="My Notes" ConvertEmptyStringToNull="true" NullDisplayText="-" 
                        ItemStyle-CssClass="notes-column" />
                    <asp:BoundField DataField="AdminNotes" HeaderText="Admin Notes" ConvertEmptyStringToNull="true" NullDisplayText="-" 
                        ItemStyle-CssClass="notes-column" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnViewDetails" runat="server" Text="View" CssClass="btn btn-small btn-info" 
                                CommandArgument='<%# Eval("RequestID") %>' OnClick="BtnViewDetails_Click" />
                            <asp:Button ID="btnCancelRequest" runat="server" Text="Cancel" CssClass="btn btn-small btn-danger" 
                                Visible='<%# Eval("Status").ToString() == "Pending" || Eval("Status").ToString() == "Approved" %>'
                                CommandArgument='<%# Eval("RequestID") %>' OnClick="BtnCancelRequest_Click" 
                                OnClientClick="return confirm('Are you sure you want to cancel this request?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="grid-pager" />
            </asp:GridView>
        </div>

        <!-- Quick Status Guide -->
        <div class="status-guide">
            <h4>Request Status Guide</h4>
            <div class="guide-items">
                <div class="guide-item">
                    <span class="status-indicator pending"></span>
                    <span><strong>Pending</strong> - Waiting for admin approval</span>
                </div>
                <div class="guide-item">
                    <span class="status-indicator approved"></span>
                    <span><strong>Approved</strong> - Request approved, waiting for scheduling</span>
                </div>
                <div class="guide-item">
                    <span class="status-indicator inprogress"></span>
                    <span><strong>In Progress</strong> - Pickup is being processed</span>
                </div>
                <div class="guide-item">
                    <span class="status-indicator completed"></span>
                    <span><strong>Completed</strong> - Pickup has been completed</span>
                </div>
                <div class="guide-item">
                    <span class="status-indicator rejected"></span>
                    <span><strong>Rejected</strong> - Request was not approved</span>
                </div>
            </div>
        </div>
    </div>

    <style>
        .my-requests-container { padding: 20px; }
        .actions-section { 
            margin-bottom: 20px; 
            padding: 15px; 
            background-color: #f5f5f5; 
            border-radius: 5px; 
            display: flex; 
            justify-content: space-between; 
            align-items: center; 
            flex-wrap: wrap; 
            gap: 10px; 
        }
        .filter-controls { display: flex; align-items: center; gap: 10px; }
        .filter-controls label { font-weight: bold; white-space: nowrap; }
        
        .request-stats { 
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
            border-left: 4px solid #2E8B57; 
        }
        .stat-card.pending { border-left-color: #ffc107; }
        .stat-card.approved { border-left-color: #28a745; }
        .stat-card.completed { border-left-color: #6c757d; }
        .stat-icon { font-size: 24px; }
        .stat-number { 
            display: block; 
            font-size: 24px; 
            font-weight: bold; 
            color: #2E8B57; 
        }
        .stat-card.pending .stat-number { color: #ffc107; }
        .stat-card.approved .stat-number { color: #28a745; }
        .stat-card.completed .stat-number { color: #6c757d; }
        .stat-label { 
            display: block; 
            font-size: 14px; 
            color: #666; 
            margin-top: 5px; 
        }
        
        .requests-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
        .requests-table th { background-color: #2E8B57; color: white; padding: 12px; text-align: left; }
        .requests-table td { padding: 10px; border-bottom: 1px solid #ddd; }
        .requests-table tr:nth-child(even) { background-color: #f9f9f9; }
        .notes-column { max-width: 150px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
        
        .status-badge { 
            padding: 4px 8px; 
            border-radius: 12px; 
            font-size: 12px; 
            font-weight: bold; 
            color: white; 
        }
        .status-pending { background-color: #ffc107; color: #000; }
        .status-approved { background-color: #28a745; }
        .status-inprogress { background-color: #17a2b8; }
        .status-completed { background-color: #6c757d; }
        .status-rejected { background-color: #dc3545; }
        
        .btn { 
            padding: 8px 12px; 
            border: none; 
            border-radius: 4px; 
            cursor: pointer; 
            font-size: 12px; 
            margin: 2px; 
        }
        .btn-small { padding: 4px 8px; font-size: 11px; }
        .btn-primary { background-color: #2E8B57; color: white; }
        .btn-primary:hover { background-color: #3CB371; }
        .btn-info { background-color: #17a2b8; color: white; }
        .btn-info:hover { background-color: #138496; }
        .btn-danger { background-color: #dc3545; color: white; }
        .btn-danger:hover { background-color: #c82333; }
        
        .status-guide { 
            margin-top: 30px; 
            padding: 15px; 
            background-color: #f8f9fa; 
            border-radius: 6px; 
            border-left: 4px solid #2E8B57; 
        }
        .guide-items { display: flex; flex-direction: column; gap: 8px; }
        .guide-item { display: flex; align-items: center; gap: 10px; }
        .status-indicator { 
            width: 12px; 
            height: 12px; 
            border-radius: 50%; 
            display: inline-block; 
        }
        .status-indicator.pending { background-color: #ffc107; }
        .status-indicator.approved { background-color: #28a745; }
        .status-indicator.inprogress { background-color: #17a2b8; }
        .status-indicator.completed { background-color: #6c757d; }
        .status-indicator.rejected { background-color: #dc3545; }
        
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