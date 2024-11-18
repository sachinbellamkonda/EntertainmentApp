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
                if (ViewState["UserType"] != null)
                {
                    LoadUsers(ViewState["UserType"].ToString());
                }
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
    }
}