using UnityEngine;
using UnityEditor;
using System.Text;

public class Object {
    
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

    public Object(ChildrenObj apiObj, string apiImage, GameObject empty, int z){
        this.type = apiObj.type;
        if(this.type == "TEXT") {
            // Altura e Largura
            var width = apiObj.absoluteBoundingBox.width/100;
            var height = apiObj.absoluteBoundingBox.height/100;

            // Localização nos Eixos X e Y
            var x = (apiObj.absoluteBoundingBox.x/100) + (width/2); 
            var y = (api.document.children[0].children[j].absoluteBoundingBox.y/100) + (height/2);

            // Negrito e Italico
            var negrito = false;
            var italic = false;
            if(apiObj.style.fontWeight <= 700)
                negrito = true; 
            if(apiObj.style.italic == true)
                italic = true;

            // Criação do Objeto
            GameObject obj = new GameObject("3D Text");
            obj.transform.position = new Vector3(x,y,(float)(0.01*j));
            TextMesh textObj = obj.AddComponent<TextMesh>() as TextMesh;

            // Propriedades do Objeto
            textObj.text = apiObj.characters;
            textObj.fontSize = apiObj.style.fontSize/5;
            if(italic == true || negrito == true)
                textObj.fontStyle = FontStyle.BoldAndItalic;
            else if(negrito == true)
                textObj.fontStyle = FontStyle.Bold;
            else if(italic == true)
                textObj.fontStyle = FontStyle.Italic;
            Debug.Log("3DText Criado");
        }
        else if(this.type == "RECTANGLE")
            this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        else if(this.type == "ELLIPSE")
            this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        setColor(apiObj, apiImage);  // Define Cor
        setSize(apiObj);   // Define Altura e Largura
        setPosition(apiObj, z);  // Define Localização nos Eixos X e Y
        this.gameObject.transform.parent = empty.transform;
        
        Debug.Log("Objeto Criado com Sucesso");
        
    }
    
    private void setSize(ChildrenObj apiObj){
        this.width = apiObj.absoluteBoundingBox.width/100;
        this.height = apiObj.absoluteBoundingBox.height/100;
        this.size = new Vector3(this.width, this.height, 1);
        this.gameObject.transform.localScale = this.size;
    }

    private void setPosition(ChildrenObj apiObj,int z){
        this.x = (apiObj.absoluteBoundingBox.x/100) + (this.width/2);
        this.y = (apiObj.absoluteBoundingBox.y/100) + (this.height/2);
        this.position = new Vector3(this.x, this.y, (float)(0.01*z));
        this.gameObject.transform.position = this.position;
    }

    private void setColor(ChildrenObj apiObj, string apiImage){
        this.colorType = apiObj.fills[0].type;
        if(this.colorType == "SOLID"){
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
        else if(this.colorType == "IMAGE"){
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
            if(!System.IO.File.Exists(path)){
                Debug.Log("Erro no Arquivo de Material");
                return;
            }
            byte[] readImg = System.IO.File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(readImg);
            Material material = new Material(Shader.Find("Unlit/Texture"));
            material.mainTexture = tex;
            material.mainTextureScale = new Vector2(-1, -1);
            System.IO.Directory.CreateDirectory("Assets/FigmaConverter/Materials");
            AssetDatabase.CreateAsset(material, $"Assets/FigmaConverter/Materials/{imageRef}.mat");
            this.gameObject.GetComponent<Renderer>().material = material;
        }
    }
}