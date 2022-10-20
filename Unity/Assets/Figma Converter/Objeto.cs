using UnityEngine;
using UnityEditor;

public class Object{
    
    private string type;
    private float width;
    private float height;
    private Vector3 size;
    private float x;
    private float y;
    private Vector3 position;
    private string colorType;
    private Color32 color;
    private GameObject gameObject;

    public Object(ChildrenObj apiObj, GameObject empty, int z){
        this.type = apiObj.type;
        setSize(apiObj);  // Altura e Largura
        setPosition(apiObj, z);  // Localização nos Eixos X e Y
        setColor(apiObj);  // Color
        createObject(empty);  // Criação do Objeto
    }
    
    private void setSize(ChildrenObj apiObj){
        this.width = apiObj.absoluteBoundingBox.width/100;
        this.height = apiObj.absoluteBoundingBox.height/100;
        this.size = new Vector3(this.width, this.height, 1);
    }

    private void setPosition(ChildrenObj apiObj,int z){
        this.x = (apiObj.absoluteBoundingBox.x/100) + (this.width/2);
        this.y = (apiObj.absoluteBoundingBox.y/100) + (this.height/2);
        this.position = new Vector3(this.x, this.y, (float)(0.01*z));
    }

    private void setColor(ChildrenObj apiObj){
        float r = apiObj.fills[0].color.r;
        float g = apiObj.fills[0].color.g;
        float b = apiObj.fills[0].color.b;
        float a = apiObj.fills[0].color.a;
        this.color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
    }

    private void createObject (GameObject empty){
        
        // Verifica se Objeto é um Retangulo
        if(this.type == "RECTANGLE"){
            this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else if(this.type == "ELLIPSE"){
            this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        this.gameObject.transform.position = this.position;
        this.gameObject.transform.localScale = this.size;
        this.gameObject.transform.parent = empty.transform;

        // Variavel teporaria para inserir a cor do Objeto
        Material tempMaterial = new Material(this.gameObject.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = this.color;
        this.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        Debug.Log("Objeto Criado com Sucesso");
    }
}