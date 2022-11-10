using UnityEngine;

public class Frame : Objects {
    
    public Frame(ObjectProperty obj, string apiImage, int z, int escala) {
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
        for(int i=0; i < obj.children.Length; i++) {
            Build objeto = new Build(obj.children[i], apiImage, this.gameObject, (z+i+1), escala);
            objeto.createObject();
        }
        return this.gameObject;
    }
}