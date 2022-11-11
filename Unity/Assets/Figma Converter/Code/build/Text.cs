using UnityEngine;

public class Text : Objects{

    public Text(ObjectProperty obj, string apiImage, int z) : base(obj, apiImage, z, 0){}
    public GameObject createObject() {
        GameObject = new GameObject("3D Text");
        TextMesh textMesh = GameObject.AddComponent<TextMesh>() as TextMesh;
        setCharacters(textMesh);
        setFontSize(textMesh);
        italicAndNegrito(textMesh);
        GameObject.transform.Rotate(180.0f, 0f, 0f, Space.World);
        setPosition();
        setColor();
        return GameObject;
    }

    public void italicAndNegrito(TextMesh textMesh) {
        if(Obj.style.italic == true && Obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.BoldAndItalic;
        else if(Obj.style.fontWeight >= 700)
            textMesh.fontStyle = FontStyle.Bold;
        else if(Obj.style.italic == true)
            textMesh.fontStyle = FontStyle.Italic;
    }

    public void setFontSize(TextMesh textMesh) {
        textMesh.fontSize = Obj.style.fontSize;
    }

    public void setCharacters(TextMesh textMesh) {
        textMesh.text = Obj.characters;
    }
}