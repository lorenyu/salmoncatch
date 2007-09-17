<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>FotoFusion</title>
    
    <link rel="Stylesheet" type="text/css" href="css/simple.css" />
    
    <script type="text/javascript">
        function toggleAdvancedOptions()
        {
            var style = document.getElementById('advancedOptions').style;
            style.display = (style.display=='none') ? '' : 'none';
        }
        
        function resetAdvancedOptionsDefaults()
        {
            document.getElementById('levelOfDetail').value = 'MEDIUM';
            document.getElementById('assembleQuality').value = 'HIGH';
        }
    </script>
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
                        1. The Nucleus
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            Pick the foto you want to be the main image of the fotofusion operation.
                            This image will guide the fotofusion operation in creating a coherent final result.
                        </p>
                        <p class="Description">
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
                        2. The Atoms
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            Pick the fotos you want to use in the fotofusion operation.
                            These images are the pieces that will be fotofused together to create the final image.
                        </p>
                        <p class="Description">
                            If you have a Flickr account, then enter your Flickr ID to use the public fotos from your Flickr account.
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
                        3. The Fusion
                    </h2>
                    <div class="Description">
                        <p class="Description">
                            The fotofusion operation is ready to begin.
                        </p>
                        <p class="Description">
                            For advanced options, click "Advanced".
                            When you are ready, click "Fuse" to begin!
                        </p>
                        <p class="Description">
                            (Be patient.  The fotofusion process may take a minute.)
                        </p>
                    </div>
                    <input class="Button assembleButton" id="fuse" name="fuse" type="submit" value="Fuse!" />
                    <hr />
                    <input type="button" class="ToggleAdvancedOptions StyledButton" onclick="toggleAdvancedOptions();" value="Advanced Options" />
                    <div class="AdvancedOptions" id="advancedOptions" style="display:none">
                        <div class="LevelOfDetail">
                            <label class="AdvancedOptions">Level of detail:</label>
                            <select id="levelOfDetail" name="levelOfDetail">
                                <option value="LOWEST">Low</option>
                                <option value="LOW">Low</option>
                                <option value="MEDIUM" selected="selected">Medium</option>
                                <option value="HIGH">High</option>
                                <option value="HIGHEST">Highest</option>
                            </select>
                        </div>
                        <div class="AssembleQuality">
                            <label class="AdvancedOptions">Quality of matching:</label>
                            <select id="assembleQuality" name="assembleQuality">
                                <option value="LOW">Low</option>
                                <option value="MEDIUM">Medium</option>
                                <option value="HIGH" selected="selected">High</option>
                                <option value="HIGHEST">Highest</option>
                            </select>
                        </div>
                        <input type="button" class="StyledButton" value="Reset defaults" onclick="resetAdvancedOptionsDefaults();" />
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
