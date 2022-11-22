using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class WindowBuilder : EditorWindow {

    [MenuItem("Tools/Builder")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<WindowBuilder>("Builder");  //Title da ferramenta
    }

    private void OnGUI() {
        if(GUILayout.Button("Apply")) {
            // try {
                var core = new CoreBuilder();
                core.Start();
            // }
            // catch (Exception e){
                // Debug.Log(e);
            // }
        }
    }
}