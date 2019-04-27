using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StreamViewManager : MonoBehaviour {

    private static readonly int[] VIEWERS_LEVELS = { 10, 100, 1000, 10000 };
    private static readonly Color[] VIEWERS_COLORS = { Color.red, Color.Lerp(Color.red, Color.green, 0.25f), Color.Lerp(Color.red, Color.green, 0.50f), Color.Lerp(Color.red, Color.green, 0.75f), Color.green };
    private static readonly float[] MONEY_PROBAS = { 0.1f, 0.2f, 0.5f, 0.7f, 0.9f };
    private static readonly float MONEY_TIME_CHECK = 5f;

    public Image viewPanel;
    public Text viewCounter;
    public Canvas carCanvas;
    public GameObject carTextUIPrefab;
    public int viewDecreaseSpeed = 5;

    private float streamPoints;
    private float money;
    private float timeSinceLastMoneyCheck;

    void Start() {
        streamPoints = 50;
        money = 0;
        timeSinceLastMoneyCheck = 0;
    }

    private void Update() {

        //Update the points accumulated so far
        Debug.Log(streamPoints);
        streamPoints -=  Time.deltaTime * viewDecreaseSpeed;
        streamPoints = Mathf.Max(0, streamPoints);
        //Check the corresponding viewer index
        int viewerIndex = 0;
        while (viewerIndex < VIEWERS_LEVELS.Length && streamPoints > VIEWERS_LEVELS[viewerIndex]) {
            viewerIndex++;
        }

        //Update View Counter
        int roundedViews = Mathf.RoundToInt(streamPoints);
        viewCounter.text = roundedViews + " viewer" + (roundedViews > 1 ? "s" : "");


        //Update View Panel
        Color panelColor = VIEWERS_COLORS[viewerIndex];
        panelColor.a = 100;
        viewPanel.color = panelColor;

        //Update Money
        timeSinceLastMoneyCheck += Time.deltaTime;
        if (timeSinceLastMoneyCheck >= MONEY_TIME_CHECK) {
            UpdateMoney(viewerIndex);
        }
    }

    public void UpdateStreamPoints(float points) {
        streamPoints += points;
        Color color = points > 0 ? Color.green : Color.red;
        StartCoroutine(StartTextUICoroutine(color, points));
    }

    public void UpdateMoney(int viewerIndex) {
        float moneyProbability = Random.Range(0f, 1f);
        if (moneyProbability >= MONEY_PROBAS[viewerIndex]) {
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
