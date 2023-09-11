using UnityEngine;
// using UnityEngine.UI;
using TMPro;    

public class Text : Object {

    public Text(ObjectProperty obj, int escala) : base(obj, escala){}

    public GameObject createObject() {
        gameObject = new GameObject();
        TextMeshProUGUI text = gameObject.AddComponent<TextMeshProUGUI>();
        setCharacters(text);
        setFontSize(text);
        italicAndNegrito(text);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        setSizeFont();
        setSizeRectFont(rectTransform);
        obj.absoluteBoundingBox.x += 10;
        setPosition(rectTransform);
        setColorText(text);
        gameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
        return gameObject;
    }

    public void setCharacters(TextMeshProUGUI text) {
        text.text = obj.characters;
    }

    public void setFontSize(TextMeshProUGUI text) {
        text.fontSize = obj.style.fontSize;
    }

    public void setSizeFont() {
        width = (float)1/escala;
        height = (float)1/escala;
        Vector3 size = new Vector3(width, height, 1);
        gameObject.transform.localScale = size;
    }

    public void setSizeRectFont(RectTransform rectTransform) {
        width = obj.absoluteBoundingBox.width + 1;
        height = obj.absoluteBoundingBox.height + 1;
        rectTransform.sizeDelta = new Vector2(width, height);
        width = width/escala;
        height = height/escala;
    }

    public void italicAndNegrito(TextMeshProUGUI text) {
        /*if(obj.style.italic == true && obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.BoldAndItalic;
        else if(obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.Bold;
        else if(obj.style.italic == true)
            textMesh.fontStyle = FontStyle.Italic;*/
    }

    public void setColorText(TextMeshProUGUI text) {
        if(obj.fills.Length != 1 && obj.fills[0].type != "SOLID") { return; }

        float r = obj.fills[0].color.r;
        float g = obj.fills[0].color.g;
        float b = obj.fills[0].color.b;
        float a = obj.fills[0].color.a;
        Color32 color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
        text.color = color;
    }
}