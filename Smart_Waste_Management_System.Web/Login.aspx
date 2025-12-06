<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Smart_Waste_Management_System.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-box {
            width: 400px;
            margin: 60px auto;
            background: #fff;
            padding: 30px;
            border-radius: 6px;
            box-shadow: 0 0 8px rgba(0,0,0,0.1);
        }
        .login-box h2 {
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
    <div class="login-box">
        <h2>User Login</h2>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter Username"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password"></asp:TextBox>

        <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="Login" OnClick="btnLogin_Click" />
        <p style="text-align:center; margin-top:10px;">
            <a href="Register.aspx" style="text-decoration:none; color:#2e7d32;">New User? Register Here</a>
        </p>
    </div>
</asp:Content>
