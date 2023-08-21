using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net;
using System.IO;
using System.Text;

abstract class loginService {
    public static string login(){
        string url = "https://www.figma.com/oauth?client_id=5OT2eSe3pVPAEENW6YdOvG&redirect_uri=http%3A%2F%2Flocalhost%3A4444%2F&scope=file_read&state=true&response_type=code";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            System.Diagnostics.Process.Start(url);
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
            System.Diagnostics.Process.Start("open", url);
        else
            System.Diagnostics.Process.Start("xdg-open", url);
        string urlResponse = startHttpListener();
        string code = getCode(urlResponse);
        Token tokenAccess = getToken(code);
        return tokenAccess.access_token;
        // return "oi";
    }

    public static string startHttpListener() {
        HttpListener listener = new HttpListener();
		listener.Prefixes.Add("http://localhost:4444/");
		listener.Start ();

        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        // Construct a response.
        string responseString = "<html><body><h1>Pode fechar e voltar para o Unity</h1></body></html>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer,0,buffer.Length);
        
        output.Close();
        listener.Stop();
        return request.Url.OriginalString;
    }

    public static string getCode(string url) {
        return url.Remove(url.IndexOf('&')).Remove(0,28);
    }

    public static Token getToken(string code) {   
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.figma.com/api/oauth/token");
        string postData = "client_id=" + Uri.EscapeDataString("5OT2eSe3pVPAEENW6YdOvG");
            postData += "&client_secret=" + Uri.EscapeDataString("t5lxa2DXm35BjK8kveHsCI9BnsOszK");
            postData += "&redirect_uri=" + Uri.EscapeDataString("http://localhost:4444/");
            postData += "&code=" + Uri.EscapeDataString(code);
            postData += "&grant_type=" + Uri.EscapeDataString("authorization_code");
        Byte[] data = Encoding.ASCII.GetBytes(postData);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
        Stream stream = request.GetRequestStream();
        stream.Write(data, 0, data.Length);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        return JsonUtility.FromJson<Token>(responseString);
    }

    // public static Token refreshToken(string refreshToken) {  
    //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.figma.com/api/oauth/refresh");
    //     string postData = "client_id=" + Uri.EscapeDataString("5OT2eSe3pVPAEENW6YdOvG");
    //         postData += "&client_secret=" + Uri.EscapeDataString("t5lxa2DXm35BjK8kveHsCI9BnsOszK");
    //         postData += "&refresh_token=" + Uri.EscapeDataString(refreshToken);
    //     Byte[] data = Encoding.ASCII.GetBytes(postData);
    //     request.Method = "POST";
    //     request.ContentType = "application/x-www-form-urlencoded";
    //     request.ContentLength = data.Length;
    //     Stream stream = request.GetRequestStream();
    //     stream.Write(data, 0, data.Length);

    //     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //     string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    //     return JsonUtility.FromJson<Token>(responseString);
    // }
}