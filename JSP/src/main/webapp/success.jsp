<%@ page import="core.domain.User" %><%--
  Created by IntelliJ IDEA.
  User: Cristi
  Date: 5/9/2020
  Time: 8:32 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Success</title>
    <script src="jquery-3.5.0.min.js"></script>
    <script src="script.js"></script>
    <link rel="stylesheet" href="style.css" type="text/css">
</head>
<body>
<%
    User user = (User) session.getAttribute("user");
    if(user == null){
        return;
    }
    out.println("Welcome "+user.getName());
%>



    <br>
    <br>
    <div class="container">
        <a href="login.html"><b>Logout</b></a><br/>
        <hr>
        Email : <input type="text" id="email"> <BR>
        Name : <input type="text" id="name"> <BR>
        Password : <input type="password" id="passwordd"> <BR>
        Address : <input type="text" id="address"> <BR>
        Picture : <input type="text" id="picture"> <BR>
        Hometown : <input type="text" id="hometown"> <BR>
        Age : <input type="text" id="age"> <BR>
        <button type="button" id="changeProfile" value="Change Profile">Change Profile</button><br/>
        <hr>
        Filter : <input type="text" id="filter">
        <button type="button" id="get">Get Users</button>
        <hr>
        <p id="main"></p>
        <ul id="user_list"></ul>
    </div>
</body>
</html>
