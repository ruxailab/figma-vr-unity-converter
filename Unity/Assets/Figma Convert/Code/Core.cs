using UnityEngine;

public class Core {
    public static void Start(int escala){
        
        int cont = 0;
        for(int i = Global.documentID.Length-1; i > 0; i--){
            if(Global.documentID[i] == '/'){
                cont++;
                if(cont == 1)
                    Global.documentID = Global.documentID.Remove(i);
                else if(cont == 2){
                    Global.documentID = Global.documentID.Remove(0,i);
                    break;
                }
            }
        }
                
        // Chamada da API
        File apiDocument = APIService.GetDocument(Global.token  , Global.documentID);
        
        // Loop Pagina
        for(int i = 0; i<apiDocument.document.children.Length; i++){
                    
            GameObject empty = new GameObject("Page " + (i+1));
                    
            // Loop Objeto
            float z = 0;
            for(int j = 0; j<apiDocument.document.children[i].children.Length; j++, z+=0.1f){
                ObjectProperty apiObj = apiDocument.document.children[i].children[j];
                Builder objeto = new Builder(apiObj, empty, z, escala);
                objeto.createObject();
            }
            empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
        }
    }
}