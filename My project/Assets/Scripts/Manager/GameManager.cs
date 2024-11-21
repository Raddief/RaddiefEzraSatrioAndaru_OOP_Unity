using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
        DontDestroyOnLoad(GameObject.Find("Player"));

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
            
            foreach (GameObject obj in allObjects)
            {
                // Check apakah objek ditag dengan label "MainCamera" atau "Player"
                if (obj.CompareTag("MainCamera") || obj.CompareTag("Player"))
                {
                    // skip camera dan player
                    continue;
                }
            }
    }
}

