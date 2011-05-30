<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="Standings.Web.Error.NotFound" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Not Found</title>
    <link href="../Content/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/modernizr-1.7.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="page">
        <header>
            <div id="title">
                <h1>Standings</h1>
            </div>
            <nav>
                <ul id="menu">
                    <li><a href="<%=Request.ApplicationPath%>">Home</a></li>
                </ul>
            </nav>
        </header>
        <section id="main">
            <h2>Oops, we didn't expect it either!</h2>
            <h3>It appears the page you were looking for doesn't exist. Sorry about that.</h3>
        </section>
        <footer>
        </footer>
    </div>
</body>
</html>