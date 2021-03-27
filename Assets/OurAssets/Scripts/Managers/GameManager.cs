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

    public bool debugMode;

    private void Awake()
    {
        EnforceSingleton();

        controls = new GameControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
