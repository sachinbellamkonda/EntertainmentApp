<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="EntertainmentApp.StaffLogin" %>
<%@ Register TagPrefix="uc" TagName="Captcha" Src="~/CaptchaControl.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Everything Entertainment - Staff Login</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
    <link rel="stylesheet" type="text/css" href="StaffLogin.css" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Main Section -->
        <section id="main-section">
            <div class="container text-center">
                <!-- App Name -->
                <h1 class="app-title">Everything Entertainment</h1>

                <!-- Login Box -->
                <div class="login-box">
                    <h2 class="login-heading">Staff Sign In</h2>

                    <!-- Login Form -->
                    <div class="form-group">
                        <label for="txtUsername">
                            <i class="bi bi-person-fill"></i> Your Username
                        </label>
                        <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter your username" CssClass="form-input" />
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">
                            <i class="bi bi-lock-fill"></i> Your Password
                        </label>
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter your password" TextMode="Password" CssClass="form-input" />
                    </div>
                    <!-- CAPTCHA User Control -->
                    <uc:Captcha ID="CaptchaControl" runat="server" />
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click" CssClass="btn-submit" />
                    <br />
                    <asp:Label ID="lblResult" runat="server" Text="" CssClass="success"></asp:Label>
                </div>
            </div>
        </section>
    </form>

    <!-- Scripts -->
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>