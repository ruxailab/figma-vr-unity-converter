<h2 align="center">Prototyped for User Interfaces in the Metaverse</h2>
<p align="center">EN - BR</p>

![license](https://img.shields.io/github/license/uramakilab/figma-vr-unity-converter) ![Issues](https://img.shields.io/github/issues/uramakilab/figma-vr-unity-converter) ![Stars](https://img.shields.io/github/stars/uramakilab/figma-vr-unity-converter) ![Status](https://img.shields.io/badge/status-Development-orange)

The objective of this project is to facilitate the prototyping of user interfaces for the metaverse, thus facilitating and assisting in the development of software and systems for it.

## Summary
* [Prerequisites](#prerequisites)
    * [How to Install](#howInstall)
    * [How to Use](#howUse)
* [Technologies](#🛠️-technologies)
* [Documentations](#📚-documents-used)
* [Additional Information](#additional-information)
* [Changelog](#changelog)
* [Contributing](#contributing)
<!--* [Thanks](#thanks)-->

## Prerequisites
Before starting, you need to have an account and install <a href="https://unity.com/pt/download" target="_blank">Unity (2021.3)</a> and <a href="https://www.figma.com/downloads/" target="_blank">Figma Desktop APP</a> on your computer. Then install Unity UI Rounded Corners following the [documentation](https://github.com/kirevdokimov/Unity-UI-Rounded-Corners).

<div id="howInstall"/>

### How to Install
First download the [Plugin.Figma.zip](https://github.com/uramakilab/figma-vr-unity-converter/releases) and [Plugin.Unity.unitypackage](https://github.com/uramakilab/figma-vr-unity-converter/releases) on GitHub.

<img src="/assets/download.gif">

Then import the plugin into Unity:
* Open Unity.
* Go to where the Plugin.Unity.unitypackage was downloaded.
* Open the Plugin.Unity.unitypackage file.
* When the import window appears, click import.

<img src="/assets/importUnity.gif">

Now to import the plugin into figma:
* Extract the Plugin.Figma.zip.
* Open Figma Desktop.
* Open a project in Figma.
* Click on the Figma icon to open the menu.
* Go to Plugins, Development, Import plugin from manifest.
* Choose the manifest.json file inside the folder that was extracted.

<img src="/assets/importFigma.gif">

<div id="howUse"/>

### How to Use
To use Figma Convert first draw one or more interfaces (it is important that precision interfaces are components).

<img src="/assets/interfaceFigma.png">

Then, to position the components the way you want them to be positioned in Unity:
* Select the components you want to export
* Click the Figma icon to open the menu
* Go to plugin, development and FTU (Figma To Unity)

In the plugin you can position each component in the place you want, just select the component with double click and place it in the position you want.

<img src="/assets/pluginFigma.gif">

Now go to Unity click on tools, Figma Convert. With the window of Figma Convert click on Login Browser so that the token can be obtained automatically or enter the token manually. Then copy the url of the Unity Document and finally click on Download Project.

<img src="/assets/pluginUnity.gif">

## 🛠️ Technologies
The following tools and technologies were used in the construction of the project:

* Language C#
* [Unity](https://unity.com/pt)
* [Figma](https://figma.com/)
* [Postman](https://www.postman.com/)
* API REST

## 📚 Documents Used
Some of the documentation used in the project:

* [C# Documentation](https://learn.microsoft.com/pt-br/dotnet/csharp/)
* [Unity Documentation](https://docs.unity.com/)
* [Figma API](https://www.figma.com/developers/api)


## Additional Information

- Access [Postman](https://orange-space-957236.postman.co/workspace/Prototipado-para-interfaces-de-~d9f0f502-42b6-4da1-b34c-cacaf76b84bf/collection/21577195-86734ae6-cf68-4ac8-8aee-78992c835af9?action=share&creator=21577195)
- Tested on Unity 2021 release

<div id="changelog"/>

## 📝 Changelog

Detailed changes for each release are documented in the [release notes](/CHANGELOG.md).

<div id="contributing"/>

## 💁‍♂️ Contributing

Developers interested in contributing should read the [Contribution Guide](/CONTRIBUTING.md).
We also have a list of [issues](https://github.com/uramakilab/figma-vr-unity-converter/issues) you might want to check out.

<!--## Thanks-->

<h4 align="center">🚧 Project under Development 🚧</h4>