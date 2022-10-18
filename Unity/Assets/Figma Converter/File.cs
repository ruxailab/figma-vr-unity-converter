[System.Serializable]
public class Constrait{
    public string type;
    public string value;
}

[System.Serializable]
public class ExportSettings{
    public string suffix;
    public string format;
    public Constrait constrait;
}

[System.Serializable]
public class Size{
    public string width;
    public string heigth;
}

[System.Serializable]
public class PrototypeDevice{
    public string type;
    public Size size;
}

[System.Serializable]
public class Color{
    public float r;
    public float g;
    public float b;
    public float a;
}

[System.Serializable]
public class Fills{
    public string blendMode;
    public string type;
    public Color color;
}

[System.Serializable]
public class Constraints{
    public string vertical;
    public string horizontal;
}

[System.Serializable]
public class Absolute{
    public float x;
    public float y;
    public float width;
    public float height;
}

[System.Serializable]
public class ChildrenObj{
    public string id;
    public string name;
    public string type;
    public string blendMode;
    public Absolute absoluteBoundingBox;
    public Absolute absoluteRenderBounds;
    public Constraints constraits;
    public Fills[] fills;
    public string[] strokes;
    public float strokeWeight;
    public string storekeAlign;
    public string[] effects;
}

[System.Serializable]
public class ChildrenPage{
    public string id;
    public string name;
    public string type;
    public ChildrenObj[] children;
    public Color backgroundColor;
    public string prototypeStartNodeID;
    public string[] flowStartingPoints;
    public PrototypeDevice prototypeDevice;
    public ExportSettings[] exportSettings;
}

[System.Serializable]
public class Document{
    public string id;
    public string name;
    public string type;
    public ChildrenPage[] children;
}

[System.Serializable]
public class File
{
    public Document document;
    public string[] components;
    public string[] componentSets;
    public string[] schemaVersion;
    public string[] styles;
    public string name;
    public string lastModified;
    public string thumbnailUrl;
    public string version;
    public string role;
    public string editorType;
    public string linkAccess;
}