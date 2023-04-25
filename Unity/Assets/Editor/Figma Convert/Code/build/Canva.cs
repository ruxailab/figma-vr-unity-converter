using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Canva : Object {

    public Canva(ObjectProperty obj, int escala) : base(obj, escala) {}

    public GameObject createObject() {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Canvas>();

        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        setSize(rectTransform);
        setPosition(rectTransform);
        
        GameObject painel = new Painel(obj, escala).createObject();
        painel.name = obj.name;
        painel.transform.SetParent(gameObject.transform);
        setPositionCanva(rectTransform);
        setRotation(rectTransform);
        return gameObject;
    }

    public void setPositionCanva(RectTransform rectTransform) {
        eixoX = Global.objPositionX;
        eixoY = Global.objPositionY;
        eixoZ = Global.objPositionZ;
        rectTransform.localPosition = new Vector3(eixoX, eixoY, eixoZ);
    }
}