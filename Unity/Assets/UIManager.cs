/*using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIManagerWindow : EditorWindow {
    
    string primaryTag = "User Interface - Primary";
    Sprite primarySprite;
    Color primaryColor;

    [MenuItem("Tools/UI Manager")] // Aba onde irá achar a ferramenta
    public static void ShowWindow() { //Mostrar janela da ferramenta
        GetWindow<UIManagerWindow>("UI Manager");  //Title da ferramenta
    }

    private void OnGUI() {
        GUILayout.Label("Classification", EditorStyles.label);  //Escrita
        GUILayout.BeginHorizontal(); //Inicia uma linha horizontal
        EditorGUILayout.LabelField("Primary Tag", GUILayout.MaxWidth(125)); //Escrita
        if(GUILayout.Button(primaryTag)) { //Se foi clicado o botão e oq tem escrito
            foreach (GameObject i in Selection.gameObjects) { //For para pegar todos os objetos selecionados
                i.gameObject.tag = primaryTag; // Troca a tag do GameObject
                Debug.Log("oi"); //Imprimir um log
            }
        }
        GUILayout.EndHorizontal(); //Finaliza
        EditorGUILayout.Space();

        GUILayout.Label("Setting", EditorStyles.label); 
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Primary Image", GUILayout.MaxWidth(125));
        primarySprite = (Sprite)EditorGUILayout.ObjectField(primarySprite, typeof(Sprite), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Primary Color", GUILayout.MaxWidth(125));
        primaryColor = EditorGUILayout.ColorField(primaryColor); //Editor de cor
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if(GUILayout.Button("Apply")) {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag(primaryTag)) {
                i.GetComponent<Image>().sprite = primarySprite;
                i.GetComponent<Image>().color = primaryColor;              
            }
        }
    }

}*/
