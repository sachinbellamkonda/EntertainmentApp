using EntertainmentApp.Models;
using EntertainmentApp.ServiceReference1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CseLibrary;

namespace EntertainmentApp
{
    public partial class memberSignup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CaptchaControl.DisplayCaptcha();
            }
        }

        private Response AuthenticateSignup(string username, string password)
        {
            var client = new Service1Client();
            string response = client.SignUp(username, password, "Member",null);
            var responseObj = JsonConvert.DeserializeObject<Response>(response);
            if (responseObj != null && responseObj.Status)
            {
                // Create a new cookie
                HttpCookie userSessionCookie = new HttpCookie("UserSession");

                // Serialize the entire UserSession object to JSON
                string jsonSession = JsonConvert.SerializeObject(responseObj.CurrentSession);
                userSessionCookie.Value = jsonSession;

                // Set the expiration date for the cookie
                userSessionCookie.Expires = DateTime.Now.AddHours(48);

                // Add the cookie to the response
                HttpContext.Current.Response.Cookies.Add(userSessionCookie);


                Session["currentSession"] = responseObj.CurrentSession;
            }
            return responseObj;
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmpassword = txtConfirmPassword.Text.Trim();
            if (!password.Equals(confirmpassword))
            {
                lblResult.Text = "Passwords did not match please try again";
                lblResult.CssClass = "error";
            }
            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            string userCaptchaInput = CaptchaControl.UserInput; // Accessing the UserInput property

            // Validate CAPTCHA
            bool isCaptchaValid = CaptchaControl.IsValidCaptcha();

            if (isCaptchaValid)
            {
                Response response = AuthenticateSignup(username, encryptedPassword);
                if (response != null && response.Status)
                {
                    lblResult.Text = "Login successful! Welcome " + username;
                    lblResult.CssClass = "success";
                    Response.Redirect("searchmovies.aspx");
                }
                else
                {
                    lblResult.Text = response.Message;
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