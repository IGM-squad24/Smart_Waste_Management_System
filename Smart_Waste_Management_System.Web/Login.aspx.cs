using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Waste_Management_System.Web
{
    public partial class Login : System.Web.UI.Page
    {
        UserService userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblMessage.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please fill all fields.";
                return;
            }

            try
            {
                User user = userService.Authenticate(email, password);

                if (user != null)
                {
                    Session["UserID"] = user.UserID;
                    Session["UserName"] = user.FullName;
                    Session["Role"] = user.Role;  // ← This line is crucial!
                    Session["FullName"] = user.FullName;
                    btnLogin.Visible = false;
                    Response.Redirect("Dashboard.aspx");
                    
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}

