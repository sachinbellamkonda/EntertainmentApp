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
                        <a href="memberLogin.aspx" class="nav-link"> Login </a>
                        <a href="memberSignup.aspx" class="nav-link"> SignUp </a>
                        <a href="SearchMovies.aspx" class="nav-link"> Member</a>
                        <a href="StaffLogin.aspx" class="nav-link"> Staff </a>
                        <a href="index.html" class="nav-link"> About </a>
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
                <h4 class="text-center"> Adithya: 33.33%  Sachin: 33.33%  Ishan: 33.33%</h4>
                <table class="table table-bordered">
                    <thead class="table-dark">
                        <tr>
                            <th>Provider Name</th>
                            <th>Page and Component Type</th>
                            <th>Input parameters and Output values of the components</th>
                            <th>Component Description</th>
                            <th>Resources, Methods, and TryIt Links</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Sachin</td>
                            <td>Default Page (ASPX)</td>
                            <td> It will dislay the main page of the web application.</td>
                            <td>The public Default page that describes the functionalities and links</td>
                            <td>GUI design and C# code behind GUI</td>
                        </tr>
                        
                        <tr>
                            <td>Adithya</td>
                            <td>Login Service</td>
                            <td> Takes in username, encrypted password, and member type(member/staff). Returns authentication message in json format</td>
                            <td>Handles user login functionality and checks the username and corresponding encrypted passwords with members.xml records. </td>
                            <td>WCF service with C# code. validates login. For user to try our functionalities user has to signup/login. This is the first step<a href="memberLogin.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Adithya</td>
                            <td>Signup service</td>
                            <td>Takes in username , password and member type and session. Returns the authentication in json format</td>
                            <td> Handles the sign up functionality for new members. Members can enter username, passsword, confirm password and signup by themselves. A staff is the only one you can signup another signup.</td>
                            <td>WCF service with C# code. Signs up member/staff <a href="memberSignup.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Adithya</td>
                            <td>Delete User service</td>
                            <td>takes in username, usertype and current session. returns result status and relevant message</td>
                            <td> A staff can delete  member/staff user.</td>
                            <td>WCF service with c# code. with dynamically updating tables with delete user option <a href="staff.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td>Sachin</td>
                            <td>List Users service</td>
                            <td>Takes the user type and retuns the list of users along with the count</td>
                            <td>WCF service with c# code which is used to read the xml database and fetch the member/staff details.</td>
                            <td>WcF code with c# code and listing users based on user type<a href="staff.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td> Ishan</td>
                            <td> Search Movies service </td>
                            <td> Takes keyword as an input and returns list of relevant movies in specific Movie datamodel </td>
                            <td> This service will user to search movies based on keywords.</td>
                            <td> It utilizes external rapid imdb api to search movie details based keywords. Deserializes the output to extract relevant information return this info.<a href="SearchMovies.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Ishan</td>
                            <td> Get streaming platform Service</td>
                            <td> It takes imdb id as input and returns list of Platform Dataclass.</td>
                            <td> Once user select the enlisted movie and click fetch streaming platform it will show the list of platforms along with the url </td>
                            <td> It will utilize external rapid stream availability api to search available streaming platforms for specific imdb id to extract relevant information return this info.<a href="SearchMovies.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Ishan</td>
                            <td> Get trailer service</td>
                            <td> It takes in movie title and returns a youtube link for that movie trailer</td>
                            <td> This service will help user to watch trailer of the selected movie once they click on "fetch trailer" button</td>
                            <td> It will consume youtube data api in order to search movie trailer url. Returns the link to the trailer <a href="SearchMovies.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Sachin</td>
                            <td> Get movie details service</td>
                            <td> It takes in imdb id in string format and returns Detailed Movie data class. </td>
                            <td> User can select enlisted movies and scroll to the bottom section and view the detailed information of the movie along with its poster</td>
                            <td> It utilizes rapid imdb title api to get the movie details <a href="SearchMovies.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Ishan</td>
                            <td> Global asax</td>
                            <td> it will update the global count of visits</td>
                            <td> This will updated in application_BeginRequest </td>
                            <td> You see the number of vists in staff page<a href="staff.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Ishan</td>
                            <td> DLL class library/ Local components</td>
                            <td> It will take the password string as input and provide an encrypted password string</td>
                            <td> This is the local dll library which contains the functionality to encrypt the password before sending it to backend server</td>
                            <td> <a href="Try.aspx">TryIt Link</a> </td>
                        </tr>
                        <tr>
                            <td> Adithya</td>
                            <td> session / local component</td>
                            <td> stores the session details in a cookie</td>
                            <td> We will store the session details to check whether the current session of the user is expired or still active from the backend</td>
                            <td> <a href="memberLogin.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td> Sachin and Adithya</td>
                            <td> Cookie /Local component</td>
                            <td> stores the profile in a cookie</td>
                            <td> We are the user session details sent from the backend server upom login/signup.</td>
                            <td> you can login staff/member user and then go to the default page and try to access login pages. It will forward you to the landing pages after login.</td>
                        </tr>
                        <tr>
                            <td> Adithya</td>
                            <td>Dynamic Table generation local component</td>
                            <td> Outputs the table whenever required by staff to list and delete the users</td>
                            <td> This table is generated dynamically based on the number users presented in the xml. </td>
                            <td> Which conmtain dynamically generated action buttons<a href="staff.aspx">TryIt Link</a></td>
                        </tr>
                        <tr>
                            <td> Sachin</td>
                            <td> Captcha/ Local component</td>
                            <td> Outputs an image of captcha</td>
                            <td> This local component will help pages to generate captcha with random string image and adding noise</td>
                            <td> try it in any login/sign up page</td>
                        </tr>
                        <tr>
                            <td>Sachin</td>
                            <td>memberLogin.aspx, memberSignup.aspx</td>
                            <td> Login and signup pages gui</td>
                            <td>Displays the gui for login /signup of users along with captcha</td>
                            <td>ASP.NET CAPTCHA control, C# code.</td>
                        </tr>
                        <tr>
                            <td>Adithya</td>
                            <td>staffLogin.aspx , staff.aspx</td>
                            <td> staff login and staff user landing page</td>
                            <td>displays the gui for staff</td>
                            <td>ASP.NET, C# code. </td>
                        </tr>
                        <tr>
                            <td>Ishan</td>
                            <td>SearchMovies.aspx, Try.aspx, CSE library</td>
                            <td>landing page for users and try it page for local components</td>
                            <td>gui for users landing page, and tryit for other local components </td>
                            <td>ASP.NET, C# code.</td>
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