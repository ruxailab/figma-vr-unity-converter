using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.UI;
using TMPro;

public class Canva : Object {

    public Canva(ObjectProperty obj, int escala) : base(obj, escala) {}

    public GameObject createObject() {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        Global.objPositionX = (float) (float.Parse(obj.componentPropertyDefinitions.PositionX.defaultValue, cultureInfo) * 0.5);
        Global.objPositionY = (float) (float.Parse(obj.componentPropertyDefinitions.PositionY.defaultValue, cultureInfo) * -1 * 0.5);
        Global.objPositionZ = (float) (float.Parse(obj.componentPropertyDefinitions.PositionZ.defaultValue, cultureInfo) * 0.5);
        Global.objRotationX = (float) (float.Parse(obj.componentPropertyDefinitions.RotationX.defaultValue, cultureInfo) * -1 * 0.5);
        Global.objRotationY = (float) (float.Parse(obj.componentPropertyDefinitions.RotationY.defaultValue, cultureInfo) * 0.5);
        Global.objAbsoluteX = obj.absoluteBoundingBox.x / escala;
        Global.objAbsoluteY = obj.absoluteBoundingBox.y / escala;
        Global.objWidth = obj.absoluteBoundingBox.width / escala;
        Global.objHeigth = obj.absoluteBoundingBox.height / escala;

        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Canvas>();
        gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();

        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        setSize(rectTransform);
        rectTransform.localPosition = new Vector3(Global.objPositionX, Global.objPositionY, Global.objPositionZ);
        
        GameObject painel = new Painel(obj, escala).createObject();
        painel.name = obj.name;
        painel.transform.SetParent(gameObject.transform);
        
        setRotation(rectTransform);
        if(obj.componentPropertyDefinitions.Visible != null) {
            canvas.enabled = bool.Parse(obj.componentPropertyDefinitions.Visible.defaultValue);
        }

        Debug.Log("oi");
        Debug.Log(Global.transitionNodeID.Count);

        foreach (string nodeId in Global.transitionNodeID) {   
            Debug.Log(nodeId);
            Debug.Log(obj.id);
            if(nodeId == obj.id) {
                canvas.enabled = false;
            }
        }

        return gameObject;
    }
}