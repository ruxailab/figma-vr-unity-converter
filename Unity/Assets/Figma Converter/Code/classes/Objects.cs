using UnityEngine;
using UnityEditor;

public class Objects {
    public ObjectProperty obj;
    public string apiImage;
    public int z;
    public int escala;
    private string type;
    private float width;
    private float height;
    private Vector3 size;
    private float x;
    private float y;
    private Vector3 position;
    private string colorType;
    private string imageRef;
    private Color32 color;
    public GameObject gameObject;

    public void setSize() {
        this.width = obj.absoluteBoundingBox.width/this.escala;
        this.height = obj.absoluteBoundingBox.height/this.escala;
        this.size = new Vector3(this.width, this.height, 1);
        this.gameObject.transform.localScale = this.size;
    }

    public void setPosition() {
        this.x = (obj.absoluteBoundingBox.x/escala) + (this.width/2);
        this.y = (obj.absoluteBoundingBox.y/escala) + (this.height/2);
        this.position = new Vector3(this.x, this.y, (float)(0.01*z));
        this.gameObject.transform.position = this.position;
    }

    public void setColor() {
        this.colorType = obj.fills[0].type;
        if(this.colorType == "SOLID") {
            float r = obj.fills[0].color.r;
            float g = obj.fills[0].color.g;
            float b = obj.fills[0].color.b;
            float a = obj.fills[0].color.a;
            this.color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));

            // Variavel teporaria para inserir a cor do Objeto
            Material tempMaterial = new Material(this.gameObject.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = this.color;
            this.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
        else if(this.colorType == "IMAGE") {
            this.imageRef = obj.fills[0].imageRef;
            string imageUrl = apiImage;
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf(this.imageRef));
            imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("https:"));
            imageUrl = imageUrl.Remove(imageUrl.IndexOf('"'));

            string contentType = APIService.ContentType(imageUrl);
            string path = $"Assets/FigmaConverter/Images/{imageRef}.{contentType}";
            
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
            System.IO.Directory.CreateDirectory("Assets/FigmaConverter/Materials");
            AssetDatabase.CreateAsset(material, $"Assets/FigmaConverter/Materials/{imageRef}.mat");
            this.gameObject.GetComponent<Renderer>().material = material;
        }
    }
}