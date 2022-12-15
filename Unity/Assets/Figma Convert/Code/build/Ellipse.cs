using UnityEngine;

public class Ellipse : Object {
    
    public Ellipse(ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){}
    
    public GameObject createObject() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        setSize();
        setPosition();
        setColor();
        return gameObject;
    }
}