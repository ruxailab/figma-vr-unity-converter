using UnityEditor;
using UnityEngine;
using System.Text;
// using System.Globalization;

public class Builder {
    private ObjectProperty obj;
    private GameObject parent;
    private int escala;
    private GameObject gameObject;

    public Builder(ObjectProperty apiObj, GameObject parent, int escala) {
        this.obj = apiObj;
        this.parent = parent;
        this.escala = escala;
    }

    public void createObject() {
        if(!obj.visible)
           return;
        
        if(obj.type == "TEXT")
            gameObject = new Text(obj, escala).createObject();
        else if(parent.name.Contains("Page")) {
            gameObject = new Canva(obj, escala).createObject();
            gameObject.transform.Rotate(Global.objRotationX, Global.objRotationY, 0f, Space.World);
        }
        else {
            gameObject = new Painel(obj, escala).createObject();
        }

        if(gameObject == null)
            return;

        addTag();
        setName();
        setParent();
    }

    private void setName() {
        gameObject.name = obj.name;
    }
    
    private void setParent() {
        gameObject.transform.SetParent(parent.transform);
    }

    private void addTag() {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
        SerializedProperty tag = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
        tag.stringValue = obj.id;
        tagManager.ApplyModifiedProperties();

        gameObject.tag = obj.id;
    }
}