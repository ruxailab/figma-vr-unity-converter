using UnityEngine;

public class Rectangle : Object {

    public Rectangle(ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){}
    
    public GameObject createObject(){
        cornerRadius();
        setSize();
        setPosition();
        setColor();
        return gameObject;
    }

    public void cornerRadius() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        return;
    }
}