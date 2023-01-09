using UnityEngine;

public class Ellipse : Object {
    
    public Ellipse(ObjectProperty obj, float eixoZ, int escala) : base(obj, eixoZ, escala){}
    
    public GameObject createObject() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        setSize();
        setPosition();
        setColor();
        return gameObject;
    }
}