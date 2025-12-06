using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Smart_Waste_Management_System.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide logout if user not logged in
            if (Session["UserID"] == null)
            {

                btnLogout.Visible = false;
               

            }
            else
                btnLogout.Visible = true;
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
       
        }
    }


