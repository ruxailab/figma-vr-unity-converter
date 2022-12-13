using UnityEngine;
using UnityEditor;

public abstract class Object {
    public ObjectProperty obj;
    public string apiImage;
    public float eixoX;
    public float eixoY;
    public float eixoZ;
    public int escala;
    public float width;
    public float height;
    public GameObject gameObject;
    public int escalaRadius = 1;

    public Object(ObjectProperty obj, string apiImage, float eixoZ, int escala) {
        this.obj = obj;
        this.apiImage = apiImage;
        this.eixoZ = eixoZ;
        this.escala = escala;
    }

    public void setSize() {
        width = (obj.absoluteBoundingBox.width/escala) / escalaRadius;
        height = (obj.absoluteBoundingBox.height/escala) / escalaRadius;
        Vector3 size = new Vector3(width, height, (float)1.0f/escalaRadius);
        gameObject.transform.localScale = size;
    }

    public void setPosition() {
        eixoX = (obj.absoluteBoundingBox.x/escala) + (width/2);
        eixoY = (obj.absoluteBoundingBox.y/escala) + (height/2);
        Vector3 position = new Vector3(eixoX, eixoY, eixoZ);
        gameObject.transform.position = position;
    }

    public void setColor() {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        int i = obj.fills.Length;
        if(i == 0) {
            renderer.enabled = false;
            return;
        }
        int subMeshCount = gameObject.GetComponent<MeshFilter>().sharedMesh.subMeshCount;
        string colorType = obj.fills[0].type;
        if(colorType == "SOLID") {
            float r = obj.fills[0].color.r;
            float g = obj.fills[0].color.g;
            float b = obj.fills[0].color.b;
            float a = obj.fills[0].color.a;
            Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));

            if(subMeshCount == 1){
                Material tempColor = new Material(renderer.sharedMaterial);
                tempColor.color = color;
                renderer.sharedMaterial = tempColor;
                return;
            }

            r = obj.strokes[0].color.r;
            g = obj.strokes[0].color.g;
            b = obj.strokes[0].color.b;
            a = obj.strokes[0].color.a;
            Color32 colorBorder = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
            
            Material[] tempMaterials = renderer.sharedMaterials;
            Material materialColor = new Material(Shader.Find("Standard"));
            materialColor.color = color;
            Material materialBorder = new Material(Shader.Find("Standard"));
            materialBorder.color = colorBorder;

            tempMaterials[0] = materialColor;
            tempMaterials[1] = materialBorder;
            renderer.sharedMaterials = tempMaterials;
        }
        else if(colorType == "IMAGE") {
            setImage(renderer);
        }
    }

    public void setImage(Renderer renderer) {
        string imageRef = obj.fills[0].imageRef;
        string imageUrl = apiImage;
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
        renderer.material = material;
    }
}