<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>FotoFusion</title>
</head>
<body>
    <div class="header">
        <h1>Try it!</h1>
    </div>
    <div class="form">
        <form id="Form1" action="Default.aspx" method="post" enctype="multipart/form-data" runat="server">
            <div class="upload">
                Upload image:
                <asp:FileUpload ID="targetImage" name="targetImage" runat="server" />
            </div>
            <div class="imageSet">
                <div class="username">
                    <input type="radio" name="imageSetType" value="username" />
                    Username:
                    <input type="text" name="username" id="username" />
                </div>
                <div class="searchText">
                    <input type="radio" name="imageSetType" value="searchText" />
                    Search:
                    <input type="text" name="searchText" id="searchText" />
                </div>
            </div>
            <div class="fuse">
                <input id="btnFuse" type="submit" value="Fuse!" class="assembleButton" />
            </div>
        </form>
    </div>
    <asp:Image ID="debugImage" runat="server" Height="552px" Width="400px" />
</body>
</html>
