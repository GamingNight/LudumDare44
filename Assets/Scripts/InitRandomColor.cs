using UnityEngine;

public class InitRandomColor : MonoBehaviour {
    public Color[] colors;

    void Start() {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }
}
