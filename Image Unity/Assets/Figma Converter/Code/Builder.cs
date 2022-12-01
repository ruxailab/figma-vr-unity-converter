using UnityEngine;
using UnityEditor;
using System.Text;

public class Builder {
    ObjectProperty obj;
    GameObject parent, gameObject;
    float width, height, eixoZ;
    int escala;
    string token, documentID;

    public Builder(ObjectProperty apiObj, GameObject parent, int escala, float eixoZ, string token, string documentID) {
        this.obj = apiObj;
        this.parent = parent;
        this.escala = escala;
        this.eixoZ = eixoZ;
        this.token = token;
        this.documentID = documentID;
    }

    public void createObject() {
        createCubo();
        setSize();
        setPosition();
        setImage(obj.id);
        try {
            if(obj.children.Length > 0) {
                for(int i = 0; i < obj.children.Length; i++) {
                    Builder objeto = new Builder(obj.children[i], gameObject, escala, eixoZ, token, documentID);
                    objeto.createObject();
                } 
            }
        } catch {}
        setName();
        setParent();
        Debug.Log(obj.name + " Criado com Sucesso");
    }

    public void createCubo() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    public void setSize() {
        width = obj.absoluteBoundingBox.width / escala;
        height = obj.absoluteBoundingBox.height / escala;
        Vector3 size = new Vector3(width, height, 0.01f);
        gameObject.transform.localScale = size;
    }

    public void setPosition() {
        float eixoX = (obj.absoluteBoundingBox.x/escala) + (width/2);
        float eixoY = (obj.absoluteBoundingBox.y/escala) + (height/2);
        Vector3 position = new Vector3(eixoX, eixoY, eixoZ);
        gameObject.transform.position = position;
    }

    public void setImage(string id) {
        string imageUrl = APIService.GetImages(token, documentID, id);
        string contentType = APIService.ContentType(imageUrl);
        string nameId = id.Replace(':', '-');
        string path = $"Assets/Figma Converter/Images/{nameId}.{contentType}";
        
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
        AssetDatabase.CreateAsset(material, $"Assets/Figma Converter/Materials/{nameId}.mat");
        gameObject.GetComponent<Renderer>().material = material;
    }

    private void setName() {
        gameObject.name = obj.name;
    }
    
    private void setParent() {
        gameObject.transform.parent = parent.transform;
    }
}