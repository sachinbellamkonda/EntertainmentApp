using CseLibrary;
using EntertainmentApp.Models;
using EntertainmentApp.ServiceReference1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EntertainmentApp
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ValidateSession();

                if (ViewState["UserType"] != null)
                {
                    LoadUsers(ViewState["UserType"].ToString());
                }
            }
            else
            {
                try
                {
                    // Check if session cookie exists
                    HttpCookie userCookie = Request.Cookies["UserSession"];
                    if (userCookie != null)
                    {
                        // Deserialize the JSON stored in the cookie
                        var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);

                        if (sessionData != null && !string.IsNullOrEmpty(sessionData.SessionId) && sessionData.UserType.Equals("Staff"))
                        {
                            using (var client = new Service1Client())
                            {
                                // Validate the session using the service
                                var currentSession = new Session
                                {
                                    SessionId = sessionData.SessionId,
                                    UserName = sessionData.UserName,
                                    UserType = sessionData.UserType
                                };

                                string validationResponse = client.ValidateSession(currentSession);
                                var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(validationResponse);

                                if (responseDictionary.ContainsKey("status") && !(bool)responseDictionary["status"])
                                {
                                    // Session invalid, redirect to login page
                                    Response.Redirect("~/sessionInvalid.aspx?source=staff");
                                }
                                else
                                {
                                    lblVisitCounter.Text = Application["VisitCounter"]?.ToString() ?? "0";
                                }
                            }
                        }
                        else
                        {
                            // Invalid session cookie data, redirect to login
                            Response.Redirect("~/sessionInvalid.aspx?source=staff");
                        }
                    }
                    else
                    {
                        // No session cookie, redirect to login page
                        Response.Redirect("~/sessionInvalid.aspx?source=staff");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions and redirect to login page
                    Console.WriteLine($"Error validating session: {ex.Message}");
                    Response.Redirect("~/sessionInvalid.aspx?source=staff");
                }

                
            }
        }

        private void ValidateSession()
        {
            try
            {
                // Check if session cookie exists
                HttpCookie userCookie = Request.Cookies["UserSession"];
                if (userCookie != null)
                {
                    // Deserialize the JSON stored in the cookie
                    var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);

                    if (sessionData != null && !string.IsNullOrEmpty(sessionData.SessionId) && sessionData.UserType.Equals("Staff"))
                    {
                        using (var client = new Service1Client())
                        {
                            // Validate the session using the service
                            var currentSession = new Session
                            {
                                SessionId = sessionData.SessionId,
                                UserName = sessionData.UserName,
                                UserType = sessionData.UserType
                            };

                            string validationResponse = client.ValidateSession(currentSession);
                            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(validationResponse);

                            if (responseDictionary.ContainsKey("status") && !(bool)responseDictionary["status"])
                            {
                                // Session invalid, redirect to login page
                                Response.Redirect("~/sessionInvalid.aspx?source=staff");
                            }
                        }
                    }
                    else
                    {
                        // Invalid session cookie data, redirect to login
                        Response.Redirect("~/sessionInvalid.aspx?source=staff");
                    }
                }
                else
                {
                    // No session cookie, redirect to login page
                    Response.Redirect("~/sessionInvalid.aspx?source=staff");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and redirect to login page
                Console.WriteLine($"Error validating session: {ex.Message}");
                Response.Redirect("~/sessionInvalid.aspx?source=staff");
            }
        }
        protected void btnListMembers_Click(object sender, EventArgs e)
        {
            ViewState["UserType"] = "Member";
            LoadUsers("Member");
        }
        protected void btnListStaff_Click(object sender, EventArgs e){
            ViewState["UserType"] = "Staff";
            LoadUsers("Staff");
        }
        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            panelAddStaff.Visible = true;
            // Add staff functionality to be implemented later
        }

        private void LoadUsers(string userType)
        {
            try
            {
                // Call the ListUsers service
                var client = new Service1Client();
                string responseJson = client.ListUsers(userType);
                var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseJson);

                // Check if service response is successful
                if (responseData.status == true)
                {
                    panelUserTable.Visible = true;
                    userTableBody.Controls.Clear();

                    foreach (var user in responseData.userList)
                    {
                        // Create a new row
                        HtmlTableRow row = new HtmlTableRow();

                        // Username column
                        HtmlTableCell cellUsername = new HtmlTableCell();
                        cellUsername.InnerText = user.UserName.ToString();
                        row.Cells.Add(cellUsername);

                        // UserType column
                        HtmlTableCell cellUserType = new HtmlTableCell();
                        cellUserType.InnerText = user.UserType.ToString();
                        row.Cells.Add(cellUserType);

                        // LastLogin column
                        HtmlTableCell cellLastLogin = new HtmlTableCell();
                        cellLastLogin.InnerText = user.LastLogin.ToString();
                        row.Cells.Add(cellLastLogin);

                        // Delete button column
                        HtmlTableCell cellAction = new HtmlTableCell();
                        Button btnDelete = new Button
                        {
                            Text = "Delete",
                            CommandArgument = $"{user.UserName},{user.UserType}",
                            OnClientClick = "return confirm('Are you sure you want to delete this user?');"
                        };
                        btnDelete.Click += DeleteUser_Click;
                        cellAction.Controls.Add(btnDelete);
                        row.Cells.Add(cellAction);

                        // Add the row to the table body
                        userTableBody.Controls.Add(row);
                    }
                }
                else
                {
                    // Handle error from service
                    panelUserTable.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Log or display exception details
                panelUserTable.Visible = false;
            }
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Step1 ");
                Button btnDelete = (Button)sender;
                string[] args = btnDelete.CommandArgument.Split(',');
                string username = args[0];
                string userType = args[1];

                System.Diagnostics.Debug.WriteLine(args[0], args[1]);

                // Call the DeleteUser service
                var client = new Service1Client();
                //string currentSession = "current-session-placeholder"; // Replace with actual session

                HttpCookie userCookie = Request.Cookies["UserSession"];
                var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);
                var serviceSession = new ServiceReference1.Session
                {
                    UserName = sessionData.UserName,
                    SessionId = sessionData.SessionId,
                    UserType = sessionData.UserType
                };

                string responseJson = client.DeleteUser(username, userType, serviceSession);
                var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseJson);

                // Check if delete was successful
                if (responseData.status == true)
                {
                    // Reload the user list
                    LoadUsers(userType);
                }
                else
                {
                    // Handle delete failure
                }
            }
            catch (Exception ex)
            {
                // Log or display exception details
            }
        }

        protected void btnSubmitStaff_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                string encryptedPassword = PasswordEncryptor.EncryptPassword(password);

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    lblAddStaffError.Text = "Username and password cannot be empty.";
                    lblAddStaffError.Visible = true;
                    return;
                }

                HttpCookie userCookie = Request.Cookies["UserSession"];
                var sessionData = JsonConvert.DeserializeObject<UserSession>(userCookie.Value);
                var serviceSession = new ServiceReference1.Session
                {
                    UserName = sessionData.UserName,
                    SessionId = sessionData.SessionId,
                    UserType = sessionData.UserType
                };

                // Call the SignUp service
                var client = new Service1Client();
                string responseJson = client.SignUp(username, encryptedPassword, "Staff", serviceSession);
                var responseData = JsonConvert.DeserializeObject<dynamic>(responseJson);

                if (responseData.status == true)
                {
                    lblAddStaffSuccess.Text = "Staff user added successfully!";
                    lblAddStaffSuccess.Visible = true;

                    // Reload staff users
                    LoadUsers("Staff");

                    // Hide the form
                    panelAddStaff.Visible = false;
                }
                else
                {
                    lblAddStaffError.Text = responseData.message ?? "Failed to add staff user.";
                    lblAddStaffError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblAddStaffError.Text = "An error occurred: " + ex.Message;
                lblAddStaffError.Visible = true;
            }
        }
    }
}