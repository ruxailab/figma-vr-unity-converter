using UnityEngine;
using UnityEditor;

public class Objects {
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
    private Vector3 size;
    public Vector3 Size { get => size; set => size = value; }
    private float eixoX;
    public float EixoX { get => eixoX; set => eixoX = value; }
    private float eixoY;
    public float EixoY { get => eixoY; set => eixoY = value; }
    private Vector3 position;
    public Vector3 Position { get => position; set => position = value; }
    private string colorType;
    public string ColorType { get => colorType; set => colorType = value; }
    private string imageRef;
    public string ImageRef { get => imageRef; set => imageRef = value; }
    private Color32 color;
    public Color32 Color { get => color; set => color = value; }
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
        Size = new Vector3(Width, Height, 1);
        GameObject.transform.localScale = Size;
    }

    public void setPosition() {
        EixoX = (Obj.absoluteBoundingBox.x/Escala) + (Width/2);
        EixoY = (Obj.absoluteBoundingBox.y/Escala) + (Height/2);
        Position = new Vector3(EixoX, EixoY, (float)(0.01*Z));
        GameObject.transform.position = Position;
    }

    public void setColor() {
        ColorType = obj.fills[0].type;
        if(ColorType == "SOLID") {
            float r = obj.fills[0].color.r;
            float g = obj.fills[0].color.g;
            float b = obj.fills[0].color.b;
            float a = obj.fills[0].color.a;
            Color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));

            // Variavel teporaria para inserir a cor do Objeto
            Material tempMaterial = new Material(GameObject.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = this.color;
            GameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
        else if(ColorType == "IMAGE") {
            ImageRef = obj.fills[0].imageRef;
            string imageUrl = apiImage;
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf(ImageRef));
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("https:"));
            imageUrl = imageUrl.Remove(imageUrl.IndexOf('"'));

            string contentType = APIService.ContentType(imageUrl);
            string path = $"Assets/Figma Converter/Images/{ImageRef}.{contentType}";
            
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
            AssetDatabase.CreateAsset(material, $"Assets/Figma Converter/Materials/{ImageRef}.mat");
            GameObject.GetComponent<Renderer>().material = material;
        }
    }
}