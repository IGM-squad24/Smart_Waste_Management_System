<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="Smart_Waste_Management_System.Web.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dashboard-container {
            text-align: center;
            margin-top: 50px;
        }
        .welcome {
            font-size: 22px;
            color: #2e7d32;
            margin-bottom: 30px;
        }
        .card-container {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 20px;
        }
        .card {
            width: 220px;
            height: 120px;
            background-color: #ffffff;
            border: 1px solid #ccc;
            border-radius: 8px;
            text-align: center;
            box-shadow: 0px 0px 8px rgba(0,0,0,0.1);
            transition: 0.2s;
            cursor: pointer;
        }
        .card:hover {
            background-color: #e8f5e9;
            transform: scale(1.02);
        }
        .card a {
            text-decoration: none;
            color: #2e7d32;
            display: block;
            padding: 35px 10px;
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-container">
        <asp:Label ID="lblWelcome" runat="server" CssClass="welcome"></asp:Label>

        <div class="card-container">
            <div class="card"><a href="ViewBinStatus.aspx">View Bin Status</a></div>
            <div class="card"><a href="RequestPickup.aspx">Request Pickup</a></div>
            <div class="card"><a href="MyRequests.aspx">My Requests</a></div>
            <div class="card"><a href="ViewSchedules.aspx">View Schedules</a></div>
        </div>
    </div>
</asp:Content>
