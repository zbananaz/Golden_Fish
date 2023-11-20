using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour, IDataPersistence 
{
    public static GameManager Instance;

    public Vector2 leftEdge, rightEdge;

    public int bank = 0;

    public List<FishController> fishes;

    [SerializeField] private TextMeshProUGUI bankText;
    public GameData gameData;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        leftEdge = Camera.main.ScreenToWorldPoint(Vector2.zero);
        rightEdge = Camera.main.ScreenToWorldPoint(new Vector2 (Screen.width, Screen.height));
        bank = FishDataManager.Instance.GetBank();
        gameData = new GameData();
        gameData.Load();
    }

    public void LoadData(GameData data)
    {
        this.bank = data.bank;
    }

    public void SaveData (ref GameData data)
    {
        data.bank = this.bank;
    }

    private void Start()
    {
        bankText.text = string.Concat("Bank: ", bank.ToString());
    }

    public void BankIncrease()
    {
        bankText.text = string.Concat("Bank: ", bank.ToString());

    }

    private void OnApplicationQuit()
    {
        gameData.Save();
    }
}
