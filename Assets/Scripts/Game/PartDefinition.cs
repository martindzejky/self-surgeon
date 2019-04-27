using System;

[Serializable]
public struct HumanPartDefinition {
    public string name;

    public uint blood;
    public uint imunity;

    public string operationScene;
}

[Serializable]
public struct RoboticPartDefinition {
    public string name;
}
