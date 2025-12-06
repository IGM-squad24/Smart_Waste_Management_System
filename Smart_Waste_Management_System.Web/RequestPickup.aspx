<%@ Page Title="Request Pickup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RequestPickup.aspx.cs" Inherits="Smart_Waste_Management_System.Web.RequestPickup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pickup-box {
            width: 500px;
            margin: 50px auto;
            background: #fff;
            padding: 30px;
            border-radius: 6px;
            box-shadow: 0 0 8px rgba(0,0,0,0.1);
        }
        .pickup-box h2 {
            text-align: center;
            color: #2e7d32;
            margin-bottom: 25px;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .btn {
            width: 100%;
            padding: 10px;
            background-color: #2e7d32;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn:hover {
            background-color: #1b5e20;
        }
        .message {
            text-align: center;
            color: red;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pickup-box">
        <h2>Request Waste Pickup</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <asp:DropDownList ID="ddlBins" runat="server" CssClass="form-control"></asp:DropDownList>
        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="Enter Pickup Location"></asp:TextBox>
        <asp:TextBox ID="txtWasteType" runat="server" CssClass="form-control" placeholder="Enter Waste Type"></asp:TextBox>
        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" placeholder="Approximate Weight (kg)"></asp:TextBox>
        <asp:TextBox ID="txtPickupDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Additional Comments (optional)"></asp:TextBox>
        <asp:Button ID="btnRequest" runat="server" CssClass="btn" Text="Submit Pickup Request" OnClick="btnRequest_Click" />
    </div>
</asp:Content>
