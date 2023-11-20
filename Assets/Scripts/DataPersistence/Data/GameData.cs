using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
[System.Serializable]
public class GameData
{
    public FishData fishData = new FishData();
    private FishController fishController;
    public int bank;
    public List<FishController> fishList;//List nhung con ca dang co trng scene
    public List<FishController> debugList;
    // The values defined in this contructor will be the default values
    // The game starts with when there's no data to Load.
    public GameData()
    {
        this.bank = 0;
        fishList = GameManager.Instance.fishes;

    }

    public void fishesRegister(List<FishController> list)
    {
        fishList = list;
    }
    public string SaveString
    {
        get => PlayerPrefs.GetString("SaveFish", string.Empty);
        set => PlayerPrefs.SetString("SaveFish", value);
    }
    public void Save()
    {
        SaveString = JsonConvert.SerializeObject(fishList);
    }
    public void Load()
    {
        if (string.IsNullOrEmpty(SaveString))
        {
            debugList = JsonConvert.DeserializeObject<List<FishController>>(SaveString);
        }
    }

}
