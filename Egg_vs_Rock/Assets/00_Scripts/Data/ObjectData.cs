using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData
{
    public List<EggData> EggDataList;
    public List<RockData> RockDataList;
}

[Serializable]
public class EggData
{
    public string Id;
    public int Hp;
    public int Damage;
}

[Serializable]
public class RockData
{
    public string Id;
    public int Hp;
    public int Damage;
}
