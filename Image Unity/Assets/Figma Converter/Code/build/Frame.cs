using UnityEngine;

public class Frame : Object {

    public Frame(ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){}

    public GameObject createObject(){
        cornerRadius();
        setSize();
        setPosition();
        setColor();
        float z = 0.3f;
        for(int i = 0; i < obj.children.Length; i++, z += 0.3f) {
            // Builder objeto = new Builder(obj.children[i], apiImage, gameObject, (eixoZ+z), escala);
            // objeto.createObject();
        }
        return gameObject;
    }

    public void cornerRadius() {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        return;
    }
}