using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoreTest {
    public static void Start(int escala){
        GameObject myGO;
        GameObject myText;
        GameObject myPainel;
        Canvas myCanvas;
        TextMeshProUGUI text;
        RectTransform rectTransform;
        Image painel;

        // Canvas
        myGO = new GameObject();
        myGO.name = "TestCanvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.WorldSpace;
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "wibble";

        
        text = myText.AddComponent<TextMeshProUGUI>();
        text.text = "wobble";
        text.fontSize = 18;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(18, 0, 0);
        rectTransform.sizeDelta = new Vector2(10, 100);

        // Painel
        myPainel = new GameObject();
        myPainel.transform.parent = myGO.transform;
        myPainel.name = "Painel";

        painel = myPainel.AddComponent<Image>(); 

    }
}