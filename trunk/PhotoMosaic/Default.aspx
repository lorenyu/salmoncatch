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
        <meta http-equiv="content-type" content="text/html; charset=UTF-8">


<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/assets/yui.css" >
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/container/assets/skins/sam/container.css" />
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/button/assets/skins/sam/button.css" />
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/fonts/fonts-min.css" />
<link rel="stylesheet" type="text/css" href="http://developer.yahoo.com/yui/build/button/assets/skins/sam/button.css" />
<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.2.2/build/reset-fonts-grids/reset-fonts-grids.css">

<link href="YUI/carousel.css" rel="stylesheet" type="text/css">
<link href="YUI/yui.css" rel="stylesheet" type="text/css">



<script type="text/javascript" src="http://developer.yahoo.com/yui/build/yahoo/yahoo-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/dom/dom-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/event/event-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/animation/animation-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/container/container-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/element/element-beta-min.js"></script>
<script type="text/javascript" src="http://developer.yahoo.com/yui/build/button/button-beta-min.js"></script>


<script type="text/javascript" src="http://yui.yahooapis.com/2.2.2/build/yahoo-dom-event/yahoo-dom-event.js"></script> 
<script type="text/javascript" src="http://yui.yahooapis.com/2.2.2/build/utilities/utilities.js"></script>
<script type="text/javascript" src="http://yui.yahooapis.com/2.2.2/build/container/container_core-min.js"></script>
<script type="text/javascript" src="YUI/carousel.js"></script>

<script type="text/javascript" src="http://developer.yahoo.com/yui/build/yahoo-dom-event/yahoo-dom-event.js"></script>


<style type="text/css">

.carousel-component { 
    padding:10px 10px 10px 10px;
    margin-top: 50px;
    margin-bottom: 50px;
    margin-right:  50px;
    margin-left: 50px;
    -moz-border-radius:0px;
    background-color: white;
    clear:both;
    border: 5px solid gray; 
}

.carousel-component .carousel-clip-region { 
    padding:0px;
    margin:0px;
}

.carousel-component ul.carousel-list{
    text-align:center;
    background-color:white;
}

.carousel-component .carousel-list li { 
    text-align:left;
    margin:0px;
    width:550px;
    height: 450px;
    border: 2px solid gray;    
}


                                           
</style>

<script type="text/javascript">

/**
 * Custom load next handler. Called when the carousel loads the next
 * set of data items. Specified to the carousel as the configuration
 * parameter: loadNextHandler
 **/
var loadNextItems = function(type, args) {    

    var start = args[0];
    var last = args[1]; 
    var alreadyCached = args[2];
    
    if(!alreadyCached) {
        load(this, start, last);
    }
};

/**
 * Custom load previous handler. Called when the carousel loads the previous
 * set of data items. Specified to the carousel as the configuration
 * parameter: loadPrevHandler
 **/
var loadPrevItems = function(type, args) {
    var start = args[0];
    var last = args[1]; 
    var alreadyCached = args[2];
    
    if(!alreadyCached) {
        load(this, start, last);
    }
};


var changePage = function(e, args) {
    var carousel = args[0];
    var pageNum = args[1];
    
    carousel.scrollTo(pageNum);
};

/**
 * You must create the carousel after the page is loaded since it is
 * dependent on an HTML element (in this case 'dhtml-carousel'.) See the
 * HTML code below.
 **/
var carousel; // for ease of debugging; globals generally not a good idea

var pageLoad = function() 
{
    carousel = new YAHOO.extension.Carousel("dhtml-carousel", 
        {
            numVisible:        1,
            animationSpeed:    0.5,
            scrollInc:         1,
            size:              4,
            navMargin:         0,
            loadNextHandler:   loadNextItems,
            loadPrevHandler:   loadPrevItems
        }
    );
    YAHOO.util.Event.addListener(this.carouselNext, "click", this._scrollNext, this);

    YAHOO.util.Event.addListener("1Forward", "click", changePage, [carousel, 2]);
    YAHOO.util.Event.addListener("2Forward", "click", changePage, [carousel, 3]);
    YAHOO.util.Event.addListener("3Forward", "click", changePage, [carousel, 4]);
    
    YAHOO.util.Event.addListener("2Back", "click", changePage, [carousel, 1]);
    YAHOO.util.Event.addListener("3Back", "click", changePage, [carousel, 2]);
    YAHOO.util.Event.addListener("4Back", "click", changePage, [carousel, 3]);
};


YAHOO.util.Event.addListener(window, 'load', pageLoad);


</script>
</head>
<body>

<form action="Default.aspx" method="post">

<div id="dhtml-carousel" class="carousel-component">
	<div class="carousel-clip-region" style="text-align: center">
		<ul class="carousel-list">

            <li id="dhtml-carousel-item-1" style="text-align: center">
                Target Image: <input type="text" name="TargetImageUrl" value="http://www.webpark.ru/uploads26/ssKelley_Hazel_4.jpg" />&nbsp;
                <div style="left: 0px; width: 550px; position: relative; top: 332px; height: 100px; text-align: center">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; 
                    <button id="1Forward" style="text-align: center"><span style="font-size: 14pt; position: relative;">Next&nbsp; &gt;</span></button></div>
            </li>
            
            <li id="dhtml-carousel-item-2"><div style="text-align: center">
                Username: <input type="text" name="Username" value="spikeblacklab" /></div>
                &nbsp;
               <div style="left: 0px; width: 550px; position: relative; top: 320px; height: 100px; text-align: center">
                <button id="2Back"><span style="font-size: 14pt; position: relative;">&lt; Back</span></button>
                <button id="2Forward"><span style="font-size: 14pt; position: relative;">Next &gt;</span></button></div>
            </li>
    

            <li id="dhtml-carousel-item-3"><div style="text-align: center">
                <input type="text" name="NumHorizontalImages" value="20" /><br />
                <input type="text" name="NumVerticalImages" value="20" /><br />
            </div><div style="left: 0px; width: 550px; position: relative; top: 312px; height: 100px; text-align: center">
                <button id="3Back"><span style="font-size: 14pt; position: relative;">&lt; Back</span></button>
                <button type="submit" id="3Forward" value="Assemble!"><span style="font-size: 14pt; position: relative;">Next &gt;</span></button></div>
            </li>
    
    
            <li id="dhtml-carousel-item-4"><div style="text-align: center">
                
                <asp:Image ID="debugImage" runat="server" Height="552px" Width="400px"  />
                
                <button id="4Back"><span style="font-size: 14pt; position: relative;">Back</span></button>
                </div>
            </li>

        </ul>
    </div>
</div>

</form>
        
</body>
</html>
