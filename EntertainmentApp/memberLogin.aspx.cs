using EntertainmentApp.ServiceReference1;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntertainmentApp.Models;
using CseLibrary;

namespace EntertainmentApp
{
    public partial class memberLogin : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user session exists
                HttpCookie userCookie = Request.Cookies["UserSession"];
                if (userCookie != null)
                {
                    try
                    {
                        // Deserialize the JSON stored in the cookie
                        var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);

                        if (sessionData != null && !string.IsNullOrEmpty(sessionData.SessionId))
                        {
                            // Session exists, redirect to SearchMovies page
                            Response.Redirect("searchmovies.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions (e.g., cookie tampering or deserialization issues)
                        Console.WriteLine("Error reading user session: " + ex.Message);
                    }
                }
                else
                {
                    // Generate CAPTCHA if no existing session
                    CaptchaControl.DisplayCaptcha();
                }
            }
          

        }

        
        private bool AuthenticateUser(string username, string password)
        {
            // Replace this with your actual authentication logic
            return username == "admin" && password == "password";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            string userCaptchaInput = CaptchaControl.UserInput; // Accessing the UserInput property

            // Validate CAPTCHA
            bool isCaptchaValid = CaptchaControl.IsValidCaptcha();

            if (isCaptchaValid)
            {
                if (AuthenticateUser(username, password))
                {
                    lblResult.Text = "Login successful! Welcome " + username;
                    lblResult.CssClass = "success";
                }
                else
                {
                    lblResult.Text = "Login failed. Please check your username and password.";
                    lblResult.CssClass = "error";
                }
            }
            else
            {
                lblResult.Text = "Invalid CAPTCHA. Please try again.";
                lblResult.CssClass = "error";
            }

            // Refresh the CAPTCHA regardless of success or failure
            CaptchaControl.DisplayCaptcha();
        }
    
    }
}