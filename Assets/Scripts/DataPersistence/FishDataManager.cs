using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataManager : MonoBehaviour
{
    public static FishDataManager Instance;
    //public string fishID = "FishID";
    public string fishCurrentHp = "CurrentHP";
    public string fishLevel = "CurrentLevel";
    public string fishPosition = "currentPosition";
    public string fishPrefab = "Prefab";
    public void SetFish(List<FishData> fishDatas)
    {
        for(int i = 0; i<fishDatas.Count; i++)
        {
            //PlayerPrefs.SetInt(fishCurrentHp + fishDatas[i].id);
        }

    }

    public void SetBank(int bank)
    {
        PlayerPrefs.SetInt("Bank", bank);
    }

    public int GetBank()
    {
        return PlayerPrefs.GetInt("Bank",0);
    }
}
