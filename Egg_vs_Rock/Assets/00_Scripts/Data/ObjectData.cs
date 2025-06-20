using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData
{
    public List<EggStat> EggData;
    public List<RockStat> RockData;
}

[Serializable]
public class EggStat
{
    public string Id;
    public int Hp;
    public int Damage;
}

[Serializable]
public class RockStat
{
    public string Id;
    public int Hp;
    public int Damage;
}
