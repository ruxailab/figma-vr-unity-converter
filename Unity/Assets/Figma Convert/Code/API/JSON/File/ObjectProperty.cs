[System.Serializable]
public class ObjectProperty {
    public string id;
    public string name;
    public string type;
    public string scrollBehavior;
    public string blendMode;
    public ObjectProperty[] children;
    public Absolute absoluteBoundingBox;
    public Absolute absoluteRenderBounds;
    public Constraints constraits;
    public bool clipsContent;
    public Fills[] background;
    public Fills[] fills;
    public Fills[] strokes;
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