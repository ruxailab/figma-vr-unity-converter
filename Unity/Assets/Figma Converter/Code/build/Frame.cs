using UnityEngine;

public class Frame : Object {
    
    public Frame(ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){}

    public GameObject createObject(){
        cornerRadius();
        setSize();
        setPosition();
        setColor();
        float z = 0;
        for(int i = 0; i < obj.children.Length; i++, z += 0.1f) {
            Builder objeto = new Builder(obj.children[i], apiImage, gameObject, (eixoZ+z), escala);
            objeto.createObject();
        }
        return gameObject;
    }

    public void cornerRadius() {
        if(obj.cornerRadius == 0){
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            return;
        }
        Create create = new Create();
        gameObject = create.createCubo((obj.cornerRadius/10));
        escalaRadius = 10;
    }
}