<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="EntertainmentApp.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Portal</title>
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        table, th, td {
            border: 1px solid black;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Staff Portal</h1>
            <p>Total Application Visits: <asp:Label ID="lblVisitCounter" runat="server" /></p>
            <!-- Buttons -->
            <asp:Button ID="btnListMembers" runat="server" Text="List Member Users" OnClick="btnListMembers_Click" />
            <asp:Button ID="btnListStaff" runat="server" Text="List Staff Users" OnClick="btnListStaff_Click" />
            <asp:Button ID="btnAddStaff" runat="server" Text="Add Staff User" OnClick="btnAddStaff_Click" />
            <br /><br />

            <!-- Table for listing users -->
            <asp:Panel ID="panelUserTable" runat="server" Visible="false">
                <table>
                    <thead>
                        <tr>
                            <th>Username</th>
                            <th>User Type</th>
                            <th>Last Login</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="userTableBody" runat="server"></tbody>
                </table>
            </asp:Panel>

            <asp:Panel ID="panelAddStaff" runat="server" Visible="false">
              <h2>Add Staff User</h2>
              <asp:Label ID="lblAddStaffError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
              <asp:Label ID="lblAddStaffSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
             <div>
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
             </div>
             <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
             </div>
             <div>
                <asp:Button ID="btnSubmitStaff" runat="server" Text="Add Staff" OnClick="btnSubmitStaff_Click" />
             </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
