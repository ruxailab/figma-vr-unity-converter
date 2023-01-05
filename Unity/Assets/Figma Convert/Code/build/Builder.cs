using UnityEngine;
using System.Text;

public class Builder {
    private string documentID;
    private string token;
    private ObjectProperty obj;
    private string apiImage;
    private GameObject parent;
    private float z;
    private int escala;
    private GameObject gameObject;

    public Builder(string documentID, string token, ObjectProperty apiObj, string apiImage, GameObject parent, float z, int escala) {
        this.documentID = documentID;
        this.token = token;
        this.obj = apiObj;
        this.apiImage = apiImage;
        this.parent = parent;
        this.z = z;
        this.escala = escala;
    }

    public void createObject() {
        switch (obj.type) {
            case "FRAME":
                gameObject = new Frame(documentID, token, obj, apiImage, z, escala).createObject();
                break;
            case "COMPONENT":
                gameObject = new Frame(documentID, token, obj, apiImage, z, escala).createObject();
                break;
            case "INSTANCE":
                gameObject = new Frame(documentID, token, obj, apiImage, z, escala).createObject();
                break;
            case "GRUP":
                gameObject = new Frame(documentID, token, obj, apiImage, z, escala).createObject();
                break;
            case "TEXT":
                gameObject = new Text(obj, apiImage, z).createObject();
                break;
            case "RECTANGLE":
                gameObject = new Rectangle(obj, apiImage, z, escala).createObject();
                break;
            case "VECTOR":
                gameObject = new Vector(documentID, token, obj, apiImage, z, escala).createObject();
                if(gameObject == null)
                    return;
                else
                    break;
            case "ELLIPSE":
                gameObject = new Ellipse(obj, apiImage, z, escala).createObject();
                break;
        }
        setName();
        setParent();
        Debug.Log(obj.name + " Criado com Sucesso");
    }

    private void setName() {
        gameObject.name = obj.name;
    }
    
    private void setParent() {
        gameObject.transform.parent = parent.transform;
    }
}