# Prototyped for User Interfaces in the Metaverse

![license](https://img.shields.io/github/license/uramakilab/figma-vr-unity-converter) ![Issues](https://img.shields.io/github/issues/uramakilab/figma-vr-unity-converter) ![Stars](https://img.shields.io/github/stars/uramakilab/figma-vr-unity-converter) ![Status](https://img.shields.io/badge/status-Development-orange)

The objective of this project is to facilitate the prototyping of user interfaces for the metaverse, thus facilitating and assisting in the development of software and systems for it.

## Summary
* [Prerequisites](#prerequisites)
    * [How to Install](#how)
    * [How to Use](#run)
* [Technologies](#🛠️-technologies)
* [Documentations](#📚-documents-used)
* [Features](#🔨-features)
* [Additional Information](#additional-information)

## Prerequisites
Before starting, you need to have an account and install [Unity (2021.3)](https://unity.com/en/download) and [Figma](https://www.figma.com/downloads/) on your computer. Then install Unity UI Rounded Corners following the [documentation](https://github.com/kirevdokimov/Unity-UI-Rounded-Corners).

### How to Install
Primeiramente realize o [download dos plugins do Unity e Figma](https://github.com/uramakilab/figma-vr-unity-converter/releases) no GitHub.

<img src="/assets/download.gif">

Com o Unity aberto importe o plugin para o Unity.

<img src="/assets/importUnity.gif">

Em seguida extraia o arquivo zip e importe o manifest plugin para o Figma.

<img src="/assets/importFigma.gif">


### How to Use
Para utilizar o Figma Convert primeiramente desenhe um ou mais interfaces (importante as interfaces precição ser componentes).

<img src="/assets/interfaceFigma.png">

Em seguida selecione os componentes que deseja exportar para o Unity e click no icone do Figma para abrir o menu, vá em plugin, development e FTU (Figma To Unity). Nesse plugin você irá posicionar todas as interfaces que foram criadas, de forma que deseje que elas sejam posicionadas no Unity. 

<img src="/assets/pluginFigma.gif">

Agora vá no Unity click em tools, Figma Convert. Com a janela do Figma Convert click em Login Browser para que possa ser obtido o token automaticamente ou insira o token manualmente. Em seguida copie a url do Documento da Unity e por final click em Download Projet.

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
* [x] GameObject Cube and Rectangle
* [x] GameObject Sphere
* [ ] GameObject Circle
* [ ] GameObject Triangle
* [ ] GameObject Start
* [x] GameObject Text
* [x] GameObject Frame
* [ ] GameObject Component
* [x] Position
* [x] Size
* [x] Color
* [x] None Color
* [x] Image
* [x] Image PNG
* [ ] Image SVG
* [x] Frame in Frame
* [x] Rounded Cube
* [ ] Border Color

## Additional Information

- Access [Postman](https://orange-space-957236.postman.co/workspace/Prototipado-para-interfaces-de-~d9f0f502-42b6-4da1-b34c-cacaf76b84bf/collection/21577195-86734ae6-cf68-4ac8-8aee-78992c835af9?action=share&creator=21577195)
- Tested on Unity 2021 release


<h4 align="center">🚧 Project under Development 🚧</h4>