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
        Vector3 size = new Vector3(width, height, (float)0.1f);
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
        int subMeshCount = gameObject.GetComponent<MeshFilter>().sharedMesh.subMeshCount;
        int i = obj.fills.Length;
        string colorType = "";
        float r = 0, g = 0, b = 0, a = 0;
        
        if(i != 0)
            colorType = obj.fills[0].type;

        if(subMeshCount == 1) {
            if(i == 0 || colorType == "SOLID") {
                Material materialBody = setColorBody(r, g, b, a);
                renderer.sharedMaterial = materialBody;
                return;
            }
            else if(colorType == "IMAGE") {
                setImage(renderer);
                return;
            }
        }
        if(i == 0 || colorType == "SOLID") {
            Material materialBody = setColorBody(r, g, b, a);
            Material materialBorder = setColorBorder(r, g, b, a);

            Material[] tempMaterials = renderer.sharedMaterials;
            tempMaterials[0] = materialBody;
            tempMaterials[1] = materialBorder;
            renderer.sharedMaterials = tempMaterials;
        }
    }

    public Material setColorBody(float r, float g, float b, float a) {
        int i = obj.fills.Length;
        Color32 color;
        Material material;
        if(i == 0) {
            color = new Color32((byte)(255), (byte)(255), (byte)(255), (byte)(0.1*255));
            material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
            material.color = color;
            return material;
        }
        r = obj.fills[0].color.r;
        g = obj.fills[0].color.g;
        b = obj.fills[0].color.b;
        a = obj.fills[0].color.a;
        color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
        material = new Material(Shader.Find("Standard"));
        material.color = color;
        return material;
    }

    public Material setColorBorder(float r, float g, float b, float a) {
        r = obj.strokes[0].color.r;
        g = obj.strokes[0].color.g;
        b = obj.strokes[0].color.b;
        a = obj.strokes[0].color.a;
        Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
        Material material = new Material(Shader.Find("Standard"));
        material.color = color;
        return material;
    }

    public void setImage(Renderer renderer) {
        string imageRef = obj.fills[0].imageRef;
        string imageUrl = apiImage;
        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf(imageRef));
        imageUrl = imageUrl.Remove(0, imageUrl.IndexOf("https:"));
        imageUrl = imageUrl.Remove(imageUrl.IndexOf('"'));

        string contentType = APIService.ContentType(imageUrl);
        string path = $"Assets/Figma Convert/Images/{imageRef}.{contentType}";
            
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
        AssetDatabase.CreateAsset(material, $"Assets/Figma Convert/Materials/{imageRef}.mat");
        renderer.material = material;
    }
}