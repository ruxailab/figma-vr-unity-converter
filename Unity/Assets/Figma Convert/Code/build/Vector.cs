using UnityEngine;
using UnityEditor;

public class Vector : Object {

    public Vector(ObjectProperty obj, float eixoZ, int escala) : base(obj, eixoZ, escala){}
    
    public GameObject createObject(){
        if(vertorExist())
            return null;
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setSVG();
        return gameObject;
    }

    public bool vertorExist() {
        string id = obj.id.Remove(obj.id.IndexOf(';'));
        id = id.Remove(id.IndexOf(":"));
        if(UnityEngine.Windows.File.Exists($"Assets/Figma Convert/Materials/{id}.mat"))
            return true;
        else
         return false;
    }

    public void setSVG() {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        setImage(renderer);
    }
}