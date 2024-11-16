<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SearchMovies.aspx.cs" Inherits="EntertainmentApp.SearchMovies" %>

<!DOCTYPE html>
<html>
<head>
    <title>Search Movies</title>
    <style>
        .container { margin: 20px; font-family: Arial, sans-serif; }
        .search-section { margin-bottom: 20px; }
        .results-section, .details-section { margin-top: 20px; }
        .trailer { margin-top: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Search Section -->
            <div class="search-section">
                <h2>Search for Movies</h2>
                <asp:TextBox ID="txtSearch" runat="server" Width="300" Placeholder="Enter movie name"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>

            <!-- Results Section -->
            <div class="results-section">
                <h3>Search Results</h3>
                <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvMovies_SelectedIndexChanged">
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
                <asp:Literal ID="litDetails" runat="server"></asp:Literal>

                <asp:Button ID="btnFetchPlatforms" runat="server" Text="Fetch Streaming Platforms" OnClick="btnFetchPlatforms_Click" />
                <asp:Button ID="btnFetchTrailer" runat="server" Text="Fetch Trailer" OnClick="btnFetchTrailer_Click" />

                <h4>Streaming Platforms</h4>
                <asp:Repeater ID="rptPlatforms" runat="server">
                    <ItemTemplate>
                        <div>
                            <strong><%# Eval("Name") %></strong> - <a href='<%# Eval("Url") %>' target="_blank">Watch Now</a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <h4>Trailer</h4>
                <iframe id="iframeTrailer" runat="server" width="560" height="315" frameborder="0" allowfullscreen></iframe>
            </div>
        </div>
    </form>
</body>
</html>
