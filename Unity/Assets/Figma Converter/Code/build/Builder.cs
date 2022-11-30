using UnityEngine;
using System.Text;

public class Builder {
    private ObjectProperty obj;
    private string apiImage;
    private GameObject parent;
    private float z;
    private int escala;
    private GameObject gameObject;

    public Builder(ObjectProperty apiObj, string apiImage, GameObject parent, float z, int escala) {
        this.obj = apiObj;
        this.apiImage = apiImage;
        this.parent = parent;
        this.z = z;
        this.escala = escala;
    }

    public void createObject() {
        switch (obj.type) {
            case "FRAME":
                gameObject = new Frame(obj, apiImage, z, escala).createObject();
                break;
            case "TEXT":
                gameObject = new Text(obj, apiImage, z).createObject();
                break;
            case "RECTANGLE":
                gameObject = new Rectangle(obj, apiImage, z, escala).createObject();
                break;
            case "ELLIPSE":
                gameObject = new Ellipse(obj, apiImage, z, escala).createObject();
                break;
        }
        setName();
        // setParent();
        Debug.Log(obj.name + " Criado com Sucesso");
    }

    private void setName() {
        gameObject.name = obj.name;
    }
    
    private void setParent() {
        gameObject.transform.parent = parent.transform;
    }
}