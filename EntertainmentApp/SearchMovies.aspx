<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SearchMovies.aspx.cs" Inherits="EntertainmentApp.SearchMovies" %>

<!DOCTYPE html>
<html>
<head>
    <title>Entertainment Hub</title>
    <style>
        /* Overall page styling */
        body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), 
                        url('https://source.unsplash.com/1920x1080/?movies,cinema') no-repeat center center fixed;
            background-size: cover;
            color: white;
        }

        .container {
            max-width: 1100px;
            margin: 40px auto;
            padding: 20px;
            background: rgba(255, 255, 255, 0.9);
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
            color: #333;
        }

        h2, h3, h4 {
            text-align: center;
            margin-bottom: 20px;
        }

        .search-section, .results-section, .details-section {
            margin-bottom: 30px;
        }

        .btn {
            background-color: #ff5722;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 10px;
        }

        .btn:hover {
            background-color: #e64a19;
        }

        /* GridView styling */
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .grid-view th, .grid-view td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .grid-view th {
            background-color: #007BFF;
            color: white;
        }

        .grid-view img {
            max-width: 80px;
            height: auto;
            border-radius: 4px;
        }

        /* Movie details styling */
        .movie-details {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            align-items: flex-start;
            margin-top: 20px;
        }

        .movie-details img {
            width: 250px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.5);
        }

        .movie-info {
            flex-grow: 1;
        }

        .movie-info h2 {
            margin-bottom: 15px;
            font-size: 1.8em;
        }

        .movie-info p {
            margin: 5px 0;
            line-height: 1.6;
        }

        /* Platform section */
        .platforms-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin-top: 20px;
            justify-content: center;
        }

        .platform-item {
            text-align: center;
            width: 120px;
        }

        .platform-item img {
            width: 80px;
            height: 80px;
            object-fit: contain;
            margin-bottom: 5px;
            border-radius: 50%;
            border: 2px solid #ddd;
        }

        .platform-item p {
            font-size: 0.9em;
            color: #555;
        }

        /* Trailer section */
        .details-section iframe {
            display: none; /* Initially hidden */
            width: 100%;
            max-width: 700px;
            height: 400px;
            margin: 20px auto;
            border: none;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.5);
        }

        .details-section iframe.show {
            display: block; /* Shown dynamically when trailer is available */
        }

        /* Responsiveness */
        @media (max-width: 768px) {
            .movie-details {
                flex-direction: column;
                align-items: center;
            }

            .movie-details img {
                width: 200px;
            }

            .movie-info {
                text-align: center;
            }

            .platform-item {
                width: 100px;
            }

            .platform-item img {
                width: 60px;
                height: 60px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Search Section -->
            <div class="search-section">
                <h2>Search for Movies</h2>
                <asp:TextBox ID="txtSearch" runat="server" Width="300" Placeholder="Enter movie name"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_Click" />
            </div>

            <!-- Results Section -->
            <div class="results-section">
                <h3>Search Results</h3>
                <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnSelectedIndexChanged="gvMovies_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ImdbId" HeaderText="IMDb ID" Visible="False" />
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Year" HeaderText="Year" />
                        <asp:ImageField DataImageUrlField="Image" HeaderText="Poster" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Details Section -->
            <div class="details-section">
                <h3>Movie Details</h3>
                <div id="movieDetailsContainer" class="movie-details">
                    <asp:Literal ID="litDetails" runat="server"></asp:Literal>
                </div>

                <asp:Button ID="btnFetchPlatforms" runat="server" CssClass="btn" Text="Fetch Streaming Platforms" OnClick="btnFetchPlatforms_Click" />
                <asp:Button ID="btnFetchTrailer" runat="server" CssClass="btn" Text="Fetch Trailer" OnClick="btnFetchTrailer_Click" />

                <h4>Streaming Platforms</h4>
                <div class="platforms-container">
                    <asp:Repeater ID="rptPlatforms" runat="server">
                        <ItemTemplate>
                            <div class="platform-item">
                                <a href='<%# Eval("Url") %>' target="_blank">
                                    <img src='<%# Eval("Logo") %>' alt='<%# Eval("Name") %>' />
                                </a>
                                <p><%# Eval("Name") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <h4>Trailer</h4>
                <iframe id="iframeTrailer" runat="server" allowfullscreen></iframe>
            </div>
        </div>
    </form>
</body>
</html>
