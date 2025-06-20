using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<string, EggStat> EggStat = new();
    public static Dictionary<string, RockStat> RockStat = new();

    public void LoadDatas()
    {
        EggStat.Clear();
        RockStat.Clear();

        TextAsset json = Resources.Load<TextAsset>("Data/GoogleSheetJson");
        if (json == null)
        {
            Debug.LogError("GoogleSheetJson.json이 없습니다.");
            return;
        }

        ObjectData data = JsonUtility.FromJson<ObjectData>(json.text);

        foreach (var egg in data.EggData)
        {
            EggStat[egg.Id] = egg;
        }

        foreach (var rock in data.RockData)
        {
            RockStat[rock.Id] = rock;
        }
    }
}
