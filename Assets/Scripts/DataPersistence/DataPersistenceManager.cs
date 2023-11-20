using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistanceObject();
        LoadGame();
    }

    
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        // TODO - Load any saved data from a file using the data handler
        // If no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Create a new one");
            NewGame();
        }

        // TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }




    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // TODO - save that data to a file using the data handler
        dataHandler.Save(gameData);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistanceObject()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

}
