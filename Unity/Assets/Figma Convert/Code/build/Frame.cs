using UnityEngine;

public class Frame : Object {

    public Frame(ObjectProperty obj, string apiImage, float eixoZ, int escala) : base(obj, apiImage, eixoZ, escala){}

    public GameObject createObject() {
        bool isCorner = cornerRadius();
        if(isCorner)
            setSizeCorner();
        else
            setSize();
        setPosition();
        setColor();
        float z = 0.1f;
        for(int i = 0; i < obj.children.Length; i++, z += 0.1f) {
            Builder objeto = new Builder(obj.children[i], apiImage, gameObject, (eixoZ+z), escala);
            objeto.createObject();
        }
        return gameObject;
    }

    public bool cornerRadius() {
        if(obj.cornerRadius == 0){
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            return false;
        }
        Create create = new Create(obj.cornerRadius, obj.strokeWeight-1, obj.absoluteBoundingBox.height, obj.absoluteBoundingBox.width);
        gameObject = create.createCubo();
        return true;
    }

    public void setSizeCorner() {
        width = (float) 1 / escala;
        height = (float) 1 / escala;
        Vector3 size = new Vector3(width, height, (float)1.0/escala);
        gameObject.transform.localScale = size;
    }
}