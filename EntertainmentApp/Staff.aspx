<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="EntertainmentApp.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Staff Portal</h1>
            <!-- Button to list member users -->
            <asp:Button ID="btnListMembers" runat="server" Text="List Member Users" OnClick="btnListMembers_Click" />
            <br /><br />
            <!-- Button to list staff users -->
            <asp:Button ID="btnListStaff" runat="server" Text="List Staff Users" OnClick="btnListStaff_Click" />
            <br /><br />
            <!-- Button to add a staff user -->
            <asp:Button ID="btnAddStaff" runat="server" Text="Add Staff User" OnClick="btnAddStaff_Click" />
        </div>
    </form>
</body>
</html>
