using UnityEngine;
using System.Text;

public class Builder {
    private ObjectProperty obj;
    private GameObject parent;
    private float z;
    private int escala;
    private GameObject gameObject;

    public Builder(ObjectProperty apiObj, GameObject parent, float z, int escala) {
        this.obj = apiObj;
        this.parent = parent;
        this.z = z;
        this.escala = escala;
    }

    public void createObject() {
        switch (obj.type) {
            /*case "FRAME":
                gameObject = new Frame(obj, z, escala).createObject();
                break;
            case "COMPONENT":
                gameObject = new Frame(obj, z, escala).createObject();
                break;
            case "INSTANCE":
                gameObject = new Frame(obj, z, escala).createObject();
                break;
            case "GROUP":
                gameObject = new Frame(obj, z, escala).createObject();
                break;
            case "TEXT":
                gameObject = new Text(obj, z).createObject();
                break;**/
            case "RECTANGLE":
                gameObject = new Rectangle(obj, z, escala).createObject();
                break;
            /*case "VECTOR":
                gameObject = new Vector(obj, z, escala).createObject();
                break;
            case "ELLIPSE":
                gameObject = new Ellipse(obj, z, escala).createObject();
                break;*/
        }
        if(gameObject == null)
            return;
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