using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class FigmaConverter : EditorWindow {

    int escala;
    string token;
    string documentID;

    [MenuItem("Tools/Figma Converter")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<FigmaConverter>("Figma Converter");  //Title da ferramenta
    }

    private void OnGUI() {
        GUILayout.Label("Settings", EditorStyles.label);  //Escrita
        escala = EditorGUILayout.IntField("Enter Scale:", escala);
        EditorGUILayout.Space();
        
        GUILayout.Label("Account", EditorStyles.label);  //Escrita
        token = EditorGUILayout.TextField("Enter Token:", token);
        documentID = EditorGUILayout.TextField("Enter URL Document:", documentID);
        EditorGUILayout.Space();

        if(GUILayout.Button("Apply")) {
            int cont = 0;
            for(int i = documentID.Length-1; i > 0; i--) {
                if(documentID[i] == '/') {
                    cont++;
                    if(cont == 1)
                        documentID = documentID.Remove(i);
                    else if(cont == 2){
                        documentID = documentID.Remove(0,i);
                        break;
                    }
                }
            }
            
            // Chamada da API
            File apiDocument = APIService.GetDocument(token, documentID);
            string apiImage = APIService.GetImages(token, documentID);

            // Loop Pagina
            for(int i = 0; i<apiDocument.document.children.Length; i++) {
                
                GameObject empty = new GameObject("Page " + (i+1));
                
                // Loop Objeto
                for(int j = 0; j<apiDocument.document.children[i].children.Length; j++) {
                    ChildrenObj apiObj = apiDocument.document.children[i].children[j];
                    Object obj = new Object(apiObj, apiImage, empty, j, escala);
                }
                empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
            }
        }
    }
}