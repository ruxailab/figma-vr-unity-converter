using UnityEngine;

public class Text : Object {

    public Text(ObjectProperty obj, string apiImage, float eixoZ) : base(obj, apiImage, eixoZ, 0){}

    public GameObject createObject() {
        gameObject = new GameObject("3D Text");
        TextMesh textMesh = gameObject.AddComponent<TextMesh>() as TextMesh;
        setCharacters(textMesh);
        setFontSize(textMesh);
        italicAndNegrito(textMesh);
        gameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
        setPosition();
        setColor();
        return gameObject;
    }

    public void italicAndNegrito(TextMesh textMesh) {
        if(obj.style.italic == true && obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.BoldAndItalic;
        else if(obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.Bold;
        else if(obj.style.italic == true)
            textMesh.fontStyle = FontStyle.Italic;
    }

    public void setFontSize(TextMesh textMesh) {
        textMesh.fontSize = obj.style.fontSize;
    }

    public void setCharacters(TextMesh textMesh) {
        textMesh.text = obj.characters;
    }
}