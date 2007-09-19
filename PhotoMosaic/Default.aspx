<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Salmon Catcher</title>
    
    <!-- YUI stuff -->
    
    <!-- utilities includes all dependencies for this example -->
    <script type="text/javascript" src="yui/build/utilities/utilities.js"></script>
    <script type="text/javascript" src="yui/build/tabview/tabview.js"></script>
    
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <link rel="stylesheet" type="text/css" href="yui/assets/yui.css" />
    <link rel="stylesheet" type="text/css" href="yui/build/container/assets/skins/sam/container.css" />
    <link rel="stylesheet" type="text/css" href="css/button.css" />
    <script type="text/javascript" src="yui/build/yahoo/yahoo-min.js"></script>
    <script type="text/javascript" src="yui/build/dom/dom-min.js"></script>

    <script type="text/javascript" src="yui/build/event/event-min.js"></script>
    <script type="text/javascript" src="yui/build/animation/animation-min.js"></script>
    <script type="text/javascript" src="yui/build/container/container-min.js"></script>
    <script type="text/javascript" src="yui/build/element/element-beta-min.js"></script>
    <script type="text/javascript" src="yui/build/button/button-beta-min.js"></script>

    <!-- end YUI stuff -->
    
    <!-- Styles TODO: factor out into files -->
    
    <script type="text/javascript">
    function showStep(stepNum) {
        YAHOO.example.container["step" + stepNum].show();
        for (var step = 1; step <= 3; step++) {
            if (step != stepNum) {
                YAHOO.example.container["step" + step].hide();
            }
        }
    }
    </script>
    
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
 
 .nextButton
 {
    padding: 5px;
    background-image: url(images/buttongradient1.png);
    background-repeat: repeat-x;
 }
 
 .logo
 {
    text-align: left;
    margin: 5px 10px;
 }
 
 .nav-bar
 {
    text-align: right;
    font-family: haettenschweiler;
    color: white;
    font-size: 20px;
    padding: 0px 10px;
 }
 
 .nav-bar-link
 {
    padding: 0px 10px;
 }
 
 a:link {text-decoration: none; color: yellow}
 a:visited {text-decoration: none; color: yellow}
 a:active {text-decoration: none; color: red}
 a:hover {text-decoration: underline bold; color: blue;}
</style>

<style type="text/css">
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

<script type="text/javascript">
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
	
	
	/*
	for (var i = 1; i < 3; i++) {
	    var btnId = "step" + i + "next";
	    var btn = new YAHOO.widget.Button(btnId, { 
            label: "Next"
        });
        YAHOO.util.Event.addListener(btnId, "click", function(e) { showStep(i + 1); }, YAHOO.example.container["step" + (i + 1)], true);
	}
	for (var i = 2; i <= 3; i++) {
	    var btnId = "step" + i + "back";
	    var btn = new YAHOO.widget.Button(btnId, { 
            label: "Back"
        });
        YAHOO.util.Event.addListener(btnId, "click", function(e) { showStep(i - 1); });
	}
	*/
	
	var btnStep1next = new YAHOO.widget.Button("step1next", { 
        label: "Next"
    });
	YAHOO.util.Event.addListener("step1next", "click", function(e) { showStep(2); }, YAHOO.example.container.step2, true);
	
	var btnStep2back = new YAHOO.widget.Button("step2back", { 
        label: "Back"
    });
    YAHOO.util.Event.addListener("step2back", "click", function(e) { showStep(1); }, YAHOO.example.container.step1, true);

    var btnStep2next = new YAHOO.widget.Button("step2next",
	    { 
            label: "Next"
        }
    );
	YAHOO.util.Event.addListener("step2next", "click", function(e) { showStep(3); }, YAHOO.example.container.step3, true);
	
	var btnStep3back = new YAHOO.widget.Button("step3back",
	    { 
            label: "Back"
        }
    );
	YAHOO.util.Event.addListener("step3back", "click", function(e) { showStep(2); }, YAHOO.example.container.step2, true);

    var btnFuse = new YAHOO.widget.Button(
	    "btnFuse",
	    { 
            label: "Fuse!"
        }
    );


    YAHOO.example.container.step1.show();
}

YAHOO.util.Event.onDOMReady(init);
</script>

<div class="header">

    <table style="width:100%;">
    <tr>
    <td>
        <div class="logo">
        <a href="Default.aspx" title="back to home page"><img alt="logo" src="images/logo.png" style="width:20%;" /></a>
        </div>
    </td>
    <td style="vertical-align: top; padding-top: 0px; margin-top: 0px">
        <p class="nav-bar">
        <a class="nav-bar-link" href="Default.aspx" title="back to home page" >home</a>
        <a class="nav-bar-link" href="tryit.htm" title="try it!" style="background-image: url(images/buttongradient2.png); background-repeat:repeat-x">try it</a>
	    </p>
	</td>

    </tr>
    </table>
	
</div>

<br />
<h1>Try it!</h1>

<form action="Default.aspx" method="post">

<div id="step1" style="visibility:hidden;">
    <div class="paneTitle">What images do you want to fuse?</div>
    Username:<input type="text" name="Username" value="spikeblacklab" /><br />
    <button class="nextButton" id="step1next">Next</button>
</div>

<div id="step2" style="visibility:hidden;">
    <div class="paneTitle">What do you want to be your main image?</div>
    Target Image:
    <input type="text" name="TargetImageUrl" value="http://images.techtree.com/ttimages/story/81238_1.jpg" /><br />
    <button class="nextButton" id="step2back">Back</button>
    <button class="nextButton" id="step2next">Next</button>
</div>

<div id="step3" style="visibility:hidden;">
    <div class="paneTitle">Go!</div>
    <input type="text" name="NumHorizontalImages" value="100" /><br />
    <input type="text" name="NumVerticalImages" value="100" /><br />
    <button class="nextButton" id="step3back">Back</button>
    <input id="btnFuse" type="submit" value="Fuse!" class="assembleButton" />
</div>
</form>

<center><div id="centerPanelLocation" style="width:50%;"></div></center>

<asp:Image ID="debugImage" runat="server" Height="552px" Width="400px" />
        
</body>
</html>
