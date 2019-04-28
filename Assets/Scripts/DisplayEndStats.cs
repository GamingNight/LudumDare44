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
        moneyText.text = GameManager.FINAL_MONEY.ToString() + "$";
        int minutes = Mathf.RoundToInt(GameManager.TOTAL_PLAY_TIME / 60f);
        int seconds = Mathf.RoundToInt(GameManager.TOTAL_PLAY_TIME % 60);
        playtimeText.text = "The last " + minutes + " minute" + (minutes > 1 ? "s " : " ") + seconds + " second" + (minutes > 1 ? "s " : " ") + "of your life brought you";
    }
}
