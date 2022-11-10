using UnityEngine;
using UnityEditor;
using System.Text;

public class Object {
    private int escala;
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
    private GameObject gameObject;

    public Object(ObjectProperty apiObj, string apiImage, GameObject empty, int z, int escala) {
        this.escala = escala;
        this.type = apiObj.type;

        switch (this.type) {
            case "FRAME":
                this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                setSize(apiObj);
                setPosition(apiObj, z);
                for(int i=0; i < apiObj.children.Length; i++) {
                    Object obj = new Object(apiObj.children[i], apiImage, this.gameObject, (z+i+1), escala);
                }
                break;
            case "TEXT":
                createText(apiObj);
                break;
            case "RECTANGLE":
                this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                setSize(apiObj);
                setPosition(apiObj, z);
                break;
            case "ELLIPSE":
                this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                setSize(apiObj);
                setPosition(apiObj, z);
                break;
        }
        setColor(apiObj, apiImage);  // Define Cor e Textura
        this.gameObject.name = apiObj.name;
        this.gameObject.transform.parent = empty.transform;
        Debug.Log(apiObj.name + " Criado com Sucesso");
    }

    private void createText(ObjectProperty apiObj) {
        this.gameObject = new GameObject("3D Text");
        TextMesh textObj = this.gameObject.AddComponent<TextMesh>() as TextMesh;
        textObj.text = apiObj.characters;
        textObj.fontSize = apiObj.style.fontSize;
        if(apiObj.style.italic == true && apiObj.style.fontWeight >= 700)
            textObj.fontStyle = FontStyle.BoldAndItalic;
        else if(apiObj.style.fontWeight >= 700)
            textObj.fontStyle = FontStyle.Bold;
        else if(apiObj.style.italic == true)
            textObj.fontStyle = FontStyle.Italic;
        this.gameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
    }
    
    private void setSize(ObjectProperty apiObj) {
        this.width = apiObj.absoluteBoundingBox.width/this.escala;
        this.height = apiObj.absoluteBoundingBox.height/this.escala;
        this.size = new Vector3(this.width, this.height, 1);
        this.gameObject.transform.localScale = this.size;
    }

    private void setPosition(ObjectProperty apiObj,int z) {
        this.x = (apiObj.absoluteBoundingBox.x/escala) + (this.width/2);
        this.y = (apiObj.absoluteBoundingBox.y/escala) + (this.height/2);
        this.position = new Vector3(this.x, this.y, (float)(0.01*z));
        this.gameObject.transform.position = this.position;
    }

    private void setColor(ObjectProperty apiObj, string apiImage) {
        this.colorType = apiObj.fills[0].type;
        if(this.colorType == "SOLID") {
            float r = apiObj.fills[0].color.r;
            float g = apiObj.fills[0].color.g;
            float b = apiObj.fills[0].color.b;
            float a = apiObj.fills[0].color.a;
            this.color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));

            // Variavel teporaria para inserir a cor do Objeto
            Material tempMaterial = new Material(this.gameObject.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = this.color;
            this.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
        else if(this.colorType == "IMAGE") {
            this.imageRef = apiObj.fills[0].imageRef;
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