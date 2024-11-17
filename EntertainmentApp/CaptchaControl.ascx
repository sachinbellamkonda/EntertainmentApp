<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="EntertainmentApp.WebUserControl1" %>


<div class="form-group">
    <label for="txtCaptchaInput">
        <i class="bi bi-shield-lock-fill"></i> CAPTCHA
    </label>
    <asp:Image ID="imgCaptcha" runat="server" CssClass="captcha-image" />
    <asp:TextBox ID="txtCaptchaInput" runat="server" placeholder="Enter CAPTCHA" CssClass="form-input" />
    <asp:Label ID="lblError" runat="server" Text="" CssClass="error"></asp:Label>
</div>