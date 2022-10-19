using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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
                        break;
                    }
                }
            }
            
            // Chamada da API
            File api = APIHelper.GetDocument(token, documentID);            

            // Loop Pagina
            for(int i = 0; i<api.document.children.Length; i++){
                
                GameObject empty = new GameObject("Page " + (i+1));
                
                // Loop Objeto
                for(int j = 0; j<api.document.children[i].children.Length; j++){
                    
                    var apiObj = api.document.children[i].children[j];
                    
                    // Verifica se Objeto é um Retangulo
                    if(apiObj.type == "RECTANGLE"){
                        // Altura e Largura
                        var width = apiObj.absoluteBoundingBox.width/100;
                        var height = apiObj.absoluteBoundingBox.height/100;
                        
                        // Localização nos Eixos X e Y
                        var x = (apiObj.absoluteBoundingBox.x/100) + (width/2); 
                        var y = (apiObj.absoluteBoundingBox.y/100) + (height/2);
                        
                        // Cores
                        var r = apiObj.fills[0].color.r;
                        var g = apiObj.fills[0].color.g;
                        var b = apiObj.fills[0].color.b;
                        var a = apiObj.fills[0].color.a;
                        
                        // Criação do Objeto
                        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        obj.transform.position = new Vector3(x, y, (float)(0.01*j));
                        obj.transform.localScale = new Vector3(width, height, 1);
                        obj.transform.parent = empty.transform;
                        
                        // Variavel teporaria para inserir a cor do Objeto
                        var tempMaterial = new Material(obj.GetComponent<Renderer>().sharedMaterial);
                        tempMaterial.color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
                        obj.GetComponent<Renderer>().sharedMaterial = tempMaterial;
                        Debug.Log("Objeto Criado com Sucesso");
                    }
                    //Verifica se é Circulo
                    else if(apiObj.type == "ELLIPSE"){
                        // Altura e Largura
                        var width = apiObj.absoluteBoundingBox.width/100;
                        var height = apiObj.absoluteBoundingBox.height/100;

                        // Localização nos Eixos X e Y
                        var x = (apiObj.absoluteBoundingBox.x/100) + (width/2); 
                        var y = (api.document.children[0].children[j].absoluteBoundingBox.y/100) + (height/2);

                        // Cores
                        var r = apiObj.fills[0].color.r;
                        var g = apiObj.fills[0].color.g;
                        var b = apiObj.fills[0].color.b;
                        var a = apiObj.fills[0].color.a;

                        // Criação do Objeto
                        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        obj.transform.position = new Vector3(x,y,(float)(0.01*j));
                        obj.transform.localScale = new Vector3(width, height, 1);
                        obj.transform.parent = empty.transform;

                        // Variavel teporaria para inserir a cor do Objeto
                        var tempMaterial = new Material(obj.GetComponent<Renderer>().sharedMaterial);
                        tempMaterial.color = new Color32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
                        obj.GetComponent<Renderer>().sharedMaterial = tempMaterial;
                        Debug.Log("Objeto Criado com Sucesso");
                    }
                    else if(apiObj.type == "TEXT"){
                        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
                        textmeshPro.SetText("The first number is {0} and the 2nd is {1:2} and the 3rd is {3:0}.", 4, 6.345f, 3.5f);
                        Debug.Log("Text Criado");
                    }
                }
                empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
            }
        }
    }
}