using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance() {

        return instance;
    }

    public GameObject player;

    void Update()
    {
        Debug.Log("pommier");
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            Debug.Log("pommier");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    public GameObject GetPlayer() {
        return player;
    }

}
