﻿using EntertainmentApp.ServiceReference1;
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

                        if (sessionData != null && !string.IsNullOrEmpty(sessionData.SessionId) && sessionData.UserType.Equals("Member"))
                        {
                            // Create a session object to send to the service
                            var currentSession = new Session
                            {
                                SessionId = sessionData.SessionId,
                                UserName = sessionData.UserName,
                                UserType = sessionData.UserType
                            };

                            // Call the ValidateSession service
                            var client = new Service1Client();
                            string validationResponse = client.ValidateSession(currentSession);

                            // Parse the JSON response
                            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(validationResponse);

                            if (responseDictionary.ContainsKey("status") && (bool)responseDictionary["status"])
                            {
                                // Session is valid, redirect to SearchMovies page
                                Response.Redirect("searchmovies.aspx");
                            }
                            else
                            {
                                // Session invalid or expired, show CAPTCHA
                                CaptchaControl.DisplayCaptcha();
                            }
                        }
                        else
                        {
                            // Invalid session data, show CAPTCHA
                            CaptchaControl.DisplayCaptcha();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions (e.g., cookie tampering or deserialization issues)
                        Console.WriteLine("Error reading user session: " + ex.Message);
                        CaptchaControl.DisplayCaptcha(); // Fallback to CAPTCHA
                    }
                }
                else
                {
                    // Generate CAPTCHA if no existing session
                    CaptchaControl.DisplayCaptcha();
                }
            }
            // if (!IsPostBack)
            // {
            //     // Check if user session exists
            //     HttpCookie userCookie = Request.Cookies["UserSession"];
            //     if (userCookie != null)
            //     {
            //         try
            //         {
            //             // Deserialize the JSON stored in the cookie
            //             var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);

            //             if (sessionData != null && !string.IsNullOrEmpty(sessionData.SessionId) && sessionData.UserType.Equals("Member"))
            //             {
            //                 // Session exists, redirect to SearchMovies page
            //                 Response.Redirect("searchmovies.aspx");
            //             }
            //         }
            //         catch (Exception ex)
            //         {
            //             // Handle any exceptions (e.g., cookie tampering or deserialization issues)
            //             Console.WriteLine("Error reading user session: " + ex.Message);
            //         }
            //     }
            //     else
            //     {
            //         // Generate CAPTCHA if no existing session
            //         CaptchaControl.DisplayCaptcha();
            //     }
            // }
           
        }

        private Response AuthenticateUser(string username, string password)
        {
            var client = new Service1Client();
            string response = client.Login(username, password, "Member");
            var responseObj = JsonConvert.DeserializeObject<Response>(response);
            if (responseObj != null && responseObj.Status)
            {
                // Create a new cookie
                HttpCookie userSessionCookie = new HttpCookie("UserSession");

                // Serialize the entire UserSession object to JSON
                string jsonSession = JsonConvert.SerializeObject(responseObj.CurrentSession);
                userSessionCookie.Value = jsonSession;
                userSessionCookie.Domain = "webstrar51.fulton.asu.edu";
                userSessionCookie.Path = "/";

                // Set the expiration date for the cookie
                userSessionCookie.Expires = DateTime.Now.AddHours(1);

                // Add the cookie to the response
                HttpContext.Current.Response.Cookies.Add(userSessionCookie);

                
                Session["currentSession"] = responseObj.CurrentSession;
            }
            return responseObj; 
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
                Response response = AuthenticateUser(username, encryptedPassword);
                if (response != null && response.Status)
                {
                    lblResult.Text = "Login successful! Welcome " + username;
                    lblResult.CssClass = "success";
                    Response.Redirect("searchmovies.aspx");
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
            CaptchaControl.DisplayCaptcha();
        }
    
    }
}