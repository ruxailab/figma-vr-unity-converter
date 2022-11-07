[System.Serializable]
public class Style {
    public string fontFamily;
    public string fontPostScriptName;
    public bool italic;
    public int fontWeight;
    public string textAutoResize;
    public int fontSize;
    public string textAlignHorizontal;
    public string textAlignVertical;
    public float letterSpacing;
    public float lineHeightPx;
    public float lineHeightPercent;
    public string lineHeightUnit;
}

[System.Serializable]
public class Constrait {
    public string type;
    public string value;
}

[System.Serializable]
public class ExportSettings {
    public string suffix;
    public string format;
    public Constrait constrait;
}

[System.Serializable]
public class Size {
    public string width;
    public string heigth;
}

[System.Serializable]
public class PrototypeDevice {
    public string type;
    public Size size;
}

[System.Serializable]
public class Color {
    public float r;
    public float g;
    public float b;
    public float a;
}

[System.Serializable]
public class Fills {
    public string blendMode;
    public string type;
    public string scaleMode;
    public string imageRef;
    public Color color;
}

[System.Serializable]
public class Constraints {
    public string vertical;
    public string horizontal;
}

[System.Serializable]
public class Absolute {
    public float x;
    public float y;
    public float width;
    public float height;
}

[System.Serializable]
public class ChildrenObj {
    public string id;
    public string name;
    public string type;
    public string scrollBehavior;
    public string blendMode;
    public ChildrenObj[] children;
    public Absolute absoluteBoundingBox;
    public Absolute absoluteRenderBounds;
    public Constraints constraits;
    public bool clipsContent;
    public Fills[] background;
    public Fills[] fills;
    public string[] strokes;
    public float cornerRadius;
    public float strokeWeight;
    public string storekeAlign;
    public Color backgroundColor;
    // public LayoutGrids[] layoutGrids;
    public ExportSettings[] exportSettings;
    public string[] effects;
    public string characters;
    public Style style;
    public int layoutVersion;
    public string[] characterStyleOverrides;
    public string styleOverrideTable;
    public string[] lineTypes;
    public int[] lineIndentations;
}

[System.Serializable]
public class ChildrenPage {
    public string id;
    public string name;
    public string type;
    public string scrollBehavior;
    public ChildrenObj[] children;
    public Color backgroundColor;
    public string prototypeStartNodeID;
    public string[] flowStartingPoints;
    public PrototypeDevice prototypeDevice;
    public ExportSettings[] exportSettings;
}

[System.Serializable]
public class Document {
    public string id;
    public string name;
    public string type;
    public string scrollBehavior;
    public ChildrenPage[] children;
}

[System.Serializable]
public class File {
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