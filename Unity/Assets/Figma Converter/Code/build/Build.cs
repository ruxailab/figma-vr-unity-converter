using UnityEngine;
using UnityEditor;
using System.Text;

public class Build {
    private ObjectProperty obj;
    private string apiImage;
    private GameObject parent;
    private int z;
    private int escala;
    private GameObject gameObject;

    public Build(ObjectProperty apiObj, string apiImage, GameObject parent, int z, int escala) {
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
        gameObject.name = obj.name;
        setParent();
        Debug.Log(obj.name + " Criado com Sucesso");
    }



    private void setParent() {
        gameObject.transform.parent = parent.transform;
    }
}