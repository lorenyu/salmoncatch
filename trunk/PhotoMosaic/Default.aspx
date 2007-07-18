<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salmon Catcher</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Welcome to theSalmonCatcher!</h1>
        <asp:TextBox ID="TargetImageLocationTextbox" runat="server">target.png</asp:TextBox><br />
        &nbsp;<asp:TextBox ID="ImageDirectoryTextbox" runat="server">colors</asp:TextBox><br />
        <asp:Button ID="AssembleButton" runat="server" OnClick="AssembleButton_Click" Text="Assemble!" /><br />
        <asp:Label ID="Label3" runat="server" Text="Number of Horizontal Images"></asp:Label>
        <asp:TextBox ID="NumHorizontalImagesTextbox" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="Number of Vertical Images"></asp:Label>
        <asp:TextBox ID="NumVerticalImagesTextbox" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="Target Image Location"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="images/target.jpg" Selected="True">Mike's Target Image</asp:ListItem>
            <asp:ListItem Value="images/Summer Psylocke.jpg">Loren's Target Image</asp:ListItem>
        </asp:DropDownList><br />
        <asp:Label ID="Label1" runat="server" Text="Component Image Directory"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="C:\Documents and Settings\AE\My Documents\Visual Studio 2005\WebSites\ColorGenerator\Colors" Selected="True">Michael's Directory</asp:ListItem>
            <asp:ListItem Value="C:\Documents and Settings\Loren Yu\Desktop\SVNSalmonCatch\ColorGenerator\color125">Loren's Directory</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        <asp:Image ID="debugImage" runat="server" Height="552px" Width="400px" /><br />
        <asp:Label ID="debugLabel" runat="server" Text="status ok"></asp:Label></div>
    </form>
</body>
</html>
