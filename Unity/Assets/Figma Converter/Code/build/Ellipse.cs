using UnityEngine;

public class Ellipse : Objects {
    
    public Ellipse(ObjectProperty obj, string apiImage, int z, int escala) {
        this.obj = obj;
        this.apiImage = apiImage;
        this.z = z;
        this.escala = escala;
    }
    
    public GameObject createObject() {
        this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        setSize();
        setPosition();
        setColor();
        return this.gameObject;
    }
}