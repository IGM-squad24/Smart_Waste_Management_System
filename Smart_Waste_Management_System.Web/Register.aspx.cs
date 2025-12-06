using smart_waste_management.BusinessLogic.Services;
using smart_waste_management.Entities.Models;
using System;
using System.Web.UI;

namespace Smart_Waste_Management_System.Web
{
    public partial class Register : Page
    {
        private readonly UserService userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblMessage.Text = "";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirmPassword.Text.Trim();

            // Basic validations
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirm))
            {
                lblMessage.Text = "Please fill all fields.";
                return;
            }

            if (password != confirm)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            try
            {
                User user = new User
                {
                    Username = username,
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    Adress = address,
                    Password = password
                };

                int result = userService.Register(user); 

                if (result > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Registration successful! Redirecting to login...";
                    Response.AddHeader("REFRESH", "2;URL=Login.aspx");
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Registration failed. Try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
