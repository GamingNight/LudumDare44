﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuNavig : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void startMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //public void GotIt1()
    //{
    //    GameObject Tuto1;
    //    foreach (Transform child in transform)
    //    {
    //        foreach (Transform child2 in child)
    //        {
    //            if (child2 == Tuto1)
    //            {
    //                Tuto1.setActive = false;
    //            }
    //            if (Tuto1.tag == Tuto1)
    //            {
    //                Tuto1.setActive = false;
    //            }
    //        }
    //    }

    //}
    public void InitLaunchMainScene()
    {
        StartCoroutine(LaunchMainSceneCoroutine());
    }

    private IEnumerator LaunchMainSceneCoroutine()
    {
        Vector2 targetPosition = new Vector2(900, -1200);
        float totalTime =0.3f;
        float step = 0.009f;
        float currentTime = 0f;
        while (currentTime < totalTime)
        {
            float x = ((RectTransform)transform).anchoredPosition.x;
            float y = ((RectTransform)transform).anchoredPosition.y;
            Vector2 velocityDamp = Vector2.zero;
            Vector2 moveDamp = Vector2.SmoothDamp(((RectTransform)transform).anchoredPosition, targetPosition, ref velocityDamp, 0.03f, Mathf.Infinity,step);
            ((RectTransform)transform).anchoredPosition = moveDamp;
            yield return new WaitForSeconds(step);
            currentTime += step;
        }
        SceneManager.LoadScene("sampleScene");
    }
}
