using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static float FINAL_MONEY;
    public static float TOTAL_PLAY_TIME;
    public static bool pause = false;

    public RectTransform phoneTransform;
    private float lerpTime = 1;
    float xI;
    float yI;
    private Vector2 posInit;
    private AudioSource audioData;

    Vector2 velocityDamp = new Vector2(0.0F, 0.0F);
    //float test=0;

    private float playtime;

    public float getPlayTime()
    {
        return playtime;
    }


    public static GameManager Instance()
    {

        return instance;
    }

    public GameObject player;

    void Start()
    {
        xI = phoneTransform.anchoredPosition.x;
        yI = phoneTransform.anchoredPosition.y;
        posInit = new Vector2(xI, yI);
        audioData = GetComponent<AudioSource>();

        pause = false;
        playtime = 0;
    }

    void Update()
    {
        playtime += Time.deltaTime;
        if (!pause)
        {
            TOTAL_PLAY_TIME = playtime;
        }
        //if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        //{
        //    Time.timeScale = 0;
        //    lerpTime = 0;
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        //{
        //    Time.timeScale = 1;
        //    lerpTime = 0;
        //}
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            pause = true;
            lerpTime = 0;
            audioData.Play(0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            pause = false;
            lerpTime = 0;
            audioData.Play(0);
        }
        if (!pause && lerpTime == 0)
        {
            float x = phoneTransform.anchoredPosition.x;
            float y = phoneTransform.anchoredPosition.y;
            Vector2 moveDamp = Vector2.SmoothDamp(phoneTransform.anchoredPosition, posInit, ref velocityDamp, 0.3f);
            phoneTransform.anchoredPosition = moveDamp;
        }
        else if (pause)
        {
            float x = phoneTransform.anchoredPosition.x;
            float y = phoneTransform.anchoredPosition.y;
            Vector2 moveDamp = Vector2.SmoothDamp(phoneTransform.anchoredPosition, new Vector2(600, -700), ref velocityDamp, 0.3f);
            phoneTransform.anchoredPosition = moveDamp;
            //lerpTime = lerpTime + Time.deltaTime;
            //phoneTransform.anchoredPosition = new Vector2(Mathf.Lerp(xI, -700, lerpTime), Mathf.Lerp(yI, 200, lerpTime));
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void EndLive(float finalMoney)
    {
        FINAL_MONEY = finalMoney;
        TOTAL_PLAY_TIME = playtime;
        SceneManager.LoadScene("EndScreen");
    }

}
