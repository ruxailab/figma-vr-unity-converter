# Prototyped for User Interfaces in the Metaverse

![license](https://img.shields.io/github/license/uramakilab/figma-vr-unity-converter) ![Issues](https://img.shields.io/github/issues/uramakilab/figma-vr-unity-converter) ![Stars](https://img.shields.io/github/stars/uramakilab/figma-vr-unity-converter) ![Status](https://img.shields.io/badge/status-Development-orange)

The objective of this project is to facilitate the prototyping of user interfaces for the metaverse, thus facilitating and assisting in the development of software and systems for it.

## Summary
* [Prerequisites](#prerequisites)
    * [How to Install](#howInstall)
    * [How to Use](#run)
* [Technologies](#🛠️-technologies)
* [Documentations](#📚-documents-used)
* [Features](#🔨-features)
* [Additional Information](#additional-information)

## Prerequisites
Before starting, you need to have an account and install [Unity (2021.3)](https://unity.com/en/download) and [Figma](https://www.figma.com/downloads/) on your computer. Then install Unity UI Rounded Corners following the [documentation](https://github.com/kirevdokimov/Unity-UI-Rounded-Corners).

<div id="howInstall"/>

### How to Install
First perform [download Unity and Figma plugins](https://github.com/uramakilab/figma-vr-unity-converter/releases) on GitHub.

<img src="/assets/download.gif">

With Unity open importer plugin for Unity.

<img src="/assets/importUnity.gif">

Then extract the zip file and import the plugin manifest to Figma.

<img src="/assets/importFigma.gif">

<div id="howUse"/>

### How to Use
To use Figma Convert first draw one or more interfaces (it is important that precision interfaces are components).

<img src="/assets/interfaceFigma.png">

Then select the components you want to export to Unity and click on the Figma icon to open the menu, go to plugin, development and FTU (Figma To Unity). In this plugin you will position all the interfaces that were created, in a way that you want them to be positioned in Unity.

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

## 🔨 Features
* [x] Unity Tools
* [x] GameObject Canva
* [x] GameObject Painel
* [x] Position
* [x] Size
* [x] Color
* [x] None Color
* [x] Image
* [x] Image PNG
* [x] Image SVG
* [x] Frame in Frame
* [x] Rounded Cube
* [x] Border Color
* [ ] Sphere
* [ ] Circle
* [ ] Triangle
* [ ] Start
* [x] Text


## Additional Information

- Access [Postman](https://orange-space-957236.postman.co/workspace/Prototipado-para-interfaces-de-~d9f0f502-42b6-4da1-b34c-cacaf76b84bf/collection/21577195-86734ae6-cf68-4ac8-8aee-78992c835af9?action=share&creator=21577195)
- Tested on Unity 2021 release


<h4 align="center">🚧 Project under Development 🚧</h4>