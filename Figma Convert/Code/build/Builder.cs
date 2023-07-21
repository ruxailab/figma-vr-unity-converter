using UnityEngine;
using System.Text;
// using System.Globalization;

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
            // CultureInfo cultureInfo = new CultureInfo("en-US");
            // Global.objPositionX = (float) (float.Parse(obj.componentPropertyDefinitions.PositionX.defaultValue, cultureInfo) * 0.5);
            // Global.objPositionY = (float) (float.Parse(obj.componentPropertyDefinitions.PositionY.defaultValue, cultureInfo) * -1 * 0.5);
            // Global.objPositionZ = (float) (float.Parse(obj.componentPropertyDefinitions.PositionZ.defaultValue, cultureInfo) * 0.5);
            // Global.objRotationX = (float) (float.Parse(obj.componentPropertyDefinitions.RotationX.defaultValue, cultureInfo) * -1 * 0.5);
            // Global.objRotationY = (float) (float.Parse(obj.componentPropertyDefinitions.RotationY.defaultValue, cultureInfo) * 0.5);
            gameObject = new Canva(obj, escala).createObject();
            gameObject.transform.Rotate(Global.objRotationX, Global.objRotationY, 0f, Space.World);
        }
        else {
            gameObject = new Painel(obj, escala).createObject();
        }

        if(gameObject == null)
            return;
        setName();
        setParent();
        // Debug.Log(obj.name + " Criado com Sucesso");
    }

    private void setName() {
        gameObject.name = obj.name;
    }
    
    private void setParent() {
        gameObject.transform.SetParent(parent.transform);
    }
}