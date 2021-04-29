using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Ensures that the GameManager can only be instantiated once.
    #region Singleton Code
    private static GameManager _instance;

    public static GameManager Instance {
        get { return _instance; }
    }

    private void EnforceSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);    //Don't allow creation of multiple objects
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject); //TODO: might actually need this
        }
    }

    private void OnDestroy()
    {
        if (this == _instance) { _instance = null; }
    }
    #endregion

    public GameControls controls;   //Contains the input mappings for the game

    public PlaytestStats playtestStats;

    public int totalEnemiesInScene;
    public int totalCollectablesInScene;
    public int uniqueEnemiesDefeated;

    public bool debugMode;
    public bool friendlyFire;

    //public bool menuOpen;

    private void Awake()
    {
        EnforceSingleton();

        controls = new GameControls();
        controls.UI.Enable();

        playtestStats = new PlaytestStats();

        uniqueEnemiesDefeated = 0;

        //menuOpen = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalEnemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalCollectablesInScene = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }

    void OnApplicationQuit()
    {
        //playtestStats.PrintStats();
    }
}
