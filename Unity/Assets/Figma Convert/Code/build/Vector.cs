using UnityEngine;
using UnityEditor;

public class Vector : Object {
    private string documentID;
    private string token;

    public Vector(string documentID, string token, ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){
        this.documentID = documentID;
        this.token = token;
    }
    
    public GameObject createObject(){
        if(vertorExist())
            return null;
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setSVG();
        return gameObject;
    }

    public bool vertorExist() {
        string id = obj.id.Remove(obj.id.IndexOf(';'));
        id = id.Remove(id.IndexOf(":"));
        if(UnityEngine.Windows.File.Exists($"Assets/Figma Convert/Materials/{id}.mat"))
            return true;
        else
         return false;
    }

    public void setSVG() {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        string id = obj.id.Remove(obj.id.IndexOf(';'));
        string imageUrl = APIService.GetImageID(token, documentID, id);
        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("http"));
        imageUrl = imageUrl.Remove(imageUrl.IndexOf("\"}}"));
        string contentType = APIService.ContentType(imageUrl);
        string path = $"Assets/Figma Convert/Images/{id}.{contentType}";
            
        if(!APIService.DownloadImage(imageUrl, path)) {
            Debug.Log("Erro no Download da Imagem");
            return;
        }
        if(!System.IO.File.Exists(path)) {
            Debug.Log("Erro no Arquivo de Material");
            return;
        }
        byte[] readImg = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(readImg);
        Material material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
        material.mainTexture = tex;
        material.mainTextureScale = new Vector2(-1, -1);
        System.IO.Directory.CreateDirectory("Assets/Figma Convert/Materials");
        id = id.Remove(id.IndexOf(":"));
        AssetDatabase.CreateAsset(material, $"Assets/Figma Convert/Materials/{id}.mat");
        renderer.material = material;
    }
}