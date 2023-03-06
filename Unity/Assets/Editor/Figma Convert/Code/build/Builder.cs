using UnityEngine;
using System.Text;

public class Builder {
    private ObjectProperty obj;
    private GameObject parent;
    private int escala;
    private GameObject gameObject;

    public Builder(ObjectProperty apiObj, GameObject parent, int escala) {
        this.obj = apiObj;
        this.parent = parent;
        this.escala = escala;
    }

    public void createObject() {
        if(!obj.visible)
           return;
        
        if(obj.type == "TEXT")
            gameObject = new Text(obj, escala).createObject();
        else if(parent.name.Contains("Page"))
            gameObject = new Canva(obj, escala).createObject();
        else
            gameObject = new Painel(obj, escala).createObject();

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
        gameObject.transform.SetParent(parent.transform);
    }
}