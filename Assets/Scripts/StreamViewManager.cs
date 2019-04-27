using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StreamViewManager : MonoBehaviour {

    public Slider slider;
    public Canvas carCanvas;
    public GameObject carTextUIPrefab;
    private float streamPoints;

    void Start() {
        streamPoints = 50;
    }

    private void Update() {

        streamPoints = Mathf.Min(slider.maxValue, Mathf.Max(slider.minValue, streamPoints));
        slider.value = streamPoints;
    }

    public void UpdateStreamPoints(float points) {
        streamPoints += points;
        Color color = points > 0 ? Color.green : Color.red;
        StartCoroutine(StartTextUICoroutine(color, points));
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
