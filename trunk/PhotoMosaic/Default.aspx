<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salmon Catcher</title>
</head>
<body onload="OnLoad">
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
        <asp:Label ID="Label2" runat="server" Text="Target Image Filename"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="target.jpg" Selected="True">Mike's Target Image</asp:ListItem>
            <asp:ListItem Value="Summer Psylocke.jpg">Loren's Target Image</asp:ListItem>
        </asp:DropDownList><br />
        <asp:Label ID="Label1" runat="server" Text="Component Image Directory"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="Colors" Selected="True">Michael's Directory</asp:ListItem>
            <asp:ListItem Value="color125">Loren's Directory</asp:ListItem>
            <asp:ListItem Value="color27">27 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color0">0 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color1">1 color - Loren</asp:ListItem>
            <asp:ListItem Value="color8">8 colors - Loren</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        <asp:Image ID="debugImage" runat="server" Height="552px" Width="400px" />
        <br />
        <br />
        Benchmarks (seconds)<br />
        <table>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 21px">
                </td>
                <td style="width: 100px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Create Objective</td>
                <td style="width: 100px">
                    <asp:Label ID="objectiveTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Create Assembler</td>
                <td style="width: 100px">
                    <asp:Label ID="assemblerTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Assemble Objective</td>
                <td style="width: 100px">
                    <asp:Label ID="assembleObjectiveTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Total</td>
                <td style="width: 100px">
                    <asp:Label ID="totalTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="debugLabel" runat="server" Text="status ok"></asp:Label></div>
    </form>
</body>
</html>
