[System.Serializable]
public class Page {
    public string id;
    public string name;
    public string type;
    public string scrollBehavior;
    public ObjectProperty[] children;
    public Color backgroundColor;
    public string prototypeStartNodeID;
    public string[] flowStartingPoints;
    public PrototypeDevice prototypeDevice;
    public ExportSettings[] exportSettings;
}