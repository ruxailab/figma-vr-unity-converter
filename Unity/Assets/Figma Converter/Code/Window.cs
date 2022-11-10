using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Window : EditorWindow {

    int escala;
    string token;
    string documentID;

    [MenuItem("Tools/Figma Converter")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<Window>("Figma Converter");  //Title da ferramenta
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
            try {
                Core.Start(token, documentID, escala);
            }
            catch (Exception e){
                Debug.Log(e);
            }
        }
    }
}