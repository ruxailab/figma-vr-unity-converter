using UnityEngine;
using UnityEditor;

public class Vector : Object {

    public Vector(ObjectProperty obj, float eixoZ, int escala) : base(obj, eixoZ, escala){}
    
    public GameObject createObject() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setVector();
        return gameObject;
    }

    public void setVector() {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        string id = obj.id;
        if(obj.id.IndexOf(';') > 0)
            id = obj.id.Remove(obj.id.IndexOf(';'));
        string imageUrl = APIService.GetImageID(id);
        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("http"));
        imageUrl = imageUrl.Remove(imageUrl.IndexOf("\"}}"));
        id = id.Remove(id.IndexOf(":"), 1);
        string path = $"Assets/Editor/Figma Convert/Images/{id}.png";
            
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
        System.IO.Directory.CreateDirectory("Assets/Editor/Figma Convert/Materials");
        AssetDatabase.CreateAsset(material, $"Assets/Editor/Figma Convert/Materials/{id}.mat");
        renderer.material = material;
    }
}