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
        <asp:Label ID="Label2" runat="server" Text="Target Image Location"></asp:Label>
        <asp:TextBox ID="TargetImageLocationTextbox" runat="server">target.png</asp:TextBox><br />
        <asp:Label ID="Label1" runat="server" Text="Image Directory"></asp:Label>
        <asp:TextBox ID="ImageDirectoryTextbox" runat="server">colors</asp:TextBox><br />
        <asp:Button ID="AssembleButton" runat="server" OnClick="AssembleButton_Click" Text="Assemble!" /><br />
        <asp:Label ID="debugLabel" runat="server" Text="status ok"></asp:Label><br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem>Test 1</asp:ListItem>
            <asp:ListItem>Test2</asp:ListItem>
            <asp:ListItem Value="C:\Documents and Settings\Loren Yu\My Documents\My Media\My Pictures\Comics\X-Men\Summer Psylocke.jpg">Loren's Target Image</asp:ListItem>
        </asp:DropDownList></div>
    </form>
</body>
</html>
