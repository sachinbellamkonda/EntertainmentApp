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

namespace EntertainmentApp
{
    public partial class StaffLogin : System.Web.UI.Page
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
                            Response.Redirect("staff.aspx");
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
                    DisplayCaptcha();
                }
            }


        }

        public string GeneratedCaptchaText
        {
            get
            {
                return Session["GeneratedCaptchaText"] as string;
            }
            set
            {
                Session["GeneratedCaptchaText"] = value;
            }
        }

        private string GenerateRandomCaptchaText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] captchaText = new char[5];
            for (int i = 0; i < 5; i++)
            {
                captchaText[i] = chars[random.Next(chars.Length)];
            }

            return new string(captchaText);
        }

        private Bitmap GenerateCaptchaImage(string captchaText, int width, int height)
        {
            Bitmap captchaImage = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(captchaImage))
            {
                // Set background color
                graphics.Clear(Color.White);

                // Define fonts and brushes
                Font font = new Font("Arial", 24, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.Black);

                // Draw the CAPTCHA text
                graphics.DrawString(captchaText, font, brush, new PointF(10, 10));

                // Add noise (lines and dots)
                AddNoise(graphics, width, height);
            }
            return captchaImage;
        }

        private void AddNoise(Graphics graphics, int width, int height)
        {
            Random random = new Random();

            // Add random lines
            for (int i = 0; i < 5; i++)
            {
                Pen pen = new Pen(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                int x1 = random.Next(width);
                int y1 = random.Next(height);
                int x2 = random.Next(width);
                int y2 = random.Next(height);
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            // Add random dots
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                SolidBrush brush = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                graphics.FillEllipse(brush, x, y, 2, 2);
            }
        }

        private void DisplayCaptcha()
        {
            // Generate CAPTCHA text and store it
            GeneratedCaptchaText = GenerateRandomCaptchaText();

            // Generate CAPTCHA image
            Bitmap captchaImage = GenerateCaptchaImage(GeneratedCaptchaText, 200, 50);

            // Convert the image to a base64 string
            using (MemoryStream stream = new MemoryStream())
            {
                captchaImage.Save(stream, ImageFormat.Png);
                byte[] imageBytes = stream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                imgCaptcha.ImageUrl = "data:image/png;base64," + base64String;
            }
        }


        private bool ValidateCaptchaInput(string userInput)
        {
            System.Diagnostics.Debug.WriteLine(GeneratedCaptchaText);
            System.Diagnostics.Debug.WriteLine(userInput);
            return userInput.Equals(GeneratedCaptchaText, StringComparison.OrdinalIgnoreCase);
        }


        private Response AuthenticateUser(string username, string password)
        {
            var client = new Service1Client();
            string response = client.Login(username, password, "Staff");
            var responseObj = JsonConvert.DeserializeObject<Response>(response);
            if (responseObj != null && responseObj.Status)
            {
                // Create a new cookie
                HttpCookie userSessionCookie = new HttpCookie("UserSession");
                userSessionCookie["UserName"] = responseObj.CurrentSession.UserName;
                userSessionCookie["SessionId"] = responseObj.CurrentSession.SessionId;
                userSessionCookie["UserType"] = responseObj.CurrentSession.UserType;

                // Set the expiration date for the cookie
                userSessionCookie.Expires = DateTime.Now.AddHours(48);

                // Add the cookie to the response
                HttpContext.Current.Response.Cookies.Add(userSessionCookie);
                ViewState["UserName"] = responseObj.CurrentSession.UserName;
                ViewState["SessionId"] = responseObj.CurrentSession.SessionId;
                ViewState["UserType"] = responseObj.CurrentSession.UserType;

                // Pass the session data to the next page using QueryString or Session
                Session["currentSession"] = responseObj.CurrentSession;
            }
            return responseObj;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string userCaptchaInput = txtCaptchaInput.Text.Trim();

            if (ValidateCaptchaInput(userCaptchaInput))
            {
                Response response = AuthenticateUser(username, password);
                if (response != null && response.Status)
                {
                    lblResult.Text = "Login successful! Welcome " + username;
                    lblResult.CssClass = "loggedIn";
                    Response.Redirect("staff.aspx");
                }
                else
                {
                    lblResult.Text = response.Message;
                    if (lblResult.Text == "No User Found")
                    {
                        lblResult.Text = "User not found, Consider signing up for free.";
                    }
                    lblResult.CssClass = "error";
                }
            }
            else
            {
                lblResult.Text = "Invalid CAPTCHA. Please try again.";
                lblResult.CssClass = "error";
            }

            // Refresh the CAPTCHA regardless of success or failure
            DisplayCaptcha();
            txtCaptchaInput.Text = "";
        }
    }
}