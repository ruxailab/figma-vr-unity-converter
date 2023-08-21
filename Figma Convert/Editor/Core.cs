using System;
using System.Collections.Generic;
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
        File apiDocument = APIService.GetDocument();
        Global.apiImage = APIService.GetImage();

        // Loop Pagina
        for(int i = 0; i<apiDocument.document.children.Length; i++){
                    
            GameObject empty = new GameObject("Page " + (i+1));

            Global.transitionNodeID = getReaction(apiDocument.document.children[i]);
                    
            // Loop Objeto
            for(int j = 0; j<apiDocument.document.children[i].children.Length; j++){
                ObjectProperty apiObj = apiDocument.document.children[i].children[j];
                if(apiObj.componentPropertyDefinitions.PositionX != null) {
                    Builder objeto = new Builder(apiObj, empty, escala);
                    objeto.createObject();
                }
            }
            empty.transform.Rotate(180.0f, 0f, 0f, Space.World);
            empty.transform.position = new Vector3(0, 1, 0);
        }
    }

    public static List<string> getReaction(Page page) {
        List<ObjectProperty> stacks = new List<ObjectProperty> {};
        List<string> transitionNodeID = new List<string> {};

        foreach (ObjectProperty compoment in page.children) {
            stacks.Add(compoment);
        }
            
        while(stacks.Count > 0) {
            ObjectProperty currentComponent = stacks[stacks.Count - 1];
            stacks.RemoveAt(stacks.Count - 1);

            if(currentComponent.children != null) {
                foreach (ObjectProperty component in currentComponent.children) {
                    stacks.Add(component);
                }
            }

            if(currentComponent.transitionNodeID != null) {
                transitionNodeID.Add(currentComponent.transitionNodeID);
            }
        }
        return transitionNodeID;
    }
}