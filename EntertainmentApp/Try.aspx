<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Try.aspx.cs" Inherits="EntertainmentApp.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Try It Out!!</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        table td {
            padding: 10px;
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .btn {
            background-color: #007BFF;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .output {
            margin-top: 20px;
            color: #333;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Try It Out!!</h1>
            <table>
                <tr>
                    <td>Enter Text:</td>
                    <td>
                        <asp:TextBox ID="txtInput" runat="server" Width="100%" Placeholder="Enter text to encrypt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnEncrypt" runat="server" CssClass="btn" Text="Encrypt" OnClick="btnEncrypt_Click" />
                    </td>
                </tr>
                <tr>                    
                    <td>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Encrypted Text:</td>
                    <td>
                        <asp:Label ID="lblOutput" runat="server" CssClass="output"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
