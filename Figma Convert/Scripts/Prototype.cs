using UnityEngine;
using UnityEngine.UI;

public class Prototype : MonoBehaviour
{
    public string transitionNodeID;

    void Start()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnClickEvent);
    }

    void OnClickEvent()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(this.transitionNodeID);
        foreach (GameObject gameObject in gameObjects)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.enabled = true;    
        }
    }
}