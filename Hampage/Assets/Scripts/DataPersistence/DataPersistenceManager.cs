using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    //get; provate set means we will be able to get instance publicly but will only be able to modify
    // instance privately within this class
    public static DataPersistenceManager instance { get; private set; }
    
    private void Awake ()
    {
        //if there is already a manager in the scene
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistance Manager in the Scene");
        }
        instance = this;
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //Debug.Log("Load Game called");
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
        Debug.Log("Save game Called");
    }

    public void NewGame()
    {
        //initnalize game data to new game data object
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();
        Debug.Log("Loaded Game data is " + dataPersistenceObjects);
        if(this.gameData == null)
        {
            Debug.Log("no data found. Initalizing game with deafault values");
                NewGame();
        }

        foreach(IDataPersistence datapersistenceObj in dataPersistenceObjects)
        {
            datapersistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded Door position = " + gameData.doorOpen);
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        //sace that data to a file using data handler
        dataHandler.Save(gameData);
        Debug.Log("Saved Door state is " + gameData.doorOpen);
        Debug.Log("Current Game Data is " + this.gameData);

    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //find all scrips implementing from IDataPersistence 
        //Scripts must extend from MonoBehaviour
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
        Debug.Log("data persistence object list created");
        foreach (IDataPersistence datapersistenceObj in dataPersistenceObjects)
        {
            Debug.Log("datapersistenceObj");
        }

    }
}
