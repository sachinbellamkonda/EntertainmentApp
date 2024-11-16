using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntertainmentApp.ServiceReference1; // Ensure this matches your service reference namespace

namespace EntertainmentApp
{
    // Movie Class
    public class Movie
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Image { get; set; }
    }

    // Platform Class
    public class Platform
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string HomePage { get; set; }
        public string Type { get; set; }
        public string Quality { get; set; }
        public string ThemeColor { get; set; }
    }

    public partial class SearchMovies : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Any page initialization can go here
        }

        // Search Button Click Event Handler
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new Service1Client())
                {
                    // Call the SearchMovies WCF method
                    var movies = await client.SearchMoviesAsync(txtSearch.Text);

                    // Bind movies to the GridView
                    gvMovies.DataSource = movies.Select(m => new
                    {
                        ImdbId = m.ImdbId,
                        Title = m.Title,
                        Year = m.Year,
                        Image = m.Image
                    });
                    gvMovies.DataKeyNames = new string[] { "ImdbId" }; // Set DataKeyNames for the GridView
                    gvMovies.DataBind();
                }
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // GridView SelectedIndexChanged Event Handler
        protected void gvMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the IMDb ID using DataKeys
                string imdbId = gvMovies.DataKeys[gvMovies.SelectedIndex].Value.ToString();
                ViewState["SelectedImdbId"] = imdbId;

                // Display basic movie details
                litDetails.Text = $"<strong>IMDb ID:</strong> {imdbId}<br /><strong>Title:</strong> {gvMovies.SelectedRow.Cells[1].Text}";
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // Button to Fetch Streaming Platforms
        protected async void btnFetchPlatforms_Click(object sender, EventArgs e)
        {
            try
            {
                string imdbId = ViewState["SelectedImdbId"]?.ToString();
                if (string.IsNullOrEmpty(imdbId))
                {
                    litDetails.Text = "No movie selected.";
                    return;
                }

                using (var client = new Service1Client())
                {
                    var platforms = await client.GetStreamingPlatformsAsync(imdbId);

                    // Bind platforms to Repeater
                    rptPlatforms.DataSource = platforms;
                    rptPlatforms.DataBind();
                }
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // Button to Fetch Trailer
        protected async void btnFetchTrailer_Click(object sender, EventArgs e)
        {
            try
            {
                string imdbId = ViewState["SelectedImdbId"]?.ToString();
                if (string.IsNullOrEmpty(imdbId))
                {
                    litDetails.Text = "No movie selected.";
                    return;
                }

                using (var client = new Service1Client())
                {
                    // Fetch the trailer URL asynchronously
                    string trailerUrl = await client.GetTrailerAsync(imdbId);

                    // Update the iframe source with the trailer URL
                    iframeTrailer.Attributes["src"] = trailerUrl;
                }
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }
    }
}
