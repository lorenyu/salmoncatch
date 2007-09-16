<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>FotoFusion</title>
    
    <link rel="Stylesheet" type="text/css" href="css/simple.css" />
</head>
<body>
    <div class="header">
        <h1 class="Title">
            FotoFusion
        </h1>
    </div>
    <div class="form">
        <form id="Form1" action="result.aspx" method="post" enctype="multipart/form-data">
            <div class="StepContainer TargetImage">
                <div class="Step">
                    <img src="images/duck.PNG" />
                    <h2 class="Title">
                        The Nucleus
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            Pick the main image of the fotofusion operation.
                            Click on "Browse" and find it on your computer to upload it.
                        </p>
                    </div>
                    <input type="file" id="targetImage" name="targetImage" />
                </div>
            </div>
            <div class="StepContainer ImageSet">
                <div class="Step">
                    <img src="images/collection.jpg" />
                    <h2 class="Title">
                        The Atoms
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            Pick the Fotos you want to Fuse
                            Select the source of images that will be used to compose the final image.
                            The set of images that will make up the final image.
                            The final image will be made up of this set of images.
                            If you have a Flickr account, then you can enter your Flickr ID to 
                        </p>
                    </div>
                    <div class="ImageSetSource">
                        <div class="FlickrUsername">
                            <span class="Radio" onclick="javascript:document.getElementById('usernameRadio').checked=true">
                                <input class="Radio" type="radio" id="usernameRadio" name="imageSetType" value="username" checked="checked" />
                                <label class="Radio">Username:</label>
                            </span>
                            <input class="ImageSetSource" type="text" name="username" id="Text3" />
                        </div>
                        <div class="FlickrSearch">
                            <span class="Radio" onclick="javascript:document.getElementById('searchTextRadio').checked=true">
                                <input class="Radio" type="radio" id="searchTextRadio" name="imageSetType" value="searchText" />
                                <label class="Radio">Search:</label>
                            </span>
                            <input class="ImageSetSource" type="text" name="searchText" id="Text4" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="StepContainer FotoFuse">
                <div class="Step">
                    <img src="images/resultimage_sqaure.png" />
                    <h2 class="Title">
                        The Fusion
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            Click on "Fuse" to begin fotofusing!
                        </p>
                        <p class="Description">
                            (Be patient.  It may take a minute)
                        </p>
                    </div>
                    <input class="Button assembleButton" id="fuse" name="fuse" type="submit" value="Fuse!" />
                </div>
            </div>
        </form>
    </div>
</body>
</html>
