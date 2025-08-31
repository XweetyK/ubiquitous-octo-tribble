using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Terrain { A,B,C};

[System.Serializable]
public class Identity{
    public Sprite skin;
    public Terrain advantage;
    public Terrain weakness;
    public int baseSpe;
    public int baseAtk;
    public int baseDef;
}
