using System;

[Serializable]
public struct HumanPartDefinition {
    public string name; // this is label for UI
    public string bodyPartName; // this is used for code
    public float price;

    public uint blood;
    public uint imunity;
}

[Serializable]
public struct RoboticPartDefinition {
    public string name; // this is label for UI
    public string bodyPartName; // this is used for code
    public float price;
}

public enum BodyPartType {
    Missing,
    Human,
    Robotic,
}

[Serializable]
public struct BodyPartGlobalState {
    public string bodyPartName;
    public BodyPartType currentType;
}
