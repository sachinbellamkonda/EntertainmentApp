using CseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntertainmentApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the input from the user
                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    lblMessage.Text = "Please enter a string to encrypt.";
                    lblOutput.Text = string.Empty;
                    return;
                }

                // Encrypt the input string
                string encryptedString = PasswordEncryptor.EncryptPassword(input);

                // Display the encrypted string
                lblMessage.Text = string.Empty;
                lblOutput.Text = encryptedString;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }


        }
    }
}