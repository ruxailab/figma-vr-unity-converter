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
        setPosition(rectTransform);
        setColorText();
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
        width = obj.absoluteBoundingBox.width;
        height = obj.absoluteBoundingBox.height;
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

    public void setColorText() {}
}