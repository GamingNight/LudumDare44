using System.Collections;
using UnityEngine;

public class DeathByCollision : MonoBehaviour {

    public Sprite deathSprite;
    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player) {
            spriteRenderer.sprite = deathSprite;
            StartCoroutine(InitiateDestroyCoroutine());
        }
    }

    private IEnumerator InitiateDestroyCoroutine() {

        float totalTime = 4f;
        float currentTime = 0f;
        float step = 0.01f;
        while (currentTime < totalTime) {
            float alpha = Mathf.Lerp(1, 0, currentTime / totalTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return new WaitForSeconds(step);
            currentTime += step;
        }
        Destroy(gameObject);
    }
}
