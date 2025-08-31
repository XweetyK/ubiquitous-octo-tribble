using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public bool active = true;
    public Identity[] Ids;
    public int CurrentId;
    public int Speed;
    public int Attack;
    public int Defense;

    Identity Current;

    public int TotalSpeed() {
        int total = Speed + Current.baseSpe;
        return total;
    }
    public int TotalAtk() {
        int total = Attack + Current.baseAtk;
        return total;
    }
    public int TotalDef() {
        int total = Defense + Current.baseDef;
        return total;
    }

    public void SetID() {
        Current = Ids[CurrentId];
    }
}
