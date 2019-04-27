using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavig : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(Input.mousePosition);
        bool submit = Input.GetButtonDown("Submit");
        Debug.Log(screenPos);
        if (submit)
        {
            Play();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("sampleScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
