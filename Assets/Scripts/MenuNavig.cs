using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavig : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
    }
    public void Play()
    {
        Debug.Log("pommier");
        SceneManager.LoadScene("sampleScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void startMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
