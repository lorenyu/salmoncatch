<%@ Page Language="C#" AutoEventWireup="true" CodeFile="result.aspx.cs" Inherits="result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="Stylesheet" type="text/css" href="css/simple.css" />

    <title>FotoFusing complete</title>
</head>
<body>
    <div class="header">
        <h1 style="text-align: center">FotoFusing complete at <% Response.Write(DateTime.Now); %></h1>
    </div>
    <div>
        <img class="ResultImage" src="<% Response.Write(imageUrl); %>" width="<% Response.Write(imageWidth); %>" height="<% Response.Write(imageHeight); %>" />
    </div>
</body>
</html>
