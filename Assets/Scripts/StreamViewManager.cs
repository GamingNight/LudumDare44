using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StreamViewManager : MonoBehaviour {

    private static readonly int[] VIEWERS_LEVELS = { 0, 10, 100, 1000, 10000 };
    private static readonly Color[] VIEWERS_COLORS = { Color.red, Color.Lerp(Color.red, Color.green, 0.2f), Color.Lerp(Color.red, Color.green, 0.4f), Color.Lerp(Color.red, Color.green, 0.6f), Color.Lerp(Color.red, Color.green, 0.8f), Color.green };
    private static readonly float[] MONEY_PROBAS = { 0, 0.1f, 0.2f, 0.5f, 0.7f, 0.9f };
    private static readonly float MONEY_TIME_CHECK = 5f;

    public Image viewPanel;
    public Text viewCounter;
    public Text moneyText;
    public Text ChatText;
    public Canvas carCanvas;
    public GameObject carTextUIPrefab;
    public AudioSource pickupViewersSource;
    public AudioSource loseViewersSource;
    public int minViewDecreaseSpeed = 5;
    public int maxViewDecreaseSpeed = 50;
    public bool neverEnd = false;

    private float streamViews;
    private float money;
    private float timeSinceLastMoneyCheck;
    private Rigidbody rgbd;
    private float viewDecreaseSpeed;

    void Start() {
        streamViews = 200;
        money = 0;
        timeSinceLastMoneyCheck = 0;
        rgbd = GetComponent<Rigidbody>();
        viewDecreaseSpeed = minViewDecreaseSpeed;
    }

    private void Update() {

        //Decrease views accumulated so far depending on the veolcity of the car
        viewDecreaseSpeed = computeViewsDecreaseSpeed(viewDecreaseSpeed);
        streamViews -= Time.deltaTime * viewDecreaseSpeed;
        streamViews = Mathf.Max(0, streamViews);
        //Check the corresponding viewer index
        int viewerIndex = 0;
        while (viewerIndex < VIEWERS_LEVELS.Length && streamViews > VIEWERS_LEVELS[viewerIndex]) {
            viewerIndex++;
        }

        //Update View Panel
        Color panelColor = VIEWERS_COLORS[viewerIndex];
        panelColor.a = 100;
        viewPanel.color = panelColor;

        //Update Money
        timeSinceLastMoneyCheck += Time.deltaTime;
        if (timeSinceLastMoneyCheck >= MONEY_TIME_CHECK) {
            UpdateMoney(viewerIndex);
        }

        //Update View and Money HUD
        int roundedViews = Mathf.RoundToInt(streamViews);
        viewCounter.text = roundedViews.ToString();
        int millions = Mathf.RoundToInt(money / 1000000);
        int thousands = Mathf.RoundToInt((money % 1000000) / 1000);
        int units = Mathf.RoundToInt(money % 1000);
        moneyText.text = "$   " + millions + "." + thousands.ToString("D3") + "." + units.ToString("D3");
        int minutes = Mathf.RoundToInt(GameManager.TOTAL_PLAY_TIME / 60f);
        int seconds = Mathf.RoundToInt(GameManager.TOTAL_PLAY_TIME % 60);
        ChatText.text = "Tired ? You already get $ " + millions + "." + thousands.ToString("D3") + "." + units.ToString("D3") +" for " + minutes + "m " + seconds + "s Live.";
        //moneyText.text = money.ToString();

        if (streamViews == 0 && !neverEnd) {
            GameManager.Instance().EndLive(money);
        }
    }

    private float computeViewsDecreaseSpeed(float currentViewDecreaseSpeed) {

        float minVelocity = 10f;
        float maxVelocity = 40f;
        float velocity = Mathf.Min(maxVelocity, Mathf.Max(minVelocity, rgbd.velocity.magnitude));
        float normvelocity = (velocity - minVelocity) / (maxVelocity - minVelocity);
        float targetDecreaseSpeed = minViewDecreaseSpeed + (1 - normvelocity) * (maxViewDecreaseSpeed - minViewDecreaseSpeed);
        return Mathf.Lerp(currentViewDecreaseSpeed, targetDecreaseSpeed, Time.deltaTime / 10f);
    }

    public void UpdateStreamPoints(float points) {
        streamViews += points;
        Color color;
        if (points > 0) {
            pickupViewersSource.Play();
            color = Color.green;
        } else {
            loseViewersSource.Play();
            color = Color.red;
        }
        StartCoroutine(StartTextUICoroutine(color, points));
    }

    public void UpdateMoney(int viewerIndex) {
        float moneyProbability = Random.Range(0f, 1f);
        if (moneyProbability < MONEY_PROBAS[viewerIndex]) {
            money += Random.Range(30, 500);
        }
        timeSinceLastMoneyCheck = 0f;
    }

    private IEnumerator StartTextUICoroutine(Color color, float points) {

        float totalTime = 0.5f;
        float step = 0.01f;
        float currentTime = 0;
        float startTopPosition = 0;
        float endTopPosition = 2;

        GameObject carTextUI = Instantiate<GameObject>(carTextUIPrefab);
        carTextUI.transform.SetParent(carCanvas.transform, false);
        carTextUI.GetComponent<Text>().text = points.ToString();
        carTextUI.GetComponent<Text>().color = color;

        while (currentTime < totalTime) {
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
