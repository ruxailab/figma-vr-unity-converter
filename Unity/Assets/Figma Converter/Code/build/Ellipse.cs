using UnityEngine;

public class Ellipse : Objects {
    
    public Ellipse(ObjectProperty obj, string apiImage, int z, int escala) : base(obj, apiImage, z, escala){}
    
    public GameObject createObject() {
        GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        setSize();
        setPosition();
        setColor();
        return GameObject;
    }
}