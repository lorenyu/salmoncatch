<%@ Page Language="VB" %>
<script Language="VB" Option="Explicit" runat="server">

	Sub Page_Load(Src as object, E as EventArgs)
		If Request.QueryString("Text") <> "" Then
			lblMethod.Text = "get"
			lblText.Text   = Request.QueryString("Text")
		End If

		If Request.Form("Text") <> "" Then
			lblMethod.Text = "post"
			lblText.Text   = Request.Form("Text")
		End If
	End Sub

</script>

<html>
<head>
 <title>ASP.NET Get vs. Post Sample from ASP 101</title>
</head>
<body>

<!--
Notice the only difference in the two forms
is the value specified in the METHOD attribute
-->

<form action="getpost.aspx" method="get">
	<input type="text" name="Text" value="Hello World" />
	<input type="submit" value="Use Get" />
</form>

<br />

<form action="getpost.aspx" method="post">
	<input type="text" name="Text" value="Hello World" />
	<input type="submit" value="Use Post" />
</form>

<p>
The text in the box was
<strong>&quot;<asp:Label id="lblText" runat="server" />&quot;</strong>
It looks like you used
<strong>&quot;<asp:Label id="lblMethod" runat="server" />&quot;</strong>
to send it.
</p>

<hr />

<p>
Using &quot;get&quot; to pass information sends the information appended
to the request for the processing page.  It tends to be simpler and you
can troubleshoot any problems simply by looking at the address bar in
your browser since all values passed are displayed there.  This is also
the primary weakness of this method.  The data being passed is visible
and is limited in size to the maximum length of a request string.
</p>
<p>
Using &quot;post&quot; to pass information sends the information embedded
in a header during the request for the processing page.  It's main
advantage is that you can send larger amounts of information.  It also
doesn't make that information visible in the address bar of the browser
which is nice if you are using the &quot;hidden&quot; input type.  The
value of this type is still readily available to the user by using view
source, but the average user won't see it or be confused by any
information you may need to pass from your form for processing.
</p>

</body>
</html>
