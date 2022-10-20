using UnityEngine;
using System.Net;
using System.IO;

public class APIService {

    public static File GetDocument(string token, string documentID){
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{documentID}");
        request.Headers.Add("X-Figma-Token", token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<File>(json);
    }

    public static Image GetImages(string token, string documentID){
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.figma.com/v1/files{documentID}/images");
        request.Headers.Add("X-Figma-Token", token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Image>(json);
    }

}


