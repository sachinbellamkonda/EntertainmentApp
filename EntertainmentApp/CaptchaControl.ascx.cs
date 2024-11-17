using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntertainmentApp
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayCaptcha();
            }
        }

        // Property to store/retrieve CAPTCHA text from session
        public string GeneratedCaptchaText
        {
            get
            {
                return Session["GeneratedCaptchaText"] as string;
            }
            set
            {
                Session["GeneratedCaptchaText"] = value;
            }
        }

        // Property to access user input from the main page
        public string UserInput
        {
            get { return txtCaptchaInput.Text.Trim(); }
            set { txtCaptchaInput.Text = value; }
        }

        // Generate a random CAPTCHA text
        private string GenerateRandomCaptchaText()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] captchaText = new char[5];
            lock (random) // Ensure thread safety
            {
                for (int i = 0; i < 5; i++)
                {
                    captchaText[i] = chars[random.Next(chars.Length)];
                }
            }
            string generatedText = new string(captchaText);
            System.Diagnostics.Debug.WriteLine("Random captcha generated --> " + generatedText);
            return generatedText;
        }

        // Generate CAPTCHA image based on the text
        private Bitmap GenerateCaptchaImage(string captchaText, int width, int height)
        {
            Bitmap captchaImage = new Bitmap(width, height);
            Random random = new Random();

            using (Graphics graphics = Graphics.FromImage(captchaImage))
            {
                // Set background color
                graphics.Clear(Color.White);

                // Define fonts and brushes
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    // Draw the CAPTCHA text with some rotation for added security
                    float x = 10;
                    float y = 10;
                    foreach (char c in captchaText)
                    {
                        // Rotate each character randomly between -15 and 15 degrees
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(random.Next(-15, 16));
                        graphics.DrawString(c.ToString(), font, brush, 0, 0);
                        graphics.ResetTransform();
                        x += 30; // Adjust spacing between characters
                    }
                }

                // Add noise (lines and dots)
                AddNoise(graphics, width, height);
            }
            return captchaImage;
        }

        // Add noise to the CAPTCHA image to enhance security
        private void AddNoise(Graphics graphics, int width, int height)
        {
            Random random = new Random();
            // Add random lines
            for (int i = 0; i < 5; i++)
            {
                using (Pen pen = new Pen(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), 1))
                {
                    int x1 = random.Next(width);
                    int y1 = random.Next(height);
                    int x2 = random.Next(width);
                    int y2 = random.Next(height);
                    graphics.DrawLine(pen, x1, y1, x2, y2);
                }
            }

            // Add random dots
            for (int i = 0; i < 100; i++)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))))
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    graphics.FillEllipse(brush, x, y, 2, 2);
                }
            }
        }

        // Display the CAPTCHA image
        public void DisplayCaptcha()
        {
            // Generate CAPTCHA text and store it
            GeneratedCaptchaText = GenerateRandomCaptchaText();

            // Generate CAPTCHA image
            Bitmap captchaImage = GenerateCaptchaImage(GeneratedCaptchaText, 200, 50);

            // Convert the image to a base64 string
            using (MemoryStream stream = new MemoryStream())
            {
                captchaImage.Save(stream, ImageFormat.Png);
                byte[] imageBytes = stream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                imgCaptcha.ImageUrl = "data:image/png;base64," + base64String;
            }

            // Clear previous error message
            lblError.Text = "";
        }

        // Validate the user's CAPTCHA input
        public bool IsValidCaptcha()
        {
            string userInput = UserInput;
            System.Diagnostics.Debug.WriteLine("Stored CAPTCHA: " + GeneratedCaptchaText);
            System.Diagnostics.Debug.WriteLine("User Input: " + userInput);
            if (userInput.Equals(GeneratedCaptchaText, StringComparison.OrdinalIgnoreCase))
            {
                lblError.Text = "";
                return true;
            }
            else
            {
                lblError.Text = "Invalid CAPTCHA. Please try again.";
                return false;
            }
        }
    }
}