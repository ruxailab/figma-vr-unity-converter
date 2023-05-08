# Figma Convert 

Below are the steps to get the plugin up and running.

This plugin uses C# for plugin creation.

First, the Plugin.Unity.unitypackage at [Release](https://github.com/uramakilab/figma-vr-unity-converter/releases). Import it into Unity.

The code has two main files, Windows and Core, Windows is responsible for rendering the image in Unity, allowing the user to insert scale, token and URL of the document. The Core is responsible for making all project calls and organizing the code.

In addition to these two main files, we also have 3 API folders, build and classes, where the API folder is responsible for storing all API calls and JSON Serialization of the project. Build is responsible for building all GameObjects. The classes, on the other hand, are responsible for maintaining an organization and standardization between the GameObjects.