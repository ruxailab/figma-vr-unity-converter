using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

class WindowTest : EditorWindow {

    int escala = 100;
    string token;
    string documentID;
    
    [MenuItem("Tools/Test")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<WindowTest>("Test");  //Title da ferramenta
    }

    private void OnGUI() {
        // GUILayout.Label("Settings", EditorStyles.label);  //Escrita
        // escala = EditorGUILayout.IntField("Enter Scale:", escala);
        // EditorGUILayout.Space();
        
        // GUILayout.Label("Account", EditorStyles.label);  //Escrita
        // Global.token = token = EditorGUILayout.TextField("Enter Token:", token);
        // Global.documentID = documentID = EditorGUILayout.TextField("Enter URL Document:", documentID);
        // EditorGUILayout.Space();

        if(GUILayout.Button("Download Projet")) {
            // try {
                CoreTest.Start(escala);
            // }
            // catch (Exception e){
                // Debug.Log(e);
            // }
        }
    }
}