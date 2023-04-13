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
        else if(parent.name.Contains("Page")) {
            if(obj.componentPropertyDefinitions.PositionX != null) {
                Global.objPositionX = (float) (float.Parse(obj.componentPropertyDefinitions.PositionX.defaultValue) * 0.5);
                Global.objPositionY = (float) (float.Parse(obj.componentPropertyDefinitions.PositionY.defaultValue) * -1 * 0.5);
                Global.objPositionZ = (float) (float.Parse(obj.componentPropertyDefinitions.PositionZ.defaultValue) * 0.5);
                Global.objRotationX = (float) (float.Parse(obj.componentPropertyDefinitions.RotationX.defaultValue) * -1 * 0.5);
                Global.objRotationY = (float) (float.Parse(obj.componentPropertyDefinitions.RotationY.defaultValue) * 0.5);
            }
            else {
                Global.objPositionX = 0;
                Global.objPositionY = 0;
                Global.objPositionZ = 0;
                Global.objRotationX = 0;
                Global.objRotationY = 0;
            }
            gameObject = new Canva(obj, escala).createObject();
            gameObject.transform.Rotate(Global.objRotationX, Global.objRotationY, 0f, Space.World);
        }
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