<%@ Page Title="View Bin Status" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewBinStatus.aspx.cs" Inherits="Smart_Waste_Management_System.Web.ViewBinStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bin-container {
            margin: 40px auto;
            width: 90%;
            max-width: 900px;
            background-color: #fff;
            padding: 25px;
            border-radius: 6px;
            box-shadow: 0px 0px 8px rgba(0,0,0,0.1);
        }
        h2 {
            color: #2e7d32;
            text-align: center;
            margin-bottom: 25px;
        }
        .gridview {
            width: 100%;
            border-collapse: collapse;
        }
        .gridview th, .gridview td {
            border: 1px solid #ccc;
            padding: 10px;
            text-align: center;
        }
        .gridview th {
            background-color: #2e7d32;
            color: white;
        }
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .gridview tr:hover {
            background-color: #e8f5e9;
        }
        .message {
            color: red;
            text-align: center;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bin-container">
        <h2>Bin Status Overview</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
        <asp:GridView ID="gvBins" runat="server" CssClass="gridview" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="BinID" HeaderText="Bin ID" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                <asp:BoundField DataField="Capacity" HeaderText="Capacity (kg)" />
                <asp:BoundField DataField="CurrentLevel" HeaderText="Current Level (kg)" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
