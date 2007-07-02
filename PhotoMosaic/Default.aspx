<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salmon Catcher</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Welcome to SalmonCatcher!</h1>
        <asp:ImageMap ID="ImageMap1" runat="server">
        </asp:ImageMap><br />
        <asp:Button ID="AssembleButton" runat="server" OnClick="AssembleButton_Click" Text="Assemble!" />
    </div>
    </form>
</body>
</html>
