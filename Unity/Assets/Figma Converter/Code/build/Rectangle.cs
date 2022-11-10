using UnityEngine;

public class Rectangle : Objects {

    public Rectangle(ObjectProperty obj, string apiImage, int z, int escala) {
        this.obj = obj;
        this.apiImage = apiImage;
        this.z = z;
        this.escala = escala;
    }
    
    public GameObject createObject(){
        this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setColor();
        return this.gameObject;
    }
}