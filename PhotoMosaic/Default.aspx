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
        <br />
        Username:
        <asp:TextBox ID="UsernameTextBox" runat="server">flickrtester123</asp:TextBox><br />
        <br />
        <asp:Label ID="TargetImageLabel" runat="server" Text="Target Image"></asp:Label>
        <asp:CheckBox ID="Use_Drop_Down_Target" runat="server" OnCheckedChanged="DropDownOrURLCheckBox_CheckedChanged"
            Text="Use Drop Down Menu" Checked="True" /><br />
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="target.jpg" Selected="True">Mike's Target Image</asp:ListItem>
            <asp:ListItem Value="Summer Psylocke.jpg">Loren's Target Image</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="sourceURL" runat="server" OnTextChanged="TextBox1_TextChanged">http://www.techtickerblog.com/wp-content/uploads/2006/10/led-blow-off-on-candles.jpg</asp:TextBox><br />
        <br />
        <asp:Button ID="AssembleButton" runat="server" OnClick="AssembleButton_Click" Text="Assemble!" />
        <asp:Button ID="TestButton" runat="server" OnClick="TestButton_Click" Text="Test" /><br />
        <asp:Label ID="Label3" runat="server" Text="Number of Horizontal Images"></asp:Label>
        <asp:TextBox ID="NumHorizontalImagesTextbox" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="Number of Vertical Images"></asp:Label>
        <asp:TextBox ID="NumVerticalImagesTextbox" runat="server"></asp:TextBox><br />
        <br />
        &nbsp;<asp:Label ID="Label1" runat="server" Text="Component Image Directory"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="Colors" Selected="True">Michael's Directory</asp:ListItem>
            <asp:ListItem Value="color4096">4096 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color1000">1000 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color125">125 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color27">27 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color8">8 colors - Loren</asp:ListItem>
            <asp:ListItem Value="color1">1 color - Loren</asp:ListItem>
            <asp:ListItem Value="color0">0 colors - Loren</asp:ListItem>
        </asp:DropDownList><br />
        <asp:CheckBox ID="UseKdTree" runat="server" Text="Use kd-tree" /><br />
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
