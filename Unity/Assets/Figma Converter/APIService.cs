using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;

public class APIService {

    public static File GetDocument(string token, string documentID) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{documentID}");
        request.Headers.Add("X-Figma-Token", token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<File>(json);
    }

    public static string GetImages(string token, string documentID) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{documentID}/images");
        request.Headers.Add("X-Figma-Token", token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        return reader.ReadToEnd();
    }

    public static string  ContentType(string url) {
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
        if(uwr.isNetworkError) {
            return false;
        } else {
            return true;
        }
    }
}


