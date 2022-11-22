using UnityEngine;
using UnityEditor;

public abstract class Objects {
    private ObjectProperty obj;
    public ObjectProperty Obj { get => obj; set => obj = value; }
    private string apiImage;
    public string ApiImage { get => apiImage; set => apiImage = value; }
    private int z;
    public int Z { get => z; set => z = value; }
    private int escala;
    public int Escala { get => escala; set => escala = value; }
    private float width;
    public float Width { get => width; set => width = value; }
    private float height;
    public float Height { get => height; set => height = value; }
    private GameObject gameObject;
    public GameObject GameObject { get => gameObject; set => gameObject = value; }

    public Objects(ObjectProperty obj, string apiImage, int z, int escala) {
        Obj = obj;
        ApiImage = apiImage;
        Z = z;
        Escala = escala;
    }

    public void setSize() {
        Width = Obj.absoluteBoundingBox.width/Escala;
        Height = Obj.absoluteBoundingBox.height/Escala;
        Vector3 size = new Vector3(Width, Height, 1);
        GameObject.transform.localScale = size;
    }

    public void setPosition() {
        float eixoX = (Obj.absoluteBoundingBox.x/Escala) + (Width/2);
        float eixoY = (Obj.absoluteBoundingBox.y/Escala) + (Height/2);
        Vector3 position = new Vector3(eixoX, eixoY, (float)(0.01*Z));
        GameObject.transform.position = position;
    }

    public void setColor() {
        Debug.Log(Obj.fills[0].type);
        string colorType = Obj.fills[0].type;
        if(colorType == "SOLID") {
            float r = Obj.fills[0].color.r;
            float g = Obj.fills[0].color.g;
            float b = Obj.fills[0].color.b;
            float a = Obj.fills[0].color.a;
            Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));

            // Variavel teporaria para inserir a cor do Objeto
            Material tempMaterial = new Material(GameObject.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = color;
            GameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
        else if(colorType == "IMAGE") {
            string imageRef = Obj.fills[0].imageRef;
            string imageUrl = ApiImage;
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf(imageRef));
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("https:"));
            imageUrl = imageUrl.Remove(imageUrl.IndexOf('"'));

            string contentType = APIService.ContentType(imageUrl);
            string path = $"Assets/Figma Converter/Images/{imageRef}.{contentType}";
            
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
            System.IO.Directory.CreateDirectory("Assets/Figma Converter/Materials");
            AssetDatabase.CreateAsset(material, $"Assets/Figma Converter/Materials/{imageRef}.mat");
            GameObject.GetComponent<Renderer>().material = material;
        }
    }
}