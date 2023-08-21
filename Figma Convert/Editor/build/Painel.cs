using UnityEngine;
using UnityEngine.UI;

public class Painel : Object {

    public Painel(ObjectProperty obj, int escala) : base(obj, escala) {}

    public GameObject createObject() {
        gameObject = new GameObject();
        Image painel = gameObject.AddComponent<Image>();
        RectTransform rectTransform = painel.GetComponent<RectTransform>();
        setSize(rectTransform);
        setPosition(rectTransform);
        setColor(painel);
        setCornerRadius();
        setBorder();

        if(obj.children != null) {
            for(int i = 0; i < obj.children.Length; i++) {
                Builder objeto = new Builder(obj.children[i], gameObject, escala);
                objeto.createObject();
            }
        }

        if(obj.transitionNodeID != null) {
            Prototype prototype = gameObject.AddComponent<Prototype>();
            prototype.transitionNodeID = obj.transitionNodeID;
        }

        return gameObject;
    }

}