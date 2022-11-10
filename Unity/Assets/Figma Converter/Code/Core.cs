using UnityEngine;

public class Core {
    public static void Start(string token, string documentID, int escala){
        
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
        File apiDocument = APIService.GetDocument(token, documentID);
        string apiImage = APIService.GetImages(token, documentID);

        // Loop Pagina
        for(int i = 0; i<apiDocument.document.children.Length; i++){
                    
            GameObject empty = new GameObject("Page " + (i+1));
                    
            // Loop Objeto
            for(int j = 0; j<apiDocument.document.children[i].children.Length; j++){
                ObjectProperty apiObj = apiDocument.document.children[i].children[j];
                Build objeto = new Build(apiObj, apiImage, empty, j, escala);
                objeto.createObject();
            }
            empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
        }
    }
}