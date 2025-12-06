using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Waste_Management_System.Web
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if user is not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string name = Session["UserName"] != null ? Session["UserName"].ToString() : "User";
                lblWelcome.Text = "Welcome, " + name + "!";
            }
        }
        protected void btnDashboard_Click(object sender,EventArgs e)
        {

        }
    }
}
