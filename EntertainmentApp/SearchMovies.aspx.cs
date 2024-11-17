using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntertainmentApp.ServiceReference1;

namespace EntertainmentApp
{
    public class ImdbResponse
    {
        public List<ImdbResult> Results { get; set; }
    }
    public class ImdbResult
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
    }
    public class Movie
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Image { get; set; }
    }

    public class Platform
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string HomePage { get; set; }
        public string Type { get; set; }
        public string Quality { get; set; }
        public string ThemeColor { get; set; }
    }
    [DataContract]
    public class DetailedMovie
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Genre { get; set; }

        [DataMember]
        public string Director { get; set; }

        [DataMember]
        public string Cast { get; set; } // Added cast

        [DataMember]
        public string Runtime { get; set; } // Added runtime

        [DataMember]
        public string Plot { get; set; }

        [DataMember]
        public string Poster { get; set; }

        [DataMember]
        public string ReleaseYear { get; set; } // Added release year

        [DataMember]
        public string IMDbRating { get; set; } // Added IMDb rating
    }

    public partial class SearchMovies : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Search Button Click Event
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear existing details, platforms, and trailer
                litDetails.Text = string.Empty;
                rptPlatforms.DataSource = null;
                rptPlatforms.DataBind();
                iframeTrailer.Attributes["src"] = string.Empty;

                using (var client = new Service1Client())
                {
                    // Search for movies
                    var movies = await client.SearchMoviesAsync(txtSearch.Text);
                    gvMovies.DataSource = movies.Select(m => new
                    {
                        ImdbId = m.ImdbId,
                        Title = m.Title,
                        Year = m.Year,
                        Image = m.Image
                    });
                    gvMovies.DataKeyNames = new string[] { "ImdbId" };
                    gvMovies.DataBind();
                }
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // GridView Selection Changed
        protected async void gvMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Retrieve selected IMDb ID
                string imdbId = gvMovies.DataKeys[gvMovies.SelectedIndex].Value.ToString();

                // Store IMDb ID in ViewState
                ViewState["SelectedImdbId"] = imdbId;
                

                // Clear other sections
                rptPlatforms.DataSource = null;
                rptPlatforms.DataBind();
                iframeTrailer.Attributes["src"] = string.Empty;

                using (var client = new ServiceReference1.Service1Client())
                {
                    // Fetch detailed movie information
                    var serviceMovieDetails = await client.GetMovieDetailsAsync(imdbId);

                    var movieDetails = new DetailedMovie
                    {
                        Title = serviceMovieDetails.Title,
                        Genre = serviceMovieDetails.Genre,
                        Director = serviceMovieDetails.Director,
                        Cast = serviceMovieDetails.Cast,
                        Runtime = serviceMovieDetails.Runtime,
                        Plot = serviceMovieDetails.Plot,
                        Poster = serviceMovieDetails.Poster,
                        ReleaseYear = serviceMovieDetails.ReleaseYear,
                        IMDbRating = serviceMovieDetails.IMDbRating
                    };

                    // Populate the details section
                    litDetails.Text = $@"
                <div style='text-align:center;'>
                    <img src='{movieDetails.Poster}' alt='Poster' style='max-width:200px; border-radius:8px;' />
                </div>
                <h2 style='text-align:center;'>{movieDetails.Title}</h2>
                <p><strong>Genre:</strong> {movieDetails.Genre}</p>
                <p><strong>Director:</strong> {movieDetails.Director}</p>
                <p><strong>Cast:</strong> {movieDetails.Cast}</p>
                <p><strong>Runtime:</strong> {movieDetails.Runtime}</p>
                <p><strong>Plot:</strong> {movieDetails.Plot}</p>
                <p><strong>Release Year:</strong> {movieDetails.ReleaseYear}</p>
                <p><strong>IMDb Rating:</strong> {movieDetails.IMDbRating}</p>";
                }

                // Scroll to the details section
                ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToDetails", "document.getElementById('detailsSection').scrollIntoView({ behavior: 'smooth' });", true);
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // Fetch Streaming Platforms
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

                // Clear the trailer section
                iframeTrailer.Attributes["src"] = string.Empty;

                using (var client = new Service1Client())
                {
                    var platforms = await client.GetStreamingPlatformsAsync(imdbId);

                    // Filter platforms to get only unique platforms with priority to "USA" links
                    var filteredPlatforms = platforms
                        .GroupBy(p => p.Name)
                        .Select(g => g.FirstOrDefault(p => p.Url.Contains("us")) ?? g.First())
                        .ToList();

                    if (filteredPlatforms.Count == 0)
                    {
                        litDetails.Text = "No streaming platforms found.";
                        return;
                    }

                    var platformData = filteredPlatforms.Select(p => new
                    {
                        Name = p.Name,
                        Url = p.Url,
                        Logo = GetPlatformLogo(p.Name) // Map platform name to logo URL
                    });

                    rptPlatforms.DataSource = platformData;
                    rptPlatforms.DataBind();
                }
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error: {ex.Message}";
            }
        }

        // Fetch Trailer
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
                    string trailerUrl = await client.GetTrailerAsync(imdbId);
                    if (!string.IsNullOrEmpty(trailerUrl))
                    {
                        iframeTrailer.Attributes["src"] = trailerUrl;
                        iframeTrailer.Attributes["class"] = "show"; // Ensure iframe is visible
                    }
                    else
                    {
                        iframeTrailer.Attributes["src"] = "";
                        iframeTrailer.Attributes["class"] = ""; // Hide if no trailer is found
                        litDetails.Text = "Trailer not available for this movie.";
                    }
                }

                // Scroll to trailer
                ScriptManager.RegisterStartupScript(this, GetType(), "scrollToTrailer", "document.getElementById('iframeTrailer').scrollIntoView({ behavior: 'smooth' });", true);
            }
            catch (Exception ex)
            {
                litDetails.Text = $"Error fetching trailer: {ex.Message}";
            }
        }

        // Get platform logo URL
        private string GetPlatformLogo(string platformName)
        {
            switch (platformName.ToLower())
            {
                case "netflix":
                    return "https://upload.wikimedia.org/wikipedia/commons/0/08/Netflix_2015_logo.svg";
                case "hulu":
                    return "https://upload.wikimedia.org/wikipedia/commons/2/20/Hulu_2019.svg";
                case "disney+":
                case "disney plus":
                    return "https://upload.wikimedia.org/wikipedia/commons/3/3e/Disney%2B_logo.svg";
                case "prime video":
                    return "https://upload.wikimedia.org/wikipedia/commons/f/f1/Prime_Video.png";
                case "hbo max":
                    return "https://upload.wikimedia.org/wikipedia/commons/1/17/HBO_Max_Logo.svg";
                case "apple tv":
                    return "https://upload.wikimedia.org/wikipedia/en/a/ae/Apple_TV_%28logo%29.svg";
                case "disney+ hotstar":
                    return "https://upload.wikimedia.org/wikipedia/commons/1/1e/Disney%2B_Hotstar_logo.svg";
                case "paramount+":
                    return "https://en.wikipedia.org/wiki/File:Paramount%2B_logo.png";
                case "pluto tv":
                    return "https://upload.wikimedia.org/wikipedia/commons/c/c3/Pluto_TV_logo_2024.svg";
                case "hotstar":
                    return "https://upload.wikimedia.org/wikipedia/en/0/0e/Hotstar_New_Logo.jpeg";
                case "stan":
                    return "https://en.wikipedia.org/wiki/Stan_(streaming_service)#/media/File:Stan_logo.svg";
                case "now":
                    return "https://upload.wikimedia.org/wikipedia/en/2/25/Now_TV_%28Sky_plc%29_logo.svg";
                case "tubi":
                    return "https://upload.wikimedia.org/wikipedia/commons/d/db/Tubi_logo.svg";
                default:
                    return "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";
            }
        }

    }
}
