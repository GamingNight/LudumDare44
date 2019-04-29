using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEndStats : MonoBehaviour
{
    public Text moneyText;
    public Text playtimeText;

    void Start()
    {
        int millions = Mathf.RoundToInt(GameManager.FINAL_MONEY / 1000000);
        int thousands = Mathf.RoundToInt((GameManager.FINAL_MONEY % 1000000)/1000);
        int units = Mathf.RoundToInt(GameManager.FINAL_MONEY % 1000);
        moneyText.text = "$ "+millions+"."+ thousands.ToString("D3") +"."+ units.ToString("D3");
        int minutes = Mathf.FloorToInt(GameManager.TOTAL_PLAY_TIME / 60f);
        int seconds = Mathf.FloorToInt(GameManager.TOTAL_PLAY_TIME % 60);
        playtimeText.text = "The last " + minutes + "m " + seconds + "s " + "of your life brought you";
    }
}
