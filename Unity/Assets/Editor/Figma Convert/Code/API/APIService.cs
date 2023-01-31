﻿using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net;
using System.IO;

abstract class APIService {

    public static File GetDocument() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{Global.documentID}");
        request.Headers.Add("Authorization", $"Bearer {Global.token}");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<File>(json);
    }

    public static string GetImage() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{Global.documentID}/images");
        request.Headers.Add("Authorization", $"Bearer {Global.token}");
        using (HttpWebResponse response = (HttpWebResponse) request.GetResponse()) {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd(); 
        }
    }

    public static string ContentType(string url) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string type = response.ContentType;
        return type.Remove(0, type.IndexOf("/")+1);
    }

    public static bool DownloadImage(string url, string path) {        
        UnityWebRequest uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        uwr.SendWebRequest();
        while(!uwr.isDone){}
        return uwr.responseCode == 200 ? true : false;

        // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        // HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
        // Response.ContentType = ;
        // img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
    }

    public static string GetImageID(string id) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/images{Global.documentID}?ids={id}");
        request.Headers.Add("Authorization", "Bearer "+ Global.token);
        using (HttpWebResponse response = (HttpWebResponse) request.GetResponse()) {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd(); 
        }
    }
}