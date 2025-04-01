using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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
            Debug.LogError("Multiple instances of Data Persistence Manager detected.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public int GetHighScore()
    {
        return gameData.score;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IDataPersistence[] dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(
            FindObjectsSortMode.None
        ).OfType<IDataPersistence>().ToArray();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
