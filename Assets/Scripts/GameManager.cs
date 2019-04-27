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

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    public GameObject GetPlayer() {
        return player;
    }

}
