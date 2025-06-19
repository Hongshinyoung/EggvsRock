using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<string, EggData> EggStat = new();
    public static Dictionary<string, RockData> RockStat = new();

    public void LoadDatas()
    {
        EggStat.Clear();
        RockStat.Clear();

        TextAsset json = Resources.Load<TextAsset>("Data/GoogleSheetJson");

        ObjectData data = JsonUtility.FromJson<ObjectData>(json.text);

        foreach (var egg in data.EggDataList)
        {
            EggStat[egg.Id] = egg;
        }

        foreach (var rock in data.RockDataList)
        {
            RockStat[rock.Id] = rock;
        }
    }
}
