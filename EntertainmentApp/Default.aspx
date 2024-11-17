<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EntertainmentApp._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entertainment App - Default Page</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
    <link rel="stylesheet" type="text/css" href="Default.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-md fixed-top" id="Navbar">
            <!-- Container for Navbar -->
            <div class="container px-5">
                <!-- Home Icon -->
                <a href="Default.aspx" class="navbar-brand"><i class="bi bi-house text-white" id="home-icon"></i></a>
                <!-- Button after the Navbar Collapse -->
                <button class="navbar-toggler ml-auto" type="button" data-bs-toggle="collapse" data-bs-target="#navbarDropdown" aria-controls="navbarDropdown" aria-expanded="false" aria-label="ToggleNavigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <!-- Navbar Links -->
                <div class="collapse navbar-collapse" id="navbarDropdown">
                    <div class="navbar-nav text-end ms-auto">
                        <a href="Default.aspx" class="nav-link"> Login/Signup </a>
                        <a href="Member.aspx" class="nav-link"> Member </a>
                        <a href="StaffPage.aspx" class="nav-link"> Staff </a>
                        <a href="contact.html" class="nav-link"> Contact </a>
                    </div>
                </div>
                <!-- Navbar Links Ends -->
            </div>
            <!-- Container for Navbar Ends -->
        </nav>
        <!-- Navbar Ends -->

        <!-- Main Section -->
        <section id="main-section">
            <div class="container text-black transbox mx-auto p-4">
                <p class="text-center display-4"> Everything Entertainment </p>
            </div>
            </div>
            
            <!-- Application and Components Summary Table -->
            <div class="container mx-auto p-4">
                <h3 class="text-center">Application and Components Summary Table</h3>
                <table class="table table-bordered">
                    <thead class="table-dark">
                        <tr>
                            <th>Provider Name</th>
                            <th>Page and Component Type</th>
                            <th>Component Description</th>
                            <th>Resources, Methods, and TryIt Links</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Sachin</td>
                            <td>Default Page (ASPX)</td>
                            <td>The public Default page that links all other pages</td>
                            <td>GUI design and C# code behind GUI</td>
                        </tr>
                        <tr>
                            <td>Ishan</td>
                            <td>Service - Login (RESTful service)</td>
                            <td>Handles user login functionality</td>
                            <td>ASP.NET Identity, C# code. <a href="LoginTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Ishan</td>
                            <td>Service - Signup (RESTful service)</td>
                            <td>Handles user signup functionality</td>
                            <td>ASP.NET Identity, C# code. <a href="SignupTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Ishan</td>
                            <td>Search Bar (User Control)</td>
                            <td>Implements search functionality for finding movies and shows</td>
                            <td>TMDB API, C# code. <a href="SearchTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Sachin</td>
                            <td>Cookie (DLL function)</td>
                            <td>Stores user profile information</td>
                            <td>HTTP cookies, C# code.</td>
                        </tr>
                        <tr>
                            <td>Sachin</td>
                            <td>Captcha (User Control)</td>
                            <td>Displays and validates CAPTCHA for user login/signup</td>
                            <td>ASP.NET CAPTCHA control, C# code. <a href="CaptchaTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Adithya</td>
                            <td>Login (User Control)</td>
                            <td>Implements user login window as a reusable control</td>
                            <td>ASP.NET, C# code. <a href="UserControlLoginTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Adithya</td>
                            <td>New User Control</td>
                            <td>Specific control for the Entertainment App, details to be added</td>
                            <td>ASP.NET, C# code. <a href="NewUserControlTryIt.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Ishan</td>
                            <td>Encryption/Decryption (DLL)</td>
                            <td>Implements encryption and decryption for sensitive data</td>
                            <td>C# DLL library function.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- Application and Components Summary Table Ends -->
        </section>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
</body>
</html>