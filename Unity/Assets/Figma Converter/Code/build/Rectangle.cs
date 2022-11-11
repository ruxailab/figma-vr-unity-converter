using UnityEngine;

public class Rectangle : Objects {

    public Rectangle(ObjectProperty obj, string apiImage, int z, int escala) : base(obj, apiImage, z, escala){}
    
    public GameObject createObject(){
        GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setColor();
        return GameObject;
    }
}