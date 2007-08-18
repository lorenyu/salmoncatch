<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Salmon Catcher</title>
    
    <!-- YUI stuff -->
    
    <!-- for default tab skin, which includes tabview-core.css and skins/sam/tabview-skin.css -->
    <link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/tabview/assets/skins/sam/tabview.css" />

    <!-- utilities includes all dependencies for this example -->
    <script type="text/javascript" src="http://developer.yahoo.com/yui/build/utilities/utilities.js"></script>
    <script type="text/javascript" src="http://developer.yahoo.com/yui/build/tabview/tabview.js"></script>
    
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/assets/yui.css" >
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/container/assets/skins/sam/container.css" />
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/button/assets/skins/sam/button.css" />
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/yahoo/yahoo-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/dom/dom-min.js"></script>

<script type="text/javascript" src="http://developer.yahoo.com/yui/build/event/event-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/animation/animation-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/container/container-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/element/element-beta-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/button/button-beta-min.js"></script>

    <!-- end YUI stuff -->
    
    <!-- Styles TODO: factor out into files -->
    
<style type="text/css">
 body
 {
    background-color: black;
    font-family: haettenschweiler;
    color: white;
 }

 h1
 {
    color: white;
    font-size: 42px;
 }
 
 h2
 {
    color: white;
    font-size: 31px;
 }
 
 .paneTitle
 {
    padding: 5px;
    color: white;
    font-size: 31px;
 }
</style>

<style>
.yui-overlay
{
    position:absolute;
    border-width: 10px;
    border-style: groove;
    border-color: white;
    padding:5px;
    margin:10px;
    background-color: black;
}
</style>
    
    <!-- end styles -->
</head>
<body class="yui-skin-sam">
        <h1>FotoFusion</h1>
        <br />
        <h1>Try it!</h1>

<script>
YAHOO.namespace("example.container");

function init() {
	// Build overlay1 based on markup
	YAHOO.example.container.step1 = new YAHOO.widget.Overlay("step1", { context:["centerPanelLocation","tl","bl"],
																			  visible:false,
																			  width:"50%",
																			  height:"50%",
																			  zIndex:1000,
																			  effect:{effect:YAHOO.widget.ContainerEffect.FADE,duration:0.5} } );
	YAHOO.example.container.step1.render("example");

	// Build overlay2 based on markup
	YAHOO.example.container.step2 = new YAHOO.widget.Overlay("step2", { context:["centerPanelLocation","tl","bl"],
																			  visible:false,
																			  width:"50%",
																			  height:"50%",
																			  zIndex:1000,
																			  effect:{effect:YAHOO.widget.ContainerEffect.FADE,duration:0.5} } );
	YAHOO.example.container.step2.render("example");

	// Build overlay3 based on markup
	YAHOO.example.container.step3 = new YAHOO.widget.Overlay("step3", { context:["centerPanelLocation","tl","bl"],
																			  visible:false,
																			  width:"50%",
																			  height:"50%",
																			  zIndex:1000,
																			  effect:[{effect:YAHOO.widget.ContainerEffect.FADE,duration:0.5}] } );
	YAHOO.example.container.step3.render("example");

	YAHOO.util.Event.addListener("show1", "click", function(e) { YAHOO.example.container.step1.show();
	                                                             YAHOO.example.container.step2.hide();
	                                                             YAHOO.example.container.step3.hide(); }, YAHOO.example.container.step1, true);

	YAHOO.util.Event.addListener("show2", "click", function(e) { YAHOO.example.container.step2.show();
	                                                             YAHOO.example.container.step1.hide();
	                                                             YAHOO.example.container.step3.hide(); }, YAHOO.example.container.step2, true);

	YAHOO.util.Event.addListener("show3", "click", function(e) { YAHOO.example.container.step3.show();
	                                                             YAHOO.example.container.step1.hide();
	                                                             YAHOO.example.container.step2.hide(); }, YAHOO.example.container.step3, true);

}

YAHOO.util.Event.onDOMReady(init);
</script>

<div>
Step 1:
<button id="show1">Show</button>
</div>

<div>
Step 2:
<button id="show2">Show</button>

</div>
<div>
Step 3:
<button id="show3">Show</button>
</div>

<form action="Default.aspx" method="post">

<div id="step1" style="visibility:hidden;">
    <div class="paneTitle">What images do you want to fuse?</div>
    Username:
    <input type="text" name="UsernameTextBox" value="spikeblacklab" />
</div>

<div id="step2" style="visibility:hidden;">
    <div class="paneTitle">What do you want to be your main image?</div>
    Target Image:
    <input type="text" name="TargetImageUrl" value="http://www.webpark.ru/uploads26/ssKelley_Hazel_4.jpg" />
</div>

<div id="step3" style="visibility:hidden;">
    <div class="paneTitle">Go!</div>
    <input type="text" name="NumHorizontalImages" value="100" /><br />
    <input type="text" name="NumVerticalImages" value="100" /><br />
    <input type="submit" value="Assemble!" />
</div>
</form>

<div id="centerPanelLocation">center panel location is here.</div>

        <a href="HelloWorld.htm">Hello world</a>
        <asp:Image ID="debugImage" runat="server" Height="552px" Width="400px" />
        
</body>
</html>
