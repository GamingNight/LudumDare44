using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class StreamViewManager : MonoBehaviour
{

    private static readonly int[] VIEWERS_LEVELS = { 0, 10, 100, 1000, 10000 };
    private static readonly Color[] VIEWERS_COLORS = { Color.red, Color.Lerp(Color.red, Color.green, 0.2f), Color.Lerp(Color.red, Color.green, 0.4f), Color.Lerp(Color.red, Color.green, 0.6f), Color.Lerp(Color.red, Color.green, 0.8f), Color.green };
    private static readonly float[] MONEY_PROBAS = { 0, 0.1f, 0.2f, 0.5f, 0.7f, 0.9f };
    private static readonly float MONEY_TIME_CHECK = 5f;

    //UI
    public Image viewPanel;
    public Text viewCounter;
    public Text moneyText;
    public Text chatText;
    public GameObject tipPanel;
    public Canvas carCanvas;
    public GameObject carTextUIPrefab;

    //Sound
    public AudioSource pickupViewersSource;
    public AudioSource loseViewersSource;

    //Various public variables
    public int minViewDecreaseSpeed = 5;
    public int maxViewDecreaseSpeed = 50;
    public bool neverEnd = false;
    public TextAsset tipperTextAsset;

    //Private
    private float streamViews;
    private float money;
    private float timeSinceLastMoneyCheck;
    private Rigidbody rgbd;
    private float viewDecreaseSpeed;
    private List<string> allTipperPseudos;

    void Start()
    {
        streamViews = 200;
        money = 0;
        timeSinceLastMoneyCheck = 0;
        rgbd = GetComponent<Rigidbody>();
        viewDecreaseSpeed = minViewDecreaseSpeed;
        allTipperPseudos = ParseTextAsset(tipperTextAsset);
    }

    private void Update()
    {

        //Decrease views accumulated so far depending on the veolcity of the car
        viewDecreaseSpeed = computeViewsDecreaseSpeed(viewDecreaseSpeed);
        streamViews -= Time.deltaTime * viewDecreaseSpeed;
        streamViews = Mathf.Max(0, streamViews);
        //Check the corresponding viewer index
        int viewerIndex = 0;
        while (viewerIndex < VIEWERS_LEVELS.Length && streamViews > VIEWERS_LEVELS[viewerIndex])
        {
            viewerIndex++;
        }

        //Update View Panel
        Color panelColor = VIEWERS_COLORS[viewerIndex];
        panelColor.a = 100;
        viewPanel.color = panelColor;

        //Update Money
        timeSinceLastMoneyCheck += Time.deltaTime;
        if (timeSinceLastMoneyCheck >= MONEY_TIME_CHECK)
        {
            UpdateMoney(viewerIndex);
        }

        //Update View and Money HUD
        int roundedViews = Mathf.RoundToInt(streamViews);
        viewCounter.text = roundedViews.ToString();
        int millions = Mathf.RoundToInt(money / 1000000);
        int thousands = Mathf.RoundToInt((money % 1000000) / 1000);
        int units = Mathf.RoundToInt(money % 1000);
        moneyText.text = "$   " + millions + "." + thousands.ToString("D3") + "." + units.ToString("D3");
        int minutes = Mathf.FloorToInt(GameManager.TOTAL_PLAY_TIME / 60f);
        int seconds = Mathf.FloorToInt(GameManager.TOTAL_PLAY_TIME % 60);
        chatText.text = "Tired ? You already get $ " + millions + "." + thousands.ToString("D3") + "." + units.ToString("D3") + " for " + minutes + "m " + seconds + "s Live.";
        //moneyText.text = money.ToString();

        if (streamViews == 0 && !neverEnd)
        {
            GameManager.Instance().EndLive(money);
        }
    }

    private float computeViewsDecreaseSpeed(float currentViewDecreaseSpeed)
    {

        float minVelocity = 10f;
        float maxVelocity = 40f;
        float velocity = Mathf.Min(maxVelocity, Mathf.Max(minVelocity, rgbd.velocity.magnitude));
        float normvelocity = (velocity - minVelocity) / (maxVelocity - minVelocity);
        float targetDecreaseSpeed = minViewDecreaseSpeed + (1 - normvelocity) * (maxViewDecreaseSpeed - minViewDecreaseSpeed);
        return Mathf.Lerp(currentViewDecreaseSpeed, targetDecreaseSpeed, Time.deltaTime / 10f);
    }

    public void UpdateStreamPoints(float points)
    {
        streamViews += points;
        Color color;
        if (points > 0)
        {
            pickupViewersSource.Play();
            color = Color.green;
        }
        else
        {
            loseViewersSource.Play();
            color = Color.red;
        }
        StartCoroutine(StartTextUICoroutine(color, points));
    }

    public void UpdateMoney(int viewerIndex)
    {
        float moneyProbability = Random.Range(0f, 1f);
        if (moneyProbability < MONEY_PROBAS[viewerIndex])
        {
            int tip = Random.Range(30, 500);
            money += tip;
            StartCoroutine(UpdateTipPanelCoroutine(tip));
        }
        timeSinceLastMoneyCheck = 0f;
    }

    private IEnumerator UpdateTipPanelCoroutine(int tip)
    {

        tipPanel.SetActive(true);

        Text tipperText = tipPanel.transform.GetChild(0).GetComponent<Text>();
        int rndPseudoIndex = Random.Range(0, allTipperPseudos.Count);
        string pseudo = allTipperPseudos[rndPseudoIndex];
        Debug.Log("tipper " + pseudo + " at index " + rndPseudoIndex);
        tipperText.text = pseudo + " tipped you:";
        Text tipText = tipPanel.transform.GetChild(1).GetComponent<Text>();
        tipText.text = "$ " + tip.ToString();

        tipPanel.SetActive(true);
        float transitionTime = 0.2f;
        float stayTime = 2f;
        float step = 0.01f;

        float currentTime = 0;
        while (currentTime < transitionTime)
        {
            tipPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentTime / transitionTime);
            float alphaLerp = Mathf.Lerp(0f, 1f, currentTime / transitionTime);
            tipperText.color = new Color(tipperText.color.r, tipperText.color.g, tipperText.color.b, alphaLerp);
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, alphaLerp);
            yield return new WaitForSeconds(step);
            currentTime += step;
        }

        yield return new WaitForSeconds(stayTime);

        currentTime = 0;
        while (currentTime < transitionTime)
        {
            tipPanel.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, currentTime / transitionTime);
            float alphaLerp = Mathf.Lerp(1f, 0f, currentTime / transitionTime);
            tipperText.color = new Color(tipperText.color.r, tipperText.color.g, tipperText.color.b, alphaLerp);
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, alphaLerp);
            yield return new WaitForSeconds(step);
            currentTime += step;
        }

        tipPanel.SetActive(false);
    }

    private List<string> ParseTextAsset(TextAsset textAsset)
    {

        string fullText = textAsset.text;
        string[] lines = Regex.Split(fullText, "\n|\r|\r\n");
        List<string> nonEmptyLines = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
            if (lines[i] != "")
            {
                nonEmptyLines.Add(lines[i]);
            }
        }
        return nonEmptyLines;
    }


    private IEnumerator StartTextUICoroutine(Color color, float points)
    {

        float totalTime = 0.5f;
        float step = 0.01f;
        float currentTime = 0;
        float startTopPosition = 0;
        float endTopPosition = 2;

        GameObject carTextUI = Instantiate<GameObject>(carTextUIPrefab);
        carTextUI.transform.SetParent(carCanvas.transform, false);
        carTextUI.GetComponent<Text>().text = points.ToString();
        carTextUI.GetComponent<Text>().color = color;

        while (currentTime < totalTime)
        {
            float top = Mathf.Lerp(startTopPosition, endTopPosition, currentTime / totalTime);
            RectTransform rectTransform = ((RectTransform)carTextUI.transform);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, top);

            float alpha = Mathf.Lerp(1, 0, currentTime / totalTime);
            carTextUI.GetComponent<Text>().color = new Color(color.r, color.g, color.b, alpha);

            yield return new WaitForSeconds(step);
            currentTime += step;
        }
        Destroy(carTextUI);
    }
}
