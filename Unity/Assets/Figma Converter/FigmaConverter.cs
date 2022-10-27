using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class FigmaConverter : EditorWindow {

    string token;
    string documentID;

    [MenuItem("Tools/Figma Converter")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<FigmaConverter>("Figma Converter");  //Title da ferramenta
    }

    private void OnGUI() {
        GUILayout.Label("Settings", EditorStyles.label);  //Escrita
        token = EditorGUILayout.TextField("Enter Token:", token);
        documentID = EditorGUILayout.TextField("Enter URL Document:", documentID);
        EditorGUILayout.Space();

        if(GUILayout.Button("Apply")) {
            int cont = 0;
            for(int i = documentID.Length-1; i > 0; i--){
                if(documentID[i] == '/'){
                    cont++;
                    if(cont == 1)
                        documentID = documentID.Remove(i);
                    else if(cont == 2){
                        documentID = documentID.Remove(0,i);
;

                    /*
                    else if(apiObj.type == "TEXT"){
                        // Altura e Largura
                        var width = apiObj.absoluteBoundingBox.width/100;
                        var height = apiObj.absoluteBoundingBox.height/100;

                        // Localização nos Eixos X e Y
                        var x = (apiObj.absoluteBoundingBox.x/100) + (width/2); 
                        var y = (api.document.children[0].children[j].absoluteBoundingBox.y/100) + (height/2);

                        // Negrito e Italico
                        var negrito = false;
                        var italic = false;
                        if(apiObj.style.fontWeight <= 700){
                            negrito = true;
                        } 
                        if(apiObj.style.italic == true){
                            italic = true;
                        }

                        // Criação do Objeto
                        GameObject obj = new GameObject("3D Text");
                        obj.transform.position = new Vector3(x,y,(float)(0.01*j));
                        TextMesh textObj = obj.AddComponent<TextMesh>() as TextMesh;

                        // Propriedades do Objeto
                        textObj.text = apiObj.characters;
                        textObj.fontSize = apiObj.style.fontSize/5;
                        if(italic == true || negrito == true){
                            textObj.fontStyle = FontStyle.BoldAndItalic;
                        }
                        else if(negrito == true){
                            textObj.fontStyle = FontStyle.Bold;
                        }
                        else if(italic == true){
                            textObj.fontStyle = FontStyle.Italic;
                        }
                        Debug.Log("3DText Criado");
                    }*/
                }
                empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
            }
        }
    }
}