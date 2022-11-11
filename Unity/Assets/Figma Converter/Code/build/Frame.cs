using UnityEngine;

public class Frame : Objects {
    
    public Frame(ObjectProperty obj, string apiImage, int z, int escala) : base(obj, apiImage, z, escala){}

    public GameObject createObject(){
        GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setSize();
        setPosition();
        setColor();
        for(int i=0; i < Obj.children.Length; i++) {
            Build objeto = new Build(Obj.children[i], ApiImage, GameObject, (Z+i+1), Escala);
            objeto.createObject();
        }
        return GameObject;
    }
}