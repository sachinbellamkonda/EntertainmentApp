<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sessionInvalid.aspx.cs" Inherits="EntertainmentApp.sessionExpired" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Invalid</title>
    <script>
        window.onload = function () {
            if (typeof source !== "undefined") {
                // Redirect based on the source
                if (source === "member") {
                    alert("Your session is not valid. Redirecting to Member Login...");
                    setTimeout(function () {
                        window.location.href = "memberLogin.aspx"; // Redirect to Member Login
                    }, 5);
                } else if (source === "staff") {
                    alert("Your session is not valid. Redirecting to Staff Login...");
                    setTimeout(function () {
                        window.location.href = "StaffLogin.aspx"; // Redirect to Staff Login
                    }, 5);
                } else {
                    alert("Session is not valid. Redirecting to default page...");
                    setTimeout(function () {
                        window.location.href = "Default.aspx"; // Default login page
                    }, 5);
                }
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Session is not Valid</h1>
            <p>You will be redirected to login page now.</p>
        </div>
    </form>
</body>
</html>
